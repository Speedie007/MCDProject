namespace Impendulo.Deployments.Reports.Reports.Schedules
{
    partial class frmStudentRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStudentRegister));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.gbCustomDatePeriodSelector = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroDateTime2 = new MetroFramework.Controls.MetroDateTime();
            this.metroDateTime1 = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.radCustomDateSelection = new MetroFramework.Controls.MetroRadioButton();
            this.radCurrentMonthSelection = new MetroFramework.Controls.MetroRadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.radSelectOnlyNotYetConfirmed = new MetroFramework.Controls.MetroRadioButton();
            this.radSelectOnlyConfirmedSchedules = new MetroFramework.Controls.MetroRadioButton();
            this.radSelectAllSchedules = new MetroFramework.Controls.MetroRadioButton();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.gbCustomDatePeriodSelector.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.crystalReportViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(1141, 683);
            this.splitContainer1.SplitterDistance = 158;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1141, 158);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Filters";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel3);
            this.groupBox3.Location = new System.Drawing.Point(6, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(749, 136);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.gbCustomDatePeriodSelector, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(743, 117);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // gbCustomDatePeriodSelector
            // 
            this.gbCustomDatePeriodSelector.Controls.Add(this.tableLayoutPanel1);
            this.gbCustomDatePeriodSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCustomDatePeriodSelector.Location = new System.Drawing.Point(3, 57);
            this.gbCustomDatePeriodSelector.Name = "gbCustomDatePeriodSelector";
            this.gbCustomDatePeriodSelector.Size = new System.Drawing.Size(737, 57);
            this.gbCustomDatePeriodSelector.TabIndex = 2;
            this.gbCustomDatePeriodSelector.TabStop = false;
            this.gbCustomDatePeriodSelector.Text = "Date Selection";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.Controls.Add(this.metroLabel2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.metroDateTime2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.metroDateTime1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(731, 38);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(337, 3);
            this.metroLabel2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(121, 19);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "Select From Period";
            // 
            // metroDateTime2
            // 
            this.metroDateTime2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroDateTime2.Location = new System.Drawing.Point(489, 3);
            this.metroDateTime2.MinimumSize = new System.Drawing.Size(0, 29);
            this.metroDateTime2.Name = "metroDateTime2";
            this.metroDateTime2.Size = new System.Drawing.Size(192, 29);
            this.metroDateTime2.TabIndex = 2;
            this.metroDateTime2.ValueChanged += new System.EventHandler(this.metroDateTime2_ValueChanged);
            // 
            // metroDateTime1
            // 
            this.metroDateTime1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroDateTime1.Location = new System.Drawing.Point(139, 3);
            this.metroDateTime1.MinimumSize = new System.Drawing.Size(0, 29);
            this.metroDateTime1.Name = "metroDateTime1";
            this.metroDateTime1.Size = new System.Drawing.Size(192, 29);
            this.metroDateTime1.TabIndex = 1;
            this.metroDateTime1.ValueChanged += new System.EventHandler(this.metroDateTime1_ValueChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(3, 3);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(121, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Select From Period";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(687, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Filter By Dates Selected.");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 406F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox5, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(737, 48);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel5);
            this.groupBox5.Location = new System.Drawing.Point(409, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(325, 41);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Date Type Selection";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.radCustomDateSelection, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.radCurrentMonthSelection, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(319, 22);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // radCustomDateSelection
            // 
            this.radCustomDateSelection.AutoSize = true;
            this.radCustomDateSelection.Location = new System.Drawing.Point(162, 3);
            this.radCustomDateSelection.Name = "radCustomDateSelection";
            this.radCustomDateSelection.Size = new System.Drawing.Size(143, 15);
            this.radCustomDateSelection.TabIndex = 1;
            this.radCustomDateSelection.Text = "Custom Date Selection";
            this.radCustomDateSelection.UseSelectable = true;
            this.radCustomDateSelection.UseStyleColors = true;
            this.radCustomDateSelection.CheckedChanged += new System.EventHandler(this.radCustomDateSelection_CheckedChanged);
            // 
            // radCurrentMonthSelection
            // 
            this.radCurrentMonthSelection.AutoSize = true;
            this.radCurrentMonthSelection.Checked = true;
            this.radCurrentMonthSelection.Location = new System.Drawing.Point(3, 3);
            this.radCurrentMonthSelection.Name = "radCurrentMonthSelection";
            this.radCurrentMonthSelection.Size = new System.Drawing.Size(102, 15);
            this.radCurrentMonthSelection.TabIndex = 0;
            this.radCurrentMonthSelection.TabStop = true;
            this.radCurrentMonthSelection.Text = "Current Month";
            this.radCurrentMonthSelection.UseSelectable = true;
            this.radCurrentMonthSelection.UseStyleColors = true;
            this.radCurrentMonthSelection.CheckedChanged += new System.EventHandler(this.radCurrentMonthSelection_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 41);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Confirmation Selection";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.radSelectOnlyNotYetConfirmed, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.radSelectOnlyConfirmedSchedules, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.radSelectAllSchedules, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(394, 22);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // radSelectOnlyNotYetConfirmed
            // 
            this.radSelectOnlyNotYetConfirmed.AutoSize = true;
            this.radSelectOnlyNotYetConfirmed.Location = new System.Drawing.Point(228, 3);
            this.radSelectOnlyNotYetConfirmed.Name = "radSelectOnlyNotYetConfirmed";
            this.radSelectOnlyNotYetConfirmed.Size = new System.Drawing.Size(156, 15);
            this.radSelectOnlyNotYetConfirmed.TabIndex = 2;
            this.radSelectOnlyNotYetConfirmed.Text = "Select Not Yet Confirmed";
            this.radSelectOnlyNotYetConfirmed.UseSelectable = true;
            this.radSelectOnlyNotYetConfirmed.UseStyleColors = true;
            this.radSelectOnlyNotYetConfirmed.CheckedChanged += new System.EventHandler(this.radSelectOnlyNotYetConfirmed_CheckedChanged);
            // 
            // radSelectOnlyConfirmedSchedules
            // 
            this.radSelectOnlyConfirmedSchedules.AutoSize = true;
            this.radSelectOnlyConfirmedSchedules.Location = new System.Drawing.Point(80, 3);
            this.radSelectOnlyConfirmedSchedules.Name = "radSelectOnlyConfirmedSchedules";
            this.radSelectOnlyConfirmedSchedules.Size = new System.Drawing.Size(142, 15);
            this.radSelectOnlyConfirmedSchedules.TabIndex = 1;
            this.radSelectOnlyConfirmedSchedules.Text = "Select Only Confirmed";
            this.radSelectOnlyConfirmedSchedules.UseSelectable = true;
            this.radSelectOnlyConfirmedSchedules.UseStyleColors = true;
            this.radSelectOnlyConfirmedSchedules.CheckedChanged += new System.EventHandler(this.radSelectOnlyConfirmedSchedules_CheckedChanged);
            // 
            // radSelectAllSchedules
            // 
            this.radSelectAllSchedules.AutoSize = true;
            this.radSelectAllSchedules.Checked = true;
            this.radSelectAllSchedules.Location = new System.Drawing.Point(3, 3);
            this.radSelectAllSchedules.Name = "radSelectAllSchedules";
            this.radSelectAllSchedules.Size = new System.Drawing.Size(71, 15);
            this.radSelectAllSchedules.TabIndex = 0;
            this.radSelectAllSchedules.TabStop = true;
            this.radSelectAllSchedules.Text = "Select All";
            this.radSelectAllSchedules.UseSelectable = true;
            this.radSelectAllSchedules.UseStyleColors = true;
            this.radSelectAllSchedules.CheckedChanged += new System.EventHandler(this.radSelectAllSchedules_CheckedChanged);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1141, 521);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // frmStudentRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 733);
            this.Controls.Add(this.splitContainer1);
            this.DisplayHeader = false;
            this.Name = "frmStudentRegister";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Text = "frmStudentRegister";
            this.Load += new System.EventHandler(this.frmStudentRegister_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.gbCustomDatePeriodSelector.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroDateTime metroDateTime2;
        private MetroFramework.Controls.MetroDateTime metroDateTime1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox gbCustomDatePeriodSelector;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private MetroFramework.Controls.MetroRadioButton radCustomDateSelection;
        private MetroFramework.Controls.MetroRadioButton radCurrentMonthSelection;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MetroFramework.Controls.MetroRadioButton radSelectOnlyNotYetConfirmed;
        private MetroFramework.Controls.MetroRadioButton radSelectOnlyConfirmedSchedules;
        private MetroFramework.Controls.MetroRadioButton radSelectAllSchedules;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}