using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ImageReader.Controllers
{
    public class ocrController : ApiController
    {
        public async Task<string> GetData(string imageURL)
        {
            return await ocrHelper.ReadText(imageURL, "unk");
        }
        
        public async Task<string> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string path = Path.GetTempPath();

           // string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(path);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    FileStream fs = new FileStream(file.LocalFileName, FileMode.Open);
                    var result= await ocrHelper.ReadText(fs, "unk");
                    fs.Close();
                    File.Delete(file.LocalFileName);
                    return result;
                }
                return "ok";
            }
            catch (System.Exception e)
            {
                return e.Message;
            }
        }

    }
}
