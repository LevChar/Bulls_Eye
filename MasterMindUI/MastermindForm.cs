using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MastemindUi
{
    public partial class MastermindForm : Form
    {
        private LimitedColorsDialog m_LimitedColorsDialog;
        private byte m_AmountOfGuesses;
        private Button[] m_PinsTargetButtonsArray;
        private Button[,] m_PinButtonsArray;
        private Button[] m_ArrowControlButtonsArray;
        private Button[,] m_PinResultsButtonsArray;
        private byte m_CurrentGuessItr = 0;
        private List<Color> m_PossibleColorsArray = new List<Color>();
        private UIToLogicConverter m_Converter;

        public Button ButtonClicked { get; private set; }

        public MastermindForm()
        {
            m_AmountOfGuesses = setAmountOfGuessesDialog();
            m_PinsTargetButtonsArray = new Button[4];
            m_PinButtonsArray = new Button[m_AmountOfGuesses, 4];
            m_ArrowControlButtonsArray = new Button[m_AmountOfGuesses];
            m_PinResultsButtonsArray = new Button[m_AmountOfGuesses, 4];
            InitializeComponent();
            setBoard();
            m_LimitedColorsDialog = new LimitedColorsDialog();
            setPossibleColors();
            m_Converter = new UIToLogicConverter(m_PossibleColorsArray, m_AmountOfGuesses);
        }

        private void setPossibleColors()
        {
            foreach(Button buttonItr in m_LimitedColorsDialog.Controls)
            {
                m_PossibleColorsArray.Add(buttonItr.BackColor);
            }
        }

        private byte setAmountOfGuessesDialog()
        {
            AmountOfGuessesForm amountOfGuessesForm = new AmountOfGuessesForm();
            amountOfGuessesForm.ShowDialog();

            return amountOfGuessesForm.AmountOfGuesses;
        }

        private void setBoard()
        {
            SuspendLayout();
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(448, 129 + (66 * m_AmountOfGuesses));

            for(byte i = 0; i < 4; i++)
            {
                m_PinsTargetButtonsArray[i] = new Button
                {
                    BackColor = Color.Black,
                    Enabled = false,
                    Location = new Point(21 + (66 * i), 21),
                    Size = new Size(60, 60),
                    UseVisualStyleBackColor = false
                };

                Controls.Add(m_PinsTargetButtonsArray[i]);
            }

            for (byte i = 0; i < m_AmountOfGuesses; i++)
            {
                for(byte j = 0; j < 4; j++)
                {
                    m_PinButtonsArray[i, j] = new Button
                    {
                        AllowDrop = true,
                        Location = new Point(21 + (66 * j), 108 + (66 * i)),
                        Name = ("pinButton" + j) + i,
                        Size = new Size(60, 60),
                        TabIndex = i * j,
                        UseVisualStyleBackColor = true,
                        Enabled = false
                    };

                    m_PinButtonsArray[i, j].Click += new EventHandler(pinButton_Click);
                    Controls.Add(m_PinButtonsArray[i, j]);
                }

                m_ArrowControlButtonsArray[i] = new Button
                {
                    Location = new Point(285, 127 + (66 * i)),
                    Name = "arrowButton" + i,
                    Size = new Size(75, 23),
                    TabIndex = 44,
                    Text = "-->>",
                    UseVisualStyleBackColor = true,
                    Enabled = false
                };

                m_ArrowControlButtonsArray[i].Click += new EventHandler(arrowControlButton_Click);
                Controls.Add(m_ArrowControlButtonsArray[i]);

                byte m_PinResultsButtonsArrayItr = 0;
                for (byte j = 0; j < 2; j++)
                {
                    for(byte m = 0; m < 2; m++)
                    {
                        m_PinResultsButtonsArray[i, m_PinResultsButtonsArrayItr] = new Button
                        {
                            Location = new Point(375 + (31 * m), 108 + (66 * i) + (31 * j)),
                            Size = new Size(25, 25),
                            Enabled = false
                        };
                        Controls.Add(m_PinResultsButtonsArray[i, m_PinResultsButtonsArrayItr]);
                        m_PinResultsButtonsArrayItr++;
                    }
                }
            }

            ResumeLayout(false);
        }

        private void pinButton_Click(object i_Sender, EventArgs i_E)
        {
            ButtonClicked = i_Sender as Button;
            bool isRowComplete = true;

            m_LimitedColorsDialog.ShowDialog(this);
            if(m_LimitedColorsDialog.DialogResult == DialogResult.OK)
            {
                ButtonClicked.BackColor = m_LimitedColorsDialog.LastColorSelected;
                for (int i = 0; i < 4; i++)
                {
                    if (m_PinButtonsArray[m_CurrentGuessItr, i].BackColor == DefaultBackColor)
                    {
                        isRowComplete = false;
                        break;
                    }
                }

                m_ArrowControlButtonsArray[m_CurrentGuessItr].Enabled = isRowComplete;
            }
        }

        private void arrowControlButton_Click(object i_Sender, EventArgs i_E)
        {
            if (m_CurrentGuessItr < m_AmountOfGuesses)
            {
                proccessCurrentGuesssByLogic();
                m_CurrentGuessItr++;
                enableRowOfPins();
            }

            m_LimitedColorsDialog.Reset();

            (i_Sender as Button).Enabled = false;
        }

        private void proccessCurrentGuesssByLogic()
        {
            List<Color> currentGuessedColors = new List<Color>();

            for(byte i = 0; i < 4; i++)
            {
                currentGuessedColors.Add(m_PinButtonsArray[m_CurrentGuessItr, i].BackColor);
            }

            Color[] result = m_Converter.CalculateUserGuess(currentGuessedColors);
            updatedCurrentResult(result);
        }

        private void updatedCurrentResult(Color[] i_Result)
        {
            byte countCorrectResults = 0;

            for(int i = 0; i < 4; i++)
            {
                m_PinResultsButtonsArray[m_CurrentGuessItr, i].BackColor = i_Result[i];
                if(i_Result[i] == Color.Yellow)
                {
                    countCorrectResults++;
                }
            }

            if(m_Converter.CehckIfGameFinished())
            {
                Color[] targetColors = m_Converter.GetTarget();
                for (int i = 0; i < 4; i++)
                {
                    m_PinsTargetButtonsArray[i].BackColor = targetColors[i];
                }
            }
        }

        private void mastermindForm_Shown(object i_Sender, EventArgs i_E)
        {
            enableRowOfPins();
        }

        private void enableRowOfPins()
        {
            if(m_CurrentGuessItr < m_AmountOfGuesses)
            {
                for (int i = 0; i < 4; i++)
                {
                    m_PinButtonsArray[m_CurrentGuessItr, i].Enabled = true;
                }
            }
        }
    }
}
