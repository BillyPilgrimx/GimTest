using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimmonixTest
{
    public class Hotel
    {
        // Fields
        public string RowId { get; set; }
        public string SupplierId { get; set; }
        public string SupplierKey { get; set; }
        public string CountryCode { get; set; }
        public string State { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string NormalizedCityName { get; set; }
        public string DisplayName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string StarRating { get; set; }
        public string ChainCode { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string RoomCount { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string CreateDate { get; set; }
        public string IsActive { get; set; }
        public string UpdateCycleId { get; set; }
        public string RatingUrl { get; set; }
        public string RatingCount { get; set; }
        public string Rating { get; set; }
        public string PropertyType { get; set; }
        public string StatusChangeDate { get; set; }
        public string ChangeScore { get; set; }
        public string PropertyCategory { get; set; }
        public string PropertySubCategory { get; set; }
        public string HotelInfoTranslation { get; set; }

        // CTORs
        public Hotel()
        {
        }

        public Hotel(string line)
        {
            char[] seperatorArray1 = { ',' };
            int maxTokensReturned = 32;
            string[] tokens = line.Split(seperatorArray1, maxTokensReturned);
            int i = 0;

            for (int j = 0; j < tokens.Count(); j++)
            {
                char[] seperatorArray2 = { '"', '\'', '\\' };
                string[] innerTokens = tokens[j].Split(seperatorArray2);

                tokens[j] = string.Empty;
                for (int k = 0; k < innerTokens.Count(); k++)
                {
                    tokens[j] += @"" + innerTokens[k] + @"";
                }

            }

            RowId = tokens[i++].Trim();
            SupplierId = tokens[i++].Trim();
            SupplierKey = tokens[i++].Trim();
            CountryCode = tokens[i++].Trim();
            State = tokens[i++].Trim();
            CityCode = tokens[i++].Trim();
            CityName = tokens[i++].Trim();
            NormalizedCityName = tokens[i++].Trim();
            DisplayName = tokens[i++].Trim();
            Address = tokens[i++].Trim();
            ZipCode = tokens[i++].Trim();
            StarRating = tokens[i++].Trim();
            ChainCode = tokens[i++].Trim();
            Lat = tokens[i++].Trim();
            Lng = tokens[i++].Trim();
            RoomCount = tokens[i++].Trim();
            Phone = tokens[i++].Trim();
            Fax = tokens[i++].Trim();
            Email = tokens[i++].Trim();
            WebSite = tokens[i++].Trim();
            CreateDate = tokens[i++].Trim();
            IsActive = tokens[i++].Trim();
            UpdateCycleId = tokens[i++].Trim();
            RatingUrl = tokens[i++].Trim();
            RatingCount = tokens[i++].Trim();
            Rating = tokens[i++].Trim();
            PropertyType = tokens[i++].Trim();
            StatusChangeDate = tokens[i++].Trim();
            ChangeScore = tokens[i++].Trim();
            PropertyCategory = tokens[i++].Trim();
            PropertySubCategory = tokens[i++].Trim();
            HotelInfoTranslation = tokens[i++]; // no trim

            /*
            RowId = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            SupplierId = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            SupplierKey = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            CountryCode = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            State = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            CityCode = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            CityName = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            NormalizedCityName = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            DisplayName = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            Address = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            ZipCode = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            StarRating = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            ChainCode = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            Lat = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            Lng = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            RoomCount = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            Phone = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            Fax = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            Email = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            WebSite = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            CreateDate = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            IsActive = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            UpdateCycleId = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            RatingUrl = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            RatingCount = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            Rating = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            PropertyType = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            StatusChangeDate = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            ChangeScore = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            PropertyCategory = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            PropertySubCategory = tokens[i++].Trim().Replace("'", "''").Replace(@"\", @"\\");
            HotelInfoTranslation = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); // no trim here
            */
        }

        public string GetSomeProperty(int propertyNumber)
        {
            switch (propertyNumber)
            {
                case 0: return RowId;
                case 1: return SupplierId;
                case 2: return SupplierKey;
                case 3: return CountryCode;
                case 4: return State;
                case 5: return CityCode;
                case 6: return CityName;
                case 7: return NormalizedCityName;
                case 8: return DisplayName;
                case 9: return Address;
                case 10: return ZipCode;
                case 11: return StarRating;
                case 12: return ChainCode;
                case 13: return Lat;
                case 14: return Lng;
                case 15: return RoomCount;
                case 16: return Phone;
                case 17: return Fax;
                case 18: return Email;
                case 19: return WebSite;
                case 20: return CreateDate;
                case 21: return IsActive;
                case 22: return UpdateCycleId;
                case 23: return RatingUrl;
                case 24: return RatingCount;
                case 25: return Rating;
                case 26: return PropertyType;
                case 27: return StatusChangeDate;
                case 28: return ChangeScore;
                case 29: return PropertyCategory;
                case 30: return PropertySubCategory;
                case 31: return HotelInfoTranslation;
            }
            return null;
        }

        public override string ToString()
        {
            return "RowId = " + RowId + "\nSupplierId = " + SupplierId + "\nSupplierKey = " + SupplierKey + "\nCountryCode = " + CountryCode
                + "\nState = " + State + "\nCityCode = " + CityCode + "\nCityName = " + CityName + "\nNormalizedCityName = " + NormalizedCityName
                + "\nDisplayName = " + DisplayName + "\nAddress = " + Address + "\nZipCode = " + ZipCode + "\nStarRating = " + StarRating + "\nChainCode = " + ChainCode
                + "\nLat = " + Lat + "\nLng = " + Lng + "\nRoomCount = " + RoomCount + "\nPhone = " + Phone + "\nFax = " + Fax + "\nEmail = " + Email
                + "\nWebSite = " + WebSite + "\nCreateDate = " + CreateDate + "\nIsActice = " + IsActive + "\nUpdateCycleId = " + UpdateCycleId
                + "\nRatingUrl = " + RatingUrl + "\nRatingCount = " + RatingCount + "\nRating = " + Rating + "\nPropertyType = " + PropertyType
                + "\nStatusChangeDate = " + StatusChangeDate + "\nChangeScore = " + ChangeScore + "\nPropertyCategory = " + PropertyCategory
                + "\nPropertySubCategory = " + PropertySubCategory + "\nHotelInfoTranslation = " + HotelInfoTranslation + "\n";
        }
    }
}
