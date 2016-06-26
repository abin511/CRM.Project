﻿using System;
using System.Web.Mvc;
using CRM.BLL;
using CRM.Common;
using CRM.IBLL;
using CRM.Model;

namespace CRM.WebManage.Controllers
{
    public class LoginController : Controller
    {
        //实例化UserInfo接口的对象
        IUserInfoService _iUserInfoService = new UserInfoService();


        public ActionResult Index()
        {
            //获取到Cookie中的值传递给前台显示
            var UName = Request.Cookies["UName"] == null ? "" : Request.Cookies["UName"].Value;
            ViewBag.UName = UName;
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
        /// <param name="userInfo"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public ActionResult CheckUserLogin(UserInfo userInfo, string Code)
        {
            //如果有用户名的话讲用户名存放到Cookie中
            if (userInfo.UName != null)
            {
                Response.Cookies["UName"].Value = userInfo.UName;
                Response.Cookies["UName"].Expires = DateTime.Now.AddDays(7);
            }


            //这里验证用户输入的验证码和系统的验证码是否相同
            string sessionCode = Session["ValidateCode"] == null ? new Guid().ToString() : Session["ValidateCode"].ToString();

            //将验证码去掉，避免暴力破解
            Session["ValidateCode"] = new Guid();  

            if (sessionCode != Code)
            {
                return Content("请输入正确的验证码");
            }

            //调用BLL检验用户名密码是否正确
            UserInfo uinfo=_iUserInfoService.checkUserLogin(userInfo);
            if (uinfo != null)
            {
                //提供Session接口方便后面判断用户登录
                Session["UserInfo"] = uinfo;
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
        /// <param name="context"></param>
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