using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SL.Common;
using SL.Entities;

namespace SL.DataAccess
{
    public class DAVentas
    {
        public IEnumerable<Ventas> GetVentas(Ventas obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@FECHA", obj.Fecha);
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);

                var result = connection.Query(
                     sql: "SP_BUSCAR_VENTA",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Ventas
                     {
                         Cod_Venta = n.Single(d => d.Key.Equals("Cod_Venta")).Value.Parse<int>(),
                         Producto = new Producto
                         {
                             Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                         },
                         Precio_Prod = n.Single(d => d.Key.Equals("Precio_Prod")).Value.Parse<int>(),
                         Precio_Venta = n.Single(d => d.Key.Equals("Precio_Venta")).Value.Parse<int>(),
                         Descuento_Venta = n.Single(d => d.Key.Equals("Descuento")).Value.Parse<int>(),
                         Precio_Final = n.Single(d => d.Key.Equals("Precio_Total")).Value.Parse<int>(),
                         Fecha = n.Single(d => d.Key.Equals("Fecha")).Value.Parse<int>(),
                         Auditoria = new Auditoria
                         {
                             TipoUsuario = obj.Auditoria.TipoUsuario
                         },
                         Operacion = new Operacion
                         {
                             TotalRows = n.Single(d => d.Key.Equals("TotalRows")).Value.Parse<int>()
                         }
                     });

                return result;
            }
        }
        public int InsertUpdateVentas(Ventas obj)
        {

            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Venta", obj.Cod_Venta);
                parm.Add("@Precio_Prod", obj.Precio_Prod);
                parm.Add("@Precio_Venta", obj.Precio_Venta);
                parm.Add("@Descuento", obj.Descuento_Venta);
                parm.Add("@Precio_Total", obj.Precio_Final);
                parm.Add("@Usuario", obj.Auditoria.UsuarioCreacion);
                parm.Add("@Talla_Ventas", obj.Talla_Ventas);

                var result = connection.Execute(
                    sql: "SP_INSERTAR_VENTA",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public IEnumerable<TallasVenta> TallasVenta(string Cod_Venta)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Venta", Cod_Venta);

                var result = connection.Query(
                     sql: "SP_TALLAS_VENTA",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new TallasVenta
                     {
                         Talla = n.Single(d => d.Key.Equals("Talla")).Value.Parse<string>(),                      
                         Producto = new Producto
                         {
                             Tipo_Prod = n.Single(d => d.Key.Equals("Tipo_Prod")).Value.Parse<int>(),
                             Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                         },
                         Cantidad = n.Single(d => d.Key.Equals("Cantidad")).Value.Parse<int>(),
                         Precio_Prod = n.Single(d => d.Key.Equals("Precio")).Value.Parse<int>()
                     });

                return result;
            }
        }

        public int DeleteVentas(Ventas obj)
        {

            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Venta", obj.Cod_Venta);
                parm.Add("@Usuario", obj.Auditoria.UsuarioModificacion);

                var result = connection.Execute(
                    sql: "SP_ELIMINAR_VENTA",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public IEnumerable<Ventas> GetAllVentas(Ventas obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@FechaDesde", obj.FechaDesde);
                parm.Add("@FechaHasta", obj.FechaHasta);

                var result = connection.Query(
                     sql: "SP_FILTRAR_VENTA",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                          .Select(n => new Ventas
                          {
                              Cod_Venta = n.Single(d => d.Key.Equals("Cod_Venta")).Value.Parse<int>(),
                              Producto = new Producto
                              {
                                  Marca_Prod = n.Single(d => d.Key.Equals("Marca_Prod")).Value.Parse<string>(),
                              },
                              Precio_Prod = n.Single(d => d.Key.Equals("Precio_Prod")).Value.Parse<int>(),
                              Precio_Venta = n.Single(d => d.Key.Equals("Precio_Venta")).Value.Parse<int>(),
                              Descuento_Venta = n.Single(d => d.Key.Equals("Descuento")).Value.Parse<int>(),
                              Precio_Final = n.Single(d => d.Key.Equals("Precio_Total")).Value.Parse<int>(),
                              Fecha = n.Single(d => d.Key.Equals("Fecha")).Value.Parse<int>()
                          });

                return result;
            }
        }

    }
}
