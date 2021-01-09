using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Common;
using SL.Entities;
using SL.DataAccess;

namespace SL.BusinessLogic
{
    public class BLVentas
    {
        private DAVentas repository;

        public BLVentas()
        {
            repository = new DAVentas();
        }

        public Response<IEnumerable<Ventas>> GetVentas(Ventas obj)
        {
            try
            {
                var result = repository.GetVentas(obj);
                return new Response<IEnumerable<Ventas>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<int> InsertUpdateVentas(Ventas obj)
        {
            try
            {
                var result = repository.InsertUpdateVentas(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<IEnumerable<TallasVenta>> TallasVenta(string Cod_Venta)
        {
            try
            {
                var result = repository.TallasVenta(Cod_Venta);
                return new Response<IEnumerable<TallasVenta>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<int> DeleteVentas(Ventas obj)
        {
            try
            {
                var result = repository.DeleteVentas(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<IEnumerable<Ventas>> GetAllVentas(Ventas obj)
        {
            try
            {
                var result = repository.GetAllVentas(obj);
                return new Response<IEnumerable<Ventas>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
