namespace Markup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.userManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testerLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addExistingRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mRNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doctorNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rangeOfDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testingSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cNCWordTestingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.azBioSentenceTestingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.login = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.createChangeUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.login.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userManagementToolStripMenuItem,
            this.testingToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(645, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // userManagementToolStripMenuItem
            // 
            this.userManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminLevelToolStripMenuItem,
            this.testerLevelToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.userManagementToolStripMenuItem.Name = "userManagementToolStripMenuItem";
            this.userManagementToolStripMenuItem.Size = new System.Drawing.Size(116, 20);
            this.userManagementToolStripMenuItem.Text = "User Management";
            // 
            // adminLevelToolStripMenuItem
            // 
            this.adminLevelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createChangeUserToolStripMenuItem,
            this.createNewGroupToolStripMenuItem});
            this.adminLevelToolStripMenuItem.Name = "adminLevelToolStripMenuItem";
            this.adminLevelToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.adminLevelToolStripMenuItem.Text = "Admin Level";
            this.adminLevelToolStripMenuItem.Click += new System.EventHandler(this.adminLevelToolStripMenuItem_Click);
            // 
            // testerLevelToolStripMenuItem
            // 
            this.testerLevelToolStripMenuItem.Name = "testerLevelToolStripMenuItem";
            this.testerLevelToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.testerLevelToolStripMenuItem.Text = "Change Password";
            this.testerLevelToolStripMenuItem.Click += new System.EventHandler(this.testerLevelToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.logOutToolStripMenuItem.Text = "Log out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // testingToolStripMenuItem
            // 
            this.testingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTestToolStripMenuItem,
            this.addToolStripMenuItem,
            this.addExistingRecordToolStripMenuItem,
            this.reportToolStripMenuItem});
            this.testingToolStripMenuItem.Name = "testingToolStripMenuItem";
            this.testingToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.testingToolStripMenuItem.Text = "Testing";
            // 
            // newTestToolStripMenuItem
            // 
            this.newTestToolStripMenuItem.Name = "newTestToolStripMenuItem";
            this.newTestToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.newTestToolStripMenuItem.Text = "Create New Patient";
            this.newTestToolStripMenuItem.Click += new System.EventHandler(this.newTestToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.addToolStripMenuItem.Text = "Select Existing Patient";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // addExistingRecordToolStripMenuItem
            // 
            this.addExistingRecordToolStripMenuItem.Name = "addExistingRecordToolStripMenuItem";
            this.addExistingRecordToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.addExistingRecordToolStripMenuItem.Text = "Add Existing Record";
            this.addExistingRecordToolStripMenuItem.Click += new System.EventHandler(this.addExistingRecordToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.reportToolStripMenuItem.Text = "Export Results";
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mRNToolStripMenuItem,
            this.patientNameToolStripMenuItem,
            this.doctorNameToolStripMenuItem,
            this.rangeOfDateToolStripMenuItem});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.searchToolStripMenuItem.Text = "Search";
            // 
            // mRNToolStripMenuItem
            // 
            this.mRNToolStripMenuItem.Name = "mRNToolStripMenuItem";
            this.mRNToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mRNToolStripMenuItem.Text = "MRN";
            this.mRNToolStripMenuItem.Click += new System.EventHandler(this.mRNToolStripMenuItem_Click);
            // 
            // patientNameToolStripMenuItem
            // 
            this.patientNameToolStripMenuItem.Name = "patientNameToolStripMenuItem";
            this.patientNameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.patientNameToolStripMenuItem.Text = "Patient Name";
            this.patientNameToolStripMenuItem.Click += new System.EventHandler(this.patientNameToolStripMenuItem_Click);
            // 
            // doctorNameToolStripMenuItem
            // 
            this.doctorNameToolStripMenuItem.Name = "doctorNameToolStripMenuItem";
            this.doctorNameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.doctorNameToolStripMenuItem.Text = "Group";
            this.doctorNameToolStripMenuItem.Click += new System.EventHandler(this.doctorNameToolStripMenuItem_Click);
            // 
            // rangeOfDateToolStripMenuItem
            // 
            this.rangeOfDateToolStripMenuItem.Name = "rangeOfDateToolStripMenuItem";
            this.rangeOfDateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rangeOfDateToolStripMenuItem.Text = "Range of Date";
            this.rangeOfDateToolStripMenuItem.Click += new System.EventHandler(this.rangeOfDateToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testingSetupToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.backupToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // testingSetupToolStripMenuItem
            // 
            this.testingSetupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cNCWordTestingToolStripMenuItem,
            this.azBioSentenceTestingToolStripMenuItem});
            this.testingSetupToolStripMenuItem.Name = "testingSetupToolStripMenuItem";
            this.testingSetupToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.testingSetupToolStripMenuItem.Text = "Testing Setup";
            // 
            // cNCWordTestingToolStripMenuItem
            // 
            this.cNCWordTestingToolStripMenuItem.Name = "cNCWordTestingToolStripMenuItem";
            this.cNCWordTestingToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.cNCWordTestingToolStripMenuItem.Text = "CNC Word Testing";
            // 
            // azBioSentenceTestingToolStripMenuItem
            // 
            this.azBioSentenceTestingToolStripMenuItem.Name = "azBioSentenceTestingToolStripMenuItem";
            this.azBioSentenceTestingToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.azBioSentenceTestingToolStripMenuItem.Text = "AzBio Sentence Testing";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.restoreToolStripMenuItem});
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(643, 162);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // login
            // 
            this.login.Controls.Add(this.comboBox1);
            this.login.Controls.Add(this.label3);
            this.login.Controls.Add(this.button1);
            this.login.Controls.Add(this.textBox2);
            this.login.Controls.Add(this.label2);
            this.login.Controls.Add(this.textBox1);
            this.login.Controls.Add(this.label1);
            this.login.Location = new System.Drawing.Point(192, 200);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(249, 161);
            this.login.TabIndex = 2;
            this.login.TabStop = false;
            this.login.Text = "Login";
            this.login.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Admin",
            "Tester"});
            this.comboBox1.Location = new System.Drawing.Point(116, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Priority Level";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(116, 98);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(116, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // createChangeUserToolStripMenuItem
            // 
            this.createChangeUserToolStripMenuItem.Name = "createChangeUserToolStripMenuItem";
            this.createChangeUserToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.createChangeUserToolStripMenuItem.Text = "Create/Change User";
            this.createChangeUserToolStripMenuItem.Click += new System.EventHandler(this.createChangeUserToolStripMenuItem_Click);
            // 
            // createNewGroupToolStripMenuItem
            // 
            this.createNewGroupToolStripMenuItem.Name = "createNewGroupToolStripMenuItem";
            this.createNewGroupToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.createNewGroupToolStripMenuItem.Text = "Create New Group";
            this.createNewGroupToolStripMenuItem.Click += new System.EventHandler(this.createNewGroupToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.saveToolStripMenuItem.Text = "Save to Server";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.restoreToolStripMenuItem.Text = "Restore from Server";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 370);
            this.Controls.Add(this.login);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.login.ResumeLayout(false);
            this.login.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addExistingRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mRNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doctorNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rangeOfDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testingSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cNCWordTestingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem azBioSentenceTestingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem userManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testerLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createChangeUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
    }
}

