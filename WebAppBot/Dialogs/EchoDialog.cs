using BL;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebAppBot.Dialogs
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        private readonly IBusinessClass _businessClass;
        public EchoDialog(IBusinessClass businessClass)
        {
            _businessClass = businessClass;
        }
        public async Task StartAsync(IDialogContext context)
        {
             context.Wait<string>(MessageReceivedAsync);
            await Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<string> result)
        {
            var message = await result;
            _businessClass.BusinessMethod();
            await context.PostAsync($"Echo: { message}");
            context.Done(default(string));
        }
    }
}