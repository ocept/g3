namespace g3
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.aliveTimer = new System.Windows.Forms.Label();
            this.reloadBar = new System.Windows.Forms.ProgressBar();
            this.healthBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(886, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "Player";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(886, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 22);
            this.button2.TabIndex = 1;
            this.button2.Text = "Zombie";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(886, 58);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(55, 22);
            this.button3.TabIndex = 2;
            this.button3.Text = "Spider";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(887, 114);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(54, 22);
            this.button4.TabIndex = 3;
            this.button4.Text = "Update";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(886, 86);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(55, 22);
            this.button5.TabIndex = 4;
            this.button5.Text = "Human";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // aliveTimer
            // 
            this.aliveTimer.AutoSize = true;
            this.aliveTimer.BackColor = System.Drawing.Color.White;
            this.aliveTimer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aliveTimer.Font = new System.Drawing.Font("Bauhaus 93", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aliveTimer.Location = new System.Drawing.Point(431, 520);
            this.aliveTimer.Name = "aliveTimer";
            this.aliveTimer.Size = new System.Drawing.Size(85, 38);
            this.aliveTimer.TabIndex = 5;
            this.aliveTimer.Text = "Time";
            this.aliveTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // reloadBar
            // 
            this.reloadBar.Location = new System.Drawing.Point(457, 314);
            this.reloadBar.MarqueeAnimationSpeed = 0;
            this.reloadBar.Name = "reloadBar";
            this.reloadBar.Size = new System.Drawing.Size(60, 10);
            this.reloadBar.TabIndex = 6;
            this.reloadBar.Visible = false;
            // 
            // healthBar
            // 
            this.healthBar.Location = new System.Drawing.Point(617, 520);
            this.healthBar.Name = "healthBar";
            this.healthBar.Size = new System.Drawing.Size(295, 38);
            this.healthBar.TabIndex = 7;
            this.healthBar.Value = 100;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Bauhaus 93", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(613, 496);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "Health";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 565);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.healthBar);
            this.Controls.Add(this.reloadBar);
            this.Controls.Add(this.aliveTimer);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximumSize = new System.Drawing.Size(960, 600);
            this.MinimumSize = new System.Drawing.Size(960, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label aliveTimer;
        private System.Windows.Forms.ProgressBar reloadBar;
        private System.Windows.Forms.ProgressBar healthBar;
        private System.Windows.Forms.Label label1;
    
        public string aliveText
        {
            get { return aliveTimer.Text; }
            set { aliveTimer.Text = value; }
        }
    }
}
