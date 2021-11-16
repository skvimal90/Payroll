using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Models
{
    public class RQRS
    {
        #region Company Registration
        public class CompanyRegistration
        {
            public string strPayrollID { get; set; }
            public string strCompanyName { get; set; }
            public string strCompanyType { get; set; }
            public string strCompanyEMail { get; set; }
            public string strCompanyPhnNo { get; set; }

            //public string strContactPerson { get; set; }
            public string strStreet { get; set; }
            public string strCity { get; set; }
            public string strState { get; set; }
            public string strCountry { get; set; }
            public string strPostalCode { get; set; }
            public string strLogoPath { get; set; }
            public string strCompanyStatus { get; set; }
            //public string strCompanyLaunchDate { get; set; }

            //public string strContactPersonMail { get; set; }

            public string strUserName { get; set; }
            public string strPassword { get; set; }
            public string strEno { get; set; }
            public string strEPF { get; set; }
            public string strCno { get; set; }
            public string strSocgo { get; set; }
            public string strZakarNo { get; set; }
            public string strFlag { get; set; }
        };
        #endregion

        #region Login
        public class LoginReq
        {
            public string strUserName { get; set; }
            public string strPassword { get; set; }
            public string strTerminalType { get; set; }


        };
        #endregion

        #region Department
        public class DepartmentReq
        {
            public string strDepartment { get; set; }
            public string strDepartmentID { get; set; }
            public string strDesignationID { get; set; }
            public string strFlag { get; set; }
            public string strRemark { get; set; }


        };
        #endregion
        #region Designation
        public class DesigantionReq
        {
            public string strDesignation { get; set; }
            public string strDesignationID { get; set; }
            public string strDepartmentID { get; set; }
            public string strFlag { get; set; }
            public string strRemark { get; set; }


        };
        #endregion

        #region EmployeeReq
        public class EmployeeReq
        {
            public string strEmpID { get; set; }
            public string strEmpCompanyId { get; set; }
            public string strEmpPayrollId { get; set; }
            public string strEmpTittle { get; set; }
            public string strEmpName { get; set; }
            public string strEmpPhoneNo { get; set; }
            public string EmpMobileNo { get; set; }
            public string strEmpEmailId { get; set; }
            public string strEmpPosition { get; set; }
            public string strEmpIdProof { get; set; }
            public string strEmpIdProofNo { get; set; }
            public string strEmpDOB { get; set; }
            public string strEmpGender { get; set; }
            public string strEmpMaritalStatus { get; set; }
            public string strEmpPerAddress { get; set; }
            public string strEmpTempAddress { get; set; }
            public string strEmpCity { get; set; }
            public string strEmpSTATE { get; set; }
            public string strEmpCOUNTRY { get; set; }
            public string strEmpPostalCode { get; set; }
            public string strEmpResidenceStatus { get; set; }
            public string strEmpWorkerStatus { get; set; }
            public string strEmpNationality { get; set; }
            public int intEmpChildren { get; set; }
            public string strEmpDisabilityStatus { get; set; }
            public string strEmpRace { get; set; }
            public string strEmpPerasonalEmailId { get; set; }
            public string strEmpEmerContactPerson { get; set; }
            public string strEmpEmerContactPhNo { get; set; }
            public string strEmpPaymentMethod { get; set; }
            public string strEmpBankAccountNo { get; set; }
            public string strEmpBankName { get; set; }
            public string strEmpBankBranch { get; set; }
            public string strEmpIFSCCode { get; set; }
            public string strEmpJoinDate { get; set; }
            public string strEmpPayrollType { get; set; }
            public string strEmpPassportNo { get; set; }
            public string strEmpPassportExpiryDate { get; set; }
            public int intEmp_Basic_Sal { get; set; }
            public int intEmpHRA { get; set; }
            public int intEmpEMPPF { get; set; }

            public int intEmpAllowance { get; set; }

            public int intEmpERPF { get; set; }
            public int intEmpEmpSOCSO { get; set; }
            public int intEmpERSOCSO { get; set; }
            public int intEmpEmpEIS { get; set; }
            public int intEmpEREIS { get; set; }
            public int intEmpEMPPFPer { get; set; }
            public int intEmpSOCSOPer { get; set; }
            public int intEmpERSOCSOPer { get; set; }
            public int intEmpEISPer { get; set; }
            public int intEREISPer { get; set; }
            public int intSOCSOCategory { get; set; }
            public int intEmpPCBNo { get; set; }
            public string strEmpUsername { get; set; }
            public string strUsername { get; set; }
            public int intLVECasualLeave { get; set; }
            public int intLVEPersonalLeave { get; set; }
            public int intLVEAnnualLeave { get; set; }
            public int intLVEMarriageLeave { get; set; }
            public int intLVEMedicalLeave { get; set; }
            public int intLVEHospitalLeave { get; set; }
            public int intLVECompassionateLeave { get; set; }
            public string strFlag { get; set; }

        }
        #endregion

        #region Leave Request
        public class LeaveReq
        {
            public string strEmpID { get; set; }
            public string strCommpanyID { get; set; }
            public string strPayrollID { get; set; }

            public string strLeaveType { get; set; }

            public string strLeaveFrom { get; set; }

            public string strLeaveTO { get; set; }

            public string strLeaveDays { get; set; }
            public string strReason { get; set; }
            public string strEmergencyCOnt { get; set; }
            public string strWillingtoWork { get; set; }
            public string struserName { get; set; }
            public string strStatus { get; set; }
            public string strFlag { get; set; }
        }
        #endregion


        #region Resopnse
        public class ResponseData
        {
            public string strStatus { get; set; }
            public string strResponse { get; set; }
            public string strErrorMessage { get; set; }

        };
        #endregion

    }
}