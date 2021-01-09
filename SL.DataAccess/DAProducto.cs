using Dapper;
using SL.Common;
using SL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.DataAccess
{
    public class DAProducto
    {
        public IEnumerable<Producto> GetProducto(Producto obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Prod", obj.Cod_Prod);
                parm.Add("@Marca_Prod", obj.Marca_Prod);
                parm.Add("@Stock_Prod", obj.Stock_Prod);
                parm.Add("@Estado", obj.Estado_Prod);
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);
                var result = connection.Query(
                     sql: "SP_BUSCAR_PRODUCTO",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Producto
                     {
                         Cod_Prod = n.Single(d => d.Key.Equals("Cod_Prod")).Value.Parse<int>(),
                         Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                         Precio_Prod = n.Single(d => d.Key.Equals("Precio_Prod")).Value.Parse<double>(),
                         Stock_Prod = n.Single(d => d.Key.Equals("Stock_Prod")).Value.Parse<int>(),
                         Tipo_Prod = n.Single(d => d.Key.Equals("Tipo_Prod")).Value.Parse<int>(),
                         IdProducto = n.Single(d => d.Key.Equals("IdProducto")).Value.Parse<int>(),
                         Estado_Prod = n.Single(d => d.Key.Equals("Estado_Prod")).Value.Parse<int>(),
                         Auditoria = new Auditoria
                         {
                             TipoUsuario = obj.Auditoria.TipoUsuario,
                         },
                         Operacion = new Operacion
                         {
                             TotalRows = n.Single(d => d.Key.Equals("TotalRows")).Value.Parse<int>(),
                         },
                         FechaDesde = n.Single(d => d.Key.Equals("Fecha")).Value.Parse<int>(),
                     });

                return result;
            }
        }

        public int InsertUpdateProducto(Producto obj)
        {

            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@IdProducto", obj.IdProducto);
                parm.Add("@Cod_Prod", obj.Cod_Prod);
                parm.Add("@Cod_Almacen", obj.Codigo_Al);
                parm.Add("@Marca_Prod", obj.Marca_Prod);
                parm.Add("@Precio_Prod", obj.Precio_Prod);
                parm.Add("@Tipo_Prod", obj.Tipo_Prod);
                parm.Add("@Usuario", obj.Auditoria.UsuarioCreacion);
                parm.Add("@Estado", obj.Estado_Prod);
                parm.Add("@Talla_Prod", obj.TallasXml);
                var result = connection.Execute(
                    sql: "SP_INSERTAR_PRODUCTO",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public int DeleteProducto(Producto obj)
        {

            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Prod", obj.Cod_Prod);
                parm.Add("@Usuario", obj.Auditoria.UsuarioModificacion);
                var result = connection.Execute(
                    sql: "SP_ELIMINAR_PRODUCTO",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public IEnumerable<Producto> GetAllProductos(Producto obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Prod", obj.Cod_Prod);
                parm.Add("@Marca_Prod", obj.Marca_Prod);
                parm.Add("@Estado", obj.Estado_Prod);
                parm.Add("@FechaDesde", obj.FechaDesde);
                parm.Add("@FechaHasta", obj.FechaHasta);

                var result = connection.Query(
                     sql: "SP_FILTRAR_PRODUCTO",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Producto
                     {
                         Cod_Prod = n.Single(d => d.Key.Equals("Cod_Prod")).Value.Parse<int>(),
                         Stock_Prod = n.Single(d => d.Key.Equals("Stock_Prod")).Value.Parse<int>(),
                         Codigo_Al = n.Single(d => d.Key.Equals("Cod_Almacen")).Value.Parse<string>(),
                         Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                         Talla_Prod = n.Single(d => d.Key.Equals("Talla_Prod")).Value.Parse<string>(),
                         Talla_Vendida_Prod = n.Single(d => d.Key.Equals("Talla_Vendida_Prod")).Value.Parse<string>(),
                         Precio_Prod = n.Single(d => d.Key.Equals("Precio_Prod")).Value.Parse<double>(),
                         Precio_Prod_Mayor = n.Single(d => d.Key.Equals("Precio_Prod_Mayor")).Value.Parse<double>(),
                         Estado_Prod = n.Single(d => d.Key.Equals("Estado_Prod")).Value.Parse<int>(),
                         FechaDesde = n.Single(d => d.Key.Equals("Fecha")).Value.Parse<int>()
                     });

                return result;
            }
        }

        public IEnumerable<Tallas> TallasProducto(string cod_prod)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Prod", cod_prod);

                var result = connection.Query(
                     sql: "SP_TALLAS_PRODUCTO",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Tallas
                     {
                         Talla = n.Single(d => d.Key.Equals("Talla")).Value.Parse<string>(),
                         CodigoProducto = n.Single(d => d.Key.Equals("Cod_Prod")).Value.Parse<int>(),
                         Cantidad = n.Single(d => d.Key.Equals("Cantidad")).Value.Parse<int>()
                     });

                return result;
            }
        }

        public IEnumerable<Tallas> TallasVenta(string cod_prod)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Prod", cod_prod);

                var result = connection.Query(
                     sql: "SP_TALLAS_VENTA",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Tallas
                     {
                         Talla = n.Single(d => d.Key.Equals("Talla")).Value.Parse<int>(),
                         Cantidad = n.Single(d => d.Key.Equals("Cantidad")).Value.Parse<int>()
                     });

                return result;
            }
        }
               
    }
}
