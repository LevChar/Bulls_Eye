using System;

namespace Mastermind.Logic
{
    public class FourLetterPins
    {
        private byte m_AmountOfRightGuessInPlace;
        private byte m_AmountOfRightGuessNotInPlace;

        public char[] Pins { get; set; }

        public FourLetterPins(char[] i_Pins)
        {
            Pins = i_Pins;
            m_AmountOfRightGuessInPlace = 0;
            m_AmountOfRightGuessNotInPlace = 0;
        }

        public byte AmountOfRightGuessInPlace
            {
                get
                {
                    return m_AmountOfRightGuessInPlace;
                }

                set
                {
                    m_AmountOfRightGuessInPlace = value;
                }
            }

        public byte AmountOfRightGuessNotInPlace
        {
            get
            {
                return m_AmountOfRightGuessNotInPlace;
            }

            set
            {
                m_AmountOfRightGuessNotInPlace = value;
            }
        }
    }
}