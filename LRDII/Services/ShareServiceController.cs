using LRDII.Infrastructure;
using LRDII.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LRDII.Services
{
    [Route("api/[controller]")]
    public class ShareServiceController : Controller, IShareService
    {
        private readonly LrdiiDbContext _context;
        private readonly ILrdiiRepository<SharePriceModel> _repository;

        public ShareServiceController(LrdiiDbContext context, ILrdiiRepository<SharePriceModel> repository)
        {
            _context = context;
            _repository = repository;
        }
     
        public int GetShareholdingByMemberId(int id)
        {
            var shareholding = _context.ShareTransactions.Where(sh => sh.NomorAnggota == id).Sum(s => s.JumlahSaham);

            return shareholding;
        }

        public SelectList GetSharePriceList()
        {
            var sharePriceList = _repository.GetAll().OrderBy(s => s.NomorHargaSaham).Select(x => new { Id = x.NomorHargaSaham, Value = $"{x.HargaSaham} {" | "} {x.TanggalHarga}" });
//            var sharePriceList = _context.SharePrices.OrderBy(s => s.NomorHargaSaham).Select(x => new { Id = x.NomorHargaSaham, Value = x.HargaSaham });
            return new SelectList(sharePriceList, "Id", "Value");
        }

        public IOrderedQueryable<SharePriceModel> GetSharePricePage()
        {
            return _repository.GetAll().OrderBy(m => m.TanggalHarga);
            //return _context.SharePrices.AsNoTracking().OrderBy(m => m.TanggalHarga);
        }

        public void Save(SharePriceModel sharePrice)
        {
            _repository.Save(sharePrice);
        }

        public SharePriceModel GetById(int? id)
        {
            return _context.SharePrices.SingleOrDefault(sp => sp.NomorHargaSaham == id);
            //return _repository.GetById(id);
        }

        public void Update(SharePriceModel sharePrice)
        {
            _repository.Update(sharePrice);
        }

        public void Delete(SharePriceModel sharePrice)
        {
            _repository.Delete(sharePrice);
        }
    }
}
