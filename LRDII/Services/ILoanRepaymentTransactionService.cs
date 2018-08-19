using LRDII.Models;
using System.Collections.Generic;
using System.Linq;

namespace LRDII.Services
{
    public interface ILoanRepaymentTransactionService
    {
        LoanRepaymentTransactionModel GetById(int? id);
        void Save(LoanRepaymentTransactionModel loanRepayment);
        void Update(LoanRepaymentTransactionModel loanRepayment);
        void Delete(LoanRepaymentTransactionModel loanRepayment);
        IOrderedQueryable<LoanRepaymentTransactionModel> GetLoanRepaymentTransactionPage();
        List<LoanRepaymentReportViewModel> GenerateLoanRepaymentReport();
    }
}
