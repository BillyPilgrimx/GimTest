using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace GimmonixTest
{
    class DBConnection
    {
        // Attributes
        private static DBConnection instance = null;
        private MySqlConnection connection = null;
        private string serverName = string.Empty;
        private string userName = string.Empty;
        private string portNumber = string.Empty;
        private string dbName = string.Empty;
        private string serverPassword = string.Empty;

        // CTORs
        private DBConnection()
        {
        }

        // Singleton
        public static DBConnection GetInstance(string _serverName, string _serverPassword, string _portNumber, string _userName)
        {
            if (DBConnection.instance == null)
            {
                DBConnection.instance = new DBConnection();
                DBConnection.instance.ServerName = _serverName;
                DBConnection.instance.ServerPassword = _serverPassword;
                DBConnection.instance.PortNumber = _portNumber;
                DBConnection.instance.UserName = _userName;
            }

            return instance;
        }

        // Properties
        public MySqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public string DBName
        {
            get { return this.dbName; }
            set { dbName = value; }
        }

        public string ServerName
        {
            get { return this.serverName; }
            set { serverName = value; }
        }

        public string UserName
        {
            get { return this.userName; }
            set { userName = value; }
        }

        public string ServerPassword
        {
            get { return this.serverPassword; }
            set { serverPassword = value; }
        }

        public string PortNumber
        {
            get { return this.portNumber; }
            set { portNumber = value; }
        }

        // Methods
        public bool ConnectToServer()
        {
            try
            {
                Console.Write("Connecting to '{0}' server... ", ServerName);
                string connectionString = string.Format("server={0}; password={1}; port={2}; UID={3}; SslMode=none", ServerName, ServerPassword, PortNumber, UserName);
                this.connection = new MySqlConnection(connectionString);
                connection.Open();

                Console.WriteLine("Connected!\n");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong during the server connection: ");
                Console.WriteLine(e.Message + "\n");
                return false;
            }
        }

        public bool IsConnectedToServer()
        {
            if (this.connection == null)
                return false;
            else
                return true;
        }

        public bool DisconnectFromServer()
        {
            try
            {
                Console.Write("Disconnecting from '{0}' server... ", ServerName);
                this.connection.Close();
                Console.WriteLine("Disconnected!\n");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong during the server disconnection: ");
                Console.WriteLine(e.Message + "\n");
                return false;
            }
        }

        public bool CreateAndUseDatabase(string _dbName)
        {
            try
            {
                Console.Write("Creating database '{0}'... ", _dbName);
                string createDatabaseCommand = string.Format("CREATE DATABASE IF NOT EXISTS {0};", _dbName);

                if (instance != null)
                {
                    MySqlCommand sqlCommand = this.connection.CreateCommand();
                    sqlCommand.CommandText = createDatabaseCommand;
                    sqlCommand.ExecuteNonQuery();
                    DBConnection.instance.DBName = _dbName;
                    Console.WriteLine("Database created!\n");

                    connection.Close();
                    Console.Write("Trying to use '{0}' database... ", _dbName);
                    string connectionString = string.Format("server={0}; password={1}; port={2}; UID={3}; database={4}; SslMode=none", ServerName, ServerPassword, PortNumber, UserName, _dbName);
                    this.connection = new MySqlConnection(connectionString);
                    connection.Open();

                    DBConnection.instance.DBName = _dbName;
                    Console.WriteLine("Database in use!\n");
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong during database creation: ");
                Console.WriteLine(e.Message + "\n");
                return false;
            }
        }

        public bool CreateTable(string _tableName)
        {
            try
            {
                Console.Write("Creating '{0}' table... ", _tableName);

                string createTableCommand = string.Format("CREATE TABLE IF NOT EXISTS {0} (RowId varchar(255) NULL, SupplierId varchar(255) NULL, SupplierKey varchar(255) NULL, CountryCode varchar(255) NULL, " +
                    "State varchar(255) NULL, CityCode varchar(255) NULL, CityName varchar(255) NULL, NormalizedCityName varchar(255) NULL, DisplayName varchar(255) NULL, Address varchar(255) NULL, ZipCode varchar(255) NULL, " +
                    "StarRating varchar(255) NULL, ChainCode varchar(255) NULL, Lat varchar(255) NULL, Lng varchar(255) NULL, RoomCount varchar(255) NULL, Phone varchar(255) NULL, Fax varchar(255) NULL, Email varchar(255) NULL, " +
                    "WebSite varchar(255) NULL, CreateDate varchar(255) NULL, IsActive varchar(255) NULL, UpdateCycleId varchar(255) NULL, RatingUrl varchar(255) NULL, RatingCount varchar(255) NULL, Rating varchar(255) NULL, " +
                    "PropertyType varchar(255) NULL, StatusChangeDate varchar(255) NULL, ChangeScore varchar(255) NULL, PropertyCategory varchar(255) NULL, PropertySubCategory varchar(255) NULL, HotelInfoTranslation varchar(255) NULL)", _tableName);

                MySqlCommand sqlCommand = this.connection.CreateCommand();
                sqlCommand.CommandText = createTableCommand;
                sqlCommand.ExecuteNonQuery();

                Console.WriteLine("Table created!\n");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong during database creation: ");
                Console.WriteLine(e.Message + "\n");
                return false;
            }
        }

        public bool InsertHotels(List<Hotel> hotelList, string _tableName)
        {
            try
            {
                MySqlDataReader dataReader;
                MySqlCommand sqlCommand;
                string query = string.Empty;
                int counter = 0;

                Console.Write("Inserting {0} hotels to '{1}' table...\n", hotelList.Count,_tableName);
                foreach (Hotel hotel in hotelList)
                {
                    query = string.Format("INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', '{4}', '{5}'," +
                        " '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}'," +
                        " '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', '{30}', '{31}', '{32}')", _tableName,
                        hotel.RowId, hotel.SupplierId, hotel.SupplierKey, hotel.CountryCode, hotel.State, hotel.CityCode,
                        hotel.CityName, hotel.NormalizedCityName, hotel.DisplayName, hotel.Address, hotel.ZipCode, hotel.StarRating,
                        hotel.ChainCode, hotel.Lat, hotel.Lng, hotel.RoomCount, hotel.Phone, hotel.Fax, hotel.Email, hotel.WebSite,
                        hotel.CreateDate, hotel.IsActive, hotel.UpdateCycleId, hotel.RatingUrl, hotel.RatingCount, hotel.Rating,
                        hotel.PropertyType, hotel.StatusChangeDate, hotel.ChangeScore, hotel.PropertyCategory, hotel.PropertySubCategory, hotel.HotelInfoTranslation);

                    sqlCommand = new MySqlCommand(query, this.Connection);
                    dataReader = sqlCommand.ExecuteReader();
                    dataReader.Close();

                    counter++;
                    Console.WriteLine("{0} hotels inserted out of {1}", counter, hotelList.Count);
                }
                Console.WriteLine("Successfully finished inserting {0} hotels to '{1}' table !\n", hotelList.Count, _tableName);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong during hotel insertion: ");
                Console.WriteLine(e.Message + "\n");
                return false;
            }
        }
    }
}

