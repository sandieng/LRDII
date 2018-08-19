using LRDII.Infrastructure;
using LRDII.Models;
using LRDII.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System;

namespace LRDII.Controllers
{
    public class LoanController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private IShareTransactionService _shareTransactionService;
        private IShareService _shareService;
        private IMemberService _memberService;
        private ILoanTransactionService _loanService;
        private ILoanRepaymentTransactionService _loanRepaymentService;

        public LoanController(IHostingEnvironment environment, 
            IShareTransactionService transactionService, IShareService shareService, 
            IMemberService memberService, ILoanTransactionService loanService, ILoanRepaymentTransactionService loanRepaymentService)
        {
            _hostingEnvironment = environment;
            _shareTransactionService = transactionService;
            _shareService = shareService;
            _memberService = memberService;
            _loanService = loanService;
            _loanRepaymentService = loanRepaymentService;
        }

        // GET: Loan
        public IActionResult Index(int? page)
        {
            if (page.HasValue)
            {
                // Hack it to get the proper page (RefelectIT.Paging only calls to Index for page navigation, it is a bug)
                return RedirectToAction("List", "Transaction", new { page = page });
            }

            return View();
        }

        // GET: Loan/List
        [HttpGet]
        public IActionResult List(int page = 1)
        {
            var query = _shareTransactionService.GetShareTransactionPage();
            var shareList = PagingList.Create(query, 10, page);

            return View(shareList);
        }

        // GET: Loan/TakeRepayLoan
        [HttpGet]
        public IActionResult TakeRepayLoan()
        {
            // Member list
            var viewModel = new LoanTransactionViewModel();
            viewModel.DaftarAnggota = _memberService.GetMembers();

            return View(viewModel);
        }

        // POST: Loan/TakeRepayLoan
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TakeRepayLoan(LoanTransactionViewModel transactionVM)
        {
            dynamic transaction = null;

            if (transactionVM.NomorAnggota <= 0) return View(transactionVM);

            if (transactionVM.JenisTransaksi == LoanTransactionType.PinjamanUang)
            {
                if (transactionVM.PersentaseBunga < 0 || transactionVM.JumlahPinjaman <= 0) return View(transactionVM);
                transaction = ViewModelMapper.MapViewModelToModel(transactionVM, new LoanTransactionModel());
            }
            else
            {
                if (transactionVM.JumlahPinjamanPokok <= 0 || transactionVM.JumlahBungaPinjaman <= 0 || transactionVM.NomorPinjaman <= 0) return View(transactionVM);

                // Validate the loan belongs to the member
                var loanExists = _loanService.GetById(transactionVM.NomorPinjaman);
                if (loanExists == null)
                {
                    ModelState.AddModelError("", "Nomor pinjaman tidak ada untuk nomor anggota ini.");

                    // Member list
                    transactionVM.DaftarAnggota = _memberService.GetMembers();
                    return View(transactionVM);
                    //return RedirectToAction("TakeRepayLoan", transactionVM);
                }

                transaction = ViewModelMapper.MapViewModelToModel(transactionVM, new LoanRepaymentTransactionModel());
            }

            if (transactionVM.JenisTransaksi == LoanTransactionType.PinjamanUang)
                _loanService.Save(transaction);
            else
                _loanRepaymentService.Save(transaction);

            return RedirectToAction(nameof(ClearForm), new { ActionName = "TakeRepayLoan" });
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("EditLoan");

            var loanTransaction = _loanService.GetById(id);
            if (loanTransaction == null)
            {
                ModelState.AddModelError("NomorPinjaman", "Nomor pinjaman tidak ada.");
                return RedirectToAction("EditLoan");
            }

            return View(loanTransaction);
        }

        [HttpPost]
        public IActionResult Edit(LoanTransactionModel loanTransaction)
        {
            if (!ModelState.IsValid)
            {
                return View(loanTransaction);
            }

            _loanService.Update(loanTransaction);

            return RedirectToAction("EditLoan");
        }

        [HttpGet]
        public IActionResult EditLoan()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditLoan(LoanTransactionViewModel loanTransaction)
        {
            return RedirectToAction("Edit", new { id = loanTransaction.NomorPinjaman });
        }

        [HttpGet]
        public IActionResult EditR(int? id)
        {
            if (id == null) return RedirectToAction("EditRepayment");

            var loanRepaymentTransaction = _loanRepaymentService.GetById(id);
            if (loanRepaymentTransaction == null)
            {
                ModelState.AddModelError("NomorPinjaman", "Nomor pinjaman tidak ada.");
                return RedirectToAction("EditRepayment");
            }

            return View(loanRepaymentTransaction);
        }

        [HttpPost]
        public IActionResult EditR(LoanRepaymentTransactionModel loanRepaymentTransaction)
        {
            if (!ModelState.IsValid)
            {
                return View(loanRepaymentTransaction);
            }

            _loanRepaymentService.Update(loanRepaymentTransaction);

            return RedirectToAction("EditRepayment");
        }

        [HttpGet]
        public IActionResult EditRepayment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditRepayment(LoanRepaymentTransactionModel loanRepaymentTransaction)
        {
            return RedirectToAction("EditR", new { id = loanRepaymentTransaction.NomorPembayaranPinjaman });
        }

        [HttpGet]
        public IActionResult Report()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerateReport([Bind("ReportType")] ReportViewModel report)
        {
            var fileName = $"{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}_{DateTime.Now.Year.ToString()}.xlsx";
            string url = "";
            bool result = false;

            switch (report.ReportType)
            {          
                case LoanReportType.LoanReport:
                    url = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, $"LaporanPinjamanUang_{fileName}");
                    var loanReport = _loanService.GenerateLoanReport();
                    result = ExportToExcel.Download<LoanReportViewModel>(_hostingEnvironment.WebRootPath, loanReport, $"LaporanPinjamanUang_{fileName}");
                    break;

                case LoanReportType.LoanRepaymentReport:
                    url = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, $"LaporanPengembalianPinjamanUang_{fileName}");
                    var loanRepaymentReport = _loanRepaymentService.GenerateLoanRepaymentReport();
                    result = ExportToExcel.Download<LoanRepaymentReportViewModel>(_hostingEnvironment.WebRootPath, loanRepaymentReport, $"LaporanPengembalianPinjamanUang_{fileName}");
                    break;
            }

            ViewBag.Report = "Download laporan";
            ViewBag.Download = url;
            return View("Report");
        }

        public IActionResult ClearForm(string actionName)
        {
            //return RedirectToAction(nameof(TransactionController.BuySellShare), "Transaction");
            if (string.IsNullOrEmpty(actionName))
                return RedirectToAction(nameof(LoanController.TakeRepayLoan), "Loan");
            else
                return RedirectToAction(actionName, "Loan");
        }
    }
}