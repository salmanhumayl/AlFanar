using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Purchase_Requisition
{
    public interface IPR
    {
        List<PROutstandingListMainModel> GetOutstandingPR(); //Approved Where Status=1

        List<PROutstandingListMainModel> GetPRNotSubmitted(); // Not Submitted Where Status=0

        List<PROutstandingListMainModel> GetPRSubmitted(); // Submitted Where Status=3


        List<EditPurchaseReqDetail> GetPurchaseReqDetail(int id);
        EditPurchaseReqHeader GetPurchaseReqHeader(int id);

        List<ReqitemCategory> GetPurchaseReqCategory(int id);

        List<PRDetail> GetItemDetail(int RecordID);

        bool SubmitApproval(int nPRID);
        bool ApprovedPR(int nPRID);
        






    }
}
