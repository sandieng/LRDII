using LRDII.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace LRDII.Services
{
    public interface IMemberService
    {
        SelectList GetMembers();
        IOrderedQueryable<MemberModel> GetMemberPage();
        MemberModel GetById(int? id);
        void Save(MemberModel member);
        void Update(MemberModel member);
        void Delete(MemberModel member);
    }
}
