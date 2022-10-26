using System;
using System.Collections.Generic;
using System.Configuration;

namespace BowlingExample
{
    public class Game
    {
        private List<Frame> frames = new List<Frame>();
        private int i = 0;
        private int numberOfFrames;
        private int maximumPins;
        private int firstThrowPins;
        private int secondThrowPins;
        private int thirdThrowPins;
        private IThrowManager _throwManager;

        public Game(IThrowManager throwManager)
        {
            _throwManager = throwManager;
            numberOfFrames =  Convert.ToInt32(ConfigurationManager.AppSettings.Get("NumberOfFrames"));
            maximumPins = Convert.ToInt32(ConfigurationManager.AppSettings.Get("MaximumPins"));
        }

        public void Play() {
            for (i = 0; i < numberOfFrames; i++) {
                _throwManager.SetNumberOfPinsToKnockDown(maximumPins);

                Console.WriteLine("First throw of " + (i + 1) + ".frame:"); ;
                firstThrowPins = _throwManager.Throw();

                if (IsStrike() && !IsLastFrame())
                {
                    Frame currentFrame = new Frame(firstThrowPins, 0);
                    frames.Add(currentFrame);
                    if (i != 0)
                        currentFrame.SetPreviousFrame(frames[i - 1]);
                    continue;
                }

                if (IsStrike() && IsLastFrame())
                {
                    _throwManager.SetNumberOfPinsToKnockDown(maximumPins);
                }

                Console.WriteLine("Second throw of " + (i + 1) + ". frame:");
                secondThrowPins = _throwManager.Throw();

                if (!IsLastFrame())
                {
                    Frame currentFrame = new Frame(firstThrowPins, secondThrowPins);
                    frames.Add(currentFrame);
                    if (i != 0)
                        currentFrame.SetPreviousFrame(frames[i - 1]); 
                }


                if (IsLastFrame())
                {
                    if (ShouldHaveExtraThrow())
                    {
                        _throwManager.SetNumberOfPinsToKnockDown(maximumPins);
                        Console.WriteLine("Third throw of " + (i + 1) + ". frame:");
                        thirdThrowPins = _throwManager.Throw();
                    }
                        
                    LastFrame currentFrame = new LastFrame(firstThrowPins, secondThrowPins, thirdThrowPins);
                    frames.Add(currentFrame);
                    currentFrame.SetPreviousFrame(frames[i - 1]);

                }
            }

            DisplayInfoAboutGame();
            Console.WriteLine("Game is over. Your result is: " + frames[numberOfFrames - 1].Score);

        }

        private bool ShouldHaveExtraThrow()
        {
            return (secondThrowPins == maximumPins) ||  IsSpare();
        }

        private bool IsSpare()
        {
            return (firstThrowPins + secondThrowPins) == maximumPins;
        }

        private bool IsStrike()
        {
            return firstThrowPins == maximumPins;
        }

        private bool IsLastFrame()
        {
            return i == (numberOfFrames - 1);
        }

        private void DisplayInfoAboutGame()
        {
            int frameNumber = 1;
            foreach(Frame frame in frames)
            {
                Console.WriteLine(frameNumber + ". frame:");
                Console.WriteLine(frame);
                frameNumber++;
            }
        }
    }
}
