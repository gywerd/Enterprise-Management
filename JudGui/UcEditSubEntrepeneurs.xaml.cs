using JudBizz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UcEditSubEntrepeneurs.xaml
    /// </summary>
    public partial class UcEditSubEntrepeneurs : UserControl
    {
        #region Fields
        public Bizz Bizz;
        public UserControl UcRight;
        public List<IndexableContact> IndexableContacts = new List<IndexableContact>();
        public List<IndexableEnterprise> IndexableEnterpriseList = new List<IndexableEnterprise>();
        public List<IndexableLegalEntity> IndexableLegalEntities = new List<IndexableLegalEntity>();
        public List<IndexableSubEntrepeneur> IndexableSubEntrepeneurs = new List<IndexableSubEntrepeneur>();

        #endregion

        public UcEditSubEntrepeneurs(Bizz bizz, UserControl ucRight)
        {
            InitializeComponent();
            this.Bizz = bizz;
            this.UcRight = ucRight;
            ComboBoxCaseId.ItemsSource = Bizz.ActiveProjects;
            ComboBoxRequest.ItemsSource = Bizz.RequestStatusList;
        }

        #region Buttons
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            //Warning about lost changes before closing
            if (MessageBox.Show("Vil du lukke redigering af Underentrepenører?", "Luk Rediger Underentrepenør", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Close right UserControl
                Bizz.UcRightActive = false;
                UcRight.Content = new UserControl();
            }
        }

        #endregion

        #region Events
        private void ComboBoxCaseId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ComboBoxCaseId.SelectedIndex;
            foreach (IndexableProject temp in Bizz.ActiveProjects)
            {
                if (temp.Index == selectedIndex)
                {
                    Bizz.tempProject = new Project(temp.Id, temp.CaseId, temp.Name, temp.Builder, temp.Status, temp.TenderForm, temp.EnterpriseForm, temp.Executive, temp.EnterpriseList, temp.Copy);
                }
            }
            TextBoxName.Text = Bizz.tempProject.Name;
            IndexableEnterpriseList = GetIndexableEnterpriseList();
            ComboBoxEnterprise.ItemsSource = IndexableEnterpriseList;
            ComboBoxEnterprise.SelectedIndex = 0;
            ClearTempEntities();
            TextBoxEntrepeneur.Text = "";
            ResetComboBoxes();
            ResetRadioButtons();
        }

        private void ComboBoxContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ComboBoxContact.SelectedIndex;
            if (selectedIndex >= 0)
            {
                bool changed = false;
                IndexableContact contact = IndexableContacts[selectedIndex];
                if (Bizz.tempSubEntrepeneur.Contact != contact.Id)
                {
                    Bizz.tempSubEntrepeneur.Contact = contact.Id;
                    changed = true;
                }
                if (changed)
                {
                    Bizz.CSE.UpdateSubEntrepeneurs(Bizz.tempSubEntrepeneur);
                }
                Bizz.SubEntrepeneurs.Clear();
                Bizz.SubEntrepeneurs = Bizz.CSE.GetSubEntrepeneurs();
            }
            else
            {
                Bizz.tempContact = new Contact(Bizz.strConnection);
                DateTime date = DateTime.Now;
                DateRequest.DisplayDate = date;
                DateRequest.Text = "";
            }
        }

        private void ComboBoxEnterprise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearTempEntities();
            TextBoxEntrepeneur.Text = "";
            TextBoxOfferPrice.Text = "";
            ResetComboBoxes();
            ResetRadioButtons();
            int selectedIndex = ComboBoxEnterprise.SelectedIndex;
            foreach (IndexableEnterprise temp in IndexableEnterpriseList)
            {
                if (temp.Index == selectedIndex)
                {
                    Bizz.tempEnterprise = new Enterprise(temp.Id, temp.Project, temp.Name, temp.Elaboration, temp.OfferList, temp.CraftGroup1, temp.CraftGroup2, temp.CraftGroup3, temp.CraftGroup4);
                }
            }
            IndexableSubEntrepeneurs = GetIndexableSubEntrepeneurs();
            ListBoxSubEntrepeneurs.UnselectAll();
            ListBoxSubEntrepeneurs.ItemsSource = null;
            if (IndexableSubEntrepeneurs.Count != 0)
            {
                ListBoxSubEntrepeneurs.ItemsSource = IndexableSubEntrepeneurs;
            }
        }

        private void ComboBoxRequest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ComboBoxRequest.SelectedIndex;
            if (selectedIndex >= 0)
            {
                bool changed = CheckRequest(selectedIndex);
                if (changed)
                {
                    UpdateRequestStatusInDb(selectedIndex, Bizz.tempSubEntrepeneur.Request);
                }
                Bizz.SubEntrepeneurs.Clear();
                Bizz.SubEntrepeneurs = Bizz.CSE.GetSubEntrepeneurs();
            }
            else
            {
                ComboBoxRequest.SelectedIndex = -1;
                Bizz.tempRequest = new Request(Bizz.strConnection);
                DateTime date = DateTime.Now;
                DateRequest.DisplayDate = date;
                DateRequest.Text = "";
            }
        }

        private void ListBoxSubEntrepeneurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ListBoxSubEntrepeneurs.SelectedIndex;
            if (selectedIndex == -1)
            {
                Bizz.tempSubEntrepeneur = new SubEntrepeneur();
                TextBoxEntrepeneur.Text = "";
                TextBoxOfferPrice.Text = "";
                ResetComboBoxes();
                ResetRadioButtons();
            }
            else if (selectedIndex < IndexableSubEntrepeneurs.Count && selectedIndex >= 0)
            {
                //TextBoxEntrepeneur.Text = "";
                //TextBoxOfferPrice.Text = "";
                //ResetComboBoxes();
                //ResetRadioButtons();
                Bizz.tempSubEntrepeneur = new SubEntrepeneur();
                IndexableSubEntrepeneurs.Clear();
                IndexableSubEntrepeneurs = GetIndexableSubEntrepeneurs();
                //ListBoxSubEntrepeneurs.ItemsSource = null;
                //ListBoxSubEntrepeneurs.ItemsSource = IndexableSubEntrepeneurs;
                SetBizzTempSubEntrepeneur(selectedIndex);
                TextBoxEntrepeneur.Text = Bizz.tempSubEntrepeneur.Name;
                SetComboBoxes();
                SetRadioButtons();
                TextBoxOfferPrice.Text = Bizz.tempOffer.Price.ToString();
            }
        }

        private void RadioButtonAgreementConcludedYes_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonAgreementConcludedYes.IsChecked = true;
                    RadioButtonAgreementConcludedNo.IsChecked = false;
                    bool changed = false;
                    if (Bizz.tempSubEntrepeneur.AgreementConcluded == false)
                    {
                        Bizz.tempSubEntrepeneur.ToggleAgreementConcluded();
                        changed = true;
                    }
                    if (changed)
                    {
                        ResetSubEntrepeneurs();
                    }
                }
                else
                {
                    RadioButtonAgreementConcludedYes.IsChecked = false;
                    RadioButtonAgreementConcludedNo.IsChecked = false;
                }
            }
        }

        private void RadioButtonAgreementConcludedNo_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonAgreementConcludedYes.IsChecked = false;
                    RadioButtonAgreementConcludedNo.IsChecked = true;
                    bool changed = false;
                    if (Bizz.tempSubEntrepeneur.AgreementConcluded == true)
                    {
                        Bizz.tempSubEntrepeneur.ToggleAgreementConcluded();
                        changed = true;
                    }
                    if (changed)
                    {
                        ResetSubEntrepeneurs();
                    }
                }
                else
                {
                    RadioButtonAgreementConcludedYes.IsChecked = false;
                    RadioButtonAgreementConcludedNo.IsChecked = false;
                }
            }
        }

        private void RadioButtonIttLetterSentYes_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonIttLetterSentYes.IsChecked = true;
                    RadioButtonIttLetterSentNo.IsChecked = false;
                    bool changed = CheckIttLetterSentYes();
                    if (changed)
                    {
                        ResetIttLetters();
                    }
                }
                else
                {
                    RadioButtonIttLetterSentYes.IsChecked = false;
                    RadioButtonIttLetterSentNo.IsChecked = false;
                }
            }
        }

        private void RadioButtonIttLetterSentNo_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonIttLetterSentYes.IsChecked = false;
                    RadioButtonIttLetterSentNo.IsChecked = true;
                    bool changed = CheckIttLetterSentNo();
                    if (changed)
                    {
                        ResetIttLetters();
                    }
                }
                else
                {
                    RadioButtonIttLetterSentYes.IsChecked = false;
                    RadioButtonIttLetterSentNo.IsChecked = false;
                    DateIttLetter.DisplayDate = DateTime.Now;
                    DateIttLetter.Text = "";
                    TextBoxOfferPrice.Text = "";
                }
            }
        }

        private void RadioButtonOfferChosenYes_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonOfferChosenYes.IsChecked = true;
                    RadioButtonOfferChosenNo.IsChecked = false;
                    bool changed = CheckOfferChosenYes();
                    if (changed)
                    {
                        ResetOffers();
                    }
                }
                else
                {
                    RadioButtonOfferChosenYes.IsChecked = false;
                    RadioButtonOfferChosenNo.IsChecked = false;
                }
            }
        }

        private void RadioButtonOfferChosenNo_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonOfferChosenYes.IsChecked = false;
                    RadioButtonOfferChosenNo.IsChecked = true;
                    bool changed = CheckOfferChosenNo();
                    if (changed)
                    {
                        ResetOffers();
                    }
                }
                else
                {
                    RadioButtonOfferChosenYes.IsChecked = false;
                    RadioButtonOfferChosenNo.IsChecked = false;
                }
            }
        }

        private void RadioButtonOfferReceivedYes_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonOfferReceivedYes.IsChecked = true;
                    RadioButtonOfferReceivedNo.IsChecked = false;
                    bool changed = CheckOfferReceivedYes();
                    if (changed)
                    {
                        ResetOffers();
                    }
                }
                else
                {
                    RadioButtonOfferReceivedYes.IsChecked = false;
                    RadioButtonOfferReceivedYes.IsChecked = false;
                    DateTime date = DateTime.Now;
                    DateOffer.DisplayDate = date;
                    DateOffer.Text = "";
                    TextBoxOfferPrice.Text = "";
                }
            }
        }

        private void RadioButtonOfferReceivedNo_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonOfferReceivedYes.IsChecked = false;
                    RadioButtonOfferReceivedNo.IsChecked = true;
                    bool changed = CheckOfferReceivedNo();
                    if (changed)
                    {
                        ResetOffers();
                    }
                }
                else
                {
                    RadioButtonOfferReceivedYes.IsChecked = false;
                    RadioButtonOfferReceivedNo.IsChecked = false;
                    DateTime date = DateTime.Now;
                    DateOffer.DisplayDate = date;
                    DateOffer.Text = "";
                    TextBoxOfferPrice.Text = "0,00" ;
                }
            }
        }

        private void RadioButtonReservationsYes_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonReservationsYes.IsChecked = true;
                    RadioButtonReservationsNo.IsChecked = false;
                    bool changed = false;
                    if (Bizz.tempSubEntrepeneur.Reservations == false)
                    {
                        Bizz.tempSubEntrepeneur.ToggleReservations();
                        changed = true;
                    }
                    if (changed)
                    {
                        ResetSubEntrepeneurs();
                    }
                }
                else
                {
                    RadioButtonReservationsYes.IsChecked = false;
                    RadioButtonReservationsNo.IsChecked = false;
                }
            }
        }

        private void RadioButtonReservationsNo_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonReservationsYes.IsChecked = false;
                    RadioButtonReservationsNo.IsChecked = true;
                    bool changed = false;
                    if (Bizz.tempSubEntrepeneur.Reservations == true)
                    {
                        Bizz.tempSubEntrepeneur.ToggleReservations();
                        changed = true;
                    }
                    if (changed)
                    {
                        ResetSubEntrepeneurs();
                    }
                }
                else
                {
                    RadioButtonReservationsYes.IsChecked = false;
                    RadioButtonReservationsNo.IsChecked = false;
                }
            }
        }

        private void RadioButtonUpholdYes_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonUpholdYes.IsChecked = true;
                    RadioButtonUpholdNo.IsChecked = false;
                    bool changed = false;
                    if (Bizz.tempSubEntrepeneur.Uphold == false)
                    {
                        Bizz.tempSubEntrepeneur.ToggleUphold();
                        changed = true;
                    }
                    if (changed)
                    {
                        ResetSubEntrepeneurs();
                    }
                }
                else
                {
                    RadioButtonUpholdYes.IsChecked = false;
                    RadioButtonUpholdNo.IsChecked = false;
                }
            }
        }

        private void RadioButtonUpholdNo_Checked(object sender, RoutedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    RadioButtonUpholdYes.IsChecked = false;
                    RadioButtonUpholdNo.IsChecked = true;
                    bool changed = false;
                    if (Bizz.tempSubEntrepeneur.Uphold == true)
                    {
                        Bizz.tempSubEntrepeneur.ToggleUphold();
                        changed = true;
                    }
                    if (changed)
                    {
                        ResetSubEntrepeneurs();
                    }
                }
                else
                {
                    RadioButtonUpholdYes.IsChecked = false;
                    RadioButtonUpholdNo.IsChecked = false;
                }
            }
        }

        private void TextBoxOfferPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Bizz != null)
            {
                bool status = CheckTempSubEntrepeneur();
                if (status)
                {
                    string temp = TextBoxOfferPrice.Text;
                    temp = ParseOfferPrice(temp);
                    TextBoxOfferPrice.Text = temp;
                    bool changed = CheckOfferPriceForChanges(temp);
                    if (changed)
                    {
                        ResetOffers();
                    }
                }
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method that compares CraftGroups in LegalEntities and EnterpriseList
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        private bool CheckCraftGroups(LegalEntity entity)
        {
            if (entity.CraftGroup4 != 0)
            {
                if (entity.CraftGroup4 == Bizz.tempEnterprise.CraftGroup1 || entity.CraftGroup4 == Bizz.tempEnterprise.CraftGroup2 || entity.CraftGroup4 == Bizz.tempEnterprise.CraftGroup3 || entity.CraftGroup4 == Bizz.tempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            else if (entity.CraftGroup3 != 0)
            {
                if (entity.CraftGroup3 == Bizz.tempEnterprise.CraftGroup1 || entity.CraftGroup3 == Bizz.tempEnterprise.CraftGroup2 || entity.CraftGroup3 == Bizz.tempEnterprise.CraftGroup3 || entity.CraftGroup3 == Bizz.tempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            else if (entity.CraftGroup2 != 0)
            {
                if (entity.CraftGroup2 == Bizz.tempEnterprise.CraftGroup1 || entity.CraftGroup2 == Bizz.tempEnterprise.CraftGroup2 || entity.CraftGroup2 == Bizz.tempEnterprise.CraftGroup3 || entity.CraftGroup2 == Bizz.tempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            else if (entity.CraftGroup1 != 0)
            {
                if (entity.CraftGroup1 == Bizz.tempEnterprise.CraftGroup1 || entity.CraftGroup1 == Bizz.tempEnterprise.CraftGroup2 || entity.CraftGroup1 == Bizz.tempEnterprise.CraftGroup3 || entity.CraftGroup1 == Bizz.tempEnterprise.CraftGroup4)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method, that checks content wether Bizz.tempIttLetter.SentDate has been changed
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckBizzTempDate(DateTime date, object temp)
        {
            bool result = false;
            if (temp == null)
            {
                return result;
            }

            DateTime tempDate = new DateTime();
            string type = temp.GetType().ToString();
            switch (type)
            {
                case "IttLetter":
                    IttLetter tempIttLetter = new IttLetter((IttLetter)temp);
                    tempDate = Convert.ToDateTime(tempIttLetter.SentDate);
                    break;
                case "Offer":
                    Offer tempOffer = new Offer((Offer)temp);
                    tempDate = Convert.ToDateTime(tempOffer.ReceivedDate);
                    break;
            }

            if (tempDate.ToShortDateString().Substring(0,10) != DateIttLetter.DisplayDate.ToShortDateString().Substring(0, 10))
            {
                switch (type)
                {
                    case "IttLetter":
                        Bizz.tempIttLetter.SentDate = date;
                        result = true;
                        break;
                    case "Offer":
                        Bizz.tempOffer.SetReceivedDate(date);
                        result = true;
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Method, that checks the wether checking RadioButtonIttLetterSentNo results in changes, that needs to be written to Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckIttLetterSentNo()
        {
            bool result = false;
            if (Bizz.tempIttLetter.Sent == true)
            {
                Bizz.tempIttLetter.ToggleSent();
                result = true;
            }
            DateTime date = Convert.ToDateTime("1899-12-31");
            DateIttLetter.DisplayDate = date;
            DateIttLetter.Text = date.ToShortDateString();
            bool tempChanged = CheckBizzTempDate(date, Bizz.tempIttLetter);
            if (!result)
            {
                result = tempChanged;
            }
            return result;
        }

        /// <summary>
        /// Method, that checks the wether checking RadioButtonIttLetterSentYes results in changes, that needs to be written to Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckIttLetterSentYes()
        {
            bool result = false;
            if (Bizz.tempIttLetter.Sent == false)
            {
                Bizz.tempIttLetter.ToggleSent();
                result = true;
            }
            DateTime date = Convert.ToDateTime(Bizz.tempIttLetter.SentDate);
            if (DateIttLetter.Text != "" && DateIttLetter.Text != "1899-12-31")
            {
                if (DateIttLetter.Text == DateOffer.DisplayDate.ToShortTimeString().Substring(0, 10))
                {
                    date = DateIttLetter.DisplayDate;
                    bool tempChanged = CheckBizzTempDate(date, Bizz.tempOffer);
                    if (!result)
                    {
                        result = tempChanged;
                    }
                }
                else
                {
                    date = Convert.ToDateTime(DateOffer.Text);
                    DateIttLetter.DisplayDate = date;
                    bool tempChanged = CheckBizzTempDate(date, Bizz.tempOffer);
                    if (!result)
                    {
                        result = tempChanged;
                    }
                }
            }
            if (DateIttLetter.Text == "")
            {
                date = DateTime.Now;
                DateIttLetter.DisplayDate = date;
                DateIttLetter.Text = date.ToShortDateString();
                bool tempChanged = CheckBizzTempDate(date, Bizz.tempOffer);
                if (!result)
                {
                    result = tempChanged;
                }
            }
            return result;
        }

        /// <summary>
        /// Method, that checks the price for changes, that needs to be written to Db
        /// </summary>
        /// <param name="temp">string</param>
        /// <returns>bool</returns>
        private bool CheckOfferPriceForChanges(string temp)
        {
            bool result = false;
            if (ParseOfferPrice(Bizz.tempOffer.Price.ToString()) != TextBoxOfferPrice.Text)
            {
                temp = Regex.Replace(temp, "[,]", ".");
                if (temp == "")
                {
                    TextBoxOfferPrice.Text = "0";
                }
                if (Bizz.tempOffer.Price != Convert.ToDouble(TextBoxOfferPrice.Text))
                {
                    Bizz.tempOffer.Price = Convert.ToDouble(TextBoxOfferPrice.Text);
                    result = true;
                }
            }
            if (Bizz.tempOffer.Received && DateOffer.Text.Substring(0, 10) == "31-12-2018")
            {
                DateTime tempDate = DateTime.Now;
                DateOffer.Text = tempDate.ToShortDateString();
                DateOffer.DisplayDate = tempDate;
                Bizz.tempOffer.SetReceivedDate(tempDate);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Method, that checks the wether checking RadioButtonOfferChosenNo results in changes, that needs to be written to Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckOfferChosenNo()
        {
            bool result = false;
            if (Bizz.tempOffer.Chosen == true)
            {
                Bizz.tempOffer.ToggleChosen();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Method, that checks the wether checking RadioButtonOfferChosenYes results in changes, that needs to be written to Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckOfferChosenYes()
        {
            bool result = false;
            if (Bizz.tempOffer.Chosen == false)
            {
                Bizz.tempOffer.ToggleChosen();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Method, that checks the wether checking RadioButtonOfferReceivedNo results in changes, that needs to be written to Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckOfferReceivedNo()
        {
            bool result = false;
            if (Bizz.tempOffer.Received == true)
            {
                Bizz.tempOffer.ToggleReceived();
                result = true;
            }
            DateTime date = Convert.ToDateTime("1899-12-31");
            if (Bizz.tempOffer.ReceivedDate != date)
            {
                Bizz.tempOffer.ResetReceived();
                result = true;
            }
            DateOffer.DisplayDate = date;
            DateOffer.Text = date.ToShortDateString();
            TextBoxOfferPrice.Text = "";
            if (Bizz.tempOffer.Price == Convert.ToDouble(0))
            {
                Bizz.tempOffer.Price = Convert.ToDouble(0);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Method, that checks the wether checking RadioButtonOfferReceivedYes results in changes, that needs to be written to Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckOfferReceivedYes()
        {
            bool result = false;
            if (Bizz.tempOffer.Received == false)
            {
                Bizz.tempOffer.ToggleReceived();
                result = true;
            }
            if (Bizz.tempOffer.ReceivedDate.Value.ToShortDateString().Substring(0, 10) == "31-12-1899")
            {
                Bizz.tempOffer.SetReceivedDate(DateTime.Now);
                result = true;
            }
            if (DateOffer.Text == "" || DateOffer.Text.Substring(0, 10) == "31-12-1899")
            {
                DateOffer.Text = DateTime.Now.ToShortDateString();
            }
            if (DateOffer.Text.Substring(0, 10) != Bizz.tempOffer.ReceivedDate.Value.ToShortDateString().Substring(0, 10))
            {
                Bizz.tempOffer.SetReceivedDate(DateOffer.DisplayDate);
                result = true;
            }
            if (DateOffer.Text != DateOffer.DisplayDate.ToShortDateString().Substring(0, 10))
            {
                DateOffer.DisplayDate = Convert.ToDateTime(DateOffer.Text);
            }
            if (TextBoxOfferPrice.Text == "")
            {
                TextBoxOfferPrice.Text = "0";
                Bizz.tempOffer.Price = Convert.ToDouble(TextBoxOfferPrice.Text);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Method, that checks the wether changed ComboBoxRequest selection results in changes, that needs to be written to Db
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckRequest(int index)
        {
            bool result = false;
            ComboBoxRequest.SelectedIndex = index;
            if (Bizz.tempRequest.Status != index)
            {
                Bizz.tempRequest.Status = index;
                result = true;
            }
            DateTime date = GetRequestDate(Bizz.tempRequest);
            if (index == 0)
            {
                if (Bizz.tempRequest.SentDate.Value.ToShortDateString().Substring(0, 10) != "31-12-1899" || Bizz.tempRequest.ReceivedDate.Value.ToShortDateString().Substring(0, 10) != "31-12-1899")
                {
                    date = Convert.ToDateTime("31-12-1899");
                    Bizz.tempRequest.ReceivedDate = date;
                    Bizz.tempRequest.SentDate = date;
                    result = true;
                }
            }
            DateRequest.DisplayDate = date;
            DateRequest.Text = date.ToShortDateString();
            return result;
        }

        /// <summary>
        /// Method, that checks content oftempSubEntrepeneur
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckTempSubEntrepeneur()
        {
            bool result = false;
            SubEntrepeneur temp = new SubEntrepeneur(Bizz.strConnection, Bizz.LegalEntities);
            if (Bizz.tempSubEntrepeneur != temp)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Method, that clears tempSubEntrepeneur, tempContact, tempRequest, tempIttLetter & tempOffer
        /// </summary>
        private void ClearTempEntities()
        {
            Bizz.tempSubEntrepeneur = new SubEntrepeneur(Bizz.strConnection,Bizz.LegalEntities);
            Bizz.tempContact = new Contact(Bizz.strConnection);
            Bizz.tempRequest = new Request(Bizz.strConnection);
            Bizz.tempIttLetter = new IttLetter(Bizz.strConnection);
            Bizz.tempOffer = new Offer(Bizz.strConnection);
        }

        /// <summary>
        /// Method, that filters existing Legal Entities in SubEntrepeneurs from list of indexable Legal Entities
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private List<IndexableLegalEntity> FilterIndexableLegalEntities(List<IndexableLegalEntity> list)
        {
            List<IndexableLegalEntity> tempList = new List<IndexableLegalEntity>();
            List<IndexableLegalEntity> tempResult = new List<IndexableLegalEntity>();
            List<IndexableLegalEntity> result = new List<IndexableLegalEntity>();
            int i = 0;
            foreach (IndexableLegalEntity temp in list)
            {
                if (temp.Active.Equals(true))
                {
                    tempList.Add(temp);
                }
            }
            foreach (IndexableLegalEntity temp in tempList)
            {
                if (!IdExistsInSubEntrepeneurs(Bizz.tempEnterprise.Id, temp.Id))
                {
                    LegalEntity legalEntity = new LegalEntity(temp.Id, temp.Name, temp.Address, temp.ContactInfo, temp.Url, temp.CraftGroup1, temp.CraftGroup2, temp.CraftGroup3, temp.CraftGroup4, temp.Region, temp.CountryWide, temp.Cooperative, temp.Active);
                    IndexableLegalEntity entity = new IndexableLegalEntity(i, legalEntity);
                    tempResult.Add(entity);
                }
            }
            int region = tempResult[1].Region;
            foreach (IndexableLegalEntity entity in tempResult)
            {
                if (entity.Region == region)
                {
                    result.Add(entity);
                }
            }
            foreach (IndexableLegalEntity entity in tempResult)
            {
                if (entity.Region != region && entity.CountryWide.Equals(true))
                {
                    result.Add(entity);
                }
            }
            return result;
        }

        /// Methods, creates a list of indexable Contacts
        /// </summary>
        /// <returns>List<IndexableContact></returns>
        private List<IndexableContact> GetIndexableContacts()
        {
            List<IndexableContact> result = new List<IndexableContact>();
            string id = Bizz.tempSubEntrepeneur.Entrepeneur;
            IndexableContact notSpecified = new IndexableContact(0, Bizz.Contacts[0]);
            result.Add(notSpecified);
            int i = 1;
            foreach (Contact contact in Bizz.Contacts)
            {
                if (contact.LegalEntity == id)
                {
                    IndexableContact temp = new IndexableContact(i, contact);
                    result.Add(temp);
                    i++;
                }
            }
            return result;
        }

        /// <summary>
        /// Methods, creates a list of indexable Enterprises
        /// </summary>
        /// <returns>List<IndexableEnterprise></returns>
        private List<IndexableEnterprise> GetIndexableEnterpriseList()
        {
            List<IndexableEnterprise> result = new List<IndexableEnterprise>();
            IndexableEnterprise notSpecified = new IndexableEnterprise(0, Bizz.EnterpriseList[0]);
            result.Add(notSpecified);
            int i = 1;
            foreach (Enterprise enterprise in Bizz.EnterpriseList)
            {
                if (enterprise.Project == Bizz.tempProject.Id)
                {
                    IndexableEnterprise temp = new IndexableEnterprise(i, enterprise);
                    result.Add(temp);
                    i++;
                }
            }
            return result;
        }

        /// <summary>
        /// Method that creates a list of indexable Legal Entities
        /// </summary>
        /// <returns>List<IndexableLegalEntity></returns>
        private List<IndexableLegalEntity> GetIndexableLegalEntities()
        {
            List<IndexableLegalEntity> result = new List<IndexableLegalEntity>();
            IndexableLegalEntity notSpecified = new IndexableLegalEntity(0, Bizz.LegalEntities[0]);
            result.Add(notSpecified);
            int i = 1;
            foreach (LegalEntity entity in Bizz.LegalEntities)
            {
                if (CheckCraftGroups(entity))
                {
                    IndexableLegalEntity temp = new IndexableLegalEntity(i, entity);
                    result.Add(temp);
                    i++;
                }
            }
            result = FilterIndexableLegalEntities(result);
            return result;
        }

        /// <summary>
        /// Method that creates a list of indexable SubEbtrepeneurs
        /// </summary>
        /// <returns>List<IndexableLegalEntity></returns>
        private List<IndexableSubEntrepeneur> GetIndexableSubEntrepeneurs()
        {
            List<IndexableSubEntrepeneur> result = new List<IndexableSubEntrepeneur>();
            int i = 0;
            int id = Bizz.tempEnterprise.Id;
            foreach (SubEntrepeneur subEntrepeneur in Bizz.SubEntrepeneurs)
            {
                if (subEntrepeneur.EnterpriseList == id)
                {
                    IndexableSubEntrepeneur temp = new IndexableSubEntrepeneur(Bizz.strConnection, Bizz.LegalEntities, i, subEntrepeneur);
                    result.Add(temp);
                    i++;
                }
            }
            return result;
        }

        /// <summary>
        /// Method, that checks request status and return SentDate or ReceivedDate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private DateTime GetRequestDate(Request request)
        {
            DateTime date = Convert.ToDateTime("1899-12-31");

            switch (request.Status)
            {
                case 0:
                    return Convert.ToDateTime("1899-12-31");
                case 1:
                    return request.SentDate.Value;
                case 2:
                    return request.ReceivedDate.Value;
                case 3:
                    return request.ReceivedDate.Value;
                case 4:
                    return request.ReceivedDate.Value;
                case 5:
                    return request.ReceivedDate.Value;
                case 6:
                    return request.ReceivedDate.Value;
                case 7:
                    return request.ReceivedDate.Value;
                case 8:
                    return request.ReceivedDate.Value;
                case 9:
                    return request.ReceivedDate.Value;
                case 10:
                    return request.ReceivedDate.Value;
            }
            return date;
        }

        /// <summary>
        /// Method that returns index for selected Contact
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>int</returns>
        private int GetSelectedContact(int id)
        {
            int result = 0;
            foreach (IndexableContact temp in IndexableContacts)
            {
                try
                {
                    if (temp.Id == id)
                    {
                        result = temp.Index;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Den valgte kontakt kunne ikke findes", "Find Valgt Kontakt", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            return result;
        }

        /// <summary>
        /// Method that returns index for selected RequestStatus
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>Request</returns>
        private Request GetSelectedRequest(int id)
        {
            Request result = new Request(Bizz.strConnection);
            foreach (Request temp in Bizz.Requests)
            {
                try
                {
                    if (temp.Id == id)
                    {
                        result = temp;
                    }
                }
                catch (Exception)
                {
                    return result;
                }
            }
            return result;
        }

        /// Method, that checks if a legal entity is already added to SubEntrepeneurs
        /// </summary>
        /// <param name="enterpriseList">int</param>
        /// <param name="entrepeneur">string</param>
        /// <returns>bool</returns>
        private bool IdExistsInSubEntrepeneurs(int enterpriseList, string entrepeneur)
        {

            foreach (SubEntrepeneur temp in Bizz.SubEntrepeneurs)
            {
                if (temp.Entrepeneur == entrepeneur && temp.EnterpriseList == enterpriseList)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method, that removes letters from string and adds to digit if necessary
        /// </summary>
        /// <param name="result">string</param>
        /// <returns>string</returns>
        private string ParseOfferPrice(string price)
        {
            string result = Regex.Replace(price, "[A-Za-z ]", "");
            result = Regex.Replace(result, "[.]", ",");
            bool comma = false;
            int i = 0;
            int j = 0;
            foreach (char c in result)
            {
                string cc = c.ToString();
                if (cc == ",")
                {
                    comma = true;
                    j++;
                }
                if (j > 1)
                {
                    result = result.Substring(0, i);
                    break;
                }
                i++;
            }
            if (!comma)
            {
                result += ",00";
            }
            if (result == ",00")
            {
                result = "0,00";
            }
            return result;
        }

        /// <summary>
        /// Method, that's resets Combocoxes
        /// </summary>
        private void ResetComboBoxes()
        {
            ComboBoxContact.SelectedIndex = -1;
            ComboBoxContact.ItemsSource = null;
            Bizz.tempContact = new Contact(Bizz.strConnection);
            ComboBoxRequest.SelectedIndex = -1;
            ComboBoxRequest.ItemsSource = Bizz.RequestStatusList;
            DateRequest.DisplayDate = DateTime.Now;
            DateRequest.Text = "";
            Bizz.tempRequest = new Request(Bizz.strConnection);
        }

        /// <summary>
        /// Method clears and updates IttLetters
        /// </summary>
        private void ResetIttLetters()
        {
            SubEntrepeneur temp = Bizz.tempSubEntrepeneur;
            UpdateIttLetterSentInDb(Bizz.tempIttLetter.Id, true);
            Bizz.IttLetters.Clear();
            Bizz.IttLetters = Bizz.CIL.GetIttLetters();
        }

        /// <summary>
        /// Method clears and updates Offers
        /// </summary>
        private void ResetOffers()
        {
            SubEntrepeneur temp = Bizz.tempSubEntrepeneur;
            UpdateOfferReceivedInDb(temp.Offer, true);
            Bizz.Offers.Clear();
            Bizz.Offers = Bizz.COF.GetOffers();
        }

        /// <summary>
        /// Method, that reset's RadioButtons
        /// </summary>
        private void ResetRadioButtons()
        {
            ResetRadioButtonsIttLetterSent();
            ResetRadioButtonsOfferReceived();
            ResetRadioButtonsReservations();
            ResetRadioButtonsUphold();
            ResetRadioButtonsAgreementConcluded();
        }

        /// <summary>
        /// Method, that resets RadioButtonAgreementConcludedYes and RadioButtonAgreementConcludedNo
        /// </summary>
        private void ResetRadioButtonsAgreementConcluded()
        {
            RadioButtonAgreementConcludedYes.IsChecked = false;
            RadioButtonAgreementConcludedNo.IsChecked = false;
        }

        /// <summary>
        /// Method, that resets RadioButtonIttLetterYes and RadioButtonIttLetterNo
        /// </summary>
        private void ResetRadioButtonsIttLetterSent()
        {
            Bizz.tempIttLetter = new IttLetter(Bizz.strConnection);
            DateIttLetter.DisplayDate = DateTime.Now;
            DateIttLetter.Text = "";
            RadioButtonIttLetterSentYes.IsChecked = false;
            RadioButtonIttLetterSentNo.IsChecked = false;
        }

        /// <summary>
        /// Method, that resets RadioButtonOfferReceivedYes and RadioButtonOfferReceivedNo
        /// </summary>
        private void ResetRadioButtonsOfferReceived()
        {
            Bizz.tempOffer = new Offer(Bizz.strConnection);
            DateOffer.DisplayDate = DateTime.Now;
            DateOffer.Text = "";
            RadioButtonOfferReceivedYes.IsChecked = false;
            RadioButtonOfferReceivedNo.IsChecked = false;
        }

        /// <summary>
        /// Method, that resets RadioButtonReservationsYes and RadioButtonReservationsNo
        /// </summary>
        private void ResetRadioButtonsReservations()
        {
            RadioButtonReservationsYes.IsChecked = false;
            RadioButtonReservationsNo.IsChecked = false;
        }

        /// <summary>
        /// Method, that sets RadioButtonUpholdYes and RadioButtonUpholdNo
        /// </summary>
        private void ResetRadioButtonsUphold()
        {
            RadioButtonUpholdYes.IsChecked = false;
            RadioButtonUpholdNo.IsChecked = false;
        }

        /// <summary>
        /// Method, that clears and update SubEntrepeneurs
        /// </summary>
        private void ResetSubEntrepeneurs()
        {
            Bizz.CSE.UpdateSubEntrepeneurs(Bizz.tempSubEntrepeneur);
            Bizz.SubEntrepeneurs.Clear();
            Bizz.SubEntrepeneurs = Bizz.CSE.GetSubEntrepeneurs();
            IndexableSubEntrepeneurs.Clear();
            IndexableSubEntrepeneurs = GetIndexableSubEntrepeneurs();
            ListBoxSubEntrepeneurs.ItemsSource = null;
            ListBoxSubEntrepeneurs.ItemsSource = IndexableSubEntrepeneurs;
        }

        /// <summary>
        /// Method, that sets content of Bizz.tempIttLetter
        /// </summary>
        /// <param name="id">int</param>
        private void SetBizzTempIttLetter(int id)
        {
            try
            {
                foreach (IttLetter temp in Bizz.IttLetters)
                {
                    if (temp.Id == id)
                    {
                        Bizz.tempIttLetter = temp;
                    }
                }
            }
            catch (Exception)
            {
                Bizz.tempIttLetter = new IttLetter(Bizz.strConnection);
            }
        }

        /// <summary>
        /// Method, that sets content of Bizz.tempIttLetter
        /// </summary>
        /// <param name="id">int</param>
        private void SetBizzTempOffer(int id)
        {
            try
            {
                foreach (Offer temp in Bizz.Offers)
                {
                    if (temp.Id == id)
                    {
                        Bizz.tempOffer = temp;
                    }
                }
            }
            catch (Exception)
            {
                Bizz.tempOffer = new Offer(Bizz.strConnection);
            }
        }

        /// <summary>
        /// Method, that sets content of Bizz.tempSubEntrepeneur
        /// </summary>
        private void SetBizzTempSubEntrepeneur(int index)
        {
            IndexableSubEntrepeneur temp = IndexableSubEntrepeneurs[index];
            Bizz.tempSubEntrepeneur = new SubEntrepeneur(Bizz.strConnection, Bizz.LegalEntities, temp.Id, temp.EnterpriseList, temp.Entrepeneur, temp.Contact, temp.Request, temp.IttLetter, temp.Offer, temp.Reservations, temp.Uphold, temp.AgreementConcluded, temp.Active);
            if (!Bizz.tempSubEntrepeneur.Active)
            {
                Bizz.tempSubEntrepeneur.ToggleActive();
            }
        }

        /// <summary>
        /// Method, that populates ComboBoxes
        /// </summary>
        private void SetComboBoxes()
        {
            IndexableContacts = GetIndexableContacts();
            int contactIndex = GetSelectedContact(Bizz.tempSubEntrepeneur.Contact);
            ComboBoxContact.ItemsSource = IndexableContacts;
            ComboBoxContact.SelectedIndex = contactIndex;
            Bizz.tempRequest = GetSelectedRequest(Bizz.tempSubEntrepeneur.Request);
            DateTime date = GetRequestDate(Bizz.tempRequest);
            DateRequest.DisplayDate = date;
            DateRequest.Text = date.ToShortDateString();
            ComboBoxRequest.ItemsSource = Bizz.RequestStatusList;
            ComboBoxRequest.SelectedIndex = Bizz.tempRequest.Status;
        }

        /// <summary>
        /// Method, that sets values for RadioButtons
        /// </summary>
        private void SetRadioButtons()
        {
            SetRadioButtonsIttLetterSent(Bizz.tempSubEntrepeneur.IttLetter);
            SetRadioButtonsOfferReceived(Bizz.tempSubEntrepeneur.Offer);
            SetRadioButtonsReservations(Bizz.tempSubEntrepeneur.Reservations);
            SetRadioButtonsUphold(Bizz.tempSubEntrepeneur.Uphold);
            SetRadioButtonsAgreementConcluded(Bizz.tempSubEntrepeneur.AgreementConcluded);
        }

        /// <summary>
        /// Method, that sets RadioButtonAgreementConcludedYes and RadioButtonAgreementConcludedNo
        /// </summary>
        /// <param name="agreementConcluded">bool</param>
        private void SetRadioButtonsAgreementConcluded(bool agreementConcluded)
        {
            if (agreementConcluded)
            {
                RadioButtonAgreementConcludedYes.IsChecked = true;
                RadioButtonAgreementConcludedNo.IsChecked = false;
            }
            else
            {
                RadioButtonAgreementConcludedYes.IsChecked = false;
                RadioButtonAgreementConcludedNo.IsChecked = true;
            }
        }

        /// <summary>
        /// Method, that sets RadioButtonIttLetterYes and RadioButtonIttLetterNo
        /// </summary>
        /// <param name="ittLetter">int</param>
        private void SetRadioButtonsIttLetterSent(int ittLetter)

        {
            SetBizzTempIttLetter(ittLetter);
            if (Bizz.tempIttLetter.Sent)
            {
                if (Bizz.tempIttLetter.SentDate.Value.ToShortDateString() == "")
                {
                    Bizz.tempIttLetter.SentDate = DateTime.Now;
                }
                DateIttLetter.DisplayDate = Bizz.tempIttLetter.SentDate.Value;
                DateIttLetter.Text = Bizz.tempIttLetter.SentDate.Value.ToShortDateString();
                RadioButtonIttLetterSentYes.IsChecked = true;
                RadioButtonIttLetterSentNo.IsChecked = false;
            }
            else
            {
                Bizz.tempIttLetter.SentDate = Convert.ToDateTime("1899-12-31");
                DateIttLetter.DisplayDate = Bizz.tempIttLetter.SentDate.Value;
                DateIttLetter.Text = Bizz.tempIttLetter.SentDate.Value.ToShortDateString();
                RadioButtonIttLetterSentYes.IsChecked = false;
                RadioButtonIttLetterSentNo.IsChecked = true;
            }
        }

        /// <summary>
        /// Method, that sets RadioButtonOfferReceivedYes and RadioButtonOfferReceivedNo
        /// </summary>
        /// <param name="offer">int</param>
        private void SetRadioButtonsOfferReceived(int offer)
        {
            SetBizzTempOffer(offer);
            if (Bizz.tempOffer.Received)
            {
                if (Bizz.tempOffer.ReceivedDate.Value.ToShortDateString() == "" || Bizz.tempOffer.ReceivedDate.Value.ToShortDateString().Substring(0, 10) == "1899-12-31")
                {
                    Bizz.tempOffer.AddReceived(DateTime.Now);
                }
                DateOffer.DisplayDate = Bizz.tempOffer.ReceivedDate.Value;
                DateOffer.Text = Bizz.tempOffer.ReceivedDate.Value.ToShortDateString();
                RadioButtonOfferReceivedYes.IsChecked = true;
                RadioButtonOfferReceivedNo.IsChecked = false;
            }
            else
            {
                Bizz.tempOffer.AddReceived(Convert.ToDateTime("1899-12-31"));
                DateOffer.DisplayDate = Bizz.tempOffer.ReceivedDate.Value;
                DateOffer.Text = Bizz.tempOffer.ReceivedDate.Value.ToShortDateString();
                RadioButtonOfferReceivedYes.IsChecked = false;
                RadioButtonOfferReceivedNo.IsChecked = true;
            }
        }

        /// <summary>
        /// Method, that sets RadioButtonReservationsYes and RadioButtonReservationsNo
        /// </summary>
        /// <param name="reservations">bool</param>
        private void SetRadioButtonsReservations(bool reservations)
        {
            if (reservations)
            {
                RadioButtonReservationsYes.IsChecked = true;
                RadioButtonReservationsNo.IsChecked = false;
            }
            else
            {
                RadioButtonReservationsYes.IsChecked = false;
                RadioButtonReservationsNo.IsChecked = true;
            }
        }

        /// <summary>
        /// Method, that sets RadioButtonUpholdYes and RadioButtonUpholdNo
        /// </summary>
        /// <param name="uphold">bool</param>
        private void SetRadioButtonsUphold(bool uphold)
        {
            if (uphold)
            {
                RadioButtonUpholdYes.IsChecked = true;
                RadioButtonUpholdNo.IsChecked = false;
            }
            else
            {
                RadioButtonUpholdYes.IsChecked = false;
                RadioButtonUpholdNo.IsChecked = true;
            }
        }


        /// <summary>
        /// Method, that update sent status on an IttLetter in Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="sent">bool</param>
        private void UpdateIttLetterSentInDb(int id, bool sent)
        {
            string date = DateIttLetter.DisplayDate.Year + "-" + DateIttLetter.DisplayDate.Month + "-" + DateIttLetter.DisplayDate.Day;
            // Code that save changes to the project
            bool result = Bizz.CIL.UpdateIttLetterSent(id, sent, date);

            if (result)
            {
                //Show confirmation
                MessageBox.Show("Udbudsbrevets status blev rettet.", "Ret Udbudsbrevsstatus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Udbudsbrevets status blev ikke rettet. Prøv igen.", "Ret Udbudsbrevsstatus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Method, that update received status on an Offer in Db
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="received">bool</param>
        private void UpdateOfferReceivedInDb(int id, bool received)
        {
            string date = DateOffer.DisplayDate.Year + "-" + DateOffer.DisplayDate.Month + "-" + DateOffer.DisplayDate.Day;
            // Code that save changes to the project
            bool result = Bizz.COF.UpdateOfferReceived(id, received, date);

            if (result)
            {
                //Show confirmation
                MessageBox.Show("Tilbuddets status blev rettet.", "Ret Tilbudsstatus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Tilbuddets status blev ikke rettet. Prøv igen.", "Ret Tilbudsstatus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Method, that update Request status on an entry in Db
        /// </summary>
        /// <param name="sent">bool</param>
        /// <param name="id">int</param>
        private void UpdateRequestStatusInDb(int status, int id)
        {
            // Code that save changes to the project
            bool result = Bizz.CRQ.UpdateRequestStatus(status, id, DateRequest.Text);

            if (!result)
            {
                //Show error
                MessageBox.Show("Databasen returnerede en fejl. Forespørgslens status blev ikke rettet. Prøv igen.", "Ret Forespørgselsstatus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Show confirmation
                MessageBox.Show("Forespørgslens status blev rettet.", "Ret Forespørgselsstatus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion

    }
}
