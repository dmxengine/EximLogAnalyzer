namespace EximLogAnalyzer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mailsTabPage = new System.Windows.Forms.TabPage();
            this.gridGroupingControl1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.findInLogTabPage = new System.Windows.Forms.TabPage();
            this.findButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.findInCurrentLogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.mailBoxIsFullTabPage = new System.Windows.Forms.TabPage();
            this.mailBoxIsFullRichTextBox = new System.Windows.Forms.RichTextBox();
            this.NotParsedLogLinesTabPage = new System.Windows.Forms.TabPage();
            this.notParsedLogLinesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.bouncedMessagesTabPage = new System.Windows.Forms.TabPage();
            this.bounceMessagesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.dovecotAuthenticatorFailedTabPage = new System.Windows.Forms.TabPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.deliveryDeferredTabPage = new System.Windows.Forms.TabPage();
            this.DeliveryDeferredRichTextBox = new System.Windows.Forms.RichTextBox();
            this.logFilesComboBox = new System.Windows.Forms.ComboBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.logPathLabel = new System.Windows.Forms.Label();
            this.logPathTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.loginLabel = new System.Windows.Forms.Label();
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.hostnameLabel = new System.Windows.Forms.Label();
            this.hostnameTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.mailsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).BeginInit();
            this.findInLogTabPage.SuspendLayout();
            this.mailBoxIsFullTabPage.SuspendLayout();
            this.NotParsedLogLinesTabPage.SuspendLayout();
            this.bouncedMessagesTabPage.SuspendLayout();
            this.dovecotAuthenticatorFailedTabPage.SuspendLayout();
            this.deliveryDeferredTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.mailsTabPage);
            this.tabControl1.Controls.Add(this.findInLogTabPage);
            this.tabControl1.Controls.Add(this.mailBoxIsFullTabPage);
            this.tabControl1.Controls.Add(this.NotParsedLogLinesTabPage);
            this.tabControl1.Controls.Add(this.bouncedMessagesTabPage);
            this.tabControl1.Controls.Add(this.dovecotAuthenticatorFailedTabPage);
            this.tabControl1.Controls.Add(this.deliveryDeferredTabPage);
            this.tabControl1.Location = new System.Drawing.Point(13, 75);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1000, 439);
            this.tabControl1.TabIndex = 7;
            // 
            // mailsTabPage
            // 
            this.mailsTabPage.Controls.Add(this.gridGroupingControl1);
            this.mailsTabPage.Location = new System.Drawing.Point(4, 22);
            this.mailsTabPage.Name = "mailsTabPage";
            this.mailsTabPage.Size = new System.Drawing.Size(992, 413);
            this.mailsTabPage.TabIndex = 3;
            this.mailsTabPage.Text = "Mails";
            this.mailsTabPage.UseVisualStyleBackColor = true;
            // 
            // gridGroupingControl1
            // 
            this.gridGroupingControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridGroupingControl1.BackColor = System.Drawing.SystemColors.Window;
            this.gridGroupingControl1.ColorStyles = Syncfusion.Windows.Forms.ColorStyles.Office2010Silver;
            this.gridGroupingControl1.FreezeCaption = false;
            this.gridGroupingControl1.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Office2007;
            this.gridGroupingControl1.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2010Silver;
            this.gridGroupingControl1.Location = new System.Drawing.Point(5, 5);
            this.gridGroupingControl1.Name = "gridGroupingControl1";
            this.gridGroupingControl1.Office2007ScrollBars = true;
            this.gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.Size = new System.Drawing.Size(984, 405);
            this.gridGroupingControl1.TabIndex = 0;
            this.gridGroupingControl1.Text = "gridGroupingControl1";
            this.gridGroupingControl1.VersionInfo = "13.4460.0.53";
            // 
            // findInLogTabPage
            // 
            this.findInLogTabPage.Controls.Add(this.findButton);
            this.findInLogTabPage.Controls.Add(this.textBox1);
            this.findInLogTabPage.Controls.Add(this.findInCurrentLogRichTextBox);
            this.findInLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.findInLogTabPage.Name = "findInLogTabPage";
            this.findInLogTabPage.Size = new System.Drawing.Size(992, 413);
            this.findInLogTabPage.TabIndex = 2;
            this.findInLogTabPage.Text = "Find In Log";
            this.findInLogTabPage.UseVisualStyleBackColor = true;
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(290, 3);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(75, 23);
            this.findButton.TabIndex = 5;
            this.findButton.Text = "Find";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(5, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(278, 20);
            this.textBox1.TabIndex = 4;
            // 
            // findInCurrentLogRichTextBox
            // 
            this.findInCurrentLogRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findInCurrentLogRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.findInCurrentLogRichTextBox.Location = new System.Drawing.Point(5, 31);
            this.findInCurrentLogRichTextBox.Name = "findInCurrentLogRichTextBox";
            this.findInCurrentLogRichTextBox.Size = new System.Drawing.Size(984, 379);
            this.findInCurrentLogRichTextBox.TabIndex = 3;
            this.findInCurrentLogRichTextBox.Text = "";
            // 
            // mailBoxIsFullTabPage
            // 
            this.mailBoxIsFullTabPage.Controls.Add(this.mailBoxIsFullRichTextBox);
            this.mailBoxIsFullTabPage.Location = new System.Drawing.Point(4, 22);
            this.mailBoxIsFullTabPage.Name = "mailBoxIsFullTabPage";
            this.mailBoxIsFullTabPage.Size = new System.Drawing.Size(992, 413);
            this.mailBoxIsFullTabPage.TabIndex = 4;
            this.mailBoxIsFullTabPage.Text = "Mailbox Is Full";
            this.mailBoxIsFullTabPage.UseVisualStyleBackColor = true;
            // 
            // mailBoxIsFullRichTextBox
            // 
            this.mailBoxIsFullRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mailBoxIsFullRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.mailBoxIsFullRichTextBox.Location = new System.Drawing.Point(5, 5);
            this.mailBoxIsFullRichTextBox.Name = "mailBoxIsFullRichTextBox";
            this.mailBoxIsFullRichTextBox.Size = new System.Drawing.Size(984, 405);
            this.mailBoxIsFullRichTextBox.TabIndex = 1;
            this.mailBoxIsFullRichTextBox.Text = "";
            // 
            // NotParsedLogLinesTabPage
            // 
            this.NotParsedLogLinesTabPage.Controls.Add(this.notParsedLogLinesRichTextBox);
            this.NotParsedLogLinesTabPage.Location = new System.Drawing.Point(4, 22);
            this.NotParsedLogLinesTabPage.Name = "NotParsedLogLinesTabPage";
            this.NotParsedLogLinesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.NotParsedLogLinesTabPage.Size = new System.Drawing.Size(992, 413);
            this.NotParsedLogLinesTabPage.TabIndex = 1;
            this.NotParsedLogLinesTabPage.Text = "Not Parsed Log Lines";
            this.NotParsedLogLinesTabPage.UseVisualStyleBackColor = true;
            // 
            // notParsedLogLinesRichTextBox
            // 
            this.notParsedLogLinesRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notParsedLogLinesRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.notParsedLogLinesRichTextBox.Location = new System.Drawing.Point(5, 5);
            this.notParsedLogLinesRichTextBox.Name = "notParsedLogLinesRichTextBox";
            this.notParsedLogLinesRichTextBox.Size = new System.Drawing.Size(984, 405);
            this.notParsedLogLinesRichTextBox.TabIndex = 1;
            this.notParsedLogLinesRichTextBox.Text = "";
            // 
            // bouncedMessagesTabPage
            // 
            this.bouncedMessagesTabPage.Controls.Add(this.bounceMessagesRichTextBox);
            this.bouncedMessagesTabPage.Location = new System.Drawing.Point(4, 22);
            this.bouncedMessagesTabPage.Name = "bouncedMessagesTabPage";
            this.bouncedMessagesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.bouncedMessagesTabPage.Size = new System.Drawing.Size(992, 413);
            this.bouncedMessagesTabPage.TabIndex = 0;
            this.bouncedMessagesTabPage.Text = "Bounced Messages";
            this.bouncedMessagesTabPage.UseVisualStyleBackColor = true;
            // 
            // bounceMessagesRichTextBox
            // 
            this.bounceMessagesRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bounceMessagesRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.bounceMessagesRichTextBox.Location = new System.Drawing.Point(5, 5);
            this.bounceMessagesRichTextBox.Name = "bounceMessagesRichTextBox";
            this.bounceMessagesRichTextBox.Size = new System.Drawing.Size(984, 405);
            this.bounceMessagesRichTextBox.TabIndex = 0;
            this.bounceMessagesRichTextBox.Text = "";
            // 
            // dovecotAuthenticatorFailedTabPage
            // 
            this.dovecotAuthenticatorFailedTabPage.Controls.Add(this.richTextBox2);
            this.dovecotAuthenticatorFailedTabPage.Location = new System.Drawing.Point(4, 22);
            this.dovecotAuthenticatorFailedTabPage.Name = "dovecotAuthenticatorFailedTabPage";
            this.dovecotAuthenticatorFailedTabPage.Size = new System.Drawing.Size(992, 413);
            this.dovecotAuthenticatorFailedTabPage.TabIndex = 5;
            this.dovecotAuthenticatorFailedTabPage.Text = "Dovecot Authenticator Failed";
            this.dovecotAuthenticatorFailedTabPage.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.richTextBox2.Location = new System.Drawing.Point(5, 5);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(984, 405);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // deliveryDeferredTabPage
            // 
            this.deliveryDeferredTabPage.Controls.Add(this.DeliveryDeferredRichTextBox);
            this.deliveryDeferredTabPage.Location = new System.Drawing.Point(4, 22);
            this.deliveryDeferredTabPage.Name = "deliveryDeferredTabPage";
            this.deliveryDeferredTabPage.Size = new System.Drawing.Size(992, 413);
            this.deliveryDeferredTabPage.TabIndex = 6;
            this.deliveryDeferredTabPage.Text = "Delivery Deferred";
            this.deliveryDeferredTabPage.UseVisualStyleBackColor = true;
            // 
            // DeliveryDeferredRichTextBox
            // 
            this.DeliveryDeferredRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeliveryDeferredRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.DeliveryDeferredRichTextBox.Location = new System.Drawing.Point(5, 5);
            this.DeliveryDeferredRichTextBox.Name = "DeliveryDeferredRichTextBox";
            this.DeliveryDeferredRichTextBox.Size = new System.Drawing.Size(984, 405);
            this.DeliveryDeferredRichTextBox.TabIndex = 2;
            this.DeliveryDeferredRichTextBox.Text = "";
            // 
            // logFilesComboBox
            // 
            this.logFilesComboBox.FormattingEnabled = true;
            this.logFilesComboBox.Location = new System.Drawing.Point(703, 24);
            this.logFilesComboBox.Name = "logFilesComboBox";
            this.logFilesComboBox.Size = new System.Drawing.Size(206, 21);
            this.logFilesComboBox.TabIndex = 5;
            this.logFilesComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(327, 25);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(100, 21);
            this.connectButton.TabIndex = 4;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Enabled = false;
            this.refreshButton.Location = new System.Drawing.Point(915, 24);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 21);
            this.refreshButton.TabIndex = 6;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.logPathLabel);
            this.groupBox1.Controls.Add(this.logPathTextBox);
            this.groupBox1.Controls.Add(this.passwordLabel);
            this.groupBox1.Controls.Add(this.refreshButton);
            this.groupBox1.Controls.Add(this.passwordTextBox);
            this.groupBox1.Controls.Add(this.logFilesComboBox);
            this.groupBox1.Controls.Add(this.connectButton);
            this.groupBox1.Controls.Add(this.loginLabel);
            this.groupBox1.Controls.Add(this.loginTextBox);
            this.groupBox1.Controls.Add(this.hostnameLabel);
            this.groupBox1.Controls.Add(this.hostnameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(997, 58);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(700, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Log Files:";
            // 
            // logPathLabel
            // 
            this.logPathLabel.AutoSize = true;
            this.logPathLabel.Location = new System.Drawing.Point(430, 9);
            this.logPathLabel.Name = "logPathLabel";
            this.logPathLabel.Size = new System.Drawing.Size(53, 13);
            this.logPathLabel.TabIndex = 8;
            this.logPathLabel.Text = "Log Path:";
            // 
            // logPathTextBox
            // 
            this.logPathTextBox.Location = new System.Drawing.Point(433, 25);
            this.logPathTextBox.Name = "logPathTextBox";
            this.logPathTextBox.Size = new System.Drawing.Size(264, 20);
            this.logPathTextBox.TabIndex = 7;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(218, 9);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Password:";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(221, 25);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(100, 20);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(112, 9);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(36, 13);
            this.loginLabel.TabIndex = 3;
            this.loginLabel.Text = "Login:";
            // 
            // loginTextBox
            // 
            this.loginTextBox.Location = new System.Drawing.Point(115, 25);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(100, 20);
            this.loginTextBox.TabIndex = 2;
            // 
            // hostnameLabel
            // 
            this.hostnameLabel.AutoSize = true;
            this.hostnameLabel.Location = new System.Drawing.Point(6, 9);
            this.hostnameLabel.Name = "hostnameLabel";
            this.hostnameLabel.Size = new System.Drawing.Size(58, 13);
            this.hostnameLabel.TabIndex = 1;
            this.hostnameLabel.Text = "Hostname:";
            // 
            // hostnameTextBox
            // 
            this.hostnameTextBox.Location = new System.Drawing.Point(9, 25);
            this.hostnameTextBox.Name = "hostnameTextBox";
            this.hostnameTextBox.Size = new System.Drawing.Size(100, 20);
            this.hostnameTextBox.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 526);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "DmxEngine EximLogAnalyzer v1.0.20160930";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.mailsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).EndInit();
            this.findInLogTabPage.ResumeLayout(false);
            this.findInLogTabPage.PerformLayout();
            this.mailBoxIsFullTabPage.ResumeLayout(false);
            this.NotParsedLogLinesTabPage.ResumeLayout(false);
            this.bouncedMessagesTabPage.ResumeLayout(false);
            this.dovecotAuthenticatorFailedTabPage.ResumeLayout(false);
            this.deliveryDeferredTabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage bouncedMessagesTabPage;
        private System.Windows.Forms.RichTextBox bounceMessagesRichTextBox;
        private System.Windows.Forms.TabPage NotParsedLogLinesTabPage;
        private System.Windows.Forms.RichTextBox notParsedLogLinesRichTextBox;
        private System.Windows.Forms.TabPage findInLogTabPage;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox findInCurrentLogRichTextBox;
        private System.Windows.Forms.TabPage mailsTabPage;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl1;
        private System.Windows.Forms.ComboBox logFilesComboBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TabPage mailBoxIsFullTabPage;
        private System.Windows.Forms.RichTextBox mailBoxIsFullRichTextBox;
        private System.Windows.Forms.TabPage dovecotAuthenticatorFailedTabPage;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TabPage deliveryDeferredTabPage;
        private System.Windows.Forms.RichTextBox DeliveryDeferredRichTextBox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.TextBox loginTextBox;
        private System.Windows.Forms.Label hostnameLabel;
        private System.Windows.Forms.TextBox hostnameTextBox;
        private System.Windows.Forms.TextBox logPathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label logPathLabel;
    }
}

