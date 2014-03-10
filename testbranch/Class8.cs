using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ConsoleApplication1
{
    [System.Xml.Serialization.XmlRoot("Review", Namespace = "", IsNullable = false)]
    public class Review 
    {
        [System.Xml.Serialization.XmlElement("Id")]
        public int Id {get; set;}

        [System.Xml.Serialization.XmlElement("Title")]
        public string Title {get; set;}

        [System.Xml.Serialization.XmlElement("Summary")]
        public string Summary {get; set;}

        [System.Xml.Serialization.XmlElement("Body")]
        public string Body {get; set;}

        [System.Xml.Serialization.XmlElement("GenreId")]
        public int GenreId {get; set;}
    }

    [System.Xml.Serialization.XmlRoot("DocumentElement", Namespace = "", IsNullable = false)]
    public class Reviews
    {
        [System.Xml.Serialization.XmlElement("Review")]
        public List<Review> ReviewList = new List<Review>(); 
    }
   
    public class XMLWrite
    {
        static string connectionString = "Data Source=JEONGHEE\\MSSQLSERVER01;Initial Catalog=PlanetWrox;Integrated Security=True";

        static void Main(string[] args)
        {
            string[] filename = { @"c:\temp\test.xml", @"c:\temp\test_Serialize.xml"};
            string[] option_serialize = {"WriteXML", "XMLSerializer"};

            // serialize using WriteXML Method
            serialize(filename[0], option_serialize[0]);
            deserialize(filename[0], option_serialize[0]);

            // serialize using XMLserializer
            serialize(filename[1], option_serialize[1]);
            deserialize(filename[1], option_serialize[1]);
           
            Console.ReadLine();
        }

        public static void deserialize(string filename, string option_serialize)
        {
            XmlSerializer serializer;

            switch (option_serialize)
            {
                case "WriteXML" :
                    Console.WriteLine("Deserialize using WriteXML() file....");
                    serializer = new XmlSerializer(typeof(Reviews), new XmlRootAttribute("DocumentElement"));
                    break;
                case "XMLSerializer":
                default :
                    Console.WriteLine("Deserialize using XMLSerializer file....");
                    serializer = new XmlSerializer(typeof(Reviews), new XmlRootAttribute("DataTable"));
                    break;
            }

            FileStream fs = new FileStream(filename, System.IO.FileMode.Open);
            Reviews xmlData = (Reviews)serializer.Deserialize(fs);
            fs.Close();

            insertDB(xmlData);

        }

        public static void serialize(string filename, string option_serialize)
        {
            DataTable dataTable = getDataTable();

            switch (option_serialize)
            {
                case "WriteXML":
                    Console.WriteLine("Serialize using WriteXML()....");
                    dataTable.WriteXml(filename);
                    break;
                case "XMLSerializer":
                default:
                    Console.WriteLine("Serialize using XMLSerializer....");
                    XmlSerializer serializer = new XmlSerializer(dataTable.GetType(), "");
                    StreamWriter file = new StreamWriter(filename);
                    serializer.Serialize(file, dataTable);
                    file.Close();
                    break;
            }
        }

        public static DataTable getDataTable()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select Id, Title, Summary, Body, GenreId from review", sqlConn);
            DataTable dataTable = new DataTable("Review", "");
            adapter.Fill(dataTable);
            sqlConn.Close();

            return dataTable;
        }

        public static void insertDB(Reviews xmlData)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "insert into TestReview (Id, Title, Summary, Body, GenreId) values (@Id, @Title, @Summary, @Body, @GenreId)";
            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters.Add("@Title", SqlDbType.NVarChar);
            command.Parameters.Add("@Summary", SqlDbType.NVarChar);
            command.Parameters.Add("@Body", SqlDbType.NVarChar);
            command.Parameters.Add("@GenreId", SqlDbType.Int);
            command.Connection = sqlConn;

            for (int i = 0; i < xmlData.ReviewList.Count(); i++)
            {
                command.Parameters["@Id"].Value = xmlData.ReviewList[i].Id;
                command.Parameters["@Title"].Value = xmlData.ReviewList[i].Title;
                command.Parameters["@Summary"].Value = xmlData.ReviewList[i].Summary;
                command.Parameters["@Body"].Value = xmlData.ReviewList[i].Body;
                command.Parameters["@GenreId"].Value = xmlData.ReviewList[i].GenreId;
                command.ExecuteNonQuery();
                
                Console.WriteLine(i.ToString() + " row(s) inserted");
            }

            sqlConn.Close();
        }

    }


}
