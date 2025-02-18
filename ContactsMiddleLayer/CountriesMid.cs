using ContactsDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsMiddleLayer
{
    public class clsCountry
    {
        public int ID { get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }
        enMode Mode { get; set; }

        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";
            this.Code = "";
            this.PhoneCode = "";

            Mode = enMode.AddNew;
        }

        private clsCountry(int ID, string CountryName, string Code, string PhoneCode)
        {
            this.ID = ID;
            this.CountryName = CountryName;
            this.Code = Code;
            this.PhoneCode = PhoneCode;

            Mode = enMode.Update;

        }

        private bool _AddNewCountry()
        {
            this.ID = clsCountryDataAccess.AddNewCountry(this.CountryName, this.Code, this.PhoneCode);

            return (ID != -1);
        }

        private bool _UpdateCountry()
        {
            if (clsCountryDataAccess.UpdateCountry(this.ID, this.CountryName, this.Code, this.PhoneCode))
            {
                return true;
            }
            return false;
        }

        public static DataTable GetALlCountries()
        {
            return clsCountryDataAccess.GetAllCountries();
        }

        public static bool DeleteCountry(int ID)
        {
            return clsCountryDataAccess.DeleteCountry(ID);
        }

        public static clsCountry Find(int ID)
        {
            string CountryName = "", Code = "", PhoneCode = "";

            if (clsCountryDataAccess.GetCountryInfoByID(ID, ref CountryName, ref Code, ref PhoneCode))
            {
                return new clsCountry(ID, CountryName, Code, PhoneCode);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry Find(string CountryName)
        {
            int ID = -1;
            string Code = "", PhoneCode = "";

            if (clsCountryDataAccess.GetCountryInfoByName(ref ID, CountryName, ref Code, ref PhoneCode))
            {
                return new clsCountry(ID, CountryName, Code, PhoneCode);
            }
            else
            {
                return null;
            }
        }

        public static bool IsCountryExist(int ID)
        {
            return clsCountryDataAccess.IsCountryExist(ID);
        }

        public static bool IsCountryExist(string CountryName)
        {
            return clsCountryDataAccess.IsCountryExist(CountryName);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    if (_UpdateCountry())
                    {
                        return true;
                    }
                    return false;
            }
            return false;
        }
    }
}
