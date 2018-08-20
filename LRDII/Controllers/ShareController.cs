using LRDII.Infrastructure;
using LRDII.Models;
using LRDII.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LRDII.Controllers
{
    public class ShareController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private IShareService _shareService;
        private IShareTransactionService _shareTransactionService;
        private IMemberService _memberService;

        public ShareController(IHostingEnvironment environment, IShareService shareService,
            IShareTransactionService shareTransactionService, IMemberService memberService)
        {
            _hostingEnvironment = environment;
            _shareService = shareService;
            _shareTransactionService = shareTransactionService;
            _memberService = memberService;
        }

        // GET: Share
        public IActionResult Index(int? page)
        {
            if (page.HasValue)
            {
                // Hack it to get the proper page (RefelectIT.Paging only calls to Index for page navigation, it is a bug)
                return RedirectToAction("List", "HargaSaham", new { page = page });
            }

            return View();
        }

        // GET: Share/List
        [HttpGet]
        public IActionResult List(int page = 1)
        {
            //return View(await _context.Members.ToListAsync());
            //var query2 = _context.SharePrices.AsNoTracking().OrderBy(m => m.TanggalHarga);
            var query = _shareService.GetSharePricePage();
            var shareList = PagingList.Create(query, 10, page);

            return View(shareList);
        }

        // GET: Share/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: HargaSaham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NomorHargaSaham,TanggalHarga,HargaSaham")] SharePriceModel sharePrice)
        {
            if (ModelState.IsValid)
            {
                _shareService.Save(sharePrice);
                return RedirectToAction(nameof(ClearForm), new { actionName = "Create" } );
            }

            return View(sharePrice);
        }

        // GET: Share/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var sharePrice = _shareService.GetById(id);
            if (sharePrice == null) return NotFound();

            return View(sharePrice);
        }

        // POST: Share/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("NomorHargaSaham,TanggalHarga,HargaSaham")] SharePriceModel sharePrice)
        {
            if (id != sharePrice.NomorHargaSaham) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _shareService.Update(sharePrice);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_shareService.GetById(sharePrice.NomorHargaSaham) == null) return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(List));
            }

            return View(sharePrice);
        }


        [HttpGet]
        public IActionResult EditShare()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditShare([Bind("NomorHargaSaham")] SharePriceModel sharePrice)
        {
            var share = _shareService.GetById(sharePrice.NomorHargaSaham);
            if (share == null)
            {
                //return RedirectToAction("EditMember", "Member");
                ModelState.AddModelError("NomorHargaSaham", "Nomor harga saham tidak ada");
                return View("EditShare");
            }

            return RedirectToAction("Edit", new { id = sharePrice.NomorHargaSaham });
            //            return View("Edit", anggota);
        }

        // GET: Share/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var share = _shareService.GetById(id);
            if (share == null) return NotFound();

            return View(share);
        }

        // POST: Share/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var share = _shareService.GetById(id);
            _shareService.Delete(share);
            return RedirectToAction(nameof(List));
        }

        // GET: Share/BuySell
        [HttpGet]
        public IActionResult BuySell()
        {
            var viewModel = new ShareTransactionViewModel();

            // Member list
            viewModel.DaftarAnggota = _memberService.GetMembers();

            // Share price list
            viewModel.DaftarHargaSaham = _shareService.GetSharePriceList();

            return View(viewModel);
        }

        // POST: Share/BuySell
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BuySell(ShareTransactionViewModel transactionVM)
        {
            if (transactionVM.NomorAnggota == 0 || transactionVM.NomorHargaSaham == 0) return View(transactionVM);
            var transaction = new ShareTransactionModel();
            if (ModelState.IsValid)
            {
                var memberHolding = _shareService.GetShareholdingByMemberId(transactionVM.NomorAnggota);
                if (transactionVM.JenisTransaksi == ShareTransactionType.JualSaham)
                {
                    if (memberHolding < transactionVM.JumlahSaham)
                    {
                        ModelState.AddModelError("JumlahSaham", "Anggota tidak memiliki jumlah saham yang memadai untuk penjualan");
                        transactionVM.DaftarAnggota = _memberService.GetMembers();
                        transactionVM.DaftarHargaSaham = _shareService.GetSharePriceList();
                        return View(transactionVM);
                    }
                }

                transaction = ViewModelMapper.MapViewModelToModel(transactionVM, transaction);

                _shareTransactionService.Save(transaction);
                return RedirectToAction(nameof(ClearForm), new { ActionName = "BuySell" });
            }

            return View(transactionVM);
        }

        [HttpGet]
        public IActionResult EditBuyShare(int? id)
        {
            if (id == null) return NotFound();

            var share = _shareTransactionService.GetById(id);
            if (share == null) return NotFound();

            var shareTransaction = ViewModelMapper.MapViewModelToModel(share, new EditShareTransactionViewModel());
            shareTransaction.DaftarAnggota = _memberService.GetMembers();
            shareTransaction.DaftarHarga = _shareService.GetSharePriceList();
            return View(shareTransaction);
        }

        [HttpPost]
        public IActionResult EditBuyShare(EditShareTransactionViewModel editShareTransaction)
        {
            if (!ModelState.IsValid) return View(editShareTransaction);

            if (editShareTransaction.JumlahSaham <= 0)
            {
                ModelState.AddModelError("JumlahSaham", "Jumlah saham tidak boleh < 0");

                editShareTransaction.DaftarAnggota = _memberService.GetMembers();
                editShareTransaction.DaftarHarga = _shareService.GetSharePriceList();
                return View(editShareTransaction);
            }

            var updatedShareTransaction = ViewModelMapper.MapViewModelToModel(editShareTransaction, new ShareTransactionModel());
            _shareTransactionService.Update(updatedShareTransaction);

            return RedirectToAction("EditBuy");
        }

        [HttpGet]
        public IActionResult EditBuy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditBuy(ShareTransactionModel shareTransaction)
        {
            var transaction = _shareTransactionService.GetById(shareTransaction.NomorTransaksi);

            if (transaction == null || transaction.JenisTransaksi == ShareTransactionType.JualSaham)
            {
                ModelState.AddModelError("NomorTransaksi", "Nomor transaksi pembelian saham tidak ada");
                return View(shareTransaction);
            }

            return RedirectToAction("EditBuyShare", new { id = transaction.NomorTransaksi });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Export(List<SharePriceModel> shareList)
        {
            var fileName = $"{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}_{DateTime.Now.Year.ToString()}.xlsx";

            // Download location from the browser
            string url = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, $"DaftarHargaSaham{fileName}");

            var result = ExportToExcel.Download<SharePriceModel>(_hostingEnvironment.WebRootPath, shareList, $"DaftarHargaSaham{fileName}");
            TempData["DownLoad"] = url;

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Report()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerateReport([Bind("ReportType")] ShareReportViewModel report)
        {
            var fileName = $"{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}_{DateTime.Now.Year.ToString()}.xlsx";
            var url = "";
            var result = false;

            switch (report.ReportType)
            {
                case ShareReportType.SharePriceReport:
                    url = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, $"DaftarHargaSaham_{fileName}");
                    var shareList = _shareService.GetSharePricePage().ToList();
                    result = ExportToExcel.Download<SharePriceModel>(_hostingEnvironment.WebRootPath, shareList, $"DaftarHargaSaham_{fileName}");
                    break;

                case ShareReportType.ShareholderReport:
                    url = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, $"LaporanPemegangSaham_{fileName}");
                    var shareholderReport = _shareTransactionService.GenerateShareholderReport();
                    result = ExportToExcel.Download<ShareholderReportViewModel>(_hostingEnvironment.WebRootPath, shareholderReport, $"LaporanPemegangSaham_{fileName}");
                    break;
            }

            ViewBag.Report = "Download laporan";
            ViewBag.Download = url;
            return View("Report");
        }

        public IActionResult ClearForm(string actionName)
        {
            if (string.IsNullOrEmpty(actionName))
                return RedirectToAction(nameof(ShareController.BuySell), "Share");
            else
                return RedirectToAction(actionName, "Share");
        }

    }
}