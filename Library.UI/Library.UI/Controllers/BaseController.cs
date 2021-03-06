﻿using Library.UI.SessionStorages;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace Library.UI.Controllers
{
    public class BaseController : Controller
    {
        protected string _inventoryApiBaseUrl => ConfigurationManager.AppSettings["inventoryApiUrl"];

        protected string _identityApiBaseUrl => ConfigurationManager.AppSettings["identityApiUrl"];

        protected string _rentalApiBaseUrl => ConfigurationManager.AppSettings["rentalApiUrl"];

        protected string _logApiBaseUrl => ConfigurationManager.AppSettings["logApiUrl"];

        protected ISessionStorage _sessionStorage = null;

        public BaseController()
        {
            _sessionStorage = new RedisSessionStorage(ConfigurationManager.AppSettings["redisServerIp"], Convert.ToInt32(ConfigurationManager.AppSettings["redisServerPort"]));
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var cookie = Request.Cookies["UserId"];

            if (cookie == null)
            {
                Response.Redirect("~/Account/Login");
            }
            else
            {
                var userId = cookie.Value;
                var user = _sessionStorage.Get<IdentityDetailsDTO>(userId);

                ViewBag.CurrentUser = user;
            }

            return base.BeginExecuteCore(callback, state);
        }
    }
}