namespace AMaruniak.FlexSMS {
    partial class frmMain {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txbDescription = new System.Windows.Forms.TextBox();
            this.txbHost = new System.Windows.Forms.TextBox();
            this.txbPort = new System.Windows.Forms.TextBox();
            this.txbSystemId = new System.Windows.Forms.TextBox();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblSystemId = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblHost = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblLogo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txbDescription
            // 
            this.txbDescription.Location = new System.Drawing.Point(125, 27);
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(152, 20);
            this.txbDescription.TabIndex = 0;
            // 
            // txbHost
            // 
            this.txbHost.Location = new System.Drawing.Point(125, 62);
            this.txbHost.Name = "txbHost";
            this.txbHost.Size = new System.Drawing.Size(152, 20);
            this.txbHost.TabIndex = 1;
            // 
            // txbPort
            // 
            this.txbPort.Location = new System.Drawing.Point(125, 102);
            this.txbPort.Name = "txbPort";
            this.txbPort.Size = new System.Drawing.Size(65, 20);
            this.txbPort.TabIndex = 2;
            this.txbPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbPort_KeyPress);
            // 
            // txbSystemId
            // 
            this.txbSystemId.Location = new System.Drawing.Point(125, 141);
            this.txbSystemId.Name = "txbSystemId";
            this.txbSystemId.Size = new System.Drawing.Size(152, 20);
            this.txbSystemId.TabIndex = 3;
            // 
            // txbPassword
            // 
            this.txbPassword.Location = new System.Drawing.Point(125, 176);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.Size = new System.Drawing.Size(152, 20);
            this.txbPassword.TabIndex = 4;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(37, 30);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(57, 13);
            this.lblDescription.TabIndex = 7;
            this.lblDescription.Text = "Описание";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(37, 105);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 8;
            this.lblPort.Text = "Port";
            // 
            // lblSystemId
            // 
            this.lblSystemId.AutoSize = true;
            this.lblSystemId.Location = new System.Drawing.Point(37, 144);
            this.lblSystemId.Name = "lblSystemId";
            this.lblSystemId.Size = new System.Drawing.Size(50, 13);
            this.lblSystemId.TabIndex = 9;
            this.lblSystemId.Text = "SystemId";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(37, 179);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Password";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(37, 65);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(29, 13);
            this.lblHost.TabIndex = 16;
            this.lblHost.Text = "Host";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(40, 230);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Сохранить в файл";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(185, 230);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(129, 23);
            this.btnLoad.TabIndex = 18;
            this.btnLoad.Text = "Загрузить из файла";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLogo.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblLogo.Location = new System.Drawing.Point(268, 272);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(92, 13);
            this.lblLogo.TabIndex = 19;
            this.lblLogo.Text = " ©AMaruniak.Flex";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 285);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblHost);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblSystemId);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txbPassword);
            this.Controls.Add(this.txbSystemId);
            this.Controls.Add(this.txbPort);
            this.Controls.Add(this.txbHost);
            this.Controls.Add(this.txbDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Конфигуратор файла настроек";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbDescription;
        private System.Windows.Forms.TextBox txbHost;
        private System.Windows.Forms.TextBox txbPort;
        private System.Windows.Forms.TextBox txbSystemId;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblSystemId;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblLogo;
    }
}

