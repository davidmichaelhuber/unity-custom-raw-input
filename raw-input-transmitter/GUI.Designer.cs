namespace raw_input_transmitter
{
    partial class GUI
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
            this.button_startListening = new System.Windows.Forms.Button();
            this.rtb_console = new System.Windows.Forms.RichTextBox();
            this.button_stopListening = new System.Windows.Forms.Button();
            this.textBox_lastX = new System.Windows.Forms.TextBox();
            this.textBox_lastY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_mbutton = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_mwheel = new System.Windows.Forms.TextBox();
            this.label_pipeConnectionStatus = new System.Windows.Forms.Label();
            this.textBox_pipeConnectionStatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_startListening
            // 
            this.button_startListening.Location = new System.Drawing.Point(12, 12);
            this.button_startListening.Name = "button_startListening";
            this.button_startListening.Size = new System.Drawing.Size(641, 34);
            this.button_startListening.TabIndex = 0;
            this.button_startListening.Text = "Start Listening";
            this.button_startListening.UseVisualStyleBackColor = true;
            this.button_startListening.Click += new System.EventHandler(this.button_startListening_Click);
            // 
            // rtb_console
            // 
            this.rtb_console.Location = new System.Drawing.Point(12, 92);
            this.rtb_console.Name = "rtb_console";
            this.rtb_console.Size = new System.Drawing.Size(641, 237);
            this.rtb_console.TabIndex = 1;
            this.rtb_console.Text = "";
            // 
            // button_stopListening
            // 
            this.button_stopListening.Location = new System.Drawing.Point(12, 52);
            this.button_stopListening.Name = "button_stopListening";
            this.button_stopListening.Size = new System.Drawing.Size(641, 34);
            this.button_stopListening.TabIndex = 2;
            this.button_stopListening.Text = "Stop Listening";
            this.button_stopListening.UseVisualStyleBackColor = true;
            this.button_stopListening.Click += new System.EventHandler(this.button_stopListening_Click);
            // 
            // textBox_lastX
            // 
            this.textBox_lastX.Location = new System.Drawing.Point(120, 338);
            this.textBox_lastX.Name = "textBox_lastX";
            this.textBox_lastX.Size = new System.Drawing.Size(36, 20);
            this.textBox_lastX.TabIndex = 3;
            // 
            // textBox_lastY
            // 
            this.textBox_lastY.Location = new System.Drawing.Point(208, 338);
            this.textBox_lastY.Name = "textBox_lastY";
            this.textBox_lastY.Size = new System.Drawing.Size(36, 20);
            this.textBox_lastY.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Last X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 341);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Last Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 341);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Button:";
            // 
            // textBox_mbutton
            // 
            this.textBox_mbutton.Location = new System.Drawing.Point(297, 338);
            this.textBox_mbutton.Name = "textBox_mbutton";
            this.textBox_mbutton.Size = new System.Drawing.Size(169, 20);
            this.textBox_mbutton.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(472, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Mouse Wheel:";
            // 
            // textBox_mwheel
            // 
            this.textBox_mwheel.Location = new System.Drawing.Point(554, 338);
            this.textBox_mwheel.Name = "textBox_mwheel";
            this.textBox_mwheel.Size = new System.Drawing.Size(36, 20);
            this.textBox_mwheel.TabIndex = 9;
            // 
            // label_pipeConnectionStatus
            // 
            this.label_pipeConnectionStatus.AutoSize = true;
            this.label_pipeConnectionStatus.Location = new System.Drawing.Point(74, 367);
            this.label_pipeConnectionStatus.Name = "label_pipeConnectionStatus";
            this.label_pipeConnectionStatus.Size = new System.Drawing.Size(121, 13);
            this.label_pipeConnectionStatus.TabIndex = 11;
            this.label_pipeConnectionStatus.Text = "Pipe Connection Status:";
            // 
            // textBox_pipeConnectionStatus
            // 
            this.textBox_pipeConnectionStatus.Location = new System.Drawing.Point(201, 364);
            this.textBox_pipeConnectionStatus.Name = "textBox_pipeConnectionStatus";
            this.textBox_pipeConnectionStatus.Size = new System.Drawing.Size(389, 20);
            this.textBox_pipeConnectionStatus.TabIndex = 12;
            // 
            // RawMouseDataListener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 394);
            this.Controls.Add(this.textBox_pipeConnectionStatus);
            this.Controls.Add(this.label_pipeConnectionStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_mwheel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_mbutton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_lastY);
            this.Controls.Add(this.textBox_lastX);
            this.Controls.Add(this.button_stopListening);
            this.Controls.Add(this.rtb_console);
            this.Controls.Add(this.button_startListening);
            this.Name = "RawMouseDataListener";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_startListening;
        private System.Windows.Forms.RichTextBox rtb_console;
        private System.Windows.Forms.Button button_stopListening;
        private System.Windows.Forms.TextBox textBox_lastX;
        private System.Windows.Forms.TextBox textBox_lastY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_mbutton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_mwheel;
        private System.Windows.Forms.Label label_pipeConnectionStatus;
        private System.Windows.Forms.TextBox textBox_pipeConnectionStatus;
    }
}

