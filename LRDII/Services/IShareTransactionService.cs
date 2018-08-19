using LRDII.Models;
using System.Collections.Generic;
using System.Linq;

namespace LRDII.Services
{
    public interface IShareTransactionService
    {
        ShareTransactionModel GetById(int? id);
        void Save(ShareTransactionModel shareTransaction);
        void Update(ShareTransactionModel shareTransaction);
        void Delete(ShareTransactionModel shareTransaction);
        IOrderedQueryable<ShareTransactionModel> GetShareTransactionPage();
        List<ShareholderReportViewModel> GenerateShareholderReport();
    }
}
