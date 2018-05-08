using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static CraftGroup CCG = new CraftGroup(strConnection);
        public static Contact CCP = new Contact(strConnection);
        public static LegalEntity CLG = new LegalEntity(strConnection);
        public static User CUS = new User(strConnection);
        public static ZipTown CZT = new ZipTown(strConnection);

        public List<User> Users = CUS.GetUserList();
        public List<CraftGroup> Groups = CCG.GetCraftGroupList();
        public List<LegalEntity> legalEntities = CLG.GetLegalEntityList();
        public List<Contact> contactPersons = CCP.GetContactPersonList();
        public List<Address> addresses = CAD.GetAddressList();
        public List<ZipTown> ZipCodeList = CZT.GetZipTownList();

        #endregion

        #region Constructors
        public bool CheckCredentials(string initials, string passWord)
        {
            foreach (User user in Users)
            {
                if (user.Initials == initials && user.PassWord == passWord)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

    }
}
