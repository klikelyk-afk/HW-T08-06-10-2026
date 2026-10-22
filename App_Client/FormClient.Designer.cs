namespace App_Client
{
    partial class FormClient
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
            this.buttonConnect = new Button();
            this.buttonSend = new Button();
            this.textBoxOutput = new TextBox();
            this.textBoxInput = new TextBox();
            SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new Point(12, 270);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new Size(94, 29);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += this.buttonConnect_Click;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new Point(331, 270);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new Size(94, 29);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += this.buttonSend_Click;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new Point(112, 271);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new Size(200, 27);
            this.textBoxOutput.TabIndex = 2;
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(12, 12);
            textBoxInput.Multiline = true;
            textBoxInput.Name = "textBoxInput";
            textBoxInput.ReadOnly = true;
            textBoxInput.Size = new Size(413, 251);
            textBoxInput.TabIndex = 3;
            // 
            // FormClient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(437, 310);
            Controls.Add(textBoxInput);
            Controls.Add(this.textBoxOutput);
            Controls.Add(this.buttonSend);
            Controls.Add(this.buttonConnect);
            Name = "FormClient";
            Text = "Client";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonConnect;
        private Button buttonSend;
        private TextBox textBoxInput;
        private TextBox textBoxOutput;
    }
}
