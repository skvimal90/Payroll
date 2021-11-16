using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Payroll.Models;
using System.Globalization;

namespace Payroll.Controllers
{
    public class HomeAPIController : ApiController
    {

        #region company Management
        public RQRS.ResponseData CompanyManagement(RQRS.CompanyRegistration CompanyRegistration)
        {
            string test = string.Empty;
            DataSet dsOutput = new DataSet();
            RQRS.ResponseData ResponseData = new RQRS.ResponseData();
            string strErrorMsg = string.Empty;
            string strStoredProcedure = string.Empty;
            try
            {
                Hashtable hsParam = new Hashtable();
                    hsParam.Add("Com_Payroll_Id", CompanyRegistration.strPayrollID ?? "");
                    hsParam.Add("Com_Name", CompanyRegistration.strCompanyName ?? "");
                    hsParam.Add("Com_Type", CompanyRegistration.strCompanyType ?? "");
                    hsParam.Add("Com_Address", CompanyRegistration.strStreet ?? "");
                    hsParam.Add("Com_Phone_No", CompanyRegistration.strCompanyPhnNo ?? "");
                    hsParam.Add("Com_Mobile_No", CompanyRegistration.strCompanyPhnNo ?? "");
                    hsParam.Add("Com_Email_Id", CompanyRegistration.strCompanyEMail ?? "");
                    hsParam.Add("Com_CITY", CompanyRegistration.strCity ?? "");
                    hsParam.Add("Com_STATE", CompanyRegistration.strState ?? "");
                    hsParam.Add("Com_COUNTRY", CompanyRegistration.strCountry ?? "");
                    hsParam.Add("Com_Postal_Code", CompanyRegistration.strPostalCode ?? "");
                    hsParam.Add("Com_CONTACT_PERSON", "");
                    hsParam.Add("Com_CONTACT_PERSON_CONTACT_NO1", "");
                    hsParam.Add("Com_CONTACT_PERSON_CONTACT_NO2", "");
                    hsParam.Add("Com_CONTACT_PERSON_EMAIL_ID", CompanyRegistration.strCompanyEMail ?? "");
                    hsParam.Add("Com_COMPANY_START_YEAR", "");
                   // hsParam.Add("Com_ACTIVE_STATUS", CompanyRegistration.strCompanyStatus ?? "");
                    hsParam.Add("Com_Logo",  (CompanyRegistration.strLogoPath != null ?ImageToStream(CompanyRegistration.strLogoPath):new byte[] { }));// "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAQUAAADBCAMAAADxRlW1AAAAgVBMVEX///8AAAIAAADHx8doaGno6OjCwsKpqan8/Pzw8PCzs7P39/dubm67u7uhoaHt7e2bm5t9fX3T09PY2Nh5eXkbGxyTk5OMjIzg4OCsrKyEhIRVVVXW1tZdXV3MzMwhISFBQUE5OTlLS0thYWErKysRERIoKCgZGRpYWFkLCwsyMjPvUZIaAAAGDklEQVR4nO2cbXeiOhSFmVgUUFF8AXyptXVaO/3/P/BKkhMCAcf7AVyc2c+HrhrjWskmJDsnBzwPAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA+L8Efn5cSY7H4zrPc//GIgyD4Nkt65GFuMv2fMlGz25jD4QX8auZUovEf3Yru+dV1DreIMeFvQ4B9bvxnqCv9s9uZtckqqtiQxNiEExD31+f5vGMdBDiynw4ZLqjy6YvTwejw6nvhvVKek8Fz1u9/RMyLHUv47YKM5Ih77NZPRP/TQUzWgRjKzXRfZy0V6EJdNZfq/qmqsI82yfJJpmVJLfSd11n/dymdkhVhZPrGXael6uVQnzcaoiX6LkN7oRd7Y6Ia+5RiNDzXnSlvDCbQqTsZoi6Ct5emP4rDrf1Ulfay/q3Mm7jIdId3FFBTr75JdtLih7rW0J4ni/kPwdew4FUKK8u2UXbJtEt4ZeCsLIPrgrkFu3QAnmrcfmvWPXf2M6YOyp8NIyFyPJWFxoNjGQYOyp8N6hAtbLiw4Rk4LPRdFWgOWDVUEuqYNaVt57b2h2uCrR9Olq16I5Iq1WyftvaHeO6X/A2DX45rtWieyLsta3d8ZgKm9q6QffEpte2dke7CrYh2Ooyc+1pMDAxTw+p4OuiL1OSOdPJoHlIBXJKZVjuqEte+mxrdzykAo3/hVvUY1M7xFWBQkulJ4obLvxZl017bGt3kApl3NFRIWwYCsYy8NhUjRwV9nUVKOCW2r9LGtbT4eKqkNVWRXLUP5XfNbmK4TJypv+0qgKJUPOJF2f2GDKuCksVTVLzXkD77PoE8KXLedgmV4WJjDYuA/VtiwgBr22lq0IkzpnaVfuvJMJnfeDTz5hsJEZNC4Akn5mB4DpEmhzHfbSxe0iFWqgg3L0Lsgm/3OSmgJd1NCpY6SrBanm2ElncUVKuI1zCLKTCYZ3n69N4l85+2/lNYtsUSDFukod/NirYeU1UoP422aIPZ0odOKSChRbjKpRxcB0BucvfT2hvNzSMBbG9pPPb0uh/ShkO9Z/QMXdldzVsaF743iRJlqVxdCqtga9GQy3BwxxOMcp0IhWc1bC4EbSZrlxzk+c0762N3TNy7c9iFWUHNR8498Tqk0RglSHtqvCtZ4fQfGt6vHhp21cMHFcF2kpLLyDXRLGVX/j3LPWwaVdBflirwRDd5olvY6nfWN0NBaRCOddVVPAOcjD8uf33Q5tpJmcQNu0qxPPJglKaisFAmQ5sDKNFqwrSR0117FVGUw7aTPKaGCXtKhSlCR3aS4t0UjJ8Pq+1XTG+OxaKyUF1/bX4SptJbivEHRXEbDJeF65RhZVUCFrNDVwiTCXtKlDs4CSsSfGipoYnNLRT/q6CZ88GU/WB24NU7dldRgW1f9J5TrEaDDwOYwwPqKAnA339/0gZLr03tFPaVTAxprASb9YrJy/T8IAK3lV1XCdAqsHw3XdDO+URFfRaqcPuE4aDwc2DdlXQ/d7qj2qZYPUUlZsT76pwqp7SJ2qZ4HIWUUAqlCFWV4VQl+gnSda2jeIBZbGWuSzV+IJEh1doebyyM5B0uFBeWXcs6AdHTL/TajSSAZTFV57JOq6pPJHTa+W6OjQYQKfP5c6gQYX6kwCtZ3dDxX18uEEFSv6jW2JmwnBMOOgOvpuSBhUyCrzoqUC7aD7+0RzUm07rV7XYmX0bqqRPqX09W3K5Jah/VhjNVSEU9VqCVcwprUZaJa4KX1atd2kg1aJhfjJogq/q4/XqpjjUVXit1iqWSzVdysOawTOdR1G0m0zigkmk8xFIBRNOWqbZfp9l6XIZR/PRqlCHvBafPI46pMK9rbO7H+fG+QEV1o7L4Aadyt57jNqv2Sh+XN3sFoeQDASntJ4KlKpx56093pRUOPfVqp4xl/meGZgaG8Voe21Dr6e5awZ8owIbF10hL91y++u8rNN98cMp/qiZiwZP7bApKzFLfJSkVbPcEk1a1V5sxeX9AxbTcKHeBHx3pAfTha7Gcl4AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADQOf8B3B84eR0mVA8AAAAASUVORK5CYII=");
                    hsParam.Add("Com_Registration_No", "");
                    hsParam.Add("Com_E_No", (CompanyRegistration.strEno ?? ""));
                    hsParam.Add("Com_C_No", (CompanyRegistration.strCno ?? ""));
                    hsParam.Add("Com_EPF_No", (CompanyRegistration.strEPF ?? ""));
                    hsParam.Add("Com_SOCSO_No", (CompanyRegistration.strSocgo ?? ""));
                    hsParam.Add("Com_Zakat_No", (CompanyRegistration.strZakarNo ?? ""));
                    hsParam.Add("Password", CompanyRegistration.strPassword ?? "");
                    hsParam.Add("Com_Username", CompanyRegistration.strUserName ?? "");
                    hsParam.Add("Username", "");
                    hsParam.Add("FLAG", CompanyRegistration.strFlag);

                dsOutput = DBHandler.ExecProcedureReturnsDataset(DBHandler.StoreProcedure.P_MANAGE_COMPANY_DETAILS, hsParam, ref strErrorMsg);

                if (dsOutput != null && dsOutput.Tables.Count > 0 && dsOutput.Tables[0].Columns.Count > 0)
                {
                    if (dsOutput.Tables[0].Columns.Contains("Status") && dsOutput.Tables[0].Rows[0]["Status"].ToString() == "01")
                    {
                        ResponseData.strStatus = "01";
                        ResponseData.strResponse = "";
                        ResponseData.strErrorMessage = dsOutput.Tables[0].Columns.Contains("Message") ? dsOutput.Tables[0].Rows[0]["Message"].ToString() : "Unable to procees your request.";
                    }
                    else
                    {
                        if(CompanyRegistration.strFlag != "F")
                        {
                            ResponseData.strStatus = "00";
                            ResponseData.strResponse = dsOutput.Tables[0].Columns.Contains("Message") ? dsOutput.Tables[0].Rows[0]["Message"].ToString() : "Unable to procees your request.";

                        }
                        else
                        {

                            ResponseData.strStatus = "00";
                            var CompanyDetails = (from dr in dsOutput.Tables[0].AsEnumerable()
                                                  select new
                                                  {
                                                      Com_Logo = Convert.ToBase64String((byte[])dr["Com_Logo"]),
                                                      Com_Id = dr["Com_Id"],
                                                      Com_Name = dr["Com_Name"],
                                                      Com_Type = dr["Com_Type"],
                                                      Com_COMPANY_START_YEAR = dr["Com_COMPANY_START_YEAR"],
                                                      Com_Email_Id = dr["Com_Email_Id"],
                                                      Com_Phone_No = dr["Com_Phone_No"],
                                                      Com_STATE = dr["Com_STATE"],
                                                      Com_Registration_No = dr["Com_Registration_No"],
                                                      Com_E_No = dr["Com_E_No"],
                                                      Com_C_No = dr["Com_C_No"],
                                                      Com_EPF_No = dr["Com_EPF_No"],
                                                      Com_SOCSO_No = dr["Com_SOCSO_No"],
                                                      Com_Zakat_No = dr["Com_Zakat_No"],
                                                      Com_Created_Date = dr["Com_Created_Date"],
                                                      Com_Created_By = dr["Com_Created_By"],
                                                      Com_Active_Status = dr["Com_Active_Status"],
                                                      Com_Address = dr["Com_Address"],
                                                      Com_CITY = dr["Com_CITY"],
                                                      Com_COUNTRY = dr["Com_COUNTRY"],
                                                      Com_Postal_Code = dr["Com_Postal_Code"],
                                                      Com_CONTACT_PERSON_EMAIL_ID = dr["Com_CONTACT_PERSON_EMAIL_ID"]
                                                  });
                            DataTable dsResult = Modal.ConvertToDataTable(CompanyDetails);
                            ResponseData.strResponse = JsonConvert.SerializeObject(CompanyDetails);
                        }
                    }
                }
                else
                {
                    ResponseData.strStatus = "01";
                    ResponseData.strResponse = "";
                    ResponseData.strErrorMessage = "Unable to process your request";
                }
            }
            catch (Exception ex)
            {
                ResponseData.strStatus = "01";
                ResponseData.strResponse = "";
                ResponseData.strErrorMessage = "Problem occured while processing request(#01).";
            }
            return ResponseData;
        }
        #endregion

