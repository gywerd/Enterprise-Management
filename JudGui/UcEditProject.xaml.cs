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
    /// Interaction logic for UcEditProject.xaml
    /// </summary>
    public partial class UcEditProject : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;

        #endregion

        public UcEditProject(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
            GenerateComboBoxCaseIdItems();
            GenerateComboBoxBuilderItems();
            GenerateComboBoxProjectStatusItems();
            GenerateComboBoxTenderFormItems();
            GenerateComboBoxEnterpriseFormItems();
            GenerateComboBoxExecutiveItems();
        }

        #region Buttons
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            //Warning about lost changes before closing
            if (MessageBox.Show("Du er ved at lukke projektet. Alt, der ikke er gemt vil blive mistet!", "Luk Projekt", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                //Close right UserControl
                UcRight.Content = new UserControl();
                Bizz.UcRightActive = false;
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //To create:
            // Code that save changes to the project
            bool result = Bizz.CPR.UpdateProject(Bizz.tempProject);

            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Projektet blev rettet", "Ret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);

                //Update list of projects
                Bizz.Projects.Clear();
                Bizz.Projects = Bizz.CPR.GetProjects();
                Bizz.ActiveProjects.Clear();
                Bizz.ActiveProjects = Bizz.GetListActiveProjects();
                Bizz.IndexableProjects.Clear();
                Bizz.IndexableProjects = Bizz.GetListIndexableProjects();

                //Close right UserControl
                UcRight.Content = new UserControl();
                Bizz.UcRightActive = false;
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Projektet blev ikke rettet. Prøv igen.", "Ret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);
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
            TextBoxCaseName.Text = Bizz.tempProject.Name;
            ComboBoxBuilder.SelectedIndex = Bizz.tempProject.Builder;
            ComboBoxProjectStatus.SelectedIndex = Bizz.tempProject.Status;
            ComboBoxTenderForm.SelectedIndex = Bizz.tempProject.TenderForm;
            ComboBoxEnterpriseForm.SelectedIndex = Bizz.tempProject.EnterpriseForm;
            ComboBoxExecutive.SelectedIndex = Bizz.tempProject.Executive;
        }

        private void TextBoxCaseName_TextChanged(object sender, RoutedEventArgs e)
        {
            Bizz.tempProject.Name = TextBoxCaseName.Text;
        }

        private void ComboBoxBuilder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bizz.tempProject.Builder = ComboBoxBuilder.SelectedIndex;
        }

        private void ComboBoxProjectStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bizz.tempProject.Status = ComboBoxProjectStatus.SelectedIndex;
        }

        private void ComboBoxTenderForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bizz.tempProject.TenderForm = ComboBoxTenderForm.SelectedIndex;
        }

        private void ComboBoxEnterpriseForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bizz.tempProject.EnterpriseForm = ComboBoxEnterpriseForm.SelectedIndex;
        }

        private void ComboBoxExecutive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bizz.tempProject.Executive = ComboBoxExecutive.SelectedIndex;
        }

        #endregion

        #region Methods
        private void GenerateComboBoxCaseIdItems()
        {
            ComboBoxCaseId.Items.Clear();
            foreach (IndexableProject temp in Bizz.ActiveProjects)
            {
                ComboBoxCaseId.Items.Add(temp);
            }
        }

        private void GenerateComboBoxBuilderItems()
        {
            ComboBoxBuilder.Items.Clear();
            foreach (Builder temp in Bizz.Builders)
            {
                ComboBoxBuilder.Items.Add(temp);
            }
        }

        private void GenerateComboBoxProjectStatusItems()
        {
            ComboBoxProjectStatus.Items.Clear();
            foreach (ProjectStatus temp in Bizz.ProjectStatusList)
            {
                ComboBoxProjectStatus.Items.Add(temp);
            }
        }

        private void GenerateComboBoxTenderFormItems()
        {
            ComboBoxTenderForm.Items.Clear();
            foreach (TenderForm temp in Bizz.TenderForms)
            {
                ComboBoxTenderForm.Items.Add(temp);
            }
        }

        private void GenerateComboBoxEnterpriseFormItems()
        {
            ComboBoxEnterpriseForm.Items.Clear();
            foreach (EnterpriseForm temp in Bizz.EnterpriseForms)
            {
                ComboBoxEnterpriseForm.Items.Add(temp);
            }
        }

        private void GenerateComboBoxExecutiveItems()
        {
            ComboBoxExecutive.Items.Clear();
            foreach (User temp in Bizz.Users)
            {
                ComboBoxExecutive.Items.Add(temp);
            }
        }

        #endregion

    }
}
