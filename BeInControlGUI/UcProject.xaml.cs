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
    /// Interaction logic for UcProject.xaml
    /// </summary>
    public partial class UcProject : UserControl
    {
        public Bizz Bizz;
        public UserControl UcRight;
        public UserControl UcLeft;
        public bool UcRightActive;

        public UcProject(Bizz bizz, UserControl ucLeft, UserControl ucRight, bool ucRightActive)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcLeft = ucLeft;
            this.UcRight = ucRight;
            this.UcRightActive = ucRightActive;
        }

        private void ButtonCreateProject_Click(object sender, RoutedEventArgs e)
        {
            UcRightActive = true;
            UcCreateProject ucCreateProject = new UcCreateProject(Bizz, UcRight, UcRightActive);
            UcRight.Content = ucCreateProject;
        }

        private void ButtonEditProject_Click(object sender, RoutedEventArgs e)
        {
            UcRightActive = true;
            UcEditProject ucEditProject = new UcEditProject(Bizz, UcRight, UcRightActive);
            UcRight.Content = ucEditProject;
        }

        private void ButtonEditCaseId_Click(object sender, RoutedEventArgs e)
        {
            UcRightActive = true;
            UcEditCaseId ucEditCaseId = new UcEditCaseId(Bizz, UcRight, UcRightActive);
            UcRight.Content = ucEditCaseId;
        }

        private void ButtonReactivateProject_Click(object sender, RoutedEventArgs e)
        {
            UcRightActive = true;
            UcReactivateProject ucReactivateProject = new UcReactivateProject(Bizz, UcRight, UcRightActive);
            UcRight.Content = ucReactivateProject;
        }

        private void ButtonCopyProject_Click(object sender, RoutedEventArgs e)
        {
            UcRightActive = true;
            UcCopyProject ucCopyProject = new UcCopyProject(Bizz, UcRight, UcRightActive);
            UcRight.Content = ucCopyProject;
        }
    }
}
