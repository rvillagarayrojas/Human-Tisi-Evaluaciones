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
using Siscom.Data.Global;
using Siscom.Entity.Global;
namespace Siscom.Business
{
    public class PuestoBL
    {
        private PuestoDA oPuestoDA;

        public PuestoBL()
        {
            oPuestoDA = new PuestoDA();
        }

         public int Insert(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.Insert(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message, ex);
             }
         }

         public int Update(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.Update(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message + " - " + ex.InnerException.Message, ex);
             }
         }
         public int Update_Usuario(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.Update_Usuario(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoBL.InsertPruebaPuesto()" + " - " + ex.Message + " - " + ex.InnerException, ex);
             }
         }
         public int UpdateUsuarios(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.UpdateUsuarios(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoBL.InsertPruebaPuesto()" + " - " + ex.Message + " - " + ex.InnerException, ex);
             }
         }
         public int UpdatePuesto(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.UpdatePuesto(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message, ex);
             }
         }
         public int UpdatePuestoTipo(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.UpdatePuestoTipo(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message, ex);
             }
         }

         public Tuple<Int32, String> DeletePuesto(PuestoBE oItem) 
         {
             try
             {
                 return oPuestoDA.DeletePuesto(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message, ex);
             }
         }

         public IList<PuestoBE> List(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.List(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoBL.List()" + " - " + ex.Message + " - " + ex.InnerException, ex);
             }
         }
         public IList<PuestoBE> ListPruebas(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.ListPruebas(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoBL.ListPruebas()" + " - " + ex.Message + " - " + ex.InnerException, ex);
             }
         }
         public IList<PuestoBE> ListaPruebaPuesto(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.ListaPruebaPuesto(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoBL.ListaPruebaPuesto()" + " - " + ex.Message + " - " + ex.InnerException, ex);
             }
         }


         public IList<PuestoBE> ListaCandidatosPuesto(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.ListaCandidatosPuesto(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoBL.ListaCandidatosPuesto()" + " - " + ex.Message + " - " + ex.InnerException, ex);
             }
         }

         public IList<PuestoBE> ListaReporte(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.ListaReporte(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoBL.ListaReporte()" + " - " + ex.Message + " - " + ex.InnerException, ex);
             }
         }

         public PuestoBE Get(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.Get(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoBL.Get()" + " - " + ex.Message + " - " + ex.InnerException, ex);
             }
         }

         public int Update_Estado_Prueba_Puesto(PuestoBE oItem)
         {
             try
             {
                 return oPuestoDA.Update_Estado_Prueba_Puesto(oItem);
             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoBL.Update()" + " - " + ex.Message + " - " + ex.InnerException, ex);
             }
         }
    }
}