        #region PayRoll_User Management
        public RQRS.ResponseData PayRoll_User_Management(RQRS.CompanyRegistration CompanyRegistration)
        {
            string test = string.Empty;
            DataSet dsOutput = new DataSet();
            RQRS.ResponseData ResponseData = new RQRS.ResponseData();
            string strErrorMsg = string.Empty;
            string strStoredProcedure = string.Empty;
            try
            {
                Hashtable hsParam = new Hashtable();
                hsParam.Add("PRL_ID", CompanyRegistration.strPayrollID ?? "");
                hsParam.Add("PRL_Name", CompanyRegistration.strCompanyName ?? "");
                hsParam.Add("PRL_Address", CompanyRegistration.strStreet ?? "");
                hsParam.Add("PRL_Phone_No", CompanyRegistration.strCompanyPhnNo ?? "");
                hsParam.Add("PRL_Mobile_No", CompanyRegistration.strCompanyPhnNo ?? "");
                hsParam.Add("PRL_Email_Id", CompanyRegistration.strCompanyEMail ?? "");
                hsParam.Add("PRL_CITY", CompanyRegistration.strCity ?? "");
                hsParam.Add("PRL_STATE", CompanyRegistration.strState ?? "");
                hsParam.Add("PRL_COUNTRY", CompanyRegistration.strCountry ?? "");
                hsParam.Add("PRL_Postal_Code", CompanyRegistration.strPostalCode ?? "");
                hsParam.Add("PRL_COMPANY_START_YEAR", "");
                hsParam.Add("PRL_Logo", (CompanyRegistration.strLogoPath != null ? ImageToStream(CompanyRegistration.strLogoPath) : new byte[] { }));// "data:image/png;base64,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");
                hsParam.Add("PRL_Registration_No", "");
                //hsParam.Add("PRL_E_No", (CompanyRegistration.strEno ?? ""));
                //hsParam.Add("PRL_C_No", (CompanyRegistration.strCno ?? ""));
                //hsParam.Add("PRL_EPF_No", (CompanyRegistration.strEPF ?? ""));
                //hsParam.Add("PRL_SOCSO_No", (CompanyRegistration.strSocgo ?? ""));
                //hsParam.Add("PRL_Zakat_No", (CompanyRegistration.strZakarNo ?? ""));
                hsParam.Add("PRL_Password", CompanyRegistration.strPassword ?? "");
                hsParam.Add("PRL_Username", CompanyRegistration.strUserName ?? "");
                hsParam.Add("Username", "");
                hsParam.Add("FLAG", CompanyRegistration.strFlag);

                dsOutput = DBHandler.ExecProcedureReturnsDataset(DBHandler.StoreProcedure.P_MANAGE_PAYROLL_DETAILS, hsParam, ref strErrorMsg);

                if (dsOutput != null && dsOutput.Tables.Count > 0 && dsOutput.Tables[0].Columns.Count > 0)
                {
                    if (dsOutput.Tables[0].Columns.Contains("Status") && dsOutput.Tables[0].Rows[0]["Status"].ToString() == "01")
                    {
                        ResponseData.strStatus = "01";
                        ResponseData.strResponse = "";
                        ResponseData.strErrorMessage = dsOutput.Tables[0].Columns.Contains("Message") ? dsOutput.Tables[0].Rows[0]["Message"].ToString() : "Unable to procees your request.";
                    }
                    else
                    {
                        if (CompanyRegistration.strFlag != "F")
                        {
                            ResponseData.strStatus = "00";
                            ResponseData.strResponse = dsOutput.Tables[0].Columns.Contains("Message") ? dsOutput.Tables[0].Rows[0]["Message"].ToString() : "Unable to procees your request.";

                        }
                        else
                        {

                            ResponseData.strStatus = "00";
                            var CompanyDetails = (from dr in dsOutput.Tables[0].AsEnumerable()
                                                  select new
                                                  {
                                                      Com_Logo = Convert.ToBase64String((byte[])dr["Com_Logo"]),
                                                      Com_Id = dr["Com_Id"],
                                                      Com_Name = dr["Com_Name"],
                                                      Com_Type = dr["Com_Type"],
                                                      Com_COMPANY_START_YEAR = dr["Com_COMPANY_START_YEAR"],
                                                      Com_Email_Id = dr["Com_Email_Id"],
                                                      Com_Phone_No = dr["Com_Phone_No"],
                                                      Com_STATE = dr["Com_STATE"],
                                                      Com_Registration_No = dr["Com_Registration_No"],
                                                      Com_E_No = dr["Com_E_No"],
                                                      Com_C_No = dr["Com_C_No"],
                                                      Com_EPF_No = dr["Com_EPF_No"],
                                                      Com_SOCSO_No = dr["Com_SOCSO_No"],
                                                      Com_Zakat_No = dr["Com_Zakat_No"],
                                                      Com_Created_Date = dr["Com_Created_Date"],
                                                      Com_Created_By = dr["Com_Created_By"],
                                                      Com_Active_Status = dr["Com_Active_Status"],
                                                      Com_Address = dr["Com_Address"],
                                                      Com_CITY = dr["Com_CITY"],
                                                      Com_COUNTRY = dr["Com_COUNTRY"],
                                                      Com_Postal_Code = dr["Com_Postal_Code"],
                                                      Com_CONTACT_PERSON_EMAIL_ID = dr["Com_CONTACT_PERSON_EMAIL_ID"]
                                                  });
                            DataTable dsResult = Modal.ConvertToDataTable(CompanyDetails);
                            ResponseData.strResponse = JsonConvert.SerializeObject(CompanyDetails);
                        }
                    }
                }
                else
                {
                    ResponseData.strStatus = "01";
                    ResponseData.strResponse = "";
                    ResponseData.strErrorMessage = "Unable to process your request";
                }
            }
            catch (Exception ex)
            {
                ResponseData.strStatus = "01";
                ResponseData.strResponse = "";
                ResponseData.strErrorMessage = "Problem occured while processing request(#01).";
            }
            return ResponseData;
        }
        #endregion

