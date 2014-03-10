using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


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
           // WriteXMLToTextFile(); //Serialization
               
            string filename = @"c:\temp\test.xml";
            string filename_serializer = @"c:\temp\test_Serialize.xml";

            //serializeUsingWriteXML(filename);


            //serializeUsingXMLSerializer(filename_serializer);


           // deserializeUsingWriteXML(filename);

            deserializeUsingXMLDeserializer(filename_serializer);



            Console.ReadLine();
        }

        public static void deserializeUsingWriteXML(string filename)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Reviews), new System.Xml.Serialization.XmlRootAttribute("DocumentElement"));
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open);
            Reviews xmlData = (Reviews)serializer.Deserialize(fs);
            fs.Close();

            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConn;
            
            for(int i =0; i<xmlData.ReviewList.Count(); i++)
            {
                //Console.WriteLine(xmlData.ReviewList[i].Id.ToString() + "\t" + xmlData.ReviewList[i].Title);
                //command.CommandText = "insert into TestReview (Id, Title, Summary, Body, GenreId) values (@Id, @Title, @Summary, @Body, @GenreId)";

                //command.Parameters.Add("@Id", SqlDbType.Int, , xmlData.ReviewList[i].Id);
                //command.Parameters.Add("@Title", SqlDbType.VarChar, 50, xmlData.ReviewList[i].Title);
                //command.Parameters.Add("@Summary", SqlDbType.VarChar, 50, xmlData.ReviewList[i].Summary);
                //command.Parameters.Add("@Body", SqlDbType.VarChar, 50, xmlData.ReviewList[i].Body);
                //command.Parameters.Add("@GenreId", SqlDbType.Int, , xmlData.ReviewList[i].GenreId);


                //command.ExecuteNonQuery();
            }

            sqlConn.Close();
        }

        public static void deserializeUsingXMLDeserializer(string filename)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Reviews), new System.Xml.Serialization.XmlRootAttribute("DataTable"));
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open);
            Reviews xmlData = (Reviews)serializer.Deserialize(fs);
            fs.Close();

            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConn;
            
            for(int i =0; i<xmlData.ReviewList.Count(); i++)
            {
                //Console.WriteLine(xmlData.ReviewList[i].Id.ToString() + "\t" + xmlData.ReviewList[i].Title);
                //command.CommandText = "insert into TestReview (Id, Title, Summary, Body, GenreId) values (@Id, @Title, @Summary, @Body, @GenreId)";

                //command.Parameters.Add("@Id", SqlDbType.Int, , xmlData.ReviewList[i].Id);
                //command.Parameters.Add("@Title", SqlDbType.VarChar, 50, xmlData.ReviewList[i].Title);
                //command.Parameters.Add("@Summary", SqlDbType.VarChar, 50, xmlData.ReviewList[i].Summary);
                //command.Parameters.Add("@Body", SqlDbType.VarChar, 50, xmlData.ReviewList[i].Body);
                //command.Parameters.Add("@GenreId", SqlDbType.Int, , xmlData.ReviewList[i].GenreId);


                //command.ExecuteNonQuery();
            }

            sqlConn.Close();
        }
        public static void serializeUsingWriteXML(string filename)
        {
            ////
            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select Id, Title, Summary, Body, GenreId from review", sqlConn);
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Review";
            adapter.Fill(dataTable);
            sqlConn.Close();

            //for test....
            dataTable.Namespace = "";
            dataTable.WriteXml(filename);

            //////
            
        }

        public static void serializeUsingXMLSerializer(string filename)
        {
            ////
            SqlConnection sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("select Id, Title, Summary, Body, GenreId from review", sqlConn);
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Review";
            adapter.Fill(dataTable);
            sqlConn.Close();


            //////
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(dataTable.GetType(), "");
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
            System.Xml.Serialization.XmlSerializerNamespaces ns = new System.Xml.Serialization.XmlSerializerNamespaces();
            ns.Add("", "");

            writer.Serialize(file, dataTable, ns);
            file.Close();
        }

    }


}
