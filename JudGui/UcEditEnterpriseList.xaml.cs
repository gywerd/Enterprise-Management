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
    /// Interaction logic for UcEditEnterpriseList.xaml
    /// </summary>
    public partial class UcEditEnterpriseList : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;
        public List<IndexableEnterprise> IndexableEnterpriseList = new List<IndexableEnterprise>();

        #endregion

        public UcEditEnterpriseList(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
            GenerateComboBoxCaseIdItems();
            GenerateCraftGroupItems();
        }

        #region Buttons
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            //Warning about lost changes before closing
            if (MessageBox.Show("Vil du lukke Redigering af Entrepriselisten?", "Luk Entrepriseliste", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Close right UserControl
                Bizz.UcRightActive = false;
                UcRight.Content = new UserControl();
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //Code that creates a new project
            bool result = Bizz.CEP.UpdateEnterpriseList(Bizz.tempEnterprise);

            if (result)
            {
                //Show Confirmation
                MessageBox.Show("Entrepriselisten blev redigeret", "Rediger Entrepriseliste", MessageBoxButton.OK, MessageBoxImage.Information);

                //Reset Boxes
                TextBoxName.Text = "";
                TextBoxElaboration.Text = "";
                TextBoxOfferList.Text = "";
                ComboBoxCraftGroup1.SelectedIndex = -1;
                ComboBoxCraftGroup2.SelectedIndex = -1;
                ComboBoxCraftGroup3.SelectedIndex = -1;
                ComboBoxCraftGroup4.SelectedIndex = -1;

                //Update list of projects
                Bizz.EnterpriseList.Clear();
                Bizz.EnterpriseList = Bizz.CEP.GetEnterpriseList();
                IndexableEnterpriseList.Clear();
                IndexableEnterpriseList = GetIndexableEnterpriseList();
                ListBoxEnterpriseList.ItemsSource = IndexableEnterpriseList;
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Entrepriselisten blev ikke redigeret. Prøv igen.", "Opret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);
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
            IndexableEnterpriseList = GetIndexableEnterpriseList();
            ListBoxEnterpriseList.ItemsSource = IndexableEnterpriseList;
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

        private void ListBoxEnterpriseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Enterprise temp = new Enterprise((Enterprise)ListBoxEnterpriseList.SelectedItem);
            Bizz.tempEnterprise = temp;
            TextBoxName.Text = temp.Name;
            TextBoxElaboration.Text = temp.Elaboration;
            TextBoxOfferList.Text = temp.OfferList;
            ComboBoxCraftGroup1.SelectedIndex = temp.CraftGroup1;
            ComboBoxCraftGroup2.SelectedIndex = temp.CraftGroup2;
            ComboBoxCraftGroup3.SelectedIndex = temp.CraftGroup3;
            ComboBoxCraftGroup4.SelectedIndex = temp.CraftGroup4;
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
                }
                i++;
            }
            return result;
        }

        #endregion

    }
}