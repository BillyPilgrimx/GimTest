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
            string serverName = "localhost";
            string portNumber = "3306";
            string serverPassword = "v614332K";
            string userName = "root";
            string dbName = "gimmonixdb";
            string tableName = "data";

            string inputPath = @"resources\hotels.csv";
            string outputPath1 = @"resources\hotelsOutput1.csv";
            string outputPath2 = @"resources\hotelsOutput2.csv";
            string headliner = string.Empty;

            StreamReader streamReader = new StreamReader(inputPath);
            StreamWriter streamWriter1 = new StreamWriter(outputPath1);
            StreamWriter streamWriter2 = new StreamWriter(outputPath2);

            Console.WriteLine("Hello there!\n");
            Console.Write("Loading: {0} input file... ", inputPath);
            string text = streamReader.ReadToEnd();
            Console.WriteLine("File loaded!\n");

            // reading the whole text and breaking it into lines + creating a list of Hotel objects (a Hotel for each line)
            string[] lines = text.Split('\n');
            List<Hotel> hotelsOriginal = new List<Hotel>();
            List<Hotel> hotelsOutput = new List<Hotel>();
            List<int> parameterList = new List<int>();

            ReadLinesAndCreateHotels(lines, ref hotelsOriginal, ref headliner);
            hotelsOutput = Menu_SortAndSearch(hotelsOriginal);
            ReadHotelsListAndCreateOutputFile(hotelsOutput, ref streamWriter1, ref headliner);

            /*
            DBConnection dbConn = DBConnection.GetInstance(serverName, serverPassword, portNumber, userName);
            dbConn.ConnectToServer();
            dbConn.CreateAndUseDatabase(dbName);
            dbConn.CreateTable(tableName);
            dbConn.InsertHotels(hotelsOriginal, tableName);
            */

            Console.WriteLine("\nEnd of Program!\n");
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

        public static void ReadHotelsListAndCreateOutputFile(List<Hotel> inputHotels, ref StreamWriter streamWriter, ref string headliner)
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

        public static List<Hotel> Menu_SortAndSearch(List<Hotel> inputList)
        {
            Hotel tmpHotel = new Hotel();
            List<KeyValuePair<int, string>> pairs = new List<KeyValuePair<int, string>>();
            string choiceStr;
            char innerChoiceChar;
            int choiceNum;

            // Menu - flow control
            Console.WriteLine("Please enter a parameter to search with (key) and a target (value):\n");
            Console.WriteLine("By which parameter would you like to make the search?\n=====================================================");
            for (int i = 0; i < tmpHotel.GetType().GetProperties().Count(); i++)
            {
                if (i < 10)
                {
                    Console.Write("{0}. {1, -26}", i, tmpHotel.GetType().GetProperties().ElementAt(i).Name);
                    if ((i + 1) % 2 == 0 && i != 0)
                        Console.WriteLine();
                }
                else
                {
                    Console.Write("{0}. {1, -25}", i, tmpHotel.GetType().GetProperties().ElementAt(i).Name);
                    if ((i + 1) % 2 == 0 && i != 0)
                        Console.WriteLine();
                }
            }

            do
            {
                do
                {
                    do
                    {
                        Console.Write("\nYour choice? (0-31): ");
                        choiceStr = Console.ReadLine();

                        if (!int.TryParse(choiceStr, out choiceNum))
                            Console.WriteLine("Invalid input, please try again...");

                    } while (!int.TryParse(choiceStr, out choiceNum));

                    if (choiceNum < 0 || 32 < choiceNum)
                        Console.WriteLine("Invalid input, please try again...");

                } while (choiceNum < 0 || 32 < choiceNum);

                Console.Write("\nEnter the target (value) to search for: ");
                choiceStr = Console.ReadLine();
                pairs.Add(new KeyValuePair<int, string>(choiceNum, choiceStr));

                do
                {
                    Console.Write("Would you like to add another parameter to the search? (y/n): ");
                    innerChoiceChar = (char)Console.Read();
                    Console.ReadLine();

                    if (innerChoiceChar != 'y' && innerChoiceChar != 'n')
                        Console.WriteLine("Invalid input, please try again...");

                } while (innerChoiceChar != 'y' && innerChoiceChar != 'n');

            } while (innerChoiceChar != 'n');

            List<Hotel> outputList = new List<Hotel>(inputList);
            foreach(KeyValuePair<int, string> pair in pairs)
            {
                outputList = MergeSort(outputList, pair.Key);
                outputList = SearchElements(outputList, 0, outputList.Count(), pair.Value, pair.Key);
            }

            return outputList;
        }

        public static List<Hotel> MergeSort(List<Hotel> inputList, int propertyIndexNumberToSortBy)
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
            leftList = MergeSort(leftList, propertyIndexNumberToSortBy);
            rightList = MergeSort(rightList, propertyIndexNumberToSortBy);
            return Merge(leftList, rightList, propertyIndexNumberToSortBy);
        }

        public static List<Hotel> Merge(List<Hotel> leftInputList, List<Hotel> rightInputList, int propertyIndexNumberToSortBy)
        {
            List<Hotel> outputList = new List<Hotel>();
            while (0 < leftInputList.Count || 0 < rightInputList.Count)
            {
                // in case the two input lists have elements
                if (0 < leftInputList.Count && 0 < rightInputList.Count)
                {
                    DateTime leftDate, rightDate;
                    double leftNum, RightNum;

                    // if both can be parsed to DateTime
                    if (DateTime.TryParse(leftInputList.First().GetSomeProperty(propertyIndexNumberToSortBy), out leftDate) && DateTime.TryParse(rightInputList.First().GetSomeProperty(propertyIndexNumberToSortBy), out rightDate))
                    {
                        if (leftDate.CompareTo(rightDate) < 0)
                        {
                            {
                                outputList.Add(leftInputList.First());
                                leftInputList.Remove(leftInputList.First());
                            }
                        }
                        else
                        {
                            outputList.Add(rightInputList.First());
                            rightInputList.Remove(rightInputList.First());
                        }
                    }
                    // if both can be parsed to double
                    else if (double.TryParse(leftInputList.First().GetSomeProperty(propertyIndexNumberToSortBy), out leftNum) && double.TryParse(rightInputList.First().GetSomeProperty(propertyIndexNumberToSortBy), out RightNum))
                    {
                        if (leftNum < RightNum)
                        {
                            {
                                outputList.Add(leftInputList.First());
                                leftInputList.Remove(leftInputList.First());
                            }
                        }
                        else
                        {
                            outputList.Add(rightInputList.First());
                            rightInputList.Remove(rightInputList.First());
                        }
                    }
                    else // can't parse both
                    {
                        if (leftInputList.First().GetSomeProperty(propertyIndexNumberToSortBy).CompareTo(rightInputList.First().GetSomeProperty(propertyIndexNumberToSortBy)) < 0)
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

        public static List<Hotel> SearchElements(List<Hotel> inputList, int leftBoundry, int rightBoundry, string target, int propertyIndexNumberToSearchBy)
        {
            if (leftBoundry <= rightBoundry)
            {
                int middle = leftBoundry + ((rightBoundry - leftBoundry) / 2);

                // in case we found on of the targeted objects
                if (inputList.ElementAt(middle).GetSomeProperty(propertyIndexNumberToSearchBy).Equals(target))
                {
                    List<Hotel> outputList = new List<Hotel>();
                    int placeholder = middle;
                    while (inputList.ElementAt(placeholder).GetSomeProperty(propertyIndexNumberToSearchBy).Equals(target))
                    {
                        placeholder--;
                    }

                    int counter = 0;
                    placeholder++;
                    while (inputList.ElementAt(placeholder).GetSomeProperty(propertyIndexNumberToSearchBy).Equals(target))
                    {
                        outputList.Add(inputList.ElementAt(placeholder));
                        placeholder++;
                        counter++;
                    }
                    Console.WriteLine("{0} requested hotels have been added!\n", counter);
                    return outputList;
                }

                // in case the target located above
                if (inputList.ElementAt(middle).GetSomeProperty(propertyIndexNumberToSearchBy).CompareTo(target) < 0)
                {
                    return SearchElements(inputList, middle + 1, rightBoundry, target, propertyIndexNumberToSearchBy);
                }

                // in case the target located below
                return SearchElements(inputList, leftBoundry, middle - 1, target, propertyIndexNumberToSearchBy);
            }

            return null;
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
}


