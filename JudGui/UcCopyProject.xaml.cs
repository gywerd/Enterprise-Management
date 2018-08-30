﻿using JudBizz;
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
    /// Interaction logic for UcCopyProject.xaml
    /// </summary>
    public partial class UcCopyProject : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;

        public UcCopyProject(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;

            GenerateComboBoxCaseIdItems();
        }
        #endregion

        #region Buttons
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Vil du annullere kopiering af projektet?", "Annuller kopiering", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Close right UserControl
                UcRight.Content = new UserControl();
                Bizz.UcRightActive = false;
            }
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            // Code that copies the current project into a new project
            Project project = new Project(Bizz.tempProject.CaseId, Bizz.tempProject.Name, Bizz.tempProject.Builder, 1, Bizz.tempProject.TenderForm, Bizz.tempProject.EnterpriseForm, Bizz.tempProject.Executive);
            bool result = Bizz.CPR.InsertIntoProject(Bizz.tempProject);

            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Projektet blev kopieret", "Kopier projekt", MessageBoxButton.OK, MessageBoxImage.Information);

                //Update list of projects
                Bizz.Projects.Clear();
                Bizz.Projects = Bizz.CPR.GetProjects();
                ReloadListActiveProjects();
                ReloadListIndexableProjects();

                //Close right UserControl
                Bizz.UcRightActive = false;
                UcRight.Content = new UserControl();
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Projektet blev ikke kopieret. Prøv igen.", "Kopier Projekt", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region Events
        private void ComboBoxCaseId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ComboBoxCaseId.SelectedIndex;
            foreach (IndexableProject temp in Bizz.IndexableProjects)
            {
                if (temp.Index == selectedIndex)
                {
                    Bizz.tempProject = new Project(temp.Id, temp.CaseId, temp.Name, temp.Builder, temp.Status, temp.TenderForm, temp.EnterpriseForm, temp.Executive, temp.EnterpriseList, temp.Copy);
                }
            }
            TextBoxCaseName.Text = Bizz.tempProject.Name;
        }

        private void TextBoxCaseId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxCaseId.Text.Count() > 6)
            {
                string id = TextBoxCaseId.Text;
                id = id.Remove(id.Length - 1);
                TextBoxCaseId.Text = id;
                TextBoxCaseId.Select(TextBoxCaseId.Text.Length, 0);
            }
            Bizz.tempProject.CaseId = Convert.ToInt32(TextBoxCaseId.Text);
        }

        private void TextBoxCaseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxCaseId.Text.Count() > 50)
            {
                string id = TextBoxCaseName.Text;
                id = id.Remove(id.Length - 1);
                TextBoxCaseName.Text = id;
                TextBoxCaseName.Select(TextBoxCaseName.Text.Length, 0);
            }
            Bizz.tempProject.Name = TextBoxCaseName.Text;
        }

        #endregion

        #region Methods
        private void GenerateComboBoxCaseIdItems()
        {
            ComboBoxCaseId.Items.Clear();
            foreach (IndexableProject temp in Bizz.IndexableProjects)
            {
                ComboBoxCaseId.Items.Add(temp);
            }
        }

        /// <summary>
        /// Method, that reloads list of active projects
        /// </summary>
        private void ReloadListActiveProjects()
        {
            Bizz.ActiveProjects.Clear();
            int i = 0;
            foreach (Project tempProject in Bizz.Projects)
            {
                if (tempProject.Status == 1)
                {
                    IndexableProject result = new IndexableProject(i, tempProject);
                    Bizz.ActiveProjects.Add(result);
                    i++;
                }
            }
        }

        /// <summary>
        /// Method, that reloads list of indexable projects
        /// </summary>
        private void ReloadListIndexableProjects()
        {
            Bizz.IndexableProjects.Clear();
            int i = 0;
            foreach (Project temp in Bizz.Projects)
            {
                IndexableProject result = new IndexableProject(i, temp);
                Bizz.IndexableProjects.Add(result);
                i++;
            }
        }

        #endregion

    }
}
