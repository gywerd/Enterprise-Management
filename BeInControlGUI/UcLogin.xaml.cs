﻿using BicBizz;
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
        public RibbonApplicationMenuItem MenuItemChangePassWord;
        public RibbonApplicationMenuItem MenuItemLogOut;
        public TextBlock UserName;
        public UserControl UcRight;
        public UserControl UcLeft;
        public bool UcRightActive;

        public static Bizz CBZ = new Bizz();
        #endregion

        public UcLogin(Bizz bizz, RibbonTab tabOffer, RibbonTab tabAdministration, RibbonApplicationMenuItem menuitemChangePassWord, RibbonApplicationMenuItem menuItemLogOut, TextBlock userName, UserControl ucLeft, UserControl ucRight, bool ucRightActive)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.TabOffer = tabOffer;
            this.TabAdministration = tabAdministration;
            this.MenuItemChangePassWord = menuitemChangePassWord;
            this.MenuItemLogOut = menuItemLogOut;
            this.UserName = userName;
            this.UcLeft = ucLeft;
            this.UcRight = ucRight;
            this.UcRightActive = ucRightActive;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (CBZ.CheckCredentials(Bizz, UserName, MenuItemChangePassWord, MenuItemLogOut, TextBoxInitials.Text, TextBoxPassword.Password))
            {
                TabOffer.IsEnabled = true;
                if (Bizz.CurrentUser.Administrator)
                {
                    TabAdministration.IsEnabled = true;
                }
                UcRightActive = false;
                UcLeft.Content = new UserControl();
                UcRight.Content = new UserControl();
            }
            else
            {
                MessageBox.Show("Initialer eller password er forkert.");
            }
            //TabOffer.IsEnabled = true;
            //TabAdministration.IsEnabled = true;
            //UcRightActive = false;
            //UcLeft.Content = new UserControl();
            //UcRight.Content = new UserControl();
        }
    }
}
