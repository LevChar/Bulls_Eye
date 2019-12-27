using System;
using System.Collections.Generic;

namespace Mastermind.Logic
{
    public class BoardLogic
    {
        // Private Data Members
        private FourLetterPins m_PinsTarget;                                   
        private Random m_random = new Random();
        
        // Properties
        public byte MaxAmountOfGuesses { get; private set; }                  

        public FourLetterPins[] PinsHistory { get; private set; }             

        public eGameStatus CurrentGameStatus { get; set; }                    

        public byte GuessesCounter { get; private set; }                      

        public FourLetterPins PinsTarget
        {
            get
            {
                FourLetterPins result = null;

                if(CurrentGameStatus == eGameStatus.GameWon || CurrentGameStatus == eGameStatus.GameLost)
                {
                    result = m_PinsTarget;
                }

                return result;
            }
        }

        // C'tor - gets the amount from UI
        public BoardLogic(byte i_MaxAmountOfGuesses)
        {
            GuessesCounter = 0;
            MaxAmountOfGuesses = i_MaxAmountOfGuesses;
            PinsHistory = new FourLetterPins[MaxAmountOfGuesses];
            initPinsAndResultArrays();
            makeNewPinsTarget();
            CurrentGameStatus = eGameStatus.Play;
        }

        private void initPinsAndResultArrays()
        {
            for(int i = 0; i < MaxAmountOfGuesses; i++)
            {
                PinsHistory[i] = new FourLetterPins(new char[GameLogicConsts.k_LengthOfGuess]);
            }
        }

        private void makeNewPinsTarget()
        {
            char[] fourRandomCharsArray = new char[GameLogicConsts.k_LengthOfGuess];
            List<byte> fourRandomNumbersList = GetRandomNumbersBetween0and8(GameLogicConsts.k_LengthOfGuess);
            byte Itr = 0;

            foreach(int randomNumber in fourRandomNumbersList)
            {
                fourRandomCharsArray[Itr++] = (char)((byte)'A' + randomNumber);
            }
            
            m_PinsTarget = new FourLetterPins(fourRandomCharsArray);
        }

        public eInputType ProccessUserInput(string i_UserInput)
        {               
            eInputType inputType = eInputType.ValidFourLetterChars;
            char[] fourLettersArray = new char[GameLogicConsts.k_LengthOfGuess];
            int pinLetterPosition = 0;

            if (i_UserInput == GameLogicConsts.k_Exit)
            {
                CurrentGameStatus = eGameStatus.QuitGame;
            }
            else
            {
                if (i_UserInput.Length != GameLogicConsts.k_LengthOfGuess)
                {
                    inputType = eInputType.WrongAmountOfChars;
                }
                else
                { 
                    foreach (char ItrCh in i_UserInput)
                    {
                        if (ItrCh < GameLogicConsts.k_LowerBoundOfGuessLatter || ItrCh > GameLogicConsts.k_HigherBoundOfGuessLatter)
                        {
                            inputType = eInputType.OutOfRangeChars;
                            break;
                        }

                        fourLettersArray[pinLetterPosition++] = ItrCh;
                    }
                }
            }

            if(inputType == eInputType.ValidFourLetterChars)
            {
                PinsHistory[GuessesCounter].Pins = fourLettersArray;
                
                if (proccessCurrentUserGuess())
                {
                    CurrentGameStatus = eGameStatus.GameWon;
                }
                else
                {
                    if(GuessesCounter == MaxAmountOfGuesses - 1)
                    {
                        CurrentGameStatus = eGameStatus.GameLost;
                    }
                }

                GuessesCounter++;
            }

            return inputType;
        }

        private bool proccessCurrentUserGuess()
        {
            bool userGuessedCorrectly = false;

            for(byte i = 0; i < 4; i++)
            {
                if(PinsHistory[GuessesCounter].Pins[i] == m_PinsTarget.Pins[i])
                {
                    PinsHistory[GuessesCounter].AmountOfRightGuessInPlace++;
                }
            }

            if(PinsHistory[GuessesCounter].AmountOfRightGuessInPlace != 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (PinsHistory[GuessesCounter].Pins[i] == m_PinsTarget.Pins[j] && i != j)
                        {
                            PinsHistory[GuessesCounter].AmountOfRightGuessNotInPlace++;
                        }
                    }
                }
            }
            else
            {
                userGuessedCorrectly = true;
            }

            return userGuessedCorrectly;
        }

        // generate list of 4 different numbers between 0 and 8
        private List<byte> GetRandomNumbersBetween0and8(byte i_Count)
        {
            List<byte> randomNumbers = new List<byte>();

            for (byte i = 0; i < i_Count; i++)
            {
                byte number;

                do
                {
                    number = (byte)m_random.Next(8);
                }
                while (randomNumbers.Contains(number));

                randomNumbers.Add(number);
            }

            return randomNumbers;
        }
    }
}