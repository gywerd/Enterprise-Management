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
    /// Interaction logic for UcEditCaseId.xaml
    /// </summary>
    public partial class UcChangeCaseId : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;

        #endregion

        public UcChangeCaseId(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;

            GenerateComboBoxCaseIdItems();
        }

        #region Buttons
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            //Warning before cancelling
            if (MessageBox.Show("Vil du annullere redigering af SagsId?", "Annuller redigering", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Close right UserControl
                UcRight.Content = new UserControl();
                Bizz.UcRightActive = false;
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            // Code that save changed CaseId to the project
            bool result = Bizz.CPR.UpdateProject(Bizz.tempProject);

            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Sagsnummer blev ændret", "Skift Sagsnummer", MessageBoxButton.OK, MessageBoxImage.Information);

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
                MessageBox.Show("Databasen returnerede en fejl. Sagsnummer blev ikke ændret. Prøv igen.", "Skift Sagsnummer", MessageBoxButton.OK, MessageBoxImage.Information);
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