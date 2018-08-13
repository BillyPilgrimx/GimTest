using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace GimmonixTest
{
    public class Program
    {
        public static void Main(string[] args)
        {

            DBConnection dbConn = DBConnection.GetInstance();
            dbConn.DBName = "gimmonixdb";
            dbConn.Password = "v614332K";
            string tableName = "gimmonix";

            dbConn.Connect(dbConn.DBName, dbConn.Password);
            if (dbConn.IsConnected())
            {
                //string query = "SELECT * FROM " + tableName;
                string query = "CHECK TABLE " + tableName;
                MySqlCommand sqlCommand = new MySqlCommand(query, dbConn.Connection);
                sqlCommand.ExecuteNonQuery();
                //MySqlDataReader reader = sqlCommand.ExecuteReader();
                //while(reader.Read())
                //{
                //    string column1 = reader.GetString(0);
                //    string column2 = reader.GetString(1);
                //    Console.WriteLine(column1 + " | " + column2);
                //}
            }

            string inputPath = @"resources\hotels.csv";
            string outputPath = @"resources\hotelsNoDuplications.csv";
            string headliner = string.Empty;
            StreamReader streamReader = new StreamReader(inputPath);
            StreamWriter streamWriter = new StreamWriter(outputPath);

            // reading the whole text and breaking it into lines + creating a list of Hotel objects (a Hotel for each line)
            string text = streamReader.ReadToEnd();
            string[] lines = text.Split('\n');
            List<Hotel> hotelsOriginal = new List<Hotel>();
            List<Hotel> hotelsSorted;
            List<Hotel> hotelsSortedAndRemoved;

            ReadLinesAndCreateHotels(lines, ref hotelsOriginal, ref headliner);

            Console.WriteLine("Number of hotels before MergeSort(): {0}\n", hotelsOriginal.Count);
            Console.WriteLine("Headliner: {0}\n", headliner);

            hotelsSorted = MergeSort(hotelsOriginal);
            hotelsSortedAndRemoved = RemoveDuplicates(hotelsSorted);

            Console.WriteLine("Number of hotels after MergeSort() and RemoveDuplicates(): {0}\n", hotelsSortedAndRemoved.Count);
            ReadHotelsAndCreateOutputFile(hotelsSortedAndRemoved, ref streamWriter, ref headliner);

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
                if (!inputList[i].RowId.Equals(inputList[i + 1].RowId))
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

        public static List<Hotel> RemoveDuplicationsLinear(ref List<Hotel> inputList)
        {
            List<Hotel> outputList = new List<Hotel>();
            bool flag = false;

            for (int i = 0; i < inputList.Count - 1; i++)
            {
                for (int j = inputList.Count - 1; i < j; j--)
                {
                    Console.WriteLine("Comparing elements {0} and {1}", i, j);
                    if (inputList.ElementAt(i).RowId.Equals(inputList.ElementAt(j).RowId))
                    {
                        Console.WriteLine("************* Match! *************");
                        throw new Exception("************************************************");
                    }
                }
            }

            return outputList;
        }
    }

    public class DBConnection
    {
        // Fields
        private static DBConnection instance = null;
        private MySqlConnection connection = null;
        private string dbName = string.Empty;
        private string dbPassword = string.Empty;

        // CTORs
        private DBConnection()
        {
        }

        // Methods
        public static DBConnection GetInstance() // singleton pattern
        {
            if (DBConnection.instance == null)
                DBConnection.instance = new DBConnection();

            return instance;
        }

        public MySqlConnection Connection
        {
            get { return connection; }
        }

        public string DBName
        {
            get { return this.dbName; }
            set { dbName = value; }
        }

        public string Password
        {
            get { return this.dbPassword; }
            set { dbPassword = value; }
        }

        public bool Connect(string _dbName, string _dbPassword)
        {
            try
            {
                Console.WriteLine("Connecting to {0}...", _dbName);
                string connectionString = string.Format("Server=localhost; database={0}; UID=root; password={1}; SslMode=none", _dbName, _dbPassword);
                this.connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong during connection opening...\n");
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public bool Disconnect(string _dbName)
        {
            try
            {
                Console.WriteLine("Disconnecting from {0}...", _dbName);
                this.connection.Close();
                Console.WriteLine("Disconnected!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong during connection closing...\n");
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool IsConnected()
        {
            if (this.connection == null)
                return false;
            else
                return true;
        }
    }
}

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

