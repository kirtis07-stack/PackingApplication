using PackingApplication.Helper;
using System.Data;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace PackingApplication
{
    partial class POYPackingForm
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
            this.lineno = new System.Windows.Forms.Label();
            this.department = new System.Windows.Forms.Label();
            this.mergeno = new System.Windows.Forms.Label();
            this.lastboxno = new System.Windows.Forms.Label();
            this.lastbox = new System.Windows.Forms.TextBox();
            this.item = new System.Windows.Forms.Label();
            this.shade = new System.Windows.Forms.Label();
            this.shadecode = new System.Windows.Forms.Label();
            this.boxno = new System.Windows.Forms.Label();
            this.packingdate = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.quality = new System.Windows.Forms.Label();
            this.saleorderno = new System.Windows.Forms.Label();
            this.packsize = new System.Windows.Forms.Label();
            this.frdenier = new System.Windows.Forms.TextBox();
            this.updenier = new System.Windows.Forms.TextBox();
            this.windingtype = new System.Windows.Forms.Label();
            this.comport = new System.Windows.Forms.Label();
            this.copssize = new System.Windows.Forms.Label();
            this.copweight = new System.Windows.Forms.Label();
            this.copstock = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.boxtype = new System.Windows.Forms.Label();
            this.boxweight = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.boxstock = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.productiontype = new System.Windows.Forms.Label();
            this.remark = new System.Windows.Forms.Label();
            this.remarks = new System.Windows.Forms.TextBox();
            this.scalemodel = new System.Windows.Forms.Label();
            this.LineNoList = new System.Windows.Forms.ComboBox();
            this.departmentname = new System.Windows.Forms.TextBox();
            this.MergeNoList = new System.Windows.Forms.ComboBox();
            this.itemname = new System.Windows.Forms.TextBox();
            this.shadename = new System.Windows.Forms.TextBox();
            this.shadecd = new System.Windows.Forms.TextBox();
            this.QualityList = new System.Windows.Forms.ComboBox();
            this.PackSizeList = new System.Windows.Forms.ComboBox();
            this.WindingTypeList = new System.Windows.Forms.ComboBox();
            this.ComPortList = new System.Windows.Forms.ComboBox();
            this.WeighingList = new System.Windows.Forms.ComboBox();
            this.CopsItemList = new System.Windows.Forms.ComboBox();
            this.BoxItemList = new System.Windows.Forms.ComboBox();
            this.SaleOrderList = new System.Windows.Forms.ComboBox();
            this.prcompany = new System.Windows.Forms.CheckBox();
            this.prowner = new System.Windows.Forms.CheckBox();
            this.prdate = new System.Windows.Forms.CheckBox();
            this.pruser = new System.Windows.Forms.CheckBox();
            this.prhindi = new System.Windows.Forms.CheckBox();
            this.prwtps = new System.Windows.Forms.CheckBox();
            this.prqrcode = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.copyno = new System.Windows.Forms.TextBox();
            this.wtpercop = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.netwt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tarewt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grosswtno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.palletwtno = new System.Windows.Forms.TextBox();
            this.palletwt = new System.Windows.Forms.Label();
            this.spoolwt = new System.Windows.Forms.TextBox();
            this.spoolno = new System.Windows.Forms.TextBox();
            this.spool = new System.Windows.Forms.Label();
            this.prodtype = new System.Windows.Forms.TextBox();
            this.palletdetails = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PalletTypeList = new System.Windows.Forms.ComboBox();
            this.pquantity = new System.Windows.Forms.Label();
            this.qnty = new System.Windows.Forms.TextBox();
            this.addqty = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.submit = new System.Windows.Forms.Button();
            this.rightpanel = new System.Windows.Forms.Panel();
            this.palletdetailslayout = new System.Windows.Forms.TableLayoutPanel();
            this.palletdetailsheader = new System.Windows.Forms.Panel();
            this.palletdetailspanel = new System.Windows.Forms.Panel();
            this.printingdetailslayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.printingdetailsheader = new System.Windows.Forms.Panel();
            this.Printinglbl = new System.Windows.Forms.Label();
            this.lastboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.lastboxpanel = new System.Windows.Forms.Panel();
            this.wgroupbox = new System.Windows.Forms.GroupBox();
            this.windinggrid = new System.Windows.Forms.DataGridView();
            this.windingqty = new System.Data.DataTable();
            this.lastbxnetwtpanel = new System.Windows.Forms.Panel();
            this.netwttxtbox = new System.Windows.Forms.TextBox();
            this.netweight = new System.Windows.Forms.Label();
            this.lastbxgrosswtpanel = new System.Windows.Forms.Panel();
            this.grosswttxtbox = new System.Windows.Forms.TextBox();
            this.grossweight = new System.Windows.Forms.Label();
            this.lastbxtarepanel = new System.Windows.Forms.Panel();
            this.tarewghttxtbox = new System.Windows.Forms.TextBox();
            this.tareweight = new System.Windows.Forms.Label();
            this.lastbxcopspanel = new System.Windows.Forms.Panel();
            this.cops = new System.Windows.Forms.Label();
            this.copstxtbox = new System.Windows.Forms.TextBox();
            this.gradewiseprodn = new System.Windows.Forms.GroupBox();
            this.totalprodbalqty = new System.Windows.Forms.Label();
            this.qualityqty = new System.Windows.Forms.DataGridView();
            this.qualityandqty = new System.Data.DataTable();
            this.saleordrqty = new System.Windows.Forms.Label();
            this.lastboxheader = new System.Windows.Forms.Panel();
            this.Lastboxlbl = new System.Windows.Forms.Label();
            this.machineboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.machineboxpanel = new System.Windows.Forms.Panel();
            this.deniervalue = new System.Windows.Forms.TextBox();
            this.denier = new System.Windows.Forms.Label();
            this.PrefixList = new System.Windows.Forms.ComboBox();
            this.machineboxheader = new System.Windows.Forms.Panel();
            this.Machinelbl = new System.Windows.Forms.Label();
            this.weighboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.weighboxpanel = new System.Windows.Forms.Panel();
            this.grosswterror = new System.Windows.Forms.Label();
            this.palletwterror = new System.Windows.Forms.Label();
            this.spoolwterror = new System.Windows.Forms.Label();
            this.spoolnoerror = new System.Windows.Forms.Label();
            this.weighboxheader = new System.Windows.Forms.Panel();
            this.Weighboxlbl = new System.Windows.Forms.Label();
            this.packagingboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.packagingboxpanel = new System.Windows.Forms.Panel();
            this.packagingboxheader = new System.Windows.Forms.Panel();
            this.Packagingboxlbl = new System.Windows.Forms.Label();
            this.orderboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.orderboxpanel = new System.Windows.Forms.Panel();
            this.orderboxheader = new System.Windows.Forms.Panel();
            this.Orderboxlbl = new System.Windows.Forms.Label();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.boxnoerror = new System.Windows.Forms.Label();
            this.windingerror = new System.Windows.Forms.Label();
            this.packsizeerror = new System.Windows.Forms.Label();
            this.soerror = new System.Windows.Forms.Label();
            this.qualityerror = new System.Windows.Forms.Label();
            this.mergenoerror = new System.Windows.Forms.Label();
            this.copynoerror = new System.Windows.Forms.Label();
            this.linenoerror = new System.Windows.Forms.Label();
            this.windingtypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soqtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.windingbalqtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.windingprodqtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poyformlabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.leftpanel = new System.Windows.Forms.Panel();
            this.reviewtable = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.reviewlbl = new System.Windows.Forms.Label();
            this.reviewsubtitle = new System.Windows.Forms.Label();
            this.reviewdtls = new System.Windows.Forms.PictureBox();
            this.weighttable = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.weighlbl = new System.Windows.Forms.Label();
            this.weighsubtitle = new System.Windows.Forms.Label();
            this.weightdtls = new System.Windows.Forms.PictureBox();
            this.packagingtable = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.packaginglbl = new System.Windows.Forms.Label();
            this.packagingsubtitle = new System.Windows.Forms.Label();
            this.packagingdtls = new System.Windows.Forms.PictureBox();
            this.ordertable = new System.Windows.Forms.TableLayoutPanel();
            this.orderdetailsrightpanel = new System.Windows.Forms.Panel();
            this.orderlbl = new System.Windows.Forms.Label();
            this.orderdetailssubtitle = new System.Windows.Forms.Label();
            this.orderdetails1 = new System.Windows.Forms.PictureBox();
            this.rightpanel.SuspendLayout();
            this.palletdetailslayout.SuspendLayout();
            this.palletdetailsheader.SuspendLayout();
            this.palletdetailspanel.SuspendLayout();
            this.printingdetailslayout.SuspendLayout();
            this.panel3.SuspendLayout();
            this.printingdetailsheader.SuspendLayout();
            this.lastboxlayout.SuspendLayout();
            this.lastboxpanel.SuspendLayout();
            this.wgroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windinggrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.windingqty)).BeginInit();
            this.lastbxnetwtpanel.SuspendLayout();
            this.lastbxgrosswtpanel.SuspendLayout();
            this.lastbxtarepanel.SuspendLayout();
            this.lastbxcopspanel.SuspendLayout();
            this.gradewiseprodn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualityqty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityandqty)).BeginInit();
            this.lastboxheader.SuspendLayout();
            this.machineboxlayout.SuspendLayout();
            this.machineboxpanel.SuspendLayout();
            this.machineboxheader.SuspendLayout();
            this.weighboxlayout.SuspendLayout();
            this.weighboxpanel.SuspendLayout();
            this.weighboxheader.SuspendLayout();
            this.packagingboxlayout.SuspendLayout();
            this.packagingboxpanel.SuspendLayout();
            this.packagingboxheader.SuspendLayout();
            this.orderboxlayout.SuspendLayout();
            this.orderboxpanel.SuspendLayout();
            this.orderboxheader.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.leftpanel.SuspendLayout();
            this.reviewtable.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reviewdtls)).BeginInit();
            this.weighttable.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weightdtls)).BeginInit();
            this.packagingtable.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packagingdtls)).BeginInit();
            this.ordertable.SuspendLayout();
            this.orderdetailsrightpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdetails1)).BeginInit();
            this.SuspendLayout();
            // 
            // lineno
            // 
            this.lineno.AutoSize = true;
            this.lineno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.lineno.Location = new System.Drawing.Point(10, 5);
            this.lineno.Name = "lineno";
            this.lineno.Size = new System.Drawing.Size(43, 20);
            this.lineno.TabIndex = 0;
            this.lineno.Text = "Line:";
            // 
            // department
            // 
            this.department.AutoSize = true;
            this.department.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.department.Location = new System.Drawing.Point(9, 67);
            this.department.Name = "department";
            this.department.Size = new System.Drawing.Size(48, 20);
            this.department.TabIndex = 2;
            this.department.Text = "Dept:";
            // 
            // mergeno
            // 
            this.mergeno.AutoSize = true;
            this.mergeno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.mergeno.Location = new System.Drawing.Point(12, 128);
            this.mergeno.Name = "mergeno";
            this.mergeno.Size = new System.Drawing.Size(82, 20);
            this.mergeno.TabIndex = 4;
            this.mergeno.Text = "Merge No:";
            // 
            // lastboxno
            // 
            this.lastboxno.AutoSize = true;
            this.lastboxno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.lastboxno.Location = new System.Drawing.Point(239, 5);
            this.lastboxno.Name = "lastboxno";
            this.lastboxno.Size = new System.Drawing.Size(75, 20);
            this.lastboxno.TabIndex = 6;
            this.lastboxno.Text = "Last Box:";
            // 
            // lastbox
            // 
            this.lastbox.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.lastbox.Location = new System.Drawing.Point(243, 30);
            this.lastbox.Name = "lastbox";
            this.lastbox.Size = new System.Drawing.Size(190, 26);
            this.lastbox.TabIndex = 7;
            // 
            // item
            // 
            this.item.AutoSize = true;
            this.item.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.item.Location = new System.Drawing.Point(12, 187);
            this.item.Name = "item";
            this.item.Size = new System.Drawing.Size(45, 20);
            this.item.TabIndex = 8;
            this.item.Text = "Item:";
            // 
            // shade
            // 
            this.shade.AutoSize = true;
            this.shade.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.shade.Location = new System.Drawing.Point(239, 187);
            this.shade.Name = "shade";
            this.shade.Size = new System.Drawing.Size(60, 20);
            this.shade.TabIndex = 10;
            this.shade.Text = "Shade:";
            // 
            // shadecode
            // 
            this.shadecode.AutoSize = true;
            this.shadecode.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.shadecode.Location = new System.Drawing.Point(9, 240);
            this.shadecode.Name = "shadecode";
            this.shadecode.Size = new System.Drawing.Size(102, 20);
            this.shadecode.TabIndex = 12;
            this.shadecode.Text = "Shade Code:";
            // 
            // boxno
            // 
            this.boxno.AutoSize = true;
            this.boxno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxno.Location = new System.Drawing.Point(239, 67);
            this.boxno.Name = "boxno";
            this.boxno.Size = new System.Drawing.Size(68, 20);
            this.boxno.TabIndex = 14;
            this.boxno.Text = "Box No.:";
            // 
            // packingdate
            // 
            this.packingdate.AutoSize = true;
            this.packingdate.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.packingdate.Location = new System.Drawing.Point(239, 126);
            this.packingdate.Name = "packingdate";
            this.packingdate.Size = new System.Drawing.Size(48, 20);
            this.packingdate.TabIndex = 16;
            this.packingdate.Text = "Date:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.dateTimePicker1.Location = new System.Drawing.Point(243, 147);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(190, 26);
            this.dateTimePicker1.TabIndex = 17;
            // 
            // quality
            // 
            this.quality.AutoSize = true;
            this.quality.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.quality.Location = new System.Drawing.Point(10, 9);
            this.quality.Name = "quality";
            this.quality.Size = new System.Drawing.Size(61, 20);
            this.quality.TabIndex = 18;
            this.quality.Text = "Quality:";
            // 
            // saleorderno
            // 
            this.saleorderno.AutoSize = true;
            this.saleorderno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.saleorderno.Location = new System.Drawing.Point(239, 9);
            this.saleorderno.Name = "saleorderno";
            this.saleorderno.Size = new System.Drawing.Size(64, 20);
            this.saleorderno.TabIndex = 20;
            this.saleorderno.Text = "SO No.:";
            // 
            // packsize
            // 
            this.packsize.AutoSize = true;
            this.packsize.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.packsize.Location = new System.Drawing.Point(11, 7);
            this.packsize.Name = "packsize";
            this.packsize.Size = new System.Drawing.Size(83, 20);
            this.packsize.TabIndex = 22;
            this.packsize.Text = "Pack Size:";
            // 
            // frdenier
            // 
            this.frdenier.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.frdenier.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.frdenier.Location = new System.Drawing.Point(254, 29);
            this.frdenier.Name = "frdenier";
            this.frdenier.ReadOnly = true;
            this.frdenier.Size = new System.Drawing.Size(91, 26);
            this.frdenier.TabIndex = 25;
            // 
            // updenier
            // 
            this.updenier.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.updenier.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.updenier.Location = new System.Drawing.Point(354, 29);
            this.updenier.Name = "updenier";
            this.updenier.ReadOnly = true;
            this.updenier.Size = new System.Drawing.Size(82, 26);
            this.updenier.TabIndex = 27;
            // 
            // windingtype
            // 
            this.windingtype.AutoSize = true;
            this.windingtype.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.windingtype.Location = new System.Drawing.Point(15, 73);
            this.windingtype.Name = "windingtype";
            this.windingtype.Size = new System.Drawing.Size(70, 20);
            this.windingtype.TabIndex = 28;
            this.windingtype.Text = "Winding:";
            // 
            // comport
            // 
            this.comport.AutoSize = true;
            this.comport.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.comport.Location = new System.Drawing.Point(235, 4);
            this.comport.Name = "comport";
            this.comport.Size = new System.Drawing.Size(79, 20);
            this.comport.TabIndex = 30;
            this.comport.Text = "Com Port:";
            this.comport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // copssize
            // 
            this.copssize.AutoSize = true;
            this.copssize.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.copssize.Location = new System.Drawing.Point(11, 128);
            this.copssize.Name = "copssize";
            this.copssize.Size = new System.Drawing.Size(85, 20);
            this.copssize.TabIndex = 32;
            this.copssize.Text = "Cops Size:";
            // 
            // copweight
            // 
            this.copweight.AutoSize = true;
            this.copweight.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.copweight.Location = new System.Drawing.Point(186, 127);
            this.copweight.Name = "copweight";
            this.copweight.Size = new System.Drawing.Size(33, 20);
            this.copweight.TabIndex = 34;
            this.copweight.Text = "Wt:";
            // 
            // copstock
            // 
            this.copstock.AutoSize = true;
            this.copstock.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.copstock.Location = new System.Drawing.Point(250, 128);
            this.copstock.Name = "copstock";
            this.copstock.Size = new System.Drawing.Size(95, 20);
            this.copstock.TabIndex = 35;
            this.copstock.Text = "Cops Stock:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.textBox1.Location = new System.Drawing.Point(187, 150);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(48, 26);
            this.textBox1.TabIndex = 36;
            // 
            // textBox2
            // 
            this.textBox2.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.textBox2.Location = new System.Drawing.Point(254, 150);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(182, 26);
            this.textBox2.TabIndex = 37;
            // 
            // boxtype
            // 
            this.boxtype.AutoSize = true;
            this.boxtype.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxtype.Location = new System.Drawing.Point(11, 69);
            this.boxtype.Name = "boxtype";
            this.boxtype.Size = new System.Drawing.Size(121, 20);
            this.boxtype.TabIndex = 38;
            this.boxtype.Text = "Box/Pallet Type:";
            // 
            // boxweight
            // 
            this.boxweight.AutoSize = true;
            this.boxweight.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxweight.Location = new System.Drawing.Point(186, 68);
            this.boxweight.Name = "boxweight";
            this.boxweight.Size = new System.Drawing.Size(33, 20);
            this.boxweight.TabIndex = 40;
            this.boxweight.Text = "Wt:";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox3.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.textBox3.Location = new System.Drawing.Point(187, 91);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(48, 26);
            this.textBox3.TabIndex = 41;
            // 
            // boxstock
            // 
            this.boxstock.AutoSize = true;
            this.boxstock.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxstock.Location = new System.Drawing.Point(250, 69);
            this.boxstock.Name = "boxstock";
            this.boxstock.Size = new System.Drawing.Size(128, 20);
            this.boxstock.TabIndex = 42;
            this.boxstock.Text = "Box/Pallet Stock:";
            // 
            // textBox4
            // 
            this.textBox4.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.textBox4.Location = new System.Drawing.Point(254, 93);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(182, 26);
            this.textBox4.TabIndex = 43;
            // 
            // productiontype
            // 
            this.productiontype.AutoSize = true;
            this.productiontype.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.productiontype.Location = new System.Drawing.Point(244, 74);
            this.productiontype.Name = "productiontype";
            this.productiontype.Size = new System.Drawing.Size(84, 20);
            this.productiontype.TabIndex = 44;
            this.productiontype.Text = "Prod Type:";
            // 
            // remark
            // 
            this.remark.AutoSize = true;
            this.remark.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.remark.Location = new System.Drawing.Point(12, 134);
            this.remark.Name = "remark";
            this.remark.Size = new System.Drawing.Size(77, 20);
            this.remark.TabIndex = 46;
            this.remark.Text = "Remarks:";
            // 
            // remarks
            // 
            this.remarks.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.remarks.Location = new System.Drawing.Point(16, 157);
            this.remarks.Name = "remarks";
            this.remarks.Size = new System.Drawing.Size(189, 26);
            this.remarks.TabIndex = 47;
            // 
            // scalemodel
            // 
            this.scalemodel.AutoSize = true;
            this.scalemodel.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.scalemodel.Location = new System.Drawing.Point(13, 5);
            this.scalemodel.Name = "scalemodel";
            this.scalemodel.Size = new System.Drawing.Size(102, 20);
            this.scalemodel.TabIndex = 48;
            this.scalemodel.Text = "Weigh Scale:";
            // 
            // LineNoList
            // 
            this.LineNoList.AllowDrop = true;
            this.LineNoList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.LineNoList.FormattingEnabled = true;
            this.LineNoList.Location = new System.Drawing.Point(13, 30);
            this.LineNoList.Margin = new System.Windows.Forms.Padding(2);
            this.LineNoList.Name = "LineNoList";
            this.LineNoList.Size = new System.Drawing.Size(192, 28);
            this.LineNoList.TabIndex = 50;
            this.LineNoList.SelectedIndexChanged += new System.EventHandler(this.LineNoList_SelectedIndexChanged);
            // 
            // departmentname
            // 
            this.departmentname.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.departmentname.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.departmentname.Location = new System.Drawing.Point(14, 89);
            this.departmentname.Margin = new System.Windows.Forms.Padding(2);
            this.departmentname.Name = "departmentname";
            this.departmentname.ReadOnly = true;
            this.departmentname.Size = new System.Drawing.Size(191, 26);
            this.departmentname.TabIndex = 51;
            // 
            // MergeNoList
            // 
            this.MergeNoList.AllowDrop = true;
            this.MergeNoList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.MergeNoList.FormattingEnabled = true;
            this.MergeNoList.Location = new System.Drawing.Point(13, 150);
            this.MergeNoList.Margin = new System.Windows.Forms.Padding(2);
            this.MergeNoList.Name = "MergeNoList";
            this.MergeNoList.Size = new System.Drawing.Size(192, 28);
            this.MergeNoList.TabIndex = 53;
            this.MergeNoList.SelectedIndexChanged += new System.EventHandler(this.MergeNoList_SelectedIndexChanged);
            // 
            // itemname
            // 
            this.itemname.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.itemname.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.itemname.Location = new System.Drawing.Point(14, 211);
            this.itemname.Margin = new System.Windows.Forms.Padding(2);
            this.itemname.Name = "itemname";
            this.itemname.ReadOnly = true;
            this.itemname.Size = new System.Drawing.Size(191, 26);
            this.itemname.TabIndex = 54;
            // 
            // shadename
            // 
            this.shadename.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.shadename.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.shadename.Location = new System.Drawing.Point(243, 208);
            this.shadename.Margin = new System.Windows.Forms.Padding(2);
            this.shadename.Name = "shadename";
            this.shadename.ReadOnly = true;
            this.shadename.Size = new System.Drawing.Size(190, 26);
            this.shadename.TabIndex = 55;
            // 
            // shadecd
            // 
            this.shadecd.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.shadecd.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.shadecd.Location = new System.Drawing.Point(13, 262);
            this.shadecd.Margin = new System.Windows.Forms.Padding(2);
            this.shadecd.Name = "shadecd";
            this.shadecd.ReadOnly = true;
            this.shadecd.Size = new System.Drawing.Size(192, 26);
            this.shadecd.TabIndex = 56;
            // 
            // QualityList
            // 
            this.QualityList.AllowDrop = true;
            this.QualityList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.QualityList.FormattingEnabled = true;
            this.QualityList.Location = new System.Drawing.Point(14, 31);
            this.QualityList.Margin = new System.Windows.Forms.Padding(2);
            this.QualityList.Name = "QualityList";
            this.QualityList.Size = new System.Drawing.Size(191, 28);
            this.QualityList.TabIndex = 57;
            this.QualityList.SelectedIndexChanged += new System.EventHandler(this.QualityList_SelectedIndexChanged);
            // 
            // PackSizeList
            // 
            this.PackSizeList.AllowDrop = true;
            this.PackSizeList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.PackSizeList.FormattingEnabled = true;
            this.PackSizeList.Location = new System.Drawing.Point(14, 29);
            this.PackSizeList.Margin = new System.Windows.Forms.Padding(2);
            this.PackSizeList.Name = "PackSizeList";
            this.PackSizeList.Size = new System.Drawing.Size(193, 28);
            this.PackSizeList.TabIndex = 58;
            this.PackSizeList.SelectedIndexChanged += new System.EventHandler(this.PackSizeList_SelectedIndexChanged);
            // 
            // WindingTypeList
            // 
            this.WindingTypeList.AllowDrop = true;
            this.WindingTypeList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.WindingTypeList.FormattingEnabled = true;
            this.WindingTypeList.Location = new System.Drawing.Point(14, 95);
            this.WindingTypeList.Margin = new System.Windows.Forms.Padding(2);
            this.WindingTypeList.Name = "WindingTypeList";
            this.WindingTypeList.Size = new System.Drawing.Size(191, 28);
            this.WindingTypeList.TabIndex = 59;
            this.WindingTypeList.SelectedIndexChanged += new System.EventHandler(this.WindingTypeList_SelectedIndexChanged);
            // 
            // ComPortList
            // 
            this.ComPortList.AllowDrop = true;
            this.ComPortList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.ComPortList.FormattingEnabled = true;
            this.ComPortList.Location = new System.Drawing.Point(239, 29);
            this.ComPortList.Margin = new System.Windows.Forms.Padding(2);
            this.ComPortList.Name = "ComPortList";
            this.ComPortList.Size = new System.Drawing.Size(197, 28);
            this.ComPortList.TabIndex = 60;
            this.ComPortList.SelectedIndexChanged += new System.EventHandler(this.ComPortList_SelectedIndexChanged);
            // 
            // WeighingList
            // 
            this.WeighingList.AllowDrop = true;
            this.WeighingList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.WeighingList.FormattingEnabled = true;
            this.WeighingList.Location = new System.Drawing.Point(15, 30);
            this.WeighingList.Margin = new System.Windows.Forms.Padding(2);
            this.WeighingList.Name = "WeighingList";
            this.WeighingList.Size = new System.Drawing.Size(197, 28);
            this.WeighingList.TabIndex = 61;
            this.WeighingList.SelectedIndexChanged += new System.EventHandler(this.WeighingList_SelectedIndexChanged);
            // 
            // CopsItemList
            // 
            this.CopsItemList.AllowDrop = true;
            this.CopsItemList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.CopsItemList.FormattingEnabled = true;
            this.CopsItemList.Location = new System.Drawing.Point(14, 150);
            this.CopsItemList.Margin = new System.Windows.Forms.Padding(2);
            this.CopsItemList.Name = "CopsItemList";
            this.CopsItemList.Size = new System.Drawing.Size(168, 28);
            this.CopsItemList.TabIndex = 62;
            this.CopsItemList.SelectedIndexChanged += new System.EventHandler(this.CopsItemList_SelectedIndexChanged);
            // 
            // BoxItemList
            // 
            this.BoxItemList.AllowDrop = true;
            this.BoxItemList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.BoxItemList.FormattingEnabled = true;
            this.BoxItemList.Location = new System.Drawing.Point(14, 91);
            this.BoxItemList.Margin = new System.Windows.Forms.Padding(2);
            this.BoxItemList.Name = "BoxItemList";
            this.BoxItemList.Size = new System.Drawing.Size(168, 28);
            this.BoxItemList.TabIndex = 63;
            this.BoxItemList.SelectedIndexChanged += new System.EventHandler(this.BoxItemList_SelectedIndexChanged);
            // 
            // SaleOrderList
            // 
            this.SaleOrderList.AllowDrop = true;
            this.SaleOrderList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.SaleOrderList.FormattingEnabled = true;
            this.SaleOrderList.Location = new System.Drawing.Point(243, 31);
            this.SaleOrderList.Margin = new System.Windows.Forms.Padding(2);
            this.SaleOrderList.Name = "SaleOrderList";
            this.SaleOrderList.Size = new System.Drawing.Size(185, 28);
            this.SaleOrderList.TabIndex = 64;
            this.SaleOrderList.SelectedIndexChanged += new System.EventHandler(this.SaleOrderList_SelectedIndexChanged);
            // 
            // prcompany
            // 
            this.prcompany.AutoSize = true;
            this.prcompany.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prcompany.Location = new System.Drawing.Point(9, 6);
            this.prcompany.Name = "prcompany";
            this.prcompany.Size = new System.Drawing.Size(138, 24);
            this.prcompany.TabIndex = 66;
            this.prcompany.Text = "Print Company";
            this.prcompany.UseVisualStyleBackColor = true;
            // 
            // prowner
            // 
            this.prowner.AutoSize = true;
            this.prowner.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prowner.Location = new System.Drawing.Point(157, 7);
            this.prowner.Name = "prowner";
            this.prowner.Size = new System.Drawing.Size(117, 24);
            this.prowner.TabIndex = 67;
            this.prowner.Text = "Print Owner";
            this.prowner.UseVisualStyleBackColor = true;
            // 
            // prdate
            // 
            this.prdate.AutoSize = true;
            this.prdate.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prdate.Location = new System.Drawing.Point(320, 7);
            this.prdate.Name = "prdate";
            this.prdate.Size = new System.Drawing.Size(106, 24);
            this.prdate.TabIndex = 68;
            this.prdate.Text = "Print Date";
            this.prdate.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prdate.UseVisualStyleBackColor = true;
            // 
            // pruser
            // 
            this.pruser.AutoSize = true;
            this.pruser.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.pruser.Location = new System.Drawing.Point(9, 35);
            this.pruser.Name = "pruser";
            this.pruser.Size = new System.Drawing.Size(105, 24);
            this.pruser.TabIndex = 69;
            this.pruser.Text = "Print User";
            this.pruser.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pruser.UseVisualStyleBackColor = true;
            // 
            // prhindi
            // 
            this.prhindi.AutoSize = true;
            this.prhindi.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prhindi.Location = new System.Drawing.Point(157, 38);
            this.prhindi.Name = "prhindi";
            this.prhindi.Size = new System.Drawing.Size(157, 24);
            this.prhindi.TabIndex = 70;
            this.prhindi.Text = "Print Hindi Words";
            this.prhindi.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prhindi.UseVisualStyleBackColor = true;
            // 
            // prwtps
            // 
            this.prwtps.AutoSize = true;
            this.prwtps.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prwtps.Location = new System.Drawing.Point(320, 38);
            this.prwtps.Name = "prwtps";
            this.prwtps.Size = new System.Drawing.Size(120, 24);
            this.prwtps.TabIndex = 71;
            this.prwtps.Text = "Print WT/PS";
            this.prwtps.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prwtps.UseVisualStyleBackColor = true;
            // 
            // prqrcode
            // 
            this.prqrcode.AutoSize = true;
            this.prqrcode.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prqrcode.Location = new System.Drawing.Point(9, 65);
            this.prqrcode.Name = "prqrcode";
            this.prqrcode.Size = new System.Drawing.Size(137, 24);
            this.prqrcode.TabIndex = 72;
            this.prqrcode.Text = "Print QR Code";
            this.prqrcode.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prqrcode.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(244, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 73;
            this.label1.Text = "No. Of Copies:";
            // 
            // copyno
            // 
            this.copyno.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.copyno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.copyno.Location = new System.Drawing.Point(243, 157);
            this.copyno.Name = "copyno";
            this.copyno.ReadOnly = true;
            this.copyno.Size = new System.Drawing.Size(185, 26);
            this.copyno.TabIndex = 74;
            this.copyno.TextChanged += new System.EventHandler(this.CopyNos_TextChanged);
            // 
            // wtpercop
            // 
            this.wtpercop.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.wtpercop.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.wtpercop.Location = new System.Drawing.Point(240, 205);
            this.wtpercop.Name = "wtpercop";
            this.wtpercop.ReadOnly = true;
            this.wtpercop.Size = new System.Drawing.Size(196, 26);
            this.wtpercop.TabIndex = 85;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(236, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 20);
            this.label5.TabIndex = 84;
            this.label5.Text = "Wt.Per Cop:";
            // 
            // netwt
            // 
            this.netwt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.netwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.netwt.Location = new System.Drawing.Point(14, 207);
            this.netwt.Name = "netwt";
            this.netwt.ReadOnly = true;
            this.netwt.Size = new System.Drawing.Size(197, 26);
            this.netwt.TabIndex = 83;
            this.netwt.TextChanged += new System.EventHandler(this.NetWeight_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(13, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.TabIndex = 82;
            this.label4.Text = "Net Wt:";
            // 
            // tarewt
            // 
            this.tarewt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tarewt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.tarewt.Location = new System.Drawing.Point(240, 146);
            this.tarewt.Name = "tarewt";
            this.tarewt.ReadOnly = true;
            this.tarewt.Size = new System.Drawing.Size(196, 26);
            this.tarewt.TabIndex = 81;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(236, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 80;
            this.label3.Text = "Tare Wt:";
            // 
            // grosswtno
            // 
            this.grosswtno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.grosswtno.Location = new System.Drawing.Point(14, 148);
            this.grosswtno.Name = "grosswtno";
            this.grosswtno.Size = new System.Drawing.Size(196, 26);
            this.grosswtno.TabIndex = 79;
            this.grosswtno.TextChanged += new System.EventHandler(this.GrossWeight_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(10, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 78;
            this.label2.Text = "Gross Wt:";
            // 
            // palletwtno
            // 
            this.palletwtno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.palletwtno.Location = new System.Drawing.Point(240, 92);
            this.palletwtno.Name = "palletwtno";
            this.palletwtno.Size = new System.Drawing.Size(196, 26);
            this.palletwtno.TabIndex = 77;
            this.palletwtno.TextChanged += new System.EventHandler(this.PalletWeight_TextChanged);
            // 
            // palletwt
            // 
            this.palletwt.AutoSize = true;
            this.palletwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.palletwt.Location = new System.Drawing.Point(235, 69);
            this.palletwt.Name = "palletwt";
            this.palletwt.Size = new System.Drawing.Size(156, 20);
            this.palletwt.TabIndex = 76;
            this.palletwt.Text = "Empty Box/Pallet Wt:";
            // 
            // spoolwt
            // 
            this.spoolwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.spoolwt.Location = new System.Drawing.Point(124, 92);
            this.spoolwt.Name = "spoolwt";
            this.spoolwt.Size = new System.Drawing.Size(83, 26);
            this.spoolwt.TabIndex = 76;
            this.spoolwt.TextChanged += new System.EventHandler(this.SpoolWeight_TextChanged);
            // 
            // spoolno
            // 
            this.spoolno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.spoolno.Location = new System.Drawing.Point(14, 92);
            this.spoolno.Name = "spoolno";
            this.spoolno.Size = new System.Drawing.Size(96, 26);
            this.spoolno.TabIndex = 1;
            this.spoolno.TextChanged += new System.EventHandler(this.SpoolNo_TextChanged);
            // 
            // spool
            // 
            this.spool.AutoSize = true;
            this.spool.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.spool.Location = new System.Drawing.Point(13, 66);
            this.spool.Name = "spool";
            this.spool.Size = new System.Drawing.Size(62, 20);
            this.spool.TabIndex = 0;
            this.spool.Text = "Spools:";
            // 
            // prodtype
            // 
            this.prodtype.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.prodtype.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prodtype.Location = new System.Drawing.Point(243, 97);
            this.prodtype.Name = "prodtype";
            this.prodtype.ReadOnly = true;
            this.prodtype.Size = new System.Drawing.Size(185, 26);
            this.prodtype.TabIndex = 76;
            // 
            // palletdetails
            // 
            this.palletdetails.AutoSize = true;
            this.palletdetails.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.palletdetails.Location = new System.Drawing.Point(6, 9);
            this.palletdetails.Name = "palletdetails";
            this.palletdetails.Size = new System.Drawing.Size(120, 20);
            this.palletdetails.TabIndex = 78;
            this.palletdetails.Text = "Pallet Details:";
            this.palletdetails.UseMnemonic = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(8, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 20);
            this.label6.TabIndex = 79;
            this.label6.Text = "Pallet Type:";
            this.label6.UseMnemonic = false;
            // 
            // PalletTypeList
            // 
            this.PalletTypeList.AllowDrop = true;
            this.PalletTypeList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.PalletTypeList.FormattingEnabled = true;
            this.PalletTypeList.Location = new System.Drawing.Point(105, 6);
            this.PalletTypeList.Margin = new System.Windows.Forms.Padding(2);
            this.PalletTypeList.Name = "PalletTypeList";
            this.PalletTypeList.Size = new System.Drawing.Size(140, 28);
            this.PalletTypeList.TabIndex = 80;
            // 
            // pquantity
            // 
            this.pquantity.AutoSize = true;
            this.pquantity.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.pquantity.Location = new System.Drawing.Point(250, 9);
            this.pquantity.Name = "pquantity";
            this.pquantity.Size = new System.Drawing.Size(37, 20);
            this.pquantity.TabIndex = 81;
            this.pquantity.Text = "Qty:";
            this.pquantity.UseMnemonic = false;
            // 
            // qnty
            // 
            this.qnty.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.qnty.Location = new System.Drawing.Point(303, 6);
            this.qnty.Name = "qnty";
            this.qnty.Size = new System.Drawing.Size(62, 26);
            this.qnty.TabIndex = 82;
            // 
            // addqty
            // 
            this.addqty.BackColor = System.Drawing.SystemColors.Highlight;
            this.addqty.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.addqty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addqty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addqty.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.addqty.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.addqty.Location = new System.Drawing.Point(378, 7);
            this.addqty.Name = "addqty";
            this.addqty.Size = new System.Drawing.Size(58, 25);
            this.addqty.TabIndex = 83;
            this.addqty.Text = "Add";
            this.addqty.UseVisualStyleBackColor = false;
            this.addqty.Click += new System.EventHandler(this.addqty_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 39);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(448, 152);
            this.flowLayoutPanel1.TabIndex = 84;
            // 
            // submit
            // 
            this.submit.BackColor = System.Drawing.SystemColors.Highlight;
            this.submit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submit.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.submit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.submit.Location = new System.Drawing.Point(462, 1082);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(95, 28);
            this.submit.TabIndex = 85;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = false;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // rightpanel
            // 
            this.rightpanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightpanel.AutoScroll = true;
            this.rightpanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rightpanel.Controls.Add(this.palletdetailslayout);
            this.rightpanel.Controls.Add(this.printingdetailslayout);
            this.rightpanel.Controls.Add(this.lastboxlayout);
            this.rightpanel.Controls.Add(this.machineboxlayout);
            this.rightpanel.Controls.Add(this.weighboxlayout);
            this.rightpanel.Controls.Add(this.packagingboxlayout);
            this.rightpanel.Controls.Add(this.orderboxlayout);
            this.rightpanel.Controls.Add(this.cancelbtn);
            this.rightpanel.Controls.Add(this.boxnoerror);
            this.rightpanel.Controls.Add(this.windingerror);
            this.rightpanel.Controls.Add(this.packsizeerror);
            this.rightpanel.Controls.Add(this.soerror);
            this.rightpanel.Controls.Add(this.qualityerror);
            this.rightpanel.Controls.Add(this.mergenoerror);
            this.rightpanel.Controls.Add(this.copynoerror);
            this.rightpanel.Controls.Add(this.linenoerror);
            this.rightpanel.Controls.Add(this.submit);
            this.rightpanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rightpanel.Location = new System.Drawing.Point(265, 3);
            this.rightpanel.Name = "rightpanel";
            this.rightpanel.Size = new System.Drawing.Size(1044, 1044);
            this.rightpanel.TabIndex = 89;
            // 
            // palletdetailslayout
            // 
            this.palletdetailslayout.ColumnCount = 1;
            this.palletdetailslayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.palletdetailslayout.Controls.Add(this.palletdetailsheader, 0, 0);
            this.palletdetailslayout.Controls.Add(this.palletdetailspanel, 0, 1);
            this.palletdetailslayout.Location = new System.Drawing.Point(506, 799);
            this.palletdetailslayout.Name = "palletdetailslayout";
            this.palletdetailslayout.Padding = new System.Windows.Forms.Padding(2);
            this.palletdetailslayout.RowCount = 2;
            this.palletdetailslayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.palletdetailslayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.palletdetailslayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.palletdetailslayout.Size = new System.Drawing.Size(464, 249);
            this.palletdetailslayout.TabIndex = 113;
            this.palletdetailslayout.Paint += new System.Windows.Forms.PaintEventHandler(this.palletdetailslayout_Paint);
            // 
            // palletdetailsheader
            // 
            this.palletdetailsheader.Controls.Add(this.palletdetails);
            this.palletdetailsheader.Location = new System.Drawing.Point(5, 5);
            this.palletdetailsheader.Name = "palletdetailsheader";
            this.palletdetailsheader.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.palletdetailsheader.Size = new System.Drawing.Size(454, 38);
            this.palletdetailsheader.TabIndex = 107;
            this.palletdetailsheader.Paint += new System.Windows.Forms.PaintEventHandler(this.palletdetailsheader_Paint);
            // 
            // palletdetailspanel
            // 
            this.palletdetailspanel.Controls.Add(this.label6);
            this.palletdetailspanel.Controls.Add(this.PalletTypeList);
            this.palletdetailspanel.Controls.Add(this.pquantity);
            this.palletdetailspanel.Controls.Add(this.qnty);
            this.palletdetailspanel.Controls.Add(this.addqty);
            this.palletdetailspanel.Controls.Add(this.flowLayoutPanel1);
            this.palletdetailspanel.Location = new System.Drawing.Point(5, 49);
            this.palletdetailspanel.Name = "palletdetailspanel";
            this.palletdetailspanel.Size = new System.Drawing.Size(454, 195);
            this.palletdetailspanel.TabIndex = 108;
            // 
            // printingdetailslayout
            // 
            this.printingdetailslayout.ColumnCount = 1;
            this.printingdetailslayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.printingdetailslayout.Controls.Add(this.panel3, 0, 1);
            this.printingdetailslayout.Controls.Add(this.printingdetailsheader, 0, 0);
            this.printingdetailslayout.Location = new System.Drawing.Point(506, 633);
            this.printingdetailslayout.Name = "printingdetailslayout";
            this.printingdetailslayout.Padding = new System.Windows.Forms.Padding(2);
            this.printingdetailslayout.RowCount = 2;
            this.printingdetailslayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.printingdetailslayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.printingdetailslayout.Size = new System.Drawing.Size(464, 145);
            this.printingdetailslayout.TabIndex = 112;
            this.printingdetailslayout.Paint += new System.Windows.Forms.PaintEventHandler(this.printingdetailslayout_Paint);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.prowner);
            this.panel3.Controls.Add(this.prwtps);
            this.panel3.Controls.Add(this.prcompany);
            this.panel3.Controls.Add(this.prqrcode);
            this.panel3.Controls.Add(this.prhindi);
            this.panel3.Controls.Add(this.pruser);
            this.panel3.Controls.Add(this.prdate);
            this.panel3.Location = new System.Drawing.Point(5, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(454, 93);
            this.panel3.TabIndex = 87;
            // 
            // printingdetailsheader
            // 
            this.printingdetailsheader.Controls.Add(this.Printinglbl);
            this.printingdetailsheader.Location = new System.Drawing.Point(5, 5);
            this.printingdetailsheader.Name = "printingdetailsheader";
            this.printingdetailsheader.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.printingdetailsheader.Size = new System.Drawing.Size(454, 36);
            this.printingdetailsheader.TabIndex = 107;
            this.printingdetailsheader.Paint += new System.Windows.Forms.PaintEventHandler(this.printingdetailsheader_Paint);
            // 
            // Printinglbl
            // 
            this.Printinglbl.AutoSize = true;
            this.Printinglbl.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Printinglbl.Location = new System.Drawing.Point(6, 9);
            this.Printinglbl.Name = "Printinglbl";
            this.Printinglbl.Size = new System.Drawing.Size(128, 20);
            this.Printinglbl.TabIndex = 107;
            this.Printinglbl.Text = "Printing details";
            // 
            // lastboxlayout
            // 
            this.lastboxlayout.ColumnCount = 1;
            this.lastboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lastboxlayout.Controls.Add(this.lastboxpanel, 0, 1);
            this.lastboxlayout.Controls.Add(this.lastboxheader, 0, 0);
            this.lastboxlayout.Location = new System.Drawing.Point(13, 633);
            this.lastboxlayout.Name = "lastboxlayout";
            this.lastboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.lastboxlayout.RowCount = 2;
            this.lastboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.lastboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.lastboxlayout.Size = new System.Drawing.Size(464, 415);
            this.lastboxlayout.TabIndex = 111;
            this.lastboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.lastboxlayout_Paint);
            // 
            // lastboxpanel
            // 
            this.lastboxpanel.Controls.Add(this.wgroupbox);
            this.lastboxpanel.Controls.Add(this.lastbxnetwtpanel);
            this.lastboxpanel.Controls.Add(this.lastbxgrosswtpanel);
            this.lastboxpanel.Controls.Add(this.lastbxtarepanel);
            this.lastboxpanel.Controls.Add(this.lastbxcopspanel);
            this.lastboxpanel.Controls.Add(this.gradewiseprodn);
            this.lastboxpanel.Location = new System.Drawing.Point(5, 46);
            this.lastboxpanel.Name = "lastboxpanel";
            this.lastboxpanel.Size = new System.Drawing.Size(454, 364);
            this.lastboxpanel.TabIndex = 107;
            // 
            // wgroupbox
            // 
            this.wgroupbox.Controls.Add(this.windinggrid);
            this.wgroupbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.wgroupbox.Location = new System.Drawing.Point(10, 96);
            this.wgroupbox.Name = "wgroupbox";
            this.wgroupbox.Size = new System.Drawing.Size(210, 264);
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
            this.windinggrid.Location = new System.Drawing.Point(7, 43);
            this.windinggrid.Name = "windinggrid";
            this.windinggrid.RowHeadersVisible = false;
            this.windinggrid.RowHeadersWidth = 62;
            this.windinggrid.Size = new System.Drawing.Size(194, 215);
            this.windinggrid.TabIndex = 92;
            this.windinggrid.Paint += new System.Windows.Forms.PaintEventHandler(this.windinggrid_Paint);
            // 
            // lastbxnetwtpanel
            // 
            this.lastbxnetwtpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxnetwtpanel.Controls.Add(this.netwttxtbox);
            this.lastbxnetwtpanel.Controls.Add(this.netweight);
            this.lastbxnetwtpanel.Location = new System.Drawing.Point(336, 5);
            this.lastbxnetwtpanel.Name = "lastbxnetwtpanel";
            this.lastbxnetwtpanel.Size = new System.Drawing.Size(105, 68);
            this.lastbxnetwtpanel.TabIndex = 8;
            this.lastbxnetwtpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lastbxnetwtpanel_Paint);
            // 
            // netwttxtbox
            // 
            this.netwttxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.netwttxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.netwttxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.netwttxtbox.Location = new System.Drawing.Point(13, 34);
            this.netwttxtbox.Name = "netwttxtbox";
            this.netwttxtbox.ReadOnly = true;
            this.netwttxtbox.Size = new System.Drawing.Size(78, 19);
            this.netwttxtbox.TabIndex = 95;
            this.netwttxtbox.TextAlign = HorizontalAlignment.Center;
            this.netwttxtbox.ForeColor = Color.FromArgb(0, 153, 0);
            // 
            // netweight
            // 
            this.netweight.AutoSize = true;
            this.netweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.netweight.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.netweight.Location = new System.Drawing.Point(35, 7);
            this.netweight.Name = "netweight";
            this.netweight.Size = new System.Drawing.Size(37, 20);
            this.netweight.TabIndex = 8;
            this.netweight.Text = "Net";
            // 
            // lastbxgrosswtpanel
            // 
            this.lastbxgrosswtpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxgrosswtpanel.Controls.Add(this.grosswttxtbox);
            this.lastbxgrosswtpanel.Controls.Add(this.grossweight);
            this.lastbxgrosswtpanel.Location = new System.Drawing.Point(211, 5);
            this.lastbxgrosswtpanel.Name = "lastbxgrosswtpanel";
            this.lastbxgrosswtpanel.Size = new System.Drawing.Size(105, 68);
            this.lastbxgrosswtpanel.TabIndex = 6;
            this.lastbxgrosswtpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lastbxgrosswtpanel_Paint);
            // 
            // grosswttxtbox
            // 
            this.grosswttxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.grosswttxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grosswttxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.grosswttxtbox.Location = new System.Drawing.Point(12, 34);
            this.grosswttxtbox.Name = "grosswttxtbox";
            this.grosswttxtbox.ReadOnly = true;
            this.grosswttxtbox.Size = new System.Drawing.Size(76, 19);
            this.grosswttxtbox.TabIndex = 7;
            this.grosswttxtbox.TextAlign = HorizontalAlignment.Center;
            // 
            // grossweight
            // 
            this.grossweight.AutoSize = true;
            this.grossweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.grossweight.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grossweight.Location = new System.Drawing.Point(30, 8);
            this.grossweight.Name = "grossweight";
            this.grossweight.Size = new System.Drawing.Size(57, 20);
            this.grossweight.TabIndex = 6;
            this.grossweight.Text = "Gross";
            // 
            // lastbxtarepanel
            // 
            this.lastbxtarepanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxtarepanel.Controls.Add(this.tarewghttxtbox);
            this.lastbxtarepanel.Controls.Add(this.tareweight);
            this.lastbxtarepanel.Location = new System.Drawing.Point(90, 5);
            this.lastbxtarepanel.Name = "lastbxtarepanel";
            this.lastbxtarepanel.Size = new System.Drawing.Size(105, 68);
            this.lastbxtarepanel.TabIndex = 5;
            this.lastbxtarepanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lastbxtarepanel_Paint);
            // 
            // tarewghttxtbox
            // 
            this.tarewghttxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.tarewghttxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tarewghttxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.tarewghttxtbox.Location = new System.Drawing.Point(8, 33);
            this.tarewghttxtbox.Name = "tarewghttxtbox";
            this.tarewghttxtbox.ReadOnly = true;
            this.tarewghttxtbox.Size = new System.Drawing.Size(86, 19);
            this.tarewghttxtbox.TabIndex = 5;
            this.tarewghttxtbox.TextAlign = HorizontalAlignment.Center;
            // 
            // tareweight
            // 
            this.tareweight.AutoSize = true;
            this.tareweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.tareweight.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tareweight.Location = new System.Drawing.Point(30, 7);
            this.tareweight.Name = "tareweight";
            this.tareweight.Size = new System.Drawing.Size(45, 20);
            this.tareweight.TabIndex = 4;
            this.tareweight.Text = "Tare";
            // 
            // lastbxcopspanel
            // 
            this.lastbxcopspanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxcopspanel.Controls.Add(this.cops);
            this.lastbxcopspanel.Controls.Add(this.copstxtbox);
            this.lastbxcopspanel.Location = new System.Drawing.Point(7, 5);
            this.lastbxcopspanel.Name = "lastbxcopspanel";
            this.lastbxcopspanel.Size = new System.Drawing.Size(68, 68);
            this.lastbxcopspanel.TabIndex = 4;
            this.lastbxcopspanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lastbxcopspanel_Paint);
            // 
            // cops
            // 
            this.cops.AutoSize = true;
            this.cops.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.cops.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cops.Location = new System.Drawing.Point(15, 10);
            this.cops.Name = "cops";
            this.cops.Size = new System.Drawing.Size(50, 20);
            this.cops.TabIndex = 2;
            this.cops.Text = "Cops";
            // 
            // copstxtbox
            // 
            this.copstxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.copstxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.copstxtbox.Location = new System.Drawing.Point(12, 30);
            this.copstxtbox.Name = "copstxtbox";
            this.copstxtbox.ReadOnly = true;
            this.copstxtbox.Size = new System.Drawing.Size(42, 21);
            this.copstxtbox.TabIndex = 3;
            this.copstxtbox.TextAlign = HorizontalAlignment.Center;
            // 
            // gradewiseprodn
            // 
            this.gradewiseprodn.Controls.Add(this.totalprodbalqty);
            this.gradewiseprodn.Controls.Add(this.qualityqty);
            this.gradewiseprodn.Controls.Add(this.saleordrqty);
            this.gradewiseprodn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.gradewiseprodn.ForeColor = System.Drawing.Color.Black;
            this.gradewiseprodn.Location = new System.Drawing.Point(236, 96);
            this.gradewiseprodn.Name = "gradewiseprodn";
            this.gradewiseprodn.Size = new System.Drawing.Size(205, 264);
            this.gradewiseprodn.TabIndex = 93;
            this.gradewiseprodn.TabStop = false;
            this.gradewiseprodn.Text = "Gradewise Prodn Status";
            // 
            // totalprodbalqty
            // 
            this.totalprodbalqty.AutoSize = true;
            this.totalprodbalqty.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.totalprodbalqty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.totalprodbalqty.Location = new System.Drawing.Point(8, 238);
            this.totalprodbalqty.Name = "totalprodbalqty";
            this.totalprodbalqty.Size = new System.Drawing.Size(144, 20);
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
            this.qualityqty.Location = new System.Drawing.Point(15, 43);
            this.qualityqty.Name = "qualityqty";
            this.qualityqty.RowHeadersVisible = false;
            this.qualityqty.RowHeadersWidth = 62;
            this.qualityqty.Size = new System.Drawing.Size(174, 192);
            this.qualityqty.TabIndex = 92;
            this.qualityqty.Paint += new System.Windows.Forms.PaintEventHandler(this.qualityqty_Paint);
            // 
            // saleordrqty
            // 
            this.saleordrqty.AutoSize = true;
            this.saleordrqty.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.saleordrqty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saleordrqty.Location = new System.Drawing.Point(10, 17);
            this.saleordrqty.Name = "saleordrqty";
            this.saleordrqty.Size = new System.Drawing.Size(121, 20);
            this.saleordrqty.TabIndex = 92;
            this.saleordrqty.Text = "Sale Order Qty :";
            // 
            // lastboxheader
            // 
            this.lastboxheader.Controls.Add(this.Lastboxlbl);
            this.lastboxheader.Location = new System.Drawing.Point(5, 5);
            this.lastboxheader.Name = "lastboxheader";
            this.lastboxheader.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.lastboxheader.Size = new System.Drawing.Size(454, 35);
            this.lastboxheader.TabIndex = 107;
            this.lastboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.lastboxheader_Paint);
            // 
            // Lastboxlbl
            // 
            this.Lastboxlbl.AutoSize = true;
            this.Lastboxlbl.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Lastboxlbl.Location = new System.Drawing.Point(6, 9);
            this.Lastboxlbl.Name = "Lastboxlbl";
            this.Lastboxlbl.Size = new System.Drawing.Size(135, 20);
            this.Lastboxlbl.TabIndex = 107;
            this.Lastboxlbl.Text = "Last box details";
            // 
            // machineboxlayout
            // 
            this.machineboxlayout.ColumnCount = 1;
            this.machineboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.machineboxlayout.Controls.Add(this.machineboxpanel, 0, 1);
            this.machineboxlayout.Controls.Add(this.machineboxheader, 0, 0);
            this.machineboxlayout.Location = new System.Drawing.Point(13, 3);
            this.machineboxlayout.Name = "machineboxlayout";
            this.machineboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.machineboxlayout.RowCount = 2;
            this.machineboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.machineboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86F));
            this.machineboxlayout.Size = new System.Drawing.Size(464, 349);
            this.machineboxlayout.TabIndex = 110;
            this.machineboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.machineboxlayout_Paint);
            // 
            // machineboxpanel
            // 
            this.machineboxpanel.Controls.Add(this.deniervalue);
            this.machineboxpanel.Controls.Add(this.denier);
            this.machineboxpanel.Controls.Add(this.shadecd);
            this.machineboxpanel.Controls.Add(this.shadecode);
            this.machineboxpanel.Controls.Add(this.lineno);
            this.machineboxpanel.Controls.Add(this.LineNoList);
            this.machineboxpanel.Controls.Add(this.lastboxno);
            this.machineboxpanel.Controls.Add(this.lastbox);
            this.machineboxpanel.Controls.Add(this.department);
            this.machineboxpanel.Controls.Add(this.departmentname);
            this.machineboxpanel.Controls.Add(this.boxno);
            this.machineboxpanel.Controls.Add(this.PrefixList);
            this.machineboxpanel.Controls.Add(this.mergeno);
            this.machineboxpanel.Controls.Add(this.MergeNoList);
            this.machineboxpanel.Controls.Add(this.packingdate);
            this.machineboxpanel.Controls.Add(this.dateTimePicker1);
            this.machineboxpanel.Controls.Add(this.item);
            this.machineboxpanel.Controls.Add(this.itemname);
            this.machineboxpanel.Controls.Add(this.shade);
            this.machineboxpanel.Controls.Add(this.shadename);
            this.machineboxpanel.Location = new System.Drawing.Point(5, 53);
            this.machineboxpanel.Name = "machineboxpanel";
            this.machineboxpanel.Size = new System.Drawing.Size(454, 291);
            this.machineboxpanel.TabIndex = 107;
            // 
            // deniervalue
            // 
            this.deniervalue.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.deniervalue.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.deniervalue.Location = new System.Drawing.Point(243, 262);
            this.deniervalue.Margin = new System.Windows.Forms.Padding(2);
            this.deniervalue.Name = "deniervalue";
            this.deniervalue.ReadOnly = true;
            this.deniervalue.Size = new System.Drawing.Size(190, 26);
            this.deniervalue.TabIndex = 95;
            // 
            // denier
            // 
            this.denier.AutoSize = true;
            this.denier.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.denier.Location = new System.Drawing.Point(239, 240);
            this.denier.Name = "denier";
            this.denier.Size = new System.Drawing.Size(60, 20);
            this.denier.TabIndex = 94;
            this.denier.Text = "Denier:";
            // 
            // PrefixList
            // 
            this.PrefixList.AllowDrop = true;
            this.PrefixList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.PrefixList.FormattingEnabled = true;
            this.PrefixList.Location = new System.Drawing.Point(243, 89);
            this.PrefixList.Margin = new System.Windows.Forms.Padding(2);
            this.PrefixList.Name = "PrefixList";
            this.PrefixList.Size = new System.Drawing.Size(190, 28);
            this.PrefixList.TabIndex = 86;
            this.PrefixList.SelectedIndexChanged += new System.EventHandler(this.PrefixList_SelectedIndexChanged);
            // 
            // machineboxheader
            // 
            this.machineboxheader.Controls.Add(this.Machinelbl);
            this.machineboxheader.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.machineboxheader.Location = new System.Drawing.Point(5, 5);
            this.machineboxheader.Name = "machineboxheader";
            this.machineboxheader.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.machineboxheader.Size = new System.Drawing.Size(454, 42);
            this.machineboxheader.TabIndex = 107;
            this.machineboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.machineboxheader_Paint);
            // 
            // Machinelbl
            // 
            this.Machinelbl.AutoSize = true;
            this.Machinelbl.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Machinelbl.Location = new System.Drawing.Point(3, 12);
            this.Machinelbl.Name = "Machinelbl";
            this.Machinelbl.Size = new System.Drawing.Size(137, 20);
            this.Machinelbl.TabIndex = 107;
            this.Machinelbl.Text = "Machine Details";
            // 
            // weighboxlayout
            // 
            this.weighboxlayout.ColumnCount = 1;
            this.weighboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.weighboxlayout.Controls.Add(this.weighboxpanel, 0, 1);
            this.weighboxlayout.Controls.Add(this.weighboxheader, 0, 0);
            this.weighboxlayout.Location = new System.Drawing.Point(506, 4);
            this.weighboxlayout.Name = "weighboxlayout";
            this.weighboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.weighboxlayout.RowCount = 2;
            this.weighboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.weighboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86F));
            this.weighboxlayout.Size = new System.Drawing.Size(464, 349);
            this.weighboxlayout.TabIndex = 109;
            this.weighboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.weighboxlayout_Paint);
            // 
            // weighboxpanel
            // 
            this.weighboxpanel.Controls.Add(this.grosswterror);
            this.weighboxpanel.Controls.Add(this.palletwterror);
            this.weighboxpanel.Controls.Add(this.spoolwterror);
            this.weighboxpanel.Controls.Add(this.spoolnoerror);
            this.weighboxpanel.Controls.Add(this.scalemodel);
            this.weighboxpanel.Controls.Add(this.WeighingList);
            this.weighboxpanel.Controls.Add(this.comport);
            this.weighboxpanel.Controls.Add(this.ComPortList);
            this.weighboxpanel.Controls.Add(this.wtpercop);
            this.weighboxpanel.Controls.Add(this.spool);
            this.weighboxpanel.Controls.Add(this.label5);
            this.weighboxpanel.Controls.Add(this.spoolno);
            this.weighboxpanel.Controls.Add(this.netwt);
            this.weighboxpanel.Controls.Add(this.spoolwt);
            this.weighboxpanel.Controls.Add(this.label4);
            this.weighboxpanel.Controls.Add(this.palletwt);
            this.weighboxpanel.Controls.Add(this.tarewt);
            this.weighboxpanel.Controls.Add(this.palletwtno);
            this.weighboxpanel.Controls.Add(this.label3);
            this.weighboxpanel.Controls.Add(this.label2);
            this.weighboxpanel.Controls.Add(this.grosswtno);
            this.weighboxpanel.Location = new System.Drawing.Point(5, 53);
            this.weighboxpanel.Name = "weighboxpanel";
            this.weighboxpanel.Size = new System.Drawing.Size(454, 291);
            this.weighboxpanel.TabIndex = 107;
            // 
            // grosswterror
            // 
            this.grosswterror.AutoSize = true;
            this.grosswterror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.grosswterror.ForeColor = System.Drawing.Color.Red;
            this.grosswterror.Location = new System.Drawing.Point(10, 122);
            this.grosswterror.Name = "grosswterror";
            this.grosswterror.Size = new System.Drawing.Size(0, 20);
            this.grosswterror.TabIndex = 89;
            this.grosswterror.Visible = false;
            // 
            // palletwterror
            // 
            this.palletwterror.AutoSize = true;
            this.palletwterror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.palletwterror.ForeColor = System.Drawing.Color.Red;
            this.palletwterror.Location = new System.Drawing.Point(10, 78);
            this.palletwterror.Name = "palletwterror";
            this.palletwterror.Size = new System.Drawing.Size(0, 20);
            this.palletwterror.TabIndex = 88;
            this.palletwterror.Visible = false;
            // 
            // spoolwterror
            // 
            this.spoolwterror.AutoSize = true;
            this.spoolwterror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.spoolwterror.ForeColor = System.Drawing.Color.Red;
            this.spoolwterror.Location = new System.Drawing.Point(96, 38);
            this.spoolwterror.Name = "spoolwterror";
            this.spoolwterror.Size = new System.Drawing.Size(0, 20);
            this.spoolwterror.TabIndex = 87;
            this.spoolwterror.Visible = false;
            // 
            // spoolnoerror
            // 
            this.spoolnoerror.AutoSize = true;
            this.spoolnoerror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.spoolnoerror.ForeColor = System.Drawing.Color.Red;
            this.spoolnoerror.Location = new System.Drawing.Point(10, 38);
            this.spoolnoerror.Name = "spoolnoerror";
            this.spoolnoerror.Size = new System.Drawing.Size(0, 20);
            this.spoolnoerror.TabIndex = 86;
            this.spoolnoerror.Visible = false;
            // 
            // weighboxheader
            // 
            this.weighboxheader.Controls.Add(this.Weighboxlbl);
            this.weighboxheader.Location = new System.Drawing.Point(5, 5);
            this.weighboxheader.Name = "weighboxheader";
            this.weighboxheader.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.weighboxheader.Size = new System.Drawing.Size(454, 42);
            this.weighboxheader.TabIndex = 107;
            this.weighboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.weighboxheader_Paint);
            // 
            // Weighboxlbl
            // 
            this.Weighboxlbl.AutoSize = true;
            this.Weighboxlbl.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Weighboxlbl.Location = new System.Drawing.Point(3, 12);
            this.Weighboxlbl.Name = "Weighboxlbl";
            this.Weighboxlbl.Size = new System.Drawing.Size(143, 20);
            this.Weighboxlbl.TabIndex = 107;
            this.Weighboxlbl.Text = "Weigh and Label";
            // 
            // packagingboxlayout
            // 
            this.packagingboxlayout.ColumnCount = 1;
            this.packagingboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.packagingboxlayout.Controls.Add(this.packagingboxpanel, 0, 1);
            this.packagingboxlayout.Controls.Add(this.packagingboxheader, 0, 0);
            this.packagingboxlayout.Location = new System.Drawing.Point(506, 367);
            this.packagingboxlayout.Name = "packagingboxlayout";
            this.packagingboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.packagingboxlayout.RowCount = 2;
            this.packagingboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.packagingboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.packagingboxlayout.Size = new System.Drawing.Size(464, 249);
            this.packagingboxlayout.TabIndex = 108;
            this.packagingboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.packagingboxlayout_Paint);
            // 
            // packagingboxpanel
            // 
            this.packagingboxpanel.Controls.Add(this.packsize);
            this.packagingboxpanel.Controls.Add(this.PackSizeList);
            this.packagingboxpanel.Controls.Add(this.frdenier);
            this.packagingboxpanel.Controls.Add(this.updenier);
            this.packagingboxpanel.Controls.Add(this.copssize);
            this.packagingboxpanel.Controls.Add(this.CopsItemList);
            this.packagingboxpanel.Controls.Add(this.boxtype);
            this.packagingboxpanel.Controls.Add(this.BoxItemList);
            this.packagingboxpanel.Controls.Add(this.boxweight);
            this.packagingboxpanel.Controls.Add(this.textBox3);
            this.packagingboxpanel.Controls.Add(this.boxstock);
            this.packagingboxpanel.Controls.Add(this.textBox4);
            this.packagingboxpanel.Controls.Add(this.copweight);
            this.packagingboxpanel.Controls.Add(this.textBox1);
            this.packagingboxpanel.Controls.Add(this.copstock);
            this.packagingboxpanel.Controls.Add(this.textBox2);
            this.packagingboxpanel.Location = new System.Drawing.Point(5, 49);
            this.packagingboxpanel.Name = "packagingboxpanel";
            this.packagingboxpanel.Size = new System.Drawing.Size(454, 194);
            this.packagingboxpanel.TabIndex = 107;
            // 
            // packagingboxheader
            // 
            this.packagingboxheader.Controls.Add(this.Packagingboxlbl);
            this.packagingboxheader.Location = new System.Drawing.Point(5, 5);
            this.packagingboxheader.Name = "packagingboxheader";
            this.packagingboxheader.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.packagingboxheader.Size = new System.Drawing.Size(454, 38);
            this.packagingboxheader.TabIndex = 107;
            this.packagingboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.packagingboxheader_Paint);
            // 
            // Packagingboxlbl
            // 
            this.Packagingboxlbl.AutoSize = true;
            this.Packagingboxlbl.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Packagingboxlbl.Location = new System.Drawing.Point(6, 9);
            this.Packagingboxlbl.Name = "Packagingboxlbl";
            this.Packagingboxlbl.Size = new System.Drawing.Size(92, 20);
            this.Packagingboxlbl.TabIndex = 107;
            this.Packagingboxlbl.Text = "Packaging";
            // 
            // orderboxlayout
            // 
            this.orderboxlayout.ColumnCount = 1;
            this.orderboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.orderboxlayout.Controls.Add(this.orderboxpanel, 0, 1);
            this.orderboxlayout.Controls.Add(this.orderboxheader, 0, 0);
            this.orderboxlayout.Location = new System.Drawing.Point(13, 367);
            this.orderboxlayout.Name = "orderboxlayout";
            this.orderboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.orderboxlayout.RowCount = 2;
            this.orderboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.orderboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.orderboxlayout.Size = new System.Drawing.Size(464, 249);
            this.orderboxlayout.TabIndex = 107;
            this.orderboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.orderboxlayout_Paint);
            // 
            // orderboxpanel
            // 
            this.orderboxpanel.Controls.Add(this.quality);
            this.orderboxpanel.Controls.Add(this.QualityList);
            this.orderboxpanel.Controls.Add(this.saleorderno);
            this.orderboxpanel.Controls.Add(this.SaleOrderList);
            this.orderboxpanel.Controls.Add(this.windingtype);
            this.orderboxpanel.Controls.Add(this.WindingTypeList);
            this.orderboxpanel.Controls.Add(this.productiontype);
            this.orderboxpanel.Controls.Add(this.prodtype);
            this.orderboxpanel.Controls.Add(this.remark);
            this.orderboxpanel.Controls.Add(this.remarks);
            this.orderboxpanel.Controls.Add(this.label1);
            this.orderboxpanel.Controls.Add(this.copyno);
            this.orderboxpanel.Location = new System.Drawing.Point(5, 49);
            this.orderboxpanel.Name = "orderboxpanel";
            this.orderboxpanel.Size = new System.Drawing.Size(454, 194);
            this.orderboxpanel.TabIndex = 107;
            // 
            // orderboxheader
            // 
            this.orderboxheader.Controls.Add(this.Orderboxlbl);
            this.orderboxheader.Location = new System.Drawing.Point(5, 5);
            this.orderboxheader.Name = "orderboxheader";
            this.orderboxheader.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.orderboxheader.Size = new System.Drawing.Size(454, 38);
            this.orderboxheader.TabIndex = 107;
            this.orderboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.orderboxheader_Paint);
            // 
            // Orderboxlbl
            // 
            this.Orderboxlbl.AutoSize = true;
            this.Orderboxlbl.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Orderboxlbl.Location = new System.Drawing.Point(6, 9);
            this.Orderboxlbl.Name = "Orderboxlbl";
            this.Orderboxlbl.Size = new System.Drawing.Size(115, 20);
            this.Orderboxlbl.TabIndex = 107;
            this.Orderboxlbl.Text = "Order Details";
            // 
            // cancelbtn
            // 
            this.cancelbtn.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.cancelbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.cancelbtn.ForeColor = System.Drawing.SystemColors.Control;
            this.cancelbtn.Location = new System.Drawing.Point(365, 1082);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 28);
            this.cancelbtn.TabIndex = 106;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = false;
            this.cancelbtn.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // boxnoerror
            // 
            this.boxnoerror.AutoSize = true;
            this.boxnoerror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxnoerror.ForeColor = System.Drawing.Color.Red;
            this.boxnoerror.Location = new System.Drawing.Point(286, 71);
            this.boxnoerror.Name = "boxnoerror";
            this.boxnoerror.Size = new System.Drawing.Size(0, 20);
            this.boxnoerror.TabIndex = 105;
            // 
            // windingerror
            // 
            this.windingerror.AutoSize = true;
            this.windingerror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.windingerror.ForeColor = System.Drawing.Color.Red;
            this.windingerror.Location = new System.Drawing.Point(291, 267);
            this.windingerror.Name = "windingerror";
            this.windingerror.Size = new System.Drawing.Size(0, 20);
            this.windingerror.TabIndex = 104;
            this.windingerror.Visible = false;
            // 
            // packsizeerror
            // 
            this.packsizeerror.AutoSize = true;
            this.packsizeerror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.packsizeerror.ForeColor = System.Drawing.Color.Red;
            this.packsizeerror.Location = new System.Drawing.Point(65, 267);
            this.packsizeerror.Name = "packsizeerror";
            this.packsizeerror.Size = new System.Drawing.Size(0, 20);
            this.packsizeerror.TabIndex = 103;
            this.packsizeerror.Visible = false;
            // 
            // soerror
            // 
            this.soerror.AutoSize = true;
            this.soerror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.soerror.ForeColor = System.Drawing.Color.Red;
            this.soerror.Location = new System.Drawing.Point(286, 224);
            this.soerror.Name = "soerror";
            this.soerror.Size = new System.Drawing.Size(0, 20);
            this.soerror.TabIndex = 102;
            // 
            // qualityerror
            // 
            this.qualityerror.AutoSize = true;
            this.qualityerror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.qualityerror.ForeColor = System.Drawing.Color.Red;
            this.qualityerror.Location = new System.Drawing.Point(65, 224);
            this.qualityerror.Name = "qualityerror";
            this.qualityerror.Size = new System.Drawing.Size(0, 20);
            this.qualityerror.TabIndex = 101;
            this.qualityerror.Visible = false;
            // 
            // mergenoerror
            // 
            this.mergenoerror.AutoSize = true;
            this.mergenoerror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.mergenoerror.ForeColor = System.Drawing.Color.Red;
            this.mergenoerror.Location = new System.Drawing.Point(65, 109);
            this.mergenoerror.Name = "mergenoerror";
            this.mergenoerror.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mergenoerror.Size = new System.Drawing.Size(0, 20);
            this.mergenoerror.TabIndex = 100;
            this.mergenoerror.Visible = false;
            // 
            // copynoerror
            // 
            this.copynoerror.AutoSize = true;
            this.copynoerror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.copynoerror.ForeColor = System.Drawing.Color.Red;
            this.copynoerror.Location = new System.Drawing.Point(440, 208);
            this.copynoerror.Name = "copynoerror";
            this.copynoerror.Size = new System.Drawing.Size(0, 20);
            this.copynoerror.TabIndex = 99;
            this.copynoerror.Visible = false;
            // 
            // linenoerror
            // 
            this.linenoerror.AutoSize = true;
            this.linenoerror.BackColor = System.Drawing.SystemColors.Control;
            this.linenoerror.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.linenoerror.ForeColor = System.Drawing.Color.Red;
            this.linenoerror.Location = new System.Drawing.Point(65, 31);
            this.linenoerror.Name = "linenoerror";
            this.linenoerror.Size = new System.Drawing.Size(0, 20);
            this.linenoerror.TabIndex = 98;
            this.linenoerror.Visible = false;
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
            // poyformlabel
            // 
            this.poyformlabel.AutoSize = true;
            this.poyformlabel.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.poyformlabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.poyformlabel.Location = new System.Drawing.Point(18, 7);
            this.poyformlabel.Name = "poyformlabel";
            this.poyformlabel.Size = new System.Drawing.Size(113, 20);
            this.poyformlabel.TabIndex = 97;
            this.poyformlabel.Text = "POY Packing";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.leftpanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rightpanel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1312, 1050);
            this.tableLayoutPanel1.TabIndex = 98;
            // 
            // leftpanel
            // 
            this.leftpanel.AutoScroll = true;
            this.leftpanel.Controls.Add(this.reviewtable);
            this.leftpanel.Controls.Add(this.weighttable);
            this.leftpanel.Controls.Add(this.packagingtable);
            this.leftpanel.Controls.Add(this.ordertable);
            this.leftpanel.Location = new System.Drawing.Point(3, 3);
            this.leftpanel.Name = "leftpanel";
            this.leftpanel.Size = new System.Drawing.Size(252, 1044);
            this.leftpanel.TabIndex = 107;
            // 
            // reviewtable
            // 
            this.reviewtable.ColumnCount = 2;
            this.reviewtable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.12384F));
            this.reviewtable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.87616F));
            this.reviewtable.Controls.Add(this.panel4, 1, 0);
            this.reviewtable.Controls.Add(this.reviewdtls, 0, 0);
            this.reviewtable.Location = new System.Drawing.Point(9, 268);
            this.reviewtable.Name = "reviewtable";
            this.reviewtable.Padding = new System.Windows.Forms.Padding(5);
            this.reviewtable.RowCount = 1;
            this.reviewtable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.reviewtable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.reviewtable.Size = new System.Drawing.Size(230, 66);
            this.reviewtable.TabIndex = 110;
            this.reviewtable.Paint += new System.Windows.Forms.PaintEventHandler(this.reviewtable_Paint);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.reviewlbl);
            this.panel4.Controls.Add(this.reviewsubtitle);
            this.panel4.Location = new System.Drawing.Point(52, 8);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(170, 49);
            this.panel4.TabIndex = 0;
            // 
            // reviewlbl
            // 
            this.reviewlbl.AutoSize = true;
            this.reviewlbl.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.reviewlbl.Location = new System.Drawing.Point(3, 4);
            this.reviewlbl.Name = "reviewlbl";
            this.reviewlbl.Size = new System.Drawing.Size(149, 20);
            this.reviewlbl.TabIndex = 0;
            this.reviewlbl.Text = "Review && Submit ";
            this.reviewlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // reviewsubtitle
            // 
            this.reviewsubtitle.AutoSize = true;
            this.reviewsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.reviewsubtitle.Location = new System.Drawing.Point(3, 27);
            this.reviewsubtitle.Name = "reviewsubtitle";
            this.reviewsubtitle.Size = new System.Drawing.Size(96, 20);
            this.reviewsubtitle.TabIndex = 0;
            this.reviewsubtitle.Text = "Status, Item";
            // 
            // reviewdtls
            // 
            this.reviewdtls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reviewdtls.Image = global::PackingApplication.Properties.Resources.icons8_circled_4_48;
            this.reviewdtls.Location = new System.Drawing.Point(8, 8);
            this.reviewdtls.Name = "reviewdtls";
            this.reviewdtls.Size = new System.Drawing.Size(38, 50);
            this.reviewdtls.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.reviewdtls.TabIndex = 107;
            this.reviewdtls.TabStop = false;
            // 
            // weighttable
            // 
            this.weighttable.ColumnCount = 2;
            this.weighttable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.12384F));
            this.weighttable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.87616F));
            this.weighttable.Controls.Add(this.panel2, 1, 0);
            this.weighttable.Controls.Add(this.weightdtls, 0, 0);
            this.weighttable.Location = new System.Drawing.Point(10, 179);
            this.weighttable.Name = "weighttable";
            this.weighttable.Padding = new System.Windows.Forms.Padding(5);
            this.weighttable.RowCount = 1;
            this.weighttable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.weighttable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.weighttable.Size = new System.Drawing.Size(230, 66);
            this.weighttable.TabIndex = 109;
            this.weighttable.Paint += new System.Windows.Forms.PaintEventHandler(this.weightable_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.weighlbl);
            this.panel2.Controls.Add(this.weighsubtitle);
            this.panel2.Location = new System.Drawing.Point(52, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(170, 49);
            this.panel2.TabIndex = 0;
            // 
            // weighlbl
            // 
            this.weighlbl.AutoSize = true;
            this.weighlbl.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.weighlbl.Location = new System.Drawing.Point(3, 4);
            this.weighlbl.Name = "weighlbl";
            this.weighlbl.Size = new System.Drawing.Size(125, 20);
            this.weighlbl.TabIndex = 0;
            this.weighlbl.Text = "Weigh && Label";
            this.weighlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weighsubtitle
            // 
            this.weighsubtitle.AutoSize = true;
            this.weighsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.weighsubtitle.Location = new System.Drawing.Point(3, 27);
            this.weighsubtitle.Name = "weighsubtitle";
            this.weighsubtitle.Size = new System.Drawing.Size(138, 20);
            this.weighsubtitle.TabIndex = 0;
            this.weighsubtitle.Text = "Scale, QR, Copies";
            // 
            // weightdtls
            // 
            this.weightdtls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.weightdtls.Image = global::PackingApplication.Properties.Resources.icons8_circled_3_48;
            this.weightdtls.Location = new System.Drawing.Point(8, 8);
            this.weightdtls.Name = "weightdtls";
            this.weightdtls.Size = new System.Drawing.Size(38, 50);
            this.weightdtls.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.weightdtls.TabIndex = 107;
            this.weightdtls.TabStop = false;
            // 
            // packagingtable
            // 
            this.packagingtable.ColumnCount = 2;
            this.packagingtable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.12384F));
            this.packagingtable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.87616F));
            this.packagingtable.Controls.Add(this.panel1, 1, 0);
            this.packagingtable.Controls.Add(this.packagingdtls, 0, 0);
            this.packagingtable.Location = new System.Drawing.Point(8, 89);
            this.packagingtable.Name = "packagingtable";
            this.packagingtable.Padding = new System.Windows.Forms.Padding(5);
            this.packagingtable.RowCount = 1;
            this.packagingtable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.packagingtable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.packagingtable.Size = new System.Drawing.Size(230, 66);
            this.packagingtable.TabIndex = 108;
            this.packagingtable.Paint += new System.Windows.Forms.PaintEventHandler(this.packagingtable_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.packaginglbl);
            this.panel1.Controls.Add(this.packagingsubtitle);
            this.panel1.Location = new System.Drawing.Point(52, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(170, 49);
            this.panel1.TabIndex = 0;
            // 
            // packaginglbl
            // 
            this.packaginglbl.AutoSize = true;
            this.packaginglbl.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.packaginglbl.Location = new System.Drawing.Point(3, 4);
            this.packaginglbl.Name = "packaginglbl";
            this.packaginglbl.Size = new System.Drawing.Size(92, 20);
            this.packaginglbl.TabIndex = 0;
            this.packaginglbl.Text = "Packaging";
            this.packaginglbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // packagingsubtitle
            // 
            this.packagingsubtitle.AutoSize = true;
            this.packagingsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.packagingsubtitle.Location = new System.Drawing.Point(3, 27);
            this.packagingsubtitle.Name = "packagingsubtitle";
            this.packagingsubtitle.Size = new System.Drawing.Size(167, 20);
            this.packagingsubtitle.TabIndex = 0;
            this.packagingsubtitle.Text = "Pack, Cops, Box/Pallet";
            // 
            // packagingdtls
            // 
            this.packagingdtls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.packagingdtls.Image = global::PackingApplication.Properties.Resources.icons8_circled_2_48;
            this.packagingdtls.Location = new System.Drawing.Point(8, 8);
            this.packagingdtls.Name = "packagingdtls";
            this.packagingdtls.Size = new System.Drawing.Size(38, 50);
            this.packagingdtls.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.packagingdtls.TabIndex = 107;
            this.packagingdtls.TabStop = false;
            // 
            // ordertable
            // 
            this.ordertable.ColumnCount = 2;
            this.ordertable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.12384F));
            this.ordertable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.87616F));
            this.ordertable.Controls.Add(this.orderdetailsrightpanel, 1, 0);
            this.ordertable.Controls.Add(this.orderdetails1, 0, 0);
            this.ordertable.Location = new System.Drawing.Point(9, 0);
            this.ordertable.Name = "ordertable";
            this.ordertable.Padding = new System.Windows.Forms.Padding(5);
            this.ordertable.RowCount = 1;
            this.ordertable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ordertable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.ordertable.Size = new System.Drawing.Size(230, 66);
            this.ordertable.TabIndex = 107;
            this.ordertable.Paint += new System.Windows.Forms.PaintEventHandler(this.ordertable_Paint);
            // 
            // orderdetailsrightpanel
            // 
            this.orderdetailsrightpanel.Controls.Add(this.orderlbl);
            this.orderdetailsrightpanel.Controls.Add(this.orderdetailssubtitle);
            this.orderdetailsrightpanel.Location = new System.Drawing.Point(52, 8);
            this.orderdetailsrightpanel.Name = "orderdetailsrightpanel";
            this.orderdetailsrightpanel.Size = new System.Drawing.Size(170, 49);
            this.orderdetailsrightpanel.TabIndex = 0;
            // 
            // orderlbl
            // 
            this.orderlbl.AutoSize = true;
            this.orderlbl.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.orderlbl.Location = new System.Drawing.Point(3, 4);
            this.orderlbl.Name = "orderlbl";
            this.orderlbl.Size = new System.Drawing.Size(112, 20);
            this.orderlbl.TabIndex = 0;
            this.orderlbl.Text = "Order details";
            this.orderlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // orderdetailssubtitle
            // 
            this.orderdetailssubtitle.AutoSize = true;
            this.orderdetailssubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.orderdetailssubtitle.Location = new System.Drawing.Point(3, 27);
            this.orderdetailssubtitle.Name = "orderdetailssubtitle";
            this.orderdetailssubtitle.Size = new System.Drawing.Size(176, 20);
            this.orderdetailssubtitle.TabIndex = 0;
            this.orderdetailssubtitle.Text = "Shade, SO No, Winding";
            // 
            // orderdetails1
            // 
            this.orderdetails1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.orderdetails1.Image = global::PackingApplication.Properties.Resources.icons8_one_48;
            this.orderdetails1.Location = new System.Drawing.Point(8, 8);
            this.orderdetails1.Name = "orderdetails1";
            this.orderdetails1.Size = new System.Drawing.Size(38, 50);
            this.orderdetails1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.orderdetails1.TabIndex = 107;
            this.orderdetails1.TabStop = false;
            // 
            // POYPackingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1312, 1050);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.poyformlabel);
            this.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "POYPackingForm";
            this.Text = "POYPackingForm";
            this.Load += new System.EventHandler(this.POYPackingForm_Load);
            this.rightpanel.ResumeLayout(false);
            this.rightpanel.PerformLayout();
            this.palletdetailslayout.ResumeLayout(false);
            this.palletdetailsheader.ResumeLayout(false);
            this.palletdetailsheader.PerformLayout();
            this.palletdetailspanel.ResumeLayout(false);
            this.palletdetailspanel.PerformLayout();
            this.printingdetailslayout.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.printingdetailsheader.ResumeLayout(false);
            this.printingdetailsheader.PerformLayout();
            this.lastboxlayout.ResumeLayout(false);
            this.lastboxpanel.ResumeLayout(false);
            this.wgroupbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.windinggrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.windingqty)).EndInit();
            this.lastbxnetwtpanel.ResumeLayout(false);
            this.lastbxnetwtpanel.PerformLayout();
            this.lastbxgrosswtpanel.ResumeLayout(false);
            this.lastbxgrosswtpanel.PerformLayout();
            this.lastbxtarepanel.ResumeLayout(false);
            this.lastbxtarepanel.PerformLayout();
            this.lastbxcopspanel.ResumeLayout(false);
            this.lastbxcopspanel.PerformLayout();
            this.gradewiseprodn.ResumeLayout(false);
            this.gradewiseprodn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualityqty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityandqty)).EndInit();
            this.lastboxheader.ResumeLayout(false);
            this.lastboxheader.PerformLayout();
            this.machineboxlayout.ResumeLayout(false);
            this.machineboxpanel.ResumeLayout(false);
            this.machineboxpanel.PerformLayout();
            this.machineboxheader.ResumeLayout(false);
            this.machineboxheader.PerformLayout();
            this.weighboxlayout.ResumeLayout(false);
            this.weighboxpanel.ResumeLayout(false);
            this.weighboxpanel.PerformLayout();
            this.weighboxheader.ResumeLayout(false);
            this.weighboxheader.PerformLayout();
            this.packagingboxlayout.ResumeLayout(false);
            this.packagingboxpanel.ResumeLayout(false);
            this.packagingboxpanel.PerformLayout();
            this.packagingboxheader.ResumeLayout(false);
            this.packagingboxheader.PerformLayout();
            this.orderboxlayout.ResumeLayout(false);
            this.orderboxpanel.ResumeLayout(false);
            this.orderboxpanel.PerformLayout();
            this.orderboxheader.ResumeLayout(false);
            this.orderboxheader.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.leftpanel.ResumeLayout(false);
            this.reviewtable.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reviewdtls)).EndInit();
            this.weighttable.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weightdtls)).EndInit();
            this.packagingtable.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packagingdtls)).EndInit();
            this.ordertable.ResumeLayout(false);
            this.orderdetailsrightpanel.ResumeLayout(false);
            this.orderdetailsrightpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdetails1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lineno;
        private System.Windows.Forms.Label department;
        private System.Windows.Forms.Label mergeno;
        private System.Windows.Forms.Label lastboxno;
        private System.Windows.Forms.TextBox lastbox;
        private System.Windows.Forms.Label item;
        private System.Windows.Forms.Label shade;
        private System.Windows.Forms.Label shadecode;
        private System.Windows.Forms.Label boxno;
        private System.Windows.Forms.Label packingdate;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label quality;
        private System.Windows.Forms.Label saleorderno;
        private System.Windows.Forms.Label packsize;
        private System.Windows.Forms.TextBox frdenier;
        private System.Windows.Forms.TextBox updenier;
        private System.Windows.Forms.Label windingtype;
        private System.Windows.Forms.Label comport;
        private System.Windows.Forms.Label copssize;
        private System.Windows.Forms.Label copweight;
        private System.Windows.Forms.Label copstock;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label boxtype;
        private System.Windows.Forms.Label boxweight;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label boxstock;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label productiontype;
        private System.Windows.Forms.Label remark;
        private System.Windows.Forms.TextBox remarks;
        private System.Windows.Forms.Label scalemodel;
        private System.Windows.Forms.ComboBox LineNoList;
        private System.Windows.Forms.TextBox departmentname;
        private System.Windows.Forms.ComboBox MergeNoList;
        private System.Windows.Forms.TextBox itemname;
        private System.Windows.Forms.TextBox shadename;
        private System.Windows.Forms.TextBox shadecd;
        private System.Windows.Forms.ComboBox QualityList;
        private System.Windows.Forms.ComboBox PackSizeList;
        private System.Windows.Forms.ComboBox WindingTypeList;
        private System.Windows.Forms.ComboBox ComPortList;
        private System.Windows.Forms.ComboBox WeighingList;
        private System.Windows.Forms.ComboBox CopsItemList;
        private System.Windows.Forms.ComboBox BoxItemList;
        private System.Windows.Forms.ComboBox SaleOrderList;
        private System.Windows.Forms.CheckBox prcompany;
        private System.Windows.Forms.CheckBox prowner;
        private System.Windows.Forms.CheckBox prdate;
        private System.Windows.Forms.CheckBox pruser;
        private System.Windows.Forms.CheckBox prhindi;
        private System.Windows.Forms.CheckBox prwtps;
        private System.Windows.Forms.CheckBox prqrcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox copyno;
        private System.Windows.Forms.Label spool;
        private System.Windows.Forms.TextBox spoolno;
        private System.Windows.Forms.Label palletwt;
        private System.Windows.Forms.TextBox spoolwt;
        private System.Windows.Forms.TextBox palletwtno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox grosswtno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox netwt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tarewt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox wtpercop;
        private System.Windows.Forms.TextBox prodtype;
        private System.Windows.Forms.Label palletdetails;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox PalletTypeList;
        private System.Windows.Forms.Label pquantity;
        private System.Windows.Forms.TextBox qnty;
        private System.Windows.Forms.Button addqty;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button submit;
        //private System.Windows.Forms.Button Logout;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.Label role;
        private Panel rightpanel;
        private ComboBox PrefixList;
        private Panel panel3;
        private DataGridView qualityqty;
        private DataGridViewTextBoxColumn qualityDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn prodQtyDataGridViewTextBoxColumn;
        private DataTable windingqty;
        private DataTable qualityandqty;
        private DataGridViewTextBoxColumn windingtypeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn soqtyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn windingprodqtyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn windingbalqtyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn windingTypeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn saleOrderQtyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn balanceQtyDataGridViewTextBoxColumn;
        private GroupBox gradewiseprodn;
        private System.Windows.Forms.Label saleordrqty;
        private System.Windows.Forms.Label totalprodbalqty;
        private DataGridView windinggrid;
        private GroupBox wgroupbox;
        private TextBox deniervalue;
        private System.Windows.Forms.Label denier;
        //private Button backbutton;
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
        private System.Windows.Forms.Label poyformlabel;
        private Button cancelbtn;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel leftpanel;
        private TableLayoutPanel ordertable;
        private PictureBox orderdetails1;
        private System.Windows.Forms.Label orderlbl;
        private System.Windows.Forms.Label orderdetailssubtitle;
        private Panel orderdetailsrightpanel;
        private TableLayoutPanel packagingtable;
        private Panel panel1;
        private System.Windows.Forms.Label packaginglbl;
        private System.Windows.Forms.Label packagingsubtitle;
        private PictureBox packagingdtls;
        private TableLayoutPanel weighttable;
        private Panel panel2;
        private System.Windows.Forms.Label weighlbl;
        private System.Windows.Forms.Label weighsubtitle;
        private PictureBox weightdtls;
        private TableLayoutPanel reviewtable;
        private Panel panel4;
        private System.Windows.Forms.Label reviewlbl;
        private System.Windows.Forms.Label reviewsubtitle;
        private PictureBox reviewdtls;
        private Panel orderboxheader;
        private System.Windows.Forms.Label Orderboxlbl;
        private TableLayoutPanel orderboxlayout;
        private Panel orderboxpanel;
        private TableLayoutPanel packagingboxlayout;
        private Panel packagingboxpanel;
        private Panel packagingboxheader;
        private System.Windows.Forms.Label Packagingboxlbl;
        private TableLayoutPanel weighboxlayout;
        private Panel weighboxpanel;
        private Panel weighboxheader;
        private System.Windows.Forms.Label Weighboxlbl;
        private TableLayoutPanel machineboxlayout;
        private Panel machineboxpanel;
        private Panel machineboxheader;
        private System.Windows.Forms.Label Machinelbl;
        private TableLayoutPanel lastboxlayout;
        private Panel lastboxpanel;
        private TextBox copstxtbox;
        private System.Windows.Forms.Label cops;
        private Panel lastboxheader;
        private System.Windows.Forms.Label Lastboxlbl;
        private Panel lastbxcopspanel;
        private Panel lastbxtarepanel;
        private TextBox tarewghttxtbox;
        private System.Windows.Forms.Label tareweight;
        private Panel lastbxnetwtpanel;
        private TextBox netwttxtbox;
        private System.Windows.Forms.Label netweight;
        private Panel lastbxgrosswtpanel;
        private TextBox grosswttxtbox;
        private System.Windows.Forms.Label grossweight;
        private TableLayoutPanel printingdetailslayout;
        private Panel printingdetailsheader;
        private System.Windows.Forms.Label Printinglbl;
        private TableLayoutPanel palletdetailslayout;
        private Panel palletdetailsheader;
        private Panel palletdetailspanel;
    }
}

