namespace MastemindUi
{
    public partial class AmountOfGuessesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numberOfGuessesButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // numberOfGuessesButton
            // 
            this.numberOfGuessesButton.Location = new System.Drawing.Point(21, 21);
            this.numberOfGuessesButton.Margin = new System.Windows.Forms.Padding(12);
            this.numberOfGuessesButton.Name = "numberOfGuessesButton";
            this.numberOfGuessesButton.Size = new System.Drawing.Size(286, 41);
            this.numberOfGuessesButton.TabIndex = 0;
            this.numberOfGuessesButton.Text = "Number of chances: 4";
            this.numberOfGuessesButton.UseVisualStyleBackColor = true;
            this.numberOfGuessesButton.Click += new System.EventHandler(this.numberOfGuessesButton_Click);
            // 
            // startButton
            // 
            this.startButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.startButton.Location = new System.Drawing.Point(192, 119);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(115, 34);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // AmountOfGuessesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 172);
            this.Controls.Add(this.numberOfGuessesButton);
            this.Controls.Add(this.startButton);
            this.Name = "AmountOfGuessesForm";
            this.Text = "AmountOfGuessesForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button numberOfGuessesButton;
        private System.Windows.Forms.Button startButton;
    }
}