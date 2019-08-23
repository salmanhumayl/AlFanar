using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Purchase_Requisition
{
    public class PRService : IPR
    {
        private PurchadeRequisitionRepository IRepository;

        public List<PRDetail> GetItemDetail(int RecordID)
        {
            IRepository = new PurchadeRequisitionRepository();
            var obj = IRepository.GetItemDetail(RecordID);
            return obj;
        }

        public List<PROutstandingListMainModel> GetOutstandingPR()
        {
            IRepository = new PurchadeRequisitionRepository();
            var obj = IRepository.GetOutstandingPR();
            return obj;
        }

        public List<PROutstandingListMainModel> GetPRNotSubmitted()
        {
            IRepository = new PurchadeRequisitionRepository();
            var obj = IRepository.GetPRNotSubmitted();
            return obj;
        }


        public List<PROutstandingListMainModel> GetPRSubmitted()
        {
            IRepository = new PurchadeRequisitionRepository();
            var obj = IRepository.GetPRSubmitted();
            return obj;
        }

        public String NewPurchaseReq(List<Core.Domain.Purchase_Requisition.PurchaseRequisitionDetail> PurchaseItem, Core.Domain.Purchase_Requisition.PurchaseRequisition PurchaseHeader)
        {
            IRepository = new PurchadeRequisitionRepository();
            return IRepository.NewPurchaseReq(PurchaseItem, PurchaseHeader);
        }

        public String EditPurchaseReq(List<Core.Domain.Purchase_Requisition.PurchaseRequisitionDetail> PurchaseItem, Core.Domain.Purchase_Requisition.PurchaseRequisition PurchaseHeader)
        {
            IRepository = new PurchadeRequisitionRepository();
            return IRepository.EditPurchaseReq(PurchaseItem, PurchaseHeader);
        }

        public bool SubmitApproval(int nPRID)
        {
            IRepository = new PurchadeRequisitionRepository();
            return IRepository.SubmitApproval(nPRID);
        }

        public bool ApprovedPR(int nPRID)
        {
            IRepository = new PurchadeRequisitionRepository();
            return IRepository.ApprovedPR(nPRID);
        }

        public List<EditPurchaseReqDetail> GetPurchaseReqDetail(int id)
        {
            IRepository = new PurchadeRequisitionRepository();
            return IRepository.GetPurchaseReqDetail(id);
        }

        public EditPurchaseReqHeader GetPurchaseReqHeader(int id)
        {
            IRepository = new PurchadeRequisitionRepository();
            return IRepository.GetPurchaseReqHeader(id);
        }

        public List<ReqitemCategory> GetPurchaseReqCategory(int id)
        {
            IRepository = new PurchadeRequisitionRepository();
            return IRepository.GetPurchaseReqCategory(id);
        }
    }
}