        #region Department
        public RQRS.ResponseData DepartmentManagement(RQRS.DepartmentReq DepartmentReq)
        {
            DataSet dsOutput = new DataSet();
            RQRS.ResponseData ResponseData = new RQRS.ResponseData();
            string strErrorMsg = string.Empty;
            try
            {
                Hashtable hsParam = new Hashtable();
                
                hsParam.Add("DEPARTMENTID", DepartmentReq.strDepartmentID != null ? DepartmentReq.strDepartmentID : "");
                hsParam.Add("DEPARTMENTNAME", DepartmentReq.strDepartment != null ? DepartmentReq.strDepartment : "");
                hsParam.Add("DESIGNATIONID", DepartmentReq.strDesignationID != null ? DepartmentReq.strDesignationID: "");
                hsParam.Add("REMARK", DepartmentReq.strRemark != null ? DepartmentReq.strRemark : "");
                hsParam.Add("FLAG", DepartmentReq.strFlag);
                hsParam.Add("STATUS", "");
                hsParam.Add("MESSAGE", "");
                
                dsOutput = DBHandler.ExecProcedureReturnsDataset(DBHandler.StoreProcedure.P_MANAGE_DEPARTMENT, hsParam, ref strErrorMsg);

                if (dsOutput != null && dsOutput.Tables.Count > 0 && dsOutput.Tables[0].Columns.Count > 0)
                {
                    if (DepartmentReq.strFlag == "F")
                    {
                        ResponseData.strStatus = "00";
                        ResponseData.strErrorMessage = "";
                        ResponseData.strResponse = JsonConvert.SerializeObject(dsOutput);

                    }
                    else if (dsOutput.Tables[0].Columns.Contains("STATUS") && dsOutput.Tables[0].Rows[0]["STATUS"].ToString() == "00")
                    {
                        ResponseData.strStatus = "00";
                        ResponseData.strResponse = (dsOutput.Tables[0].Columns.Contains("MESSAGE") && dsOutput.Tables[0].Rows[0]["MESSAGE"] != null ? dsOutput.Tables[0].Rows[0]["MESSAGE"].ToString() : "");
                        ResponseData.strErrorMessage = "";
                    }
                    else
                    {
                        ResponseData.strStatus = "01";
                        ResponseData.strErrorMessage = (dsOutput.Tables[0].Columns.Contains("MESSAGE") && dsOutput.Tables[0].Rows[0]["MESSAGE"] != null ? dsOutput.Tables[0].Rows[0]["MESSAGE"].ToString() : "Unable to process your request.");
                    }
                }
                else
                {
                    ResponseData.strStatus = "01";
                    ResponseData.strResponse = "";
                    ResponseData.strErrorMessage = "Unable to process your request.";
                }
            }
            catch (Exception ex)
            {
                ResponseData.strStatus = "01";
                ResponseData.strResponse = "";
                ResponseData.strErrorMessage = "Problem occured while processing request(#01).";
            }
            return ResponseData;
        }
        #endregion

