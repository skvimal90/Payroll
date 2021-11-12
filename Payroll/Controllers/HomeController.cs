using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home()
        {
            return View();
        }
        

        #region Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SubmitLogin(RQRS.LoginReq LoginReq)
        {
            string strStatus = string.Empty;
            string strMessage = string.Empty;
            string strResult = string.Empty;
            string strResponse = string.Empty;
            try
            {
                LoginReq.strTerminalType = "C";
                string strRequest = JsonConvert.SerializeObject(LoginReq);
                strResponse = Modal.InvokeServiceReq("HomeAPI/FetchLogin", strRequest, "POST");
                RQRS.ResponseData Response = JsonConvert.DeserializeObject<RQRS.ResponseData>(strResponse);
                if (Response.strStatus == "00")
                {
                    strStatus = "00";
                    strResult = Response.strResponse;
                    DataSet DsRes = JsonConvert.DeserializeObject<DataSet>(Response.strResponse);
                    if (DsRes != null && DsRes.Tables.Count > 0 && DsRes.Tables[0].Rows.Count > 0)
                    {
                        Session.Add("LoginUserName", DsRes.Tables[0].Rows[0]["LGN_UserName"].ToString());
                        Session.Add("LoginUserID", DsRes.Tables[0].Rows[0]["LGN_User_ID"].ToString());
                        Session.Add("LoginCompanyName", DsRes.Tables[0].Rows[0]["LGN_Name"].ToString());
                        Session.Add("LoginUserType", DsRes.Tables[0].Rows[0]["LGN_User_Type"].ToString());
                        Session.Add("PhoneNo", DsRes.Tables[0].Rows[0]["Phone_No"].ToString());
                        Session.Add("MobileNo", DsRes.Tables[0].Rows[0]["Mobile_No"].ToString());
                        Session.Add("EmailId", DsRes.Tables[0].Rows[0]["Email_Id"].ToString());
                    }

                }
                else
                {
                    strStatus = "01";
                    strMessage = Response.strErrorMessage;
                }
            }
            catch (Exception ex)
            {
                strStatus = "01";
                strResult = "Problem occured while fetch login(#05).";
            }
            return Json(new { Status = strStatus, Message = strMessage, Result = strResult });
        }
        #endregion

        #region Company 
        public ActionResult Company()
        {
            return View();
        }
        public ActionResult InsertCompanyDetails(RQRS.CompanyRegistration CompanyRegistration)
        {
            string strStatus = string.Empty;
            string strMessage = string.Empty;
            string strResult = string.Empty;
            string strResponse = string.Empty;
            try
            {

                string strRequest = JsonConvert.SerializeObject(CompanyRegistration);
                strResponse = Modal.InvokeServiceReq("HomeAPI/CompanyManagement", strRequest, "POST");
                RQRS.ResponseData Response = JsonConvert.DeserializeObject<RQRS.ResponseData>(strResponse);
                if (Response.strStatus == "00")
                {
                    strStatus = "00";
                    strResult = Response.strResponse;
                }
                else
                {
                    strStatus = "01";
                    strMessage = Response.strErrorMessage;
                }

            }
            catch (Exception ex)
            {
                strStatus = "01";
                strMessage = "Problem occured while processing request(#05).";
            }
            return Json(new { Status = strStatus, Message = strMessage, Result = strResult });
        }
        public ActionResult FetchCompanyDetails(string strCompanyName,string strCompanyType,string strCompanyMail)
        {
            string strStatus = string.Empty;
            string strMessage = string.Empty;
            string strResult = string.Empty;
            string strResponse = string.Empty;
            try

            {
                RQRS.CompanyRegistration CompanyRegistration = new RQRS.CompanyRegistration();
                CompanyRegistration.strCompanyName = strCompanyName != null ? strCompanyName : "";
                CompanyRegistration.strCompanyType = strCompanyType != null ? strCompanyType : ""; 
                CompanyRegistration.strCompanyEMail = strCompanyMail != null ? strCompanyMail : ""; 
                CompanyRegistration.strFlag = "F";
                string strRequest = JsonConvert.SerializeObject(CompanyRegistration);
                strResponse = Modal.InvokeServiceReq("HomeAPI/CompanyManagement", strRequest, "POST");
                RQRS.ResponseData Response = JsonConvert.DeserializeObject<RQRS.ResponseData>(strResponse);
                if (Response.strStatus == "00")
                {
                    strStatus = "00";
                    strResult = Response.strResponse;
                }
                else
                {
                    strStatus = "01";
                    strMessage = Response.strErrorMessage;
                }

            }
            catch (Exception ex)
            {
                strStatus = "01";
                strMessage = "Problem occured while processing request(#05).";
            }
            return Json(new { Status = strStatus, Message = strMessage, Result = strResult });
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            string strStatus = string.Empty;
            string StrMessage = string.Empty;
            string StrResult = string.Empty;
            try
            {
                string fname = string.Empty;
                string filename = string.Empty;
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        HttpFileCollectionBase files = Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFileBase file = files[i];
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];

                            fname = Path.Combine(Server.MapPath("~/Imgs/Client_Logo/"), fname);
                            file.SaveAs(fname);
                        }
                        strStatus = "00";
                        StrMessage = "";
                        StrResult = fname;
                    }
                    catch (Exception ex)
                    {
                        strStatus = "01";
                        StrMessage = "Problem occred while upload file(#05)";
                    }
                }
                else
                {
                    StrMessage = "No files selected.";
                    strStatus = "01";
                }
            }
            catch (Exception ex)
            {
                strStatus = "01";
                StrMessage = "Problem occred while upload file(#05)";
            }

            return Json(new { Status = strStatus, Result = StrResult, Message = StrMessage });
        }
        #endregion

        #region Add Pay Roll 
        public ActionResult AddPayRoll()
        {
            return View();
        }
        public ActionResult InsertPayRollDetails(RQRS.CompanyRegistration CompanyRegistration)
        {
            string strStatus = string.Empty;
            string strMessage = string.Empty;
            string strResult = string.Empty;
            string strResponse = string.Empty;
            try
            {

                string strRequest = JsonConvert.SerializeObject(CompanyRegistration);
                strResponse = Modal.InvokeServiceReq("HomeAPI/PayRoll_User_Management", strRequest, "POST");
                RQRS.ResponseData Response = JsonConvert.DeserializeObject<RQRS.ResponseData>(strResponse);
                if (Response.strStatus == "00")
                {
                    strStatus = "00";
                    strResult = Response.strResponse;
                }
                else
                {
                    strStatus = "01";
                    strMessage = Response.strErrorMessage;
                }

            }
            catch (Exception ex)
            {
                strStatus = "01";
                strMessage = "Problem occured while processing request(#05).";
            }
            return Json(new { Status = strStatus, Message = strMessage, Result = strResult });
        }

        public ActionResult FetchPayRollDetails()
        {
            string strStatus = string.Empty;
            string strMessage = string.Empty;
            string strResult = string.Empty;
            string strResponse = string.Empty;
            try

            {
                RQRS.CompanyRegistration CompanyRegistration = new RQRS.CompanyRegistration();
                CompanyRegistration.strPayrollID = "";
                CompanyRegistration.strFlag = "F";
                string strRequest = JsonConvert.SerializeObject(CompanyRegistration);
                strResponse = Modal.InvokeServiceReq("HomeAPI/PayRoll_User_Management", strRequest, "POST");
                RQRS.ResponseData Response = JsonConvert.DeserializeObject<RQRS.ResponseData>(strResponse);
                if (Response.strStatus == "00")
                {
                    strStatus = "00";
                    strResult = Response.strResponse;
                }
                else
                {
                    strStatus = "01";
                    strMessage = Response.strErrorMessage;
                }

            }
            catch (Exception ex)
            {
                strStatus = "01";
                strMessage = "Problem occured while processing request(#05).";
            }
            return Json(new { Status = strStatus, Message = strMessage, Result = strResult });
        }

        #endregion

        #region Department
        public ActionResult Department()
        {
            return View();
        }
        public ActionResult ManageDepartment(RQRS.DepartmentReq DepartmentReq)
        {
            string strStatus = string.Empty;
            string strMessage = string.Empty;
            string strResult = string.Empty;
            string strResponse = string.Empty;
            try
            {

                string strRequest = JsonConvert.SerializeObject(DepartmentReq);
                strResponse = Modal.InvokeServiceReq("HomeAPI/DepartmentManagement", strRequest, "POST");
                RQRS.ResponseData Response = JsonConvert.DeserializeObject<RQRS.ResponseData>(strResponse);

                if (Response.strStatus == "00")
                {
                    strStatus = "00";
                    strResult = Response.strResponse;
                }
                else
                {
                    strStatus = "01";
                    strMessage = Response.strErrorMessage;
                }

            }
            catch (Exception ex)
            {
                strStatus = "01";
                strMessage = "Problem occured while processing request(#05).";
            }
            return Json(new { Status = strStatus, Message = strMessage, Result = strResult });
        }
        #endregion

        #region Designation
        public ActionResult Designation()
        {
            return View();
        }
        public ActionResult ManageDesignation(RQRS.DesigantionReq DesigantionReq)
        {
            string strStatus = string.Empty;
            string strMessage = string.Empty;
            string strResult = string.Empty;
            string strResponse = string.Empty;
            try
            {

                string strRequest = JsonConvert.SerializeObject(DesigantionReq);
                strResponse = Modal.InvokeServiceReq("HomeAPI/DesignationManagement", strRequest, "POST");
                RQRS.ResponseData Response = JsonConvert.DeserializeObject<RQRS.ResponseData>(strResponse);

                if (Response.strStatus == "00")
                {
                    strStatus = "00";
                    strResult = Response.strResponse;
                }
                else
                {
                    strStatus = "01";
                    strMessage = Response.strErrorMessage;
                }

            }
            catch (Exception ex)
            {
                strStatus = "01";
                strMessage = "Problem occured while processing request(#05).";
            }
            return Json(new { Status = strStatus, Message = strMessage, Result = strResult });
        }
        #endregion

        #region Emplyee
        public ActionResult EmployeeCreation()
        {
            return View();
        }
        public ActionResult EmployeeList()
        {
            return View();
        }

        public ActionResult ManageEmployeeDetails(RQRS.EmployeeReq EmployeeReq)
        {
            string strStatus = string.Empty;
            string strMessage = string.Empty;
            string strResult = string.Empty;
            string strResponse = string.Empty;
            try
            {

                string strRequest = JsonConvert.SerializeObject(EmployeeReq);
                strResponse = Modal.InvokeServiceReq("HomeAPI/EmployeeManagement", strRequest, "POST");
                RQRS.ResponseData Response = JsonConvert.DeserializeObject<RQRS.ResponseData>(strResponse);
                if (Response.strStatus == "00")
                {
                    strStatus = "00";
                    strResult = Response.strResponse;
                }
                else
                {
                    strStatus = "01";
                    strMessage = Response.strErrorMessage;
                }

            }
            catch (Exception ex)
            {
                strStatus = "01";
                strMessage = "Problem occured while processing request(#05).";
            }
            return Json(new { Status = strStatus, Message = strMessage, Result = strResult });
        }
        #endregion

        #region Leave Request
        public ActionResult LeaveForm()
        {
            return View();
        }

        public ActionResult ManageLeaveRequest(RQRS.LeaveReq LeaveReq)
        {
            string strStatus = string.Empty;
            string strMessage = string.Empty;
            string strResult = string.Empty;
            string strResponse = string.Empty;
            try
            {

                string strRequest = JsonConvert.SerializeObject(LeaveReq);
                strResponse = Modal.InvokeServiceReq("HomeAPI/LeaveRequestManagement", strRequest, "POST");
                RQRS.ResponseData Response = JsonConvert.DeserializeObject<RQRS.ResponseData>(strResponse);
                if (Response.strStatus == "00")
                {
                    strStatus = "00";
                    strResult = Response.strResponse;
                }
                else
                {
                    strStatus = "01";
                    strMessage = Response.strErrorMessage;
                }

            }
            catch (Exception ex)
            {
                strStatus = "01";
                strMessage = "Problem occured while processing request(#05).";
            }
            return Json(new { Status = strStatus, Message = strMessage, Result = strResult });
        }
        #endregion

    }
}