using System;
using System.Configuration;

namespace BowlingExample
{
    public class Frame
    {
        private int maximumPins;
        protected int firstThrowPins;
        protected int secondThrowPins;

        public int Score { get; set; }

        public Frame() {
            maximumPins = Convert.ToInt32(ConfigurationManager.AppSettings.Get("MaximumPins"));
        }


        public Frame(int firstThrowPins, int secondThrowPins): this()
        {
            this.firstThrowPins = firstThrowPins;
            this.secondThrowPins = secondThrowPins;
            Score += firstThrowPins + secondThrowPins;
        }

        public void SetPreviousFrame(Frame previousFrame)
        {
            ApplyBonusTo(previousFrame);
            Score += previousFrame.Score;
        }

        private void ApplyBonusTo(Frame previousFrame)
        {
            if (previousFrame.IsSpare())
                previousFrame.Score += firstThrowPins;
            if (previousFrame.IsStrike())
                previousFrame.Score += firstThrowPins + secondThrowPins;
        }

        public bool IsSpare()
        {
            return (firstThrowPins < maximumPins && (firstThrowPins + secondThrowPins == maximumPins));
        }

        public bool IsStrike()
        {
            return firstThrowPins == maximumPins;
        }

        override
        public string ToString()
        {
            string frameString = "First throw: " + firstThrowPins + ", Second Throw: " + secondThrowPins;
            return frameString + ", Final score: " + Score;
        }
    }
}
