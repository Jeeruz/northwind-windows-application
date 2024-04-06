using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NorthwindProject
{
    class dbConnect
    {
        // declare sql parameter for later initialization 
        public static SqlConnection dbConnectInst = null;

        // use this if you want to use sql server authentication
        // create connection string that has details of the local database 
        public static string dbConnectionString = @"Data Source=LAPTOP-JEFF;"    // sql servername 
                                                  + "Initial Catalog=NORTHWIND;" // sql database name 
                                                  + "Integrated Security=false;" // ssl security
                                                  + "User ID = jeff;"           // sql username 
                                                  + "Password = 123;";   // sql password 

        // use this if you want to use windows authentication
        // create connection string that has details of the local database 
        //public static string dbConnectionString = @"Server=LAPTOP-JEFF;"          // sql servername 
        //                                        + "Database=NORTHWIND;"           // sql database name 
        //                                        + "Integrated Security=True;";    // ssl security 

        // used to open connection to database 
        public static void dbConnectOpen()
        {
            dbConnectInst = new SqlConnection();                 // initialize sql parameter declared above 
            dbConnectInst.ConnectionString = dbConnectionString; // set initialized sql parameter to have the declared sql connection string 
            dbConnectInst.Open();                                // open sql connection 
        }

        // used to close connection to database 
        public static void dbConnectClose()
        {
            dbConnectInst.Dispose(); // close sql connection 
        }
    }
}
