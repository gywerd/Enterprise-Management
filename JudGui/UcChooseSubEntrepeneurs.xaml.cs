using JudBizz;
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
    /// Interaction logic for UcChooseSubEntrepeneurs.xaml
    /// </summary>
    public partial class UcChooseSubEntrepeneurs : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;
        public List<IndexableContact> IndexableContacts = new List<IndexableContact>();
        public List<Enterprise> IndexableEnterpriseList = new List<Enterprise>();
        public List<IndexableLegalEntity> IndexableLegalEntities = new List<IndexableLegalEntity>();

        #endregion

        #region Constructors
        public UcChooseSubEntrepeneurs(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
            GenerateComboBoxCaseIdItems();
            ComboBoxArea.ItemsSource = Bizz.Regions;
        }

        #endregion

        #region Buttons
        private void ButtonChoose_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            if (ListBoxLegalEntities.SelectedItems.Count == 0)
            {
                //Show Confirmation
                MessageBox.Show("Du har ikke valgt nogen underentrepenører.", "Vælg Underentrepenør", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (ListBoxLegalEntities.SelectedItems.Count == 1)
            {
                //Code that adds IttLetter, Offer and Request to Bizz.TempSubEntrepeneur
                CreateIttLetter();
                CreateOffer();
                CreateRequest();
                Contact tempContact = GetContact();
                Bizz.TempSubEntrepeneur.Contact = tempContact.Id;

                //Code that adds a SubEntrepeneur to Enterprise List
                result = Bizz.CSE.InsertIntoSubEntrepeneurs(Bizz.TempSubEntrepeneur);
            }
            else
            {
                result = AddMultipleSubentrepeneurs();
            }
            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Underentrepenøre(r)n(e) blev føjet til Entrepriselisten. Ved flere underentrepenører, er der ikke valgt kontaktperson. Ret dette under 'Rediger Underentrepenør'", "Vælg Underentrepenør", MessageBoxButton.OK, MessageBoxImage.Information);

                //Reset Boxes
                TextBoxName.Text = "";

                //Update Enterprise List
                Bizz.SubEntrepeneurs.Clear();
                Bizz.SubEntrepeneurs = Bizz.CSE.GetSubEntrepeneurs();
                IndexableLegalEntities.Clear();
                IndexableLegalEntities = GetIndexableLegalEntities();
                ListBoxLegalEntities.ItemsSource = IndexableLegalEntities;
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Underentrepenøren blev ikke føjet til Entrepriselisten. Prøv igen.", "Rediger Projekt", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            //Warning about lost changes before closing
            if (MessageBox.Show("Vil du lukke Vælg Underentrepenør?", "Luk Vælg Underentrepenør", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
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
                    Bizz.TempProject = new Project(Bizz.StrConnection, temp.Id, temp.CaseId, temp.Name, temp.Builder, temp.Status, temp.TenderForm, temp.EnterpriseForm, temp.Executive, temp.EnterpriseList, temp.Copy);
                }
            }
            TextBoxName.Text = Bizz.TempProject.Name;
            IndexableEnterpriseList = GetIndexableEnterpriseList();
            ComboBoxEnterprise.ItemsSource = IndexableEnterpriseList;
        }

        private void ComboBoxContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxEnterprise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ComboBoxEnterprise.SelectedIndex;
            if (IndexableEnterpriseList.Count == 0)
            {
                IndexableEnterpriseList = GetIndexableEnterpriseList();
            }
            foreach (IndexableEnterprise temp in IndexableEnterpriseList)
            {
                if (temp.Index == selectedIndex)
                {
                    Bizz.TempEnterprise = new Enterprise(Bizz.StrConnection, temp.Id, temp.Project, temp.Name, temp.Elaboration, temp.OfferList, temp.CraftGroup1, temp.CraftGroup2, temp.CraftGroup3, temp.CraftGroup4);
                    break;
                }
            }
            Bizz.LegalEntities.Clear();
            Bizz.LegalEntities = Bizz.CLE.GetLegalEntities();
            IndexableLegalEntities.Clear();
            IndexableLegalEntities = GetIndexableLegalEntities();
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
                        Bizz.TempSubEntrepeneur = new SubEntrepeneur(Bizz.StrConnection, Bizz.LegalEntities);
                        Bizz.TempSubEntrepeneur.EnterpriseList = Bizz.TempEnterprise.Id;
                        Bizz.TempSubEntrepeneur.Entrepeneur = temp.Id;
                        if (!Bizz.TempSubEntrepeneur.Active)
                        {
                            Bizz.TempSubEntrepeneur.ToggleActive();
                        }
                    }
                }
                Bizz.Contacts.Clear();
                Bizz.Contacts = Bizz.CCP.GetContacts();
                IndexableContacts.Clear();
                IndexableContacts = GetIndexableContacts();
                //ListBoxLegalEntities.ItemsSource = IndexableLegalEntities;
                ComboBoxContact.ItemsSource = IndexableContacts;
                ComboBoxContact.SelectedIndex = 0;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that adds multiple SubEntrepeneurs to Db
        /// </summary>
        /// <returns></returns>
        private bool AddMultipleSubentrepeneurs()
        {
            bool result = false;
            List<IndexableLegalEntity> tempList = new List<IndexableLegalEntity>();
            foreach (IndexableLegalEntity entity in ListBoxLegalEntities.SelectedItems)
            {
                Bizz.TempLegalEntity = entity;
                Bizz.TempSubEntrepeneur = new SubEntrepeneur(Bizz.StrConnection, Bizz.LegalEntities);
                Bizz.TempSubEntrepeneur.EnterpriseList = Bizz.TempEnterprise.Id;
                Bizz.TempSubEntrepeneur.Entrepeneur = entity.Id;
                if (!Bizz.TempSubEntrepeneur.Active)
                {
                    Bizz.TempSubEntrepeneur.ToggleActive();
                }
                Bizz.TempSubEntrepeneur.Contact = 0;
                CreateIttLetter();
                CreateOffer();
                CreateRequest();

                //Code that ads a enterprise to Enterprise List
                bool tempResult = Bizz.CSE.InsertIntoSubEntrepeneurs(Bizz.TempSubEntrepeneur);

                //Code, that checks result
                if (!result)
                {
                    result = tempResult;
                }
            }

            return result;
        }

        /// <summary>
        /// Method that compares CraftGroups in LegalEntities and EnterpriseList
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool CheckCraftGroups(LegalEntity entity)
        {
            if (entity.CraftGroup1 != 0)
            {
                if (entity.CraftGroup1 == Bizz.TempEnterprise.CraftGroup1 || entity.CraftGroup1 == Bizz.TempEnterprise.CraftGroup2 || entity.CraftGroup1 == Bizz.TempEnterprise.CraftGroup3 || entity.CraftGroup1 == Bizz.TempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            if (entity.CraftGroup2 != 0)
            {
                if (entity.CraftGroup2 == Bizz.TempEnterprise.CraftGroup1 || entity.CraftGroup2 == Bizz.TempEnterprise.CraftGroup2 || entity.CraftGroup2 == Bizz.TempEnterprise.CraftGroup3 || entity.CraftGroup2 == Bizz.TempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            if (entity.CraftGroup3 != 0)
            {
                if (entity.CraftGroup3 == Bizz.TempEnterprise.CraftGroup1 || entity.CraftGroup3 == Bizz.TempEnterprise.CraftGroup2 || entity.CraftGroup3 == Bizz.TempEnterprise.CraftGroup3 || entity.CraftGroup3 == Bizz.TempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            if (entity.CraftGroup4 != 0)
            {
                if (entity.CraftGroup4 == Bizz.TempEnterprise.CraftGroup1 || entity.CraftGroup4 == Bizz.TempEnterprise.CraftGroup2 || entity.CraftGroup4 == Bizz.TempEnterprise.CraftGroup3 || entity.CraftGroup4 == Bizz.TempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method, that inserts an Offer to Db, and register it in Bizz.tempSubentrepeneur
        /// </summary>
        private void CreateIttLetter()
        {
            Bizz.TempIttLetter = new IttLetter(Bizz.StrConnection);
            int id = Bizz.CIL.CreateIttLetterInDb(Bizz.TempIttLetter);
            Bizz.TempIttLetter.SetId(id);
            Bizz.TempSubEntrepeneur.IttLetter = id;
        }

        /// <summary>
        /// Method, that inserts an Offer to Db, and register it in Bizz.tempSubentrepeneur
        /// </summary>
        private void CreateOffer()
        {
            Bizz.TempOffer = new Offer(Bizz.StrConnection);
            int id = Bizz.COF.CreateOfferInDb(Bizz.TempOffer);
            Bizz.TempOffer.SetId(id);
            Bizz.TempSubEntrepeneur.Offer = id;
        }

        /// <summary>
        /// Method, that inserts a Request to Db, and register it in Bizz.tempSubentrepeneur
        /// </summary>
        private void CreateRequest()
        {
            Bizz.TempRequest = new Request(Bizz.StrConnection);
            int id = Bizz.CRQ.CreateRequestInDb(Bizz.TempRequest);
            Bizz.TempRequest.SetId(id);
            Bizz.TempSubEntrepeneur.Request = id;
        }

        /// <summary>
        /// Method, that filters existing Legal Entities in SubEntrepeneurs from list of indexable Legal Entities
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private List<IndexableLegalEntity> FilterIndexableLegalEntities(List<LegalEntity> list)
        {
            List<LegalEntity> tempResult = new List<LegalEntity>();
            List<IndexableLegalEntity> result = new List<IndexableLegalEntity>();
            LegalEntity tempEntity = new LegalEntity(Bizz.StrConnection);
            foreach (LegalEntity entity in list)
            {
                if (entity.Region == ComboBoxArea.SelectedIndex)
                {
                    tempResult.Add(entity);
                }
            }
            foreach (LegalEntity entity in tempResult)
            {
                if (entity.Region != ComboBoxArea.SelectedIndex && entity.CountryWide.Equals(true))
                {
                    tempResult.Add(entity);
                }
            }
            int i = 0;
            foreach (LegalEntity temp in tempResult)
            {
                if (!IdExistsInSubEntrepeneurs(Bizz.TempEnterprise.Id, temp.Id))
                {
                    IndexableLegalEntity entity = new IndexableLegalEntity(Bizz.StrConnection, i, temp);
                    result.Add(entity);
                    i++;
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
        /// Method, that generates Items for ComboBoxContact
        /// </summary>
        private void GenerateContactItems()
        {
            ComboBoxContact.Items.Clear();
            IndexableContacts = GetIndexableContacts();
            foreach (IndexableContact temp in IndexableContacts)
            {
                    ComboBoxContact.Items.Add(temp);
            }
        }

        /// <summary>
        /// Method, that generates Items for ComboBoxEnterprise
        /// </summary>
        private void GenerateEnterpriseItems()
        {
            ComboBoxEnterprise.Items.Clear();
            IndexableEnterpriseList = GetIndexableEnterpriseList();
            foreach (Enterprise temp in IndexableEnterpriseList)
            {
                    ComboBoxEnterprise.Items.Add(temp);
            }
        }

        /// <summary>
        /// Method, that finds a Contact
        /// </summary>
        /// <returns>Contact</returns>
        private Contact GetContact()
        {
            Contact result = new Contact(Bizz.StrConnection);
            int index = ComboBoxContact.SelectedIndex;
            if (IndexableContacts.Count == 0)
            {
                GetIndexableContacts();
            }
            foreach (IndexableContact tempContact in IndexableContacts)
            {
                if (tempContact.Index == index)
                {
                    return tempContact;
                }
            }
            return result;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method that creates a list of indexable Contacts
        /// </summary>
        /// <returns>List<IndexableSubEntrepeneur></returns>
        private List<IndexableContact> GetIndexableContacts()
        {
            List<IndexableContact> result = new List<IndexableContact>();
            IndexableContact iContact = new IndexableContact(Bizz.StrConnection, 0, Bizz.Contacts[0]);
            result.Add(iContact);
            int i = 1;
            foreach (Contact contact in Bizz.Contacts)
            {
                if (contact.LegalEntity == Bizz.TempLegalEntity.Id)
                {
                    IndexableContact temp = new IndexableContact(Bizz.StrConnection, i, contact);
                    result.Add(temp);
                    i++;
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
                    IndexableEnterprise temp = new IndexableEnterprise(Bizz.StrConnection, i, enterprise);
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
        private List<IndexableLegalEntity> GetIndexableLegalEntities()
        {
            List<LegalEntity> tempResult = new List<LegalEntity>();
            List<IndexableLegalEntity> result = new List<IndexableLegalEntity>();
            IndexableLegalEntity temp = new IndexableLegalEntity(Bizz.StrConnection, 0, Bizz.LegalEntities[0]);
            result.Add(temp);
            int i = 1;
            foreach (LegalEntity entity in Bizz.LegalEntities)
            {
                if (CheckCraftGroups(entity))
                {
                    tempResult.Add(entity);
                    i++;
                }
            }
            result = FilterIndexableLegalEntities(tempResult);
            return result;
        }

        /// <summary>
        /// <summary>
        /// Method, that checks if a legal entity is already added to SubEntrepeneurs
        /// </summary>
        /// <param name="enterpriseList">int</param>
        /// <param name="entrepeneur">string</param>
        /// <returns>bool</returns>
        private bool IdExistsInSubEntrepeneurs(int enterpriseList, string entrepeneur)
        {

            foreach (SubEntrepeneur temp in Bizz.SubEntrepeneurs)
            {
                if (temp.Entrepeneur == entrepeneur && temp.EnterpriseList == enterpriseList)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

    }
}
