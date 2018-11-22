using JudDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudRepository
{
    public class Address
    {
        #region Fields
        private int id;
        private string street;
        private string place;
        private string zip;

        private static string strConnection;
        private Executor executor;
        public static ZipTown CZT;

        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Address(string strCon)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            this.id = 0;
            this.street = "";
            this.place = "";
            this.zip = "";
        }

        /// <summary>
        /// Constructor to add a new address
        /// </summary>
        /// <param name="street">string</param>
        /// <param name="zip">int</param>
        /// <param name="place">string</param>
        public Address(string strCon, string street, string zip, string place = "")
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            CZT = new ZipTown(strConnection);

            this.id = 0;
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
        /// <param name="place">string</param>
        public Address(string strCon, int id, string street, string place, string zip)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            CZT = new ZipTown(strConnection);

            this.id = id;
            this.street = street;
            this.place = place;
            this.zip = zip;
        }

        /// <summary>
        /// Constructor that accepts an existing Address
        /// </summary>
        /// <param name="bizz">Bizz</param>
        /// <param name="address">Address</param>
        /// <param name="street">string</param>
        /// <param name="zip">int</param>
        /// <param name="place">string</param>
        public Address(string strCon, Address address)
        {
            strConnection = strCon;
            executor = new Executor(strConnection);

            CZT = new ZipTown(strConnection);

            if (address != null)
            {
                this.id = address.Id;
                this.street = address.Street;
                this.place = address.Place;
                this.zip = address.Zip;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method, that fetches a list of Adresse from Db
        /// </summary>
        /// <returns>List<Address></returns>
        public List<Address> GetAddresses(string strCon)
        {
            List<string> results = executor.ReadListFromDataBase("Addresses");
            List<Address> addresses = new List<Address>();
            foreach (string result in results)
            {
                string[] resultArray = new string[4];
                resultArray = result.Split(';');
                Address address = new Address(strConnection, Convert.ToInt32(resultArray[0]), resultArray[1], resultArray[2], resultArray[3]);
                addresses.Add(address);
            }
            return addresses;
        }

        /// <summary>
        /// Method, that finds ZipTown from a Zip
        /// </summary>
        /// <param name="zip">string</param>
        /// <returns>ZipTown</returns>
        public ZipTown GetZipTown(string zip)
        {
            ZipTown result = new ZipTown(strConnection);
            List<ZipTown> zips = CZT.GetZipTownList();
            foreach (ZipTown zip2 in zips)
            {
                if (zip2.Zip.Equals(zip))
                {
                    result = zip2;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Returns main content as a string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            ZipTown zipTown = GetZipTown(zip);
            string tempAddress;
            tempAddress = street;
            if (place != "" && place != null)
            {
                tempAddress += "\n" + place;
            }
            tempAddress += "\n" + zip + " " + zipTown.Town.ToString();
            return tempAddress;
        }

        #endregion

        #region Properties
        public int Id { get => id; }

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

        public string Zip
        {
            get => zip;
            set
            {
                try
                {
                    if (value != null)
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
