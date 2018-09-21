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
                    bool result = Bizz.CPR.DeleteFromProject(Bizz.tempProject.Id);

                    if (result)
                    {
                        foreach (Enterprise enterprise in Bizz.EnterpriseList)
                        {
                            if (enterprise.Project == Bizz.tempProject.CaseId)
                            {
                                foreach (SubEntrepeneur subEntrepeneur in Bizz.SubEntrepeneurs)
                                {
                                    if (subEntrepeneur.EnterpriseList == enterprise.Id)
                                    {
                                        Bizz.CRQ.DeleteFromRequests(subEntrepeneur.Request);
                                        Bizz.CIL.DeleteFromIttLetters(subEntrepeneur.IttLetter);
                                        Bizz.COF.DeleteFromOffers(subEntrepeneur.Offer);
                                        Bizz.CSE.DeleteFromSubEntrepeneurs(subEntrepeneur.Id);
                                    }
                                }
                                Bizz.CEP.DeleteFromEnterpriseList(enterprise.Id);
                            }
                        }

                        //Show Confirmation
                        MessageBox.Show("Projektet blev slettet", "Slet Projekt", MessageBoxButton.OK, MessageBoxImage.Information);

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
            foreach (IndexableProject temp in Bizz.IndexableProjects)
            {
                if (temp.Index == selectedIndex)
                {
                    Bizz.tempProject = new Project(temp.Id, temp.CaseId, temp.Name, temp.Builder, temp.Status, temp.TenderForm, temp.EnterpriseForm, temp.Executive, temp.EnterpriseList, temp.Copy);
                }
            }
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