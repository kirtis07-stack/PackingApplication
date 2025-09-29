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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.copsitemwt = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.boxtype = new System.Windows.Forms.Label();
            this.boxweight = new System.Windows.Forms.Label();
            this.boxpalletitemwt = new System.Windows.Forms.TextBox();
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
            this.gradewiseprodn = new System.Windows.Forms.GroupBox();
            this.prodnbalqty = new System.Windows.Forms.Label();
            this.grdsoqty = new System.Windows.Forms.Label();
            this.qualityqty = new System.Windows.Forms.DataGridView();
            this.qualityandqty = new System.Data.DataTable();
            this.totalprodbalqty = new System.Windows.Forms.Label();
            this.saleordrqty = new System.Windows.Forms.Label();
            this.wgroupbox = new System.Windows.Forms.GroupBox();
            this.windinggrid = new System.Windows.Forms.DataGridView();
            this.windingqty = new System.Data.DataTable();
            this.saveprint = new System.Windows.Forms.Button();
            this.rowMaterialBox = new System.Windows.Forms.GroupBox();
            this.rowMaterialPanel = new System.Windows.Forms.Panel();
            this.rowMaterial = new System.Windows.Forms.DataGridView();
            this.palletdetailslayout = new System.Windows.Forms.TableLayoutPanel();
            this.palletdetailsheader = new System.Windows.Forms.Panel();
            this.palletdetailspanel = new System.Windows.Forms.Panel();
            this.printingdetailslayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.printingdetailsheader = new System.Windows.Forms.Panel();
            this.Printinglbl = new System.Windows.Forms.Label();
            this.lastboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.lastboxpanel = new System.Windows.Forms.Panel();
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
            this.lastboxheader = new System.Windows.Forms.Panel();
            this.Lastboxlbl = new System.Windows.Forms.Label();
            this.machineboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.machineboxpanel = new System.Windows.Forms.Panel();
            this.req3 = new System.Windows.Forms.Label();
            this.req2 = new System.Windows.Forms.Label();
            this.req1 = new System.Windows.Forms.Label();
            this.deniervalue = new System.Windows.Forms.TextBox();
            this.denier = new System.Windows.Forms.Label();
            this.linenoerror = new System.Windows.Forms.Label();
            this.boxnoerror = new System.Windows.Forms.Label();
            this.mergenoerror = new System.Windows.Forms.Label();
            this.PrefixList = new System.Windows.Forms.ComboBox();
            this.machineboxheader = new System.Windows.Forms.Panel();
            this.Machinelbl = new System.Windows.Forms.Label();
            this.weighboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.weighboxpanel = new System.Windows.Forms.Panel();
            this.spoolweight = new System.Windows.Forms.Label();
            this.req10 = new System.Windows.Forms.Label();
            this.req9 = new System.Windows.Forms.Label();
            this.req8 = new System.Windows.Forms.Label();
            this.req7 = new System.Windows.Forms.Label();
            this.grosswterror = new System.Windows.Forms.Label();
            this.palletwterror = new System.Windows.Forms.Label();
            this.spoolwterror = new System.Windows.Forms.Label();
            this.spoolnoerror = new System.Windows.Forms.Label();
            this.windingerror = new System.Windows.Forms.Label();
            this.weighboxheader = new System.Windows.Forms.Panel();
            this.Weighboxlbl = new System.Windows.Forms.Label();
            this.packagingboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.packagingboxheader = new System.Windows.Forms.Panel();
            this.Packagingboxlbl = new System.Windows.Forms.Label();
            this.packagingboxpanel = new System.Windows.Forms.Panel();
            this.uptodenier = new System.Windows.Forms.Label();
            this.fromdenier = new System.Windows.Forms.Label();
            this.req6 = new System.Windows.Forms.Label();
            this.req5 = new System.Windows.Forms.Label();
            this.req4 = new System.Windows.Forms.Label();
            this.packsizeerror = new System.Windows.Forms.Label();
            this.soerror = new System.Windows.Forms.Label();
            this.copynoerror = new System.Windows.Forms.Label();
            this.qualityerror = new System.Windows.Forms.Label();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.windingtypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soqtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.windingbalqtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.windingprodqtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.menu = new System.Windows.Forms.Label();
            this.menuBtn = new System.Windows.Forms.PictureBox();
            this.sidebarTimer = new System.Windows.Forms.Timer(this.components);
            this.rightpanel.SuspendLayout();
            this.gradewiseprodn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualityqty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityandqty)).BeginInit();
            this.wgroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windinggrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.windingqty)).BeginInit();
            this.rowMaterialBox.SuspendLayout();
            this.rowMaterialPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rowMaterial)).BeginInit();
            this.palletdetailslayout.SuspendLayout();
            this.palletdetailsheader.SuspendLayout();
            this.palletdetailspanel.SuspendLayout();
            this.printingdetailslayout.SuspendLayout();
            this.panel3.SuspendLayout();
            this.printingdetailsheader.SuspendLayout();
            this.lastboxlayout.SuspendLayout();
            this.lastboxpanel.SuspendLayout();
            this.lastbxnetwtpanel.SuspendLayout();
            this.lastbxgrosswtpanel.SuspendLayout();
            this.lastbxtarepanel.SuspendLayout();
            this.lastbxcopspanel.SuspendLayout();
            this.lastboxheader.SuspendLayout();
            this.machineboxlayout.SuspendLayout();
            this.machineboxpanel.SuspendLayout();
            this.machineboxheader.SuspendLayout();
            this.weighboxlayout.SuspendLayout();
            this.weighboxpanel.SuspendLayout();
            this.weighboxheader.SuspendLayout();
            this.packagingboxlayout.SuspendLayout();
            this.packagingboxheader.SuspendLayout();
            this.packagingboxpanel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // lineno
            // 
            this.lineno.AutoSize = true;
            this.lineno.Location = new System.Drawing.Point(2, 2);
            this.lineno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lineno.Name = "lineno";
            this.lineno.Size = new System.Drawing.Size(30, 13);
            this.lineno.TabIndex = 108;
            this.lineno.Text = "Line:";
            // 
            // department
            // 
            this.department.AutoSize = true;
            this.department.Location = new System.Drawing.Point(2, 44);
            this.department.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.department.Name = "department";
            this.department.Size = new System.Drawing.Size(33, 13);
            this.department.TabIndex = 2;
            this.department.Text = "Dept:";
            // 
            // mergeno
            // 
            this.mergeno.AutoSize = true;
            this.mergeno.Location = new System.Drawing.Point(140, 44);
            this.mergeno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mergeno.Name = "mergeno";
            this.mergeno.Size = new System.Drawing.Size(57, 13);
            this.mergeno.TabIndex = 4;
            this.mergeno.Text = "Merge No:";
            // 
            // lastboxno
            // 
            this.lastboxno.AutoSize = true;
            this.lastboxno.Location = new System.Drawing.Point(140, 2);
            this.lastboxno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lastboxno.Name = "lastboxno";
            this.lastboxno.Size = new System.Drawing.Size(51, 13);
            this.lastboxno.TabIndex = 6;
            this.lastboxno.Text = "Last Box:";
            // 
            // lastbox
            // 
            this.lastbox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lastbox.Location = new System.Drawing.Point(140, 16);
            this.lastbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastbox.Name = "lastbox";
            this.lastbox.ReadOnly = true;
            this.lastbox.Size = new System.Drawing.Size(120, 20);
            this.lastbox.TabIndex = 1;
            // 
            // item
            // 
            this.item.AutoSize = true;
            this.item.Location = new System.Drawing.Point(2, 85);
            this.item.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.item.Name = "item";
            this.item.Size = new System.Drawing.Size(30, 13);
            this.item.TabIndex = 8;
            this.item.Text = "Item:";
            // 
            // shade
            // 
            this.shade.AutoSize = true;
            this.shade.Location = new System.Drawing.Point(140, 85);
            this.shade.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.shade.Name = "shade";
            this.shade.Size = new System.Drawing.Size(41, 13);
            this.shade.TabIndex = 10;
            this.shade.Text = "Shade:";
            // 
            // shadecode
            // 
            this.shadecode.AutoSize = true;
            this.shadecode.Location = new System.Drawing.Point(276, 85);
            this.shadecode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.shadecode.Name = "shadecode";
            this.shadecode.Size = new System.Drawing.Size(69, 13);
            this.shadecode.TabIndex = 12;
            this.shadecode.Text = "Shade Code:";
            // 
            // boxno
            // 
            this.boxno.AutoSize = true;
            this.boxno.Location = new System.Drawing.Point(276, 2);
            this.boxno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.boxno.Name = "boxno";
            this.boxno.Size = new System.Drawing.Size(48, 13);
            this.boxno.TabIndex = 14;
            this.boxno.Text = "Box No.:";
            // 
            // packingdate
            // 
            this.packingdate.AutoSize = true;
            this.packingdate.Location = new System.Drawing.Point(276, 44);
            this.packingdate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.packingdate.Name = "packingdate";
            this.packingdate.Size = new System.Drawing.Size(33, 13);
            this.packingdate.TabIndex = 16;
            this.packingdate.Text = "Date:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(276, 57);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 20);
            this.dateTimePicker1.TabIndex = 6;
            // 
            // quality
            // 
            this.quality.AutoSize = true;
            this.quality.Location = new System.Drawing.Point(2, 2);
            this.quality.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.quality.Name = "quality";
            this.quality.Size = new System.Drawing.Size(42, 13);
            this.quality.TabIndex = 18;
            this.quality.Text = "Quality:";
            // 
            // saleorderno
            // 
            this.saleorderno.AutoSize = true;
            this.saleorderno.Location = new System.Drawing.Point(140, 2);
            this.saleorderno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.saleorderno.Name = "saleorderno";
            this.saleorderno.Size = new System.Drawing.Size(45, 13);
            this.saleorderno.TabIndex = 20;
            this.saleorderno.Text = "SO No.:";
            // 
            // packsize
            // 
            this.packsize.AutoSize = true;
            this.packsize.Location = new System.Drawing.Point(2, 44);
            this.packsize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.packsize.Name = "packsize";
            this.packsize.Size = new System.Drawing.Size(58, 13);
            this.packsize.TabIndex = 22;
            this.packsize.Text = "Pack Size:";
            // 
            // frdenier
            // 
            this.frdenier.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.frdenier.Location = new System.Drawing.Point(140, 59);
            this.frdenier.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.frdenier.Name = "frdenier";
            this.frdenier.ReadOnly = true;
            this.frdenier.Size = new System.Drawing.Size(120, 20);
            this.frdenier.TabIndex = 5;
            // 
            // updenier
            // 
            this.updenier.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.updenier.Location = new System.Drawing.Point(276, 59);
            this.updenier.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.updenier.Name = "updenier";
            this.updenier.ReadOnly = true;
            this.updenier.Size = new System.Drawing.Size(120, 20);
            this.updenier.TabIndex = 6;
            // 
            // windingtype
            // 
            this.windingtype.AutoSize = true;
            this.windingtype.Location = new System.Drawing.Point(255, 2);
            this.windingtype.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.windingtype.Name = "windingtype";
            this.windingtype.Size = new System.Drawing.Size(49, 13);
            this.windingtype.TabIndex = 28;
            this.windingtype.Text = "Winding:";
            // 
            // comport
            // 
            this.comport.AutoSize = true;
            this.comport.Location = new System.Drawing.Point(2, 2);
            this.comport.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.comport.Name = "comport";
            this.comport.Size = new System.Drawing.Size(53, 13);
            this.comport.TabIndex = 30;
            this.comport.Text = "Com Port:";
            this.comport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // copssize
            // 
            this.copssize.AutoSize = true;
            this.copssize.Location = new System.Drawing.Point(2, 126);
            this.copssize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.copssize.Name = "copssize";
            this.copssize.Size = new System.Drawing.Size(57, 13);
            this.copssize.TabIndex = 32;
            this.copssize.Text = "Cops Size:";
            // 
            // copweight
            // 
            this.copweight.AutoSize = true;
            this.copweight.Location = new System.Drawing.Point(140, 126);
            this.copweight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.copweight.Name = "copweight";
            this.copweight.Size = new System.Drawing.Size(24, 13);
            this.copweight.TabIndex = 34;
            this.copweight.Text = "Wt:";
            // 
            // copstock
            // 
            this.copstock.AutoSize = true;
            this.copstock.Location = new System.Drawing.Point(276, 126);
            this.copstock.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.copstock.Name = "copstock";
            this.copstock.Size = new System.Drawing.Size(65, 13);
            this.copstock.TabIndex = 35;
            this.copstock.Text = "Cops Stock:";
            // 
            // copsitemwt
            // 
            this.copsitemwt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.copsitemwt.Location = new System.Drawing.Point(140, 141);
            this.copsitemwt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.copsitemwt.Name = "copsitemwt";
            this.copsitemwt.ReadOnly = true;
            this.copsitemwt.Size = new System.Drawing.Size(120, 20);
            this.copsitemwt.TabIndex = 11;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(276, 141);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(120, 20);
            this.textBox2.TabIndex = 12;
            // 
            // boxtype
            // 
            this.boxtype.AutoSize = true;
            this.boxtype.Location = new System.Drawing.Point(2, 85);
            this.boxtype.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.boxtype.Name = "boxtype";
            this.boxtype.Size = new System.Drawing.Size(86, 13);
            this.boxtype.TabIndex = 38;
            this.boxtype.Text = "Box/Pallet Type:";
            // 
            // boxweight
            // 
            this.boxweight.AutoSize = true;
            this.boxweight.Location = new System.Drawing.Point(140, 85);
            this.boxweight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.boxweight.Name = "boxweight";
            this.boxweight.Size = new System.Drawing.Size(24, 13);
            this.boxweight.TabIndex = 40;
            this.boxweight.Text = "Wt:";
            // 
            // boxpalletitemwt
            // 
            this.boxpalletitemwt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.boxpalletitemwt.Location = new System.Drawing.Point(140, 100);
            this.boxpalletitemwt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.boxpalletitemwt.Name = "boxpalletitemwt";
            this.boxpalletitemwt.ReadOnly = true;
            this.boxpalletitemwt.Size = new System.Drawing.Size(120, 20);
            this.boxpalletitemwt.TabIndex = 8;
            // 
            // boxstock
            // 
            this.boxstock.AutoSize = true;
            this.boxstock.Location = new System.Drawing.Point(276, 85);
            this.boxstock.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.boxstock.Name = "boxstock";
            this.boxstock.Size = new System.Drawing.Size(90, 13);
            this.boxstock.TabIndex = 42;
            this.boxstock.Text = "Box/Pallet Stock:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(276, 100);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(120, 20);
            this.textBox4.TabIndex = 9;
            // 
            // productiontype
            // 
            this.productiontype.AutoSize = true;
            this.productiontype.Location = new System.Drawing.Point(140, 126);
            this.productiontype.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.productiontype.Name = "productiontype";
            this.productiontype.Size = new System.Drawing.Size(59, 13);
            this.productiontype.TabIndex = 44;
            this.productiontype.Text = "Prod Type:";
            // 
            // remark
            // 
            this.remark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.remark.AutoSize = true;
            this.remark.Location = new System.Drawing.Point(334, 329);
            this.remark.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.remark.Name = "remark";
            this.remark.Size = new System.Drawing.Size(52, 13);
            this.remark.TabIndex = 46;
            this.remark.Text = "Remarks:";
            // 
            // remarks
            // 
            this.remarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.remarks.Location = new System.Drawing.Point(334, 344);
            this.remarks.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.remarks.Multiline = true;
            this.remarks.Name = "remarks";
            this.remarks.Size = new System.Drawing.Size(380, 49);
            this.remarks.TabIndex = 6;
            // 
            // scalemodel
            // 
            this.scalemodel.AutoSize = true;
            this.scalemodel.Location = new System.Drawing.Point(125, 2);
            this.scalemodel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.scalemodel.Name = "scalemodel";
            this.scalemodel.Size = new System.Drawing.Size(71, 13);
            this.scalemodel.TabIndex = 48;
            this.scalemodel.Text = "Weigh Scale:";
            // 
            // LineNoList
            // 
            this.LineNoList.AllowDrop = true;
            this.LineNoList.FormattingEnabled = true;
            this.LineNoList.Location = new System.Drawing.Point(2, 16);
            this.LineNoList.Margin = new System.Windows.Forms.Padding(2);
            this.LineNoList.Name = "LineNoList";
            this.LineNoList.Size = new System.Drawing.Size(120, 21);
            this.LineNoList.TabIndex = 0;
            this.LineNoList.SelectedIndexChanged += new System.EventHandler(this.LineNoList_SelectedIndexChanged);
            // 
            // departmentname
            // 
            this.departmentname.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.departmentname.Location = new System.Drawing.Point(2, 57);
            this.departmentname.Margin = new System.Windows.Forms.Padding(2);
            this.departmentname.Name = "departmentname";
            this.departmentname.ReadOnly = true;
            this.departmentname.Size = new System.Drawing.Size(120, 20);
            this.departmentname.TabIndex = 4;
            // 
            // MergeNoList
            // 
            this.MergeNoList.AllowDrop = true;
            this.MergeNoList.FormattingEnabled = true;
            this.MergeNoList.Location = new System.Drawing.Point(140, 57);
            this.MergeNoList.Margin = new System.Windows.Forms.Padding(2);
            this.MergeNoList.Name = "MergeNoList";
            this.MergeNoList.Size = new System.Drawing.Size(120, 21);
            this.MergeNoList.TabIndex = 5;
            this.MergeNoList.SelectedIndexChanged += new System.EventHandler(this.MergeNoList_SelectedIndexChanged);
            // 
            // itemname
            // 
            this.itemname.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.itemname.Location = new System.Drawing.Point(2, 100);
            this.itemname.Margin = new System.Windows.Forms.Padding(2);
            this.itemname.Name = "itemname";
            this.itemname.ReadOnly = true;
            this.itemname.Size = new System.Drawing.Size(120, 20);
            this.itemname.TabIndex = 7;
            // 
            // shadename
            // 
            this.shadename.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.shadename.Location = new System.Drawing.Point(140, 100);
            this.shadename.Margin = new System.Windows.Forms.Padding(2);
            this.shadename.Name = "shadename";
            this.shadename.ReadOnly = true;
            this.shadename.Size = new System.Drawing.Size(120, 20);
            this.shadename.TabIndex = 8;
            // 
            // shadecd
            // 
            this.shadecd.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.shadecd.Location = new System.Drawing.Point(276, 100);
            this.shadecd.Margin = new System.Windows.Forms.Padding(2);
            this.shadecd.Name = "shadecd";
            this.shadecd.ReadOnly = true;
            this.shadecd.Size = new System.Drawing.Size(120, 20);
            this.shadecd.TabIndex = 9;
            // 
            // QualityList
            // 
            this.QualityList.AllowDrop = true;
            this.QualityList.FormattingEnabled = true;
            this.QualityList.Location = new System.Drawing.Point(2, 16);
            this.QualityList.Margin = new System.Windows.Forms.Padding(2);
            this.QualityList.Name = "QualityList";
            this.QualityList.Size = new System.Drawing.Size(120, 21);
            this.QualityList.TabIndex = 1;
            this.QualityList.SelectedIndexChanged += new System.EventHandler(this.QualityList_SelectedIndexChanged);
            // 
            // PackSizeList
            // 
            this.PackSizeList.AllowDrop = true;
            this.PackSizeList.FormattingEnabled = true;
            this.PackSizeList.Location = new System.Drawing.Point(2, 58);
            this.PackSizeList.Margin = new System.Windows.Forms.Padding(2);
            this.PackSizeList.Name = "PackSizeList";
            this.PackSizeList.Size = new System.Drawing.Size(120, 21);
            this.PackSizeList.TabIndex = 4;
            this.PackSizeList.SelectedIndexChanged += new System.EventHandler(this.PackSizeList_SelectedIndexChanged);
            // 
            // WindingTypeList
            // 
            this.WindingTypeList.AllowDrop = true;
            this.WindingTypeList.FormattingEnabled = true;
            this.WindingTypeList.Location = new System.Drawing.Point(255, 16);
            this.WindingTypeList.Margin = new System.Windows.Forms.Padding(2);
            this.WindingTypeList.Name = "WindingTypeList";
            this.WindingTypeList.Size = new System.Drawing.Size(110, 21);
            this.WindingTypeList.TabIndex = 3;
            this.WindingTypeList.SelectedIndexChanged += new System.EventHandler(this.WindingTypeList_SelectedIndexChanged);
            // 
            // ComPortList
            // 
            this.ComPortList.AllowDrop = true;
            this.ComPortList.FormattingEnabled = true;
            this.ComPortList.Location = new System.Drawing.Point(2, 16);
            this.ComPortList.Margin = new System.Windows.Forms.Padding(2);
            this.ComPortList.Name = "ComPortList";
            this.ComPortList.Size = new System.Drawing.Size(105, 21);
            this.ComPortList.TabIndex = 1;
            this.ComPortList.SelectedIndexChanged += new System.EventHandler(this.ComPortList_SelectedIndexChanged);
            // 
            // WeighingList
            // 
            this.WeighingList.AllowDrop = true;
            this.WeighingList.FormattingEnabled = true;
            this.WeighingList.Location = new System.Drawing.Point(125, 16);
            this.WeighingList.Margin = new System.Windows.Forms.Padding(2);
            this.WeighingList.Name = "WeighingList";
            this.WeighingList.Size = new System.Drawing.Size(110, 21);
            this.WeighingList.TabIndex = 2;
            this.WeighingList.SelectedIndexChanged += new System.EventHandler(this.WeighingList_SelectedIndexChanged);
            // 
            // CopsItemList
            // 
            this.CopsItemList.AllowDrop = true;
            this.CopsItemList.FormattingEnabled = true;
            this.CopsItemList.Location = new System.Drawing.Point(2, 141);
            this.CopsItemList.Margin = new System.Windows.Forms.Padding(2);
            this.CopsItemList.Name = "CopsItemList";
            this.CopsItemList.Size = new System.Drawing.Size(120, 21);
            this.CopsItemList.TabIndex = 10;
            this.CopsItemList.SelectedIndexChanged += new System.EventHandler(this.CopsItemList_SelectedIndexChanged);
            // 
            // BoxItemList
            // 
            this.BoxItemList.AllowDrop = true;
            this.BoxItemList.FormattingEnabled = true;
            this.BoxItemList.Location = new System.Drawing.Point(2, 99);
            this.BoxItemList.Margin = new System.Windows.Forms.Padding(2);
            this.BoxItemList.Name = "BoxItemList";
            this.BoxItemList.Size = new System.Drawing.Size(120, 21);
            this.BoxItemList.TabIndex = 7;
            this.BoxItemList.SelectedIndexChanged += new System.EventHandler(this.BoxItemList_SelectedIndexChanged);
            // 
            // SaleOrderList
            // 
            this.SaleOrderList.AllowDrop = true;
            this.SaleOrderList.FormattingEnabled = true;
            this.SaleOrderList.Location = new System.Drawing.Point(140, 16);
            this.SaleOrderList.Margin = new System.Windows.Forms.Padding(2);
            this.SaleOrderList.Name = "SaleOrderList";
            this.SaleOrderList.Size = new System.Drawing.Size(120, 21);
            this.SaleOrderList.TabIndex = 2;
            this.SaleOrderList.SelectedIndexChanged += new System.EventHandler(this.SaleOrderList_SelectedIndexChanged);
            // 
            // prcompany
            // 
            this.prcompany.AutoSize = true;
            this.prcompany.Location = new System.Drawing.Point(2, 2);
            this.prcompany.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prcompany.Name = "prcompany";
            this.prcompany.Size = new System.Drawing.Size(94, 17);
            this.prcompany.TabIndex = 1;
            this.prcompany.Text = "Print Company";
            this.prcompany.UseVisualStyleBackColor = true;
            // 
            // prowner
            // 
            this.prowner.AutoSize = true;
            this.prowner.Location = new System.Drawing.Point(100, 2);
            this.prowner.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prowner.Name = "prowner";
            this.prowner.Size = new System.Drawing.Size(81, 17);
            this.prowner.TabIndex = 2;
            this.prowner.Text = "Print Owner";
            this.prowner.UseVisualStyleBackColor = true;
            // 
            // prdate
            // 
            this.prdate.AutoSize = true;
            this.prdate.Location = new System.Drawing.Point(212, 2);
            this.prdate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prdate.Name = "prdate";
            this.prdate.Size = new System.Drawing.Size(73, 17);
            this.prdate.TabIndex = 3;
            this.prdate.Text = "Print Date";
            this.prdate.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prdate.UseVisualStyleBackColor = true;
            // 
            // pruser
            // 
            this.pruser.AutoSize = true;
            this.pruser.Location = new System.Drawing.Point(2, 20);
            this.pruser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pruser.Name = "pruser";
            this.pruser.Size = new System.Drawing.Size(72, 17);
            this.pruser.TabIndex = 5;
            this.pruser.Text = "Print User";
            this.pruser.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pruser.UseVisualStyleBackColor = true;
            // 
            // prhindi
            // 
            this.prhindi.AutoSize = true;
            this.prhindi.Location = new System.Drawing.Point(100, 20);
            this.prhindi.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prhindi.Name = "prhindi";
            this.prhindi.Size = new System.Drawing.Size(108, 17);
            this.prhindi.TabIndex = 6;
            this.prhindi.Text = "Print Hindi Words";
            this.prhindi.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prhindi.UseVisualStyleBackColor = true;
            // 
            // prwtps
            // 
            this.prwtps.AutoSize = true;
            this.prwtps.Location = new System.Drawing.Point(212, 20);
            this.prwtps.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prwtps.Name = "prwtps";
            this.prwtps.Size = new System.Drawing.Size(87, 17);
            this.prwtps.TabIndex = 7;
            this.prwtps.Text = "Print WT/PS";
            this.prwtps.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prwtps.UseVisualStyleBackColor = true;
            // 
            // prqrcode
            // 
            this.prqrcode.AutoSize = true;
            this.prqrcode.Location = new System.Drawing.Point(293, 2);
            this.prqrcode.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prqrcode.Name = "prqrcode";
            this.prqrcode.Size = new System.Drawing.Size(94, 17);
            this.prqrcode.TabIndex = 4;
            this.prqrcode.Text = "Print QR Code";
            this.prqrcode.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prqrcode.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "No. Of Copies:";
            // 
            // copyno
            // 
            this.copyno.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.copyno.Location = new System.Drawing.Point(276, 16);
            this.copyno.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.copyno.Name = "copyno";
            this.copyno.ReadOnly = true;
            this.copyno.Size = new System.Drawing.Size(120, 20);
            this.copyno.TabIndex = 3;
            this.copyno.TextChanged += new System.EventHandler(this.CopyNos_TextChanged);
            // 
            // wtpercop
            // 
            this.wtpercop.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.wtpercop.Location = new System.Drawing.Point(255, 100);
            this.wtpercop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.wtpercop.Name = "wtpercop";
            this.wtpercop.ReadOnly = true;
            this.wtpercop.Size = new System.Drawing.Size(110, 20);
            this.wtpercop.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(252, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 84;
            this.label5.Text = "Wt.Per Cop:";
            // 
            // netwt
            // 
            this.netwt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.netwt.Location = new System.Drawing.Point(125, 98);
            this.netwt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.netwt.Name = "netwt";
            this.netwt.ReadOnly = true;
            this.netwt.Size = new System.Drawing.Size(110, 20);
            this.netwt.TabIndex = 9;
            this.netwt.TextChanged += new System.EventHandler(this.NetWeight_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 85);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 82;
            this.label4.Text = "Net Wt:";
            // 
            // tarewt
            // 
            this.tarewt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tarewt.Location = new System.Drawing.Point(2, 100);
            this.tarewt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tarewt.Name = "tarewt";
            this.tarewt.ReadOnly = true;
            this.tarewt.Size = new System.Drawing.Size(105, 20);
            this.tarewt.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 80;
            this.label3.Text = "Tare Wt:";
            // 
            // grosswtno
            // 
            this.grosswtno.Location = new System.Drawing.Point(255, 57);
            this.grosswtno.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grosswtno.Name = "grosswtno";
            this.grosswtno.Size = new System.Drawing.Size(110, 20);
            this.grosswtno.TabIndex = 7;
            this.grosswtno.TextChanged += new System.EventHandler(this.GrossWeight_TextChanged);
            this.grosswtno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "Gross Wt:";
            // 
            // palletwtno
            // 
            this.palletwtno.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.palletwtno.Location = new System.Drawing.Point(125, 57);
            this.palletwtno.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.palletwtno.Name = "palletwtno";
            this.palletwtno.Size = new System.Drawing.Size(110, 20);
            this.palletwtno.TabIndex = 6;
            this.palletwtno.TextChanged += new System.EventHandler(this.PalletWeight_TextChanged);
            // 
            // palletwt
            // 
            this.palletwt.AutoSize = true;
            this.palletwt.Location = new System.Drawing.Point(119, 41);
            this.palletwt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.palletwt.Name = "palletwt";
            this.palletwt.Size = new System.Drawing.Size(108, 13);
            this.palletwt.TabIndex = 76;
            this.palletwt.Text = "Empty Box/Pallet Wt:";
            // 
            // spoolwt
            // 
            this.spoolwt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.spoolwt.Location = new System.Drawing.Point(49, 57);
            this.spoolwt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.spoolwt.Name = "spoolwt";
            this.spoolwt.ReadOnly = true;
            this.spoolwt.Size = new System.Drawing.Size(58, 20);
            this.spoolwt.TabIndex = 5;
            this.spoolwt.TextChanged += new System.EventHandler(this.SpoolWeight_TextChanged);
            this.spoolwt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // spoolno
            // 
            this.spoolno.Location = new System.Drawing.Point(2, 57);
            this.spoolno.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.spoolno.Name = "spoolno";
            this.spoolno.Size = new System.Drawing.Size(38, 20);
            this.spoolno.TabIndex = 4;
            this.spoolno.TextChanged += new System.EventHandler(this.SpoolNo_TextChanged);
            this.spoolno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // spool
            // 
            this.spool.AutoSize = true;
            this.spool.Location = new System.Drawing.Point(2, 44);
            this.spool.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.spool.Name = "spool";
            this.spool.Size = new System.Drawing.Size(42, 13);
            this.spool.TabIndex = 0;
            this.spool.Text = "Spools:";
            // 
            // prodtype
            // 
            this.prodtype.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.prodtype.Location = new System.Drawing.Point(140, 141);
            this.prodtype.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prodtype.Name = "prodtype";
            this.prodtype.ReadOnly = true;
            this.prodtype.Size = new System.Drawing.Size(120, 20);
            this.prodtype.TabIndex = 11;
            // 
            // palletdetails
            // 
            this.palletdetails.AutoSize = true;
            this.palletdetails.Location = new System.Drawing.Point(2, 5);
            this.palletdetails.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.palletdetails.Name = "palletdetails";
            this.palletdetails.Size = new System.Drawing.Size(71, 13);
            this.palletdetails.TabIndex = 78;
            this.palletdetails.Text = "Pallet Details:";
            this.palletdetails.UseMnemonic = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 79;
            this.label6.Text = "Pallet Type:";
            this.label6.UseMnemonic = false;
            // 
            // PalletTypeList
            // 
            this.PalletTypeList.AllowDrop = true;
            this.PalletTypeList.FormattingEnabled = true;
            this.PalletTypeList.Location = new System.Drawing.Point(65, 5);
            this.PalletTypeList.Margin = new System.Windows.Forms.Padding(2);
            this.PalletTypeList.Name = "PalletTypeList";
            this.PalletTypeList.Size = new System.Drawing.Size(100, 21);
            this.PalletTypeList.TabIndex = 1;
            // 
            // pquantity
            // 
            this.pquantity.AutoSize = true;
            this.pquantity.Location = new System.Drawing.Point(170, 8);
            this.pquantity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pquantity.Name = "pquantity";
            this.pquantity.Size = new System.Drawing.Size(26, 13);
            this.pquantity.TabIndex = 81;
            this.pquantity.Text = "Qty:";
            this.pquantity.UseMnemonic = false;
            // 
            // qnty
            // 
            this.qnty.Location = new System.Drawing.Point(198, 5);
            this.qnty.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.qnty.Name = "qnty";
            this.qnty.Size = new System.Drawing.Size(48, 20);
            this.qnty.TabIndex = 2;
            // 
            // addqty
            // 
            this.addqty.BackColor = System.Drawing.SystemColors.Highlight;
            this.addqty.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.addqty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addqty.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.addqty.FlatAppearance.BorderSize = 0;
            this.addqty.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.addqty.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.addqty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addqty.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.addqty.Location = new System.Drawing.Point(256, 4);
            this.addqty.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.addqty.Name = "addqty";
            this.addqty.Size = new System.Drawing.Size(48, 19);
            this.addqty.TabIndex = 3;
            this.addqty.Text = "Add";
            this.addqty.UseVisualStyleBackColor = false;
            this.addqty.Click += new System.EventHandler(this.addqty_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(-5, 34);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(345, 199);
            this.flowLayoutPanel1.TabIndex = 84;
            // 
            // submit
            // 
            this.submit.BackColor = System.Drawing.SystemColors.Highlight;
            this.submit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.submit.Location = new System.Drawing.Point(487, 518);
            this.submit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(71, 24);
            this.submit.TabIndex = 11;
            this.submit.Text = "Save";
            this.submit.UseVisualStyleBackColor = false;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // rightpanel
            // 
            this.rightpanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightpanel.AutoScroll = true;
            this.rightpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.rightpanel.Controls.Add(this.gradewiseprodn);
            this.rightpanel.Controls.Add(this.wgroupbox);
            this.rightpanel.Controls.Add(this.saveprint);
            this.rightpanel.Controls.Add(this.rowMaterialBox);
            this.rightpanel.Controls.Add(this.palletdetailslayout);
            this.rightpanel.Controls.Add(this.printingdetailslayout);
            this.rightpanel.Controls.Add(this.lastboxlayout);
            this.rightpanel.Controls.Add(this.machineboxlayout);
            this.rightpanel.Controls.Add(this.weighboxlayout);
            this.rightpanel.Controls.Add(this.packagingboxlayout);
            this.rightpanel.Controls.Add(this.cancelbtn);
            this.rightpanel.Controls.Add(this.remarks);
            this.rightpanel.Controls.Add(this.submit);
            this.rightpanel.Controls.Add(this.remark);
            this.rightpanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rightpanel.Location = new System.Drawing.Point(0, 0);
            this.rightpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rightpanel.Name = "rightpanel";
            this.rightpanel.Size = new System.Drawing.Size(1075, 569);
            this.rightpanel.TabIndex = 89;
            // 
            // gradewiseprodn
            // 
            this.gradewiseprodn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gradewiseprodn.Controls.Add(this.prodnbalqty);
            this.gradewiseprodn.Controls.Add(this.grdsoqty);
            this.gradewiseprodn.Controls.Add(this.qualityqty);
            this.gradewiseprodn.Controls.Add(this.totalprodbalqty);
            this.gradewiseprodn.Controls.Add(this.saleordrqty);
            this.gradewiseprodn.ForeColor = System.Drawing.Color.Black;
            this.gradewiseprodn.Location = new System.Drawing.Point(718, 112);
            this.gradewiseprodn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gradewiseprodn.Name = "gradewiseprodn";
            this.gradewiseprodn.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gradewiseprodn.Size = new System.Drawing.Size(350, 115);
            this.gradewiseprodn.TabIndex = 8;
            this.gradewiseprodn.TabStop = false;
            this.gradewiseprodn.Text = "Gradewise Production Status";
            // 
            // prodnbalqty
            // 
            this.prodnbalqty.AutoSize = true;
            this.prodnbalqty.Location = new System.Drawing.Point(102, 99);
            this.prodnbalqty.Name = "prodnbalqty";
            this.prodnbalqty.Size = new System.Drawing.Size(0, 13);
            this.prodnbalqty.TabIndex = 95;
            // 
            // grdsoqty
            // 
            this.grdsoqty.AutoSize = true;
            this.grdsoqty.Location = new System.Drawing.Point(91, 16);
            this.grdsoqty.Name = "grdsoqty";
            this.grdsoqty.Size = new System.Drawing.Size(0, 13);
            this.grdsoqty.TabIndex = 94;
            // 
            // qualityqty
            // 
            this.qualityqty.AllowUserToAddRows = false;
            this.qualityqty.AllowUserToDeleteRows = false;
            this.qualityqty.AllowUserToResizeColumns = false;
            this.qualityqty.AllowUserToResizeRows = false;
            this.qualityqty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qualityqty.AutoGenerateColumns = false;
            this.qualityqty.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.qualityqty.BackgroundColor = System.Drawing.Color.White;
            this.qualityqty.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.qualityqty.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.qualityqty.ColumnHeadersHeight = 34;
            this.qualityqty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.qualityqty.DataSource = this.qualityandqty;
            this.qualityqty.EnableHeadersVisualStyles = false;
            this.qualityqty.GridColor = System.Drawing.SystemColors.Control;
            this.qualityqty.Location = new System.Drawing.Point(0, 30);
            this.qualityqty.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.qualityqty.MultiSelect = false;
            this.qualityqty.Name = "qualityqty";
            this.qualityqty.ReadOnly = true;
            this.qualityqty.RowHeadersVisible = false;
            this.qualityqty.RowHeadersWidth = 62;
            this.qualityqty.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.qualityqty.Size = new System.Drawing.Size(350, 66);
            this.qualityqty.TabIndex = 92;
            this.qualityqty.Paint += new System.Windows.Forms.PaintEventHandler(this.qualityqty_Paint);
            // 
            // totalprodbalqty
            // 
            this.totalprodbalqty.AutoSize = true;
            this.totalprodbalqty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.totalprodbalqty.Location = new System.Drawing.Point(4, 99);
            this.totalprodbalqty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalprodbalqty.Name = "totalprodbalqty";
            this.totalprodbalqty.Size = new System.Drawing.Size(98, 13);
            this.totalprodbalqty.TabIndex = 93;
            this.totalprodbalqty.Text = "Production Bal Qty:";
            // 
            // saleordrqty
            // 
            this.saleordrqty.AutoSize = true;
            this.saleordrqty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saleordrqty.Location = new System.Drawing.Point(8, 14);
            this.saleordrqty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.saleordrqty.Name = "saleordrqty";
            this.saleordrqty.Size = new System.Drawing.Size(82, 13);
            this.saleordrqty.TabIndex = 92;
            this.saleordrqty.Text = "Sale Order Qty :";
            // 
            // wgroupbox
            // 
            this.wgroupbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wgroupbox.Controls.Add(this.windinggrid);
            this.wgroupbox.Location = new System.Drawing.Point(718, 1);
            this.wgroupbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.wgroupbox.Name = "wgroupbox";
            this.wgroupbox.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.wgroupbox.Size = new System.Drawing.Size(350, 104);
            this.wgroupbox.TabIndex = 7;
            this.wgroupbox.TabStop = false;
            this.wgroupbox.Text = "Winding Type + Gradewise Production Status";
            // 
            // windinggrid
            // 
            this.windinggrid.AllowUserToAddRows = false;
            this.windinggrid.AllowUserToDeleteRows = false;
            this.windinggrid.AllowUserToResizeColumns = false;
            this.windinggrid.AllowUserToResizeRows = false;
            this.windinggrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.windinggrid.AutoGenerateColumns = false;
            this.windinggrid.BackgroundColor = System.Drawing.Color.White;
            this.windinggrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.windinggrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.windinggrid.ColumnHeadersHeight = 34;
            this.windinggrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.windinggrid.DataSource = this.windingqty;
            this.windinggrid.EnableHeadersVisualStyles = false;
            this.windinggrid.Location = new System.Drawing.Point(0, 19);
            this.windinggrid.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.windinggrid.MultiSelect = false;
            this.windinggrid.Name = "windinggrid";
            this.windinggrid.ReadOnly = true;
            this.windinggrid.RowHeadersVisible = false;
            this.windinggrid.RowHeadersWidth = 62;
            this.windinggrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.windinggrid.Size = new System.Drawing.Size(350, 82);
            this.windinggrid.TabIndex = 92;
            this.windinggrid.Paint += new System.Windows.Forms.PaintEventHandler(this.windinggrid_Paint);
            // 
            // saveprint
            // 
            this.saveprint.BackColor = System.Drawing.SystemColors.Highlight;
            this.saveprint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveprint.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.saveprint.Location = new System.Drawing.Point(577, 518);
            this.saveprint.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.saveprint.Name = "saveprint";
            this.saveprint.Size = new System.Drawing.Size(81, 24);
            this.saveprint.TabIndex = 12;
            this.saveprint.Text = "Save && Print";
            this.saveprint.UseVisualStyleBackColor = false;
            this.saveprint.Click += new System.EventHandler(this.saveprint_Click);
            // 
            // rowMaterialBox
            // 
            this.rowMaterialBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rowMaterialBox.BackColor = System.Drawing.Color.White;
            this.rowMaterialBox.Controls.Add(this.rowMaterialPanel);
            this.rowMaterialBox.Location = new System.Drawing.Point(2, 402);
            this.rowMaterialBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rowMaterialBox.Name = "rowMaterialBox";
            this.rowMaterialBox.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rowMaterialBox.Size = new System.Drawing.Size(710, 104);
            this.rowMaterialBox.TabIndex = 10;
            this.rowMaterialBox.TabStop = false;
            this.rowMaterialBox.Text = "Key Raw Material Stock Status";
            // 
            // rowMaterialPanel
            // 
            this.rowMaterialPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rowMaterialPanel.Controls.Add(this.rowMaterial);
            this.rowMaterialPanel.Location = new System.Drawing.Point(13, 19);
            this.rowMaterialPanel.Name = "rowMaterialPanel";
            this.rowMaterialPanel.Size = new System.Drawing.Size(688, 79);
            this.rowMaterialPanel.TabIndex = 115;
            // 
            // rowMaterial
            // 
            this.rowMaterial.AllowUserToAddRows = false;
            this.rowMaterial.AllowUserToDeleteRows = false;
            this.rowMaterial.AllowUserToResizeColumns = false;
            this.rowMaterial.AllowUserToResizeRows = false;
            this.rowMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rowMaterial.BackgroundColor = System.Drawing.Color.White;
            this.rowMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rowMaterial.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.rowMaterial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.rowMaterial.ColumnHeadersHeight = 34;
            this.rowMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.rowMaterial.EnableHeadersVisualStyles = false;
            this.rowMaterial.Location = new System.Drawing.Point(2, 0);
            this.rowMaterial.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rowMaterial.MultiSelect = false;
            this.rowMaterial.Name = "rowMaterial";
            this.rowMaterial.ReadOnly = true;
            this.rowMaterial.RowHeadersVisible = false;
            this.rowMaterial.RowHeadersWidth = 62;
            this.rowMaterial.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.rowMaterial.Size = new System.Drawing.Size(690, 76);
            this.rowMaterial.TabIndex = 2;
            // 
            // palletdetailslayout
            // 
            this.palletdetailslayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.palletdetailslayout.BackColor = System.Drawing.Color.White;
            this.palletdetailslayout.ColumnCount = 1;
            this.palletdetailslayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.palletdetailslayout.Controls.Add(this.palletdetailsheader, 0, 0);
            this.palletdetailslayout.Controls.Add(this.palletdetailspanel, 0, 1);
            this.palletdetailslayout.Location = new System.Drawing.Point(718, 233);
            this.palletdetailslayout.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.palletdetailslayout.Name = "palletdetailslayout";
            this.palletdetailslayout.Padding = new System.Windows.Forms.Padding(2);
            this.palletdetailslayout.RowCount = 2;
            this.palletdetailslayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.palletdetailslayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.palletdetailslayout.Size = new System.Drawing.Size(350, 267);
            this.palletdetailslayout.TabIndex = 9;
            this.palletdetailslayout.Paint += new System.Windows.Forms.PaintEventHandler(this.palletdetailslayout_Paint);
            // 
            // palletdetailsheader
            // 
            this.palletdetailsheader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.palletdetailsheader.Controls.Add(this.palletdetails);
            this.palletdetailsheader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palletdetailsheader.Location = new System.Drawing.Point(2, 2);
            this.palletdetailsheader.Margin = new System.Windows.Forms.Padding(0);
            this.palletdetailsheader.Name = "palletdetailsheader";
            this.palletdetailsheader.Size = new System.Drawing.Size(346, 26);
            this.palletdetailsheader.TabIndex = 107;
            this.palletdetailsheader.Paint += new System.Windows.Forms.PaintEventHandler(this.palletdetailsheader_Paint);
            this.palletdetailsheader.Resize += new System.EventHandler(this.palletdetailsheader_Resize);
            // 
            // palletdetailspanel
            // 
            this.palletdetailspanel.Controls.Add(this.label6);
            this.palletdetailspanel.Controls.Add(this.PalletTypeList);
            this.palletdetailspanel.Controls.Add(this.pquantity);
            this.palletdetailspanel.Controls.Add(this.qnty);
            this.palletdetailspanel.Controls.Add(this.addqty);
            this.palletdetailspanel.Controls.Add(this.flowLayoutPanel1);
            this.palletdetailspanel.Location = new System.Drawing.Point(4, 31);
            this.palletdetailspanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.palletdetailspanel.Name = "palletdetailspanel";
            this.palletdetailspanel.Size = new System.Drawing.Size(342, 203);
            this.palletdetailspanel.TabIndex = 108;
            // 
            // printingdetailslayout
            // 
            this.printingdetailslayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.printingdetailslayout.BackColor = System.Drawing.Color.White;
            this.printingdetailslayout.ColumnCount = 1;
            this.printingdetailslayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.printingdetailslayout.Controls.Add(this.panel3, 0, 1);
            this.printingdetailslayout.Controls.Add(this.printingdetailsheader, 0, 0);
            this.printingdetailslayout.Location = new System.Drawing.Point(334, 1);
            this.printingdetailslayout.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.printingdetailslayout.Name = "printingdetailslayout";
            this.printingdetailslayout.Padding = new System.Windows.Forms.Padding(2);
            this.printingdetailslayout.RowCount = 2;
            this.printingdetailslayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.printingdetailslayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78F));
            this.printingdetailslayout.Size = new System.Drawing.Size(380, 75);
            this.printingdetailslayout.TabIndex = 2;
            this.printingdetailslayout.Paint += new System.Windows.Forms.PaintEventHandler(this.printingdetailslayout_Paint);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.prowner);
            this.panel3.Controls.Add(this.prwtps);
            this.panel3.Controls.Add(this.prcompany);
            this.panel3.Controls.Add(this.prqrcode);
            this.panel3.Controls.Add(this.prhindi);
            this.panel3.Controls.Add(this.pruser);
            this.panel3.Controls.Add(this.prdate);
            this.panel3.Location = new System.Drawing.Point(4, 20);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(372, 50);
            this.panel3.TabIndex = 87;
            // 
            // printingdetailsheader
            // 
            this.printingdetailsheader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.printingdetailsheader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.printingdetailsheader.Controls.Add(this.Printinglbl);
            this.printingdetailsheader.Location = new System.Drawing.Point(2, 2);
            this.printingdetailsheader.Margin = new System.Windows.Forms.Padding(0);
            this.printingdetailsheader.Name = "printingdetailsheader";
            this.printingdetailsheader.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.printingdetailsheader.Size = new System.Drawing.Size(376, 15);
            this.printingdetailsheader.TabIndex = 107;
            this.printingdetailsheader.Paint += new System.Windows.Forms.PaintEventHandler(this.printingdetailsheader_Paint);
            this.printingdetailsheader.Resize += new System.EventHandler(this.printingdetailsheader_Resize);
            // 
            // Printinglbl
            // 
            this.Printinglbl.AutoSize = true;
            this.Printinglbl.Location = new System.Drawing.Point(2, 1);
            this.Printinglbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Printinglbl.Name = "Printinglbl";
            this.Printinglbl.Size = new System.Drawing.Size(77, 13);
            this.Printinglbl.TabIndex = 107;
            this.Printinglbl.Text = "Printing Details";
            // 
            // lastboxlayout
            // 
            this.lastboxlayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lastboxlayout.BackColor = System.Drawing.Color.White;
            this.lastboxlayout.ColumnCount = 1;
            this.lastboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lastboxlayout.Controls.Add(this.lastboxpanel, 0, 1);
            this.lastboxlayout.Controls.Add(this.lastboxheader, 0, 0);
            this.lastboxlayout.Location = new System.Drawing.Point(334, 82);
            this.lastboxlayout.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastboxlayout.Name = "lastboxlayout";
            this.lastboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.lastboxlayout.RowCount = 2;
            this.lastboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.lastboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.lastboxlayout.Size = new System.Drawing.Size(380, 82);
            this.lastboxlayout.TabIndex = 4;
            this.lastboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.lastboxlayout_Paint);
            // 
            // lastboxpanel
            // 
            this.lastboxpanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lastboxpanel.Controls.Add(this.lastbxnetwtpanel);
            this.lastboxpanel.Controls.Add(this.lastbxgrosswtpanel);
            this.lastboxpanel.Controls.Add(this.lastbxtarepanel);
            this.lastboxpanel.Controls.Add(this.lastbxcopspanel);
            this.lastboxpanel.Location = new System.Drawing.Point(4, 28);
            this.lastboxpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastboxpanel.Name = "lastboxpanel";
            this.lastboxpanel.Size = new System.Drawing.Size(372, 49);
            this.lastboxpanel.TabIndex = 107;
            // 
            // lastbxnetwtpanel
            // 
            this.lastbxnetwtpanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lastbxnetwtpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxnetwtpanel.Controls.Add(this.netwttxtbox);
            this.lastbxnetwtpanel.Controls.Add(this.netweight);
            this.lastbxnetwtpanel.Location = new System.Drawing.Point(268, 2);
            this.lastbxnetwtpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastbxnetwtpanel.Name = "lastbxnetwtpanel";
            this.lastbxnetwtpanel.Size = new System.Drawing.Size(74, 45);
            this.lastbxnetwtpanel.TabIndex = 8;
            this.lastbxnetwtpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lastbxnetwtpanel_Paint);
            // 
            // netwttxtbox
            // 
            this.netwttxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.netwttxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.netwttxtbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.netwttxtbox.Location = new System.Drawing.Point(7, 23);
            this.netwttxtbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.netwttxtbox.Name = "netwttxtbox";
            this.netwttxtbox.ReadOnly = true;
            this.netwttxtbox.Size = new System.Drawing.Size(57, 13);
            this.netwttxtbox.TabIndex = 95;
            this.netwttxtbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // netweight
            // 
            this.netweight.AutoSize = true;
            this.netweight.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.netweight.Location = new System.Drawing.Point(20, 7);
            this.netweight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.netweight.Name = "netweight";
            this.netweight.Size = new System.Drawing.Size(24, 13);
            this.netweight.TabIndex = 8;
            this.netweight.Text = "Net";
            // 
            // lastbxgrosswtpanel
            // 
            this.lastbxgrosswtpanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lastbxgrosswtpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxgrosswtpanel.Controls.Add(this.grosswttxtbox);
            this.lastbxgrosswtpanel.Controls.Add(this.grossweight);
            this.lastbxgrosswtpanel.Location = new System.Drawing.Point(187, 2);
            this.lastbxgrosswtpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastbxgrosswtpanel.Name = "lastbxgrosswtpanel";
            this.lastbxgrosswtpanel.Size = new System.Drawing.Size(74, 45);
            this.lastbxgrosswtpanel.TabIndex = 6;
            this.lastbxgrosswtpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lastbxgrosswtpanel_Paint);
            // 
            // grosswttxtbox
            // 
            this.grosswttxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.grosswttxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grosswttxtbox.Location = new System.Drawing.Point(7, 23);
            this.grosswttxtbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grosswttxtbox.Name = "grosswttxtbox";
            this.grosswttxtbox.ReadOnly = true;
            this.grosswttxtbox.Size = new System.Drawing.Size(57, 13);
            this.grosswttxtbox.TabIndex = 7;
            this.grosswttxtbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grossweight
            // 
            this.grossweight.AutoSize = true;
            this.grossweight.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grossweight.Location = new System.Drawing.Point(15, 7);
            this.grossweight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.grossweight.Name = "grossweight";
            this.grossweight.Size = new System.Drawing.Size(34, 13);
            this.grossweight.TabIndex = 6;
            this.grossweight.Text = "Gross";
            // 
            // lastbxtarepanel
            // 
            this.lastbxtarepanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lastbxtarepanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxtarepanel.Controls.Add(this.tarewghttxtbox);
            this.lastbxtarepanel.Controls.Add(this.tareweight);
            this.lastbxtarepanel.Location = new System.Drawing.Point(106, 2);
            this.lastbxtarepanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastbxtarepanel.Name = "lastbxtarepanel";
            this.lastbxtarepanel.Size = new System.Drawing.Size(74, 45);
            this.lastbxtarepanel.TabIndex = 5;
            this.lastbxtarepanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lastbxtarepanel_Paint);
            // 
            // tarewghttxtbox
            // 
            this.tarewghttxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.tarewghttxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tarewghttxtbox.Location = new System.Drawing.Point(7, 23);
            this.tarewghttxtbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tarewghttxtbox.Name = "tarewghttxtbox";
            this.tarewghttxtbox.ReadOnly = true;
            this.tarewghttxtbox.Size = new System.Drawing.Size(57, 13);
            this.tarewghttxtbox.TabIndex = 5;
            this.tarewghttxtbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tareweight
            // 
            this.tareweight.AutoSize = true;
            this.tareweight.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tareweight.Location = new System.Drawing.Point(17, 7);
            this.tareweight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tareweight.Name = "tareweight";
            this.tareweight.Size = new System.Drawing.Size(29, 13);
            this.tareweight.TabIndex = 4;
            this.tareweight.Text = "Tare";
            // 
            // lastbxcopspanel
            // 
            this.lastbxcopspanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lastbxcopspanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxcopspanel.Controls.Add(this.cops);
            this.lastbxcopspanel.Controls.Add(this.copstxtbox);
            this.lastbxcopspanel.Location = new System.Drawing.Point(23, 2);
            this.lastbxcopspanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastbxcopspanel.Name = "lastbxcopspanel";
            this.lastbxcopspanel.Size = new System.Drawing.Size(76, 45);
            this.lastbxcopspanel.TabIndex = 4;
            this.lastbxcopspanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lastbxcopspanel_Paint);
            // 
            // cops
            // 
            this.cops.AutoSize = true;
            this.cops.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cops.Location = new System.Drawing.Point(17, 7);
            this.cops.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cops.Name = "cops";
            this.cops.Size = new System.Drawing.Size(31, 13);
            this.cops.TabIndex = 2;
            this.cops.Text = "Cops";
            // 
            // copstxtbox
            // 
            this.copstxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.copstxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.copstxtbox.Location = new System.Drawing.Point(7, 23);
            this.copstxtbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.copstxtbox.Name = "copstxtbox";
            this.copstxtbox.ReadOnly = true;
            this.copstxtbox.Size = new System.Drawing.Size(45, 13);
            this.copstxtbox.TabIndex = 3;
            this.copstxtbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lastboxheader
            // 
            this.lastboxheader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lastboxheader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.lastboxheader.Controls.Add(this.Lastboxlbl);
            this.lastboxheader.Location = new System.Drawing.Point(2, 2);
            this.lastboxheader.Margin = new System.Windows.Forms.Padding(0);
            this.lastboxheader.Name = "lastboxheader";
            this.lastboxheader.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lastboxheader.Size = new System.Drawing.Size(376, 21);
            this.lastboxheader.TabIndex = 107;
            this.lastboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.lastboxheader_Paint);
            this.lastboxheader.Resize += new System.EventHandler(this.lastboxheader_Resize);
            // 
            // Lastboxlbl
            // 
            this.Lastboxlbl.AutoSize = true;
            this.Lastboxlbl.Location = new System.Drawing.Point(4, 4);
            this.Lastboxlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lastboxlbl.Name = "Lastboxlbl";
            this.Lastboxlbl.Size = new System.Drawing.Size(80, 13);
            this.Lastboxlbl.TabIndex = 107;
            this.Lastboxlbl.Text = "Last box details";
            // 
            // machineboxlayout
            // 
            this.machineboxlayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.machineboxlayout.BackColor = System.Drawing.Color.White;
            this.machineboxlayout.ColumnCount = 1;
            this.machineboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.machineboxlayout.Controls.Add(this.machineboxpanel, 0, 1);
            this.machineboxlayout.Controls.Add(this.machineboxheader, 0, 0);
            this.machineboxlayout.Location = new System.Drawing.Point(2, 1);
            this.machineboxlayout.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.machineboxlayout.Name = "machineboxlayout";
            this.machineboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.machineboxlayout.RowCount = 2;
            this.machineboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.machineboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.machineboxlayout.Size = new System.Drawing.Size(328, 194);
            this.machineboxlayout.TabIndex = 0;
            this.machineboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.machineboxlayout_Paint);
            // 
            // machineboxpanel
            // 
            this.machineboxpanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.machineboxpanel.BackColor = System.Drawing.Color.White;
            this.machineboxpanel.Controls.Add(this.req3);
            this.machineboxpanel.Controls.Add(this.req2);
            this.machineboxpanel.Controls.Add(this.req1);
            this.machineboxpanel.Controls.Add(this.deniervalue);
            this.machineboxpanel.Controls.Add(this.denier);
            this.machineboxpanel.Controls.Add(this.shadecd);
            this.machineboxpanel.Controls.Add(this.shadecode);
            this.machineboxpanel.Controls.Add(this.linenoerror);
            this.machineboxpanel.Controls.Add(this.boxnoerror);
            this.machineboxpanel.Controls.Add(this.mergenoerror);
            this.machineboxpanel.Controls.Add(this.lineno);
            this.machineboxpanel.Controls.Add(this.prodtype);
            this.machineboxpanel.Controls.Add(this.productiontype);
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
            this.machineboxpanel.Location = new System.Drawing.Point(4, 24);
            this.machineboxpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.machineboxpanel.Name = "machineboxpanel";
            this.machineboxpanel.Size = new System.Drawing.Size(320, 165);
            this.machineboxpanel.TabIndex = 107;
            // 
            // req3
            // 
            this.req3.AutoSize = true;
            this.req3.ForeColor = System.Drawing.Color.Red;
            this.req3.Location = new System.Drawing.Point(195, 44);
            this.req3.Name = "req3";
            this.req3.Size = new System.Drawing.Size(11, 13);
            this.req3.TabIndex = 107;
            this.req3.Text = "*";
            this.req3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // req2
            // 
            this.req2.AutoSize = true;
            this.req2.ForeColor = System.Drawing.Color.Red;
            this.req2.Location = new System.Drawing.Point(318, 2);
            this.req2.Name = "req2";
            this.req2.Size = new System.Drawing.Size(11, 13);
            this.req2.TabIndex = 106;
            this.req2.Text = "*";
            this.req2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // req1
            // 
            this.req1.AutoSize = true;
            this.req1.ForeColor = System.Drawing.Color.Red;
            this.req1.Location = new System.Drawing.Point(30, 3);
            this.req1.Name = "req1";
            this.req1.Size = new System.Drawing.Size(11, 13);
            this.req1.TabIndex = 93;
            this.req1.Text = "*";
            this.req1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // deniervalue
            // 
            this.deniervalue.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.deniervalue.Location = new System.Drawing.Point(2, 141);
            this.deniervalue.Margin = new System.Windows.Forms.Padding(2);
            this.deniervalue.Name = "deniervalue";
            this.deniervalue.ReadOnly = true;
            this.deniervalue.Size = new System.Drawing.Size(120, 20);
            this.deniervalue.TabIndex = 10;
            // 
            // denier
            // 
            this.denier.AutoSize = true;
            this.denier.Location = new System.Drawing.Point(2, 126);
            this.denier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.denier.Name = "denier";
            this.denier.Size = new System.Drawing.Size(41, 13);
            this.denier.TabIndex = 94;
            this.denier.Text = "Denier:";
            // 
            // linenoerror
            // 
            this.linenoerror.AutoSize = true;
            this.linenoerror.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.linenoerror.ForeColor = System.Drawing.Color.Red;
            this.linenoerror.Location = new System.Drawing.Point(2, 35);
            this.linenoerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linenoerror.Name = "linenoerror";
            this.linenoerror.Size = new System.Drawing.Size(0, 13);
            this.linenoerror.TabIndex = 98;
            this.linenoerror.Visible = false;
            // 
            // boxnoerror
            // 
            this.boxnoerror.AutoSize = true;
            this.boxnoerror.ForeColor = System.Drawing.Color.Red;
            this.boxnoerror.Location = new System.Drawing.Point(276, 35);
            this.boxnoerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.boxnoerror.Name = "boxnoerror";
            this.boxnoerror.Size = new System.Drawing.Size(0, 13);
            this.boxnoerror.TabIndex = 105;
            this.boxnoerror.Visible = false;
            // 
            // mergenoerror
            // 
            this.mergenoerror.AutoSize = true;
            this.mergenoerror.ForeColor = System.Drawing.Color.Red;
            this.mergenoerror.Location = new System.Drawing.Point(140, 76);
            this.mergenoerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mergenoerror.Name = "mergenoerror";
            this.mergenoerror.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mergenoerror.Size = new System.Drawing.Size(0, 13);
            this.mergenoerror.TabIndex = 100;
            this.mergenoerror.Visible = false;
            // 
            // PrefixList
            // 
            this.PrefixList.AllowDrop = true;
            this.PrefixList.FormattingEnabled = true;
            this.PrefixList.Location = new System.Drawing.Point(276, 16);
            this.PrefixList.Margin = new System.Windows.Forms.Padding(2);
            this.PrefixList.Name = "PrefixList";
            this.PrefixList.Size = new System.Drawing.Size(120, 21);
            this.PrefixList.TabIndex = 3;
            this.PrefixList.SelectedIndexChanged += new System.EventHandler(this.PrefixList_SelectedIndexChanged);
            // 
            // machineboxheader
            // 
            this.machineboxheader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.machineboxheader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.machineboxheader.Controls.Add(this.Machinelbl);
            this.machineboxheader.Location = new System.Drawing.Point(2, 2);
            this.machineboxheader.Margin = new System.Windows.Forms.Padding(0);
            this.machineboxheader.Name = "machineboxheader";
            this.machineboxheader.Size = new System.Drawing.Size(324, 19);
            this.machineboxheader.TabIndex = 107;
            this.machineboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.machineboxheader_Paint);
            this.machineboxheader.Resize += new System.EventHandler(this.machineboxheader_Resize);
            // 
            // Machinelbl
            // 
            this.Machinelbl.AutoSize = true;
            this.Machinelbl.Location = new System.Drawing.Point(2, 3);
            this.Machinelbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Machinelbl.Name = "Machinelbl";
            this.Machinelbl.Size = new System.Drawing.Size(68, 13);
            this.Machinelbl.TabIndex = 107;
            this.Machinelbl.Text = "Order Details";
            // 
            // weighboxlayout
            // 
            this.weighboxlayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.weighboxlayout.BackColor = System.Drawing.Color.White;
            this.weighboxlayout.ColumnCount = 1;
            this.weighboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.weighboxlayout.Controls.Add(this.weighboxpanel, 0, 1);
            this.weighboxlayout.Controls.Add(this.weighboxheader, 0, 0);
            this.weighboxlayout.Location = new System.Drawing.Point(334, 170);
            this.weighboxlayout.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.weighboxlayout.Name = "weighboxlayout";
            this.weighboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.weighboxlayout.RowCount = 2;
            this.weighboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.weighboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86F));
            this.weighboxlayout.Size = new System.Drawing.Size(380, 152);
            this.weighboxlayout.TabIndex = 5;
            this.weighboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.weighboxlayout_Paint);
            // 
            // weighboxpanel
            // 
            this.weighboxpanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.weighboxpanel.Controls.Add(this.spoolweight);
            this.weighboxpanel.Controls.Add(this.WeighingList);
            this.weighboxpanel.Controls.Add(this.req10);
            this.weighboxpanel.Controls.Add(this.req9);
            this.weighboxpanel.Controls.Add(this.req8);
            this.weighboxpanel.Controls.Add(this.req7);
            this.weighboxpanel.Controls.Add(this.grosswterror);
            this.weighboxpanel.Controls.Add(this.palletwterror);
            this.weighboxpanel.Controls.Add(this.spoolwterror);
            this.weighboxpanel.Controls.Add(this.spoolnoerror);
            this.weighboxpanel.Controls.Add(this.scalemodel);
            this.weighboxpanel.Controls.Add(this.comport);
            this.weighboxpanel.Controls.Add(this.ComPortList);
            this.weighboxpanel.Controls.Add(this.WindingTypeList);
            this.weighboxpanel.Controls.Add(this.windingtype);
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
            this.weighboxpanel.Controls.Add(this.windingerror);
            this.weighboxpanel.Location = new System.Drawing.Point(4, 25);
            this.weighboxpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.weighboxpanel.Name = "weighboxpanel";
            this.weighboxpanel.Size = new System.Drawing.Size(372, 122);
            this.weighboxpanel.TabIndex = 107;
            // 
            // spoolweight
            // 
            this.spoolweight.AutoSize = true;
            this.spoolweight.Location = new System.Drawing.Point(58, 44);
            this.spoolweight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.spoolweight.Name = "spoolweight";
            this.spoolweight.Size = new System.Drawing.Size(24, 13);
            this.spoolweight.TabIndex = 116;
            this.spoolweight.Text = "Wt:";
            // 
            // req10
            // 
            this.req10.AutoSize = true;
            this.req10.ForeColor = System.Drawing.Color.Red;
            this.req10.Location = new System.Drawing.Point(300, 2);
            this.req10.Name = "req10";
            this.req10.Size = new System.Drawing.Size(11, 13);
            this.req10.TabIndex = 114;
            this.req10.Text = "*";
            this.req10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // req9
            // 
            this.req9.AutoSize = true;
            this.req9.ForeColor = System.Drawing.Color.Red;
            this.req9.Location = new System.Drawing.Point(305, 44);
            this.req9.Name = "req9";
            this.req9.Size = new System.Drawing.Size(11, 13);
            this.req9.TabIndex = 113;
            this.req9.Text = "*";
            this.req9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // req8
            // 
            this.req8.AutoSize = true;
            this.req8.ForeColor = System.Drawing.Color.Red;
            this.req8.Location = new System.Drawing.Point(220, 41);
            this.req8.Name = "req8";
            this.req8.Size = new System.Drawing.Size(11, 13);
            this.req8.TabIndex = 112;
            this.req8.Text = "*";
            this.req8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // req7
            // 
            this.req7.AutoSize = true;
            this.req7.ForeColor = System.Drawing.Color.Red;
            this.req7.Location = new System.Drawing.Point(40, 44);
            this.req7.Name = "req7";
            this.req7.Size = new System.Drawing.Size(11, 13);
            this.req7.TabIndex = 111;
            this.req7.Text = "*";
            this.req7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grosswterror
            // 
            this.grosswterror.AutoSize = true;
            this.grosswterror.ForeColor = System.Drawing.Color.Red;
            this.grosswterror.Location = new System.Drawing.Point(255, 76);
            this.grosswterror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.grosswterror.Name = "grosswterror";
            this.grosswterror.Size = new System.Drawing.Size(0, 13);
            this.grosswterror.TabIndex = 89;
            this.grosswterror.Visible = false;
            // 
            // palletwterror
            // 
            this.palletwterror.AutoSize = true;
            this.palletwterror.ForeColor = System.Drawing.Color.Red;
            this.palletwterror.Location = new System.Drawing.Point(125, 76);
            this.palletwterror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.palletwterror.Name = "palletwterror";
            this.palletwterror.Size = new System.Drawing.Size(0, 13);
            this.palletwterror.TabIndex = 88;
            this.palletwterror.Visible = false;
            // 
            // spoolwterror
            // 
            this.spoolwterror.AutoSize = true;
            this.spoolwterror.ForeColor = System.Drawing.Color.Red;
            this.spoolwterror.Location = new System.Drawing.Point(2, 76);
            this.spoolwterror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.spoolwterror.Name = "spoolwterror";
            this.spoolwterror.Size = new System.Drawing.Size(0, 13);
            this.spoolwterror.TabIndex = 87;
            this.spoolwterror.Visible = false;
            // 
            // spoolnoerror
            // 
            this.spoolnoerror.AutoSize = true;
            this.spoolnoerror.ForeColor = System.Drawing.Color.Red;
            this.spoolnoerror.Location = new System.Drawing.Point(2, 76);
            this.spoolnoerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.spoolnoerror.Name = "spoolnoerror";
            this.spoolnoerror.Size = new System.Drawing.Size(0, 13);
            this.spoolnoerror.TabIndex = 86;
            this.spoolnoerror.Visible = false;
            // 
            // windingerror
            // 
            this.windingerror.AutoSize = true;
            this.windingerror.ForeColor = System.Drawing.Color.Red;
            this.windingerror.Location = new System.Drawing.Point(255, 35);
            this.windingerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.windingerror.Name = "windingerror";
            this.windingerror.Size = new System.Drawing.Size(0, 13);
            this.windingerror.TabIndex = 104;
            this.windingerror.Visible = false;
            // 
            // weighboxheader
            // 
            this.weighboxheader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.weighboxheader.Controls.Add(this.Weighboxlbl);
            this.weighboxheader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weighboxheader.Location = new System.Drawing.Point(2, 2);
            this.weighboxheader.Margin = new System.Windows.Forms.Padding(0);
            this.weighboxheader.Name = "weighboxheader";
            this.weighboxheader.Size = new System.Drawing.Size(376, 20);
            this.weighboxheader.TabIndex = 107;
            this.weighboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.weighboxheader_Paint);
            this.weighboxheader.Resize += new System.EventHandler(this.weighboxheader_Resize);
            // 
            // Weighboxlbl
            // 
            this.Weighboxlbl.AutoSize = true;
            this.Weighboxlbl.Location = new System.Drawing.Point(2, 4);
            this.Weighboxlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Weighboxlbl.Name = "Weighboxlbl";
            this.Weighboxlbl.Size = new System.Drawing.Size(88, 13);
            this.Weighboxlbl.TabIndex = 107;
            this.Weighboxlbl.Text = "Weigh and Label";
            // 
            // packagingboxlayout
            // 
            this.packagingboxlayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.packagingboxlayout.BackColor = System.Drawing.Color.White;
            this.packagingboxlayout.ColumnCount = 1;
            this.packagingboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.packagingboxlayout.Controls.Add(this.packagingboxheader, 0, 0);
            this.packagingboxlayout.Controls.Add(this.packagingboxpanel, 0, 1);
            this.packagingboxlayout.Location = new System.Drawing.Point(2, 200);
            this.packagingboxlayout.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.packagingboxlayout.Name = "packagingboxlayout";
            this.packagingboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.packagingboxlayout.RowCount = 2;
            this.packagingboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.packagingboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89F));
            this.packagingboxlayout.Size = new System.Drawing.Size(328, 193);
            this.packagingboxlayout.TabIndex = 3;
            this.packagingboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.packagingboxlayout_Paint);
            // 
            // packagingboxheader
            // 
            this.packagingboxheader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.packagingboxheader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.packagingboxheader.Controls.Add(this.Packagingboxlbl);
            this.packagingboxheader.Location = new System.Drawing.Point(2, 2);
            this.packagingboxheader.Margin = new System.Windows.Forms.Padding(0);
            this.packagingboxheader.Name = "packagingboxheader";
            this.packagingboxheader.Size = new System.Drawing.Size(324, 20);
            this.packagingboxheader.TabIndex = 107;
            this.packagingboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.packagingboxheader_Paint);
            this.packagingboxheader.Resize += new System.EventHandler(this.packagingboxheader_Resize);
            // 
            // Packagingboxlbl
            // 
            this.Packagingboxlbl.AutoSize = true;
            this.Packagingboxlbl.Location = new System.Drawing.Point(2, 3);
            this.Packagingboxlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Packagingboxlbl.Name = "Packagingboxlbl";
            this.Packagingboxlbl.Size = new System.Drawing.Size(58, 13);
            this.Packagingboxlbl.TabIndex = 107;
            this.Packagingboxlbl.Text = "Packaging";
            // 
            // packagingboxpanel
            // 
            this.packagingboxpanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.packagingboxpanel.Controls.Add(this.uptodenier);
            this.packagingboxpanel.Controls.Add(this.fromdenier);
            this.packagingboxpanel.Controls.Add(this.req6);
            this.packagingboxpanel.Controls.Add(this.req5);
            this.packagingboxpanel.Controls.Add(this.req4);
            this.packagingboxpanel.Controls.Add(this.SaleOrderList);
            this.packagingboxpanel.Controls.Add(this.saleorderno);
            this.packagingboxpanel.Controls.Add(this.QualityList);
            this.packagingboxpanel.Controls.Add(this.quality);
            this.packagingboxpanel.Controls.Add(this.label1);
            this.packagingboxpanel.Controls.Add(this.packsize);
            this.packagingboxpanel.Controls.Add(this.copyno);
            this.packagingboxpanel.Controls.Add(this.PackSizeList);
            this.packagingboxpanel.Controls.Add(this.frdenier);
            this.packagingboxpanel.Controls.Add(this.updenier);
            this.packagingboxpanel.Controls.Add(this.copssize);
            this.packagingboxpanel.Controls.Add(this.packsizeerror);
            this.packagingboxpanel.Controls.Add(this.CopsItemList);
            this.packagingboxpanel.Controls.Add(this.boxtype);
            this.packagingboxpanel.Controls.Add(this.BoxItemList);
            this.packagingboxpanel.Controls.Add(this.boxweight);
            this.packagingboxpanel.Controls.Add(this.boxpalletitemwt);
            this.packagingboxpanel.Controls.Add(this.boxstock);
            this.packagingboxpanel.Controls.Add(this.textBox4);
            this.packagingboxpanel.Controls.Add(this.copweight);
            this.packagingboxpanel.Controls.Add(this.copsitemwt);
            this.packagingboxpanel.Controls.Add(this.copstock);
            this.packagingboxpanel.Controls.Add(this.textBox2);
            this.packagingboxpanel.Controls.Add(this.soerror);
            this.packagingboxpanel.Controls.Add(this.copynoerror);
            this.packagingboxpanel.Controls.Add(this.qualityerror);
            this.packagingboxpanel.Location = new System.Drawing.Point(4, 25);
            this.packagingboxpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.packagingboxpanel.Name = "packagingboxpanel";
            this.packagingboxpanel.Size = new System.Drawing.Size(320, 163);
            this.packagingboxpanel.TabIndex = 107;
            // 
            // uptodenier
            // 
            this.uptodenier.AutoSize = true;
            this.uptodenier.Location = new System.Drawing.Point(276, 44);
            this.uptodenier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.uptodenier.Name = "uptodenier";
            this.uptodenier.Size = new System.Drawing.Size(67, 13);
            this.uptodenier.TabIndex = 117;
            this.uptodenier.Text = "Upto Denier:";
            // 
            // fromdenier
            // 
            this.fromdenier.AutoSize = true;
            this.fromdenier.Location = new System.Drawing.Point(140, 44);
            this.fromdenier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fromdenier.Name = "fromdenier";
            this.fromdenier.Size = new System.Drawing.Size(67, 13);
            this.fromdenier.TabIndex = 116;
            this.fromdenier.Text = "From Denier:";
            // 
            // req6
            // 
            this.req6.AutoSize = true;
            this.req6.ForeColor = System.Drawing.Color.Red;
            this.req6.Location = new System.Drawing.Point(55, 44);
            this.req6.Name = "req6";
            this.req6.Size = new System.Drawing.Size(11, 13);
            this.req6.TabIndex = 110;
            this.req6.Text = "*";
            this.req6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // req5
            // 
            this.req5.AutoSize = true;
            this.req5.ForeColor = System.Drawing.Color.Red;
            this.req5.Location = new System.Drawing.Point(180, 3);
            this.req5.Name = "req5";
            this.req5.Size = new System.Drawing.Size(11, 13);
            this.req5.TabIndex = 109;
            this.req5.Text = "*";
            this.req5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // req4
            // 
            this.req4.AutoSize = true;
            this.req4.ForeColor = System.Drawing.Color.Red;
            this.req4.Location = new System.Drawing.Point(40, 3);
            this.req4.Name = "req4";
            this.req4.Size = new System.Drawing.Size(11, 13);
            this.req4.TabIndex = 108;
            this.req4.Text = "*";
            this.req4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // packsizeerror
            // 
            this.packsizeerror.AutoSize = true;
            this.packsizeerror.ForeColor = System.Drawing.Color.Red;
            this.packsizeerror.Location = new System.Drawing.Point(2, 75);
            this.packsizeerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.packsizeerror.Name = "packsizeerror";
            this.packsizeerror.Size = new System.Drawing.Size(0, 13);
            this.packsizeerror.TabIndex = 103;
            this.packsizeerror.Visible = false;
            // 
            // soerror
            // 
            this.soerror.AutoSize = true;
            this.soerror.ForeColor = System.Drawing.Color.Red;
            this.soerror.Location = new System.Drawing.Point(140, 35);
            this.soerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.soerror.Name = "soerror";
            this.soerror.Size = new System.Drawing.Size(0, 13);
            this.soerror.TabIndex = 102;
            this.soerror.Visible = false;
            // 
            // copynoerror
            // 
            this.copynoerror.AutoSize = true;
            this.copynoerror.ForeColor = System.Drawing.Color.Red;
            this.copynoerror.Location = new System.Drawing.Point(276, 35);
            this.copynoerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.copynoerror.Name = "copynoerror";
            this.copynoerror.Size = new System.Drawing.Size(0, 13);
            this.copynoerror.TabIndex = 99;
            this.copynoerror.Visible = false;
            // 
            // qualityerror
            // 
            this.qualityerror.AutoSize = true;
            this.qualityerror.ForeColor = System.Drawing.Color.Red;
            this.qualityerror.Location = new System.Drawing.Point(2, 35);
            this.qualityerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.qualityerror.Name = "qualityerror";
            this.qualityerror.Size = new System.Drawing.Size(0, 13);
            this.qualityerror.TabIndex = 101;
            this.qualityerror.Visible = false;
            // 
            // cancelbtn
            // 
            this.cancelbtn.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.cancelbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelbtn.ForeColor = System.Drawing.SystemColors.Control;
            this.cancelbtn.Location = new System.Drawing.Point(405, 518);
            this.cancelbtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(65, 24);
            this.cancelbtn.TabIndex = 13;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = false;
            this.cancelbtn.Click += new System.EventHandler(this.btnCancel_Click);
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
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(197, 76);
            this.panel5.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(195, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(606, 447);
            this.panel6.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.menu);
            this.panel7.Controls.Add(this.menuBtn);
            this.panel7.Location = new System.Drawing.Point(21, 9);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(139, 55);
            this.panel7.TabIndex = 1;
            // 
            // menu
            // 
            this.menu.AutoSize = true;
            this.menu.ForeColor = System.Drawing.Color.Black;
            this.menu.Location = new System.Drawing.Point(48, 20);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(34, 13);
            this.menu.TabIndex = 1;
            this.menu.Text = "Menu";
            // 
            // menuBtn
            // 
            this.menuBtn.Image = global::PackingApplication.Properties.Resources.icons8_menu_50;
            this.menuBtn.Location = new System.Drawing.Point(3, 15);
            this.menuBtn.Name = "menuBtn";
            this.menuBtn.Size = new System.Drawing.Size(29, 24);
            this.menuBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.menuBtn.TabIndex = 1;
            this.menuBtn.TabStop = false;
            // 
            // sidebarTimer
            // 
            this.sidebarTimer.Interval = 10;
            // 
            // POYPackingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1075, 569);
            this.Controls.Add(this.rightpanel);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "POYPackingForm";
            this.Text = "POYPackingForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.POYPackingForm_Load);
            this.rightpanel.ResumeLayout(false);
            this.rightpanel.PerformLayout();
            this.gradewiseprodn.ResumeLayout(false);
            this.gradewiseprodn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualityqty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityandqty)).EndInit();
            this.wgroupbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.windinggrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.windingqty)).EndInit();
            this.rowMaterialBox.ResumeLayout(false);
            this.rowMaterialPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rowMaterial)).EndInit();
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
            this.lastbxnetwtpanel.ResumeLayout(false);
            this.lastbxnetwtpanel.PerformLayout();
            this.lastbxgrosswtpanel.ResumeLayout(false);
            this.lastbxgrosswtpanel.PerformLayout();
            this.lastbxtarepanel.ResumeLayout(false);
            this.lastbxtarepanel.PerformLayout();
            this.lastbxcopspanel.ResumeLayout(false);
            this.lastbxcopspanel.PerformLayout();
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
            this.packagingboxheader.ResumeLayout(false);
            this.packagingboxheader.PerformLayout();
            this.packagingboxpanel.ResumeLayout(false);
            this.packagingboxpanel.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuBtn)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TextBox copsitemwt;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label boxtype;
        private System.Windows.Forms.Label boxweight;
        private System.Windows.Forms.TextBox boxpalletitemwt;
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
        private System.Windows.Forms.Label mergenoerror;
        private System.Windows.Forms.Label packsizeerror;
        private System.Windows.Forms.Label spoolnoerror;
        private System.Windows.Forms.Label spoolwterror;
        private System.Windows.Forms.Label palletwterror;
        private System.Windows.Forms.Label grosswterror;
        private System.Windows.Forms.Label boxnoerror;
        private Button cancelbtn;
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
        private System.Windows.Forms.Label palletdetails;
        private System.Windows.Forms.Label grdsoqty;
        private System.Windows.Forms.Label prodnbalqty;
        private GroupBox rowMaterialBox;
        private DataGridView rowMaterial;
        private Panel rowMaterialPanel;
        private System.Windows.Forms.Label copynoerror;
        private System.Windows.Forms.Label qualityerror;
        private System.Windows.Forms.Label soerror;
        private System.Windows.Forms.Label windingerror;
        private System.Windows.Forms.Label req1;
        private System.Windows.Forms.Label req3;
        private System.Windows.Forms.Label req2;
        private System.Windows.Forms.Label req6;
        private System.Windows.Forms.Label req5;
        private System.Windows.Forms.Label req4;
        private System.Windows.Forms.Label req9;
        private System.Windows.Forms.Label req8;
        private System.Windows.Forms.Label req7;
        private System.Windows.Forms.Label req10;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private System.Windows.Forms.Label menu;
        private PictureBox menuBtn;
        private Timer sidebarTimer;
        private Button saveprint;
        private System.Windows.Forms.Label spoolweight;
        private System.Windows.Forms.Label fromdenier;
        private System.Windows.Forms.Label uptodenier;
        //private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}

