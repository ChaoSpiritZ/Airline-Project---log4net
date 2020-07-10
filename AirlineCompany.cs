using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    [MyTableName("AirlineCompanies")] //template design pattern
    public class AirlineCompany : IPoco, IUser
    {
        public long ID { get; set; }
        public string AirlineName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long CountryCode { get; set; }

        public AirlineCompany()
        {
        }

        public AirlineCompany(long iD, string airlineName, string userName, string password, long countryCode)
        {
            ID = iD;
            AirlineName = airlineName;
            UserName = userName;
            Password = password;
            CountryCode = countryCode;
        }

        public static bool operator ==(AirlineCompany item1, AirlineCompany item2)
        {
            if (ReferenceEquals(item1, null) && ReferenceEquals(item2, null))
                return true;
            if (ReferenceEquals(item1, null) || ReferenceEquals(item2, null))
                return false;
            return item1.ID == item2.ID;
        }

        public static bool operator !=(AirlineCompany item1, AirlineCompany item2)
        {
            return !(item1 == item2);
        }

        public override bool Equals(object obj)
        {
            AirlineCompany other = (AirlineCompany)obj;
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }

        public override string ToString()
        {
            //return $"Airline Company --- ID: {ID}, Airline Name: {AirlineName}, UserName: {UserName}, Password: {Password}, Country Code: {CountryCode}";
            //return JsonConvert.SerializeObject(this);

            return this.ToStringJson();
        }
    }
}
