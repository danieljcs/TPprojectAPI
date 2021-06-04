using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPAPIproject.Models;
using TPAPIproject.VMmodels;

namespace TPAPIproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : Controller
    {
        private FacadeController Decode = new FacadeController();
        private string CadenaConexion = "";
        private readonly DbContextTP _context;

        public MainController(DbContextTP context, IConfiguration configuration)
        {
            //CADENA DE CONEXION Y CONTEXT EL CUAL CONTIENE LOS MODELOS
            _context = context;
            CadenaConexion = Decode.Base64Decode(configuration.GetConnectionString("Database"));
        }


        //METODO PARA REGISTRAR O MODIFICAR LA INFORMACION
        [HttpPost("[action]")]
        public async Task<ActionResult<Respuesta>> RegisterModify(ReqDocumentation datareq)
        {
            Respuesta respuesta = new Respuesta();//SE DEFINE LA RESPUESTA

            try
            {
                //SE VALIDA SI VIENE EL ID DEL REGISTRO EN CASO DE QUE NO SE CREA UN NUEVO OBJETO
                TpDocumentation register_documentation = 
                    datareq.Id == 0   ?
                    new TpDocumentation() 
                    : _context.TpDocumentations.FirstOrDefault(x => x.Id == datareq.Id); ;

                //SE LLENAN LOS CAMPOS CON LA INFORMACION RECIBIDA
                register_documentation.IdentificationNumber = datareq.IdentificationNumber;
                register_documentation.IdentificationTypeId = datareq.IdentificationTypeId;
                register_documentation.CompanyName = datareq.CompanyName=="" ? null : datareq.CompanyName;
                register_documentation.FirstName = datareq.FirstName == "" ? null : datareq.FirstName;
                register_documentation.SecondName = datareq.SecondName == "" ? null : datareq.SecondName;
                register_documentation.FirstLastName = datareq.FirstLastName == "" ? null : datareq.FirstLastName;
                register_documentation.SecondLastName = datareq.SecondLastName == "" ? null : datareq.SecondLastName;
                register_documentation.Email = datareq.Email;
                register_documentation.CheckMessagesCell = datareq.CheckMessagesCell;
                register_documentation.CheckMessagesEmail = datareq.CheckMessagesEmail;
                //SE VALIDA SI ES ACTUALIZACION O CREACION EN CASO DE QUE SEA CREACION SE GUARDA LA FEHCA DE HOY Y SE AGREGA AL CONTEXTO
                if (datareq.Id == 0)
                {
                    register_documentation.CreationDate = DateTime.Now;
                    _context.TpDocumentations.Add(register_documentation);
                }else // EN CASO DE QUE SEA MODIFICACION SIMPLEMENTE SE AGREGA AL CONTEXTO PARA LA MODIFICACION
                {
                    _context.Entry(register_documentation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                //SE GUARDAN LOS CAMBIOS
                await _context.SaveChangesAsync();



                //SE ARMA EL OBJETO RESPUESTA Y SE ENVIA
                respuesta.error = false;
                respuesta.message = "Registro creado Exitosamente";
                respuesta.data = register_documentation;
                respuesta.code = 0;
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                //EN CASO DE ERROR CONTROLADO SE ARMA EL OBEJTO RESPUESTA Y SE MANDA EL CODIGO DEL ERROR
                respuesta.error = true;
                respuesta.message = "Error metodo principal";
                respuesta.data = e;
                respuesta.code = 500;
                return Ok(respuesta);
            }

        }


        //ENPOINT PARA LLAMAR LA DATA RELACIONADA A UN REGISTRO
        [HttpGet("[action]")]
        public async Task<ActionResult<Respuesta>> GetDocumentation(long identification)
        {
            Respuesta respuesta = new Respuesta();//SE DEFINE LA RESPUESTA

            try
            {
                //SE CONSULTA EL REGISTRO POR LA IDENTIFICACION RECIVIDA
                TpDocumentation data_documentation = _context.TpDocumentations.FirstOrDefault(x => x.IdentificationNumber == identification);


                //SE ARMA EL OBJETO Y SE RESPONDE CON LA DATA ENCONTRADA
                respuesta.error = false;
                respuesta.message = "Todo OK";
                respuesta.data = data_documentation;
                respuesta.code = 0;
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                //EN CASO DE ERROR CONTROLADO SE ARMA EL OBEJTO RESPUESTA Y SE MANDA EL CODIGO DEL ERROR
                respuesta.error = true;
                respuesta.message = "Error metodo principal";
                respuesta.data = e;
                respuesta.code = 500;
                return Ok(respuesta);
            }

        }


        //API PARA LLAMAR LOS TIPOS DE IDENTIFICACION
        [HttpGet("[action]")]
        public async Task<ActionResult<Respuesta>> GetIdentificationTypes()
        {
            Respuesta respuesta = new Respuesta();//SE DEFINE LA RESPUESTA

            try
            {
                // SE CONSULTAN TODOS LOS RESGISTROS DE TIPO IDENTIFICACION EXISTENTES Y SE RETORNAN
                List<TpIdentificationType> identificationType = _context.TpIdentificationTypes.Select(x => x).ToList();

                //SE ARMA EL OBJETO Y SE RESPONDE CON LA DATA ENCONTRADA
                respuesta.error = false;
                respuesta.message = "Todo OK";
                respuesta.data = identificationType;
                respuesta.code = 0;
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                //EN CASO DE ERROR CONTROLADO SE ARMA EL OBEJTO RESPUESTA Y SE MANDA EL CODIGO DEL ERROR
                respuesta.error = true;
                respuesta.message = "Error metodo principal";
                respuesta.data = e;
                respuesta.code = 500;
                return Ok(respuesta);
            }

        }


        public class Respuesta
        {
            public bool error { get; set; }
            public string message { get; set; }
            public object data { get; set; }
            public int code { get; set; }
        }
    }
}
