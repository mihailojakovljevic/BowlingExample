
namespace BowlingExample
{
    public class LastFrame: Frame
    {

        private int extraThrowPins;
        public LastFrame(int firstThrowPins, int secondThrowPins, int extraThrowPins) : base(firstThrowPins, secondThrowPins)
        {
            this.extraThrowPins = extraThrowPins;
            Score += extraThrowPins;
        }

        override
       public string ToString()
        {
            string frameString = "First throw: " + firstThrowPins + ", Second Throw: " + secondThrowPins + ", Extra Throw: " + extraThrowPins;
            return frameString + ", Final score: " + Score;
        }
    }
}
