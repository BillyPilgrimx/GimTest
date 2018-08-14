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
        public Hotel(string line)
        {
            char[] seperatorArray = { ',' };
            int maxTokensReturned = 32;
            string[] tokens = line.Split(seperatorArray, maxTokensReturned);
            int i = 0;

            RowId = tokens[i++]; SupplierId = tokens[i++]; SupplierKey = tokens[i++]; CountryCode = tokens[i++]; State = tokens[i++];
            CityCode = tokens[i++]; CityName = tokens[i++]; NormalizedCityName = tokens[i++]; DisplayName = tokens[i++]; Address = tokens[i++];
            ZipCode = tokens[i++]; StarRating = tokens[i++]; ChainCode = tokens[i++]; Lat = tokens[i++]; Lng = tokens[i++]; RoomCount = tokens[i++];
            Phone = tokens[i++]; Fax = tokens[i++]; Email = tokens[i++]; WebSite = tokens[i++]; CreateDate = tokens[i++]; IsActive = tokens[i++];
            UpdateCycleId = tokens[i++]; RatingUrl = tokens[i++]; RatingCount = tokens[i++]; Rating = tokens[i++]; PropertyType = tokens[i++];
            StatusChangeDate = tokens[i++]; ChangeScore = tokens[i++]; PropertyCategory = tokens[i++]; PropertySubCategory = tokens[i++]; HotelInfoTranslation = tokens[i++];
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
