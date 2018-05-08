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
using System.Windows.Shapes;

namespace BicGui
{
    /// <summary>
    /// Interaction logic for BicGui.xaml
    /// </summary>
    public partial class BicGui : Window
    {
        #region Fields
        public UcLogin UcLogin;
        public Bizz Bizz = new Bizz();
        public UcLoginHelp ucLoginHelp;
        public UcLogin ucLogin;
        public UcProject ucProject;
        #endregion

        public BicGui()
        {
            InitializeComponent();
            TabOffer.IsEnabled = false;
            TabAdministration.IsEnabled = false;
            Users.IsEnabled = false;
            CraftGroups.IsEnabled = false;
            EnterpriseForms.IsEnabled = false;
            Status.IsEnabled = false;
            OpenUcLogin();
            OpenUcLoginHelp();
        }

        #region Methods
        private void OpenUcLoginHelp()
        {
            ucLoginHelp = new UcLoginHelp();
            UcLeft.Content = ucLoginHelp;
        }

        private void OpenUcLogin()
        {
            Bizz.UcRightActive = true;
            ucLogin = new UcLogin(Bizz, TabOffer, TabAdministration, Users, CraftGroups, EnterpriseForms, Status, MenuItemChangePassWord, MenuItemLogOut, UserName, UcLeft, UcRight);
            UcRight.Content = ucLogin;
        }
        #endregion

        #region Buttons
        private void ButtonAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Entreprise Manager V. 0.1 ALPHA\n\n©Jorton\n©2018 Daniel Giversen", "Om Underentrepenører", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonNewsV115_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "BeInControl 1.1.5", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonBicV116_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "BeInControl 1.1.6", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonBicV117_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "BeInControl 1.1.7", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonCalculations_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Beregninger", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonEnterprise_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Entreprise", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonEnterpriseManagerV10_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Enterprise Manager 1.0", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonFormList_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Former", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonFormAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Tilføj form", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonFormRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Fjern form", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonGroupList_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Grupper", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonGroupAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Tilføj gruppe", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonGroupRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Fjern gruppe", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Hjælp", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonOptions_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Indstillinger", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonProjekt_Click(object sender, RoutedEventArgs e)
        {
            if (Bizz.UcRightActive)
            {
                ucProject = new UcProject(Bizz, UcLeft, UcRight);
                if (MessageBox.Show("Vil du åbne 'Projekt'. Alt, der ikke er gemt vil blive mistet!", "Åbn Projekt", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    UcLeft.Content = ucProject;
                    Bizz.UcRightActive = false;
                    UcRight.Content = new UserControl();
                }
            }
            else
            {
                ucProject = new UcProject(Bizz, UcLeft, UcRight);
                Bizz.UcRightActive = false;
                UcLeft.Content = ucProject;
                UcRight.Content = new UserControl();
            }
        }

        private void ButtonStatusList_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Statusliste", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonStatusAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Tilføj status", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonStatusRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Fjern status", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonSubEntrepeneurList_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Entrepenører", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonSubEntrepeneurAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Tilføj Entrepenør", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ButtonSubEntrepeneurRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Fjern Entrepenør", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonSubEntrepeneurs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Underentrepenører", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonUserList_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Brugere", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonUserAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Tilføj bruger", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonUserRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Fjern bruger", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonZipList_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Postnumre", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonZipAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Tilføj postnummer", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonZipRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Fjern postnummer", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItemQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItemLogout_Click(object sender, RoutedEventArgs e)
        {
            TabOffer.IsEnabled = false;
            TabAdministration.IsEnabled = false;
            Bizz = new Bizz();
            UserName.Text = "";
            MenuItemChangePassWord.IsEnabled = false;
            MenuItemLogOut.IsEnabled = false;
            OpenUcLogin();
            OpenUcLoginHelp();
        }

        private void MenuItemChangePassWord_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Endnu ikke implementeret", "Ændre password", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

    }
}
