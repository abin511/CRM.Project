using System;
using System.Web.Mvc;
using CRM.BLL;
using CRM.Common;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebManage.Controllers
{
    public class LoginController : Controller
    {
        //实例化AdminInfo接口的对象
        readonly IAdminInfoService _adminInfoService = new AdminInfoService();


        public ActionResult Index()
        {
            //获取到Cookie中的值传递给前台显示
            var uName = Request.Cookies["UName"] == null ? "" : Request.Cookies["UName"].Value;
            ViewBag.UName = uName;
            return View();
        }
        public ActionResult Out()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Login");
        }
        /// <summary>
        /// 处理登录的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogin(AdminInfo adminInfo, string code)
        {
            Common.LogHelper.Debug("yonghulail");
            //如果有用户名的话讲用户名存放到Cookie中
            if (adminInfo.UName != null)
            {
                var httpCookie = Response.Cookies["UName"];
                if (httpCookie != null)
                {
                    httpCookie.Value = adminInfo.UName;
                    httpCookie.Expires = DateTime.Now.AddDays(7);
                }
            }
            //这里验证用户输入的验证码和系统的验证码是否相同
            string sessionCode = Session["ValidateCode"]?.ToString() ?? new Guid().ToString();

            //将验证码去掉，避免暴力破解
            Session["ValidateCode"] = new Guid();  

            if (sessionCode != code)
            {
                return Content("请输入正确的验证码");
            }

            //调用BLL检验用户名密码是否正确
            AdminInfo uinfo =_adminInfoService.UserLogin(adminInfo);
            if (uinfo != null)
            {
                //提供Session接口方便后面判断用户登录
                Session["AdminInfo"] = uinfo;
                return Content("OK");
            }
            else
            {
                return Content("用户名密码错误，请您检查");
            }
        }

        /// <summary>
        /// 验证码的校验
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckCode()
        {
            //生成验证码
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
    }
}
