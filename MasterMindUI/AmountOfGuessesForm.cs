using System;
using System.Windows.Forms;

namespace MastemindUi
{
    public partial class AmountOfGuessesForm : Form
    {
        private string maxGuessesMessage = "Cannot exceed 10 guesss";

        public byte AmountOfGuesses { get; private set; } = 4;

        public AmountOfGuessesForm()
        {
            InitializeComponent();
        }

        private void numberOfGuessesButton_Click(object sender, EventArgs e)
        {
            if(AmountOfGuesses < 10)
            {
                this.numberOfGuessesButton.Text = string.Format(@"Number of chances: {0} ", ++AmountOfGuesses);
            }
            else
            {
                MessageBox.Show(maxGuessesMessage);
            }
        }
    }
}
