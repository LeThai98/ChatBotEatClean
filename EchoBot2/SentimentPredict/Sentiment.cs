using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBot2.SentimentPredict
{
    public static class Sentiment
    {
        public static TimeSpan? Time { get; set; }
        public static string VegaPredict { get; set; }
        public static string VegaComment { get; set; }
        public static string FoodPredict { get; set; }
        public static string FoodComment { get; set; }
        public static string ServicePredict { get; set; }
        public static string ServiceComment { get; set; }
        public static string UserName { get; set; }
        public static bool Check { get; set; } = true;
        public static bool End { get; set; } = true;
    }
}
