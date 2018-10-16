using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;

namespace JudBizz
{
    public class Bizz
    {
        #region Fields
        #region Ordinary Fields
        private static string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JortonSubEnt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string macAdresss = GetMacAddress();
        public User CurrentUser;
        public Address TempAddress;
        public Contact TempContact;
        public ContactInfo TempContactInfo;
        public Enterprise TempEnterprise;
        public IttLetter TempIttLetter;
        public IttLetterReceiver TempIttLetterReceiver;
        public IttLetterShipping TempIttLetterShipping;
        public LegalEntity TempLegalEntity;
        public Offer TempOffer;
        public Project TempProject;
        public Request TempRequest;
        public SubEntrepeneur TempSubEntrepeneur;
        public ZipTown TempZipTown;
        public bool UcRightActive = false;

        #endregion

        #region Entities used for methods
        public static Address CAD = new Address();
        public static Builder CBU = new Builder();
        public static Category CCT = new Category();
        public static ContactInfo CCI = new ContactInfo();
        public static Contact CCP = new Contact();
        public static CraftGroup CCG = new CraftGroup();
        public static Enterprise CEP = new Enterprise();
        public static EnterpriseForm CEF = new EnterpriseForm();
        public static IttLetter CIL = new IttLetter();
        public static IttLetterReceiver CIR = new IttLetterReceiver();
        public static IttLetterShipping CIS = new IttLetterShipping();
        public static JobDescription CJD = new JobDescription();
        public static LegalEntity CLE = new LegalEntity();
        public static Offer COF = new Offer();
        public static Project CPR = new Project();
        public static ProjectStatus CPS = new ProjectStatus();
        public static Region CRG = new Region();
        public static Request CRQ = new Request();
        public static RequestStatus CRS = new RequestStatus();
        public static SubEntrepeneur CSE;
        public static TenderForm CTF = new TenderForm();
        public static User CUS = new User();
        public static ZipTown CZT = new ZipTown();
        #endregion

        #region Lists
        public List<Address> Addresses = CAD.GetAddresses();
        public List<Builder> Builders = CBU.GetBuilders();
        public List<Category> Categories = CCT.GetCategories();
        public List<Contact> Contacts = CCP.GetContacts();
        public List<ContactInfo> ContactInfoList = CCI.GetContactInfoList();
        public List<CraftGroup> CraftGroups = CCG.GetCraftGroups();
        public List<Enterprise> EnterpriseList = CEP.GetEnterpriseList();
        public List<EnterpriseForm> EnterpriseForms = CEF.GetEnterpriseForms();
        public List<Region> Geography = CRG.GetRegions();
        public List<IttLetter> IttLetters = CIL.GetIttLetters();
        public List<IttLetterReceiver> IttLetterReceivers = CIR.GetIttLetterReceivers();
        public List<IttLetterShipping> IttLetterShippingList = CIS.GetIttLetterShippingList();
        public List<JobDescription> JobDescriptions = CJD.GetJobDescriptions();
        public List<LegalEntity> LegalEntities = CLE.GetLegalEntities();
        public List<Offer> Offers = COF.GetOffers();
        public List<Project> Projects = CPR.GetProjects();
        public List<IndexableProject> ActiveProjects = new List<IndexableProject>();
        public List<IndexableProject> IndexableProjects = new List<IndexableProject>();
        public List<ProjectStatus> ProjectStatusList = CPS.GetProjectStatusList();
        public List<Region> Regions = CRG.GetRegions();
        public List<Request> Requests = CRQ.GetRequests();
        public List<RequestStatus> RequestStatusList = CRS.GetRequestStatusList();
        public List<SubEntrepeneur> SubEntrepeneurs;
        public List<TenderForm> TenderForms = CTF.GetTenderForms();
        public List<User> Users = CUS.GetUsers();
        public List<ZipTown> ZipCodeList = CZT.GetZipTownList();

        #endregion

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Bizz()
        {
            ActivateFields();
            ActiveProjects = GetListActiveProjects();
            IndexableProjects = GetListIndexableProjects();
        }

        #endregion

        #region Methods

        private void ActivateFields()
        {
            ActivateSubEntrepeneurs();
            ActivateOrdinaryFields();
        }

        /// <summary>
        /// Method, that loads data into Entities for Methods
        /// </summary>
        private void ActivateSubEntrepeneurs()
        {
            Bizz temp = this;
            CSE = new SubEntrepeneur(LegalEntities);
            SubEntrepeneurs = CSE.GetSubEntrepeneurs();
        }

        /// <summary>
        /// Method, that loads data into Fields
        /// </summary>
        private void ActivateOrdinaryFields()
        {
            Bizz temp = this;
            CurrentUser = new User();
            TempAddress = new Address();
            TempContact = new Contact();
            TempContactInfo = new ContactInfo();
            TempEnterprise = new Enterprise();
            TempIttLetter = new IttLetter();
            TempIttLetterReceiver = new IttLetterReceiver();
            TempIttLetterShipping = new IttLetterShipping();
            TempLegalEntity = new LegalEntity();
            TempOffer = new Offer();
            TempProject = new Project();
            TempRequest = new Request();
            TempSubEntrepeneur = new SubEntrepeneur(LegalEntities);
            TempZipTown = new ZipTown();
        }

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
                    userName.Text = user.Name;
                    menuItemChangePassWord.IsEnabled = true;
                    menuItemLogOut.IsEnabled = true;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method, that generate list of active projects
        /// </summary>
        /// <returns>List<IndexableEnterprise></returns>
        public List<IndexableProject> GetListActiveProjects()
        {
            List<IndexableProject> result = new List<IndexableProject>();
            int i = 0;
            foreach (Project tempProject in Projects)
            {
                if (tempProject.Status == 1)
                {
                    IndexableProject temp = new IndexableProject(i, tempProject);
                    result.Add(temp);
                    i++;
                }
            }
            return result;
        }

        /// <summary>
        /// Method, that generate list of active projects
        /// </summary>
        /// <returns>List<IndexableEnterprise></returns>
        public List<IndexableProject> GetListIndexableProjects()
        {
            List<IndexableProject> result = new List<IndexableProject>();
            int i = 0;
            foreach (Project tempProject in Projects)
            {
                    IndexableProject temp = new IndexableProject(i, tempProject);
                    result.Add(temp);
                    i++;
            }
            return result;
        }

        /// <summary>
        /// Method, that reads MAC-address
        /// </summary>
        /// <returns></returns>
        private static string GetMacAddress()
        {
            String result = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();
            return result;
        }

        #endregion

        #region Properties
        public static string StrConnection { get => strConnection; }

        public static string MacAdresss { get => macAdresss; }

        #endregion
    }
}
