using System;
using System.Configuration;

namespace BowlingExample
{
    class ConsoleReadThrowManager : IThrowManager
    {
        private int numberOfPinsToKnockDown;

        public ConsoleReadThrowManager() { 
            numberOfPinsToKnockDown = Convert.ToInt32(ConfigurationManager.AppSettings.Get("MaximumPins"));
        }

        public void SetNumberOfPinsToKnockDown(int numberOfPinsToKnockDown)
        {
            this.numberOfPinsToKnockDown = numberOfPinsToKnockDown;
        }

        public int Throw()
        {
            int numberOfKnockedDownPins;
            string consoleReadLine = Console.ReadLine();
            bool isValidInput = int.TryParse(consoleReadLine, out numberOfKnockedDownPins);

            if (!isValidInput || numberOfKnockedDownPins > this.numberOfPinsToKnockDown)
                numberOfKnockedDownPins = GetNumberOfKnockedDownPinsFromConsole();

            this.numberOfPinsToKnockDown -= numberOfKnockedDownPins;
            return numberOfKnockedDownPins;
        }

        private int GetNumberOfKnockedDownPinsFromConsole()
        {
            bool isValidInput = false;
            int numberOfKnockedDownPins = 0;

            while (!isValidInput || numberOfKnockedDownPins > this.numberOfPinsToKnockDown)
            {
                Console.WriteLine("Invalid input. You can't knock down more than " + this.numberOfPinsToKnockDown + " in this throw. Try again.");
                string consoleReadLine = Console.ReadLine();
                isValidInput = int.TryParse(consoleReadLine, out numberOfKnockedDownPins);
               
            }
            return numberOfKnockedDownPins;
        }
    }
}
