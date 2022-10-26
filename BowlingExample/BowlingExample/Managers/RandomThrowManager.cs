using System;
using System.Configuration;

namespace BowlingExample
{
    class RandomThrowManager : IThrowManager
    {
        private static Random rand = new Random();
        private int numberOfPinsToKnockDown;
       
        public RandomThrowManager() {
            numberOfPinsToKnockDown = Convert.ToInt32(ConfigurationManager.AppSettings.Get("MaximumPins"));
        }

        public void SetNumberOfPinsToKnockDown(int numberOfPinsToKnockDown)
        {
            this.numberOfPinsToKnockDown = numberOfPinsToKnockDown;
        }

        public int Throw()
        {
            int numberOfKnockedDownPins = rand.Next(0, numberOfPinsToKnockDown + 1);
            this.numberOfPinsToKnockDown -= numberOfKnockedDownPins;
            return numberOfKnockedDownPins;
        }
    }
}
