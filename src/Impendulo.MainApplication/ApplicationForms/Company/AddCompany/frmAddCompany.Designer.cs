﻿namespace Impendulo.Deployment.Company
{
    partial class frmAddCompany
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddCompany = new System.Windows.Forms.Button();
            this.txtFaxNumber = new System.Windows.Forms.MaskedTextBox();
            this.txtOfficeNumber = new System.Windows.Forms.MaskedTextBox();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSARSLevyRegistration = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSicCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCompanySETANumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtComapnyName = new System.Windows.Forms.TextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnAddCompany);
            this.groupBox1.Controls.Add(this.txtFaxNumber);
            this.groupBox1.Controls.Add(this.txtOfficeNumber);
            this.groupBox1.Controls.Add(this.txtEmailAddress);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSARSLevyRegistration);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSicCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCompanySETANumber);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtComapnyName);
            this.groupBox1.Location = new System.Drawing.Point(23, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(547, 254);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Company Details";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(274, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 23);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddCompany
            // 
            this.btnAddCompany.Location = new System.Drawing.Point(400, 216);
            this.btnAddCompany.Name = "btnAddCompany";
            this.btnAddCompany.Size = new System.Drawing.Size(138, 23);
            this.btnAddCompany.TabIndex = 27;
            this.btnAddCompany.Text = "Add Company";
            this.btnAddCompany.UseVisualStyleBackColor = true;
            this.btnAddCompany.Click += new System.EventHandler(this.btnAddCompany_Click);
            // 
            // txtFaxNumber
            // 
            this.txtFaxNumber.Location = new System.Drawing.Point(133, 155);
            this.txtFaxNumber.Mask = "(999) 000-0000";
            this.txtFaxNumber.Name = "txtFaxNumber";
            this.txtFaxNumber.Size = new System.Drawing.Size(86, 20);
            this.txtFaxNumber.TabIndex = 26;
            // 
            // txtOfficeNumber
            // 
            this.txtOfficeNumber.Location = new System.Drawing.Point(133, 132);
            this.txtOfficeNumber.Mask = "(999) 000-0000";
            this.txtOfficeNumber.Name = "txtOfficeNumber";
            this.txtOfficeNumber.Size = new System.Drawing.Size(86, 20);
            this.txtOfficeNumber.TabIndex = 25;
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Location = new System.Drawing.Point(133, 181);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(405, 20);
            this.txtEmailAddress.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Email Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Fax Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Office Number";
            // 
            // txtSARSLevyRegistration
            // 
            this.txtSARSLevyRegistration.Location = new System.Drawing.Point(133, 103);
            this.txtSARSLevyRegistration.Name = "txtSARSLevyRegistration";
            this.txtSARSLevyRegistration.Size = new System.Drawing.Size(216, 20);
            this.txtSARSLevyRegistration.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "SARS Levy Registration";
            // 
            // txtSicCode
            // 
            this.txtSicCode.Location = new System.Drawing.Point(133, 77);
            this.txtSicCode.Name = "txtSicCode";
            this.txtSicCode.Size = new System.Drawing.Size(216, 20);
            this.txtSicCode.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Sic Code";
            // 
            // txtCompanySETANumber
            // 
            this.txtCompanySETANumber.Location = new System.Drawing.Point(133, 51);
            this.txtCompanySETANumber.Name = "txtCompanySETANumber";
            this.txtCompanySETANumber.Size = new System.Drawing.Size(216, 20);
            this.txtCompanySETANumber.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "SETA Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Company";
            // 
            // txtComapnyName
            // 
            this.txtComapnyName.Location = new System.Drawing.Point(133, 25);
            this.txtComapnyName.Name = "txtComapnyName";
            this.txtComapnyName.Size = new System.Drawing.Size(405, 20);
            this.txtComapnyName.TabIndex = 0;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(355, 51);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(68, 19);
            this.metroLabel1.TabIndex = 29;
            this.metroLabel1.Text = "(Optional)";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(355, 77);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(68, 19);
            this.metroLabel2.TabIndex = 30;
            this.metroLabel2.Text = "(Optional)";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(355, 104);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(68, 19);
            this.metroLabel3.TabIndex = 31;
            this.metroLabel3.Text = "(Optional)";
            // 
            // frmAddCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 332);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmAddCompany";
            this.Text = "Add Company";
            this.Load += new System.EventHandler(this.frmCompanySearchV2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddCompany;
        private System.Windows.Forms.MaskedTextBox txtFaxNumber;
        private System.Windows.Forms.MaskedTextBox txtOfficeNumber;
        private System.Windows.Forms.TextBox txtEmailAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSARSLevyRegistration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSicCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCompanySETANumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtComapnyName;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
    }
}