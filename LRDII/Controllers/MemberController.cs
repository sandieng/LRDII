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
    public class MemberController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private IMemberService _memberService;

        public MemberController(IHostingEnvironment environment, IMemberService memberService)
        {
            _hostingEnvironment = environment;
            _memberService = memberService;
        }

        // GET: Member
        public IActionResult Index(int? page)
        {
            if (page.HasValue)
            {
                // Hack it to get the proper page (RefelectIT.Paging only calls to Index for page navigation, it is a bug)
                return RedirectToAction("List", "Member", new { page = page });
            }

            return View();
        }

        // GET: Member/List
        [HttpGet]
        public IActionResult List(int page = 1)
        {
            //return View(await _context.Members.ToListAsync());
            //var query = _context.Members.AsNoTracking().OrderBy(m => m.NamaLengkap);
            var query = _memberService.GetMemberPage();
            var memberList = PagingList.Create(query, 10, page);

            return View(memberList);
        }     

        // GET: Member/Details/5
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anggota = _memberService.GetById(id);
            if (anggota == null) return NotFound();

            return View(anggota);
        }

        // GET: Member/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NomorAnggota,NamaLengkap,AlamatLengkap,NomorHp,Email,JenisAnggota,IsActive")] MemberModel member)
        {
            if (ModelState.IsValid)
            {
                _memberService.Save(member);
                return RedirectToAction(nameof(ClearForm));
            }

            return View(member);
        }

        // GET: Member/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var member = _memberService.GetById(id);
            if (member == null) return NotFound();
         
            return View(member);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("NomorAnggota,NamaLengkap,AlamatLengkap,NomorHp,Email,JenisAnggota,IsActive")] MemberModel member)
        {
            if (id != member.NomorAnggota) return NotFound();
          
            if (ModelState.IsValid)
            {
                try
                {
                    _memberService.Update(member);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_memberService.GetById(member.NomorAnggota) == null) return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(List));
            }

            return View(member);
        }

        [HttpGet]
        public IActionResult EditMember()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMember([Bind("NomorAnggota")] MemberModel member)
        {
            var anggota = _memberService.GetById(member.NomorAnggota);
            if (anggota == null)
            {
                //return RedirectToAction("EditMember", "Member");
                ModelState.AddModelError("NomorAnggota", "Nomor anggota tidak ada");
                return View("EditMember");
            }

            return RedirectToAction("Edit", new { id = anggota.NomorAnggota });
//            return View("Edit", anggota);
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchMember([Bind("NomorAnggota")] MemberModel member)
        {
            var anggota = _memberService.GetById(member.NomorAnggota);
            if (anggota == null)
            {
                //return RedirectToAction("Search", "Member");
                ModelState.AddModelError("NomorAnggota", "Nomor anggota tidak ada");
                return View("Search");
            }

            return View("Details", anggota);
        }

        // GET: Member/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var anggota = _memberService.GetById(id);
            if (anggota == null) return NotFound();
         
            return View(anggota);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var anggota = _memberService.GetById(id);
            _memberService.Delete(anggota);
            return RedirectToAction(nameof(List));
        }

        public IActionResult ClearForm()
        {
            return RedirectToAction(nameof(MemberController.Create), "Member");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Export(List<MemberModel> memberList)
        {
            var fileName = $"{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}_{DateTime.Now.Year.ToString()}.xlsx";

            // Download location from the browser
            string url = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, $"DaftarAnggota_{fileName}");

            var result = ExportToExcel.Download<MemberModel>(_hostingEnvironment.WebRootPath, memberList, $"DaftarAnggota_{fileName}");        
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
        public IActionResult GenerateReport()
        {
            var fileName = $"{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}_{DateTime.Now.Year.ToString()}.xlsx";
            var memberList = _memberService.GetMemberPage().ToList();

            // Download location from the browser
            string url = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, $"DaftarAnggota_{fileName}");

            var result = ExportToExcel.Download<MemberModel>(_hostingEnvironment.WebRootPath, memberList, $"DaftarAnggota_{fileName}");

            ViewBag.Report = "Download laporan";
            ViewBag.Download = url;
            return View("Report");
        }
    }
}
