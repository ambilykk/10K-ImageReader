using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.ProjectOxford.Vision;
using System.Configuration;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Text;
using System.IO;

namespace ImageReader.Controllers
{
    public static class ocrHelper
    {
        static string SubscriptionKey = ConfigurationManager.AppSettings["visionKey"].ToString();
        public static async Task<string> ReadText(string imageUrl, string language)
        {
            VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey);
            OcrResults ocrResult = await VisionServiceClient.RecognizeTextAsync(imageUrl, language);


            StringBuilder result = new StringBuilder();

            foreach (Region region in ocrResult.Regions)
            {
                foreach (Line line in region.Lines)
                {
                    foreach (Word word in line.Words)
                    {
                        result.Append(word.Text + " ");
                    }
                    result.Append("\n\r");
                }
            }
            return result.ToString();
        }

        public static async Task<string> ReadText(Stream stream, string language)
        {
            try
            {
                VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey);
                OcrResults ocrResult = await VisionServiceClient.RecognizeTextAsync(stream, language);

                StringBuilder result = new StringBuilder();
                foreach (Region region in ocrResult.Regions)
                {
                    foreach (Line line in region.Lines)
                    {
                        foreach (Word word in line.Words)
                        {
                            result.Append(word.Text + " ");
                        }
                        result.Append("\n\r");
                    }
                }
                return result.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}