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
        public Bizz Bizz;
        public UserControl UcRight;
        public bool UcRightActive;

        public UcCreateProject(Bizz bizz, UserControl ucRight, bool ucRightActive)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
            this.UcRightActive = ucRightActive;
        }

        private void ButtonCreateClose_Click(object sender, RoutedEventArgs e)
        {
            //To Create:
            //Code that creates a new project

            //Show Confirmation
            MessageBox.Show("Projektet blev oprettet", "Ret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);

            //Close right UserControl
            UcRightActive = false;
            UcRight.Content = new UserControl();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            //Warnig about lost changes before closing
            if (MessageBox.Show("Vil du annullere oprettelse af projektet?", "Luk Projekt", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Close right UserControl
                UcRightActive = false;
                UcRight.Content = new UserControl();
            }
        }

        private void ButtonCreateNew_Click(object sender, RoutedEventArgs e)
        {
            //To Create:
            //Code that creates a new project

            //Show Confirmation
            MessageBox.Show("Projektetr blev oprettet", "Ret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);

            //Reset form
            TextBoxCaseId1.Text = "";
            TextBoxCaseId2.Text = "";
            ComboBoxProjectStatus.SelectedIndex = -1;
            ComboBoxOfferExecutive.SelectedIndex = -1;
            ComboBoxTenderForm.SelectedIndex = -1;
            TextBoxCaseName.Text = "";
        }
    }
}
