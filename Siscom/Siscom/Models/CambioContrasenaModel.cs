using System;
using System.Collections.Generic;
using Siscom.Entity.Global;
using System.Web.Mvc;
using Siscom.Models.Base;

namespace Siscom.Models
{
    public class CambioContrasenaModel : BaseModel
    {
        public UsuarioBE UsuarioBE { get; set; }
        public List<UsuarioBE> LstUsuario { get; set; }

        public CambioContrasenaModel()
        {
            Id = Guid.NewGuid();
            UsuarioBE = new UsuarioBE();

        }
    }
}