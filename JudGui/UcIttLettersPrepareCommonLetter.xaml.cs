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
    /// Interaction logic for UcIttLettersPrepareCommonLetter.xaml
    /// </summary>
    public partial class UcIttLettersPrepareCommonLetter : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;
        public List<IttLetterShipping> ShippingList = new List<IttLetterShipping>();
        public List<Contact> ProjectContacts = new List<Contact>();
        public List<Enterprise> ProjectEnterpriseList = new List<Enterprise>();
        public List<IndexableEnterprise> IndexableEnterprises = new List<IndexableEnterprise>();
        public List<IndexableIttLetterBullet> IndexableIttLetterBulletList = new List<IndexableIttLetterBullet>();
        public List<IndexableIttLetterParagraph> IndexableIttLetterParagraphList = new List<IndexableIttLetterParagraph>();
        public List<IndexableLegalEntity> IndexableLegalEntities = new List<IndexableLegalEntity>();
        public List<IttLetterParagraph> paragraphs = new List<IttLetterParagraph>();
        public List<SubEntrepeneur> ProjectSubEntrepeneurs = new List<SubEntrepeneur>();
        public List<IttLetterReceiver> IttLetterReceivers = new List<IttLetterReceiver>();

        #endregion

        #region Constructors
        public UcIttLettersPrepareCommonLetter(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
            GenerateComboBoxCaseIdItems();
        }

        #endregion

        #region Buttons
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxNewBullet.Text != "")
            {
                bool dbAnswer = false;
                Exception exception = new Exception();
                Bizz.TempIttLetterBullet = new IttLetterBullet(Bizz.StrConnection, Bizz.TempIttLetterParagraph.Id, TextBoxNewBullet.Text);
                try
                {
                    dbAnswer = Bizz.CIB.InsertIntoIttLetterBulletList(Bizz.TempIttLetterBullet);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                if (!dbAnswer)
                {
                    Exception tempEx = new Exception();
                    if (exception != tempEx)
                    {
                        MessageBox.Show("Databasen meldte en fejl. Linjen blev ikke tilføjet til afsnittet\n" + exception, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Databasen meldte en fejl. Linjen blev ikke tilføjet til afsnittet", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    UpdateBullets();
                    GetIndexableIttLetterBulletList();
                    ListBoxBullets.ItemsSource = IndexableIttLetterBulletList;
                    ListBoxBullets.SelectedIndex = 0;
                    TextBoxNewBullet.Text = "";
                }
            }
        }

        private void ButtonPrepare_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxReceiverListExist.IsChecked == true)
            {

            }
            else
            {
                MessageBox.Show("Der er ingen modtagerliste. Fælles del af Udbudsbrev kan ikke genereres.", "Forbered Udbudsbrev", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    Bizz.TempProject = new Project(Bizz.StrConnection, temp.Id, temp.CaseId, temp.Name, temp.Builder, temp.Status, temp.TenderForm, temp.EnterpriseForm, temp.Executive, temp.EnterpriseList, temp.Copy);
                }
            }
            TextBoxName.Text = Bizz.TempProject.Name;
            GetProjectDetails();
            SetCheckBoxReceiverListExist();
            ComboBoxParagraphs.ItemsSource = IndexableIttLetterParagraphList;
            ComboBoxParagraphs.SelectedIndex = 0;
        }

        private void ComboBoxParagraphs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ComboBoxParagraphs.SelectedIndex;
            if (IndexableIttLetterParagraphList.Count == 0)
            {
                GetIndexableIttLetterParagraphList();
            }
            foreach (IndexableIttLetterParagraph temp in IndexableIttLetterParagraphList)
            {
                if (temp.Index == selectedIndex)
                {
                    Bizz.TempIttLetterParagraph = new IttLetterParagraph(Bizz.StrConnection, temp.Id, temp.Project, temp.Name);
                    break;
                }
            }
            Bizz.LegalEntities.Clear();
            Bizz.LegalEntities = Bizz.CLE.GetLegalEntities();
            IndexableLegalEntities.Clear();
            GetIndexableIttLetterBulletList();
            ListBoxBullets.ItemsSource = IndexableIttLetterBulletList;
            ListBoxBullets.SelectedIndex = 0;
        }

        private void ListBoxBullets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Method that adds Paragraphs to Db
        /// </summary>
        private void AddParagraphs()
        {
            string[] names = { "Komplet sæt beskrivelse i henhold til vedlagte dokumenter", "Projektdokumenter", "Tegninger i henhold til Tegningsliste", "Tidsplaner", "Øvrigt udbudsmateriale" };
            int i = 0;
            foreach (string name in names)
            {
                IttLetterParagraph temp = new IttLetterParagraph(Bizz.StrConnection, Bizz.TempProject.Id, names[i]);
                bool dbAnswer = Bizz.CIP.InsertIntoIttLetterParagraphList(temp);
                if (!dbAnswer)
                {
                    MessageBox.Show("Databasen meldte fejl. Afsnittet blev ikke tilføjet.", "Tilføj Afsnit", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                i++;
            }
            UpdateParagraphs();
        }

        /// <summary>
        /// Method that adds a Shipping to Shipping List
        /// </summary>
        /// <param name="id">int</param>
        private void AddShipping(int id)
        {
            IttLetterShipping shipping = new IttLetterShipping(Bizz.StrConnection);
            foreach (IttLetterShipping temp in Bizz.IttLetterShippingList)
            {
                if (temp.Id == id)
                {
                    shipping = temp;
                    break;
                }
            }
            ShippingList.Add(shipping);
        }

        /// <summary>
        /// Method, that checks, whether a LegalEntity exists in in a list
        /// </summary>
        /// <param name="entity">LegalEntity</param>
        /// <returns>bool</returns>
        private bool CheckEntity(LegalEntity entity, List<LegalEntity> list)
        {
            bool result = false;
            foreach (LegalEntity sub in list)
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
        /// Method that creates an Indexable Bullet List
        /// </summary>
        private void GetIndexableIttLetterBulletList()
        {
            IndexableIttLetterBulletList.Clear();
            IndexableIttLetterBulletList.Add(new IndexableIttLetterBullet(Bizz.StrConnection, 0, Bizz.IttLetterBulletList[0]));
            int i = 1;
            foreach (IttLetterBullet temp in Bizz.IttLetterBulletList)
            {
                if (temp.Paragraph == Bizz.TempIttLetterParagraph.Id)
                {
                    IndexableIttLetterBullet other = new IndexableIttLetterBullet(Bizz.StrConnection, i, temp);
                    IndexableIttLetterBulletList.Add(other);
                    i++;
                }
            }
        }

        /// <summary>
        /// Method that creates an Indexable Enterprise List
        /// </summary>
        private void GetIndexableIttLetterParagraphList()
        {
            IndexableIttLetterParagraphList.Clear();
            IndexableIttLetterParagraphList.Add(new IndexableIttLetterParagraph(Bizz.StrConnection, 0, Bizz.IttLetterParagraphList[0]));
            if (!ParagraphsExist())
            {
                AddParagraphs();
            }
            int i = 1;
            foreach (IttLetterParagraph temp in Bizz.IttLetterParagraphList)
            {
                if (temp.Project == Bizz.TempProject.Id)
                {
                    IndexableIttLetterParagraph other = new IndexableIttLetterParagraph(Bizz.StrConnection, i, temp);
                    IndexableIttLetterParagraphList.Add(other);
                    i++;
                }
            }
        }

        /// <summary>
        /// Method that creates a list of indexable Legal Entities
        /// </summary>
        private void GetIndexableLegalEntities()
        {
            List<LegalEntity> tempResult = new List<LegalEntity>();
            IndexableLegalEntity temp = new IndexableLegalEntity(Bizz.StrConnection, 0, Bizz.LegalEntities[0]);
            IndexableLegalEntities.Clear();
            IndexableLegalEntities.Add(temp);
            foreach (Enterprise enterprise in Bizz.EnterpriseList)
            {
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
                temp = new IndexableLegalEntity(Bizz.StrConnection, i, sub);
                IndexableLegalEntities.Add(temp);
                i++;
            }
        }

        /// <summary>
        /// Method, that generates List of ProjectSubEntrepeneurs
        /// </summary>
        /// <param name="id">int</param>
        /// <returns></returns>
        private void GetProjectDetails()
        {
            ProjectSubEntrepeneurs.Clear();
            ProjectEnterpriseList.Clear();
            IndexableEnterprises.Clear();
            IttLetterReceivers.Clear();
            ShippingList.Clear();
            foreach (Enterprise enterprise in Bizz.EnterpriseList)
            {
                if (enterprise.Project == Bizz.TempProject.Id)
                {
                    foreach (SubEntrepeneur sub in Bizz.SubEntrepeneurs)
                    {
                        if (sub.EnterpriseList == enterprise.Id)
                        {
                            ProjectSubEntrepeneurs.Add(sub);
                        }
                        foreach (LegalEntity entity in Bizz.LegalEntities)
                        {
                            if (sub.Entrepeneur == entity.Id)
                            {
                                foreach (IttLetterReceiver receiver in Bizz.IttLetterReceivers)
                                {
                                    if (receiver.CompanyId == sub.Entrepeneur && receiver.Project == Bizz.TempProject.Id)
                                    {
                                        IttLetterReceivers.Add(receiver);
                                        AddShipping(receiver.Id);
                                    }
                                }
                            }
                        }
                    }
                    ProjectEnterpriseList.Add(enterprise);
                }
            }
            GetIndexableIttLetterParagraphList();
            GetIndexableLegalEntities();
        }

        /// <summary>
        /// Method that checks wether paragraphs have already been added
        /// </summary>
        private bool ParagraphsExist()
        {
            bool result = false;
            foreach (IttLetterParagraph temp in Bizz.IttLetterParagraphList)
            {
                if (temp.Project == Bizz.TempProject.Id)
                {
                    result = true;
                    break;
                }

            }
            return result;
        }

        private void SetCheckBoxReceiverListExist()
        {
            if (IttLetterReceivers.Count <= 1)
            {
                CheckBoxReceiverListExist.IsChecked = true;
            }
            else
            {
                CheckBoxReceiverListExist.IsChecked = false;
            }
        }

        private void UpdateBullets()
        {
            Bizz.IttLetterBulletList.Clear();
            Bizz.IttLetterBulletList = Bizz.CIB.GetIttLetterBulletList();
        }

        private void UpdateParagraphs()
        {
            Bizz.IttLetterParagraphList.Clear();
            Bizz.IttLetterParagraphList = Bizz.CIP.GetIttLetterParagraphList();
        }

        #endregion

    }
}
