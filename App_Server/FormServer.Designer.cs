namespace App_Server
{
    partial class FormServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonStart = new Button();
            textBoxInput = new TextBox();
            textBoxOutput = new TextBox();
            button2 = new Button();
            listBoxClients = new ListBox();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(10, 9);
            buttonStart.Margin = new Padding(3, 2, 3, 2);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(82, 22);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(10, 44);
            textBoxInput.Margin = new Padding(3, 2, 3, 2);
            textBoxInput.Multiline = true;
            textBoxInput.Name = "textBoxInput";
            textBoxInput.ReadOnly = true;
            textBoxInput.Size = new Size(165, 156);
            textBoxInput.TabIndex = 1;
            // 
            // textBoxOutput
            // 
            textBoxOutput.Location = new Point(10, 203);
            textBoxOutput.Margin = new Padding(3, 2, 3, 2);
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.Size = new Size(274, 23);
            textBoxOutput.TabIndex = 2;
            // 
            // button2
            // 
            button2.Location = new Point(290, 203);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(82, 22);
            button2.TabIndex = 3;
            button2.Text = "Send";
            button2.UseVisualStyleBackColor = true;
            button2.Click += buttonSend_Click;
            // 
            // listBoxClients
            // 
            listBoxClients.FormattingEnabled = true;
            listBoxClients.Location = new Point(250, 65);
            listBoxClients.Name = "listBoxClients";
            listBoxClients.Size = new Size(120, 94);
            listBoxClients.TabIndex = 4;
            // 
            // FormServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 232);
            Controls.Add(listBoxClients);
            Controls.Add(button2);
            Controls.Add(textBoxOutput);
            Controls.Add(textBoxInput);
            Controls.Add(buttonStart);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormServer";
            Text = "Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStart;
        private TextBox textBoxInput;
        private TextBox textBoxOutput;
        private Button button2;
        private ListBox listBoxClients;
    }
}
