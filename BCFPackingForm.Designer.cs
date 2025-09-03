using System.Data;
using System.Windows.Forms;

namespace PackingApplication
{
    partial class BCFPackingForm
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
            this.backbutton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.boxnoerror = new System.Windows.Forms.Label();
            this.windingerror = new System.Windows.Forms.Label();
            this.packsizeerror = new System.Windows.Forms.Label();
            this.soerror = new System.Windows.Forms.Label();
            this.qualityerror = new System.Windows.Forms.Label();
            this.mergenoerror = new System.Windows.Forms.Label();
            this.copynoerror = new System.Windows.Forms.Label();
            this.linenoerror = new System.Windows.Forms.Label();
            this.deniervalue = new System.Windows.Forms.TextBox();
            this.denier = new System.Windows.Forms.Label();
            this.lastboxdetails = new System.Windows.Forms.GroupBox();
            this.netwttxtbox = new System.Windows.Forms.TextBox();
            this.netweight = new System.Windows.Forms.Label();
            this.grosswttxtbox = new System.Windows.Forms.TextBox();
            this.grossweight = new System.Windows.Forms.Label();
            this.tarewghttxtbox = new System.Windows.Forms.TextBox();
            this.tareweight = new System.Windows.Forms.Label();
            this.copstxtbox = new System.Windows.Forms.TextBox();
            this.cops = new System.Windows.Forms.Label();
            this.lastboxldetailist = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grosswterror = new System.Windows.Forms.Label();
            this.palletwterror = new System.Windows.Forms.Label();
            this.spoolwterror = new System.Windows.Forms.Label();
            this.spoolnoerror = new System.Windows.Forms.Label();
            this.wtpercop = new System.Windows.Forms.TextBox();
            this.spool = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.spoolno = new System.Windows.Forms.TextBox();
            this.netwt = new System.Windows.Forms.TextBox();
            this.spoolwt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.palletwt = new System.Windows.Forms.Label();
            this.tarewt = new System.Windows.Forms.TextBox();
            this.palletwtno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grosswtno = new System.Windows.Forms.TextBox();
            this.wgroupbox = new System.Windows.Forms.GroupBox();
            this.windinggrid = new System.Windows.Forms.DataGridView();
            this.windingqty = new System.Data.DataTable();
            this.gradewiseprodn = new System.Windows.Forms.GroupBox();
            this.totalprodbalqty = new System.Windows.Forms.Label();
            this.qualityqty = new System.Windows.Forms.DataGridView();
            this.qualityandqty = new System.Data.DataTable();
            this.saleordrqty = new System.Windows.Forms.Label();
            this.addqty = new System.Windows.Forms.Button();
            this.qnty = new System.Windows.Forms.TextBox();
            this.pquantity = new System.Windows.Forms.Label();
            this.PalletTypeList = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.prowner = new System.Windows.Forms.CheckBox();
            this.prwtps = new System.Windows.Forms.CheckBox();
            this.prcompany = new System.Windows.Forms.CheckBox();
            this.prqrcode = new System.Windows.Forms.CheckBox();
            this.pruser = new System.Windows.Forms.CheckBox();
            this.prdate = new System.Windows.Forms.CheckBox();
            this.PrefixList = new System.Windows.Forms.ComboBox();
            this.lineno = new System.Windows.Forms.Label();
            this.LineNoList = new System.Windows.Forms.ComboBox();
            this.lastboxno = new System.Windows.Forms.Label();
            this.lastbox = new System.Windows.Forms.TextBox();
            this.submit = new System.Windows.Forms.Button();
            this.department = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.departmentname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.copyno = new System.Windows.Forms.TextBox();
            this.boxno = new System.Windows.Forms.Label();
            this.mergeno = new System.Windows.Forms.Label();
            this.MergeNoList = new System.Windows.Forms.ComboBox();
            this.packingdate = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.prodtype = new System.Windows.Forms.TextBox();
            this.item = new System.Windows.Forms.Label();
            this.itemname = new System.Windows.Forms.TextBox();
            this.shade = new System.Windows.Forms.Label();
            this.shadename = new System.Windows.Forms.TextBox();
            this.shadecd = new System.Windows.Forms.TextBox();
            this.shadecode = new System.Windows.Forms.Label();
            this.remarks = new System.Windows.Forms.TextBox();
            this.remark = new System.Windows.Forms.Label();
            this.QualityList = new System.Windows.Forms.ComboBox();
            this.BoxItemList = new System.Windows.Forms.ComboBox();
            this.productiontype = new System.Windows.Forms.Label();
            this.quality = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.boxstock = new System.Windows.Forms.Label();
            this.saleorderno = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SaleOrderList = new System.Windows.Forms.ComboBox();
            this.boxweight = new System.Windows.Forms.Label();
            this.packsize = new System.Windows.Forms.Label();
            this.PackSizeList = new System.Windows.Forms.ComboBox();
            this.CopsItemList = new System.Windows.Forms.ComboBox();
            this.boxtype = new System.Windows.Forms.Label();
            this.frdenier = new System.Windows.Forms.TextBox();
            this.WeighingList = new System.Windows.Forms.ComboBox();
            this.updenier = new System.Windows.Forms.TextBox();
            this.scalemodel = new System.Windows.Forms.Label();
            this.ComPortList = new System.Windows.Forms.ComboBox();
            this.windingtype = new System.Windows.Forms.Label();
            this.WindingTypeList = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comport = new System.Windows.Forms.Label();
            this.copstock = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.copssize = new System.Windows.Forms.Label();
            this.copweight = new System.Windows.Forms.Label();
            this.bcfformlabel = new System.Windows.Forms.Label();
            this.windingtypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soqtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.windingbalqtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.windingprodqtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.lastboxdetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.wgroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windinggrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.windingqty)).BeginInit();
            this.gradewiseprodn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualityqty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityandqty)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // backbutton
            // 
            this.backbutton.BackColor = System.Drawing.SystemColors.Highlight;
            this.backbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.backbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backbutton.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backbutton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.backbutton.Location = new System.Drawing.Point(1208, 6);
            this.backbutton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.backbutton.Name = "backbutton";
            this.backbutton.Size = new System.Drawing.Size(82, 35);
            this.backbutton.TabIndex = 91;
            this.backbutton.Text = "Back";
            this.backbutton.UseVisualStyleBackColor = false;
            this.backbutton.Click += new System.EventHandler(this.backbutton_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.boxnoerror);
            this.panel2.Controls.Add(this.windingerror);
            this.panel2.Controls.Add(this.packsizeerror);
            this.panel2.Controls.Add(this.soerror);
            this.panel2.Controls.Add(this.qualityerror);
            this.panel2.Controls.Add(this.mergenoerror);
            this.panel2.Controls.Add(this.copynoerror);
            this.panel2.Controls.Add(this.linenoerror);
            this.panel2.Controls.Add(this.deniervalue);
            this.panel2.Controls.Add(this.denier);
            this.panel2.Controls.Add(this.lastboxdetails);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.wgroupbox);
            this.panel2.Controls.Add(this.gradewiseprodn);
            this.panel2.Controls.Add(this.addqty);
            this.panel2.Controls.Add(this.qnty);
            this.panel2.Controls.Add(this.pquantity);
            this.panel2.Controls.Add(this.PalletTypeList);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.PrefixList);
            this.panel2.Controls.Add(this.lineno);
            this.panel2.Controls.Add(this.LineNoList);
            this.panel2.Controls.Add(this.lastboxno);
            this.panel2.Controls.Add(this.lastbox);
            this.panel2.Controls.Add(this.submit);
            this.panel2.Controls.Add(this.department);
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.departmentname);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.copyno);
            this.panel2.Controls.Add(this.boxno);
            this.panel2.Controls.Add(this.mergeno);
            this.panel2.Controls.Add(this.MergeNoList);
            this.panel2.Controls.Add(this.packingdate);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.prodtype);
            this.panel2.Controls.Add(this.item);
            this.panel2.Controls.Add(this.itemname);
            this.panel2.Controls.Add(this.shade);
            this.panel2.Controls.Add(this.shadename);
            this.panel2.Controls.Add(this.shadecd);
            this.panel2.Controls.Add(this.shadecode);
            this.panel2.Controls.Add(this.remarks);
            this.panel2.Controls.Add(this.remark);
            this.panel2.Controls.Add(this.QualityList);
            this.panel2.Controls.Add(this.BoxItemList);
            this.panel2.Controls.Add(this.productiontype);
            this.panel2.Controls.Add(this.quality);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.boxstock);
            this.panel2.Controls.Add(this.saleorderno);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.SaleOrderList);
            this.panel2.Controls.Add(this.boxweight);
            this.panel2.Controls.Add(this.packsize);
            this.panel2.Controls.Add(this.PackSizeList);
            this.panel2.Controls.Add(this.CopsItemList);
            this.panel2.Controls.Add(this.boxtype);
            this.panel2.Controls.Add(this.frdenier);
            this.panel2.Controls.Add(this.WeighingList);
            this.panel2.Controls.Add(this.updenier);
            this.panel2.Controls.Add(this.scalemodel);
            this.panel2.Controls.Add(this.ComPortList);
            this.panel2.Controls.Add(this.windingtype);
            this.panel2.Controls.Add(this.WindingTypeList);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.comport);
            this.panel2.Controls.Add(this.copstock);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.copssize);
            this.panel2.Controls.Add(this.copweight);
            this.panel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Location = new System.Drawing.Point(18, 51);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1276, 995);
            this.panel2.TabIndex = 92;
            // 
            // boxnoerror
            // 
            this.boxnoerror.AutoSize = true;
            this.boxnoerror.ForeColor = System.Drawing.Color.Red;
            this.boxnoerror.Location = new System.Drawing.Point(427, 113);
            this.boxnoerror.Name = "boxnoerror";
            this.boxnoerror.Size = new System.Drawing.Size(0, 20);
            this.boxnoerror.TabIndex = 105;
            this.boxnoerror.Visible = false;
            // 
            // windingerror
            // 
            this.windingerror.AutoSize = true;
            this.windingerror.ForeColor = System.Drawing.Color.Red;
            this.windingerror.Location = new System.Drawing.Point(436, 410);
            this.windingerror.Name = "windingerror";
            this.windingerror.Size = new System.Drawing.Size(0, 20);
            this.windingerror.TabIndex = 104;
            this.windingerror.Visible = false;
            // 
            // packsizeerror
            // 
            this.packsizeerror.AutoSize = true;
            this.packsizeerror.ForeColor = System.Drawing.Color.Red;
            this.packsizeerror.Location = new System.Drawing.Point(100, 410);
            this.packsizeerror.Name = "packsizeerror";
            this.packsizeerror.Size = new System.Drawing.Size(0, 20);
            this.packsizeerror.TabIndex = 103;
            this.packsizeerror.Visible = false;
            // 
            // soerror
            // 
            this.soerror.AutoSize = true;
            this.soerror.ForeColor = System.Drawing.Color.Red;
            this.soerror.Location = new System.Drawing.Point(427, 347);
            this.soerror.Name = "soerror";
            this.soerror.Size = new System.Drawing.Size(0, 20);
            this.soerror.TabIndex = 102;
            // 
            // qualityerror
            // 
            this.qualityerror.AutoSize = true;
            this.qualityerror.ForeColor = System.Drawing.Color.Red;
            this.qualityerror.Location = new System.Drawing.Point(101, 347);
            this.qualityerror.Name = "qualityerror";
            this.qualityerror.Size = new System.Drawing.Size(0, 20);
            this.qualityerror.TabIndex = 101;
            this.qualityerror.Visible = false;
            // 
            // mergenoerror
            // 
            this.mergenoerror.AutoSize = true;
            this.mergenoerror.ForeColor = System.Drawing.Color.Red;
            this.mergenoerror.Location = new System.Drawing.Point(96, 182);
            this.mergenoerror.Name = "mergenoerror";
            this.mergenoerror.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mergenoerror.Size = new System.Drawing.Size(0, 20);
            this.mergenoerror.TabIndex = 100;
            this.mergenoerror.Visible = false;
            // 
            // copynoerror
            // 
            this.copynoerror.AutoSize = true;
            this.copynoerror.ForeColor = System.Drawing.Color.Red;
            this.copynoerror.Location = new System.Drawing.Point(673, 376);
            this.copynoerror.Name = "copynoerror";
            this.copynoerror.Size = new System.Drawing.Size(0, 20);
            this.copynoerror.TabIndex = 99;
            this.copynoerror.Visible = false;
            // 
            // linenoerror
            // 
            this.linenoerror.AutoSize = true;
            this.linenoerror.BackColor = System.Drawing.SystemColors.Control;
            this.linenoerror.ForeColor = System.Drawing.Color.Red;
            this.linenoerror.Location = new System.Drawing.Point(101, 45);
            this.linenoerror.Name = "linenoerror";
            this.linenoerror.Size = new System.Drawing.Size(0, 20);
            this.linenoerror.TabIndex = 98;
            this.linenoerror.Visible = false;
            // 
            // deniervalue
            // 
            this.deniervalue.Location = new System.Drawing.Point(423, 258);
            this.deniervalue.Name = "deniervalue";
            this.deniervalue.ReadOnly = true;
            this.deniervalue.Size = new System.Drawing.Size(199, 26);
            this.deniervalue.TabIndex = 95;
            // 
            // denier
            // 
            this.denier.AutoSize = true;
            this.denier.Location = new System.Drawing.Point(342, 261);
            this.denier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.denier.Name = "denier";
            this.denier.Size = new System.Drawing.Size(60, 20);
            this.denier.TabIndex = 94;
            this.denier.Text = "Denier:";
            // 
            // lastboxdetails
            // 
            this.lastboxdetails.Controls.Add(this.netwttxtbox);
            this.lastboxdetails.Controls.Add(this.netweight);
            this.lastboxdetails.Controls.Add(this.grosswttxtbox);
            this.lastboxdetails.Controls.Add(this.grossweight);
            this.lastboxdetails.Controls.Add(this.tarewghttxtbox);
            this.lastboxdetails.Controls.Add(this.tareweight);
            this.lastboxdetails.Controls.Add(this.copstxtbox);
            this.lastboxdetails.Controls.Add(this.cops);
            this.lastboxdetails.Controls.Add(this.lastboxldetailist);
            this.lastboxdetails.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastboxdetails.Location = new System.Drawing.Point(664, 435);
            this.lastboxdetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lastboxdetails.Name = "lastboxdetails";
            this.lastboxdetails.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lastboxdetails.Size = new System.Drawing.Size(255, 211);
            this.lastboxdetails.TabIndex = 93;
            this.lastboxdetails.TabStop = false;
            this.lastboxdetails.Text = "Last Box Details";
            // 
            // netwttxtbox
            // 
            this.netwttxtbox.Location = new System.Drawing.Point(129, 157);
            this.netwttxtbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.netwttxtbox.Name = "netwttxtbox";
            this.netwttxtbox.ReadOnly = true;
            this.netwttxtbox.Size = new System.Drawing.Size(100, 28);
            this.netwttxtbox.TabIndex = 94;
            // 
            // netweight
            // 
            this.netweight.AutoSize = true;
            this.netweight.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.netweight.Location = new System.Drawing.Point(16, 162);
            this.netweight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.netweight.Name = "netweight";
            this.netweight.Size = new System.Drawing.Size(98, 22);
            this.netweight.TabIndex = 7;
            this.netweight.Text = "Net Weight";
            // 
            // grosswttxtbox
            // 
            this.grosswttxtbox.Location = new System.Drawing.Point(130, 115);
            this.grosswttxtbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grosswttxtbox.Name = "grosswttxtbox";
            this.grosswttxtbox.ReadOnly = true;
            this.grosswttxtbox.Size = new System.Drawing.Size(98, 28);
            this.grosswttxtbox.TabIndex = 6;
            // 
            // grossweight
            // 
            this.grossweight.AutoSize = true;
            this.grossweight.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grossweight.Location = new System.Drawing.Point(16, 120);
            this.grossweight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.grossweight.Name = "grossweight";
            this.grossweight.Size = new System.Drawing.Size(112, 22);
            this.grossweight.TabIndex = 5;
            this.grossweight.Text = "Gross Weight";
            // 
            // tarewghttxtbox
            // 
            this.tarewghttxtbox.Location = new System.Drawing.Point(130, 74);
            this.tarewghttxtbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tarewghttxtbox.Name = "tarewghttxtbox";
            this.tarewghttxtbox.ReadOnly = true;
            this.tarewghttxtbox.Size = new System.Drawing.Size(100, 28);
            this.tarewghttxtbox.TabIndex = 4;
            // 
            // tareweight
            // 
            this.tareweight.AutoSize = true;
            this.tareweight.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tareweight.Location = new System.Drawing.Point(16, 78);
            this.tareweight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tareweight.Name = "tareweight";
            this.tareweight.Size = new System.Drawing.Size(103, 22);
            this.tareweight.TabIndex = 3;
            this.tareweight.Text = "Tare Weight";
            // 
            // copstxtbox
            // 
            this.copstxtbox.Location = new System.Drawing.Point(129, 35);
            this.copstxtbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.copstxtbox.Name = "copstxtbox";
            this.copstxtbox.ReadOnly = true;
            this.copstxtbox.Size = new System.Drawing.Size(100, 28);
            this.copstxtbox.TabIndex = 2;
            // 
            // cops
            // 
            this.cops.AutoSize = true;
            this.cops.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cops.Location = new System.Drawing.Point(16, 40);
            this.cops.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cops.Name = "cops";
            this.cops.Size = new System.Drawing.Size(48, 22);
            this.cops.TabIndex = 1;
            this.cops.Text = "Cops";
            // 
            // lastboxldetailist
            // 
            this.lastboxldetailist.BackColor = System.Drawing.SystemColors.Control;
            this.lastboxldetailist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lastboxldetailist.HideSelection = false;
            this.lastboxldetailist.Location = new System.Drawing.Point(12, 31);
            this.lastboxldetailist.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lastboxldetailist.Name = "lastboxldetailist";
            this.lastboxldetailist.Size = new System.Drawing.Size(234, 165);
            this.lastboxldetailist.TabIndex = 0;
            this.lastboxldetailist.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grosswterror);
            this.groupBox1.Controls.Add(this.palletwterror);
            this.groupBox1.Controls.Add(this.spoolwterror);
            this.groupBox1.Controls.Add(this.spoolnoerror);
            this.groupBox1.Controls.Add(this.wtpercop);
            this.groupBox1.Controls.Add(this.spool);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.spoolno);
            this.groupBox1.Controls.Add(this.netwt);
            this.groupBox1.Controls.Add(this.spoolwt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.palletwt);
            this.groupBox1.Controls.Add(this.tarewt);
            this.groupBox1.Controls.Add(this.palletwtno);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.grosswtno);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(945, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(308, 379);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Weighing Details";
            // 
            // grosswterror
            // 
            this.grosswterror.AutoSize = true;
            this.grosswterror.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grosswterror.ForeColor = System.Drawing.Color.Red;
            this.grosswterror.Location = new System.Drawing.Point(16, 185);
            this.grosswterror.Name = "grosswterror";
            this.grosswterror.Size = new System.Drawing.Size(105, 22);
            this.grosswterror.TabIndex = 89;
            this.grosswterror.Visible = false;
            // 
            // palletwterror
            // 
            this.palletwterror.AutoSize = true;
            this.palletwterror.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.palletwterror.ForeColor = System.Drawing.Color.Red;
            this.palletwterror.Location = new System.Drawing.Point(12, 120);
            this.palletwterror.Name = "palletwterror";
            this.palletwterror.Size = new System.Drawing.Size(107, 22);
            this.palletwterror.TabIndex = 88;
            this.palletwterror.Visible = false;
            // 
            // spoolwterror
            // 
            this.spoolwterror.AutoSize = true;
            this.spoolwterror.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spoolwterror.ForeColor = System.Drawing.Color.Red;
            this.spoolwterror.Location = new System.Drawing.Point(181, 59);
            this.spoolwterror.Name = "spoolwterror";
            this.spoolwterror.Size = new System.Drawing.Size(106, 22);
            this.spoolwterror.TabIndex = 87;
            this.spoolwterror.Visible = false;
            // 
            // spoolnoerror
            // 
            this.spoolnoerror.AutoSize = true;
            this.spoolnoerror.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spoolnoerror.ForeColor = System.Drawing.Color.Red;
            this.spoolnoerror.Location = new System.Drawing.Point(13, 59);
            this.spoolnoerror.Name = "spoolnoerror";
            this.spoolnoerror.Size = new System.Drawing.Size(108, 22);
            this.spoolnoerror.TabIndex = 86;
            this.spoolnoerror.Visible = false;
            // 
            // wtpercop
            // 
            this.wtpercop.Location = new System.Drawing.Point(183, 330);
            this.wtpercop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wtpercop.Name = "wtpercop";
            this.wtpercop.ReadOnly = true;
            this.wtpercop.Size = new System.Drawing.Size(104, 28);
            this.wtpercop.TabIndex = 85;
            // 
            // spool
            // 
            this.spool.AutoSize = true;
            this.spool.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spool.Location = new System.Drawing.Point(14, 31);
            this.spool.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.spool.Name = "spool";
            this.spool.Size = new System.Drawing.Size(64, 22);
            this.spool.TabIndex = 0;
            this.spool.Text = "Spools:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 336);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 22);
            this.label5.TabIndex = 84;
            this.label5.Text = "Wt.Per Cop:";
            // 
            // spoolno
            // 
            this.spoolno.Location = new System.Drawing.Point(111, 26);
            this.spoolno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.spoolno.Name = "spoolno";
            this.spoolno.Size = new System.Drawing.Size(61, 28);
            this.spoolno.TabIndex = 1;
            this.spoolno.TextChanged += new System.EventHandler(this.SpoolNo_TextChanged);
            // 
            // netwt
            // 
            this.netwt.Location = new System.Drawing.Point(185, 277);
            this.netwt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.netwt.Name = "netwt";
            this.netwt.ReadOnly = true;
            this.netwt.Size = new System.Drawing.Size(104, 28);
            this.netwt.TabIndex = 83;
            this.netwt.TextChanged += new System.EventHandler(this.NetWeight_TextChanged);
            // 
            // spoolwt
            // 
            this.spoolwt.Location = new System.Drawing.Point(183, 26);
            this.spoolwt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.spoolwt.Name = "spoolwt";
            this.spoolwt.Size = new System.Drawing.Size(104, 28);
            this.spoolwt.TabIndex = 76;
            this.spoolwt.TextChanged += new System.EventHandler(this.SpoolWeight_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 280);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 22);
            this.label4.TabIndex = 82;
            this.label4.Text = "Net Wt:";
            // 
            // palletwt
            // 
            this.palletwt.AutoSize = true;
            this.palletwt.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.palletwt.Location = new System.Drawing.Point(10, 95);
            this.palletwt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.palletwt.Name = "palletwt";
            this.palletwt.Size = new System.Drawing.Size(171, 22);
            this.palletwt.TabIndex = 76;
            this.palletwt.Text = "Empty Box/Pallet Wt:";
            // 
            // tarewt
            // 
            this.tarewt.Location = new System.Drawing.Point(185, 218);
            this.tarewt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tarewt.Name = "tarewt";
            this.tarewt.ReadOnly = true;
            this.tarewt.Size = new System.Drawing.Size(104, 28);
            this.tarewt.TabIndex = 81;
            // 
            // palletwtno
            // 
            this.palletwtno.Location = new System.Drawing.Point(183, 91);
            this.palletwtno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.palletwtno.Name = "palletwtno";
            this.palletwtno.Size = new System.Drawing.Size(104, 28);
            this.palletwtno.TabIndex = 77;
            this.palletwtno.TextChanged += new System.EventHandler(this.PalletWeight_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 224);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 22);
            this.label3.TabIndex = 80;
            this.label3.Text = "Tare Wt:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 159);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 22);
            this.label2.TabIndex = 78;
            this.label2.Text = "Gross Wt:";
            // 
            // grosswtno
            // 
            this.grosswtno.Location = new System.Drawing.Point(185, 156);
            this.grosswtno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grosswtno.Name = "grosswtno";
            this.grosswtno.Size = new System.Drawing.Size(104, 28);
            this.grosswtno.TabIndex = 79;
            this.grosswtno.TextChanged += new System.EventHandler(this.GrossWeight_TextChanged);
            // 
            // wgroupbox
            // 
            this.wgroupbox.Controls.Add(this.windinggrid);
            this.wgroupbox.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wgroupbox.Location = new System.Drawing.Point(675, 666);
            this.wgroupbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wgroupbox.Name = "wgroupbox";
            this.wgroupbox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wgroupbox.Size = new System.Drawing.Size(588, 248);
            this.wgroupbox.TabIndex = 90;
            this.wgroupbox.TabStop = false;
            this.wgroupbox.Text = "Winding Type + Gradewise Production Status";
            // 
            // windinggrid
            // 
            this.windinggrid.AutoGenerateColumns = false;
            this.windinggrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.windinggrid.ColumnHeadersHeight = 34;
            this.windinggrid.DataSource = this.windingqty;
            this.windinggrid.Location = new System.Drawing.Point(12, 31);
            this.windinggrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windinggrid.Name = "windinggrid";
            this.windinggrid.RowHeadersVisible = false;
            this.windinggrid.RowHeadersWidth = 62;
            this.windinggrid.Size = new System.Drawing.Size(567, 208);
            this.windinggrid.TabIndex = 92;
            this.windinggrid.Paint += new System.Windows.Forms.PaintEventHandler(this.windinggrid_Paint);
            // 
            // gradewiseprodn
            // 
            this.gradewiseprodn.Controls.Add(this.totalprodbalqty);
            this.gradewiseprodn.Controls.Add(this.qualityqty);
            this.gradewiseprodn.Controls.Add(this.saleordrqty);
            this.gradewiseprodn.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradewiseprodn.ForeColor = System.Drawing.Color.Black;
            this.gradewiseprodn.Location = new System.Drawing.Point(955, 394);
            this.gradewiseprodn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gradewiseprodn.Name = "gradewiseprodn";
            this.gradewiseprodn.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gradewiseprodn.Size = new System.Drawing.Size(308, 252);
            this.gradewiseprodn.TabIndex = 93;
            this.gradewiseprodn.TabStop = false;
            this.gradewiseprodn.Text = "Gradewise Prodn Status";
            // 
            // totalprodbalqty
            // 
            this.totalprodbalqty.AutoSize = true;
            this.totalprodbalqty.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalprodbalqty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.totalprodbalqty.Location = new System.Drawing.Point(15, 226);
            this.totalprodbalqty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.totalprodbalqty.Name = "totalprodbalqty";
            this.totalprodbalqty.Size = new System.Drawing.Size(158, 22);
            this.totalprodbalqty.TabIndex = 93;
            this.totalprodbalqty.Text = "Production Bal Qty:";
            // 
            // qualityqty
            // 
            this.qualityqty.AutoGenerateColumns = false;
            this.qualityqty.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.qualityqty.BackgroundColor = System.Drawing.SystemColors.Control;
            this.qualityqty.ColumnHeadersHeight = 34;
            this.qualityqty.DataSource = this.qualityandqty;
            this.qualityqty.GridColor = System.Drawing.SystemColors.Control;
            this.qualityqty.Location = new System.Drawing.Point(18, 52);
            this.qualityqty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.qualityqty.Name = "qualityqty";
            this.qualityqty.RowHeadersVisible = false;
            this.qualityqty.RowHeadersWidth = 62;
            this.qualityqty.Size = new System.Drawing.Size(272, 169);
            this.qualityqty.TabIndex = 92;
            this.qualityqty.Paint += new System.Windows.Forms.PaintEventHandler(this.qualityqty_Paint);
            // 
            // saleordrqty
            // 
            this.saleordrqty.AutoSize = true;
            this.saleordrqty.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saleordrqty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saleordrqty.Location = new System.Drawing.Point(15, 26);
            this.saleordrqty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.saleordrqty.Name = "saleordrqty";
            this.saleordrqty.Size = new System.Drawing.Size(131, 22);
            this.saleordrqty.TabIndex = 92;
            this.saleordrqty.Text = "Sale Order Qty :";
            // 
            // addqty
            // 
            this.addqty.BackColor = System.Drawing.SystemColors.Highlight;
            this.addqty.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.addqty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addqty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addqty.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addqty.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.addqty.Location = new System.Drawing.Point(527, 649);
            this.addqty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addqty.Name = "addqty";
            this.addqty.Size = new System.Drawing.Size(100, 38);
            this.addqty.TabIndex = 83;
            this.addqty.Text = "Add";
            this.addqty.UseVisualStyleBackColor = false;
            this.addqty.Click += new System.EventHandler(this.addqty_Click);
            // 
            // qnty
            // 
            this.qnty.Location = new System.Drawing.Point(421, 654);
            this.qnty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.qnty.Name = "qnty";
            this.qnty.Size = new System.Drawing.Size(91, 26);
            this.qnty.TabIndex = 82;
            // 
            // pquantity
            // 
            this.pquantity.AutoSize = true;
            this.pquantity.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pquantity.Location = new System.Drawing.Point(340, 657);
            this.pquantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pquantity.Name = "pquantity";
            this.pquantity.Size = new System.Drawing.Size(41, 22);
            this.pquantity.TabIndex = 81;
            this.pquantity.Text = "Qty:";
            this.pquantity.UseMnemonic = false;
            // 
            // PalletTypeList
            // 
            this.PalletTypeList.AllowDrop = true;
            this.PalletTypeList.FormattingEnabled = true;
            this.PalletTypeList.Location = new System.Drawing.Point(97, 654);
            this.PalletTypeList.Name = "PalletTypeList";
            this.PalletTypeList.Size = new System.Drawing.Size(208, 28);
            this.PalletTypeList.TabIndex = 80;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 648);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 44);
            this.label6.TabIndex = 79;
            this.label6.Text = "Pallet\nType:";
            this.label6.UseMnemonic = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.prowner);
            this.panel3.Controls.Add(this.prwtps);
            this.panel3.Controls.Add(this.prcompany);
            this.panel3.Controls.Add(this.prqrcode);
            this.panel3.Controls.Add(this.pruser);
            this.panel3.Controls.Add(this.prdate);
            this.panel3.Location = new System.Drawing.Point(664, 12);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(254, 251);
            this.panel3.TabIndex = 87;
            // 
            // prowner
            // 
            this.prowner.AutoSize = true;
            this.prowner.Location = new System.Drawing.Point(12, 46);
            this.prowner.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.prowner.Name = "prowner";
            this.prowner.Size = new System.Drawing.Size(117, 24);
            this.prowner.TabIndex = 67;
            this.prowner.Text = "Print Owner";
            this.prowner.UseVisualStyleBackColor = true;
            // 
            // prwtps
            // 
            this.prwtps.AutoSize = true;
            this.prwtps.Location = new System.Drawing.Point(10, 165);
            this.prwtps.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.prwtps.Name = "prwtps";
            this.prwtps.Size = new System.Drawing.Size(120, 24);
            this.prwtps.TabIndex = 71;
            this.prwtps.Text = "Print WT/PS";
            this.prwtps.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prwtps.UseVisualStyleBackColor = true;
            // 
            // prcompany
            // 
            this.prcompany.AutoSize = true;
            this.prcompany.Location = new System.Drawing.Point(12, 9);
            this.prcompany.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.prcompany.Name = "prcompany";
            this.prcompany.Size = new System.Drawing.Size(138, 24);
            this.prcompany.TabIndex = 66;
            this.prcompany.Text = "Print Company";
            this.prcompany.UseVisualStyleBackColor = true;
            // 
            // prqrcode
            // 
            this.prqrcode.AutoSize = true;
            this.prqrcode.Location = new System.Drawing.Point(10, 203);
            this.prqrcode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.prqrcode.Name = "prqrcode";
            this.prqrcode.Size = new System.Drawing.Size(137, 24);
            this.prqrcode.TabIndex = 72;
            this.prqrcode.Text = "Print QR Code";
            this.prqrcode.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prqrcode.UseVisualStyleBackColor = true;
            // 
            // pruser
            // 
            this.pruser.AutoSize = true;
            this.pruser.Location = new System.Drawing.Point(12, 123);
            this.pruser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pruser.Name = "pruser";
            this.pruser.Size = new System.Drawing.Size(105, 24);
            this.pruser.TabIndex = 69;
            this.pruser.Text = "Print User";
            this.pruser.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pruser.UseVisualStyleBackColor = true;
            // 
            // prdate
            // 
            this.prdate.AutoSize = true;
            this.prdate.Location = new System.Drawing.Point(12, 85);
            this.prdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.prdate.Name = "prdate";
            this.prdate.Size = new System.Drawing.Size(106, 24);
            this.prdate.TabIndex = 68;
            this.prdate.Text = "Print Date";
            this.prdate.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prdate.UseVisualStyleBackColor = true;
            // 
            // PrefixList
            // 
            this.PrefixList.AllowDrop = true;
            this.PrefixList.FormattingEnabled = true;
            this.PrefixList.Location = new System.Drawing.Point(428, 74);
            this.PrefixList.Name = "PrefixList";
            this.PrefixList.Size = new System.Drawing.Size(199, 28);
            this.PrefixList.TabIndex = 86;
            this.PrefixList.SelectedIndexChanged += new System.EventHandler(this.PrefixList_SelectedIndexChanged);
            // 
            // lineno
            // 
            this.lineno.AutoSize = true;
            this.lineno.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineno.Location = new System.Drawing.Point(9, 17);
            this.lineno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lineno.Name = "lineno";
            this.lineno.Size = new System.Drawing.Size(45, 22);
            this.lineno.TabIndex = 0;
            this.lineno.Text = "Line:";
            // 
            // LineNoList
            // 
            this.LineNoList.AllowDrop = true;
            this.LineNoList.FormattingEnabled = true;
            this.LineNoList.Location = new System.Drawing.Point(104, 12);
            this.LineNoList.Name = "LineNoList";
            this.LineNoList.Size = new System.Drawing.Size(205, 28);
            this.LineNoList.TabIndex = 50;
            this.LineNoList.SelectedIndexChanged += new System.EventHandler(this.LineNoList_SelectedIndexChanged);
            // 
            // lastboxno
            // 
            this.lastboxno.AutoSize = true;
            this.lastboxno.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastboxno.Location = new System.Drawing.Point(338, 17);
            this.lastboxno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lastboxno.Name = "lastboxno";
            this.lastboxno.Size = new System.Drawing.Size(77, 22);
            this.lastboxno.TabIndex = 6;
            this.lastboxno.Text = "Last Box:";
            // 
            // lastbox
            // 
            this.lastbox.Location = new System.Drawing.Point(428, 12);
            this.lastbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lastbox.Name = "lastbox";
            this.lastbox.Size = new System.Drawing.Size(199, 26);
            this.lastbox.TabIndex = 7;
            // 
            // submit
            // 
            this.submit.BackColor = System.Drawing.SystemColors.Highlight;
            this.submit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submit.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.submit.Location = new System.Drawing.Point(581, 935);
            this.submit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(112, 43);
            this.submit.TabIndex = 85;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = false;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // department
            // 
            this.department.AutoSize = true;
            this.department.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.department.Location = new System.Drawing.Point(4, 76);
            this.department.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.department.Name = "department";
            this.department.Size = new System.Drawing.Size(51, 22);
            this.department.TabIndex = 2;
            this.department.Text = "Dept:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 697);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(616, 217);
            this.flowLayoutPanel1.TabIndex = 84;
            // 
            // departmentname
            // 
            this.departmentname.Location = new System.Drawing.Point(99, 74);
            this.departmentname.Name = "departmentname";
            this.departmentname.ReadOnly = true;
            this.departmentname.Size = new System.Drawing.Size(206, 26);
            this.departmentname.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(671, 345);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 22);
            this.label1.TabIndex = 73;
            this.label1.Text = "No. Of Copies:";
            // 
            // copyno
            // 
            this.copyno.Location = new System.Drawing.Point(818, 341);
            this.copyno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.copyno.Name = "copyno";
            this.copyno.Size = new System.Drawing.Size(92, 26);
            this.copyno.TabIndex = 74;
            this.copyno.TextChanged += new System.EventHandler(this.CopyNos_TextChanged);
            // 
            // boxno
            // 
            this.boxno.AutoSize = true;
            this.boxno.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxno.Location = new System.Drawing.Point(340, 77);
            this.boxno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.boxno.Name = "boxno";
            this.boxno.Size = new System.Drawing.Size(74, 22);
            this.boxno.TabIndex = 14;
            this.boxno.Text = "Box No.:";
            // 
            // mergeno
            // 
            this.mergeno.AutoSize = true;
            this.mergeno.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mergeno.Location = new System.Drawing.Point(8, 145);
            this.mergeno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mergeno.Name = "mergeno";
            this.mergeno.Size = new System.Drawing.Size(91, 22);
            this.mergeno.TabIndex = 4;
            this.mergeno.Text = "Merge No:";
            // 
            // MergeNoList
            // 
            this.MergeNoList.AllowDrop = true;
            this.MergeNoList.FormattingEnabled = true;
            this.MergeNoList.Location = new System.Drawing.Point(99, 147);
            this.MergeNoList.Name = "MergeNoList";
            this.MergeNoList.Size = new System.Drawing.Size(206, 28);
            this.MergeNoList.TabIndex = 53;
            this.MergeNoList.SelectedIndexChanged += new System.EventHandler(this.MergeNoList_SelectedIndexChanged);
            // 
            // packingdate
            // 
            this.packingdate.AutoSize = true;
            this.packingdate.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.packingdate.Location = new System.Drawing.Point(342, 145);
            this.packingdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.packingdate.Name = "packingdate";
            this.packingdate.Size = new System.Drawing.Size(50, 22);
            this.packingdate.TabIndex = 16;
            this.packingdate.Text = "Date:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(429, 145);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(199, 26);
            this.dateTimePicker1.TabIndex = 17;
            // 
            // prodtype
            // 
            this.prodtype.Location = new System.Drawing.Point(99, 602);
            this.prodtype.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.prodtype.Name = "prodtype";
            this.prodtype.ReadOnly = true;
            this.prodtype.Size = new System.Drawing.Size(208, 26);
            this.prodtype.TabIndex = 76;
            // 
            // item
            // 
            this.item.AutoSize = true;
            this.item.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.item.Location = new System.Drawing.Point(9, 214);
            this.item.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.item.Name = "item";
            this.item.Size = new System.Drawing.Size(49, 22);
            this.item.TabIndex = 8;
            this.item.Text = "Item:";
            // 
            // itemname
            // 
            this.itemname.Location = new System.Drawing.Point(97, 205);
            this.itemname.Name = "itemname";
            this.itemname.ReadOnly = true;
            this.itemname.Size = new System.Drawing.Size(206, 26);
            this.itemname.TabIndex = 54;
            // 
            // shade
            // 
            this.shade.AutoSize = true;
            this.shade.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shade.Location = new System.Drawing.Point(344, 205);
            this.shade.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.shade.Name = "shade";
            this.shade.Size = new System.Drawing.Size(61, 22);
            this.shade.TabIndex = 10;
            this.shade.Text = "Shade:";
            // 
            // shadename
            // 
            this.shadename.Location = new System.Drawing.Point(423, 205);
            this.shadename.Name = "shadename";
            this.shadename.ReadOnly = true;
            this.shadename.Size = new System.Drawing.Size(199, 26);
            this.shadename.TabIndex = 55;
            // 
            // shadecd
            // 
            this.shadecd.Location = new System.Drawing.Point(97, 258);
            this.shadecd.Name = "shadecd";
            this.shadecd.ReadOnly = true;
            this.shadecd.Size = new System.Drawing.Size(206, 26);
            this.shadecd.TabIndex = 56;
            // 
            // shadecode
            // 
            this.shadecode.AutoSize = true;
            this.shadecode.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shadecode.Location = new System.Drawing.Point(8, 249);
            this.shadecode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.shadecode.Name = "shadecode";
            this.shadecode.Size = new System.Drawing.Size(57, 44);
            this.shadecode.TabIndex = 12;
            this.shadecode.Text = "Shade\nCode:";
            // 
            // remarks
            // 
            this.remarks.Location = new System.Drawing.Point(429, 605);
            this.remarks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.remarks.Name = "remarks";
            this.remarks.Size = new System.Drawing.Size(200, 26);
            this.remarks.TabIndex = 47;
            // 
            // remark
            // 
            this.remark.AutoSize = true;
            this.remark.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remark.Location = new System.Drawing.Point(336, 605);
            this.remark.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.remark.Name = "remark";
            this.remark.Size = new System.Drawing.Size(78, 22);
            this.remark.TabIndex = 46;
            this.remark.Text = "Remarks:";
            // 
            // QualityList
            // 
            this.QualityList.AllowDrop = true;
            this.QualityList.FormattingEnabled = true;
            this.QualityList.Location = new System.Drawing.Point(97, 311);
            this.QualityList.Name = "QualityList";
            this.QualityList.Size = new System.Drawing.Size(206, 28);
            this.QualityList.TabIndex = 57;
            this.QualityList.SelectedIndexChanged += new System.EventHandler(this.QualityList_SelectedIndexChanged);
            // 
            // BoxItemList
            // 
            this.BoxItemList.AllowDrop = true;
            this.BoxItemList.FormattingEnabled = true;
            this.BoxItemList.Location = new System.Drawing.Point(100, 499);
            this.BoxItemList.Name = "BoxItemList";
            this.BoxItemList.Size = new System.Drawing.Size(187, 28);
            this.BoxItemList.TabIndex = 63;
            this.BoxItemList.SelectedIndexChanged += new System.EventHandler(this.BoxItemList_SelectedIndexChanged);
            // 
            // productiontype
            // 
            this.productiontype.AutoSize = true;
            this.productiontype.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productiontype.Location = new System.Drawing.Point(8, 602);
            this.productiontype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.productiontype.Name = "productiontype";
            this.productiontype.Size = new System.Drawing.Size(91, 22);
            this.productiontype.TabIndex = 44;
            this.productiontype.Text = "Prod Type:";
            // 
            // quality
            // 
            this.quality.AutoSize = true;
            this.quality.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quality.Location = new System.Drawing.Point(4, 309);
            this.quality.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.quality.Name = "quality";
            this.quality.Size = new System.Drawing.Size(68, 22);
            this.quality.TabIndex = 18;
            this.quality.Text = "Quality:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(528, 509);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 26);
            this.textBox4.TabIndex = 43;
            // 
            // boxstock
            // 
            this.boxstock.AutoSize = true;
            this.boxstock.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxstock.Location = new System.Drawing.Point(425, 508);
            this.boxstock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.boxstock.Name = "boxstock";
            this.boxstock.Size = new System.Drawing.Size(87, 44);
            this.boxstock.TabIndex = 42;
            this.boxstock.Text = "Box/Pallet\nStock:";
            // 
            // saleorderno
            // 
            this.saleorderno.AutoSize = true;
            this.saleorderno.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saleorderno.Location = new System.Drawing.Point(344, 314);
            this.saleorderno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.saleorderno.Name = "saleorderno";
            this.saleorderno.Size = new System.Drawing.Size(68, 22);
            this.saleorderno.TabIndex = 20;
            this.saleorderno.Text = "SO No.:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(344, 505);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(61, 26);
            this.textBox3.TabIndex = 41;
            // 
            // SaleOrderList
            // 
            this.SaleOrderList.AllowDrop = true;
            this.SaleOrderList.FormattingEnabled = true;
            this.SaleOrderList.Location = new System.Drawing.Point(427, 311);
            this.SaleOrderList.Name = "SaleOrderList";
            this.SaleOrderList.Size = new System.Drawing.Size(200, 28);
            this.SaleOrderList.TabIndex = 64;
            this.SaleOrderList.SelectedIndexChanged += new System.EventHandler(this.SaleOrderList_SelectedIndexChanged);
            // 
            // boxweight
            // 
            this.boxweight.AutoSize = true;
            this.boxweight.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxweight.Location = new System.Drawing.Point(300, 505);
            this.boxweight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.boxweight.Name = "boxweight";
            this.boxweight.Size = new System.Drawing.Size(36, 22);
            this.boxweight.TabIndex = 40;
            this.boxweight.Text = "Wt:";
            // 
            // packsize
            // 
            this.packsize.AutoSize = true;
            this.packsize.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.packsize.Location = new System.Drawing.Point(4, 378);
            this.packsize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.packsize.Name = "packsize";
            this.packsize.Size = new System.Drawing.Size(84, 22);
            this.packsize.TabIndex = 22;
            this.packsize.Text = "Pack Size:";
            // 
            // PackSizeList
            // 
            this.PackSizeList.AllowDrop = true;
            this.PackSizeList.FormattingEnabled = true;
            this.PackSizeList.Location = new System.Drawing.Point(100, 376);
            this.PackSizeList.Name = "PackSizeList";
            this.PackSizeList.Size = new System.Drawing.Size(187, 28);
            this.PackSizeList.TabIndex = 58;
            this.PackSizeList.SelectedIndexChanged += new System.EventHandler(this.PackSizeList_SelectedIndexChanged);
            // 
            // CopsItemList
            // 
            this.CopsItemList.AllowDrop = true;
            this.CopsItemList.FormattingEnabled = true;
            this.CopsItemList.Location = new System.Drawing.Point(101, 443);
            this.CopsItemList.Name = "CopsItemList";
            this.CopsItemList.Size = new System.Drawing.Size(188, 28);
            this.CopsItemList.TabIndex = 62;
            this.CopsItemList.SelectedIndexChanged += new System.EventHandler(this.CopsItemList_SelectedIndexChanged);
            // 
            // boxtype
            // 
            this.boxtype.AutoSize = true;
            this.boxtype.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxtype.Location = new System.Drawing.Point(4, 493);
            this.boxtype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.boxtype.Name = "boxtype";
            this.boxtype.Size = new System.Drawing.Size(87, 44);
            this.boxtype.TabIndex = 38;
            this.boxtype.Text = "Box/Pallet\nType:";
            // 
            // frdenier
            // 
            this.frdenier.Location = new System.Drawing.Point(303, 378);
            this.frdenier.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.frdenier.Name = "frdenier";
            this.frdenier.ReadOnly = true;
            this.frdenier.Size = new System.Drawing.Size(52, 26);
            this.frdenier.TabIndex = 25;
            // 
            // WeighingList
            // 
            this.WeighingList.AllowDrop = true;
            this.WeighingList.FormattingEnabled = true;
            this.WeighingList.Location = new System.Drawing.Point(103, 550);
            this.WeighingList.Name = "WeighingList";
            this.WeighingList.Size = new System.Drawing.Size(206, 28);
            this.WeighingList.TabIndex = 61;
            this.WeighingList.SelectedIndexChanged += new System.EventHandler(this.WeighingList_SelectedIndexChanged);
            // 
            // updenier
            // 
            this.updenier.Location = new System.Drawing.Point(362, 378);
            this.updenier.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.updenier.Name = "updenier";
            this.updenier.ReadOnly = true;
            this.updenier.Size = new System.Drawing.Size(52, 26);
            this.updenier.TabIndex = 27;
            // 
            // scalemodel
            // 
            this.scalemodel.AutoSize = true;
            this.scalemodel.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scalemodel.Location = new System.Drawing.Point(5, 550);
            this.scalemodel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scalemodel.Name = "scalemodel";
            this.scalemodel.Size = new System.Drawing.Size(59, 44);
            this.scalemodel.TabIndex = 48;
            this.scalemodel.Text = "Weigh\nScale:";
            // 
            // ComPortList
            // 
            this.ComPortList.AllowDrop = true;
            this.ComPortList.FormattingEnabled = true;
            this.ComPortList.Location = new System.Drawing.Point(427, 555);
            this.ComPortList.Name = "ComPortList";
            this.ComPortList.Size = new System.Drawing.Size(199, 28);
            this.ComPortList.TabIndex = 60;
            this.ComPortList.SelectedIndexChanged += new System.EventHandler(this.ComPortList_SelectedIndexChanged);
            // 
            // windingtype
            // 
            this.windingtype.AutoSize = true;
            this.windingtype.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windingtype.Location = new System.Drawing.Point(427, 379);
            this.windingtype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.windingtype.Name = "windingtype";
            this.windingtype.Size = new System.Drawing.Size(78, 22);
            this.windingtype.TabIndex = 28;
            this.windingtype.Text = "Winding:";
            // 
            // WindingTypeList
            // 
            this.WindingTypeList.AllowDrop = true;
            this.WindingTypeList.FormattingEnabled = true;
            this.WindingTypeList.Location = new System.Drawing.Point(526, 376);
            this.WindingTypeList.Name = "WindingTypeList";
            this.WindingTypeList.Size = new System.Drawing.Size(100, 28);
            this.WindingTypeList.TabIndex = 59;
            this.WindingTypeList.SelectedIndexChanged += new System.EventHandler(this.WindingTypeList_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(528, 452);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 26);
            this.textBox2.TabIndex = 37;
            // 
            // comport
            // 
            this.comport.AutoSize = true;
            this.comport.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comport.Location = new System.Drawing.Point(338, 558);
            this.comport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.comport.Name = "comport";
            this.comport.Size = new System.Drawing.Size(87, 22);
            this.comport.TabIndex = 30;
            this.comport.Text = "Com Port:";
            this.comport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // copstock
            // 
            this.copstock.AutoSize = true;
            this.copstock.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copstock.Location = new System.Drawing.Point(427, 446);
            this.copstock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.copstock.Name = "copstock";
            this.copstock.Size = new System.Drawing.Size(55, 44);
            this.copstock.TabIndex = 35;
            this.copstock.Text = "Cops\nStock:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(349, 449);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(61, 26);
            this.textBox1.TabIndex = 36;
            // 
            // copssize
            // 
            this.copssize.AutoSize = true;
            this.copssize.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copssize.Location = new System.Drawing.Point(8, 446);
            this.copssize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.copssize.Name = "copssize";
            this.copssize.Size = new System.Drawing.Size(87, 22);
            this.copssize.TabIndex = 32;
            this.copssize.Text = "Cops Size:";
            // 
            // copweight
            // 
            this.copweight.AutoSize = true;
            this.copweight.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copweight.Location = new System.Drawing.Point(305, 452);
            this.copweight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.copweight.Name = "copweight";
            this.copweight.Size = new System.Drawing.Size(36, 22);
            this.copweight.TabIndex = 34;
            this.copweight.Text = "Wt:";
            // 
            // bcfformlabel
            // 
            this.bcfformlabel.AutoSize = true;
            this.bcfformlabel.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bcfformlabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bcfformlabel.Location = new System.Drawing.Point(24, 17);
            this.bcfformlabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.bcfformlabel.Name = "bcfformlabel";
            this.bcfformlabel.Size = new System.Drawing.Size(114, 23);
            this.bcfformlabel.TabIndex = 94;
            this.bcfformlabel.Text = "BCF Packing";
            // 
            // windingtypeDataGridViewTextBoxColumn
            // 
            this.windingtypeDataGridViewTextBoxColumn.DataPropertyName = "Winding Type";
            this.windingtypeDataGridViewTextBoxColumn.FillWeight = 25F;
            this.windingtypeDataGridViewTextBoxColumn.HeaderText = "Winding Type";
            this.windingtypeDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.windingtypeDataGridViewTextBoxColumn.Name = "windingtypeDataGridViewTextBoxColumn";
            this.windingtypeDataGridViewTextBoxColumn.Width = 150;
            // 
            // soqtyDataGridViewTextBoxColumn
            // 
            this.soqtyDataGridViewTextBoxColumn.DataPropertyName = "Sale Order Qty";
            this.soqtyDataGridViewTextBoxColumn.FillWeight = 25F;
            this.soqtyDataGridViewTextBoxColumn.HeaderText = "Sale Order Qty";
            this.soqtyDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.soqtyDataGridViewTextBoxColumn.Name = "soqtyDataGridViewTextBoxColumn";
            this.soqtyDataGridViewTextBoxColumn.Width = 150;
            // 
            // windingbalqtyDataGridViewTextBoxColumn
            // 
            this.windingbalqtyDataGridViewTextBoxColumn.DataPropertyName = "Balance Qty";
            this.windingbalqtyDataGridViewTextBoxColumn.FillWeight = 25F;
            this.windingbalqtyDataGridViewTextBoxColumn.HeaderText = "Balance Qty";
            this.windingbalqtyDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.windingbalqtyDataGridViewTextBoxColumn.Name = "windingbalqtyDataGridViewTextBoxColumn";
            this.windingbalqtyDataGridViewTextBoxColumn.Width = 150;
            // 
            // windingprodqtyDataGridViewTextBoxColumn
            // 
            this.windingprodqtyDataGridViewTextBoxColumn.DataPropertyName = "WProduction Qty";
            this.windingprodqtyDataGridViewTextBoxColumn.FillWeight = 25F;
            this.windingprodqtyDataGridViewTextBoxColumn.HeaderText = "Production Qty";
            this.windingprodqtyDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.windingprodqtyDataGridViewTextBoxColumn.Name = "windingprodqtyDataGridViewTextBoxColumn";
            this.windingprodqtyDataGridViewTextBoxColumn.Width = 150;
            // 
            // BCFPackingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1308, 1050);
            this.Controls.Add(this.bcfformlabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.backbutton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BCFPackingForm";
            this.Text = "BCFPackingForm";
            this.Load += new System.EventHandler(this.BCFPackingForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.lastboxdetails.ResumeLayout(false);
            this.lastboxdetails.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.wgroupbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.windinggrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.windingqty)).EndInit();
            this.gradewiseprodn.ResumeLayout(false);
            this.gradewiseprodn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualityqty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityandqty)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backbutton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox deniervalue;
        private System.Windows.Forms.Label denier;
        private System.Windows.Forms.GroupBox lastboxdetails;
        private System.Windows.Forms.TextBox netwttxtbox;
        private System.Windows.Forms.Label netweight;
        private System.Windows.Forms.TextBox grosswttxtbox;
        private System.Windows.Forms.Label grossweight;
        private System.Windows.Forms.TextBox tarewghttxtbox;
        private System.Windows.Forms.Label tareweight;
        private System.Windows.Forms.TextBox copstxtbox;
        private System.Windows.Forms.Label cops;
        private System.Windows.Forms.ListView lastboxldetailist;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox wtpercop;
        private System.Windows.Forms.Label spool;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox spoolno;
        private System.Windows.Forms.TextBox netwt;
        private System.Windows.Forms.TextBox spoolwt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label palletwt;
        private System.Windows.Forms.TextBox tarewt;
        private System.Windows.Forms.TextBox palletwtno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox grosswtno;
        private System.Windows.Forms.GroupBox wgroupbox;
        private System.Windows.Forms.DataGridView windinggrid;
        private System.Windows.Forms.GroupBox gradewiseprodn;
        private System.Windows.Forms.Label totalprodbalqty;
        private System.Windows.Forms.DataGridView qualityqty;
        private System.Windows.Forms.Label saleordrqty;
        private System.Windows.Forms.Button addqty;
        private System.Windows.Forms.TextBox qnty;
        private System.Windows.Forms.Label pquantity;
        private System.Windows.Forms.ComboBox PalletTypeList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox prowner;
        private System.Windows.Forms.CheckBox prwtps;
        private System.Windows.Forms.CheckBox prcompany;
        private System.Windows.Forms.CheckBox prqrcode;
        private System.Windows.Forms.CheckBox pruser;
        private System.Windows.Forms.CheckBox prdate;
        private System.Windows.Forms.ComboBox PrefixList;
        private System.Windows.Forms.Label lineno;
        private System.Windows.Forms.ComboBox LineNoList;
        private System.Windows.Forms.Label lastboxno;
        private System.Windows.Forms.TextBox lastbox;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Label department;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox departmentname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox copyno;
        private System.Windows.Forms.Label boxno;
        private System.Windows.Forms.Label mergeno;
        private System.Windows.Forms.ComboBox MergeNoList;
        private System.Windows.Forms.Label packingdate;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox prodtype;
        private System.Windows.Forms.Label item;
        private System.Windows.Forms.TextBox itemname;
        private System.Windows.Forms.Label shade;
        private System.Windows.Forms.TextBox shadename;
        private System.Windows.Forms.TextBox shadecd;
        private System.Windows.Forms.Label shadecode;
        private System.Windows.Forms.TextBox remarks;
        private System.Windows.Forms.Label remark;
        private System.Windows.Forms.ComboBox QualityList;
        private System.Windows.Forms.ComboBox BoxItemList;
        private System.Windows.Forms.Label productiontype;
        private System.Windows.Forms.Label quality;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label boxstock;
        private System.Windows.Forms.Label saleorderno;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox SaleOrderList;
        private System.Windows.Forms.Label boxweight;
        private System.Windows.Forms.Label packsize;
        private System.Windows.Forms.ComboBox PackSizeList;
        private System.Windows.Forms.ComboBox CopsItemList;
        private System.Windows.Forms.Label boxtype;
        private System.Windows.Forms.TextBox frdenier;
        private System.Windows.Forms.ComboBox WeighingList;
        private System.Windows.Forms.TextBox updenier;
        private System.Windows.Forms.Label scalemodel;
        private System.Windows.Forms.ComboBox ComPortList;
        private System.Windows.Forms.Label windingtype;
        private System.Windows.Forms.ComboBox WindingTypeList;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label comport;
        private System.Windows.Forms.Label copstock;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label copssize;
        private System.Windows.Forms.Label copweight;
        private System.Windows.Forms.Label bcfformlabel;
        private DataTable windingqty;
        private DataTable qualityandqty;
        private DataGridViewTextBoxColumn windingtypeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn soqtyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn windingprodqtyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn windingbalqtyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn windingTypeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn saleOrderQtyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn balanceQtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label linenoerror;
        private System.Windows.Forms.Label copynoerror;
        private System.Windows.Forms.Label mergenoerror;
        private System.Windows.Forms.Label qualityerror;
        private System.Windows.Forms.Label soerror;
        private System.Windows.Forms.Label packsizeerror;
        private System.Windows.Forms.Label windingerror;
        private System.Windows.Forms.Label spoolnoerror;
        private System.Windows.Forms.Label spoolwterror;
        private System.Windows.Forms.Label palletwterror;
        private System.Windows.Forms.Label grosswterror;
        private System.Windows.Forms.Label boxnoerror;
    }
}