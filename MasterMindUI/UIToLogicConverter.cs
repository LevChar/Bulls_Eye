using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MastemindUi
{
    public class UIToLogicConverter
    {
        private readonly Dictionary<Color, char> k_ConvertionTable = new Dictionary<Color, char>();
        private readonly char k_BaseChar = 'A';
        private Mastermind.Logic.BoardLogic game;

        public UIToLogicConverter(List<Color> i_InputToConvert, byte i_AmountOfGuesses)
        {
            game = new Mastermind.Logic.BoardLogic(i_AmountOfGuesses);

            for (int i = 0; i < i_InputToConvert.Count; i++)
            {
                k_ConvertionTable.Add(i_InputToConvert[i], (char)(k_BaseChar + i));
            }
        }

        private char[] convertFromColorToCharBasis(List<Color> i_InputOfColorsToConvert)
        {
            char[] resultOfConvertion = new char[i_InputOfColorsToConvert.Count];

            for (int i = 0; i < i_InputOfColorsToConvert.Count; i++)
            {
                if (!k_ConvertionTable.TryGetValue(i_InputOfColorsToConvert[i], out char tempCalculatedChar))
                {
                    resultOfConvertion[i] = ' ';
                }
                else
                {
                    resultOfConvertion[i] = tempCalculatedChar;
                }
            }

            return resultOfConvertion;
        }

        private string convertFromColorArrayTostring(List<Color> i_InputOfColorsToConvert)
        {
            return new string(convertFromColorToCharBasis(i_InputOfColorsToConvert));
        }

        private Color[] parseFromStringToColor(string i_StringRepresentation)
        {
            Color[] userGuessCheck = new Color[i_StringRepresentation.Length];
            game.ProccessUserInput(i_StringRepresentation);

            byte correctGuessInplace = game.PinsHistory[game.GuessesCounter - 1].AmountOfRightGuessInPlace;
            byte correctGuessNotInplace = game.PinsHistory[game.GuessesCounter - 1].AmountOfRightGuessNotInPlace;
            
            for (int i = 0; i < i_StringRepresentation.Length; i++)
            {
                if (i < correctGuessInplace)
                {
                    userGuessCheck[i] = Color.Black;
                }
                else if (i >= correctGuessInplace && (i - correctGuessInplace) < correctGuessNotInplace)
                {
                    userGuessCheck[i] = Color.Yellow;
                }
                else
                {
                    userGuessCheck[i] = Control.DefaultBackColor;
                }
            }

            return userGuessCheck;
        }

        public Color[] CalculateUserGuess(List<Color> i_InputOfColorsToConvert)
        {
            Color[] resultOfUserGuess = new Color[i_InputOfColorsToConvert.Count];
            string userGuessAsString = convertFromColorArrayTostring(i_InputOfColorsToConvert);
            return parseFromStringToColor(userGuessAsString);
        }

        public Color[] GetTarget()
        {
            return convertFromCharToColor(game.PinsTarget.Pins);
        }

        private Color[] convertFromCharToColor(char[] i_InputOfCharsToConvert)
        {
            Color[] resultOfConvertion = new Color[i_InputOfCharsToConvert.Length];

            for (int i = 0; i < i_InputOfCharsToConvert.Length; i++)
            {
                foreach(KeyValuePair<Color, char> colorCharPair in k_ConvertionTable)
                {
                    if(i_InputOfCharsToConvert[i] == colorCharPair.Value)
                    {
                        resultOfConvertion[i] = colorCharPair.Key;
                    }
                }
            }

            return resultOfConvertion;
        }

        public bool CehckIfGameFinished()
        {
            bool isGameFinished = false;
            if(game.CurrentGameStatus == Mastermind.Logic.eGameStatus.GameLost || 
                game.CurrentGameStatus == Mastermind.Logic.eGameStatus.GameWon)
            {
                isGameFinished = true;
            }

            return isGameFinished;
        }
    }
}