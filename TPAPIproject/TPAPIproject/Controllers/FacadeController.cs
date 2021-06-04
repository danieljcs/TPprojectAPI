using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TPAPIproject.Controllers
{
    public class FacadeController : Controller
    {
        internal string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = WebEncoders.Base64UrlDecode(base64EncodedData);
            var resultado = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return resultado;
        }
    }
}
