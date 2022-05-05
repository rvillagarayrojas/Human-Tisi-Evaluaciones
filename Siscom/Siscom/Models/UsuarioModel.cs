using System;
using System.Collections.Generic;
using Siscom.Entity.Global;
using System.Web.Mvc;

namespace Siscom.Models
{
    public class UsuarioModel
    {
        public const string SessionName = "UsuarioModel";
        public UsuarioBE    Usuario1;
        public decimal?     id      { get; set; }
        public string       nombre  { get; set; }
      
        public List<UsuarioBE> ListaUsuario{ get; set; }
        public List<SelectListItem> ListaEmpresas { get; set; }

        public UsuarioModel()
        {

           Usuario1 = new UsuarioBE();
        }

    }
}