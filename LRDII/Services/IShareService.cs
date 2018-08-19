using LRDII.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace LRDII.Services
{
    public interface IShareService
    {
        int GetShareholdingByMemberId(int memberId);
        SelectList GetSharePriceList();
        IOrderedQueryable<SharePriceModel> GetSharePricePage();
        void Save(SharePriceModel sharePrice);
        SharePriceModel GetById(int? id);
        void Update(SharePriceModel sharePrice);
        void Delete(SharePriceModel sharePrice);
    }
}
