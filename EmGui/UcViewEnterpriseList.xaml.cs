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
    /// Interaction logic for UcViewEnterpriseList.xaml
    /// </summary>
    public partial class UcViewEnterpriseList : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;
        public List<Enterprise> IndexableEnterpriseList = new List<Enterprise>();

        #endregion

        public UcViewEnterpriseList(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
            ComboBoxCaseId.ItemsSource = Bizz.IndexedActiveProjects;
        }

        #region Buttons
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            //Close right UserControl
            MessageBox.Show("Visning af Entrepriselisten lukkes.", "Luk Entrepriseliste", MessageBoxButton.OK, MessageBoxImage.Information);
            Bizz.UcRightActive = false;
            UcRight.Content = new UserControl();
        }

        private void ButtonGeneratePdf_Click(object sender, RoutedEventArgs e)
        {
            PdfCreator pdfCreator = new PdfCreator(Bizz.MEFW.ObtainStrConnection());
            string path = pdfCreator.GenerateEnterpriseListPdf(Bizz, IndexableEnterpriseList, Bizz.Users);
            System.Diagnostics.Process.Start(path);
        }

        #endregion

        #region Events
        private void ComboBoxCaseId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ComboBoxCaseId.SelectedIndex;
            foreach (IndexedProject temp in Bizz.IndexedActiveProjects)
            {
                if (temp.Index == selectedIndex)
                {
                    Bizz.TempProject = new Project(temp.Id, temp.CaseId, temp.Name, temp.Builder, temp.Status, temp.TenderForm, temp.EnterpriseForm, temp.Executive, temp.EnterpriseList, temp.Copy);
                }
            }
            TextBoxCaseName.Text = Bizz.TempProject.Name;
            IndexableEnterpriseList = GetIndexableEnterpriseList();
        }

        #endregion

        #region Methods
        //private void GenerateComboBoxCaseIdItems()
        //{
        //    ComboBoxCaseId.Items.Clear();
        //    foreach (IndexableProject temp in Bizz.ActiveProjects)
        //    {
        //        ComboBoxCaseId.Items.Add(temp);
        //    }
        //}

        private List<Enterprise> GetIndexableEnterpriseList()
        {
            List<Enterprise> result = new List<Enterprise>();
            int i = 0;
            foreach (Enterprise enterprise in Bizz.EnterpriseList)
            {
                if (enterprise.Project.Id == Bizz.TempProject.Id)
                {
                    IndexedEnterprise temp = new IndexedEnterprise(i, enterprise);
                    result.Add(temp);
                }
                i++;
            }
            return result;
        }

        #endregion

    }
}
