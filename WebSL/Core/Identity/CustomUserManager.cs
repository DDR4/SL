using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;


namespace WebSL.Core.Identity
{
    public class CustomUserManager : UserManager<CustomApplicationUser>
    {
        public CustomUserManager() : base(new CustomUserStore<CustomApplicationUser>())
        {
        }

        public override Task<CustomApplicationUser> FindAsync(string userName, string password)
        {
            var taskInvoke = Task<CustomApplicationUser>.Factory.StartNew(() =>
            {
                var credential = new SL.Entities.Usuario
                {
                    Nombre_Usu = userName,
                    Pass_Usu = password,
                    Tipo_Prod = 1
                };

                var authBL = new SL.BusinessLogic.BLAuthorization();
                var result = authBL.Authorize(credential);

                return new CustomApplicationUser(result.Result);

            });

            return taskInvoke;
        }
    }
}