        #region Designation
        public RQRS.ResponseData DesignationManagement(RQRS.DesigantionReq DesigantionReq)
        {
            DataSet dsOutput = new DataSet();
            RQRS.ResponseData ResponseData = new RQRS.ResponseData();
            string strErrorMsg = string.Empty;
            try
            {
                Hashtable hsParam = new Hashtable();

                hsParam.Add("DESIGNATIONID", DesigantionReq.strDesignationID!= null ? DesigantionReq.strDesignationID : "");
                hsParam.Add("DESIGNATTIONNAME", DesigantionReq.strDesignation != null ? DesigantionReq.strDesignation : "");
                hsParam.Add("DEPARTMENTID", DesigantionReq.strDepartmentID != null ? DesigantionReq.strDepartmentID : "");
                hsParam.Add("REMARK", DesigantionReq.strRemark != null ? DesigantionReq.strRemark : "");
                hsParam.Add("FLAG", DesigantionReq.strFlag);
                hsParam.Add("STATUS", "");
                hsParam.Add("MESSAGE", "");

                
                dsOutput = DBHandler.ExecProcedureReturnsDataset(DBHandler.StoreProcedure.P_MANAGE_DESIGNATION, hsParam, ref strErrorMsg);

                if (dsOutput != null && dsOutput.Tables.Count > 0 && dsOutput.Tables[0].Columns.Count > 0)
                {
                    if (DesigantionReq.strFlag == "F")
                    {
                        ResponseData.strStatus = "00";
                        ResponseData.strErrorMessage = "";
                        ResponseData.strResponse = JsonConvert.SerializeObject(dsOutput);

                    }
                    else if (dsOutput.Tables[0].Columns.Contains("STATUS") && dsOutput.Tables[0].Rows[0]["STATUS"].ToString() == "00")
                    {
                        ResponseData.strStatus = "00";
                        ResponseData.strResponse = (dsOutput.Tables[0].Columns.Contains("MESSAGE") && dsOutput.Tables[0].Rows[0]["MESSAGE"] != null ? dsOutput.Tables[0].Rows[0]["MESSAGE"].ToString() : "");
                        ResponseData.strErrorMessage = "";
                    }
                    else
                    {
                        ResponseData.strStatus = "01";
                        ResponseData.strErrorMessage = (dsOutput.Tables[0].Columns.Contains("MESSAGE") && dsOutput.Tables[0].Rows[0]["MESSAGE"] != null ? dsOutput.Tables[0].Rows[0]["MESSAGE"].ToString() : "Unable to process your request.");
                    }
                }
                else
                {
                    ResponseData.strStatus = "01";
                    ResponseData.strResponse = "";
                    ResponseData.strErrorMessage = "Unable to process your request.";
                }
            }
            catch (Exception ex)
            {
                ResponseData.strStatus = "01";
                ResponseData.strResponse = "";
                ResponseData.strErrorMessage = "Problem occured while processing request(#01).";
            }
            return ResponseData;
        }
        #endregion

