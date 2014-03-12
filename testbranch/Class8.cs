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
            string[] rootname = { "DocumentElement", "DataTable"};


            // serialize   : DataTable.WriteXML()  
            // deserialize : System.Xml.Serialization.XmlSerializer.Deserialize()
            serializeUsingDataTable(filename[0]);
            deserializeUsingXmlDeserializer(filename[0], rootname[0]);


            // serialize   : System.Xml.Serialization.XmlSerializer.Deserialize()
            // deserialize : DataSet.ReadXml()
            serializeUsingXmlSerializer(filename[1]);
            deserializeUsingDataSet(filename[1]);

            // not working...
            deserializeUsingXmlDeserializer(filename[1], rootname[1]);

            Console.ReadLine();
        }

        public static void deserializeUsingDataSet(string filename)
        {
            Console.WriteLine("Deserialize using XMLSerializer file....");

            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable("Review");
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Summary", typeof(string));
            dataTable.Columns.Add("Body", typeof(string));
            dataTable.Columns.Add("GenreId", typeof(int));
            dataSet.Tables.Add(dataTable);

            FileStream fs = new FileStream(filename, System.IO.FileMode.Open);
            dataTable.BeginLoadData();
            dataSet.ReadXml(fs, XmlReadMode.IgnoreSchema);
            dataTable.EndLoadData();
            fs.Close();
           
            insertDB(dataSet.Tables["Review"]);

        }

        public static void deserializeUsingXmlDeserializer(string filename, string rootname)
        {
            Console.WriteLine("Deserialize....");

            XmlSerializer serializer = new XmlSerializer(typeof(Reviews), new XmlRootAttribute(rootname));
            FileStream fs = new FileStream(filename, System.IO.FileMode.Open);
            Reviews xmlData = (Reviews)serializer.Deserialize(fs);
            fs.Close();

            insertDB(xmlData);
        }

        public static void serializeUsingDataTable(string filename)
        {
            Console.WriteLine("Serialize using DataTable.WriteXML()...."); 

            DataTable dataTable = getDataTable();
            dataTable.WriteXml(filename);
        }

        public static void serializeUsingXmlSerializer(string filename)
        {
            Console.WriteLine("Serialize using XMLSerializer....");
            
            DataTable dataTable = getDataTable();
            XmlSerializer serializer = new XmlSerializer(dataTable.GetType(), "");
            StreamWriter file = new StreamWriter(filename);
            serializer.Serialize(file, dataTable);
            file.Close();
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

        public static void insertDB(DataTable dataTable)
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

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                command.Parameters["@Id"].Value = dataTable.Rows[i]["Id"];
                command.Parameters["@Title"].Value = dataTable.Rows[i]["Title"];
                command.Parameters["@Summary"].Value = dataTable.Rows[i]["Summary"];
                command.Parameters["@Body"].Value = dataTable.Rows[i]["Body"];
                command.Parameters["@GenreId"].Value = dataTable.Rows[i]["GenreId"];
                command.ExecuteNonQuery();

                Console.WriteLine(i.ToString() + " row(s) inserted");
            }

            sqlConn.Close();
        }
    }


}
