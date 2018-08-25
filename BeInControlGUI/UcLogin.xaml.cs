using BicBizz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
    /// Interaction logic for UcLogin.xaml
    /// </summary>
    public partial class UcLogin : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public RibbonTab TabOffer;
        public RibbonTab TabAdministration;
        public RibbonGroup Users;
        public RibbonGroup CraftGroups;
        public RibbonGroup EnterpriseForms;
        public RibbonGroup Status;
        public RibbonApplicationMenuItem MenuItemChangePassWord;
        public RibbonApplicationMenuItem MenuItemLogOut;
        public TextBlock UserName;
        public UserControl UcLeft;
        public UserControl UcRight;

        public static Bizz CBZ = new Bizz();
        #endregion

        public UcLogin(Bizz bizz, RibbonTab tabOffer, RibbonTab tabAdministration, RibbonGroup users, RibbonGroup craftGroups, RibbonGroup enterpriseForms, RibbonGroup status, RibbonApplicationMenuItem menuitemChangePassWord, RibbonApplicationMenuItem menuItemLogOut, TextBlock userName, UserControl ucLeft, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.TabOffer = tabOffer;
            this.TabAdministration = tabAdministration;
            this.Users = users;
            this.CraftGroups = craftGroups;
            this.EnterpriseForms = enterpriseForms;
            this.Status = status;
            this.MenuItemChangePassWord = menuitemChangePassWord;
            this.MenuItemLogOut = menuItemLogOut;
            this.UserName = userName;
            this.UcLeft = ucLeft;
            this.UcRight = ucRight;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (CBZ.CheckCredentials(Bizz, UserName, MenuItemChangePassWord, MenuItemLogOut, TextBoxInitials.Text, TextBoxPassword.Password))
            {
                TabOffer.IsEnabled = true;
                TabAdministration.IsEnabled = true;
                if (Bizz.CurrentUser.Administrator)
                {
                    Users.IsEnabled = true;
                    CraftGroups.IsEnabled = true;
                    EnterpriseForms.IsEnabled = true;
                    Status.IsEnabled = true;
                }
                else
                {
                    Users.IsEnabled = false;
                    CraftGroups.IsEnabled = false;
                    EnterpriseForms.IsEnabled = false;
                    Status.IsEnabled = false;
                }
                UcLeft.Content = new UserControl();
                UcRight.Content = new UserControl();
                Bizz.UcRightActive = false;
            }
            else
            {
                MessageBox.Show("Initialer eller password er forkert.");
            }
        }
    }
}
