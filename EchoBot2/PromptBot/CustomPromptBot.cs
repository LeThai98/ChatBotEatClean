using EchoBot2.SaveInfor;
using EchoBot2.SentimentPredict;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;
using Microsoft.Recognizers.Text.Number;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot2.PromptBot
{
    // kế thừa từ ActivityHandler Class - class dùng để xử lý các activity 
    public class CustomPromptBot : ActivityHandler
    {
        // create state object to track conversation
        private readonly BotState _userState;
        private readonly BotState _conversationState;


        // pass 2 state into constructor 
        public CustomPromptBot(ConversationState conversationState, UserState userState)
        {
            _conversationState = conversationState;
            _userState = userState;
        }

        // handlet activity when user send messsage 
        // parameter inclue:
        // turnContext: turn object in conversation. 
        //public const string WelcomeText = "This bot will introduce you to suggestedActions. Please answer the question:";

       

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {

            // Send a welcome message to the user and tell them what actions they may perform to use this bot
            // await SendWelcomeMessageAsync(turnContext, cancellationToken);
            var welcomeText = "Hello and welcome! What's your name ?";
            await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
            
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            // Get the state properties from the turn context and associte the propertis to Storage that we created
            var conversationStateAccessors = _conversationState.CreateProperty<ConversationFlow>(nameof(ConversationFlow));
            // get scope of state properties that associated with turn context . the value return of the GetAsync is property value( storage)      
            var flow = await conversationStateAccessors.GetAsync(turnContext, () => new ConversationFlow(), cancellationToken);

            var userStateAccessors = _userState.CreateProperty<UserProfile>(nameof(UserProfile));
            var profile = await userStateAccessors.GetAsync(turnContext, () => new UserProfile(), cancellationToken);

            // Hander activity to respone to user 
            // parameter include: flow state, profile state, turn, cancellationToken
            await FillOutUserProfileAsync(flow, profile, turnContext, cancellationToken);

            // Save changes.
            await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            
            await _userState.SaveChangesAsync(turnContext, false, cancellationToken);
        }
        private static async Task FillOutUserProfileAsync(ConversationFlow flow, UserProfile profile, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            // get text in message of user type to bot. we get from turn, property Activity, text property and Trim this test end. 
            var input = turnContext.Activity.Text?.Trim();
            string message;

            // switch for LastQuestionAsked enum 
            switch (flow.LastQuestionAsked)
            {
                // when enum is none
                case ConversationFlow.Question.None:
                    // send message to user
                   // await turnContext.SendActivityAsync("Let's get started. What is your name?", null, null, cancellationToken);
                    // with flow state . we resetup LastQuestionAsked enum into Name value. 
                    flow.LastQuestionAsked = ConversationFlow.Question.Name;
                    break;
                // when enum is Name  
                case ConversationFlow.Question.Name:
                    if (ValidateName(input, out var name, out message))
                    {
                        // property Name of profile state is name that uset typed 
                        profile.Name = name;
                        await turnContext.SendActivityAsync($"Hi {profile.Name}.", null, null, cancellationToken);
                        await turnContext.SendActivityAsync("How many kilos do you weigh?", null, null, cancellationToken);
                        flow.LastQuestionAsked = ConversationFlow.Question.Weight;
                        break;
                    }
                    else
                    {
                        // if validation return is false 
                        await turnContext.SendActivityAsync(message ?? "I'm sorry, I didn't understand that.", null, null, cancellationToken);
                        break;
                    }
                case ConversationFlow.Question.Weight:
                    if (ValidateWeight(input, out var weight, out message))
                    {
                        profile.Weight = weight;
                        await turnContext.SendActivityAsync($"I have your weight as {profile.Weight} kg.", null, null, cancellationToken);
                        await turnContext.SendActivityAsync("How tall are you?(met)", null, null, cancellationToken);
                        flow.LastQuestionAsked = ConversationFlow.Question.Height;
                        break;
                    }
                    else
                    {
                        await turnContext.SendActivityAsync(message ?? "I'm sorry, I didn't understand that.", null, null, cancellationToken);
                        break;
                    }

                case ConversationFlow.Question.Height:
                    if (ValidateDate(input, out var height, out message))
                    {
                        profile.Height = height;
                        double BMI = (profile.Weight)/(profile.Height * profile.Height);
                        await turnContext.SendActivityAsync($"Your BMI is: {BMI}");
                        await turnContext.SendActivityAsync($"How do you feel about Vegafood ???");
                        // await turnContext.SendActivityAsync($"Thanks for completing the support.");
                        //await turnContext.SendActivityAsync($"Type anything to run the bot again.");
                        flow.LastQuestionAsked = ConversationFlow.Question.None;
                        profile = new UserProfile();
                        flow.LastQuestionAsked = ConversationFlow.Question.Emotion;
                        break;
                    }
                    else
                    {
                        await turnContext.SendActivityAsync(message ?? "I'm sorry, I didn't understand that.", null, null, cancellationToken);
                        break;
                    }

                case ConversationFlow.Question.Emotion:
                    var predict = new PredictSentiment(input);
                    string result = predict.Predict();
                    await turnContext.SendActivityAsync($"Your sentiment is: {result}");
                    await turnContext.SendActivityAsync($"Thanks for completing the support.");
                    break;
                        //if (ValidateDate(input, out var height, out message))
                        //{
                        //    profile.Height = height;
                        //    float BMI = (profile.Weight) / (profile.Height * profile.Height);
                        //    await turnContext.SendActivityAsync($"Your BMI is: {BMI}");
                        //    await turnContext.SendActivityAsync($"How do you feel about Vegafood ???");
                        //    // await turnContext.SendActivityAsync($"Thanks for completing the support.");
                        //    //await turnContext.SendActivityAsync($"Type anything to run the bot again.");
                        //    flow.LastQuestionAsked = ConversationFlow.Question.None;
                        //    profile = new UserProfile();
                        //    flow.LastQuestionAsked = ConversationFlow.Question.Emotion;
                        //    break;
                        //}
                        //else
                        //{
                        //    await turnContext.SendActivityAsync(message ?? "I'm sorry, I didn't understand that.", null, null, cancellationToken);
                        //    break;
                        //}
                    
                    
                    //PS(input, turnContext);


                    //if (ValidateDate(input, out var height, out message))
                    //{
                    //    profile.Height = height;
                    //    float BMI = (profile.Weight) / (profile.Height * profile.Height);
                    //    await turnContext.SendActivityAsync($"Your BMI is: {BMI}");
                    //    await turnContext.SendActivityAsync($"Thanks for completing the support.");
                    //    await turnContext.SendActivityAsync($"Type anything to run the bot again.");
                    //    flow.LastQuestionAsked = ConversationFlow.Question.None;
                    //    profile = new UserProfile();
                    //    flow.LastQuestionAsked = ConversationFlow.Question.Emotion;
                    //    break;
                    //}
                    //else
                    //{
                    //    await turnContext.SendActivityAsync(message ?? "I'm sorry, I didn't understand that.", null, null, cancellationToken);
                    //    break;
                    //}
            }
        }

        private async static void PS( string input, ITurnContext turnContext)
        {
            var predict = new PredictSentiment(input);
            string result = predict.Predict();
            await turnContext.SendActivityAsync($"Your sentiment is: {result}");
            await turnContext.SendActivityAsync($"Thanks for completing the support.");
        }
        private static bool ValidateName(string input, out string name, out string message)
        {
            name = null;
            message = null;

            if (string.IsNullOrWhiteSpace(input))
            {
                message = "Please enter a name that contains at least one character.";
            }
            else
            {
                name = input.Trim();
            }

            return message is null;
        }

        private static bool ValidateWeight(string input, out double weight, out string message)
        {
            weight = 0;
            message = null;

            // Try to recognize the input as a number. This works for responses such as "twelve" as well as "12".
            try
            {
                // Attempt to convert the Recognizer result to an integer. This works for "a dozen", "twelve", "12", and so on.
                // The recognizer returns a list of potential recognition results, if any.

                var results = NumberRecognizer.RecognizeNumber(input, Culture.English);

                foreach (var result in results)
                {
                    // The result resolution is a dictionary, where the "value" entry contains the processed string.
                    if (result.Resolution.TryGetValue("value", out var value))
                    {
                        //double a = 6.6;


                        var x = value;
                        weight = (float)value;
                        var a = weight;
                        if (weight > 0)
                        {
                            return true;
                        }
                    }
                }

                message = "Please enter an weight greater than 0. ";
            }
            catch
            {
                message = "I'm sorry, I could not interpret that as an age. Please enter an age between 18 and 120.";
            }

            return message is null;
        }

        private static bool ValidateDate(string input, out float height, out string message)
        {
            height = 1;
            message = null;

            // Try to recognize the input as a date-time. This works for responses such as "11/14/2018", "9pm", "tomorrow", "Sunday at 5pm", and so on.
            // The recognizer returns a list of potential recognition results, if any.
            try
            {
                // var results = DateTimeRecognizer.RecognizeDateTime(input, Culture.English);

                // Check whether any of the recognized date-times are appropriate,
                // and if so, return the first appropriate date-time. We're checking for a value at least an hour in the future.
                //var earliest = DateTime.Now.AddHours(1.0);

                var results = NumberRecognizer.RecognizeNumber(input, Culture.English);

                foreach (var result in results)
                {
                    // The result resolution is a dictionary, where the "value" entry contains the processed string.
                    if (result.Resolution.TryGetValue("value", out var value))
                    {
                        height = (float)value;
                        if (height > 0)
                        {
                            return true;
                        }
                    }
                }

                message = "Please enter an height greater than 0. ";

               
            }
            catch
            {
                message = "I'm sorry, I could not interpret that as an height. Please enter a height  greater than 0.";
            }

            return false;
        }
        private static bool ValidateEmotuon(string input, out string emotion, out string message)
        {
            emotion = "";
            message = null;
            try
            {
                var results = NumberRecognizer.RecognizeNumber(input, Culture.English);

                foreach (var result in results)
                {
                    // The result resolution is a dictionary, where the "value" entry contains the processed string.
                    if (result.Resolution.TryGetValue("value", out var value))
                    {
                        emotion = Convert.ToString(value);
                        if (emotion.Length>0 )
                        {
                            return true;
                        }
                    }
                }

                message = "Please enter your emotion.";


            }
            catch
            {
                message = "I'm sorry, I could not interpret that as an emotion. Please enter your emotion about us.";
            }

            return false;
        }
    }
}
