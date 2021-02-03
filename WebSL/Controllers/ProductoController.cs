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
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetProducto(Producto obj)
        {
            var ctx = HttpContext.GetOwinContext();
            var tipoUsuario = ctx.Authentication.User.Claims.FirstOrDefault().Value;
            obj.Auditoria = new Auditoria
            {
                TipoUsuario = tipoUsuario
            };

            string draw = Request.Form.GetValues("draw")[0];
            int inicio = Convert.ToInt32(Request.Form.GetValues("start").FirstOrDefault());
            int fin = Convert.ToInt32(Request.Form.GetValues("length").FirstOrDefault());

            obj.Operacion = new Operacion
            {
                Inicio = (inicio / fin),
                Fin = fin
            };

            var bussingLogic = new SL.BusinessLogic.BLProducto();
            var response = bussingLogic.GetProducto(obj);

            var Datos = response.Data;
            int totalRecords = Datos.Any() ? Datos.FirstOrDefault().Operacion.TotalRows : 0;
            int recFilter = totalRecords;

            var result = (new
            {
                draw = Convert.ToInt32(draw),
                recordsTotal = totalRecords,
                recordsFiltered = recFilter,
                data = Datos
            });

            return Json(result);
        }

        public JsonResult InsertUpdateProducto(Producto obj)
        {
            var bussingLogic = new SL.BusinessLogic.BLProducto();

            obj.Auditoria = new Auditoria
            {
                UsuarioCreacion = User.Identity.Name,
                UsuarioModificacion = User.Identity.Name
            };

            var tallasxml = obj.Tallas_Prod.Select(i => new XElement("Talla",
                        new XElement("talla", i.Talla),
                        new XElement("idproducto", i.IdProducto),
                        new XElement("cantidad", i.Cantidad)));
            obj.TallasXml = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Tallas", tallasxml));

            var response = bussingLogic.InsertUpdateProducto(obj);

            return Json(response);
        }


        public JsonResult DeleteProducto(Producto obj)
        {
            var bussingLogic = new SL.BusinessLogic.BLProducto();
            obj.Auditoria = new Auditoria
            {
                UsuarioModificacion = User.Identity.Name
            };
            var response = bussingLogic.DeleteProducto(obj);

            return Json(response);

        }


        public JsonResult GetAllProductos(Producto obj)
        {
            try
            {
                Session["ReporteProducto"] = null;
                var bussingLogic = new SL.BusinessLogic.BLProducto();
                var response = bussingLogic.GetAllProductos(obj);
                Session["ReporteProducto"] = response.Data.ToList();
                return Json(response);
            }
            catch (Exception ex)
            {
                var result = new SL.Common.Response<List<Producto>>(ex);
                return Json(result);
            }

        }

        public void GenerarExcel()
        {
            var NombreExcel = "Producto - Sistemas de Ventas ";

            // Recuperamos la data  de las consulta DB
            var data = (List<Producto>)Session["ReporteProducto"];

            // Creación del libro excel xlsx.
            var wb = new XSSFWorkbook();

            // Creación del la hoja y se especifica un nombre
            var fileName = WorkbookUtil.CreateSafeSheetName("Productos");
            ISheet sheet = wb.CreateSheet(fileName);

            // Contadores para filas y columnas.
            int rownum = 0;
            int cellnum = 0;

            // Creacion del estilo de la letra para las cabeceras.
            var fontCab = wb.CreateFont();
            fontCab.FontHeightInPoints = 10;
            fontCab.FontName = "Calibri";
            fontCab.Boldweight = (short)FontBoldWeight.Bold;
            fontCab.Color = HSSFColor.White.Index;

            // Creacion del color del estilo.
            var colorCab = new XSSFColor(new byte[] { 7, 105, 173 });

            // Se crea el estilo y se agrega el font al estilo
            var styleCab = (XSSFCellStyle)wb.CreateCellStyle();
            styleCab.SetFont(fontCab);
            styleCab.FillForegroundXSSFColor = colorCab;
            styleCab.FillPattern = FillPattern.SolidForeground;


            string[] Cabezeras = {
                    "Tipo de Producto", "Marca", "Stock", "Precio", "Estado", "Fecha"
                };

            // Se crea la primera fila para las cabceras.
            IRow row = sheet.CreateRow(rownum++);
            ICell cell;

            foreach (var item in Cabezeras)
            {
                // Se crea celdas y se agrega las cabeceras
                cell = row.CreateCell(cellnum);
                cell.SetCellValue(item);
                cell.CellStyle = styleCab;

                sheet.AutoSizeColumn(cellnum);
                cellnum++;
            }

            // Creacion del estilo de la letra para la data.
            var fontBody = wb.CreateFont();
            fontBody.FontHeightInPoints = 10;
            fontBody.FontName = "Arial";

            var styleBody = (XSSFCellStyle)wb.CreateCellStyle();
            styleBody.SetFont(fontBody);


            // Impresión de la data
            foreach (var item in data)
            {
                cellnum = 0;
                row = sheet.CreateRow(rownum++);

                sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Tipo_Prod == 1 ? "Polo" : item.Tipo_Prod == 2 ? "Vestido"
                : item.Tipo_Prod == 3 ? "Cafarena" : item.Tipo_Prod == 4 ? "Chompa" : item.Tipo_Prod == 5 ? "Short"
                : item.Tipo_Prod == 6 ? "Pantalon" : item.Tipo_Prod == 7 ? "Falda" : item.Tipo_Prod == 8 ? "Zapatilla"
                : item.Tipo_Prod == 9 ? "Blusa" : item.Tipo_Prod == 10 ? "Camisa": "".ToString(), styleBody); sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Marca_Prod, styleBody); sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Stock_Prod.ToString(), styleBody); sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Precio_Prod.ToString("F2"), styleBody); sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Estado_Prod == 1 ? "Activo" : "Inactivo".ToString(), styleBody); sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.FechaDesde.ToString().Substring(6, 2) + "/" + item.FechaDesde.ToString().Substring(4, 2) + "/" + item.FechaDesde.ToString().Substring(0, 4), styleBody); sheet.AutoSizeColumn(cellnum);

            }

            var nameFile = NombreExcel + DateTime.Now.ToString("dd_MM_yyyy HH:mm:ss") + ".xlsx";
            Response.AddHeader("content-disposition", "attachment; filename=" + nameFile);
            Response.ContentType = "application/octet-stream";
            Stream outStream = Response.OutputStream;
            wb.Write(outStream);
            outStream.Close();
            Response.End();
        }

        public void AddValue(IRow row, int cellnum, string value, ICellStyle styleBody)
        {
            ICell cell;
            cell = row.CreateCell(cellnum);
            cell.SetCellValue(value);
            cell.CellStyle = styleBody;

        }

        public JsonResult TallasProducto(string IdProducto)
        {
            var bussingLogic = new SL.BusinessLogic.BLProducto();
            var response = bussingLogic.TallasProducto(IdProducto);

            return Json(response);
        }

        public JsonResult TallasVenta(string Cod_Prod)
        {
            var bussingLogic = new SL.BusinessLogic.BLProducto();
            var response = bussingLogic.TallasVenta(Cod_Prod);

            return Json(response);

        }
    }
}