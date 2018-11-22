﻿using JudBizz;
using JudRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JudGui
{
    /// <summary>
    /// Interaction logic for UcIttLettersChooseReceivers.xaml
    /// </summary>
    public partial class UcIttLettersChooseReceivers : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public bool result;
        public UserControl UcRight;
        public IttLetterShipping Shipping = new IttLetterShipping(Bizz.StrConnection);
        public List<Contact> ProjectContacts = new List<Contact>();
        public List<Enterprise> ProjectEnterpriseList = new List<Enterprise>();
        public List<IndexableLegalEntity> IndexableLegalEntities = new List<IndexableLegalEntity>();
        public List<SubEntrepeneur> ProjectSubEntrepeneurs = new List<SubEntrepeneur>();

        #endregion

        #region Constructors
        public UcIttLettersChooseReceivers(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
            GenerateComboBoxCaseIdItems();
        }

        #endregion

        #region Buttons
        private void ButtonChoose_Click(object sender, RoutedEventArgs e)
        {
            result = false;
            bool receivers = false;

            if (ListBoxLegalEntities.SelectedItems.Count >= 1)
            {
                receivers = true;
            }
            switch (receivers)
            {
                case false:
                    //Show Confirmation
                    MessageBox.Show("Du har ikke valgt nogen modtagere. Der blev ikke føjet modtagere til modtagerlisten.", "Tilføj Modtager(e)", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case true:
                    AddReceivers();
                    break;
            }

            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Modtager(e)n(ne) blev føjet til modtagerlisten.", "Tilføj Modtager(e)", MessageBoxButton.OK, MessageBoxImage.Information);

                //Reset Boxes
                ComboBoxCaseId.SelectedIndex = -1;

                //Update lists and fields
                Bizz.UpdateIttLetterReceivers();
                Bizz.UpdateIttLetterShippingList();
                Shipping = new IttLetterShipping(Bizz.StrConnection);
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Modtager(e)n(ne) blev ikke føjet til modtagerlisten. Prøv igen.", "Tilføj Modtager(e)", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ButtonClearAll_Click(object sender, RoutedEventArgs e)
        {
            ListBoxLegalEntities.UnselectAll();
        }

        private void ButtonChoseAll_Click(object sender, RoutedEventArgs e)
        {
            ListBoxLegalEntities.SelectAll();
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            //Warning about lost changes before closing
            if (MessageBox.Show("Vil du lukke Vælg Modtagere?", "Luk Vælg Modtagere", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Close right UserControl
                Bizz.UcRightActive = false;
                UcRight.Content = new UserControl();
            }
        }

        #endregion

        #region Events
        private void ComboBoxCaseId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ComboBoxCaseId.SelectedIndex;
            if (selectedIndex >= 0)
            {
                foreach (IndexableProject temp in Bizz.ActiveProjects)
                {
                    if (temp.Index == selectedIndex)
                    {
                        Bizz.TempProject = new Project(Bizz.StrConnection, temp.Id, temp.CaseId, temp.Name, temp.Builder, temp.Status, temp.TenderForm, temp.EnterpriseForm, temp.Executive, temp.EnterpriseList, temp.Copy);
                        break;
                    }
                }
                TextBoxName.Text = Bizz.TempProject.Name;
                GetProjectSubEntrepeneurs();
                GetIndexableLegalEntities();
                ListBoxLegalEntities.ItemsSource = "";
                ListBoxLegalEntities.ItemsSource = IndexableLegalEntities;
            }
            else
            {
                TextBoxName.Text = "";
                ProjectSubEntrepeneurs.Clear();
                ProjectEnterpriseList.Clear();
                IndexableLegalEntities.Clear();
                ListBoxLegalEntities.ItemsSource = "";
            }
        }

        private void ListBoxLegalEntities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedItemsCount = ListBoxLegalEntities.SelectedItems.Count;
            switch (selectedItemsCount)
            {
                case 1:
                    int selectedIndex = ListBoxLegalEntities.SelectedIndex;
                    foreach (IndexableLegalEntity temp in IndexableLegalEntities)
                    {
                        if (temp.Index == selectedIndex)
                        {
                            Bizz.TempLegalEntity = temp;
                            Bizz.TempSubEntrepeneur = GetSubEntrepeneur(Bizz.TempLegalEntity.Id);
                            if (!Bizz.TempSubEntrepeneur.Active)
                            {
                                Bizz.TempSubEntrepeneur.ToggleActive();
                            }
                        }
                    }
                    break;
                default:
                    Bizz.TempLegalEntity = null;
                    Bizz.TempSubEntrepeneur = null;
                    break;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that adds multiple Receivers to Db
        /// </summary>
        /// <returns></returns>
        private void AddReceivers()
        {
            IttLetterReceiver tempReceiver;
            foreach (IndexableLegalEntity entity in ListBoxLegalEntities.SelectedItems)
            {
                tempReceiver = FillIttLetterReceiver(entity);
                //Code that ads a enterprise to Enterprise List
                bool tempResult = Bizz.CIR.CreateIttLetterReceiverInDb(tempReceiver);

                //Code, that checks result
                if (!result)
                {
                    result = tempResult;
                }
            }
        }

        /// <summary>
        /// Method, that checks, whether a LegalEntity exists in IttLetter Receivers list
        /// </summary>
        /// <param name="entity">LegalEntity</param>
        /// <returns>bool</returns>
        private bool CheckEntityIttLetterReceivers(LegalEntity entity)
        {
            bool result = false;
            foreach (IttLetterReceiver receiver in Bizz.IttLetterReceivers)
            {
                if (receiver.CompanyId == entity.Id)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Method, that checks, whether a LegalEntity exists in tempResult list
        /// </summary>
        /// <param name="tempEntity">LegalEntity</param>
        /// <param name="sub">SubEntrepeneur</param>
        /// <param name="List<LegalEntity>"></param>
        /// <returns></returns>
        private bool CheckEntityTempResult(LegalEntity tempEntity, List<LegalEntity> tempResult)
        {
            bool exist = false;
            foreach (LegalEntity entity in tempResult)
            {
                if (entity.Id == tempEntity.Id)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        /// <summary>
        /// Method, that adds data to an ITT-letter receiver
        /// </summary>
        /// <param name="entity">IndexableLegalEntity</param>
        /// <returns>IttLetterReceiver</returns>
        private IttLetterReceiver FillIttLetterReceiver(IndexableLegalEntity entity)
        {
            GetIttLetterShipping();
            int shippingId = Shipping.Id;
            int project = Shipping.Project;
            string companyId = entity.Id;
            string companyName = entity.Name;
            Contact contact = GetContact(entity.Id);
            string attention = contact.Name;
            Address address = GetAddress(entity.Address);
            string street = address.Street;
            string place = address.Place;
            ZipTown zipTown = address.GetZipTown(address.Zip);
            string zip = zipTown.ToString();
            string email = GetContactEmail(contact.ContactInfo);

            IttLetterReceiver result = new IttLetterReceiver(Bizz.StrConnection, shippingId, project, companyId, companyName, attention, street, zip, email, place);

            return result;

        }

        /// <summary>
        /// Method, that generates Items for ComboBoxCaseId
        /// </summary>
        private void GenerateComboBoxCaseIdItems()
        {
            ComboBoxCaseId.Items.Clear();
            foreach (IndexableProject temp in Bizz.ActiveProjects)
            {
                ComboBoxCaseId.Items.Add(temp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>Address</returns>
        private Address GetAddress(int id)
        {
            Address result = new Address(Bizz.StrConnection);
            foreach (Address temp in Bizz.Addresses)
            {
                if (temp.Id == id)
                {
                    result = temp;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Method, that returns a Contact
        /// </summary>
        /// <param name="entrepeneur">string</param>
        /// <returns>Contact</returns>
        private Contact GetContact(string entrepeneur)
        {
            SubEntrepeneur sub = GetSubEntrepeneur(entrepeneur);
            Contact contact = GetContactFromList(sub.Contact);
            return contact;
        }

        /// <summary>
        /// Method, that retrieves a Contact from Contact List
        /// </summary>
        /// <param name="sub">int</param>
        /// <returns>Contact</returns>
        private Contact GetContactFromList(int id)
        {
            Contact result = new Contact(Bizz.StrConnection);
            foreach (Contact contact in Bizz.Contacts)
            {
                if (contact.Id == id)
                {
                    result = contact;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Method, that finds an email address in list
        /// </summary>
        /// <returns>string</returns>
        private string GetContactEmail(int id)
        {
            string result = "";
            foreach (ContactInfo info in Bizz.ContactInfoList)
            {
                if (info.Id == id)
                {
                    result = info.Email;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Method, that creates an indexable EnterpriseList
        /// </summary>
        /// <returns>List<IndexableEnterprise></returns>
        private List<Enterprise> GetIndexableEnterpriseList()
        {
            List<Enterprise> result = new List<Enterprise>();
            int i = 0;
            foreach (Enterprise enterprise in ProjectEnterpriseList)
            {
                IndexableEnterprise temp = new IndexableEnterprise(Bizz.StrConnection, i, enterprise);
                result.Add(temp);
                i++;
            }
            return result;
        }

        /// <summary>
        /// Method that creates a list of indexable Legal Entities
        /// </summary>
        /// <returns>List<IndexableLegalEntity></returns>
        private void GetIndexableLegalEntities()
        {
            List<LegalEntity> tempResult = new List<LegalEntity>();
            foreach (SubEntrepeneur sub in ProjectSubEntrepeneurs)
            {
                this.Bizz.UpdateIttLetterReceivers();
                foreach (LegalEntity tempEntity in Bizz.LegalEntities)
                {
                    if (tempEntity.Id == sub.Entrepeneur)
                    {
                        bool result = CheckEntityTempResult(tempEntity, tempResult);
                        bool exist = CheckEntityIttLetterReceivers(tempEntity);
                        if (!result && !exist)
                        {
                            tempResult.Add(tempEntity);
                            break;
                        }
                    }
                }
            }
            int i = 0;
            IndexableLegalEntities.Clear();
            foreach (LegalEntity entity in tempResult)
            {
                bool result = CheckEntityIttLetterReceivers(entity);
                if (!result)
                {
                    IndexableLegalEntity temp = new IndexableLegalEntity(Bizz.StrConnection, i, entity);
                    IndexableLegalEntities.Add(temp);
                    i++;
                }
            }
            ListBoxLegalEntities.ItemsSource = "";
            ListBoxLegalEntities.ItemsSource = IndexableLegalEntities;
        }

        /// <summary>
        /// Method, that creates a IttLetterShipping
        /// </summary>
        /// <returns></returns>
        private void GetIttLetterShipping()
        {
            Shipping = new IttLetterShipping(Bizz.StrConnection, Bizz.TempProject.Id, @"PDF_Documents\", Bizz.MacAdresss);
            try
            {
                int id = Bizz.CIS.CreateIttLetterShippingInDb(Bizz.MacAdresss, Bizz.IttLetterShippingList, Shipping);
                Shipping.SetId(id);
                Shipping.PdfPath = "";
                Shipping.UpdateIttLetterShipping(Shipping);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Databasen returnerede en fejl. Forsendelsen blev ikke opdateret.\n" + ex, "Opdater forsendelse", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method, that generates List of ProjectSubEntrepeneurs
        /// </summary>
        /// <returns></returns>
        private void GetProjectSubEntrepeneurs()
        {
            ProjectEnterpriseList.Clear();
            ProjectSubEntrepeneurs.Clear();
            foreach (Enterprise enterprise in Bizz.EnterpriseList)
            {
                if (enterprise.Project == Bizz.TempProject.Id)
                {
                    ProjectEnterpriseList.Add(enterprise);
                    foreach (SubEntrepeneur sub in Bizz.SubEntrepeneurs)
                    {
                        if (sub.EnterpriseList == enterprise.Id)
                        {
                            ProjectSubEntrepeneurs.Add(sub);
                        }
                    }
                }
            }
        }

        private SubEntrepeneur GetSubEntrepeneur(string entrepeneur)
        {
            SubEntrepeneur tempSub = new SubEntrepeneur(Bizz.StrConnection, Bizz.LegalEntities);
            foreach (SubEntrepeneur sub in ProjectSubEntrepeneurs)
            {
                if (sub.Entrepeneur == entrepeneur)
                {
                    tempSub = sub;
                }
            }
            return tempSub;
        }

        #endregion

    }
}
