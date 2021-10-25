using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using RestSharp;
using System.Data;
using System.Reflection;

namespace Payroll.Models
{
    public class Modal
    {
        public static string ProjectConfiguration() {
            
            return "";

        }
        public static string InvokeServiceReq(string MethodName, string ParamInput, string MethodType)
        {
            string REQRES = string.Empty;
            try
            {
                string ServiceURL = ConfigurationManager.AppSettings["SERVICEURL"].ToString().Trim() + MethodName;
                var client = new RestClient(ServiceURL);
                client.Timeout = -1;
                var request = new RestRequest(MethodType == "POST" ? Method.POST : Method.GET);
                //request.AddHeader("Authorization", "");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", ParamInput, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                REQRES = response.Content;
            }
            catch (Exception ex)
            {
                REQRES = string.Empty;
            }
            return REQRES;
        }

        public static DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names   
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow   
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

    }
}