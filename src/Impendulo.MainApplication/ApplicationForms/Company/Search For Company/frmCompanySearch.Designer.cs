﻿namespace Impendulo.Deployment.Company
{
    partial class frmCompanySearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompanySearch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbSearchForStudent = new System.Windows.Forms.GroupBox();
            this.btnAddCompany = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSARSRegNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSicCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSETANumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dgvCompanySearchResults = new System.Windows.Forms.DataGridView();
            this.colSelectStudent = new System.Windows.Forms.DataGridViewLinkColumn();
            this.companyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companySETANumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companySicCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companySARSLevyRegistrationNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbSearchForStudent.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompanySearchResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(20, 30);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbSearchForStudent);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer1);
            this.splitContainer1.Size = new System.Drawing.Size(616, 426);
            this.splitContainer1.SplitterDistance = 143;
            this.splitContainer1.TabIndex = 14;
            // 
            // gbSearchForStudent
            // 
            this.gbSearchForStudent.Controls.Add(this.btnAddCompany);
            this.gbSearchForStudent.Controls.Add(this.btnRefresh);
            this.gbSearchForStudent.Controls.Add(this.btnSearch);
            this.gbSearchForStudent.Controls.Add(this.label5);
            this.gbSearchForStudent.Controls.Add(this.txtSARSRegNumber);
            this.gbSearchForStudent.Controls.Add(this.label3);
            this.gbSearchForStudent.Controls.Add(this.txtSicCode);
            this.gbSearchForStudent.Controls.Add(this.label1);
            this.gbSearchForStudent.Controls.Add(this.txtSETANumber);
            this.gbSearchForStudent.Controls.Add(this.label4);
            this.gbSearchForStudent.Controls.Add(this.label2);
            this.gbSearchForStudent.Controls.Add(this.txtCompanyName);
            this.gbSearchForStudent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSearchForStudent.Location = new System.Drawing.Point(0, 0);
            this.gbSearchForStudent.Name = "gbSearchForStudent";
            this.gbSearchForStudent.Size = new System.Drawing.Size(616, 143);
            this.gbSearchForStudent.TabIndex = 2;
            this.gbSearchForStudent.TabStop = false;
            this.gbSearchForStudent.Text = "Search Fields";
            // 
            // btnAddCompany
            // 
            this.btnAddCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCompany.Image")));
            this.btnAddCompany.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAddCompany.Location = new System.Drawing.Point(531, 27);
            this.btnAddCompany.Name = "btnAddCompany";
            this.btnAddCompany.Size = new System.Drawing.Size(75, 73);
            this.btnAddCompany.TabIndex = 19;
            this.btnAddCompany.Text = "Add New";
            this.btnAddCompany.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddCompany.UseVisualStyleBackColor = true;
            this.btnAddCompany.Visible = false;
            this.btnAddCompany.Click += new System.EventHandler(this.btnAddCompany_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(450, 27);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 73);
            this.btnRefresh.TabIndex = 18;
            this.btnRefresh.Text = "Reset";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSearch.Location = new System.Drawing.Point(369, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 73);
            this.btnSearch.TabIndex = 17;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "SARS Reg. Number";
            // 
            // txtSARSRegNumber
            // 
            this.txtSARSRegNumber.Location = new System.Drawing.Point(122, 106);
            this.txtSARSRegNumber.Name = "txtSARSRegNumber";
            this.txtSARSRegNumber.Size = new System.Drawing.Size(239, 20);
            this.txtSARSRegNumber.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Sic Code";
            // 
            // txtSicCode
            // 
            this.txtSicCode.Location = new System.Drawing.Point(122, 80);
            this.txtSicCode.Name = "txtSicCode";
            this.txtSicCode.Size = new System.Drawing.Size(239, 20);
            this.txtSicCode.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "SETA Number";
            // 
            // txtSETANumber
            // 
            this.txtSETANumber.Location = new System.Drawing.Point(122, 54);
            this.txtSETANumber.Name = "txtSETANumber";
            this.txtSETANumber.Size = new System.Drawing.Size(239, 20);
            this.txtSETANumber.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Company Name";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(122, 28);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(239, 20);
            this.txtCompanyName.TabIndex = 3;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dgvCompanySearchResults);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(616, 252);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(616, 279);
            this.toolStripContainer1.TabIndex = 12;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.bindingNavigator1);
            // 
            // dgvCompanySearchResults
            // 
            this.dgvCompanySearchResults.AllowUserToAddRows = false;
            this.dgvCompanySearchResults.AllowUserToDeleteRows = false;
            this.dgvCompanySearchResults.AutoGenerateColumns = false;
            this.dgvCompanySearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompanySearchResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelectStudent,
            this.companyNameDataGridViewTextBoxColumn,
            this.companySETANumberDataGridViewTextBoxColumn,
            this.companySicCodeDataGridViewTextBoxColumn,
            this.companySARSLevyRegistrationNumberDataGridViewTextBoxColumn});
            this.dgvCompanySearchResults.DataSource = this.companyBindingSource;
            this.dgvCompanySearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompanySearchResults.Location = new System.Drawing.Point(0, 0);
            this.dgvCompanySearchResults.Name = "dgvCompanySearchResults";
            this.dgvCompanySearchResults.ReadOnly = true;
            this.dgvCompanySearchResults.RowTemplate.Height = 24;
            this.dgvCompanySearchResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompanySearchResults.Size = new System.Drawing.Size(616, 252);
            this.dgvCompanySearchResults.TabIndex = 1;
            this.dgvCompanySearchResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCompanySearchResults_CellContentClick);
            // 
            // colSelectStudent
            // 
            this.colSelectStudent.ActiveLinkColor = System.Drawing.Color.Black;
            this.colSelectStudent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.colSelectStudent.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSelectStudent.HeaderText = "Select";
            this.colSelectStudent.LinkColor = System.Drawing.Color.Black;
            this.colSelectStudent.Name = "colSelectStudent";
            this.colSelectStudent.ReadOnly = true;
            this.colSelectStudent.Text = "Select";
            this.colSelectStudent.TrackVisitedState = false;
            this.colSelectStudent.UseColumnTextForLinkValue = true;
            this.colSelectStudent.VisitedLinkColor = System.Drawing.Color.Black;
            this.colSelectStudent.Width = 43;
            // 
            // companyNameDataGridViewTextBoxColumn
            // 
            this.companyNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.companyNameDataGridViewTextBoxColumn.DataPropertyName = "CompanyName";
            this.companyNameDataGridViewTextBoxColumn.HeaderText = "Company Name";
            this.companyNameDataGridViewTextBoxColumn.MinimumWidth = 105;
            this.companyNameDataGridViewTextBoxColumn.Name = "companyNameDataGridViewTextBoxColumn";
            this.companyNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.companyNameDataGridViewTextBoxColumn.Width = 105;
            // 
            // companySETANumberDataGridViewTextBoxColumn
            // 
            this.companySETANumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.companySETANumberDataGridViewTextBoxColumn.DataPropertyName = "CompanySETANumber";
            this.companySETANumberDataGridViewTextBoxColumn.HeaderText = "SETA Number";
            this.companySETANumberDataGridViewTextBoxColumn.Name = "companySETANumberDataGridViewTextBoxColumn";
            this.companySETANumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // companySicCodeDataGridViewTextBoxColumn
            // 
            this.companySicCodeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.companySicCodeDataGridViewTextBoxColumn.DataPropertyName = "CompanySicCode";
            this.companySicCodeDataGridViewTextBoxColumn.HeaderText = "Sic Code";
            this.companySicCodeDataGridViewTextBoxColumn.Name = "companySicCodeDataGridViewTextBoxColumn";
            this.companySicCodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.companySicCodeDataGridViewTextBoxColumn.Width = 75;
            // 
            // companySARSLevyRegistrationNumberDataGridViewTextBoxColumn
            // 
            this.companySARSLevyRegistrationNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.companySARSLevyRegistrationNumberDataGridViewTextBoxColumn.DataPropertyName = "CompanySARSLevyRegistrationNumber";
            this.companySARSLevyRegistrationNumberDataGridViewTextBoxColumn.HeaderText = "SARS Reg. Number";
            this.companySARSLevyRegistrationNumberDataGridViewTextBoxColumn.Name = "companySARSLevyRegistrationNumberDataGridViewTextBoxColumn";
            this.companySARSLevyRegistrationNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // companyBindingSource
            // 
            this.companyBindingSource.DataSource = typeof(Impendulo.Data.Models.Company);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.BindingSource = this.companyBindingSource;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(616, 27);
            this.bindingNavigator1.Stretch = true;
            this.bindingNavigator1.TabIndex = 0;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 24);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 27);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // frmCompanySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 476);
            this.Controls.Add(this.splitContainer1);
            this.DisplayHeader = false;
            this.Name = "frmCompanySearch";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Text = "Company Search";
            this.Load += new System.EventHandler(this.frmCompanySearchV2_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbSearchForStudent.ResumeLayout(false);
            this.gbSearchForStudent.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompanySearchResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gbSearchForStudent;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSARSRegNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSicCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSETANumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.BindingSource companyBindingSource;
        private System.Windows.Forms.DataGridView dgvCompanySearchResults;
        private System.Windows.Forms.DataGridViewLinkColumn colSelectStudent;
        private System.Windows.Forms.DataGridViewTextBoxColumn companyNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn companySETANumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn companySicCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn companySARSLevyRegistrationNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnAddCompany;
    }
}