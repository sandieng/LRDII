using LRDII.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LRDII.Services
{
    public interface ILoanTransactionService
    {
        LoanTransactionModel GetById(int? id);
        void Save(LoanTransactionModel loanTransaction);
        void Update(LoanTransactionModel loanTransaction);
        void Delete(LoanTransactionModel loanTransaction);
        IOrderedQueryable<LoanTransactionModel> GetLoanTransactionPage();
        List<LoanReportViewModel> GenerateLoanReport();
        SelectList GetLoanList(int id);
        //   List<ShareholderReportViewModel> GenerateLoanReport();
    }
}
