using JudBizz;
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
        public UserControl UcRight;
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
            bool result = false;
            if (ListBoxLegalEntities.SelectedItems.Count == 0)
            {
                //Show Confirmation
                MessageBox.Show("Du har ikke valgt nogen modtagere.", "Vælg Modtagere", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (ListBoxLegalEntities.SelectedItems.Count == 1)
            {

                LegalEntity entity = new LegalEntity((LegalEntity)ListBoxLegalEntities.SelectedItem);
                IttLetterReceiver tempIttLetterReceiver = new IttLetterReceiver();

                //Code that adds a infor to a temp Receiver
                tempIttLetterReceiver = new IttLetterReceiver();
                tempIttLetterReceiver.CompanyId = entity.Id;
                tempIttLetterReceiver.ShippingId = GetIttLetterShippingId();
                tempIttLetterReceiver.CompanyName = entity.Name;
                Address tempAddress = GetAddress(entity.Address);
                tempIttLetterReceiver.Street = tempAddress.Street;
                tempIttLetterReceiver.Place = tempAddress.Place;
                ZipTown tempZipTown = tempAddress.GetZipTown(tempAddress.Zip);
                tempIttLetterReceiver.Zip = tempZipTown.ToString();
                string tempEmail = GetContactEmail(entity.Id);
                tempIttLetterReceiver.Email = tempEmail;

                //Code that ads a Receiver to IttLetterReceivers in dB
                bool tempResult = Bizz.CIR.UpdateIttLetterIttLetterReceiver(tempIttLetterReceiver);

                if (!result)
                {
                    result = tempResult;
                }
            }
            else
            {
                bool tempResult = AddMultipleIttLetterReceivers();
                if (!result)
                {
                    result = tempResult;
                }
            }
            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Modtager(e)n(ne) blev føjet til modtagerlisten.", "Tilføj Modtager(e)", MessageBoxButton.OK, MessageBoxImage.Information);

                //Reset Boxes
                TextBoxName.Text = "";

                //Update Enterprise List
                Bizz.LegalEntities.Clear();
                Bizz.LegalEntities = Bizz.CLE.GetLegalEntities();
                IndexableLegalEntities.Clear();
                GetIndexableLegalEntities();
                ListBoxLegalEntities.ItemsSource = IndexableLegalEntities;
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Modtager(e)n(ne) blev ikke føjet til modtagerlisten. Prøv igen.", "Tilføj Modtager(e)", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
            foreach (IndexableProject temp in Bizz.ActiveProjects)
            {
                if (temp.Index == selectedIndex)
                {
                    Bizz.TempProject = new Project(temp.Id, temp.CaseId, temp.Name, temp.Builder, temp.Status, temp.TenderForm, temp.EnterpriseForm, temp.Executive, temp.EnterpriseList, temp.Copy);
                }
            }
            TextBoxName.Text = Bizz.TempProject.Name;
            GetProjectSubEntrepeneurs();
            GetIndexableLegalEntities();
            ListBoxLegalEntities.ItemsSource = IndexableLegalEntities;
        }

        private void ListBoxLegalEntities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxLegalEntities.SelectedItems.Count < 2)
            {
                int selectedIndex = ListBoxLegalEntities.SelectedIndex;
                foreach (IndexableLegalEntity temp in IndexableLegalEntities)
                {
                    if (temp.Index == selectedIndex)
                    {
                        Bizz.TempLegalEntity = temp;
                        Bizz.TempSubEntrepeneur = new SubEntrepeneur(Bizz.LegalEntities);
                        Bizz.TempSubEntrepeneur.EnterpriseList = Bizz.TempEnterprise.Id;
                        Bizz.TempSubEntrepeneur.Entrepeneur = temp.Id;
                        if (!Bizz.TempSubEntrepeneur.Active)
                        {
                            Bizz.TempSubEntrepeneur.ToggleActive();
                        }
                    }
                }
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that adds multiple IttLetterReceivers to Db
        /// </summary>
        /// <returns></returns>
        private bool AddMultipleIttLetterReceivers()
        {
            bool result = false;
            IttLetterReceiver tempIttLetterReceiver = new IttLetterReceiver();
            foreach (IndexableLegalEntity entity in ListBoxLegalEntities.SelectedItems)
            {
                tempIttLetterReceiver = new IttLetterReceiver();
                tempIttLetterReceiver.CompanyId = entity.Id;
                tempIttLetterReceiver.CompanyName = entity.Name;
                Address tempAddress = GetAddress(entity.Address);
                tempIttLetterReceiver.Street = tempAddress.Street;
                tempIttLetterReceiver.Place = tempAddress.Place;
                ZipTown tempZipTown = tempAddress.GetZipTown(tempAddress.Zip);
                tempIttLetterReceiver.Zip = tempZipTown.ToString();
                string tempEmail = GetContactEmail(entity.Id);
                tempIttLetterReceiver.Email = tempEmail;

                //Code that ads a enterprise to Enterprise List
                bool tempResult = Bizz.CIR.UpdateIttLetterIttLetterReceiver(tempIttLetterReceiver);

                //Code, that checks result
                if (!result)
                {
                    result = tempResult;
                }
            }

            return result;
        }

        /// <summary>
        /// Method, that checks, whether a LegalEntity exists in in a list
        /// </summary>
        /// <param name="entity">LegalEntity</param>
        /// <returns>bool</returns>
        private bool CheckEntity(LegalEntity entity, List<LegalEntity> tempResult)
        {
            bool result = false;
            foreach (LegalEntity sub in tempResult)
            {
                if (sub.Id == entity.Id)
                {
                    result = true;
                    break;
                }
            }
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
            Address result = new Address();
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
        /// Method, that finds a Contact in list
        /// </summary>
        /// <param name="entrepeneur">int</param>
        /// <returns>Contact</returns>
        private Contact GetContact(string entrepeneur)
        {
            Contact result = new Contact();
            bool resultFound = false;
            foreach (Enterprise enterprise in ProjectEnterpriseList)
            {
                if (enterprise.Project == Bizz.TempProject.Id && !resultFound)
                {
                    foreach (SubEntrepeneur sub in ProjectSubEntrepeneurs)
                    {
                        if (sub.Entrepeneur == entrepeneur && sub.EnterpriseList == enterprise.Id && !resultFound)
                        {
                            foreach (Contact contact in Bizz.Contacts)
                            {
                                if (contact.Id == sub.Contact)
                                {
                                    result = contact;
                                    resultFound = true;
                                    break;
                                }
                            }
                        }
                        if (resultFound)
                        {
                            break;
                        }
                    }
                }
                if (resultFound)
                {
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Method, that finds an email address in list
        /// </summary>
        /// <returns>string</returns>
        private string GetContactEmail(string entrepeneur)
        {
            string result = "";
            Contact temp = GetContact(entrepeneur);
            foreach (ContactInfo info in Bizz.ContactInfoList)
            {
                if (info.Id == temp.ContactInfo)
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
            foreach (Enterprise enterprise in Bizz.EnterpriseList)
            {
                if (enterprise.Project == Bizz.TempProject.Id)
                {
                    IndexableEnterprise temp = new IndexableEnterprise(i, enterprise);
                    result.Add(temp);
                    i++;
                }
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
            List<IndexableLegalEntity> result = new List<IndexableLegalEntity>();
            IndexableLegalEntity temp = new IndexableLegalEntity(0, Bizz.LegalEntities[0]);
            result.Add(temp);
            foreach (Enterprise enterprise in Bizz.EnterpriseList)
            {
                ProjectEnterpriseList.Add(enterprise);
                foreach (LegalEntity entity in Bizz.LegalEntities)
                {
                    if (!CheckEntity(entity, tempResult))
                    {
                        tempResult.Add(entity);
                    }
                }
            }
            int i = 1;
            foreach (LegalEntity sub in tempResult)
            {
                temp = new IndexableLegalEntity(i, sub);
                result.Add(temp);
                i++;
            }
            IndexableLegalEntities = result;
        }

        /// <summary>
        /// Method, that creates a IttLetterShipping
        /// </summary>
        /// <returns></returns>
        private int GetIttLetterShippingId()
        {
            int result = 0;
            IttLetterShipping shipping = new IttLetterShipping(@"PDF_Documents\", Bizz.MacAdresss);
            result = Bizz.CIS.CreateIttLetterShippingInDb(Bizz, shipping);
            shipping.SetId(result);
            bool dbAnswer = Bizz.CIS.UpdateIttLetterShipping(shipping);
            if (!dbAnswer)
            {
                MessageBox.Show("Databasen returnerede en fejl. Forsendelsen blev ikke opdateret.", "Opdater forsendelse", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        /// <summary>
        /// Method, that generates List of ProjectSubEntrepeneurs
        /// </summary>
        /// <param name="id">int</param>
        /// <returns></returns>
        private void GetProjectSubEntrepeneurs()
        {
            List<SubEntrepeneur> result = new List<SubEntrepeneur>();
            foreach (Enterprise enterprise in Bizz.EnterpriseList)
            {
                if (enterprise.Project == Bizz.TempProject.Id)
                {
                    foreach (SubEntrepeneur sub in Bizz.SubEntrepeneurs)
                    {
                        if (sub.EnterpriseList == enterprise.Id)
                        {
                            result.Add(sub);
                        }
                    }
                }
            }
            ProjectSubEntrepeneurs = result;
        }

        #endregion

    }
}
