namespace e94131114_practice_6_1
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            labelTool = new Label();
            labelChangeDay = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(142, 23);
            label1.TabIndex = 0;
            label1.Text = "Selected Tool：";
            // 
            // labelTool
            // 
            labelTool.AutoSize = true;
            labelTool.Location = new Point(149, 21);
            labelTool.Name = "labelTool";
            labelTool.Size = new Size(57, 23);
            labelTool.TabIndex = 1;
            labelTool.Text = "None";
            // 
            // labelChangeDay
            // 
            labelChangeDay.AutoSize = true;
            labelChangeDay.Font = new Font("Microsoft JhengHei UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 136);
            labelChangeDay.Location = new Point(290, 231);
            labelChangeDay.Name = "labelChangeDay";
            labelChangeDay.Size = new Size(237, 91);
            labelChangeDay.TabIndex = 2;
            labelChangeDay.Text = "隔日...";
            // 
            // timer1
            // 
            timer1.Interval = 35;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 580);
            Controls.Add(labelChangeDay);
            Controls.Add(labelTool);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            MouseDown += Form1_MouseDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label labelTool;
        private Label labelChangeDay;
        private System.Windows.Forms.Timer timer1;
    }
}
