namespace Impendulo.Development.Addresses
{
    partial class frmAddUpdateAddresses
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
            System.Windows.Forms.Label lookupProvinceLabel;
            System.Windows.Forms.Label lookupCountryLabel;
            System.Windows.Forms.Label lookupAddressTypeLabel;
            System.Windows.Forms.Label addressLineOneLabel;
            System.Windows.Forms.Label addressLineTwoLabel;
            System.Windows.Forms.Label addressTownLabel;
            System.Windows.Forms.Label addressSuburbLabel;
            System.Windows.Forms.Label addressAreaCodeLabel;
            System.Windows.Forms.Label label1;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cboStudentAddressAddressType = new MetroFramework.Controls.MetroComboBox();
            this.lookupAddressTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtStudentAddressLineOne = new MetroFramework.Controls.MetroTextBox();
            this.txtStudentAddressLineTwo = new MetroFramework.Controls.MetroTextBox();
            this.chkStudnetAddressIsDefault = new MetroFramework.Controls.MetroCheckBox();
            this.cboStudentAddressProvince = new MetroFramework.Controls.MetroComboBox();
            this.lookupProvinceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtStudentAddressTown = new MetroFramework.Controls.MetroTextBox();
            this.txtStudentAddressSuburb = new MetroFramework.Controls.MetroTextBox();
            this.txtStudentAddressAreaCode = new MetroFramework.Controls.MetroTextBox();
            this.cboStudentAddressCountry = new MetroFramework.Controls.MetroComboBox();
            this.lookupCountryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnUpdateAddress = new System.Windows.Forms.Button();
            this.btnAddAddress = new System.Windows.Forms.Button();
            this.btnlCancel = new System.Windows.Forms.Button();
            this.txtAddressDescription = new MetroFramework.Controls.MetroTextBox();
            lookupProvinceLabel = new System.Windows.Forms.Label();
            lookupCountryLabel = new System.Windows.Forms.Label();
            lookupAddressTypeLabel = new System.Windows.Forms.Label();
            addressLineOneLabel = new System.Windows.Forms.Label();
            addressLineTwoLabel = new System.Windows.Forms.Label();
            addressTownLabel = new System.Windows.Forms.Label();
            addressSuburbLabel = new System.Windows.Forms.Label();
            addressAreaCodeLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookupAddressTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupProvinceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupCountryBindingSource)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lookupProvinceLabel
            // 
            lookupProvinceLabel.AutoSize = true;
            lookupProvinceLabel.Location = new System.Drawing.Point(3, 150);
            lookupProvinceLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            lookupProvinceLabel.Name = "lookupProvinceLabel";
            lookupProvinceLabel.Size = new System.Drawing.Size(52, 13);
            lookupProvinceLabel.TabIndex = 62;
            lookupProvinceLabel.Text = "Province:";
            // 
            // lookupCountryLabel
            // 
            lookupCountryLabel.AutoSize = true;
            lookupCountryLabel.Location = new System.Drawing.Point(3, 272);
            lookupCountryLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            lookupCountryLabel.Name = "lookupCountryLabel";
            lookupCountryLabel.Size = new System.Drawing.Size(46, 13);
            lookupCountryLabel.TabIndex = 59;
            lookupCountryLabel.Text = "Country:";
            // 
            // lookupAddressTypeLabel
            // 
            lookupAddressTypeLabel.AutoSize = true;
            lookupAddressTypeLabel.Location = new System.Drawing.Point(3, 36);
            lookupAddressTypeLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            lookupAddressTypeLabel.Name = "lookupAddressTypeLabel";
            lookupAddressTypeLabel.Size = new System.Drawing.Size(75, 13);
            lookupAddressTypeLabel.TabIndex = 58;
            lookupAddressTypeLabel.Text = "Address Type:";
            // 
            // addressLineOneLabel
            // 
            addressLineOneLabel.AutoSize = true;
            addressLineOneLabel.Location = new System.Drawing.Point(3, 92);
            addressLineOneLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            addressLineOneLabel.Name = "addressLineOneLabel";
            addressLineOneLabel.Size = new System.Drawing.Size(94, 13);
            addressLineOneLabel.TabIndex = 47;
            addressLineOneLabel.Text = "Address Line One:";
            // 
            // addressLineTwoLabel
            // 
            addressLineTwoLabel.AutoSize = true;
            addressLineTwoLabel.Location = new System.Drawing.Point(3, 121);
            addressLineTwoLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            addressLineTwoLabel.Name = "addressLineTwoLabel";
            addressLineTwoLabel.Size = new System.Drawing.Size(95, 13);
            addressLineTwoLabel.TabIndex = 49;
            addressLineTwoLabel.Text = "Address Line Two:";
            // 
            // addressTownLabel
            // 
            addressTownLabel.AutoSize = true;
            addressTownLabel.Location = new System.Drawing.Point(3, 185);
            addressTownLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            addressTownLabel.Name = "addressTownLabel";
            addressTownLabel.Size = new System.Drawing.Size(37, 13);
            addressTownLabel.TabIndex = 51;
            addressTownLabel.Text = "Town:";
            // 
            // addressSuburbLabel
            // 
            addressSuburbLabel.AutoSize = true;
            addressSuburbLabel.Location = new System.Drawing.Point(3, 214);
            addressSuburbLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            addressSuburbLabel.Name = "addressSuburbLabel";
            addressSuburbLabel.Size = new System.Drawing.Size(44, 13);
            addressSuburbLabel.TabIndex = 53;
            addressSuburbLabel.Text = "Suburb:";
            // 
            // addressAreaCodeLabel
            // 
            addressAreaCodeLabel.AutoSize = true;
            addressAreaCodeLabel.Location = new System.Drawing.Point(3, 243);
            addressAreaCodeLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            addressAreaCodeLabel.Name = "addressAreaCodeLabel";
            addressAreaCodeLabel.Size = new System.Drawing.Size(60, 13);
            addressAreaCodeLabel.TabIndex = 55;
            addressAreaCodeLabel.Text = "Area Code:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(20, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 364);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Address Details";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboStudentAddressCountry, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.txtStudentAddressAreaCode, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtStudentAddressSuburb, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtStudentAddressTown, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.cboStudentAddressProvince, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtStudentAddressLineTwo, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtStudentAddressLineOne, 1, 3);
            this.tableLayoutPanel1.Controls.Add(lookupAddressTypeLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkStudnetAddressIsDefault, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cboStudentAddressAddressType, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtAddressDescription, 1, 0);
            this.tableLayoutPanel1.Controls.Add(lookupCountryLabel, 0, 9);
            this.tableLayoutPanel1.Controls.Add(addressAreaCodeLabel, 0, 8);
            this.tableLayoutPanel1.Controls.Add(addressSuburbLabel, 0, 7);
            this.tableLayoutPanel1.Controls.Add(addressTownLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(lookupProvinceLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(addressLineTwoLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(addressLineOneLabel, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(491, 302);
            this.tableLayoutPanel1.TabIndex = 67;
            // 
            // cboStudentAddressAddressType
            // 
            this.cboStudentAddressAddressType.DataSource = this.lookupAddressTypeBindingSource;
            this.cboStudentAddressAddressType.DisplayMember = "AddressType";
            this.cboStudentAddressAddressType.FormattingEnabled = true;
            this.cboStudentAddressAddressType.ItemHeight = 23;
            this.cboStudentAddressAddressType.Location = new System.Drawing.Point(123, 32);
            this.cboStudentAddressAddressType.Name = "cboStudentAddressAddressType";
            this.cboStudentAddressAddressType.Size = new System.Drawing.Size(200, 29);
            this.cboStudentAddressAddressType.TabIndex = 64;
            this.cboStudentAddressAddressType.UseSelectable = true;
            this.cboStudentAddressAddressType.UseStyleColors = true;
            this.cboStudentAddressAddressType.ValueMember = "AddressTypeID";
            // 
            // lookupAddressTypeBindingSource
            // 
            this.lookupAddressTypeBindingSource.DataSource = typeof(Impendulo.Data.Models.LookupAddressType);
            // 
            // txtStudentAddressLineOne
            // 
            // 
            // 
            // 
            this.txtStudentAddressLineOne.CustomButton.Image = null;
            this.txtStudentAddressLineOne.CustomButton.Location = new System.Drawing.Point(343, 1);
            this.txtStudentAddressLineOne.CustomButton.Name = "";
            this.txtStudentAddressLineOne.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtStudentAddressLineOne.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtStudentAddressLineOne.CustomButton.TabIndex = 1;
            this.txtStudentAddressLineOne.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtStudentAddressLineOne.CustomButton.UseSelectable = true;
            this.txtStudentAddressLineOne.CustomButton.Visible = false;
            this.txtStudentAddressLineOne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStudentAddressLineOne.Lines = new string[0];
            this.txtStudentAddressLineOne.Location = new System.Drawing.Point(123, 88);
            this.txtStudentAddressLineOne.MaxLength = 32767;
            this.txtStudentAddressLineOne.Name = "txtStudentAddressLineOne";
            this.txtStudentAddressLineOne.PasswordChar = '\0';
            this.txtStudentAddressLineOne.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtStudentAddressLineOne.SelectedText = "";
            this.txtStudentAddressLineOne.SelectionLength = 0;
            this.txtStudentAddressLineOne.SelectionStart = 0;
            this.txtStudentAddressLineOne.ShortcutsEnabled = true;
            this.txtStudentAddressLineOne.Size = new System.Drawing.Size(365, 23);
            this.txtStudentAddressLineOne.TabIndex = 65;
            this.txtStudentAddressLineOne.UseSelectable = true;
            this.txtStudentAddressLineOne.UseStyleColors = true;
            this.txtStudentAddressLineOne.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtStudentAddressLineOne.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtStudentAddressLineTwo
            // 
            // 
            // 
            // 
            this.txtStudentAddressLineTwo.CustomButton.Image = null;
            this.txtStudentAddressLineTwo.CustomButton.Location = new System.Drawing.Point(343, 1);
            this.txtStudentAddressLineTwo.CustomButton.Name = "";
            this.txtStudentAddressLineTwo.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtStudentAddressLineTwo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtStudentAddressLineTwo.CustomButton.TabIndex = 1;
            this.txtStudentAddressLineTwo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtStudentAddressLineTwo.CustomButton.UseSelectable = true;
            this.txtStudentAddressLineTwo.CustomButton.Visible = false;
            this.txtStudentAddressLineTwo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStudentAddressLineTwo.Lines = new string[0];
            this.txtStudentAddressLineTwo.Location = new System.Drawing.Point(123, 117);
            this.txtStudentAddressLineTwo.MaxLength = 32767;
            this.txtStudentAddressLineTwo.Name = "txtStudentAddressLineTwo";
            this.txtStudentAddressLineTwo.PasswordChar = '\0';
            this.txtStudentAddressLineTwo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtStudentAddressLineTwo.SelectedText = "";
            this.txtStudentAddressLineTwo.SelectionLength = 0;
            this.txtStudentAddressLineTwo.SelectionStart = 0;
            this.txtStudentAddressLineTwo.ShortcutsEnabled = true;
            this.txtStudentAddressLineTwo.Size = new System.Drawing.Size(365, 23);
            this.txtStudentAddressLineTwo.TabIndex = 66;
            this.txtStudentAddressLineTwo.UseSelectable = true;
            this.txtStudentAddressLineTwo.UseStyleColors = true;
            this.txtStudentAddressLineTwo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtStudentAddressLineTwo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // chkStudnetAddressIsDefault
            // 
            this.chkStudnetAddressIsDefault.AutoSize = true;
            this.chkStudnetAddressIsDefault.Location = new System.Drawing.Point(123, 67);
            this.chkStudnetAddressIsDefault.Name = "chkStudnetAddressIsDefault";
            this.chkStudnetAddressIsDefault.Size = new System.Drawing.Size(72, 15);
            this.chkStudnetAddressIsDefault.TabIndex = 67;
            this.chkStudnetAddressIsDefault.Text = "Is Default";
            this.chkStudnetAddressIsDefault.UseSelectable = true;
            // 
            // cboStudentAddressProvince
            // 
            this.cboStudentAddressProvince.DataSource = this.lookupProvinceBindingSource;
            this.cboStudentAddressProvince.DisplayMember = "Province";
            this.cboStudentAddressProvince.FormattingEnabled = true;
            this.cboStudentAddressProvince.ItemHeight = 23;
            this.cboStudentAddressProvince.Location = new System.Drawing.Point(123, 146);
            this.cboStudentAddressProvince.Name = "cboStudentAddressProvince";
            this.cboStudentAddressProvince.Size = new System.Drawing.Size(200, 29);
            this.cboStudentAddressProvince.TabIndex = 68;
            this.cboStudentAddressProvince.UseSelectable = true;
            this.cboStudentAddressProvince.UseStyleColors = true;
            this.cboStudentAddressProvince.ValueMember = "ProvinceID";
            // 
            // lookupProvinceBindingSource
            // 
            this.lookupProvinceBindingSource.DataSource = typeof(Impendulo.Data.Models.LookupProvince);
            // 
            // txtStudentAddressTown
            // 
            // 
            // 
            // 
            this.txtStudentAddressTown.CustomButton.Image = null;
            this.txtStudentAddressTown.CustomButton.Location = new System.Drawing.Point(343, 1);
            this.txtStudentAddressTown.CustomButton.Name = "";
            this.txtStudentAddressTown.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtStudentAddressTown.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtStudentAddressTown.CustomButton.TabIndex = 1;
            this.txtStudentAddressTown.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtStudentAddressTown.CustomButton.UseSelectable = true;
            this.txtStudentAddressTown.CustomButton.Visible = false;
            this.txtStudentAddressTown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStudentAddressTown.Lines = new string[0];
            this.txtStudentAddressTown.Location = new System.Drawing.Point(123, 181);
            this.txtStudentAddressTown.MaxLength = 32767;
            this.txtStudentAddressTown.Name = "txtStudentAddressTown";
            this.txtStudentAddressTown.PasswordChar = '\0';
            this.txtStudentAddressTown.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtStudentAddressTown.SelectedText = "";
            this.txtStudentAddressTown.SelectionLength = 0;
            this.txtStudentAddressTown.SelectionStart = 0;
            this.txtStudentAddressTown.ShortcutsEnabled = true;
            this.txtStudentAddressTown.Size = new System.Drawing.Size(365, 23);
            this.txtStudentAddressTown.TabIndex = 69;
            this.txtStudentAddressTown.UseSelectable = true;
            this.txtStudentAddressTown.UseStyleColors = true;
            this.txtStudentAddressTown.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtStudentAddressTown.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtStudentAddressSuburb
            // 
            // 
            // 
            // 
            this.txtStudentAddressSuburb.CustomButton.Image = null;
            this.txtStudentAddressSuburb.CustomButton.Location = new System.Drawing.Point(343, 1);
            this.txtStudentAddressSuburb.CustomButton.Name = "";
            this.txtStudentAddressSuburb.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtStudentAddressSuburb.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtStudentAddressSuburb.CustomButton.TabIndex = 1;
            this.txtStudentAddressSuburb.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtStudentAddressSuburb.CustomButton.UseSelectable = true;
            this.txtStudentAddressSuburb.CustomButton.Visible = false;
            this.txtStudentAddressSuburb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStudentAddressSuburb.Lines = new string[0];
            this.txtStudentAddressSuburb.Location = new System.Drawing.Point(123, 210);
            this.txtStudentAddressSuburb.MaxLength = 32767;
            this.txtStudentAddressSuburb.Name = "txtStudentAddressSuburb";
            this.txtStudentAddressSuburb.PasswordChar = '\0';
            this.txtStudentAddressSuburb.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtStudentAddressSuburb.SelectedText = "";
            this.txtStudentAddressSuburb.SelectionLength = 0;
            this.txtStudentAddressSuburb.SelectionStart = 0;
            this.txtStudentAddressSuburb.ShortcutsEnabled = true;
            this.txtStudentAddressSuburb.Size = new System.Drawing.Size(365, 23);
            this.txtStudentAddressSuburb.TabIndex = 70;
            this.txtStudentAddressSuburb.UseSelectable = true;
            this.txtStudentAddressSuburb.UseStyleColors = true;
            this.txtStudentAddressSuburb.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtStudentAddressSuburb.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtStudentAddressAreaCode
            // 
            // 
            // 
            // 
            this.txtStudentAddressAreaCode.CustomButton.Image = null;
            this.txtStudentAddressAreaCode.CustomButton.Location = new System.Drawing.Point(343, 1);
            this.txtStudentAddressAreaCode.CustomButton.Name = "";
            this.txtStudentAddressAreaCode.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtStudentAddressAreaCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtStudentAddressAreaCode.CustomButton.TabIndex = 1;
            this.txtStudentAddressAreaCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtStudentAddressAreaCode.CustomButton.UseSelectable = true;
            this.txtStudentAddressAreaCode.CustomButton.Visible = false;
            this.txtStudentAddressAreaCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStudentAddressAreaCode.Lines = new string[0];
            this.txtStudentAddressAreaCode.Location = new System.Drawing.Point(123, 239);
            this.txtStudentAddressAreaCode.MaxLength = 32767;
            this.txtStudentAddressAreaCode.Name = "txtStudentAddressAreaCode";
            this.txtStudentAddressAreaCode.PasswordChar = '\0';
            this.txtStudentAddressAreaCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtStudentAddressAreaCode.SelectedText = "";
            this.txtStudentAddressAreaCode.SelectionLength = 0;
            this.txtStudentAddressAreaCode.SelectionStart = 0;
            this.txtStudentAddressAreaCode.ShortcutsEnabled = true;
            this.txtStudentAddressAreaCode.Size = new System.Drawing.Size(365, 23);
            this.txtStudentAddressAreaCode.TabIndex = 71;
            this.txtStudentAddressAreaCode.UseSelectable = true;
            this.txtStudentAddressAreaCode.UseStyleColors = true;
            this.txtStudentAddressAreaCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtStudentAddressAreaCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // cboStudentAddressCountry
            // 
            this.cboStudentAddressCountry.DataSource = this.lookupCountryBindingSource;
            this.cboStudentAddressCountry.DisplayMember = "CountryName";
            this.cboStudentAddressCountry.FormattingEnabled = true;
            this.cboStudentAddressCountry.ItemHeight = 23;
            this.cboStudentAddressCountry.Location = new System.Drawing.Point(123, 268);
            this.cboStudentAddressCountry.Name = "cboStudentAddressCountry";
            this.cboStudentAddressCountry.Size = new System.Drawing.Size(200, 29);
            this.cboStudentAddressCountry.TabIndex = 72;
            this.cboStudentAddressCountry.UseSelectable = true;
            this.cboStudentAddressCountry.UseStyleColors = true;
            this.cboStudentAddressCountry.ValueMember = "CountryID";
            // 
            // lookupCountryBindingSource
            // 
            this.lookupCountryBindingSource.DataSource = typeof(Impendulo.Data.Models.LookupCountry);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnUpdateAddress);
            this.flowLayoutPanel1.Controls.Add(this.btnAddAddress);
            this.flowLayoutPanel1.Controls.Add(this.btnlCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 318);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(491, 43);
            this.flowLayoutPanel1.TabIndex = 66;
            // 
            // btnUpdateAddress
            // 
            this.btnUpdateAddress.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdateAddress.Location = new System.Drawing.Point(411, 3);
            this.btnUpdateAddress.Name = "btnUpdateAddress";
            this.btnUpdateAddress.Size = new System.Drawing.Size(77, 37);
            this.btnUpdateAddress.TabIndex = 65;
            this.btnUpdateAddress.Text = "Update";
            this.btnUpdateAddress.UseVisualStyleBackColor = true;
            this.btnUpdateAddress.Click += new System.EventHandler(this.btnStudentAddressAddUpdate_Click);
            // 
            // btnAddAddress
            // 
            this.btnAddAddress.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddAddress.Location = new System.Drawing.Point(328, 3);
            this.btnAddAddress.Name = "btnAddAddress";
            this.btnAddAddress.Size = new System.Drawing.Size(77, 37);
            this.btnAddAddress.TabIndex = 66;
            this.btnAddAddress.Text = "Add";
            this.btnAddAddress.UseVisualStyleBackColor = true;
            this.btnAddAddress.Click += new System.EventHandler(this.btnAddAddress_Click);
            // 
            // btnlCancel
            // 
            this.btnlCancel.Location = new System.Drawing.Point(247, 3);
            this.btnlCancel.Name = "btnlCancel";
            this.btnlCancel.Size = new System.Drawing.Size(75, 36);
            this.btnlCancel.TabIndex = 64;
            this.btnlCancel.Text = "Cancel";
            this.btnlCancel.UseVisualStyleBackColor = true;
            this.btnlCancel.Click += new System.EventHandler(this.btnStudentAddressCancelAddUpdate_Click);
            // 
            // txtAddressDescription
            // 
            // 
            // 
            // 
            this.txtAddressDescription.CustomButton.Image = null;
            this.txtAddressDescription.CustomButton.Location = new System.Drawing.Point(343, 1);
            this.txtAddressDescription.CustomButton.Name = "";
            this.txtAddressDescription.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtAddressDescription.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtAddressDescription.CustomButton.TabIndex = 1;
            this.txtAddressDescription.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtAddressDescription.CustomButton.UseSelectable = true;
            this.txtAddressDescription.CustomButton.Visible = false;
            this.txtAddressDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddressDescription.Lines = new string[0];
            this.txtAddressDescription.Location = new System.Drawing.Point(123, 3);
            this.txtAddressDescription.MaxLength = 32767;
            this.txtAddressDescription.Name = "txtAddressDescription";
            this.txtAddressDescription.PasswordChar = '\0';
            //this.txtAddressDescription.PromptText = "Enter User Frienldy Description - Used to identify this address.";
            this.txtAddressDescription.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAddressDescription.SelectedText = "";
            this.txtAddressDescription.SelectionLength = 0;
            this.txtAddressDescription.SelectionStart = 0;
            this.txtAddressDescription.ShortcutsEnabled = true;
            this.txtAddressDescription.Size = new System.Drawing.Size(365, 23);
            this.txtAddressDescription.TabIndex = 73;
            this.txtAddressDescription.UseSelectable = true;
            this.txtAddressDescription.UseStyleColors = true;
            this.txtAddressDescription.WaterMark = "Enter User Frienldy Description - Used to identify this address.";
            this.txtAddressDescription.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtAddressDescription.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 7);
            label1.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(104, 13);
            label1.TabIndex = 74;
            label1.Text = "Address Description:";
            // 
            // frmAddUpdateAddresses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 444);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmAddUpdateAddresses";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Add/Update Address Details";
            this.Load += new System.EventHandler(this.frmAddresses_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookupAddressTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupProvinceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupCountryBindingSource)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnlCancel;
        private System.Windows.Forms.Button btnUpdateAddress;
        private System.Windows.Forms.Button btnAddAddress;
        private System.Windows.Forms.BindingSource lookupProvinceBindingSource;
        private System.Windows.Forms.BindingSource lookupCountryBindingSource;
        private System.Windows.Forms.BindingSource lookupAddressTypeBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroComboBox cboStudentAddressAddressType;
        private MetroFramework.Controls.MetroTextBox txtStudentAddressLineOne;
        private MetroFramework.Controls.MetroTextBox txtStudentAddressLineTwo;
        private MetroFramework.Controls.MetroCheckBox chkStudnetAddressIsDefault;
        private MetroFramework.Controls.MetroComboBox cboStudentAddressProvince;
        private MetroFramework.Controls.MetroTextBox txtStudentAddressTown;
        private MetroFramework.Controls.MetroTextBox txtStudentAddressSuburb;
        private MetroFramework.Controls.MetroTextBox txtStudentAddressAreaCode;
        private MetroFramework.Controls.MetroComboBox cboStudentAddressCountry;
        private MetroFramework.Controls.MetroTextBox txtAddressDescription;
    }
}

