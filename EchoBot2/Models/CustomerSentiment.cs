using System;
using System.Collections.Generic;

#nullable disable

namespace EchoBot2.Models
{
    public partial class CustomerSentiment
    {
        public int Id { get; set; }
        public TimeSpan? Time { get; set; }
        public string VegaPredict { get; set; }
        public string VegaComment { get; set; }
        public string FoodPredict { get; set; }
        public string FoodComment { get; set; }
        public string ServicePredict { get; set; }
        public string ServiceComment { get; set; }
        public string UserName { get; set; }
    }
}
