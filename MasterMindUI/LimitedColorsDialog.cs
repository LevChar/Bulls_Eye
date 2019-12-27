using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MastemindUi
{
    public partial class LimitedColorsDialog : Form
    {
        // private Color[] m_ColorsSelectedArray = new Color[4];
        // public Button SelectedButton { get; private set; }
        public Color LastColorSelected { get; private set; }

        public LimitedColorsDialog()
        {
            InitializeComponent();
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            Button senderButton = sender as Button;
            LastColorSelected = senderButton.BackColor;
            senderButton.Enabled = false;
        }

        private void LimitedColorsDialog_Shown(object sender, EventArgs e)
        {
            MastermindForm parentForm = Owner as MastermindForm;
            if (parentForm.ButtonClicked != null)
            {
                if (parentForm.ButtonClicked.BackColor != null)
                {
                    enableButtonByColor(parentForm.ButtonClicked.BackColor);
                }
            }
        }

        private void enableButtonByColor(Color i_BackColor)
        {
            foreach(Button buttonItr in Controls)
            {
                if(buttonItr.BackColor == i_BackColor)
                {
                    buttonItr.Enabled = true;
                }
            }
        }

        public void Reset()
        {
            foreach (Button buttonItr in Controls)
            {
                buttonItr.Enabled = true;
            }
        }
    }
}
