using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Scorables;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebAppBot.Factory;

namespace WebAppBot.Dialogs
{
   // [LuisModel("appId","subscriptionkey")]
    [Serializable]
    public class RootDialog : DispatchDialog
    {
        private readonly IDialogFactory _dialogFactory;
        public RootDialog(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        [RegexPattern("(?i)abc")] //case insensitive string abc
        [ScorableGroup(0)]
        public async Task DoSomething(IDialogContext dialogContext, IActivity activity)
        {
            var res = activity as Activity;
            await dialogContext.Forward(_dialogFactory.Create<EchoDialog>(), AfterComplete, res.Text, default(CancellationToken));
        }

        [LuisIntent("def")]
        [ScorableGroup(1)]
        public async Task LuisDemo(IDialogContext dialogContext, LuisResult result)
        {
            var res = (Activity)dialogContext.Activity;
            await dialogContext.Forward(_dialogFactory.Create<EchoDialog>(), AfterComplete, res.Text, default(CancellationToken));
        }

        [MethodBind]
        [ScorableGroup(2)]
        public async Task Default(IDialogContext dialogContext, IActivity activity) {
            var res = activity as Activity;
            await dialogContext.PostAsync($"Echo Default: { res.Text }");
        }

        private async Task AfterComplete(IDialogContext context, IAwaitable<object> result)
        {
            var response = await result;
        }
    }
}