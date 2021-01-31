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
    public class BLProducto
    {
        private DataAccess.DAProducto repository;

        public BLProducto()
        {
            repository = new DataAccess.DAProducto();
        }

        public Response<IEnumerable<Producto>> GetProducto(Producto obj)
        {
            try
            {
                var result = repository.GetProducto(obj);
                return new Response<IEnumerable<Producto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<int> InsertUpdateProducto(Producto obj)
        {
            try
            {

                var result = repository.InsertUpdateProducto(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<int> DeleteProducto(Producto obj)
        {
            try
            {
                var result = repository.DeleteProducto(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<IEnumerable<Producto>> GetAllProductos(Producto obj)
        {
            try
            {
                var result = repository.GetAllProductos(obj);
                return new Response<IEnumerable<Producto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<IEnumerable<Tallas>> TallasProducto(string IdProducto)
        {
            try
            {
                var result = repository.TallasProducto(IdProducto);
                return new Response<IEnumerable<Tallas>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<IEnumerable<Tallas>> TallasVenta(string cod_prod)
        {
            try
            {
                var result = repository.TallasVenta(cod_prod);
                return new Response<IEnumerable<Tallas>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
