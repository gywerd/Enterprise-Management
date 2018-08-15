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
        #region Ordinary Fields
        public static string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JortonSubEnt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public User CurrentUser = new User();
        public Name tempName = new Name();
        public Address tempAddress = new Address();
        public ZipTown tempZipTown = new ZipTown();
        public bool UcRightActive = false;
        #endregion

        #region Entities used for methods
        public static Address CAD = new Address(strConnection);
        public static Category CCT = new Category(strConnection);
        public static Contact CCP = new Contact(strConnection);
        public static CraftGroup CCG = new CraftGroup(strConnection);
        public static Enterprise CEP = new Enterprise(strConnection);
        public static EnterpriseForm CEF = new EnterpriseForm(strConnection);
        public static IttLetter CIL = new IttLetter(strConnection);
        public static JobDescription CJD = new JobDescription(strConnection);
        public static LegalEntity CLE = new LegalEntity(strConnection);
        public static Name CNA = new Name(strConnection);
        public static Project CPR = new Project(strConnection);
        public static ProjectStatus CPS = new ProjectStatus(strConnection);
        public static Region CRG = new Region(strConnection);
        public static Request CRQ = new Request(strConnection);
        public static SubEntrepeneur CSE = new SubEntrepeneur(strConnection);
        public static User CUS = new User(strConnection);
        public static ZipTown CZT = new ZipTown(strConnection);
        #endregion

        #region Lists
        public List<Address> Addresses = CAD.GetAddressList();
        public List<Category> Categories = CCT.GetCategoryList();
        public List<Contact> Contacts = CCP.GetContactList();
        public List<CraftGroup> CraftGroups = CCG.GetCraftGroupList();
        public List<Enterprise> EnterpriseList = CEP.GetEnterpriseList();
        public List<EnterpriseForm> EnterpriseForms = CEF.GetEnterpriseFormList();
        public List<Region> Geography = CRG.GetGeography();
        public List<IttLetter> IttLetters = CIL.GetIttLetters();
        public List<JobDescription> JobDescriptions = CJD.GetJobDescriptions();
        public List<LegalEntity> LegalEntities = CLE.GetLegalEntities();
        public List<Name> Names = CNA.GetNameList();
        public List<Project> Projects = CPR.GetProjectList();
        public List<ProjectStatus> ProjectStatusList = CPS.GetProjectStatusList();
        public List<Request> Requests = CRQ.GetRequestList();
        public List<SubEntrepeneur> SubEntrepeneurs = CSE.GetSubEntrepeneurList();
        public List<User> Users = CUS.GetUserList();
        public List<ZipTown> ZipCodeList = CZT.GetZipTownList();
        #endregion

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Bizz() { }
        #endregion

        #region Methods
        /// <summary>
        /// Method, that checks credentials
        /// </summary>
        /// <param name="bizz">Bizz</param>
        /// <param name="userName">TextBlock</param>
        /// <param name="menuItemChangePassWord">RibbonApplicationMenuItem</param>
        /// <param name="menuItemLogOut">RibbonApplicationMenuItem</param>
        /// <param name="initials">string</param>
        /// <param name="passWord">string</param>
        /// <returns>bool</returns>
        public bool CheckCredentials(Bizz bizz, TextBlock userName, RibbonApplicationMenuItem menuItemChangePassWord, RibbonApplicationMenuItem menuItemLogOut, string initials, string passWord)
        {
            foreach (User user in Users)
            {
                if (user.Initials == initials && user.PassWord == passWord)
                {
                    bizz.CurrentUser = user;
                    userName.Text = GetUserName(user.Name);
                    menuItemChangePassWord.IsEnabled = true;
                    menuItemLogOut.IsEnabled = true;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method, that retrieves Username
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetUserName(int? name)
        {
            foreach (Name tempName in Names)
            {
                if (tempName.NameId == name)
                {
                    return tempName.ToString();
                }
            }
            return "";
        }

        #endregion

    }
}
