using LRDII.Infrastructure;
using LRDII.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LRDII.Services
{
    [Route("api/[controller]")]
    public class MemberServiceController : Controller, IMemberService
    {
        private readonly LrdiiDbContext _context;
        private readonly ILrdiiRepository<MemberModel> _repository;

        public MemberServiceController(LrdiiDbContext context, ILrdiiRepository<MemberModel> repository)
        {
            _context = context;
            _repository = repository;
        }

        public void Delete(MemberModel member)
        {
            _repository.Delete(member);
        }

        public MemberModel GetById(int? id)
        {
            return _context.Members.SingleOrDefault(m => m.NomorAnggota == id);
        }

        public IOrderedQueryable<MemberModel> GetMemberPage()
        {
            return _repository.GetAll().OrderBy(m => m.NamaLengkap);
        }

        [HttpGet]
        public SelectList GetMembers()
        {
            var memberList = _context.Members.OrderBy(m => m.NamaLengkap).Select(x => new { Id = x.NomorAnggota, Value = x.NamaLengkap });
            return new SelectList(memberList, "Id", "Value");
        }

        public void Save(MemberModel member)
        {
            _repository.Save(member);
        }

        public void Update(MemberModel member)
        {
            _repository.Update(member);
        }
    }
}
