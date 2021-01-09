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
    public class DAAuthorization
    {
        public Task<Usuario> Authorize(Usuario obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();

                var parameter = new DynamicParameters();

                parameter.Add("@Nombre_Usu", obj.Nombre_Usu);
                parameter.Add("@Pass_Usu", obj.Pass_Usu);

                var result = connection.Query(
                    sql: "LOGEAR",
                    param: parameter,
                    commandType: CommandType.StoredProcedure)
                    .Select(m => m as IDictionary<string, object>)
                    .Select(n => new Usuario
                    {
                        Cod_Usu = n.Single(d => d.Key.Equals("Cod_Usu")).Value.Parse<int>(),
                        Nombre_Usu = n.Single(d => d.Key.Equals("Nombre_Usu")).Value.Parse<string>(),
                        Tipo_Usu = n.Single(d => d.Key.Equals("Tipo_Usu")).Value.Parse<int>(),
                    });

                return Task.FromResult(result.FirstOrDefault());

            }


        }
    }
}
