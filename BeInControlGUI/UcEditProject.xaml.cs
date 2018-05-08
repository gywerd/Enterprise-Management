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
    /// Interaction logic for UcEditProject.xaml
    /// </summary>
    public partial class UcEditProject : UserControl
    {
        public Bizz Bizz;
        public UserControl UcRight;

        public UcEditProject(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            //Warnig about lost changes before closing
            if (MessageBox.Show("Du er ved at lukke projektet. Alt, der ikke er gemt vil blive mistet!", "Luk Projekt", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                //Close right UserControl
                UcRight.Content = new UserControl();
                Bizz.UcRightActive = false;
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //To create:
            // Code that save changes to the project

            //Show Confirmation
            MessageBox.Show("Projektoplysninger blev rettet", "Ret Projekt", MessageBoxButton.OK, MessageBoxImage.Information);

            //Close right UserControl
            UcRight.Content = new UserControl();
            Bizz.UcRightActive = false;
        }

    }
}
