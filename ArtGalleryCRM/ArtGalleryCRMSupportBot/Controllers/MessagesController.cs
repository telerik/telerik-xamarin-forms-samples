using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ArtGalleryCRMSupportBot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ArtGalleryCRMSupportBot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new SupportDialog());
            }

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}