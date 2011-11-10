using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Filters
{
    public class ActionLogFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            MusicStoreEntities storeDB = new MusicStoreEntities();

            ActionLog log = new ActionLog()
            {
                Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                Action = filterContext.ActionDescriptor.ActionName,
                IP = filterContext.HttpContext.Request.UserHostAddress,
                DateTime = filterContext.HttpContext.Timestamp
            };

            storeDB.ActionLogs.Add(log);
            storeDB.SaveChanges();

            base.OnActionExecuting(filterContext);
        }
    }
}