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
    /// Interaction logic for UcCopyProject.xaml
    /// </summary>
    public partial class UcCopyProject : UserControl
    {
        public Bizz Bizz;
        public UserControl UcRight;

        public UcCopyProject(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Vil du annullere kopiering af projektet?", "Annuller kopiering", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Close right UserControl
                UcRight.Content = new UserControl();
                Bizz.UcRightActive = false;
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //To Create:
            // Code that copies the current project into a new project

            //Show Confirmation
            MessageBox.Show("Projektet blev kopieret", "Kopier projekt", MessageBoxButton.OK, MessageBoxImage.Information);

            //Close right UserControl
            UcRight.Content = new UserControl();
            Bizz.UcRightActive = false;
        }
    }
}
