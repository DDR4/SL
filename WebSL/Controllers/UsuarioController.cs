using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using SL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WebSL.Controllers
{
    public class UsuarioController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Registrar(Usuario obj)
        {
            var bussingLogic = new SL.BusinessLogic.BLUsuario();
            var response = bussingLogic.Registrar(obj);

            return Json(response);
        }

    }
}