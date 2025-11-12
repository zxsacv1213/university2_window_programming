namespace e94131114_practice_6_1
{
    partial class FormPalse
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
            label1 = new Label();
            labelCount = new Label();
            buttonRead = new Button();
            buttonSafe = new Button();
            buttonNothing = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(247, 83);
            label1.Name = "label1";
            label1.Size = new Size(179, 57);
            label1.TabIndex = 0;
            label1.Text = "PALSED";
            label1.Click += label1_Click;
            // 
            // labelCount
            // 
            labelCount.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 136);
            labelCount.Location = new Point(165, 170);
            labelCount.Name = "labelCount";
            labelCount.Size = new Size(288, 50);
            labelCount.TabIndex = 4;
            labelCount.Text = "PALSED";
            // 
            // buttonRead
            // 
            buttonRead.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            buttonRead.Location = new Point(220, 256);
            buttonRead.Name = "buttonRead";
            buttonRead.Size = new Size(206, 74);
            buttonRead.TabIndex = 5;
            buttonRead.Text = "載入存檔";
            buttonRead.UseVisualStyleBackColor = true;
            buttonRead.Click += buttonRead_Click;
            // 
            // buttonSafe
            // 
            buttonSafe.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            buttonSafe.Location = new Point(220, 357);
            buttonSafe.Name = "buttonSafe";
            buttonSafe.Size = new Size(206, 74);
            buttonSafe.TabIndex = 6;
            buttonSafe.Text = "儲存並重置";
            buttonSafe.UseVisualStyleBackColor = true;
            buttonSafe.Click += buttonSafe_Click;
            // 
            // buttonNothing
            // 
            buttonNothing.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            buttonNothing.Location = new Point(220, 457);
            buttonNothing.Name = "buttonNothing";
            buttonNothing.Size = new Size(206, 74);
            buttonNothing.TabIndex = 7;
            buttonNothing.Text = "返回遊戲";
            buttonNothing.UseVisualStyleBackColor = true;
            buttonNothing.Click += buttonNothing_Click;
            // 
            // FormPalse
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(648, 594);
            Controls.Add(buttonNothing);
            Controls.Add(buttonSafe);
            Controls.Add(buttonRead);
            Controls.Add(labelCount);
            Controls.Add(label1);
            Name = "FormPalse";
            Text = "FormPalse";
            Load += FormPalse_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label labelCount;
        private Button buttonRead;
        private Button buttonSafe;
        private Button buttonNothing;
    }
}