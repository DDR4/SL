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
        public int Registrar(Usuario obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Nombre_Usu", obj.Nombre_Usu);
                parm.Add("@Pass_Usu", obj.Pass_Usu);
                parm.Add("@Tipo_Usu", obj.Tipo_Usu);
                var result = connection.Execute(
                    sql: "INSERTAR_USUARIO",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
