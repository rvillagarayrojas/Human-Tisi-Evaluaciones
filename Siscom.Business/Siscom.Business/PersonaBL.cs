using System;
using System.Data;
using System.Text;
using System.Data.Common;
using System.Collections.Generic;
using System.IO;
using Siscom.Entity.Persona;
using Siscom.Data.Persona;
using Siscom.Business.Interface;
using System.Transactions;

namespace Siscom.Business
{
    public class PersonaBL
    {
        private PersonaDA oPersonaDA;
        public PersonaBL()
        {
            oPersonaDA = new PersonaDA();
        }

        public int Insert(PersonaBE oItem)
        {
            try
            {
                return oPersonaDA.Insert(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public int Actualizar(PersonaBE oItem)
        {
            try
            {
                return oPersonaDA.Actualizar(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int Update_Usuario(PersonaBE oItem)
        {
            try
            {
                return oPersonaDA.Update_Usuario(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception("PersonaBL.Update_Usuario()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }
        public int Reactivar_Usuario(PersonaBE oItem)
        {
            try
            {
                return oPersonaDA.Reactivar_Usuario(oItem);
            }
            catch (Exception ex)
            {
                throw new Exception("PersonaBL.Reactivar_Usuario()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }

        public IList<PersonaBE> List(PersonaBE oItem)
        {
            try
            {
                if (oItem.opcion == 0)
                {
                    return oPersonaDA.List(oItem);
                }
                if (oItem.opcion == 6)
                {
                    return oPersonaDA.ListPuesto(oItem);
                }
                if (oItem.opcion == 7)
                {
                    return oPersonaDA.ListPuestoView(oItem);
                } 
                else
                {
                    return oPersonaDA.ListUsuario(oItem);
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("PersonaBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }


        public IList<PersonaBE> ListSeguimiento(PersonaBE oItem)
        {
            try
            {
                return oPersonaDA.ListSeguimientoView(oItem);           
            }
            catch (Exception ex)
            {
                throw new Exception("PersonaBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }

    }
}
