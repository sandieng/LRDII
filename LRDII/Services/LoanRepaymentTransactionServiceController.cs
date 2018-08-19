using LRDII.Infrastructure;
using LRDII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LRDII.Services
{
    [Route("api/[controller]")]
    public class LoanRepaymentTransactionServiceController : Controller, ILoanRepaymentTransactionService
    {
        private readonly LrdiiDbContext _context;
        private readonly ILrdiiRepository<LoanRepaymentTransactionModel> _repository;

        public LoanRepaymentTransactionServiceController(LrdiiDbContext context, ILrdiiRepository<LoanRepaymentTransactionModel> repository)
        {
            _context = context;
            _repository = repository;
        }

        public void Delete(LoanRepaymentTransactionModel loanRepaymentTransaction)
        {
            _repository.Delete(loanRepaymentTransaction);
        }

        public List<LoanRepaymentReportViewModel> GenerateLoanRepaymentReport()
        {
            var loanRepaymentList = from LoanRepaymentTransactions in _context.LoanRepaymentTransactions
                                    join LoanTransactions in _context.LoanTransactions on LoanRepaymentTransactions.NomorPinjaman equals LoanTransactions.NomorPinjaman
                                    join Members in _context.Members on LoanRepaymentTransactions.NomorPinjaman equals Members.NomorAnggota
                                    orderby LoanRepaymentTransactions.NomorAnggota, LoanRepaymentTransactions.NomorPinjaman, LoanRepaymentTransactions.NomorPembayaranPinjaman
                                    select new LoanRepaymentReportViewModel
                                    {
                                        NomorPinjaman = LoanRepaymentTransactions.NomorPinjaman,
                                        NomorPembayaranPinjaman = LoanRepaymentTransactions.NomorPembayaranPinjaman,
                                        NomorAnggota = LoanRepaymentTransactions.NomorAnggota,
                                        NamaLengkap = Members.NamaLengkap,
                                        JumlahPinjaman = LoanTransactions.JumlahPinjaman,
                                        JumlahPinjamanPokok = LoanRepaymentTransactions.JumlahPinjamanPokok,
                                        JumlahBungaPinjaman = LoanRepaymentTransactions.JumlahBungaPinjaman,
                                        TanggalTransaksi = LoanRepaymentTransactions.TanggalTransaksi
                                    };

            return loanRepaymentList.ToList();
        }

        public LoanRepaymentTransactionModel GetById(int? id)
        {
            return _context.LoanRepaymentTransactions.SingleOrDefault(l => l.NomorPembayaranPinjaman == id);
        }

        public IOrderedQueryable<LoanRepaymentTransactionModel> GetLoanRepaymentTransactionPage()
        {
            return _repository.GetAll().OrderBy(m => m.TanggalTransaksi);
        }

        public void Save(LoanRepaymentTransactionModel loanRepaymentTransaction)
        {
            _repository.Save(loanRepaymentTransaction);
        }

        public void Update(LoanRepaymentTransactionModel loanRepaymentTransaction)
        {
            _repository.Update(loanRepaymentTransaction);
        }
    }
}
