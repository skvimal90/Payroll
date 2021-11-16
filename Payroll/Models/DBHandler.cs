using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class DBHandler
    {
        public static bool strResultProcedure(string type, string ProcedureName, Hashtable Parameters, ref string strResult, ref string strErrorMsg)
        {
            SqlConnection my_connection = null;
            SqlDataReader sqlDatareader = null;
            try
            {
                my_connection = new SqlConnection(Connection("A"));
                my_connection.Open();
                SqlCommand my_command = my_connection.CreateCommand();
                my_command.CommandText = ProcedureName;
                my_command.CommandType = CommandType.StoredProcedure;
                my_command.CommandTimeout = 0;

                AssignParameters(my_command, Parameters);

                sqlDatareader = my_command.ExecuteReader();
                //strResult = my_command.ExecuteNonQuery();
                if (sqlDatareader.Read())
                {
                    strResult = sqlDatareader[0].ToString().Trim();
                }
                sqlDatareader.Close();
            }
            catch (Exception my_exception)
            {
                strErrorMsg = my_exception.Message.ToString().Trim();
                return false;
            }
            finally
            {
                if (my_connection != null)
                {
                    my_connection.Close();
                }
            }
            return true;
        }

        public static DataSet ExecProcedureReturnsDataset(string ProcedureName, Hashtable Parameters, ref string strResult)
        {
            SqlConnection my_connection = null;
            DataSet my_dataset = new DataSet();
            string my_procedure = ProcedureName;
            try
            {
                if (true == false)
                {

                }
                else
                {
                    my_connection = new SqlConnection(Connection("A"));
                    SqlCommand my_command = my_connection.CreateCommand();
                    my_command.CommandText = ProcedureName;
                    my_command.CommandType = CommandType.StoredProcedure;
                    my_command.CommandTimeout = 0;
                    AssignParameters(my_command, Parameters);
                    SqlDataAdapter my_adapter = new SqlDataAdapter();
                    my_adapter.SelectCommand = my_command;
                    my_adapter.Fill(my_dataset, "Temp");

                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message.ToString();
                //  WriteLog(my_exception.Message + "\n" + my_exception.StackTrace);
            }
            finally
            {
                if (my_connection != null)
                {
                    my_connection.Close();
                }
            }
            return (my_dataset);
        }

        public static void AssignParameters(SqlCommand Command, Hashtable Parameters)
        {
            if (Parameters == null) return;
            foreach (object key in Parameters.Keys)
            {

                Command.Parameters.Add(("@" + key.ToString().ToUpper()), Parameters[key]);

            }

        }

        public static string Connection(string strType)
        {
            string strtype = string.Empty;
            string strConnectionString = string.Empty;
            switch (strType)
            {
                case "A":
                    strConnectionString = ConfigurationManager.AppSettings["ApplicationConnectionString"].ToString() != null ? ConfigurationManager.AppSettings["ApplicationConnectionString"].ToString() : "";
                    break;
                case "L":
                    strConnectionString = ConfigurationManager.AppSettings["LogConnectionString"].ToString() != null ? ConfigurationManager.AppSettings["LogConnectionString"].ToString() : "";
                    break;
               
            }
            return strConnectionString;
        }

        public class StoreProcedure
        {
            public static string P_MANAGE_PAYROLL_DETAILS = "P_MANAGE_PAYROLL_DETAILS";
            public static string P_MANAGE_COMPANY_DETAILS = "P_MANAGE_COMPANY_DETAILS";
            public static string P_LOGIN_DETAILS = "P_LOGIN_DETAILS";
            public static string P_MANAGE_DEPARTMENT = "P_MANAGE_DEPARTMENT";
            public static string P_MANAGE_DESIGNATION = "P_MANAGE_DESIGNATION";
            public static string P_INSERT_EMPLOYEE_DETAILS = "P_INSERT_EMPLOYEE_DETAILS";
            public static string P_MANAGE_EMPLOYEE_DETAILS = "P_MANAGE_EMPLOYEE_DETAILS";
            public static string P_INSERT_LEAVE_REQUEST = "P_INSERT_LEAVE_REQUEST";
            public static string P_FETCH_LEAVE_REPORT = "P_FETCH_LEAVE_REPORT";
        }
    }
}