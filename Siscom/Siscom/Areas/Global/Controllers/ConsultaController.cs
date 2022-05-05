using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Siscom.Areas.Global.Controllers
{
    public class ConsultaController : Controller
    {
        //
        // GET: /Global/Consulta/

        public ActionResult Index()
        {
            return View();
        }

        public class paramsOut {
            public string query { get; set; }
            public int bd { get; set; }
            public bool sType { get; set; }
        }

        public string execute(paramsOut paramsOut)
        {
            try
            {
                String BD_name="";
                if (paramsOut.bd == 1) BD_name = "HUMAN_WEB_FINAL";
                else if (paramsOut.bd == 2) BD_name = "HUMAN_WEB_FINAL_QA";
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection("Data Source=SRVDB\\HSMART; Initial Catalog=" + BD_name + ";user=sa;pwd=yJ4cKhYE7F;"))
                {
                    using (SqlCommand cmd = new SqlCommand(@paramsOut.query, con))
                    {
                        con.Open();

                        if (!paramsOut.sType)
                        {
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                            Dictionary<string, object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }
                            return serializer.Serialize(rows);
                        }
                        else {
                            
                            int result = cmd.ExecuteNonQuery();
                            if (result>=0)
                                return "{\"status\": \"OK\", \"MenssageOut\": \"" + "Ejecutado Correctamente: filas afectadas (" + result .ToString()+ ")" + "\"}";
                            else
                                return "{\"status\": \"FAIL\", \"MenssageOut\": \"" + "El query no afecta registros, probablemente sea un select\"}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }


        
    }
}
