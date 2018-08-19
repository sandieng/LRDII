using LRDII.Infrastructure;
using LRDII.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LRDII.Services
{
    [Route("api/[controller]")]
    public class LoanTransactionServiceController : Controller, ILoanTransactionService
    {
        private readonly LrdiiDbContext _context;
        private readonly ILrdiiRepository<LoanTransactionModel> _repository;

        public LoanTransactionServiceController(LrdiiDbContext context, ILrdiiRepository<LoanTransactionModel> repository)
        {
            _context = context;
            _repository = repository;
        }

        public void Delete(LoanTransactionModel loanTransaction)
        {
            _repository.Delete(loanTransaction);
        }

        public List<LoanReportViewModel> GenerateLoanReport()
        {
            var loanList = from LoanTransactions in _context.LoanTransactions
                           join Members in _context.Members on LoanTransactions.NomorAnggota equals Members.NomorAnggota
                           orderby LoanTransactions.NomorAnggota, LoanTransactions.NomorPinjaman
                           select new LoanReportViewModel
                           {
                               NomorPinjaman = LoanTransactions.NomorPinjaman,
                               NomorAnggota = LoanTransactions.NomorAnggota,
                               NamaLengkap = Members.NamaLengkap,
                               TanggalTransaksi = LoanTransactions.TanggalTransaksi,
                               JumlahPinjaman = LoanTransactions.JumlahPinjaman,
                               LamaPinjaman = LoanTransactions.LamaPinjaman,
                               PersentaseBunga = LoanTransactions.PersentaseBunga
                           };

            return loanList.ToList();
        }

        public LoanTransactionModel GetById(int? id)
        {
            return _context.LoanTransactions.SingleOrDefault(l => l.NomorPinjaman == id);
        }

        public SelectList GetLoanList(int id)
        {
            var memberLoans = _context.LoanTransactions.Where(l => l.NomorAnggota == id).Select(x => new { Id = x.NomorPinjaman, Value = x.JumlahPinjaman });
            var loanList = new SelectList(memberLoans, "Id", "Value");

            return loanList;
        }

        public IOrderedQueryable<LoanTransactionModel> GetLoanTransactionPage()
        {
            return _repository.GetAll().OrderBy(m => m.TanggalTransaksi);
        }

        public void Save(LoanTransactionModel shareTransaction)
        {
            _repository.Save(shareTransaction);
        }
    
        public void Update(LoanTransactionModel shareTransaction)
        {
            _repository.Update(shareTransaction);
        }
    }
}
