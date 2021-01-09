using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebSL.Core.Identity
{
    public class CustomApplicationUser : IUser
    {
        public string Id { get; }
        public string UserName { get; set; }
        public SL.Common.Response<SL.Entities.Usuario> Usuario { get; set; }

        public CustomApplicationUser() : this(new SL.Common.Response<SL.Entities.Usuario>(default(SL.Entities.Usuario)))
        {
        }


        public CustomApplicationUser(SL.Common.Response<SL.Entities.Usuario> usuario)
        {
            Usuario = usuario;

            if (usuario.InternalStatus == SL.Common.EnumTypes.InternalStatus.Success)
            {
                Id = usuario.Data.Tipo_Usu.ToString();
                UserName = usuario.Data.Nombre_Usu;
            }
            else
            {
                // Valores por defecto, lo cuales no tendran relevancia, puesto que la aplicación no iniciará sesión.
                Id = "01";
                UserName = "USER";
            }
        }


    }
}