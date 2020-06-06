namespace Lab10
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtBCrypt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBPublicKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBSecretKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBDecrypt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBIn = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtBCrypt
            // 
            this.txtBCrypt.Location = new System.Drawing.Point(250, 40);
            this.txtBCrypt.Name = "Crypt";
            this.txtBCrypt.Size = new System.Drawing.Size(177, 20);
            this.txtBCrypt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Crypt";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(63, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "El Gamal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(225, 212);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "RSA";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "PublicKey";
            // 
            // txtBPublicKey
            // 
            this.txtBPublicKey.Location = new System.Drawing.Point(63, 109);
            this.txtBPublicKey.Name = "PublicKey";
            this.txtBPublicKey.Size = new System.Drawing.Size(221, 20);
            this.txtBPublicKey.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "SecretKey";
            // 
            // txtBSecretKey
            // 
            this.txtBSecretKey.Location = new System.Drawing.Point(63, 168);
            this.txtBSecretKey.Name = "SecretKey";
            this.txtBSecretKey.Size = new System.Drawing.Size(221, 20);
            this.txtBSecretKey.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(477, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Decrypt";
            // 
            // txtBDecrypt
            // 
            this.txtBDecrypt.Location = new System.Drawing.Point(480, 40);
            this.txtBDecrypt.Name = "Decrypt";
            this.txtBDecrypt.Size = new System.Drawing.Size(173, 20);
            this.txtBDecrypt.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "In";
            // 
            // txtBIn
            // 
            this.txtBIn.Location = new System.Drawing.Point(63, 40);
            this.txtBIn.Name = "In";
            this.txtBIn.Size = new System.Drawing.Size(153, 20);
            this.txtBIn.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 259);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBIn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBDecrypt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBSecretKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBPublicKey);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBCrypt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBCrypt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBPublicKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBSecretKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBDecrypt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBIn;
    }
}

