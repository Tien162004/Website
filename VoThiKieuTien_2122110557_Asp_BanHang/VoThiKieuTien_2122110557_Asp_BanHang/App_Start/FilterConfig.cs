using System.Web;
using System.Web.Mvc;

namespace VoThiKieuTien_2122110557_Asp_BanHang
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
