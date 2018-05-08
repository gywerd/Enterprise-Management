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
    /// Interaction logic for UcReactivateProject.xaml
    /// </summary>
    public partial class UcReactivateProject : UserControl
    {
        public Bizz Bizz;
        public UserControl UcRight;

        public UcReactivateProject(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Vil du annullere reaktivering af projektet!", "Annuller reaktivering", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                //Close right UserControl
                UcRight.Content = new UserControl();
                Bizz.UcRightActive = false;
            }
        }

        private void ButtonReactivate_Click(object sender, RoutedEventArgs e)
        {
            //To Create:
            // Code that reactivates the project

            //Show Confirmation
            MessageBox.Show("Projektet blev reaktiveret", "Reaktiver Projekt", MessageBoxButton.OK, MessageBoxImage.Information);

            //Close right UserControl
            UcRight.Content = new UserControl();
            Bizz.UcRightActive = false;
        }
    }
}
