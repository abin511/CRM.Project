using System.Web.Mvc;
using CRM.BLL;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebManage.Controllers
{
    public class HomeController : BaseController
    {
        readonly IAdminInfoService _adminInfoService = new AdminInfoService();

        public ActionResult Index()
        {
            AdminInfo uInfo = Session["AdminInfo"] as AdminInfo;
            if (uInfo != null)
            {
                ViewBag.UName = uInfo.UName;
            }
            return View();
        }

        /// <summary>
        /// 查询出数据显示在菜单栏目中
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadMenuData()
        {
            var data = _adminInfoService.LoadMenuData(CurrentUserInfo.ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