        #region Employee
        public RQRS.ResponseData EmployeeManagement(RQRS.EmployeeReq EmployeeReq)
        {
            DataSet dsOutput = new DataSet();
            RQRS.ResponseData ResponseData = new RQRS.ResponseData();
            string strErrorMsg = string.Empty;
            string strStoredProcedure = string.Empty;
            try
            {
                string strProcedure = string.Empty;
                Hashtable hsParam = new Hashtable();
                hsParam.Add("Emp_Id", EmployeeReq.strEmpID ?? "");
                hsParam.Add("Emp_Company_Id", EmployeeReq.strEmpCompanyId ?? "");
                hsParam.Add("Emp_Payroll_Id", EmployeeReq.strEmpID ?? "");
                hsParam.Add("Emp_Tittle", EmployeeReq.strEmpTittle ?? "");
                hsParam.Add("Emp_Name", EmployeeReq.strEmpName ?? "");
                hsParam.Add("Emp_Phone_No", EmployeeReq.strEmpPhoneNo ?? "");
                hsParam.Add("Emp_Mobile_No", EmployeeReq.EmpMobileNo ?? "");
                hsParam.Add("Emp_Email_Id", EmployeeReq.strEmpEmailId ?? "");
                hsParam.Add("Emp_Position", EmployeeReq.strEmpPosition ?? "");
                hsParam.Add("Emp_Id_Proof", EmployeeReq.strEmpIdProof ?? "");


                hsParam.Add("Emp_Id_Proof_No", EmployeeReq.strEmpIdProofNo ?? "");
                hsParam.Add("Emp_DOB", EmployeeReq.strEmpDOB ?? "");
                hsParam.Add("Emp_Gender", EmployeeReq.strEmpGender ?? "");
                hsParam.Add("Emp_Marital_Status", EmployeeReq.strEmpMaritalStatus ?? "");
                hsParam.Add("Emp_Per_Address", EmployeeReq.strEmpPerAddress ?? "");
                hsParam.Add("Emp_Temp_Address", EmployeeReq.strEmpTempAddress ?? "");
                hsParam.Add("Emp_City", EmployeeReq.strEmpCity ?? "");
                hsParam.Add("Emp_STATE", EmployeeReq.strEmpSTATE ?? "");
                hsParam.Add("Emp_COUNTRY", EmployeeReq.strEmpCOUNTRY ?? "");
                hsParam.Add("Emp_Postal_Code", EmployeeReq.strEmpPostalCode ?? "");

                hsParam.Add("Emp_Residence_Status", EmployeeReq.strEmpResidenceStatus ?? "");
                hsParam.Add("Emp_Worker_Status", EmployeeReq.strEmpWorkerStatus ?? "");
                hsParam.Add("Emp_Nationality", EmployeeReq.strEmpNationality ?? "");
                hsParam.Add("Emp_Children", EmployeeReq.intEmpChildren );
                hsParam.Add("Emp_Disability_Status", EmployeeReq.strEmpDisabilityStatus ?? "");
                hsParam.Add("Emp_Race", EmployeeReq.strEmpRace ?? "");
                hsParam.Add("Emp_Perasonal_Email_Id", EmployeeReq.strEmpPerasonalEmailId ?? "");
                hsParam.Add("Emp_Emer_Contact_Person", EmployeeReq.strEmpEmerContactPerson ?? "");
                hsParam.Add("Emp_Emer_Contact_Ph_No", EmployeeReq.strEmpEmerContactPhNo ?? "");
                hsParam.Add("Emp_Payment_Method", EmployeeReq.strEmpPaymentMethod ?? "");

                hsParam.Add("Emp_Bank_Account_No", EmployeeReq.strEmpBankAccountNo ?? "");
                hsParam.Add("Emp_Bank_Name", EmployeeReq.strEmpBankName ?? "");
                hsParam.Add("Emp_Bank_Branch", EmployeeReq.strEmpBankBranch ?? "");
                hsParam.Add("Emp_IFSC_Code", EmployeeReq.strEmpIFSCCode ?? "");
                hsParam.Add("Emp_Join_Date", EmployeeReq.strEmpJoinDate ?? "");
                hsParam.Add("Emp_Payroll_Type ", EmployeeReq.strEmpPayrollType ?? "");
                hsParam.Add("Emp_Passport_No", EmployeeReq.strEmpPassportNo ?? "");
                hsParam.Add("Emp_Passport_Expiry_Date", EmployeeReq.strEmpPassportExpiryDate ?? "");
                hsParam.Add("Emp_Basic_Sal", EmployeeReq.intEmp_Basic_Sal);
                hsParam.Add("Emp_HRA", EmployeeReq.intEmpHRA);

                hsParam.Add("Emp_Allowance", EmployeeReq.intEmpAllowance);
                hsParam.Add("Emp_EMP_PF", EmployeeReq.intEmpEMPPF);
                hsParam.Add("Emp_ER_PF", EmployeeReq.intEmpERPF);
                hsParam.Add("Emp_Emp_SOCSO", EmployeeReq.intEmpEmpSOCSO);
                hsParam.Add("Emp_ER_SOCSO", EmployeeReq.intEmpERSOCSO);
                hsParam.Add("Emp_Emp_EIS", EmployeeReq.intEmpEmpEIS);
                hsParam.Add("Emp_ER_EIS", EmployeeReq.intEmpEREIS);
                hsParam.Add("Emp_EMP_PF_Per", EmployeeReq.intEmpEMPPFPer);
                hsParam.Add("Emp_ER_PF_Per", EmployeeReq.intEmpERPF);
                hsParam.Add("Emp_Emp_SOCSO_Per", EmployeeReq.intEmpSOCSOPer);

                hsParam.Add("Emp_ER_SOCSO_Per", EmployeeReq.intEmpERSOCSOPer);
                hsParam.Add("Emp_Emp_EIS_Per", EmployeeReq.intEmpEISPer);
                hsParam.Add("Emp_ER_EIS_Per", EmployeeReq.intEREISPer);
                hsParam.Add("Emp_SOCSO_Category", EmployeeReq.intEmpEmpSOCSO);
                hsParam.Add("Emp_PCB_No", EmployeeReq.intEmpPCBNo);
                hsParam.Add("Emp_Username", EmployeeReq.strEmpUsername ?? "");
                hsParam.Add("Username", EmployeeReq.strUsername ?? "");
                hsParam.Add("LVE_Casual_Leave", EmployeeReq.intLVECasualLeave);
                hsParam.Add("LVE_Personal_Leave", EmployeeReq.intLVEPersonalLeave);
                hsParam.Add("LVE_Annual_Leave", EmployeeReq.intLVEAnnualLeave);


                hsParam.Add("LVE_Marriage_Leave", EmployeeReq.intLVEMarriageLeave);
                hsParam.Add("LVE_Medical_Leave", EmployeeReq.intLVEMedicalLeave);
                hsParam.Add("LVE_Hospital_Leave", EmployeeReq.intLVEHospitalLeave);
                hsParam.Add("LVE_Compassionate_Leave", EmployeeReq.intLVECompassionateLeave);
                hsParam.Add("FLAG", EmployeeReq.strFlag);

                    strProcedure = DBHandler.StoreProcedure.P_INSERT_EMPLOYEE_DETAILS;
                

                dsOutput = DBHandler.ExecProcedureReturnsDataset(strProcedure, hsParam, ref strErrorMsg);
                string str = JsonConvert.SerializeObject(dsOutput);

                if (dsOutput != null && dsOutput.Tables.Count > 0 && dsOutput.Tables[0].Columns.Count > 0)
                {
                    if (dsOutput.Tables[0].Columns.Contains("Status") && dsOutput.Tables[0].Rows[0]["Status"].ToString() == "01")
                    {
                        ResponseData.strStatus = "01";
                        ResponseData.strResponse = "";
                        ResponseData.strErrorMessage = dsOutput.Tables[0].Columns.Contains("Message") ? dsOutput.Tables[0].Rows[0]["Message"].ToString() : "Unable to procees your request.";
                    }
                    else
                    {

                        ResponseData.strStatus = "00";
                        ResponseData.strResponse = dsOutput.Tables[0].Columns.Contains("Message") ? dsOutput.Tables[0].Rows[0]["Message"].ToString() : JsonConvert.SerializeObject(dsOutput);
                    }
                }
                else
                {
                    ResponseData.strStatus = "01";
                    ResponseData.strResponse = "";
                    ResponseData.strErrorMessage = "Unable to process your request";
                }
            }
            catch (Exception ex)
            {
                ResponseData.strStatus = "01";
                ResponseData.strResponse = "";
                ResponseData.strErrorMessage = "Problem occured while processing request(#01).";
            }
            return ResponseData;
        }
        #endregion

