﻿using EmBizz;
using EmRepository;
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

namespace EmGui
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UcDeleteProject : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;

        #endregion

        public UcDeleteProject(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;

            GenerateComboBoxCaseIdItems();
        }

        #region Buttons
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Vil du annullere reaktivering af projektet!", "Annuller reaktivering", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                //Close right UserControl
                UcRight.Content = new UserControl();
                Bizz.UcRightActive = false;
            }
        }

        private void ButtonErase_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxEraseProject.IsChecked == true)
            {
                if (MessageBox.Show("Er du sikker på, at du vil slette projektet? Alle data vil gå tabt!", "Slet Projekt", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    // Code that changes project status
                    bool result = Bizz.DeleteFromDb("Projects", Bizz.TempProject.Id.ToString());

                    if (result)
                    {
                        foreach (Enterprise enterprise in Bizz.EnterpriseList)
                        {
                            if (enterprise.Project.Id == Bizz.TempProject.CaseId)
                            {
                                foreach (SubEntrepeneur subEntrepeneur in Bizz.SubEntrepeneurs)
                                {
                                    if (subEntrepeneur.EnterpriseList.Id == enterprise.Id)
                                    {
                                        Bizz.DeleteFromDb("Requests", subEntrepeneur.Request.Id.ToString());
                                        Bizz.DeleteFromDb("IttLetters", subEntrepeneur.IttLetter.Id.ToString());
                                        Bizz.DeleteFromDb("Offers", subEntrepeneur.Offer.Id.ToString());
                                        Bizz.DeleteFromDb("SubEntrepeneurs", subEntrepeneur.Id.ToString());
                                    }
                                }
                                Bizz.DeleteFromDb("EnterpriseList", enterprise.Id.ToString());
                            }
                        }

                        //Show Confirmation
                        MessageBox.Show("Projektet blev slettet", "Slet Projekt", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Update list of projects
                        Bizz.RefreshList("Projects");
                        Bizz.RefreshIndexedList("IndexedActiveProjects");
                        Bizz.RefreshIndexedList("IndexableProjects");

                        //Close right UserControl
                        Bizz.UcRightActive = false;
                        UcRight.Content = new UserControl();
                    }
                    else
                    {
                        //Show error
                        MessageBox.Show("Databasen returnerede en fejl. Projektet blev ikke slettet. Prøv igen.", "Slet Projekt", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                //Show error
                MessageBox.Show("Du har glemt at markere 'Godkend sletning af projekt'.", "Slet Projekt", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion

        #region Events
        private void ComboBoxCaseId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ComboBoxCaseId.SelectedIndex;
            foreach (IndexedProject temp in Bizz.IndexedProjects)
            {
                if (temp.Index == selectedIndex)
                {
                    Bizz.TempProject = new Project(temp.Id, temp.CaseId, temp.Name, temp.Builder, temp.Status, temp.TenderForm, temp.EnterpriseForm, temp.Executive, temp.EnterpriseList, temp.Copy);
                }
            }
        }

        #endregion

        #region Methods
        private void GenerateComboBoxCaseIdItems()
        {
            ComboBoxCaseId.Items.Clear();
            foreach (IndexedProject temp in Bizz.IndexedProjects)
            {
                ComboBoxCaseId.Items.Add(temp);
            }
        }

        /// <summary>
        /// Method, that reloads list of active projects
        /// </summary>
        private void ReloadListActiveProjects()
        {
            Bizz.IndexedActiveProjects.Clear();
            int i = 0;
            foreach (Project tempProject in Bizz.Projects)
            {
                if (tempProject.Status.Id == 1)
                {
                    IndexedProject result = new IndexedProject(i, tempProject);
                    Bizz.IndexedActiveProjects.Add(result);
                    i++;
                }
            }
        }

        /// <summary>
        /// Method, that reloads list of indexable projects
        /// </summary>
        private void ReloadListIndexableProjects()
        {
            Bizz.IndexedProjects.Clear();
            int i = 0;
            foreach (Project temp in Bizz.Projects)
            {
                IndexedProject result = new IndexedProject(i, temp);
                Bizz.IndexedProjects.Add(result);
                i++;
            }
        }

        #endregion

    }
}
