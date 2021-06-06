using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBot2.SaveInfor
{
    // Defines a state property used to track information about the user.
    public class UserProfile
    {
        public string Name { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public string Emotion { get; set; }
    }
}
