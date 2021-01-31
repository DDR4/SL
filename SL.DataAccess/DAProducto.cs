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
                         IdProducto = n.Single(d => d.Key.Equals("IdProducto")).Value.Parse<int>(),
                         Estacion_Prod = n.Single(d => d.Key.Equals("Estacion_Prod")).Value.Parse<int>(),
                         Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                         Precio_Prod = n.Single(d => d.Key.Equals("Precio_Prod")).Value.Parse<double>(),
                         Stock_Prod = n.Single(d => d.Key.Equals("Stock_Prod")).Value.Parse<int>(),
                         Tipo_Prod = n.Single(d => d.Key.Equals("Tipo_Prod")).Value.Parse<int>(),                         
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
                parm.Add("@Tipo_Prod", obj.Tipo_Prod);
                parm.Add("@Marca_Prod", obj.Marca_Prod);
                parm.Add("@Precio_Prod", obj.Precio_Prod);
                parm.Add("@Estacion_Prod", obj.Estacion_Prod);
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
                parm.Add("@IdProducto", obj.IdProducto);
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
                parm.Add("@IdProducto", obj.IdProducto);
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
                         Stock_Prod = n.Single(d => d.Key.Equals("Stock_Prod")).Value.Parse<int>(),
                         Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                         Talla_Prod = n.Single(d => d.Key.Equals("Talla_Prod")).Value.Parse<string>(),
                         Talla_Vendida_Prod = n.Single(d => d.Key.Equals("Talla_Vendida_Prod")).Value.Parse<string>(),
                         Precio_Prod = n.Single(d => d.Key.Equals("Precio_Prod")).Value.Parse<double>(),
                         Estado_Prod = n.Single(d => d.Key.Equals("Estado_Prod")).Value.Parse<int>(),
                         FechaDesde = n.Single(d => d.Key.Equals("Fecha")).Value.Parse<int>()
                     });

                return result;
            }
        }

        public IEnumerable<Tallas> TallasProducto(string IdProducto)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@IdProducto", IdProducto);

                var result = connection.Query(
                     sql: "SP_TALLAS_PRODUCTO",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Tallas
                     {
                         Talla = n.Single(d => d.Key.Equals("Talla")).Value.Parse<string>(),
                         IdProducto = n.Single(d => d.Key.Equals("IdProducto")).Value.Parse<int>(),
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
