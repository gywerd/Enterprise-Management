using BicDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicBizz
{
    public class Address
    {
        #region Fields
        private static string strConnection;
        private int addressId;
        private string street;
        private string place;
        private int zip;

        private Executor executor;

        public static ZipTown CZT = new ZipTown(strConnection);

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Address() { }

        public Address(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);
        }

        /// <summary>
        /// Constructor to add a new address
        /// </summary>
        /// <param name="street">string</param>
        /// <param name="zip">int</param>
        /// <param name="town">string</param>
        /// <param name="place">string</param>
        public Address(string street, int zip, string place = null)
        {
            this.street = street;
            this.place = place;
            this.zip = zip;
        }

        /// <summary>
        /// Constructor to add address from Db to list
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="street">string</param>
        /// <param name="zip">int</param>
        /// <param name="town">string</param>
        /// <param name="place">string</param>
        public Address(int id, string street, string place, int zip)
        {
            this.addressId = id;
            this.street = street;
            this.place = place;
            this.zip = zip;
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            ZipTown zipTown = GetZipTown(zip);
            string tempAddress;
            tempAddress = street + "\n" + place + "\n" + zipTown.ToString();
            return tempAddress;
        }

        public ZipTown GetZipTown(int id)
        {
            ZipTown result = new ZipTown();
            List<ZipTown> zips = CZT.GetZipTownList();
            foreach (ZipTown zip in zips)
            {
                if (zip.Id == id)
                {
                    return zip;
                }
            }
            return result;
        }

        public List<Address> GetAddressList()
        {
            List<string> results = executor.ReadListFromDataBase("Addresses");
            List<Address> addresses = new List<Address>();
            foreach (string result in results)
            {
                string[] resultArray = new string[4];
                resultArray = result.Split(';');
                Address address = new Address(Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2], Convert.ToInt32(resultArray[3]));
                addresses.Add(address);
            }
            return addresses;
        }

        #endregion


        #region Properties
        public int AddressId { get => addressId; }
        public string Street
        {
            get => street;
            set
            {
                try
                {
                    if (value != null)
                    {
                        street = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public string Place
        {
            get => place;
            set
            {
                try
                {
                    if (value != null)
                    {
                        place = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public int Zip
        {
            get => zip;
            set
            {
                try
                {
                    if (value < 0)
                    {
                        zip = value;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        #endregion
    }
}
