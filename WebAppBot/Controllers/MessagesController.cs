using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAppBot.Dialogs;

namespace WebAppBot.Controllers
{
    public class MessagesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {

            try
            {
                if (activity.Type == ActivityTypes.Message)
                {
                    /* Creates a dialog stack for the new conversation, adds RootDialog to the stack, and forwards all 
                     *  messages to the dialog stack. */
                    using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
                    {
                        var dialog = scope.Resolve<IDialog<object>>();
                        await Conversation.SendAsync(activity, () => dialog);
                    }

                   // await Conversation.SendAsync(activity, () => new RootDialog());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

    }
}
