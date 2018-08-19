using LRDII.Infrastructure;
using LRDII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LRDII.Services
{
    [Route("api/[controller]")]
    public class ShareTransactionServiceController : Controller, IShareTransactionService
    {
        private readonly LrdiiDbContext _context;
        private readonly ILrdiiRepository<ShareTransactionModel> _repository;

        public ShareTransactionServiceController(LrdiiDbContext context, ILrdiiRepository<ShareTransactionModel> repository)
        {
            _context = context;
            _repository = repository;
        }

        public void Delete(ShareTransactionModel shareTransaction)
        {
            _repository.Delete(shareTransaction);
        }

        public List<ShareholderReportViewModel> GenerateShareholderReport()
        {
            var shareholderList = from ShareTransactions in _context.ShareTransactions
                                  join Members in _context.Members on ShareTransactions.NomorAnggota equals Members.NomorAnggota
                                  join SharePrices in _context.SharePrices on ShareTransactions.NomorHargaSaham equals SharePrices.NomorHargaSaham
                                  orderby ShareTransactions.NomorAnggota, ShareTransactions.TanggalTransaksi, ShareTransactions.NomorTransaksi
                                  select new ShareholderReportViewModel
                                  {
                                      NomorAnggota = Members.NomorAnggota,
                                      NamaLengkap = Members.NamaLengkap,
                                      TanggalTransaksi = ShareTransactions.TanggalTransaksi,
                                      JenisTransaksi = ShareTransactions.JenisTransaksi,
                                      JumlahSaham = ShareTransactions.JumlahSaham,
                                      HargaSaham = SharePrices.HargaSaham,
                                      TotalSaham = 0
                                  };

            // Calculate share holding
            var shareholderWithHoldingList = shareholderList;
            var previousShareholder = new ShareholderReportViewModel();
            var finalShareholderList = new List<ShareholderReportViewModel>();
            foreach (var share in shareholderList)
            {
                var targetShareholder = shareholderWithHoldingList.FirstOrDefault(sh => sh.NomorAnggota == share.NomorAnggota
                        && sh.TanggalTransaksi == share.TanggalTransaksi
                        && sh.JenisTransaksi == share.JenisTransaksi
                        && sh.JumlahSaham == share.JumlahSaham);

                if (targetShareholder != null)
                {

                    if (targetShareholder.NomorAnggota == previousShareholder.NomorAnggota)
                    {
                        targetShareholder.TotalSaham += share.JumlahSaham + previousShareholder.TotalSaham;
                        targetShareholder.TotalNilaiSaham += (share.JumlahSaham * share.HargaSaham) + previousShareholder.TotalNilaiSaham;
                    }
                    else
                    {
                        targetShareholder.TotalSaham += share.JumlahSaham;
                        targetShareholder.TotalNilaiSaham += (share.JumlahSaham * share.HargaSaham);
                    }

                    previousShareholder = targetShareholder;

                    finalShareholderList.Add(targetShareholder);
                }
            }

            return finalShareholderList.ToList();
        }

        public ShareTransactionModel GetById(int? id)
        {
            return _context.ShareTransactions.SingleOrDefault(m => m.NomorTransaksi == id);
        }

        public IOrderedQueryable<ShareTransactionModel> GetShareTransactionPage()
        {
            return _repository.GetAll().OrderBy(m => m.TanggalTransaksi);
        }

        public void Save(ShareTransactionModel shareTransaction)
        {
            _repository.Save(shareTransaction);
        }

        public void Update(ShareTransactionModel shareTransaction)
        {
            _repository.Update(shareTransaction);
        }
    }
}
