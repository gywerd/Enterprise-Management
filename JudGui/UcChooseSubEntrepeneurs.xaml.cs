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
    /// Interaction logic for UcChooseSubEntrepeneurs.xaml
    /// </summary>
    public partial class UcChooseSubEntrepeneurs : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;
        public List<IndexableContact> IndexableContacts = new List<IndexableContact>();
        public List<IndexableEnterprise> IndexableEnterpriseList = new List<IndexableEnterprise>();
        public List<IndexableLegalEntity> IndexableLegalEntities = new List<IndexableLegalEntity>();
        public List<IndexableSubEntrepeneur> IndexableSubEntrepeneurs = new List<IndexableSubEntrepeneur>();

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
            //Code that ads a enterprise to Enterprise List
            bool result = Bizz.CSE.InsertIntoSubEntrepeneurs(Bizz.tempSubEntrepeneur);

            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Underentrepenøren blev føjet til Entrepriselisten", "Vælg Underentrepenør", MessageBoxButton.OK, MessageBoxImage.Information);

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
                    Bizz.tempProject = new Project(temp.Id, temp.CaseId, temp.Name, temp.Builder, temp.Status, temp.TenderForm, temp.EnterpriseForm, temp.Executive, temp.EnterpriseList, temp.Copy);
                }
            }
            TextBoxName.Text = Bizz.tempProject.Name;
            IndexableEnterpriseList = GetIndexableEnterpriseList();
            ComboBoxEnterprise.ItemsSource = IndexableEnterpriseList;
        }

        private void ComboBoxContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxEnterprise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ComboBoxEnterprise.SelectedIndex;
            foreach (IndexableEnterprise temp in IndexableEnterpriseList)
            {
                if (temp.Index == selectedIndex)
                {
                    Bizz.tempEnterprise = new Enterprise(temp.Id, temp.Project, temp.Name, temp.Elaboration, temp.OfferList, temp.CraftGroup1, temp.CraftGroup2, temp.CraftGroup3, temp.CraftGroup4);
                }
            }
            IndexableLegalEntities = GetIndexableLegalEntities();
            ListBoxLegalEntities.ItemsSource = IndexableLegalEntities;
        }

        private void ListBoxLegalEntities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ComboBoxEnterprise.SelectedIndex;
            foreach (IndexableLegalEntity temp in IndexableLegalEntities)
            {
                if (temp.Index == selectedIndex)
                {
                    Bizz.tempLegalEntity = temp;
                    Bizz.tempSubEntrepeneur = new SubEntrepeneur();
                    Bizz.tempSubEntrepeneur.EnterpriseList = Bizz.tempEnterprise.Id;
                    Bizz.tempSubEntrepeneur.Entrepeneur = temp.Id;
                    if (!Bizz.tempSubEntrepeneur.Active)
                    {
                        Bizz.tempSubEntrepeneur.ToggleActive();
                    }
                }
            }
            IndexableContacts = GetIndexableContacts();
            ListBoxLegalEntities.ItemsSource = IndexableLegalEntities;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method that compares CraftGroups in LegalEntities and EnterpriseList
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool CheckCraftGroups(LegalEntity entity)
        {
            if (entity.CraftGroup4 != 0)
            {
                if (entity.CraftGroup4 == Bizz.tempEnterprise.CraftGroup1 || entity.CraftGroup4 == Bizz.tempEnterprise.CraftGroup2 || entity.CraftGroup4 == Bizz.tempEnterprise.CraftGroup3 || entity.CraftGroup4 == Bizz.tempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            else if (entity.CraftGroup3 != 0)
            {
                if (entity.CraftGroup3 == Bizz.tempEnterprise.CraftGroup1 || entity.CraftGroup3 == Bizz.tempEnterprise.CraftGroup2 || entity.CraftGroup3 == Bizz.tempEnterprise.CraftGroup3 || entity.CraftGroup3 == Bizz.tempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            else if (entity.CraftGroup2 != 0)
            {
                if (entity.CraftGroup2 == Bizz.tempEnterprise.CraftGroup1 || entity.CraftGroup2 == Bizz.tempEnterprise.CraftGroup2 || entity.CraftGroup2 == Bizz.tempEnterprise.CraftGroup3 || entity.CraftGroup2 == Bizz.tempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            else if (entity.CraftGroup1 != 0)
            {
                if (entity.CraftGroup1 == Bizz.tempEnterprise.CraftGroup1 || entity.CraftGroup1 == Bizz.tempEnterprise.CraftGroup2 || entity.CraftGroup1 == Bizz.tempEnterprise.CraftGroup3 || entity.CraftGroup1 == Bizz.tempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method, that filters existing Legal Entities in SubEntrepeneurs from list of indexable Legal Entities
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private List<IndexableLegalEntity> FilterIndexableLegalEntities(List<IndexableLegalEntity> list)
        {
            List<IndexableLegalEntity> tempResult = new List<IndexableLegalEntity>();
            List<IndexableLegalEntity> result = new List<IndexableLegalEntity>();
            int i = 0;
            foreach (IndexableLegalEntity temp in list)
            {
                if (!IdExistsInSubEntrepeneurs(Bizz.tempEnterprise.Id, temp.Id))
                {
                    LegalEntity legalEntity = new LegalEntity(temp.Id, temp.Name, temp.Address, temp.ContactInfo, temp.Url, temp.CraftGroup1, temp.CraftGroup2, temp.CraftGroup3, temp.CraftGroup4, temp.Region, temp.CountryWide, temp.Cooperative, temp.Active);
                    IndexableLegalEntity entity = new IndexableLegalEntity(i, legalEntity);
                    tempResult.Add(entity);
                }
            }
            foreach (IndexableLegalEntity entity in tempResult)
            {
                if (entity.Region == ComboBoxArea.SelectedIndex)
                {
                    result.Add(entity);
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
            foreach (IndexableEnterprise temp in IndexableEnterpriseList)
            {
                    ComboBoxEnterprise.Items.Add(temp);
            }
        }

        /// <summary>
        /// Method that creates a list of indexable Contacts
        /// </summary>
        /// <returns>List<IndexableSubEntrepeneur></returns>
        private List<IndexableContact> GetIndexableContacts()
        {
            List<IndexableContact> result = new List<IndexableContact>();
            int i = 0;
            foreach (Contact contact in Bizz.Contacts)
            {
                if (contact.LegalEntity == Bizz.tempLegalEntity.Id)
                {
                    IndexableContact temp = new IndexableContact(i, contact);
                    result.Add(temp);
                    i++;
                }
            }
            return result;
        }

        private List<IndexableEnterprise> GetIndexableEnterpriseList()
        {
            List<IndexableEnterprise> result = new List<IndexableEnterprise>();
            int i = 0;
            foreach (Enterprise enterprise in Bizz.EnterpriseList)
            {
                if (enterprise.Project == Bizz.tempProject.Id)
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
        private List<IndexableLegalEntity> GetIndexableLegalEntities()
        {
            List<IndexableLegalEntity> result = new List<IndexableLegalEntity>();
            int i = 0;
            foreach (LegalEntity entity in Bizz.LegalEntities)
            {
                if (CheckCraftGroups(entity))
                {
                    IndexableLegalEntity temp = new IndexableLegalEntity(i, entity);
                    result.Add(temp);
                    i++;
                }
            }
            result = FilterIndexableLegalEntities(result);
            return result;
        }

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
