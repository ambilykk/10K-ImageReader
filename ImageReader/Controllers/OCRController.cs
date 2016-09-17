using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ImageReader.Controllers
{
    public class ocrController : ApiController
    {
        public async Task<string> GetData(string imageURL)
        {
            return await ocrHelper.ReadText(imageURL, "unk");
        }

        //public string GetDataNew(string imageURL)
        //{
        //    return "Good";
        //}
    }
}
