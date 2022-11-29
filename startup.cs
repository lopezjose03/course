using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using ConsumoWCF.ServiceMedicamentos;

namespace ConsumoWCF.Controllers
{
    public class MedicamentoController : Controller
    {
        // GET: Medicamento
        public ActionResult Index()
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(remove);
            return View();
        }

        public bool remove(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        public JsonResult listarMedicamentos()
        {
            MedicamentosClient oMedicamentosClient = new MedicamentosClient();
            oMedicamentosClient.ClientCredentials.UserName.UserName = "josel";
            oMedicamentosClient.ClientCredentials.UserName.Password = "1234";
            var lista = oMedicamentosClient.listarMedicamentos().Where(p =>p.bhabilitado ==1)
                .Select(
                 p => new
                 {
                     p.IdMedicamento,
                     p.nombre,
                     p.concentracion,
                     p.nombreFormaFarmaceutica,
                     p.stock,
                     p.precio,
                     p.presentacion
                 }
                 ).ToList();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult buscarMedicamentosPorNombre(string nombreMedicamento)
        {
            MedicamentosClient oMedicamentosClient = new MedicamentosClient();
            oMedicamentosClient.ClientCredentials.UserName.UserName = "josel";
            oMedicamentosClient.ClientCredentials.UserName.Password = "1234";
            var lista = oMedicamentosClient.listarMedicamentos().Where(p => p.bhabilitado == 1 && p.nombre.ToLower().Contains(nombreMedicamento.ToLower()))
               .Select(
                p => new
                {
                    p.IdMedicamento,
                    p.nombre,
                    p.concentracion,
                    p.nombreFormaFarmaceutica,
                    p.stock,
                    p.precio,
                    p.presentacion
                }
                ).ToList();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public JsonResult listarFormaFarmaceutica()
        {
            MedicamentosClient oMedicamentosClient = new MedicamentosClient();
            oMedicamentosClient.ClientCredentials.UserName.UserName = "josel";
            oMedicamentosClient.ClientCredentials.UserName.Password = "1234";
            var lista = oMedicamentosClient.listarFormaFarmaceutica()
                .Select(p => new
                {
                    p.Idformafarmaceutica,
                    p.nombreFormaFarmaceutica
                }).ToList();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult recuperarMedicamento(int Idmedicamento)
        {
            MedicamentosClient oMedicamentosClient = new MedicamentosClient();
            oMedicamentosClient.ClientCredentials.UserName.UserName = "josel";
            oMedicamentosClient.ClientCredentials.UserName.Password = "1234";
            var medicamento = oMedicamentosClient.recuperarMedicamento(Idmedicamento);

            return Json(medicamento, JsonRequestBehavior.AllowGet);
        }

        public int agregarYEditarMedicamento( MedicamentoCLS oMedicamentoCLS)
        {
           int respuesta = 0;

            try
            {
                MedicamentosClient oMedicamentosClient = new MedicamentosClient();
                oMedicamentosClient.ClientCredentials.UserName.UserName = "josel";
                oMedicamentosClient.ClientCredentials.UserName.Password = "1234";
                respuesta = oMedicamentosClient.registarYActualizarMedicamento(oMedicamentoCLS);

            } catch(Exception ex)
            {
                respuesta = 0;
            }
            return respuesta;

        }


        public int eliminarMedicamento(int IdMedicamento)
        {
            int respuesta = 0;

            try
            {
                MedicamentosClient oMedicamentoClient = new MedicamentosClient();
                oMedicamentoClient.ClientCredentials.UserName.UserName = "josel";
                oMedicamentoClient.ClientCredentials.UserName.Password = "1234";
                respuesta = oMedicamentoClient.eliminarMedicamento(IdMedicamento);


            } catch(Exception ex)
            {
                respuesta= 0;
            }
            return respuesta;
        }

    }
}
