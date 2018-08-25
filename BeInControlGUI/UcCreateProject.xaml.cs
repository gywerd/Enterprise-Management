using BicBizz;
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

namespace BicGui
{
    /// <summary>
    /// Interaction logic for UcCreateProject.xaml
    /// </summary>
    public partial class UcCreateProject : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;
        #endregion

        public UcCreateProject(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
            GenerateComboBoxBuilderItems();
            GenerateComboBoxProjectStatusItems();
            GenerateComboBoxTenderFormItems();
            GenerateComboBoxEnterpriseFormItems();
            GenerateComboBoxExecutiveItems();
        }

        #region Methods
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

        /// <summary>
        /// Method, that generate list of active projects
        /// </summary>
        private void ReloadListActiveProjects()
        {
            Bizz.ActiveProjects.Clear();
            int i = 0;
            foreach (Project tempProject in Bizz.Projects)
            {
                if (tempProject.Status == 1)
                {
                    ActiveProject result = new ActiveProject(i, tempProject);
                    Bizz.ActiveProjects.Add(result);
                    i++;
                }
            }
        }

        #endregion

        #region Buttons
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            //Warnig about lost changes before closing
            if (MessageBox.Show("Vil du annullere oprettelse af projektet?", "Luk Projekt", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Close right UserControl
                Bizz.UcRightActive = false;
                UcRight.Content = new UserControl();
            }
        }

        private void ButtonCreateClose_Click(object sender, RoutedEventArgs e)
        {
            //To Create:
            //Code that creates a new project
            //Code that creates a new project
            Project project = new Project(Convert.ToInt32(TextBoxCaseId.Text), TextBoxCaseName.Text, ComboBoxBuilder.SelectedIndex, ComboBoxProjectStatus.SelectedIndex, ComboBoxTenderForm.SelectedIndex, ComboBoxEnterpriseForm.SelectedIndex, ComboBoxExecutive.SelectedIndex);
            bool result = Bizz.CPR.InsertIntoProject(project);

            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Projektet blev oprettet", "Opret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);

                //Update list of projects
                Bizz.Projects.Clear();
                Bizz.Projects = Bizz.CPR.GetProjects();
                ReloadListActiveProjects();

                //Close right UserControl
                Bizz.UcRightActive = false;
                UcRight.Content = new UserControl();
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Projektet blev ikke oprettet. Prøv igen.", "Opret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ButtonCreateNew_Click(object sender, RoutedEventArgs e)
        {
            //Code that creates a new project
            Project project = new Project(Convert.ToInt32(TextBoxCaseId.Text), TextBoxCaseName.Text, ComboBoxBuilder.SelectedIndex, ComboBoxProjectStatus.SelectedIndex, ComboBoxTenderForm.SelectedIndex, ComboBoxEnterpriseForm.SelectedIndex, ComboBoxExecutive.SelectedIndex);
            bool result = Bizz.CPR.InsertIntoProject(project);

            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Projektet blev oprettet", "Opret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);

                //Reset form
                TextBoxCaseId.Text = "";
                TextBoxCaseName.Text = "";
                ComboBoxBuilder.SelectedIndex = -1;
                ComboBoxProjectStatus.SelectedIndex = 1;
                ComboBoxTenderForm.SelectedIndex = -1;
                ComboBoxEnterpriseForm.SelectedIndex = -1;
                ComboBoxExecutive.SelectedIndex = -1;

                //Update list of projects
                Bizz.Projects.Clear();
                Bizz.Projects = Bizz.CPR.GetProjects();
                ReloadListActiveProjects();
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Projektet blev ikke oprettet. Prøv igen.", "Opret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion

        #region Events
        private void TextBoxCaseId_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (TextBoxCaseId.Text.Count() > 6)
            {
                string id = TextBoxCaseId.Text;
                id = id.Remove(id.Length - 1);
                TextBoxCaseId.Text = id;
            }
        }

        private void TextBoxCaseName_TextChanged(object sender, RoutedEventArgs e)
        {
            if (TextBoxCaseId.Text.Count() > 50)
            {
                string id = TextBoxCaseId.Text;
                id = id.Remove(id.Length - 1);
                TextBoxCaseId.Text = id;
            }
        }

        #endregion

    }
}
