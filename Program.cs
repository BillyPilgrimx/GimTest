using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimmonixTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string inputPath = @"resources\hotels.csv";
            string tmpPath = @"resources\hotelsTmp.csv";
            string outputPath = @"resources\hotelsNoDuplications.csv";
            string headliner = "";
            StreamReader streamReader = new StreamReader(inputPath);
            StreamWriter tmpStreamWriter = new StreamWriter(tmpPath);
            StreamWriter streamWriter = new StreamWriter(outputPath);

            // reading the whole text and breaking it into lines + creating a list of Hotel objects (a Hotel for each line)
            string text = streamReader.ReadToEnd();
            string[] lines = text.Split('\n');
            List<Hotel> hotelsOriginal = new List<Hotel>();
            List<Hotel> hotelsSorted;
            List<Hotel> hotelsSortedAndRemoved;

            // original
            ReadLinesAndCreateHotels(lines, ref hotelsOriginal, ref headliner);
            Console.WriteLine("Number of hotels before MergeSort(): {0}\n", hotelsOriginal.Count);
            Console.WriteLine("Headliner: {0}\n", headliner);

            hotelsSorted = MergeSort(hotelsOriginal);
            Console.WriteLine("Number of hotels after MergeSort(): {0}\n", hotelsSorted.Count);
            ReadHotelsAndCreateOutputFile(hotelsSorted, ref tmpStreamWriter, ref headliner);

            hotelsSortedAndRemoved = RemoveDuplicates(hotelsSorted);
            Console.WriteLine("Number of hotels after MergeSort() and RemoveDuplicates(): {0}\n", hotelsSortedAndRemoved.Count);
            ReadHotelsAndCreateOutputFile(hotelsSortedAndRemoved, ref streamWriter, ref headliner);

            /*
            foreach (Hotel hotel in hotelsSortedAndRemoved)
                Console.WriteLine(hotel.ToString());
            */

            Console.WriteLine("Done!\n");
        }

        public static void ReadLinesAndCreateHotels(string[] lines, ref List<Hotel> inputHotels, ref string headliner)
        {
            for (int i = 0; i < lines.Length - 1; i++)
            {
                if (i == 0)
                    headliner = lines[i];
                else
                    inputHotels.Add(new Hotel(lines[i]));
            }
        }

        public static void ReadHotelsAndCreateOutputFile(List<Hotel> inputHotels, ref StreamWriter streamWriter, ref string headliner)
        {
            for (int i = 0; i < inputHotels.Count; i++)
            {
                if (i == 0 && !string.IsNullOrEmpty(headliner))
                    streamWriter.Write(headliner);

                streamWriter.Write("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31}",
                inputHotels[i].RowId, inputHotels[i].SupplierId, inputHotels[i].SupplierKey, inputHotels[i].CountryCode, inputHotels[i].State, inputHotels[i].CityCode,
                inputHotels[i].CityName, inputHotels[i].NormalizedCityName, inputHotels[i].DisplayName, inputHotels[i].Address, inputHotels[i].ZipCode, inputHotels[i].StarRating,
                inputHotels[i].ChainCode, inputHotels[i].Lat, inputHotels[i].Lng, inputHotels[i].RoomCount, inputHotels[i].Phone, inputHotels[i].Fax, inputHotels[i].Email, inputHotels[i].WebSite,
                inputHotels[i].CreateDate, inputHotels[i].IsActive, inputHotels[i].UpdateCycleId, inputHotels[i].RatingUrl, inputHotels[i].RatingCount, inputHotels[i].Rating,
                inputHotels[i].PropertyType, inputHotels[i].StatusChangeDate, inputHotels[i].ChangeScore, inputHotels[i].PropertyCategory, inputHotels[i].PropertySubCategory, inputHotels[i].HotelInfoTranslation);

                streamWriter.Flush();
            }
        }

        public static List<Hotel> RemoveDuplicationsLinear(ref List<Hotel> inputList)
        {
            List<Hotel> outputList = new List<Hotel>();
            bool flag = false;

            for (int i = 0; i < inputList.Count - 1; i++)
            {
                for (int j = inputList.Count - 1; i < j; j--)
                {
                    Console.WriteLine("Comparing elements {0} and {1}", i, j);
                    if (inputList.ElementAt(i).RowId == inputList.ElementAt(j).RowId)
                    {
                        Console.WriteLine("************* Match! *************");
                        throw new Exception("************************************************");
                    }
                }
            }

            return outputList;
        }

        public static List<Hotel> MergeSort(List<Hotel> inputList)
        {
            // stop condition
            if (inputList.Count <= 1)
                return inputList;

            // spliting the inputList into to lists and executing MergeSort() on them
            List<Hotel> leftList = new List<Hotel>();
            List<Hotel> rightList = new List<Hotel>();
            int middle = inputList.Count / 2;

            for (int i = 0; i < middle; i++)
                leftList.Add(inputList.ElementAt(i));

            for (int i = middle; i < inputList.Count; i++)
                rightList.Add(inputList.ElementAt(i));

            // recursive invocation on the two side + merging
            leftList = MergeSort(leftList);
            rightList = MergeSort(rightList);
            return Merge(leftList, rightList);
        }

        public static List<Hotel> Merge(List<Hotel> leftInputList, List<Hotel> rightInputList)
        {
            List<Hotel> outputList = new List<Hotel>();
            while (0 < leftInputList.Count || 0 < rightInputList.Count)
            {
                // in case the two input lists have elements
                if (0 < leftInputList.Count && 0 < rightInputList.Count)
                {
                    if (int.Parse(leftInputList.First().RowId) <= int.Parse(rightInputList.First().RowId))
                    {
                        outputList.Add(leftInputList.First());
                        leftInputList.Remove(leftInputList.First());
                    }
                    else
                    {
                        outputList.Add(rightInputList.First());
                        rightInputList.Remove(rightInputList.First());
                    }
                }
                // incase the rightInputList empty and leftInputList is not
                else if (0 < leftInputList.Count)
                {
                    outputList.Add(leftInputList.First());
                    leftInputList.Remove(leftInputList.First());
                }
                // incase the leftInputList empty and is not rightInputList
                else if (0 < rightInputList.Count)
                {
                    outputList.Add(rightInputList.First());
                    rightInputList.Remove(rightInputList.First());
                }
            }
            return outputList;
        }

        public static List<Hotel> RemoveDuplicates(List<Hotel> inputList)
        {
            List<Hotel> outputList = new List<Hotel>();

            int i;
            for (i = 0; i < inputList.Count - 1; i++)
            {
                if (inputList[i].RowId != inputList[i + 1].RowId)
                    outputList.Add(inputList[i]);

                else
                    Console.WriteLine("Duplicate found with ID:" + inputList[i].RowId);

            }

            outputList.Add(inputList[i]);
            return outputList;
        }

        public static Hotel FindHotel(List<Hotel> inputList, int inspectedRowId)
        {
            foreach (Hotel hotel in inputList)
            {
                if (hotel.RowId.Equals(inspectedRowId))
                    return hotel;
            }
            return null;
        }
    }

    public class Hotel
    {
        // Fields
        public string RowId, SupplierId, SupplierKey, CountryCode, State, CityCode, CityName, NormalizedCityName, DisplayName, Address,
            ZipCode, StarRating, ChainCode, Lat, Lng, RoomCount, Phone, Fax, Email, WebSite, CreateDate, IsActive, UpdateCycleId,
            RatingUrl, RatingCount, Rating, PropertyType, StatusChangeDate, ChangeScore, PropertyCategory, PropertySubCategory, HotelInfoTranslation;

        /*
        public int RowId, SupplierId, CityCode, ZipCode, StarRating, RoomCount, UpdateCycleId, RatingCount, Rating, ChangeScore;
        public double Lat, Lng;
        public DateTime CreateDate, StatusChangeDate;
        public string SupplierKey, CountryCode, State, CityName, NormalizedCityName, DisplayName, Address, ChainCode, Phone, Fax, Email, WebSite,
            IsActive, RatingUrl, PropertyType, PropertyCategory, PropertySubCategory, HotelInfoTranslation;
            */

        // CTORs
        public Hotel(string line)
        {
            char[] seperatorArr = { ',' };
            int maxTokensReturned = 32;
            string[] tokens = line.Split(seperatorArr, maxTokensReturned);
            int i = 0;

            RowId = tokens[i++]; SupplierId = tokens[i++]; SupplierKey = tokens[i++]; CountryCode = tokens[i++]; State = tokens[i++];
            CityCode = tokens[i++]; CityName = tokens[i++]; NormalizedCityName = tokens[i++]; DisplayName = tokens[i++]; Address = tokens[i++];
            ZipCode = tokens[i++]; StarRating = tokens[i++]; ChainCode = tokens[i++]; Lat = tokens[i++]; Lng = tokens[i++]; RoomCount = tokens[i++];
            Phone = tokens[i++]; Fax = tokens[i++]; Email = tokens[i++]; WebSite = tokens[i++]; CreateDate = tokens[i++]; IsActive = tokens[i++];
            UpdateCycleId = tokens[i++]; RatingUrl = tokens[i++]; RatingCount = tokens[i++]; Rating = tokens[i++]; PropertyType = tokens[i++];
            StatusChangeDate = tokens[i++]; ChangeScore = tokens[i++]; PropertyCategory = tokens[i++]; PropertySubCategory = tokens[i++]; HotelInfoTranslation = tokens[i++];

            /*
            string[] tokens = line.Split(',');
            int i = 0;

            int.TryParse(tokens[i++], out RowId);
            int.TryParse(tokens[i++], out SupplierId);
            SupplierKey = tokens[i++];
            CountryCode = tokens[i++];
            State = tokens[i++];
            int.TryParse(tokens[i++], out CityCode);
            CityName = tokens[i++];
            NormalizedCityName = tokens[i++];
            DisplayName = tokens[i++];
            Address = tokens[i++];
            int.TryParse(tokens[i++], out ZipCode);
            int.TryParse(tokens[i++], out StarRating);
            ChainCode = tokens[i++];
            double.TryParse(tokens[i++], out Lat);
            double.TryParse(tokens[i++], out Lng);
            int.TryParse(tokens[i++], out RoomCount);
            Phone = tokens[i++];
            Fax = tokens[i++];
            Email = tokens[i++];
            WebSite = tokens[i++];
            DateTime.TryParse(tokens[i++], out CreateDate);
            IsActive = tokens[i++];
            int.TryParse(tokens[i++], out UpdateCycleId);
            RatingUrl = tokens[i++];
            int.TryParse(tokens[i++], out RatingCount);
            int.TryParse(tokens[i++], out Rating);
            PropertyType = tokens[i++];
            DateTime.TryParse(tokens[i++], out StatusChangeDate);
            int.TryParse(tokens[i++], out ChangeScore);
            PropertyCategory = tokens[i++];
            PropertySubCategory = tokens[i++];
            HotelInfoTranslation = tokens[i++];
            */
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