        #region Leave Request
        public RQRS.ResponseData LeaveRequestManagement(RQRS.LeaveReq LeaveReq)
        {
            DataSet dsOutput = new DataSet();
            RQRS.ResponseData ResponseData = new RQRS.ResponseData();
            string strErrorMsg = string.Empty;
            string strStoredProcedure = string.Empty;
            try
            {
                Hashtable hsParam = new Hashtable();
                if (LeaveReq.strFlag == "F")
                {
                    hsParam.Add("PEL_EMP_ID", LeaveReq.strEmpID ?? "");
                    hsParam.Add("PEL_Company_Id", LeaveReq.strCommpanyID ?? "");
                    hsParam.Add("PEL_Payroll_Id", LeaveReq.strPayrollID ?? "");
                    hsParam.Add("FROMDATE", LeaveReq.strLeaveFrom ?? "");
                    hsParam.Add("TODATE", LeaveReq.strLeaveTO ?? "");
                    hsParam.Add("Leave_Status", LeaveReq.strStatus ?? "");
                    strStoredProcedure = DBHandler.StoreProcedure.P_FETCH_LEAVE_REPORT;
                }
                else
                {
                    hsParam.Add("PEL_EMP_ID", LeaveReq.strEmpID ?? "");
                    hsParam.Add("PEL_Company_Id", LeaveReq.strCommpanyID ?? "");
                    hsParam.Add("PEL_Payroll_Id", LeaveReq.strPayrollID ?? "");
                    hsParam.Add("PEL_Leave_Type", LeaveReq.strLeaveType ?? "");
                    hsParam.Add("PEL_Leave_From", DateTime.ParseExact(LeaveReq.strLeaveFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    hsParam.Add("PEL_Leave_To", DateTime.ParseExact(LeaveReq.strLeaveTO, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    hsParam.Add("PEL_Leave_Days", LeaveReq.strLeaveDays);
                    hsParam.Add("PEL_Reason", LeaveReq.strReason ?? "");
                    hsParam.Add("PEL_Emergency_Contact_No", LeaveReq.strEmergencyCOnt ?? "");
                    hsParam.Add("PET_Willingtowork_Ondemand", LeaveReq.strWillingtoWork ?? "");
                    hsParam.Add("Username", LeaveReq.struserName ?? "");
                    strStoredProcedure = DBHandler.StoreProcedure.P_INSERT_LEAVE_REQUEST;
                } 
                
                dsOutput = DBHandler.ExecProcedureReturnsDataset(strStoredProcedure, hsParam, ref strErrorMsg);
                string str = JsonConvert.SerializeObject(dsOutput);

                if (dsOutput != null && dsOutput.Tables.Count > 0 && dsOutput.Tables[0].Columns.Count > 0)
                {
                    if (dsOutput.Tables[0].Columns.Contains("Status") && dsOutput.Tables[0].Rows[0]["Status"].ToString() == "01")
                    {
                        ResponseData.strStatus = "01";
                        ResponseData.strResponse = "";
                        ResponseData.strErrorMessage = dsOutput.Tables[0].Columns.Contains("Message") ? dsOutput.Tables[0].Rows[0]["Message"].ToString() : "Unable to procees your request.";
                    }
                    else
                    {

                        ResponseData.strStatus = "00";
                        ResponseData.strResponse = dsOutput.Tables[0].Columns.Contains("Message") ? dsOutput.Tables[0].Rows[0]["Message"].ToString() : JsonConvert.SerializeObject(dsOutput);
                    }
                }
                else
                {
                    ResponseData.strStatus = "01";
                    ResponseData.strResponse = "";
                    ResponseData.strErrorMessage = "Unable to process your request";
                }
            }
            catch (Exception ex)
            {
                ResponseData.strStatus = "01";
                ResponseData.strResponse = "";
                ResponseData.strErrorMessage = "Problem occured while processing request(#01).";
            }
            return ResponseData;
        }
        #endregion

        #region Login
        public RQRS.ResponseData FetchLogin(RQRS.LoginReq LoginReq)
        {
            DataSet dsOutput = new DataSet();
            RQRS.ResponseData ResponseData = new RQRS.ResponseData();
            string strErrorMsg = string.Empty;
            try
            {
                Hashtable hsParam = new Hashtable();
                hsParam.Add("Username", LoginReq.strUserName);
                hsParam.Add("Password", LoginReq.strPassword);

                dsOutput = DBHandler.ExecProcedureReturnsDataset(DBHandler.StoreProcedure.P_LOGIN_DETAILS, hsParam, ref strErrorMsg);

                if (dsOutput != null && dsOutput.Tables.Count > 0 && dsOutput.Tables[0].Columns.Count > 0)
                {
                    if (dsOutput.Tables[0].Columns.Contains("Col") && dsOutput.Tables[0].Rows[0]["Col"].ToString() != "")
                    {
                        ResponseData.strStatus = "01";
                        ResponseData.strResponse = "";
                        ResponseData.strErrorMessage = dsOutput.Tables[0].Rows[0]["Col"].ToString();
                    }
                    else
                    {
                        ResponseData.strStatus = "00";
                        ResponseData.strResponse = JsonConvert.SerializeObject(dsOutput);
                    }
                }
                else
                {
                    ResponseData.strStatus = "01";
                    ResponseData.strResponse = "";
                    ResponseData.strErrorMessage = "Unable to login. Please try again";
                }
            }
            catch (Exception ex)
            {
                ResponseData.strStatus = "01";
                ResponseData.strResponse = "";
                ResponseData.strErrorMessage = "Problem occured while processing request(#01).";
            }
            return ResponseData;
        }
        #endregion

        private byte[] ImageToStream(string fileName)
        {
            MemoryStream stream = new MemoryStream();

        tryagain:
            try
            {
                Bitmap image = new Bitmap(fileName);
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                goto tryagain;
            }

            return stream.ToArray();
        }

    }
}
