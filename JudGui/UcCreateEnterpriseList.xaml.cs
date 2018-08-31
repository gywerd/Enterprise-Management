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
    /// Interaction logic for UcCreateEnterpriseList.xaml
    /// </summary>
    public partial class UcCreateEnterpriseList : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;

        #endregion

        #region Constructors
        public UcCreateEnterpriseList(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
            GenerateComboBoxCaseIdItems();
            GenerateCraftGroupItems();
        }

        #endregion

        #region Buttons
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            //Warning about lost changes before closing
            if (MessageBox.Show("Vil du annullere oprettelse af EntrepriseLister?", "Luk Projekt", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Close right UserControl
                Bizz.UcRightActive = false;
                UcRight.Content = new UserControl();
            }
        }

        private void ButtonCreateClose_Click(object sender, RoutedEventArgs e)
        {
            //Code that creates a new project
            if (Bizz.tempProject.EnterpriseList == false)
            {
                Bizz.tempProject.ToggleEnterpriseList();
                Bizz.CPR.UpdateProject(Bizz.tempProject);
                Bizz.Projects.Clear();
                Bizz.Projects = Bizz.CPR.GetProjects();
                ReloadListActiveProjects();
                ReloadListIndexableProjects();
            }
            bool result = Bizz.CEP.InsertIntoEnterpriseList(Bizz.tempEnterprise);

            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Entrepriselisten blev oprettet", "Opret Entrepriseliste", MessageBoxButton.OK, MessageBoxImage.Information);

                //Update EnterpriseList
                Bizz.EnterpriseList.Clear();
                Bizz.EnterpriseList = Bizz.CEP.GetEnterpriseList();
                Bizz.tempEnterprise = new Enterprise(Bizz.strConnection);

                //Close right UserControl
                Bizz.UcRightActive = false;
                UcRight.Content = new UserControl();
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Entrepriselisten blev ikke oprettet. Prøv igen.", "Opret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ButtonCreateNew_Click(object sender, RoutedEventArgs e)
        {
            //Code that creates a new project
            if (Bizz.tempProject.EnterpriseList == false)
            {
                Bizz.tempProject.ToggleEnterpriseList();
                Bizz.CPR.UpdateProject(Bizz.tempProject);
                Bizz.Projects.Clear();
                Bizz.Projects = Bizz.CPR.GetProjects();
                ReloadListActiveProjects();
                ReloadListIndexableProjects();
            }
            bool result = Bizz.CEP.InsertIntoEnterpriseList(Bizz.tempEnterprise);

            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Entrepriselisten blev oprettet", "Opret Entrepriselisten", MessageBoxButton.OK, MessageBoxImage.Information);

                //Reset Boxes
                TextBoxCaseName.Content = "";
                TextBoxName.Text = "";
                TextBoxElaboration.Text = "";
                TextBoxOfferList.Text = "";
                ComboBoxCraftGroup1.SelectedIndex = 0;
                ComboBoxCraftGroup2.SelectedIndex = 0;
                ComboBoxCraftGroup3.SelectedIndex = 0;
                ComboBoxCraftGroup4.SelectedIndex = 0;

                //Update Enterprise list
                Bizz.EnterpriseList.Clear();
                Bizz.EnterpriseList = Bizz.CEP.GetEnterpriseList();
                Bizz.tempEnterprise.Name = "";
                Bizz.tempEnterprise.Elaboration = "";
                Bizz.tempEnterprise.OfferList = "";
                Bizz.tempEnterprise.CraftGroup1 = 0;
                Bizz.tempEnterprise.CraftGroup2 = 0;
                Bizz.tempEnterprise.CraftGroup3 = 0;
                Bizz.tempEnterprise.CraftGroup4 = 0;
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Entrepriselisten blev ikke oprettet. Prøv igen.", "Opret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);
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
            TextBoxCaseName.Content = Bizz.tempProject.Name;
            Bizz.tempEnterprise.Project = Bizz.tempProject.Id;
            ComboBoxCraftGroup1.SelectedIndex = 0;
            ComboBoxCraftGroup2.SelectedIndex = 0;
            ComboBoxCraftGroup3.SelectedIndex = 0;
            ComboBoxCraftGroup4.SelectedIndex = 0;
            Bizz.tempEnterprise.CraftGroup1 = 0;
            Bizz.tempEnterprise.CraftGroup2 = 0;
            Bizz.tempEnterprise.CraftGroup3 = 0;
            Bizz.tempEnterprise.CraftGroup4 = 0;
        }

        private void ComboBoxCraftGroup1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bizz.tempEnterprise.CraftGroup1 = ComboBoxCraftGroup1.SelectedIndex;
        }

        private void ComboBoxCraftGroup2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bizz.tempEnterprise.CraftGroup2 = ComboBoxCraftGroup2.SelectedIndex;
        }

        private void ComboBoxCraftGroup3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bizz.tempEnterprise.CraftGroup3 = ComboBoxCraftGroup3.SelectedIndex;
        }

        private void ComboBoxCraftGroup4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bizz.tempEnterprise.CraftGroup4 = ComboBoxCraftGroup4.SelectedIndex;
        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxName.Text.Count() > 255)
            {
                string textBlock = TextBoxName.Text;
                textBlock = textBlock.Remove(textBlock.Length - 1);
                TextBoxName.Text = textBlock;
                TextBoxName.Select(TextBoxName.Text.Length, 0);
            }
            Bizz.tempEnterprise.Name = TextBoxName.Text;
        }

        private void TextBoxElaboration_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxElaboration.Text.Count() > 255)
            {
                string textBlock = TextBoxElaboration.Text;
                textBlock = textBlock.Remove(textBlock.Length - 1);
                TextBoxElaboration.Text = textBlock;
                TextBoxElaboration.Select(TextBoxElaboration.Text.Length, 0);
            }
            Bizz.tempEnterprise.Elaboration = TextBoxElaboration.Text;
        }

        private void TextBoxOfferList_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxOfferList.Text.Count() > 255)
            {
                string textBlock = TextBoxOfferList.Text;
                textBlock = textBlock.Remove(textBlock.Length - 1);
                TextBoxOfferList.Text = textBlock;
                TextBoxOfferList.Select(TextBoxOfferList.Text.Length, 0);
            }
            Bizz.tempEnterprise.OfferList = TextBoxOfferList.Text;
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

        private void GenerateCraftGroupItems()
        {
            ComboBoxCraftGroup1.Items.Clear();
            ComboBoxCraftGroup2.Items.Clear();
            ComboBoxCraftGroup3.Items.Clear();
            ComboBoxCraftGroup4.Items.Clear();
            foreach (CraftGroup temp in Bizz.CraftGroups)
            {
                ComboBoxCraftGroup1.Items.Add(temp);
                ComboBoxCraftGroup2.Items.Add(temp);
                ComboBoxCraftGroup3.Items.Add(temp);
                ComboBoxCraftGroup4.Items.Add(temp);
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