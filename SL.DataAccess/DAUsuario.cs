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
    public class DAUsuario
    {
        public IEnumerable<Usuario> GetUsuario(Usuario obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Estado_Usu", obj.Estado_Usu);
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);
                var result = connection.Query(
                     sql: "SP_BUSCAR_USUARIO",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Usuario
                     {
                         Cod_Usu = n.Single(d => d.Key.Equals("Cod_Usu")).Value.Parse<int>(),
                         Nombre_Usu = n.Single(d => d.Key.Equals("Nombre_Usu")).Value.Parse<string>(),
                         Tipo_Usu = n.Single(d => d.Key.Equals("Tipo_Usu")).Value.Parse<int>(),
                         Estado_Usu = n.Single(d => d.Key.Equals("Estado_Usu")).Value.Parse<int>(),
                         Auditoria = new Auditoria
                         {
                             TipoUsuario = obj.Auditoria.TipoUsuario,
                         },
                         Operacion = new Operacion
                         {
                             TotalRows = n.Single(d => d.Key.Equals("TotalRows")).Value.Parse<int>(),
                         }
                     });

                return result;
            }
        }

        public int InsertUpdateUsuario(Usuario obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Usu", obj.Cod_Usu);
                parm.Add("@Nombre_Usu", obj.Nombre_Usu);
                parm.Add("@Pass_Usu", obj.Pass_Usu);
                parm.Add("@Tipo_Usu", obj.Tipo_Usu);
                parm.Add("@Estado_Usu", obj.Estado_Usu);
                var result = connection.Execute(
                    sql: "SP_INSERTAR_USUARIO",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public int DeleteUsuario(Usuario obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cod_Usu", obj.Cod_Usu);
                var result = connection.Execute(
                    sql: "SP_ELIMINAR_USUARIO",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
