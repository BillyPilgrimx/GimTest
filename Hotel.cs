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
            char[] seperatorArray = { ',' };
            int maxTokensReturned = 32;
            string[] tokens = line.Split(seperatorArray, maxTokensReturned);
            int i = 0;

            RowId = tokens[i++].Replace("'", "''").Replace(@"\", @"\\");
            SupplierId = tokens[i++].Replace("'", "''").Replace(@"\", @"\\");
            SupplierKey = tokens[i++].Replace("'", "''").Replace(@"\", @"\\");
            CountryCode = tokens[i++].Replace("'", "''").Replace(@"\", @"\\");
            State = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            CityCode = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            CityName = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            NormalizedCityName = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            DisplayName = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            Address = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            ZipCode = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            StarRating = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            ChainCode = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            Lat = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            Lng = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            RoomCount = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            Phone = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            Fax = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            Email = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            WebSite = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            CreateDate = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            IsActive = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            UpdateCycleId = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            RatingUrl = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            RatingCount = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            Rating = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            PropertyType = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            StatusChangeDate = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            ChangeScore = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            PropertyCategory = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            PropertySubCategory = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
            HotelInfoTranslation = tokens[i++].Replace("'", "''").Replace(@"\", @"\\"); ;
        }

        // Methods
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
