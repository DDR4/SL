﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Entities
{
    public class Usuario
    {
        public int Cod_Usu { get; set; }
        public int Tipo_Prod { get; set; }
        public string Nombre_Usu { get; set; }
        public string Pass_Usu { get; set; }
        public int Tipo_Usu { get; set; }
        public int Estado_Usu { get; set; }
        public Operacion Operacion { get; set; }
    }
}
