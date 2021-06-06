using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBot2.PromptBot
{
    public class ConversationFlow
    {
        // Identifies the last question asked.
        public enum Question
        {
            Name,
            Emotion,
            Height,
            Weight,
            None,
            Choose
        }

        public enum BMI
        {
            Weight,
            Height,
            None
        }
        public enum Choose
        {
            None,
            Consulting,
            Product,
            
        }
        public enum Emotion
        {
            None,
            Service,
            Product,
            End,
            Thanks
        }
        public string Input;
        // The last question asked.
        public Question LastQuestionAsked { get; set; } = Question.Name;
        public BMI Calculation { get; set; } = BMI.None;
        public Choose UserChoose { get; set; } = Choose.None;
        public Emotion UserEmotion { get; set; } = Emotion.None;
    }
}
