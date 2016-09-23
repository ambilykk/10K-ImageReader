using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ImageReader.Controllers
{
    public class testController : ApiController
    {

        public async Task<string> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);

                    FileStream fs = new FileStream(file.LocalFileName, FileMode.Open);

                    return await ocrHelper.ReadText(fs, "unk");
                }
                return "ok";// Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return e.Message;// Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }



        //public Task<IEnumerable<FileDesc>> Post()
        //{
        //    string folderName = "uploads";
        //    string PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
        //    string rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);

        //    if (Request.Content.IsMimeMultipartContent())
        //    {
        //        var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
        //        var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<FileDesc>>(t =>
        //        {

        //            if (t.IsFaulted || t.IsCanceled)
        //            {
        //                throw new HttpResponseException(HttpStatusCode.InternalServerError);
        //            }

        //            var fileInfo = streamProvider.FileData.Select(i =>
        //            {
        //                var info = new FileInfo(i.LocalFileName);
        //                return new FileDesc(info.Name, rootUrl + "/" + folderName + "/" + info.Name, info.Length / 1024);
        //            });
        //            return fileInfo;
        //        });

        //        return task;
        //    }
        //    else
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
        //    }

        //}
    }
}
