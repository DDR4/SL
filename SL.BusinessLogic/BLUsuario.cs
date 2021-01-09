using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Common;
using SL.DataAccess;
using SL.Entities;

namespace SL.BusinessLogic
{
    public class BLUsuario
    {
        private DAUsuario repository;

        public BLUsuario()
        {
            repository = new DAUsuario();
        }


        public Response<int> Registrar(Usuario obj)
        {
            try
            {
                var result = repository.Registrar(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

    }
}
