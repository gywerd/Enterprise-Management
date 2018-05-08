using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;

namespace BicBizz
{
    public class Bizz
    {
        #region Fields
        public static string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JortonSubEnt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public User CurrentUser = new User();
        public Name tempName = new Name();
        public Address tempAddress = new Address();
        public ZipTown tempZipTown = new ZipTown();

        public static Address CAD = new Address(strConnection);
        public static Name CNA = new Name(strConnection);
        public static CraftGroup CCG = new CraftGroup(strConnection);
        public static Contact CCP = new Contact(strConnection);
        public static LegalEntity CLG = new LegalEntity(strConnection);
        public static User CUS = new User(strConnection);
        public static ZipTown CZT = new ZipTown(strConnection);

        public List<User> Users = CUS.GetUserList();
        public List<Name> Names = CNA.GetNameList();
        public List<CraftGroup> Groups = CCG.GetCraftGroupList();
        public List<LegalEntity> legalEntities = CLG.GetLegalEntityList();
        public List<Contact> contactPersons = CCP.GetContactPersonList();
        public List<Address> addresses = CAD.GetAddressList();
        public List<ZipTown> ZipCodeList = CZT.GetZipTownList();

        #endregion

        #region Constructors
        public Bizz() { }
        #endregion

        #region Methods
        public bool CheckCredentials(Bizz bizz, TextBlock userName, RibbonApplicationMenuItem menuItemChangePassWord, RibbonApplicationMenuItem menuItemLogOut, string initials, string passWord)
        {
            foreach (User user in Users)
            {
                if (user.Initials == initials && user.PassWord == passWord)
                {
                    this.CurrentUser = user;
                    userName.Text = GetUserName(user.Name);
                    menuItemChangePassWord.IsEnabled = true;
                    menuItemLogOut.IsEnabled = true;
                    return true;
                }
            }

            return false;
        }

        private string GetUserName(int id)
        {
            string result = "";
            foreach (Name name in Names)
            {
                if (name.NameId == id)
                {
                    result = name.ToString();
                }
            }
            return result;
        }

        #endregion

    }
}
