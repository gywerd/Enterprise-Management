using JudRepository;
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
            public User CurrentUser = new User(strConnection);
            public Address TempAddress = new Address(strConnection);
            public Contact TempContact = new Contact(strConnection);
            public ContactInfo TempContactInfo = new ContactInfo(strConnection);
            public Enterprise TempEnterprise = new Enterprise(strConnection);
            public IttLetter TempIttLetter = new IttLetter(strConnection);
            public IttLetterBullet TempIttLetterBullet = new IttLetterBullet(strConnection);
            public IttLetterParagraph TempIttLetterParagraph = new IttLetterParagraph(strConnection);
            public IttLetterReceiver TempIttLetterReceiver = new IttLetterReceiver(strConnection);
            public IttLetterShipping TempIttLetterShipping = new IttLetterShipping(strConnection);
            public LegalEntity TempLegalEntity = new LegalEntity(strConnection);
            public Offer TempOffer = new Offer(strConnection);
            public Project TempProject = new Project(strConnection);
            public Request TempRequest = new Request(strConnection);
            public SubEntrepeneur TempSubEntrepeneur;
            public ZipTown TempZipTown = new ZipTown(strConnection);
            public bool UcRightActive = false;

            #endregion

            #region Entities used for methods
            public static Address CAD = new Address(strConnection);
            public static Builder CBU = new Builder(strConnection);
            public static Category CCT = new Category(strConnection);
            public static ContactInfo CCI = new ContactInfo(strConnection);
            public static Contact CCP = new Contact(strConnection);
            public static CraftGroup CCG = new CraftGroup(strConnection);
            public static Enterprise CEP = new Enterprise(strConnection);
            public static EnterpriseForm CEF = new EnterpriseForm(strConnection);
            public static IttLetterBullet CIB = new IttLetterBullet(strConnection);
            public static IttLetter CIL = new IttLetter(strConnection);
            public static IttLetterParagraph CIP = new IttLetterParagraph(strConnection);
            public static IttLetterReceiver CIR = new IttLetterReceiver(strConnection);
            public static IttLetterShipping CIS = new IttLetterShipping(strConnection);
            public static JobDescription CJD = new JobDescription(strConnection);
            public static LegalEntity CLE = new LegalEntity(strConnection);
            public static Offer COF = new Offer(strConnection);
            public static Project CPR = new Project(strConnection);
            public static ProjectStatus CPS = new ProjectStatus(strConnection);
            public static Region CRG = new Region(strConnection);
            public static Request CRQ = new Request(strConnection);
            public static RequestStatus CRS = new RequestStatus(strConnection);
            public static SubEntrepeneur CSE;
            public static TenderForm CTF = new TenderForm(strConnection);
            public static User CUS = new User(strConnection);
            public static ZipTown CZT = new ZipTown(strConnection);
            #endregion

            #region Lists
            public List<Address> Addresses = CAD.GetAddresses(strConnection);
            public List<Builder> Builders = CBU.GetBuilders();
            public List<Category> Categories = CCT.GetCategories();
            public List<Contact> Contacts = CCP.GetContacts();
            public List<ContactInfo> ContactInfoList = CCI.GetContactInfoList();
            public List<CraftGroup> CraftGroups = CCG.GetCraftGroups();
            public List<Enterprise> EnterpriseList = CEP.GetEnterpriseList();
            public List<EnterpriseForm> EnterpriseForms = CEF.GetEnterpriseForms();
            public List<Region> Geography = CRG.GetRegions();
            public List<IttLetter> IttLetters = CIL.GetIttLetters();
            public List<IttLetterBullet> IttLetterBulletList = CIB.GetIttLetterBulletList();
            public List<IttLetterParagraph> IttLetterParagraphList = CIP.GetIttLetterParagraphList(); 
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
            ActivateSubEntrepeneurs();
            ActiveProjects = GetListActiveProjects();
            IndexableProjects = GetListIndexableProjects();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method, that loads data into list of SubEntrepeneurs
        /// </summary>
        private void ActivateSubEntrepeneurs()
        {
            CSE = new SubEntrepeneur(strConnection, LegalEntities);
            SubEntrepeneurs = CSE.GetSubEntrepeneurs();
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
                    IndexableProject temp = new IndexableProject(strConnection, i, tempProject);
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
                    IndexableProject temp = new IndexableProject(strConnection,i, tempProject);
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

        public void UpdateIttLetterReceivers()
        {
            IttLetterReceivers.Clear();
            IttLetterReceivers = CIR.GetIttLetterReceivers();
        }

        public void UpdateIttLetterShippingList()
        {
            IttLetterShippingList.Clear();
            IttLetterShippingList = CIS.GetIttLetterShippingList();

        }

        #endregion

        #region Properties
        public static string StrConnection { get => strConnection; }

        public static string MacAdresss { get => macAdresss; }

        #endregion
    }
}
