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
            this.testingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addExistingRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.login.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testingToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(645, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // testingToolStripMenuItem
            // 
            this.testingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTestToolStripMenuItem,
            this.addExistingRecordToolStripMenuItem});
            this.testingToolStripMenuItem.Name = "testingToolStripMenuItem";
            this.testingToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.testingToolStripMenuItem.Text = "Testing";
            // 
            // newTestToolStripMenuItem
            // 
            this.newTestToolStripMenuItem.Name = "newTestToolStripMenuItem";
            this.newTestToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newTestToolStripMenuItem.Text = "New Test";
            this.newTestToolStripMenuItem.Click += new System.EventHandler(this.newTestToolStripMenuItem_Click);
            // 
            // addExistingRecordToolStripMenuItem
            // 
            this.addExistingRecordToolStripMenuItem.Name = "addExistingRecordToolStripMenuItem";
            this.addExistingRecordToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.addExistingRecordToolStripMenuItem.Text = "Add Existing Record";
            this.addExistingRecordToolStripMenuItem.Click += new System.EventHandler(this.addExistingRecordToolStripMenuItem_Click);
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
            this.mRNToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.mRNToolStripMenuItem.Text = "MRN";
            this.mRNToolStripMenuItem.Click += new System.EventHandler(this.mRNToolStripMenuItem_Click);
            // 
            // patientNameToolStripMenuItem
            // 
            this.patientNameToolStripMenuItem.Name = "patientNameToolStripMenuItem";
            this.patientNameToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.patientNameToolStripMenuItem.Text = "Patient Name";
            // 
            // doctorNameToolStripMenuItem
            // 
            this.doctorNameToolStripMenuItem.Name = "doctorNameToolStripMenuItem";
            this.doctorNameToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.doctorNameToolStripMenuItem.Text = "Tester Name";
            // 
            // rangeOfDateToolStripMenuItem
            // 
            this.rangeOfDateToolStripMenuItem.Name = "rangeOfDateToolStripMenuItem";
            this.rangeOfDateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.rangeOfDateToolStripMenuItem.Text = "Range of Date";
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
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
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
            this.login.Controls.Add(this.button1);
            this.login.Controls.Add(this.textBox2);
            this.login.Controls.Add(this.label2);
            this.login.Controls.Add(this.textBox1);
            this.login.Controls.Add(this.label1);
            this.login.Location = new System.Drawing.Point(200, 224);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(241, 137);
            this.login.TabIndex = 2;
            this.login.TabStop = false;
            this.login.Text = "Login";
            this.login.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(79, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(102, 74);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(102, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
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
    }
}

