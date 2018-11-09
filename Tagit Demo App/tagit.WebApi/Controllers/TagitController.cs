using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using tagit.WebApi.Models;

namespace tagit.WebApi.Controllers
{
    public class TagitController : ApiController
    {
        // POST: api/Tagit
        public async Task<HttpResponseMessage> Post()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                Stream stream = null;

                FileTaggingInformation tagInformation = null;

                var contents = await Request.Content.ReadAsMultipartAsync();

                foreach (var contentPart in contents.Contents)
                {
                    if (contentPart.Headers.ContentType.MediaType == "application/json")
                    {
                        var payload = await contentPart.ReadAsStringAsync();

                        tagInformation = Newtonsoft.Json.JsonConvert.DeserializeObject<FileTaggingInformation>(payload);
                    }
                    else if (contentPart.Headers.ContentType.MediaType == "image/jpeg")
                    {
                        stream = await contentPart.ReadAsStreamAsync();
                    }
                }

                if (tagInformation != null)
                {
                    byte[] taggedFile = Helpers.ImageHelper.UpdateImageProperties(stream, tagInformation?.Caption, tagInformation?.Tags, tagInformation.Rating);

                    MemoryStream dataStream = new MemoryStream(taggedFile);

                    response.Content = new StreamContent(dataStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                }
                else
                {
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("The image contents could not be determined or read. Confirm image is valid or try a different image.")
                    };
                }
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)
                {
                    Content = new StringContent(ex.Message)
                };
            }

            return response;
        }
    }
}