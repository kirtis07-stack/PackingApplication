using PackingApplication.Helper;
using System.Data;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace PackingApplication
{
    partial class ModifyChipsPackingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.shadecode = new System.Windows.Forms.Label();
            this.boxno = new System.Windows.Forms.Label();
            this.quality = new System.Windows.Forms.Label();
            this.packsize = new System.Windows.Forms.Label();
            this.comport = new System.Windows.Forms.Label();
            this.remark = new System.Windows.Forms.Label();
            this.remarks = new System.Windows.Forms.TextBox();
            this.scalemodel = new System.Windows.Forms.Label();
            this.QualityList = new System.Windows.Forms.ComboBox();
            this.PackSizeList = new System.Windows.Forms.ComboBox();
            this.ComPortList = new System.Windows.Forms.ComboBox();
            this.WeighingList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.copyno = new System.Windows.Forms.TextBox();
            this.netwt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grosswtno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.palletwtno = new System.Windows.Forms.TextBox();
            this.palletwt = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.panel35 = new System.Windows.Forms.Panel();
            this.panel28 = new System.Windows.Forms.Panel();
            this.panel29 = new System.Windows.Forms.Panel();
            this.panel34 = new System.Windows.Forms.Panel();
            this.tarewt = new System.Windows.Forms.Label();
            this.panel45 = new System.Windows.Forms.Panel();
            this.copynoerror = new System.Windows.Forms.Label();
            this.panel32 = new System.Windows.Forms.Panel();
            this.req8 = new System.Windows.Forms.Label();
            this.palletwterror = new System.Windows.Forms.Label();
            this.panel33 = new System.Windows.Forms.Panel();
            this.grosswterror = new System.Windows.Forms.Label();
            this.req9 = new System.Windows.Forms.Label();
            this.panel30 = new System.Windows.Forms.Panel();
            this.windingerror = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            this.rightpanel = new System.Windows.Forms.Panel();
            this.datalistpopuppanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.nextbtn = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.prevbtn = new System.Windows.Forms.Button();
            this.panel22 = new System.Windows.Forms.Panel();
            this.closelistbtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.popuppanel = new System.Windows.Forms.Panel();
            this.searchbtn = new System.Windows.Forms.Button();
            this.closepopupbtn = new System.Windows.Forms.Button();
            this.panel58 = new System.Windows.Forms.Panel();
            this.srproddateradiobtn = new System.Windows.Forms.RadioButton();
            this.srboxnoradiobtn = new System.Windows.Forms.RadioButton();
            this.srdeptradiobtn = new System.Windows.Forms.RadioButton();
            this.srlinenoradiobtn = new System.Windows.Forms.RadioButton();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.SrBoxNoList = new System.Windows.Forms.ComboBox();
            this.SrDeptList = new System.Windows.Forms.ComboBox();
            this.SrLineNoList = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttontablelayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel21 = new System.Windows.Forms.Panel();
            this.findbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.saveprint = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.rowMaterialBox = new System.Windows.Forms.GroupBox();
            this.rowMaterialPanel = new System.Windows.Forms.Panel();
            this.rowMaterial = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel44 = new System.Windows.Forms.Panel();
            this.packagingboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.packagingboxheader = new System.Windows.Forms.Panel();
            this.Packagingboxlbl = new System.Windows.Forms.Label();
            this.packagingboxpanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel25 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.panel54 = new System.Windows.Forms.Panel();
            this.BoxItemList = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.boxtype = new System.Windows.Forms.Label();
            this.panel55 = new System.Windows.Forms.Panel();
            this.boxweight = new System.Windows.Forms.Label();
            this.boxpalletitemwt = new System.Windows.Forms.Label();
            this.panel56 = new System.Windows.Forms.Panel();
            this.boxstock = new System.Windows.Forms.Label();
            this.boxpalletstock = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.uptodenier = new System.Windows.Forms.Label();
            this.updenier = new System.Windows.Forms.Label();
            this.panel26 = new System.Windows.Forms.Panel();
            this.fromwt = new System.Windows.Forms.Label();
            this.frwt = new System.Windows.Forms.Label();
            this.panel50 = new System.Windows.Forms.Panel();
            this.uptowt = new System.Windows.Forms.Label();
            this.upwt = new System.Windows.Forms.Label();
            this.panel20 = new System.Windows.Forms.Panel();
            this.fromdenier = new System.Windows.Forms.Label();
            this.frdenier = new System.Windows.Forms.Label();
            this.panel19 = new System.Windows.Forms.Panel();
            this.req6 = new System.Windows.Forms.Label();
            this.packsizeerror = new System.Windows.Forms.Label();
            this.panel27 = new System.Windows.Forms.Panel();
            this.OwnerList = new System.Windows.Forms.ComboBox();
            this.owner = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.machineboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.machineboxheader = new System.Windows.Forms.Panel();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Machinelbl = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lastbox = new System.Windows.Forms.Label();
            this.lastboxno = new System.Windows.Forms.Label();
            this.panel57 = new System.Windows.Forms.Panel();
            this.packingdate = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.machineboxpanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lineno = new System.Windows.Forms.Label();
            this.req1 = new System.Windows.Forms.Label();
            this.LineNoList = new System.Windows.Forms.ComboBox();
            this.linenoerror = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.DeptList = new System.Windows.Forms.ComboBox();
            this.department = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.req4 = new System.Windows.Forms.Label();
            this.qualityerror = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.boxnofrmt = new System.Windows.Forms.Label();
            this.req2 = new System.Windows.Forms.Label();
            this.boxnoerror = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.req3 = new System.Windows.Forms.Label();
            this.mergeno = new System.Windows.Forms.Label();
            this.MergeNoList = new System.Windows.Forms.ComboBox();
            this.mergenoerror = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.itemname = new System.Windows.Forms.Label();
            this.item = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.shadecd = new System.Windows.Forms.Label();
            this.shadename = new System.Windows.Forms.Label();
            this.shade = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.deniervalue = new System.Windows.Forms.Label();
            this.denier = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel46 = new System.Windows.Forms.Panel();
            this.salelotvalue = new System.Windows.Forms.Label();
            this.salelot = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.prodtype = new System.Windows.Forms.Label();
            this.productiontype = new System.Windows.Forms.Label();
            this.tblpanl1 = new System.Windows.Forms.Panel();
            this.printingdetailslayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.panel37 = new System.Windows.Forms.Panel();
            this.prcompany = new System.Windows.Forms.CheckBox();
            this.panel38 = new System.Windows.Forms.Panel();
            this.prowner = new System.Windows.Forms.CheckBox();
            this.panel39 = new System.Windows.Forms.Panel();
            this.prdate = new System.Windows.Forms.CheckBox();
            this.panel24 = new System.Windows.Forms.Panel();
            this.prtwist = new System.Windows.Forms.CheckBox();
            this.panel42 = new System.Windows.Forms.Panel();
            this.prwtps = new System.Windows.Forms.CheckBox();
            this.panel43 = new System.Windows.Forms.Panel();
            this.prqrcode = new System.Windows.Forms.CheckBox();
            this.panel40 = new System.Windows.Forms.Panel();
            this.prhindi = new System.Windows.Forms.CheckBox();
            this.panel41 = new System.Windows.Forms.Panel();
            this.pruser = new System.Windows.Forms.CheckBox();
            this.printingdetailsheader = new System.Windows.Forms.Panel();
            this.Printinglbl = new System.Windows.Forms.Label();
            this.weighboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.weighboxheader = new System.Windows.Forms.Panel();
            this.Weighboxlbl = new System.Windows.Forms.Label();
            this.weighboxpanel = new System.Windows.Forms.Panel();
            this.spoolwterror = new System.Windows.Forms.Label();
            this.lastboxlayout = new System.Windows.Forms.TableLayoutPanel();
            this.lastboxpanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lastbxnetwtpanel = new System.Windows.Forms.Panel();
            this.netwttxtbox = new System.Windows.Forms.TextBox();
            this.netweight = new System.Windows.Forms.Label();
            this.lastbxcopspanel = new System.Windows.Forms.Panel();
            this.cops = new System.Windows.Forms.Label();
            this.copstxtbox = new System.Windows.Forms.TextBox();
            this.lastbxgrosswtpanel = new System.Windows.Forms.Panel();
            this.grosswttxtbox = new System.Windows.Forms.TextBox();
            this.grossweight = new System.Windows.Forms.Label();
            this.lastbxtarepanel = new System.Windows.Forms.Panel();
            this.tarewghttxtbox = new System.Windows.Forms.TextBox();
            this.tareweight = new System.Windows.Forms.Label();
            this.lastboxheader = new System.Windows.Forms.Panel();
            this.Lastboxlbl = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.menu = new System.Windows.Forms.Label();
            this.menuBtn = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel6.SuspendLayout();
            this.panel35.SuspendLayout();
            this.panel28.SuspendLayout();
            this.panel29.SuspendLayout();
            this.panel34.SuspendLayout();
            this.panel45.SuspendLayout();
            this.panel32.SuspendLayout();
            this.panel33.SuspendLayout();
            this.panel30.SuspendLayout();
            this.rightpanel.SuspendLayout();
            this.datalistpopuppanel.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.popuppanel.SuspendLayout();
            this.panel58.SuspendLayout();
            this.buttontablelayout.SuspendLayout();
            this.panel21.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.rowMaterialBox.SuspendLayout();
            this.rowMaterialPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rowMaterial)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel44.SuspendLayout();
            this.packagingboxlayout.SuspendLayout();
            this.packagingboxheader.SuspendLayout();
            this.packagingboxpanel.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel25.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.panel54.SuspendLayout();
            this.panel55.SuspendLayout();
            this.panel56.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel26.SuspendLayout();
            this.panel50.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel27.SuspendLayout();
            this.panel15.SuspendLayout();
            this.machineboxlayout.SuspendLayout();
            this.machineboxheader.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel57.SuspendLayout();
            this.machineboxpanel.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel46.SuspendLayout();
            this.panel13.SuspendLayout();
            this.tblpanl1.SuspendLayout();
            this.printingdetailslayout.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.panel37.SuspendLayout();
            this.panel38.SuspendLayout();
            this.panel39.SuspendLayout();
            this.panel24.SuspendLayout();
            this.panel42.SuspendLayout();
            this.panel43.SuspendLayout();
            this.panel40.SuspendLayout();
            this.panel41.SuspendLayout();
            this.printingdetailsheader.SuspendLayout();
            this.weighboxlayout.SuspendLayout();
            this.weighboxheader.SuspendLayout();
            this.weighboxpanel.SuspendLayout();
            this.lastboxlayout.SuspendLayout();
            this.lastboxpanel.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.lastbxnetwtpanel.SuspendLayout();
            this.lastbxcopspanel.SuspendLayout();
            this.lastbxgrosswtpanel.SuspendLayout();
            this.lastbxtarepanel.SuspendLayout();
            this.lastboxheader.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // shadecode
            // 
            this.shadecode.AutoSize = true;
            this.shadecode.Location = new System.Drawing.Point(0, 5);
            this.shadecode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.shadecode.Name = "shadecode";
            this.shadecode.Size = new System.Drawing.Size(35, 13);
            this.shadecode.TabIndex = 12;
            this.shadecode.Text = "Code:";
            // 
            // boxno
            // 
            this.boxno.AutoSize = true;
            this.boxno.Location = new System.Drawing.Point(0, 0);
            this.boxno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.boxno.Name = "boxno";
            this.boxno.Size = new System.Drawing.Size(25, 26);
            this.boxno.TabIndex = 14;
            this.boxno.Text = "Box\nNo:";
            // 
            // quality
            // 
            this.quality.AutoSize = true;
            this.quality.Location = new System.Drawing.Point(-3, 3);
            this.quality.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.quality.Name = "quality";
            this.quality.Size = new System.Drawing.Size(42, 13);
            this.quality.TabIndex = 18;
            this.quality.Text = "Quality:";
            // 
            // packsize
            // 
            this.packsize.AutoSize = true;
            this.packsize.Location = new System.Drawing.Point(-3, 5);
            this.packsize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.packsize.Name = "packsize";
            this.packsize.Size = new System.Drawing.Size(58, 13);
            this.packsize.TabIndex = 22;
            this.packsize.Text = "Pack Size:";
            // 
            // comport
            // 
            this.comport.AutoSize = true;
            this.comport.Location = new System.Drawing.Point(-3, 2);
            this.comport.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.comport.Name = "comport";
            this.comport.Size = new System.Drawing.Size(29, 26);
            this.comport.TabIndex = 30;
            this.comport.Text = "Com\nPort:";
            this.comport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // remark
            // 
            this.remark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remark.AutoSize = true;
            this.remark.Location = new System.Drawing.Point(0, 5);
            this.remark.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.remark.Name = "remark";
            this.remark.Size = new System.Drawing.Size(52, 13);
            this.remark.TabIndex = 46;
            this.remark.Text = "Remarks:";
            // 
            // remarks
            // 
            this.remarks.AcceptsReturn = true;
            this.remarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remarks.Location = new System.Drawing.Point(52, 0);
            this.remarks.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.remarks.Multiline = true;
            this.remarks.Name = "remarks";
            this.remarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.remarks.Size = new System.Drawing.Size(139, 36);
            this.remarks.TabIndex = 4;
            this.remarks.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // scalemodel
            // 
            this.scalemodel.AutoSize = true;
            this.scalemodel.Location = new System.Drawing.Point(1, 2);
            this.scalemodel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.scalemodel.Name = "scalemodel";
            this.scalemodel.Size = new System.Drawing.Size(38, 26);
            this.scalemodel.TabIndex = 48;
            this.scalemodel.Text = "Weigh\nScale:";
            // 
            // QualityList
            // 
            this.QualityList.AllowDrop = true;
            this.QualityList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QualityList.FormattingEnabled = true;
            this.QualityList.Location = new System.Drawing.Point(55, 0);
            this.QualityList.Margin = new System.Windows.Forms.Padding(2);
            this.QualityList.Name = "QualityList";
            this.QualityList.Size = new System.Drawing.Size(188, 21);
            this.QualityList.TabIndex = 5;
            this.QualityList.SelectedIndexChanged += new System.EventHandler(this.QualityList_SelectedIndexChanged);
            this.QualityList.TextUpdate += new System.EventHandler(this.QualityList_TextUpdate);
            this.QualityList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QualityList_KeyDown);
            this.QualityList.Leave += new System.EventHandler(this.ComboBox_Leave);
            // 
            // PackSizeList
            // 
            this.PackSizeList.AllowDrop = true;
            this.PackSizeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PackSizeList.FormattingEnabled = true;
            this.PackSizeList.Location = new System.Drawing.Point(62, 2);
            this.PackSizeList.Margin = new System.Windows.Forms.Padding(2);
            this.PackSizeList.Name = "PackSizeList";
            this.PackSizeList.Size = new System.Drawing.Size(75, 21);
            this.PackSizeList.TabIndex = 1;
            this.PackSizeList.SelectionChangeCommitted += new System.EventHandler(this.PackSizeList_SelectionChangeCommitted);
            this.PackSizeList.TextUpdate += new System.EventHandler(this.PackSizeList_TextUpdate);
            this.PackSizeList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PackSizeList_KeyDown);
            this.PackSizeList.Leave += new System.EventHandler(this.ComboBox_Leave);
            // 
            // ComPortList
            // 
            this.ComPortList.AllowDrop = true;
            this.ComPortList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComPortList.FormattingEnabled = true;
            this.ComPortList.Location = new System.Drawing.Point(30, 2);
            this.ComPortList.Margin = new System.Windows.Forms.Padding(2);
            this.ComPortList.Name = "ComPortList";
            this.ComPortList.Size = new System.Drawing.Size(34, 21);
            this.ComPortList.TabIndex = 1;
            this.ComPortList.SelectedIndexChanged += new System.EventHandler(this.ComPortList_SelectedIndexChanged);
            this.ComPortList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ComPortList_KeyDown);
            this.ComPortList.Leave += new System.EventHandler(this.ComboBox_Leave);
            // 
            // WeighingList
            // 
            this.WeighingList.AllowDrop = true;
            this.WeighingList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WeighingList.FormattingEnabled = true;
            this.WeighingList.Location = new System.Drawing.Point(42, 3);
            this.WeighingList.Margin = new System.Windows.Forms.Padding(2);
            this.WeighingList.Name = "WeighingList";
            this.WeighingList.Size = new System.Drawing.Size(101, 21);
            this.WeighingList.TabIndex = 1;
            this.WeighingList.SelectedIndexChanged += new System.EventHandler(this.WeighingList_SelectedIndexChanged);
            this.WeighingList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WeighingList_KeyDown);
            this.WeighingList.Leave += new System.EventHandler(this.ComboBox_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "Copies:";
            // 
            // copyno
            // 
            this.copyno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.copyno.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.copyno.Location = new System.Drawing.Point(45, 3);
            this.copyno.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.copyno.Name = "copyno";
            this.copyno.ReadOnly = true;
            this.copyno.Size = new System.Drawing.Size(19, 20);
            this.copyno.TabIndex = 3;
            this.copyno.TabStop = false;
            this.copyno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.copyno.TextChanged += new System.EventHandler(this.CopyNos_TextChanged);
            // 
            // netwt
            // 
            this.netwt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.netwt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.netwt.Location = new System.Drawing.Point(62, 2);
            this.netwt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.netwt.Name = "netwt";
            this.netwt.ReadOnly = true;
            this.netwt.Size = new System.Drawing.Size(8, 20);
            this.netwt.TabIndex = 9;
            this.netwt.TabStop = false;
            this.netwt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.netwt.TextChanged += new System.EventHandler(this.NetWeight_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 82;
            this.label4.Text = "Net Wt:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 80;
            this.label3.Text = "Tare Wt:";
            // 
            // grosswtno
            // 
            this.grosswtno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grosswtno.Location = new System.Drawing.Point(57, 2);
            this.grosswtno.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grosswtno.Name = "grosswtno";
            this.grosswtno.Size = new System.Drawing.Size(13, 20);
            this.grosswtno.TabIndex = 6;
            this.grosswtno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.grosswtno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_EnterKeyMoveNext);
            this.grosswtno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
            this.grosswtno.Leave += new System.EventHandler(this.txtNumeric_Leave);
            this.grosswtno.Validating += new System.ComponentModel.CancelEventHandler(this.GrossWeight_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "Gross Wt:";
            // 
            // palletwtno
            // 
            this.palletwtno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.palletwtno.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.palletwtno.Location = new System.Drawing.Point(55, 2);
            this.palletwtno.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.palletwtno.Name = "palletwtno";
            this.palletwtno.Size = new System.Drawing.Size(8, 20);
            this.palletwtno.TabIndex = 5;
            this.palletwtno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.palletwtno.TextChanged += new System.EventHandler(this.PalletWeight_TextChanged);
            this.palletwtno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_EnterKeyMoveNext);
            this.palletwtno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
            this.palletwtno.Leave += new System.EventHandler(this.txtNumeric_Leave);
            // 
            // palletwt
            // 
            this.palletwt.AutoSize = true;
            this.palletwt.Location = new System.Drawing.Point(-3, 0);
            this.palletwt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.palletwt.Name = "palletwt";
            this.palletwt.Size = new System.Drawing.Size(56, 26);
            this.palletwt.TabIndex = 76;
            this.palletwt.Text = "Box/Pallet\nWt:";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel6.Controls.Add(this.panel35, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.panel28, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.panel29, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.panel34, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.panel45, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.panel32, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.panel33, 1, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.8196F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.35038F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.46027F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.36975F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(212, 102);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // panel35
            // 
            this.panel35.Controls.Add(this.label4);
            this.panel35.Controls.Add(this.netwt);
            this.panel35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel35.Location = new System.Drawing.Point(67, 57);
            this.panel35.Margin = new System.Windows.Forms.Padding(0);
            this.panel35.Name = "panel35";
            this.panel35.Size = new System.Drawing.Size(72, 21);
            this.panel35.TabIndex = 7;
            // 
            // panel28
            // 
            this.panel28.Controls.Add(this.comport);
            this.panel28.Controls.Add(this.ComPortList);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel28.Location = new System.Drawing.Point(0, 0);
            this.panel28.Margin = new System.Windows.Forms.Padding(0);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(67, 27);
            this.panel28.TabIndex = 0;
            // 
            // panel29
            // 
            this.tableLayoutPanel6.SetColumnSpan(this.panel29, 2);
            this.panel29.Controls.Add(this.scalemodel);
            this.panel29.Controls.Add(this.WeighingList);
            this.panel29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel29.Location = new System.Drawing.Point(67, 0);
            this.panel29.Margin = new System.Windows.Forms.Padding(0);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(145, 27);
            this.panel29.TabIndex = 1;
            // 
            // panel34
            // 
            this.panel34.Controls.Add(this.tarewt);
            this.panel34.Controls.Add(this.label3);
            this.panel34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel34.Location = new System.Drawing.Point(0, 57);
            this.panel34.Margin = new System.Windows.Forms.Padding(0);
            this.panel34.Name = "panel34";
            this.panel34.Size = new System.Drawing.Size(67, 21);
            this.panel34.TabIndex = 6;
            // 
            // tarewt
            // 
            this.tarewt.AutoSize = true;
            this.tarewt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tarewt.Location = new System.Drawing.Point(45, 5);
            this.tarewt.Name = "tarewt";
            this.tarewt.Size = new System.Drawing.Size(0, 13);
            this.tarewt.TabIndex = 81;
            this.tarewt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel45
            // 
            this.panel45.Controls.Add(this.copyno);
            this.panel45.Controls.Add(this.copynoerror);
            this.panel45.Controls.Add(this.label1);
            this.panel45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel45.Location = new System.Drawing.Point(0, 78);
            this.panel45.Margin = new System.Windows.Forms.Padding(0);
            this.panel45.Name = "panel45";
            this.panel45.Size = new System.Drawing.Size(67, 24);
            this.panel45.TabIndex = 9;
            // 
            // copynoerror
            // 
            this.copynoerror.AutoSize = true;
            this.copynoerror.ForeColor = System.Drawing.Color.Red;
            this.copynoerror.Location = new System.Drawing.Point(3, 34);
            this.copynoerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.copynoerror.Name = "copynoerror";
            this.copynoerror.Size = new System.Drawing.Size(0, 13);
            this.copynoerror.TabIndex = 99;
            this.copynoerror.Visible = false;
            // 
            // panel32
            // 
            this.panel32.Controls.Add(this.palletwt);
            this.panel32.Controls.Add(this.req8);
            this.panel32.Controls.Add(this.palletwterror);
            this.panel32.Controls.Add(this.palletwtno);
            this.panel32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel32.Location = new System.Drawing.Point(0, 27);
            this.panel32.Margin = new System.Windows.Forms.Padding(0);
            this.panel32.Name = "panel32";
            this.panel32.Size = new System.Drawing.Size(67, 30);
            this.panel32.TabIndex = 4;
            // 
            // req8
            // 
            this.req8.AutoSize = true;
            this.req8.ForeColor = System.Drawing.Color.Red;
            this.req8.Location = new System.Drawing.Point(45, 0);
            this.req8.Name = "req8";
            this.req8.Size = new System.Drawing.Size(11, 13);
            this.req8.TabIndex = 112;
            this.req8.Text = "*";
            this.req8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // palletwterror
            // 
            this.palletwterror.AutoSize = true;
            this.palletwterror.ForeColor = System.Drawing.Color.Red;
            this.palletwterror.Location = new System.Drawing.Point(2, 31);
            this.palletwterror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.palletwterror.Name = "palletwterror";
            this.palletwterror.Size = new System.Drawing.Size(0, 13);
            this.palletwterror.TabIndex = 88;
            this.palletwterror.Visible = false;
            // 
            // panel33
            // 
            this.panel33.Controls.Add(this.grosswterror);
            this.panel33.Controls.Add(this.req9);
            this.panel33.Controls.Add(this.label2);
            this.panel33.Controls.Add(this.grosswtno);
            this.panel33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel33.Location = new System.Drawing.Point(67, 27);
            this.panel33.Margin = new System.Windows.Forms.Padding(0);
            this.panel33.Name = "panel33";
            this.panel33.Size = new System.Drawing.Size(72, 30);
            this.panel33.TabIndex = 5;
            // 
            // grosswterror
            // 
            this.grosswterror.AutoSize = true;
            this.grosswterror.ForeColor = System.Drawing.Color.Red;
            this.grosswterror.Location = new System.Drawing.Point(2, 31);
            this.grosswterror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.grosswterror.Name = "grosswterror";
            this.grosswterror.Size = new System.Drawing.Size(0, 13);
            this.grosswterror.TabIndex = 89;
            this.grosswterror.Visible = false;
            // 
            // req9
            // 
            this.req9.AutoSize = true;
            this.req9.ForeColor = System.Drawing.Color.Red;
            this.req9.Location = new System.Drawing.Point(46, 5);
            this.req9.Name = "req9";
            this.req9.Size = new System.Drawing.Size(11, 13);
            this.req9.TabIndex = 113;
            this.req9.Text = "*";
            this.req9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel30
            // 
            this.panel30.Controls.Add(this.windingerror);
            this.panel30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel30.Location = new System.Drawing.Point(246, 124);
            this.panel30.Margin = new System.Windows.Forms.Padding(0);
            this.panel30.Name = "panel30";
            this.panel30.Size = new System.Drawing.Size(82, 23);
            this.panel30.TabIndex = 6;
            // 
            // windingerror
            // 
            this.windingerror.AutoSize = true;
            this.windingerror.ForeColor = System.Drawing.Color.Red;
            this.windingerror.Location = new System.Drawing.Point(2, 31);
            this.windingerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.windingerror.Name = "windingerror";
            this.windingerror.Size = new System.Drawing.Size(0, 13);
            this.windingerror.TabIndex = 104;
            this.windingerror.Visible = false;
            // 
            // submit
            // 
            this.submit.BackColor = System.Drawing.SystemColors.Highlight;
            this.submit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.submit.Location = new System.Drawing.Point(275, 0);
            this.submit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(81, 24);
            this.submit.TabIndex = 122;
            this.submit.Text = "Save";
            this.submit.UseVisualStyleBackColor = false;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // rightpanel
            // 
            this.rightpanel.AutoScroll = true;
            this.rightpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.rightpanel.Controls.Add(this.datalistpopuppanel);
            this.rightpanel.Controls.Add(this.popuppanel);
            this.rightpanel.Controls.Add(this.buttontablelayout);
            this.rightpanel.Controls.Add(this.tableLayoutPanel3);
            this.rightpanel.Controls.Add(this.tableLayoutPanel1);
            this.rightpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightpanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rightpanel.Location = new System.Drawing.Point(0, 0);
            this.rightpanel.Margin = new System.Windows.Forms.Padding(0);
            this.rightpanel.Name = "rightpanel";
            this.rightpanel.Size = new System.Drawing.Size(909, 520);
            this.rightpanel.TabIndex = 89;
            // 
            // datalistpopuppanel
            // 
            this.datalistpopuppanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.datalistpopuppanel.Controls.Add(this.tableLayoutPanel10);
            this.datalistpopuppanel.Controls.Add(this.dataGridView1);
            this.datalistpopuppanel.Location = new System.Drawing.Point(10, 450);
            this.datalistpopuppanel.Margin = new System.Windows.Forms.Padding(0);
            this.datalistpopuppanel.Name = "datalistpopuppanel";
            this.datalistpopuppanel.Padding = new System.Windows.Forms.Padding(5);
            this.datalistpopuppanel.Size = new System.Drawing.Size(580, 417);
            this.datalistpopuppanel.TabIndex = 126;
            this.datalistpopuppanel.Visible = false;
            this.datalistpopuppanel.Paint += new System.Windows.Forms.PaintEventHandler(this.popuppanel_Paint);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 3;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tableLayoutPanel10.Controls.Add(this.panel18, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.panel22, 2, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(5, 383);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(570, 29);
            this.tableLayoutPanel10.TabIndex = 110;
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.nextbtn);
            this.panel18.Controls.Add(this.lblPageInfo);
            this.panel18.Controls.Add(this.prevbtn);
            this.panel18.Location = new System.Drawing.Point(3, 3);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(235, 23);
            this.panel18.TabIndex = 2;
            // 
            // nextbtn
            // 
            this.nextbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nextbtn.Location = new System.Drawing.Point(189, 0);
            this.nextbtn.Name = "nextbtn";
            this.nextbtn.Size = new System.Drawing.Size(47, 22);
            this.nextbtn.TabIndex = 3;
            this.nextbtn.Text = "Next";
            this.nextbtn.UseVisualStyleBackColor = true;
            this.nextbtn.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Location = new System.Drawing.Point(62, 5);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(0, 13);
            this.lblPageInfo.TabIndex = 4;
            // 
            // prevbtn
            // 
            this.prevbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.prevbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.prevbtn.Location = new System.Drawing.Point(3, 0);
            this.prevbtn.Name = "prevbtn";
            this.prevbtn.Size = new System.Drawing.Size(53, 23);
            this.prevbtn.TabIndex = 2;
            this.prevbtn.Text = "Prev";
            this.prevbtn.UseVisualStyleBackColor = true;
            this.prevbtn.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.closelistbtn);
            this.panel22.Location = new System.Drawing.Point(330, 3);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(237, 23);
            this.panel22.TabIndex = 3;
            // 
            // closelistbtn
            // 
            this.closelistbtn.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.closelistbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closelistbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closelistbtn.ForeColor = System.Drawing.SystemColors.Control;
            this.closelistbtn.Location = new System.Drawing.Point(160, 0);
            this.closelistbtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.closelistbtn.Name = "closelistbtn";
            this.closelistbtn.Size = new System.Drawing.Size(75, 22);
            this.closelistbtn.TabIndex = 1;
            this.closelistbtn.Text = "Close";
            this.closelistbtn.UseVisualStyleBackColor = false;
            this.closelistbtn.Click += new System.EventHandler(this.btnDatalistClosePopup_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 34;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.Location = new System.Drawing.Point(5, 5);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(570, 407);
            this.dataGridView1.TabIndex = 99;
            // 
            // popuppanel
            // 
            this.popuppanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.popuppanel.Controls.Add(this.searchbtn);
            this.popuppanel.Controls.Add(this.closepopupbtn);
            this.popuppanel.Controls.Add(this.panel58);
            this.popuppanel.Location = new System.Drawing.Point(645, 450);
            this.popuppanel.Margin = new System.Windows.Forms.Padding(0);
            this.popuppanel.Name = "popuppanel";
            this.popuppanel.Padding = new System.Windows.Forms.Padding(5);
            this.popuppanel.Size = new System.Drawing.Size(241, 180);
            this.popuppanel.TabIndex = 0;
            this.popuppanel.Visible = false;
            this.popuppanel.Paint += new System.Windows.Forms.PaintEventHandler(this.popuppanel_Paint);
            // 
            // searchbtn
            // 
            this.searchbtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.searchbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.searchbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchbtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.searchbtn.Location = new System.Drawing.Point(27, 150);
            this.searchbtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.searchbtn.Name = "searchbtn";
            this.searchbtn.Size = new System.Drawing.Size(70, 22);
            this.searchbtn.TabIndex = 9;
            this.searchbtn.Text = "Search";
            this.searchbtn.UseVisualStyleBackColor = false;
            this.searchbtn.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // closepopupbtn
            // 
            this.closepopupbtn.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.closepopupbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closepopupbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.closepopupbtn.ForeColor = System.Drawing.SystemColors.Control;
            this.closepopupbtn.Location = new System.Drawing.Point(146, 150);
            this.closepopupbtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.closepopupbtn.Name = "closepopupbtn";
            this.closepopupbtn.Size = new System.Drawing.Size(70, 22);
            this.closepopupbtn.TabIndex = 10;
            this.closepopupbtn.Text = "Close";
            this.closepopupbtn.UseVisualStyleBackColor = false;
            this.closepopupbtn.Click += new System.EventHandler(this.btnClosePopup_Click);
            // 
            // panel58
            // 
            this.panel58.Controls.Add(this.srproddateradiobtn);
            this.panel58.Controls.Add(this.srboxnoradiobtn);
            this.panel58.Controls.Add(this.srdeptradiobtn);
            this.panel58.Controls.Add(this.srlinenoradiobtn);
            this.panel58.Controls.Add(this.dateTimePicker2);
            this.panel58.Controls.Add(this.SrBoxNoList);
            this.panel58.Controls.Add(this.SrDeptList);
            this.panel58.Controls.Add(this.SrLineNoList);
            this.panel58.Controls.Add(this.label10);
            this.panel58.Location = new System.Drawing.Point(7, 14);
            this.panel58.Margin = new System.Windows.Forms.Padding(2);
            this.panel58.Name = "panel58";
            this.panel58.Padding = new System.Windows.Forms.Padding(5);
            this.panel58.Size = new System.Drawing.Size(230, 120);
            this.panel58.TabIndex = 1;
            this.panel58.TabStop = true;
            // 
            // srproddateradiobtn
            // 
            this.srproddateradiobtn.AutoCheck = false;
            this.srproddateradiobtn.AutoSize = true;
            this.srproddateradiobtn.Location = new System.Drawing.Point(0, 92);
            this.srproddateradiobtn.Name = "srproddateradiobtn";
            this.srproddateradiobtn.Size = new System.Drawing.Size(51, 17);
            this.srproddateradiobtn.TabIndex = 7;
            this.srproddateradiobtn.TabStop = true;
            this.srproddateradiobtn.Text = "Date:";
            this.srproddateradiobtn.UseVisualStyleBackColor = true;
            this.srproddateradiobtn.CheckedChanged += new System.EventHandler(this.rbDate_CheckedChanged);
            this.srproddateradiobtn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SrProdDateRadiobtn_KeyDown);
            this.srproddateradiobtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadioButton_MouseDown);
            // 
            // srboxnoradiobtn
            // 
            this.srboxnoradiobtn.AutoCheck = false;
            this.srboxnoradiobtn.AutoSize = true;
            this.srboxnoradiobtn.Location = new System.Drawing.Point(0, 64);
            this.srboxnoradiobtn.Name = "srboxnoradiobtn";
            this.srboxnoradiobtn.Size = new System.Drawing.Size(60, 17);
            this.srboxnoradiobtn.TabIndex = 5;
            this.srboxnoradiobtn.TabStop = true;
            this.srboxnoradiobtn.Text = "BoxNo:";
            this.srboxnoradiobtn.UseVisualStyleBackColor = true;
            this.srboxnoradiobtn.CheckedChanged += new System.EventHandler(this.rbBoxNo_CheckedChanged);
            this.srboxnoradiobtn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SrBoxNoRadiobtn_KeyDown);
            this.srboxnoradiobtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadioButton_MouseDown);
            // 
            // srdeptradiobtn
            // 
            this.srdeptradiobtn.AutoCheck = false;
            this.srdeptradiobtn.AutoSize = true;
            this.srdeptradiobtn.Location = new System.Drawing.Point(0, 36);
            this.srdeptradiobtn.Name = "srdeptradiobtn";
            this.srdeptradiobtn.Size = new System.Drawing.Size(51, 17);
            this.srdeptradiobtn.TabIndex = 3;
            this.srdeptradiobtn.TabStop = true;
            this.srdeptradiobtn.Text = "Dept:";
            this.srdeptradiobtn.UseVisualStyleBackColor = true;
            this.srdeptradiobtn.CheckedChanged += new System.EventHandler(this.rbDepartment_CheckedChanged);
            this.srdeptradiobtn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SrDeptRadiobtn_KeyDown);
            this.srdeptradiobtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadioButton_MouseDown);
            // 
            // srlinenoradiobtn
            // 
            this.srlinenoradiobtn.AutoCheck = false;
            this.srlinenoradiobtn.AutoSize = true;
            this.srlinenoradiobtn.Location = new System.Drawing.Point(0, 8);
            this.srlinenoradiobtn.Name = "srlinenoradiobtn";
            this.srlinenoradiobtn.Size = new System.Drawing.Size(62, 17);
            this.srlinenoradiobtn.TabIndex = 1;
            this.srlinenoradiobtn.TabStop = true;
            this.srlinenoradiobtn.Text = "LineNo:";
            this.srlinenoradiobtn.UseVisualStyleBackColor = true;
            this.srlinenoradiobtn.CheckedChanged += new System.EventHandler(this.rbLineNo_CheckedChanged);
            this.srlinenoradiobtn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SrLineNoRadiobtn_KeyDown);
            this.srlinenoradiobtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadioButton_MouseDown);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(62, 89);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(161, 20);
            this.dateTimePicker2.TabIndex = 8;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.SrProdDate_ValueChanged);
            // 
            // SrBoxNoList
            // 
            this.SrBoxNoList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SrBoxNoList.FormattingEnabled = true;
            this.SrBoxNoList.Location = new System.Drawing.Point(62, 62);
            this.SrBoxNoList.Name = "SrBoxNoList";
            this.SrBoxNoList.Size = new System.Drawing.Size(163, 21);
            this.SrBoxNoList.TabIndex = 6;
            this.SrBoxNoList.SelectionChangeCommitted += new System.EventHandler(this.SrBoxNoList_SelectionChangeCommitted);
            this.SrBoxNoList.TextUpdate += new System.EventHandler(this.SrBoxNoList_TextUpdate);
            this.SrBoxNoList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SrBoxNoList_KeyDown);
            // 
            // SrDeptList
            // 
            this.SrDeptList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SrDeptList.FormattingEnabled = true;
            this.SrDeptList.Location = new System.Drawing.Point(62, 33);
            this.SrDeptList.Name = "SrDeptList";
            this.SrDeptList.Size = new System.Drawing.Size(163, 21);
            this.SrDeptList.TabIndex = 4;
            this.SrDeptList.SelectionChangeCommitted += new System.EventHandler(this.SrDeptList_SelectionChangeCommitted);
            this.SrDeptList.TextUpdate += new System.EventHandler(this.SrDeptList_TextUpdate);
            this.SrDeptList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SrDeptList_KeyDown);
            // 
            // SrLineNoList
            // 
            this.SrLineNoList.AllowDrop = true;
            this.SrLineNoList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SrLineNoList.FormattingEnabled = true;
            this.SrLineNoList.Location = new System.Drawing.Point(62, 5);
            this.SrLineNoList.Margin = new System.Windows.Forms.Padding(2);
            this.SrLineNoList.Name = "SrLineNoList";
            this.SrLineNoList.Size = new System.Drawing.Size(163, 21);
            this.SrLineNoList.TabIndex = 2;
            this.SrLineNoList.SelectionChangeCommitted += new System.EventHandler(this.SrLineNoList_SelectionChangeCommitted);
            this.SrLineNoList.TextUpdate += new System.EventHandler(this.SrLineNoList_TextUpdate);
            this.SrLineNoList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SrLineNoList_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(7, 36);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 98;
            this.label10.Visible = false;
            // 
            // buttontablelayout
            // 
            this.buttontablelayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttontablelayout.ColumnCount = 3;
            this.buttontablelayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.buttontablelayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.buttontablelayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.buttontablelayout.Controls.Add(this.panel21, 1, 0);
            this.buttontablelayout.Location = new System.Drawing.Point(0, 416);
            this.buttontablelayout.Name = "buttontablelayout";
            this.buttontablelayout.RowCount = 1;
            this.buttontablelayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttontablelayout.Size = new System.Drawing.Size(890, 31);
            this.buttontablelayout.TabIndex = 123;
            // 
            // panel21
            // 
            this.panel21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel21.Controls.Add(this.findbtn);
            this.panel21.Controls.Add(this.submit);
            this.panel21.Controls.Add(this.cancelbtn);
            this.panel21.Controls.Add(this.saveprint);
            this.panel21.Location = new System.Drawing.Point(270, 3);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(350, 25);
            this.panel21.TabIndex = 0;
            // 
            // findbtn
            // 
            this.findbtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.findbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.findbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findbtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.findbtn.Location = new System.Drawing.Point(2, 0);
            this.findbtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.findbtn.Name = "findbtn";
            this.findbtn.Size = new System.Drawing.Size(81, 24);
            this.findbtn.TabIndex = 119;
            this.findbtn.Text = "Find";
            this.findbtn.UseVisualStyleBackColor = false;
            this.findbtn.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.cancelbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelbtn.ForeColor = System.Drawing.SystemColors.Control;
            this.cancelbtn.Location = new System.Drawing.Point(184, 1);
            this.cancelbtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(81, 24);
            this.cancelbtn.TabIndex = 121;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = false;
            this.cancelbtn.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // saveprint
            // 
            this.saveprint.BackColor = System.Drawing.SystemColors.Highlight;
            this.saveprint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveprint.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.saveprint.Location = new System.Drawing.Point(93, 1);
            this.saveprint.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.saveprint.Name = "saveprint";
            this.saveprint.Size = new System.Drawing.Size(81, 24);
            this.saveprint.TabIndex = 120;
            this.saveprint.Text = "Save && Print";
            this.saveprint.UseVisualStyleBackColor = false;
            this.saveprint.Click += new System.EventHandler(this.saveprint_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.rowMaterialBox, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1, 323);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(568, 94);
            this.tableLayoutPanel3.TabIndex = 15;
            // 
            // rowMaterialBox
            // 
            this.rowMaterialBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rowMaterialBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.rowMaterialBox.Controls.Add(this.rowMaterialPanel);
            this.rowMaterialBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rowMaterialBox.Location = new System.Drawing.Point(2, 3);
            this.rowMaterialBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rowMaterialBox.Name = "rowMaterialBox";
            this.rowMaterialBox.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rowMaterialBox.Size = new System.Drawing.Size(564, 88);
            this.rowMaterialBox.TabIndex = 10;
            this.rowMaterialBox.TabStop = false;
            this.rowMaterialBox.Text = "Key Raw Material Stock Status";
            // 
            // rowMaterialPanel
            // 
            this.rowMaterialPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rowMaterialPanel.Controls.Add(this.rowMaterial);
            this.rowMaterialPanel.Location = new System.Drawing.Point(7, 16);
            this.rowMaterialPanel.Name = "rowMaterialPanel";
            this.rowMaterialPanel.Size = new System.Drawing.Size(550, 66);
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
            this.rowMaterial.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.rowMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rowMaterial.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.rowMaterial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.rowMaterial.ColumnHeadersHeight = 34;
            this.rowMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.rowMaterial.EnableHeadersVisualStyles = false;
            this.rowMaterial.Location = new System.Drawing.Point(4, 0);
            this.rowMaterial.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rowMaterial.MultiSelect = false;
            this.rowMaterial.Name = "rowMaterial";
            this.rowMaterial.ReadOnly = true;
            this.rowMaterial.RowHeadersVisible = false;
            this.rowMaterial.RowHeadersWidth = 62;
            this.rowMaterial.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.rowMaterial.Size = new System.Drawing.Size(546, 60);
            this.rowMaterial.TabIndex = 2;
            this.rowMaterial.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.panel44, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tblpanl1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(570, 320);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel44
            // 
            this.panel44.Controls.Add(this.packagingboxlayout);
            this.panel44.Controls.Add(this.machineboxlayout);
            this.panel44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel44.Location = new System.Drawing.Point(3, 3);
            this.panel44.Name = "panel44";
            this.panel44.Size = new System.Drawing.Size(336, 314);
            this.panel44.TabIndex = 1;
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
            this.packagingboxlayout.Location = new System.Drawing.Point(0, 180);
            this.packagingboxlayout.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.packagingboxlayout.Name = "packagingboxlayout";
            this.packagingboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.packagingboxlayout.RowCount = 2;
            this.packagingboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.packagingboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89F));
            this.packagingboxlayout.Size = new System.Drawing.Size(336, 134);
            this.packagingboxlayout.TabIndex = 2;
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
            this.packagingboxheader.Size = new System.Drawing.Size(332, 14);
            this.packagingboxheader.TabIndex = 107;
            this.packagingboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.packagingboxheader_Paint);
            this.packagingboxheader.Resize += new System.EventHandler(this.packagingboxheader_Resize);
            // 
            // Packagingboxlbl
            // 
            this.Packagingboxlbl.AutoSize = true;
            this.Packagingboxlbl.Location = new System.Drawing.Point(2, 0);
            this.Packagingboxlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Packagingboxlbl.Name = "Packagingboxlbl";
            this.Packagingboxlbl.Size = new System.Drawing.Size(58, 13);
            this.Packagingboxlbl.TabIndex = 107;
            this.Packagingboxlbl.Text = "Packaging";
            // 
            // packagingboxpanel
            // 
            this.packagingboxpanel.Controls.Add(this.tableLayoutPanel5);
            this.packagingboxpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packagingboxpanel.Location = new System.Drawing.Point(4, 19);
            this.packagingboxpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.packagingboxpanel.Name = "packagingboxpanel";
            this.packagingboxpanel.Size = new System.Drawing.Size(328, 110);
            this.packagingboxpanel.TabIndex = 107;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tableLayoutPanel5.Controls.Add(this.panel25, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel19, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel27, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.panel15, 1, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(328, 110);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // panel25
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.panel25, 2);
            this.panel25.Controls.Add(this.tableLayoutPanel9);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel25.Location = new System.Drawing.Point(0, 36);
            this.panel25.Margin = new System.Windows.Forms.Padding(0);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(328, 36);
            this.panel25.TabIndex = 2;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 3;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel9.Controls.Add(this.panel54, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.panel55, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.panel56, 2, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(328, 36);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // panel54
            // 
            this.panel54.Controls.Add(this.BoxItemList);
            this.panel54.Controls.Add(this.label7);
            this.panel54.Controls.Add(this.boxtype);
            this.panel54.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel54.Location = new System.Drawing.Point(0, 0);
            this.panel54.Margin = new System.Windows.Forms.Padding(0);
            this.panel54.Name = "panel54";
            this.panel54.Size = new System.Drawing.Size(232, 36);
            this.panel54.TabIndex = 0;
            // 
            // BoxItemList
            // 
            this.BoxItemList.AllowDrop = true;
            this.BoxItemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BoxItemList.FormattingEnabled = true;
            this.BoxItemList.Location = new System.Drawing.Point(60, 2);
            this.BoxItemList.Margin = new System.Windows.Forms.Padding(2);
            this.BoxItemList.Name = "BoxItemList";
            this.BoxItemList.Size = new System.Drawing.Size(170, 21);
            this.BoxItemList.TabIndex = 3;
            this.BoxItemList.SelectionChangeCommitted += new System.EventHandler(this.BoxItemList_SelectionChangeCommitted);
            this.BoxItemList.TextUpdate += new System.EventHandler(this.BoxItemList_TextUpdate);
            this.BoxItemList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BoxItemList_KeyDown);
            this.BoxItemList.Leave += new System.EventHandler(this.ComboBox_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(50, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 13);
            this.label7.TabIndex = 118;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // boxtype
            // 
            this.boxtype.AutoSize = true;
            this.boxtype.Location = new System.Drawing.Point(-3, 2);
            this.boxtype.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.boxtype.Name = "boxtype";
            this.boxtype.Size = new System.Drawing.Size(56, 26);
            this.boxtype.TabIndex = 38;
            this.boxtype.Text = "Box/Pallet\nType:";
            // 
            // panel55
            // 
            this.panel55.Controls.Add(this.boxweight);
            this.panel55.Controls.Add(this.boxpalletitemwt);
            this.panel55.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel55.Location = new System.Drawing.Point(232, 0);
            this.panel55.Margin = new System.Windows.Forms.Padding(0);
            this.panel55.Name = "panel55";
            this.panel55.Size = new System.Drawing.Size(45, 36);
            this.panel55.TabIndex = 1;
            // 
            // boxweight
            // 
            this.boxweight.AutoSize = true;
            this.boxweight.Location = new System.Drawing.Point(0, 5);
            this.boxweight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.boxweight.Name = "boxweight";
            this.boxweight.Size = new System.Drawing.Size(24, 13);
            this.boxweight.TabIndex = 40;
            this.boxweight.Text = "Wt:";
            // 
            // boxpalletitemwt
            // 
            this.boxpalletitemwt.AutoSize = true;
            this.boxpalletitemwt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.boxpalletitemwt.Location = new System.Drawing.Point(20, 5);
            this.boxpalletitemwt.Name = "boxpalletitemwt";
            this.boxpalletitemwt.Size = new System.Drawing.Size(0, 13);
            this.boxpalletitemwt.TabIndex = 5;
            this.boxpalletitemwt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel56
            // 
            this.panel56.Controls.Add(this.boxstock);
            this.panel56.Controls.Add(this.boxpalletstock);
            this.panel56.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel56.Location = new System.Drawing.Point(277, 0);
            this.panel56.Margin = new System.Windows.Forms.Padding(0);
            this.panel56.Name = "panel56";
            this.panel56.Size = new System.Drawing.Size(51, 36);
            this.panel56.TabIndex = 2;
            // 
            // boxstock
            // 
            this.boxstock.AutoSize = true;
            this.boxstock.Location = new System.Drawing.Point(-3, 3);
            this.boxstock.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.boxstock.Name = "boxstock";
            this.boxstock.Size = new System.Drawing.Size(56, 26);
            this.boxstock.TabIndex = 42;
            this.boxstock.Text = "Box/Pallet\nStock:";
            // 
            // boxpalletstock
            // 
            this.boxpalletstock.AutoSize = true;
            this.boxpalletstock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.boxpalletstock.Location = new System.Drawing.Point(50, 5);
            this.boxpalletstock.Name = "boxpalletstock";
            this.boxpalletstock.Size = new System.Drawing.Size(0, 16);
            this.boxpalletstock.TabIndex = 124;
            this.boxpalletstock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxpalletstock.UseCompatibleTextRendering = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.panel23, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel26, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel50, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel20, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(137, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(191, 36);
            this.tableLayoutPanel2.TabIndex = 127;
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.uptodenier);
            this.panel23.Controls.Add(this.updenier);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel23.Location = new System.Drawing.Point(43, 0);
            this.panel23.Margin = new System.Windows.Forms.Padding(0);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(51, 36);
            this.panel23.TabIndex = 126;
            // 
            // uptodenier
            // 
            this.uptodenier.AutoSize = true;
            this.uptodenier.Location = new System.Drawing.Point(0, 0);
            this.uptodenier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.uptodenier.Name = "uptodenier";
            this.uptodenier.Size = new System.Drawing.Size(41, 26);
            this.uptodenier.TabIndex = 117;
            this.uptodenier.Text = "Upto\nDenier:";
            // 
            // updenier
            // 
            this.updenier.AutoSize = true;
            this.updenier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.updenier.Location = new System.Drawing.Point(40, 5);
            this.updenier.Name = "updenier";
            this.updenier.Size = new System.Drawing.Size(0, 16);
            this.updenier.TabIndex = 6;
            this.updenier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.updenier.UseCompatibleTextRendering = true;
            // 
            // panel26
            // 
            this.panel26.Controls.Add(this.fromwt);
            this.panel26.Controls.Add(this.frwt);
            this.panel26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel26.Location = new System.Drawing.Point(94, 0);
            this.panel26.Margin = new System.Windows.Forms.Padding(0);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(47, 36);
            this.panel26.TabIndex = 127;
            // 
            // fromwt
            // 
            this.fromwt.AutoSize = true;
            this.fromwt.Location = new System.Drawing.Point(0, 0);
            this.fromwt.Name = "fromwt";
            this.fromwt.Size = new System.Drawing.Size(30, 26);
            this.fromwt.TabIndex = 124;
            this.fromwt.Text = "From\nWt:";
            // 
            // frwt
            // 
            this.frwt.AutoSize = true;
            this.frwt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.frwt.Location = new System.Drawing.Point(30, 5);
            this.frwt.Name = "frwt";
            this.frwt.Size = new System.Drawing.Size(0, 16);
            this.frwt.TabIndex = 124;
            this.frwt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.frwt.UseCompatibleTextRendering = true;
            // 
            // panel50
            // 
            this.panel50.Controls.Add(this.uptowt);
            this.panel50.Controls.Add(this.upwt);
            this.panel50.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel50.Location = new System.Drawing.Point(141, 0);
            this.panel50.Margin = new System.Windows.Forms.Padding(0);
            this.panel50.Name = "panel50";
            this.panel50.Size = new System.Drawing.Size(50, 36);
            this.panel50.TabIndex = 128;
            // 
            // uptowt
            // 
            this.uptowt.AutoSize = true;
            this.uptowt.Location = new System.Drawing.Point(0, 0);
            this.uptowt.Name = "uptowt";
            this.uptowt.Size = new System.Drawing.Size(30, 26);
            this.uptowt.TabIndex = 124;
            this.uptowt.Text = "Upto\nWt:";
            // 
            // upwt
            // 
            this.upwt.AutoSize = true;
            this.upwt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.upwt.Location = new System.Drawing.Point(30, 5);
            this.upwt.Name = "upwt";
            this.upwt.Size = new System.Drawing.Size(0, 16);
            this.upwt.TabIndex = 124;
            this.upwt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.upwt.UseCompatibleTextRendering = true;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.fromdenier);
            this.panel20.Controls.Add(this.frdenier);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(0, 0);
            this.panel20.Margin = new System.Windows.Forms.Padding(0);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(43, 36);
            this.panel20.TabIndex = 4;
            // 
            // fromdenier
            // 
            this.fromdenier.AutoSize = true;
            this.fromdenier.Location = new System.Drawing.Point(0, 0);
            this.fromdenier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fromdenier.Name = "fromdenier";
            this.fromdenier.Size = new System.Drawing.Size(41, 26);
            this.fromdenier.TabIndex = 116;
            this.fromdenier.Text = "From\nDenier:";
            // 
            // frdenier
            // 
            this.frdenier.AutoSize = true;
            this.frdenier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.frdenier.Location = new System.Drawing.Point(37, 5);
            this.frdenier.Name = "frdenier";
            this.frdenier.Size = new System.Drawing.Size(0, 16);
            this.frdenier.TabIndex = 5;
            this.frdenier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.frdenier.UseCompatibleTextRendering = true;
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.packsize);
            this.panel19.Controls.Add(this.req6);
            this.panel19.Controls.Add(this.PackSizeList);
            this.panel19.Controls.Add(this.packsizeerror);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel19.Location = new System.Drawing.Point(0, 0);
            this.panel19.Margin = new System.Windows.Forms.Padding(0);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(137, 36);
            this.panel19.TabIndex = 1;
            // 
            // req6
            // 
            this.req6.AutoSize = true;
            this.req6.ForeColor = System.Drawing.Color.Red;
            this.req6.Location = new System.Drawing.Point(50, 5);
            this.req6.Name = "req6";
            this.req6.Size = new System.Drawing.Size(11, 13);
            this.req6.TabIndex = 110;
            this.req6.Text = "*";
            this.req6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // packsizeerror
            // 
            this.packsizeerror.AutoSize = true;
            this.packsizeerror.ForeColor = System.Drawing.Color.Red;
            this.packsizeerror.Location = new System.Drawing.Point(2, 31);
            this.packsizeerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.packsizeerror.Name = "packsizeerror";
            this.packsizeerror.Size = new System.Drawing.Size(0, 13);
            this.packsizeerror.TabIndex = 103;
            this.packsizeerror.Visible = false;
            // 
            // panel27
            // 
            this.panel27.Controls.Add(this.OwnerList);
            this.panel27.Controls.Add(this.owner);
            this.panel27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel27.Location = new System.Drawing.Point(0, 72);
            this.panel27.Margin = new System.Windows.Forms.Padding(0);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(137, 38);
            this.panel27.TabIndex = 3;
            // 
            // OwnerList
            // 
            this.OwnerList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OwnerList.FormattingEnabled = true;
            this.OwnerList.Location = new System.Drawing.Point(62, 2);
            this.OwnerList.Name = "OwnerList";
            this.OwnerList.Size = new System.Drawing.Size(75, 21);
            this.OwnerList.TabIndex = 3;
            this.OwnerList.SelectedIndexChanged += new System.EventHandler(this.OwnerList_SelectedIndexChanged);
            this.OwnerList.TextUpdate += new System.EventHandler(this.OwnerList_TextUpdate);
            this.OwnerList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OwnerList_KeyDown);
            this.OwnerList.Leave += new System.EventHandler(this.ComboBox_Leave);
            // 
            // owner
            // 
            this.owner.AutoSize = true;
            this.owner.Location = new System.Drawing.Point(-2, 5);
            this.owner.Name = "owner";
            this.owner.Size = new System.Drawing.Size(41, 13);
            this.owner.TabIndex = 124;
            this.owner.Text = "Owner:";
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.remark);
            this.panel15.Controls.Add(this.remarks);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(137, 72);
            this.panel15.Margin = new System.Windows.Forms.Padding(0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(191, 38);
            this.panel15.TabIndex = 4;
            // 
            // machineboxlayout
            // 
            this.machineboxlayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.machineboxlayout.BackColor = System.Drawing.Color.White;
            this.machineboxlayout.ColumnCount = 1;
            this.machineboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.machineboxlayout.Controls.Add(this.machineboxheader, 0, 0);
            this.machineboxlayout.Controls.Add(this.machineboxpanel, 0, 1);
            this.machineboxlayout.Location = new System.Drawing.Point(0, 0);
            this.machineboxlayout.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.machineboxlayout.Name = "machineboxlayout";
            this.machineboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.machineboxlayout.RowCount = 2;
            this.machineboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.machineboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.machineboxlayout.Size = new System.Drawing.Size(336, 179);
            this.machineboxlayout.TabIndex = 1;
            this.machineboxlayout.TabStop = true;
            this.machineboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.machineboxlayout_Paint);
            // 
            // machineboxheader
            // 
            this.machineboxheader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.machineboxheader.Controls.Add(this.tableLayoutPanel11);
            this.machineboxheader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.machineboxheader.Location = new System.Drawing.Point(2, 2);
            this.machineboxheader.Margin = new System.Windows.Forms.Padding(0);
            this.machineboxheader.Name = "machineboxheader";
            this.machineboxheader.Size = new System.Drawing.Size(332, 22);
            this.machineboxheader.TabIndex = 110;
            this.machineboxheader.Resize += new System.EventHandler(this.machineboxheader_Resize);
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 3;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel11.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.panel10, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.panel57, 2, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(332, 22);
            this.tableLayoutPanel11.TabIndex = 0;
            this.tableLayoutPanel11.Paint += new System.Windows.Forms.PaintEventHandler(this.machinetablelayout_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.Machinelbl);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(80, 21);
            this.panel2.TabIndex = 108;
            // 
            // Machinelbl
            // 
            this.Machinelbl.AutoSize = true;
            this.Machinelbl.Location = new System.Drawing.Point(2, 5);
            this.Machinelbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Machinelbl.Name = "Machinelbl";
            this.Machinelbl.Size = new System.Drawing.Size(68, 13);
            this.Machinelbl.TabIndex = 107;
            this.Machinelbl.Text = "Order Details";
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel10.Controls.Add(this.lastbox);
            this.panel10.Controls.Add(this.lastboxno);
            this.panel10.Location = new System.Drawing.Point(83, 0);
            this.panel10.Margin = new System.Windows.Forms.Padding(0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(149, 21);
            this.panel10.TabIndex = 1;
            // 
            // lastbox
            // 
            this.lastbox.AutoSize = true;
            this.lastbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.lastbox.Location = new System.Drawing.Point(55, 5);
            this.lastbox.Name = "lastbox";
            this.lastbox.Size = new System.Drawing.Size(0, 13);
            this.lastbox.TabIndex = 5;
            // 
            // lastboxno
            // 
            this.lastboxno.AutoSize = true;
            this.lastboxno.Location = new System.Drawing.Point(2, 5);
            this.lastboxno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lastboxno.Name = "lastboxno";
            this.lastboxno.Size = new System.Drawing.Size(51, 13);
            this.lastboxno.TabIndex = 6;
            this.lastboxno.Text = "Last Box:";
            // 
            // panel57
            // 
            this.panel57.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel57.Controls.Add(this.packingdate);
            this.panel57.Controls.Add(this.dateTimePicker1);
            this.panel57.Location = new System.Drawing.Point(232, 0);
            this.panel57.Margin = new System.Windows.Forms.Padding(0);
            this.panel57.Name = "panel57";
            this.panel57.Size = new System.Drawing.Size(100, 21);
            this.panel57.TabIndex = 5;
            // 
            // packingdate
            // 
            this.packingdate.AutoSize = true;
            this.packingdate.Location = new System.Drawing.Point(2, 5);
            this.packingdate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.packingdate.Name = "packingdate";
            this.packingdate.Size = new System.Drawing.Size(33, 13);
            this.packingdate.TabIndex = 16;
            this.packingdate.Text = "Date:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(39, 2);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(59, 20);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.TabStop = false;
            // 
            // machineboxpanel
            // 
            this.machineboxpanel.BackColor = System.Drawing.Color.White;
            this.machineboxpanel.Controls.Add(this.tableLayoutPanel4);
            this.machineboxpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.machineboxpanel.Location = new System.Drawing.Point(4, 27);
            this.machineboxpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.machineboxpanel.Name = "machineboxpanel";
            this.machineboxpanel.Size = new System.Drawing.Size(328, 147);
            this.machineboxpanel.TabIndex = 107;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel8, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel16, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel9, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel30, 3, 4);
            this.tableLayoutPanel4.Controls.Add(this.panel11, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel12, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.panel14, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.panel17, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.panel46, 3, 3);
            this.tableLayoutPanel4.Controls.Add(this.panel13, 0, 4);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(328, 147);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lineno);
            this.panel1.Controls.Add(this.req1);
            this.panel1.Controls.Add(this.LineNoList);
            this.panel1.Controls.Add(this.linenoerror);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(82, 29);
            this.panel1.TabIndex = 0;
            // 
            // lineno
            // 
            this.lineno.AutoSize = true;
            this.lineno.Location = new System.Drawing.Point(-3, 3);
            this.lineno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lineno.Name = "lineno";
            this.lineno.Size = new System.Drawing.Size(30, 13);
            this.lineno.TabIndex = 108;
            this.lineno.Text = "Line:";
            // 
            // req1
            // 
            this.req1.AutoSize = true;
            this.req1.ForeColor = System.Drawing.Color.Red;
            this.req1.Location = new System.Drawing.Point(23, 3);
            this.req1.Margin = new System.Windows.Forms.Padding(0);
            this.req1.Name = "req1";
            this.req1.Size = new System.Drawing.Size(11, 13);
            this.req1.TabIndex = 93;
            this.req1.Text = "*";
            this.req1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LineNoList
            // 
            this.LineNoList.AllowDrop = true;
            this.LineNoList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LineNoList.FormattingEnabled = true;
            this.LineNoList.Location = new System.Drawing.Point(34, 0);
            this.LineNoList.Margin = new System.Windows.Forms.Padding(2);
            this.LineNoList.Name = "LineNoList";
            this.LineNoList.Size = new System.Drawing.Size(48, 21);
            this.LineNoList.TabIndex = 1;
            this.LineNoList.SelectedIndexChanged += new System.EventHandler(this.LineNoList_SelectedIndexChanged);
            this.LineNoList.TextUpdate += new System.EventHandler(this.LinoNoList_TextUpdate);
            this.LineNoList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LineNoList_KeyDown);
            this.LineNoList.Leave += new System.EventHandler(this.ComboBox_Leave);
            // 
            // linenoerror
            // 
            this.linenoerror.AutoSize = true;
            this.linenoerror.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.linenoerror.ForeColor = System.Drawing.Color.Red;
            this.linenoerror.Location = new System.Drawing.Point(2, 31);
            this.linenoerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linenoerror.Name = "linenoerror";
            this.linenoerror.Size = new System.Drawing.Size(0, 13);
            this.linenoerror.TabIndex = 98;
            this.linenoerror.Visible = false;
            // 
            // panel8
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.panel8, 2);
            this.panel8.Controls.Add(this.DeptList);
            this.panel8.Controls.Add(this.department);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(82, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(164, 29);
            this.panel8.TabIndex = 2;
            // 
            // DeptList
            // 
            this.DeptList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeptList.FormattingEnabled = true;
            this.DeptList.Location = new System.Drawing.Point(35, 0);
            this.DeptList.Name = "DeptList";
            this.DeptList.Size = new System.Drawing.Size(128, 21);
            this.DeptList.TabIndex = 2;
            this.DeptList.SelectedIndexChanged += new System.EventHandler(this.DeptList_SelectedIndexChanged);
            this.DeptList.TextUpdate += new System.EventHandler(this.DeptList_TextUpdate);
            this.DeptList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DeptList_KeyDown);
            this.DeptList.Leave += new System.EventHandler(this.ComboBox_Leave);
            // 
            // department
            // 
            this.department.AutoSize = true;
            this.department.Location = new System.Drawing.Point(0, 3);
            this.department.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.department.Name = "department";
            this.department.Size = new System.Drawing.Size(33, 13);
            this.department.TabIndex = 2;
            this.department.Text = "Dept:";
            // 
            // panel16
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.panel16, 3);
            this.panel16.Controls.Add(this.quality);
            this.panel16.Controls.Add(this.req4);
            this.panel16.Controls.Add(this.QualityList);
            this.panel16.Controls.Add(this.qualityerror);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(0, 98);
            this.panel16.Margin = new System.Windows.Forms.Padding(0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(246, 26);
            this.panel16.TabIndex = 5;
            // 
            // req4
            // 
            this.req4.AutoSize = true;
            this.req4.ForeColor = System.Drawing.Color.Red;
            this.req4.Location = new System.Drawing.Point(42, 3);
            this.req4.Name = "req4";
            this.req4.Size = new System.Drawing.Size(11, 13);
            this.req4.TabIndex = 108;
            this.req4.Text = "*";
            this.req4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // qualityerror
            // 
            this.qualityerror.AutoSize = true;
            this.qualityerror.ForeColor = System.Drawing.Color.Red;
            this.qualityerror.Location = new System.Drawing.Point(2, 31);
            this.qualityerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.qualityerror.Name = "qualityerror";
            this.qualityerror.Size = new System.Drawing.Size(0, 13);
            this.qualityerror.TabIndex = 101;
            this.qualityerror.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.boxnofrmt);
            this.panel4.Controls.Add(this.req2);
            this.panel4.Controls.Add(this.boxno);
            this.panel4.Controls.Add(this.boxnoerror);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(246, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(82, 29);
            this.panel4.TabIndex = 3;
            // 
            // boxnofrmt
            // 
            this.boxnofrmt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxnofrmt.AutoSize = true;
            this.boxnofrmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.boxnofrmt.Location = new System.Drawing.Point(34, 5);
            this.boxnofrmt.MaximumSize = new System.Drawing.Size(80, 0);
            this.boxnofrmt.Name = "boxnofrmt";
            this.boxnofrmt.Size = new System.Drawing.Size(0, 16);
            this.boxnofrmt.TabIndex = 124;
            this.boxnofrmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxnofrmt.UseCompatibleTextRendering = true;
            // 
            // req2
            // 
            this.req2.AutoSize = true;
            this.req2.ForeColor = System.Drawing.Color.Red;
            this.req2.Location = new System.Drawing.Point(20, 2);
            this.req2.Name = "req2";
            this.req2.Size = new System.Drawing.Size(11, 13);
            this.req2.TabIndex = 106;
            this.req2.Text = "*";
            this.req2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // boxnoerror
            // 
            this.boxnoerror.AutoSize = true;
            this.boxnoerror.ForeColor = System.Drawing.Color.Red;
            this.boxnoerror.Location = new System.Drawing.Point(2, 31);
            this.boxnoerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.boxnoerror.Name = "boxnoerror";
            this.boxnoerror.Size = new System.Drawing.Size(0, 13);
            this.boxnoerror.TabIndex = 105;
            this.boxnoerror.Visible = false;
            // 
            // panel9
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.panel9, 2);
            this.panel9.Controls.Add(this.req3);
            this.panel9.Controls.Add(this.mergeno);
            this.panel9.Controls.Add(this.MergeNoList);
            this.panel9.Controls.Add(this.mergenoerror);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 29);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(164, 36);
            this.panel9.TabIndex = 4;
            // 
            // req3
            // 
            this.req3.AutoSize = true;
            this.req3.ForeColor = System.Drawing.Color.Red;
            this.req3.Location = new System.Drawing.Point(31, 2);
            this.req3.Name = "req3";
            this.req3.Size = new System.Drawing.Size(11, 13);
            this.req3.TabIndex = 107;
            this.req3.Text = "*";
            this.req3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mergeno
            // 
            this.mergeno.AutoSize = true;
            this.mergeno.Location = new System.Drawing.Point(-3, 1);
            this.mergeno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mergeno.Name = "mergeno";
            this.mergeno.Size = new System.Drawing.Size(37, 26);
            this.mergeno.TabIndex = 4;
            this.mergeno.Text = "Merge\nNo:";
            // 
            // MergeNoList
            // 
            this.MergeNoList.AllowDrop = true;
            this.MergeNoList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MergeNoList.FormattingEnabled = true;
            this.MergeNoList.Location = new System.Drawing.Point(42, 0);
            this.MergeNoList.Margin = new System.Windows.Forms.Padding(2);
            this.MergeNoList.Name = "MergeNoList";
            this.MergeNoList.Size = new System.Drawing.Size(122, 21);
            this.MergeNoList.TabIndex = 4;
            this.MergeNoList.SelectionChangeCommitted += new System.EventHandler(this.MergeNoList_SelectionChangeCommitted);
            this.MergeNoList.TextUpdate += new System.EventHandler(this.MergeNoList_TextUpdate);
            this.MergeNoList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MergeNoList_KeyDown);
            this.MergeNoList.Leave += new System.EventHandler(this.ComboBox_Leave);
            // 
            // mergenoerror
            // 
            this.mergenoerror.AutoSize = true;
            this.mergenoerror.ForeColor = System.Drawing.Color.Red;
            this.mergenoerror.Location = new System.Drawing.Point(2, 31);
            this.mergenoerror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mergenoerror.Name = "mergenoerror";
            this.mergenoerror.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mergenoerror.Size = new System.Drawing.Size(0, 13);
            this.mergenoerror.TabIndex = 100;
            this.mergenoerror.Visible = false;
            // 
            // panel11
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.panel11, 2);
            this.panel11.Controls.Add(this.itemname);
            this.panel11.Controls.Add(this.item);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(164, 29);
            this.panel11.Margin = new System.Windows.Forms.Padding(0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(164, 36);
            this.panel11.TabIndex = 6;
            // 
            // itemname
            // 
            this.itemname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemname.AutoSize = true;
            this.itemname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.itemname.Location = new System.Drawing.Point(28, -3);
            this.itemname.MaximumSize = new System.Drawing.Size(195, 0);
            this.itemname.Name = "itemname";
            this.itemname.Size = new System.Drawing.Size(0, 16);
            this.itemname.TabIndex = 5;
            this.itemname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.itemname.UseCompatibleTextRendering = true;
            // 
            // item
            // 
            this.item.AutoSize = true;
            this.item.Location = new System.Drawing.Point(0, 5);
            this.item.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.item.Name = "item";
            this.item.Size = new System.Drawing.Size(30, 13);
            this.item.TabIndex = 8;
            this.item.Text = "Item:";
            // 
            // panel12
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.panel12, 2);
            this.panel12.Controls.Add(this.shadecd);
            this.panel12.Controls.Add(this.shadename);
            this.panel12.Controls.Add(this.shade);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 65);
            this.panel12.Margin = new System.Windows.Forms.Padding(0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(164, 33);
            this.panel12.TabIndex = 7;
            // 
            // shadecd
            // 
            this.shadecd.AutoSize = true;
            this.shadecd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.shadecd.Location = new System.Drawing.Point(38, 5);
            this.shadecd.Name = "shadecd";
            this.shadecd.Size = new System.Drawing.Size(0, 13);
            this.shadecd.TabIndex = 5;
            // 
            // shadename
            // 
            this.shadename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shadename.AutoSize = true;
            this.shadename.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.shadename.Location = new System.Drawing.Point(43, -3);
            this.shadename.MaximumSize = new System.Drawing.Size(200, 0);
            this.shadename.Name = "shadename";
            this.shadename.Size = new System.Drawing.Size(0, 16);
            this.shadename.TabIndex = 5;
            this.shadename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.shadename.UseCompatibleTextRendering = true;
            // 
            // shade
            // 
            this.shade.AutoSize = true;
            this.shade.Location = new System.Drawing.Point(-3, 5);
            this.shade.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.shade.Name = "shade";
            this.shade.Size = new System.Drawing.Size(41, 13);
            this.shade.TabIndex = 10;
            this.shade.Text = "Shade:";
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.deniervalue);
            this.panel14.Controls.Add(this.denier);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(246, 65);
            this.panel14.Margin = new System.Windows.Forms.Padding(0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(82, 33);
            this.panel14.TabIndex = 9;
            // 
            // deniervalue
            // 
            this.deniervalue.AutoSize = true;
            this.deniervalue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.deniervalue.Location = new System.Drawing.Point(38, 5);
            this.deniervalue.Name = "deniervalue";
            this.deniervalue.Size = new System.Drawing.Size(0, 13);
            this.deniervalue.TabIndex = 5;
            // 
            // denier
            // 
            this.denier.AutoSize = true;
            this.denier.Location = new System.Drawing.Point(0, 5);
            this.denier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.denier.Name = "denier";
            this.denier.Size = new System.Drawing.Size(41, 13);
            this.denier.TabIndex = 94;
            this.denier.Text = "Denier:";
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.shadecode);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel17.Location = new System.Drawing.Point(164, 65);
            this.panel17.Margin = new System.Windows.Forms.Padding(0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(82, 33);
            this.panel17.TabIndex = 124;
            // 
            // panel46
            // 
            this.panel46.Controls.Add(this.salelotvalue);
            this.panel46.Controls.Add(this.salelot);
            this.panel46.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel46.Location = new System.Drawing.Point(246, 98);
            this.panel46.Margin = new System.Windows.Forms.Padding(0);
            this.panel46.Name = "panel46";
            this.panel46.Size = new System.Drawing.Size(82, 26);
            this.panel46.TabIndex = 127;
            // 
            // salelotvalue
            // 
            this.salelotvalue.AutoSize = true;
            this.salelotvalue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.salelotvalue.Location = new System.Drawing.Point(45, 5);
            this.salelotvalue.Name = "salelotvalue";
            this.salelotvalue.Size = new System.Drawing.Size(0, 13);
            this.salelotvalue.TabIndex = 124;
            // 
            // salelot
            // 
            this.salelot.AutoSize = true;
            this.salelot.Location = new System.Drawing.Point(0, 5);
            this.salelot.Name = "salelot";
            this.salelot.Size = new System.Drawing.Size(46, 13);
            this.salelot.TabIndex = 124;
            this.salelot.Text = "SaleLot:";
            // 
            // panel13
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.panel13, 2);
            this.panel13.Controls.Add(this.prodtype);
            this.panel13.Controls.Add(this.productiontype);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(0, 124);
            this.panel13.Margin = new System.Windows.Forms.Padding(0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(164, 23);
            this.panel13.TabIndex = 8;
            // 
            // prodtype
            // 
            this.prodtype.AutoSize = true;
            this.prodtype.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.prodtype.Location = new System.Drawing.Point(50, 3);
            this.prodtype.Name = "prodtype";
            this.prodtype.Size = new System.Drawing.Size(0, 13);
            this.prodtype.TabIndex = 5;
            // 
            // productiontype
            // 
            this.productiontype.AutoSize = true;
            this.productiontype.Location = new System.Drawing.Point(-3, 3);
            this.productiontype.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.productiontype.Name = "productiontype";
            this.productiontype.Size = new System.Drawing.Size(59, 13);
            this.productiontype.TabIndex = 44;
            this.productiontype.Text = "Prod Type:";
            // 
            // tblpanl1
            // 
            this.tblpanl1.Controls.Add(this.printingdetailslayout);
            this.tblpanl1.Controls.Add(this.weighboxlayout);
            this.tblpanl1.Controls.Add(this.lastboxlayout);
            this.tblpanl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblpanl1.Location = new System.Drawing.Point(345, 3);
            this.tblpanl1.Name = "tblpanl1";
            this.tblpanl1.Size = new System.Drawing.Size(222, 314);
            this.tblpanl1.TabIndex = 2;
            // 
            // printingdetailslayout
            // 
            this.printingdetailslayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.printingdetailslayout.BackColor = System.Drawing.Color.White;
            this.printingdetailslayout.ColumnCount = 1;
            this.printingdetailslayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.printingdetailslayout.Controls.Add(this.panel3, 0, 1);
            this.printingdetailslayout.Controls.Add(this.printingdetailsheader, 0, 0);
            this.printingdetailslayout.Location = new System.Drawing.Point(0, 0);
            this.printingdetailslayout.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.printingdetailslayout.Name = "printingdetailslayout";
            this.printingdetailslayout.Padding = new System.Windows.Forms.Padding(2);
            this.printingdetailslayout.RowCount = 2;
            this.printingdetailslayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.printingdetailslayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77F));
            this.printingdetailslayout.Size = new System.Drawing.Size(220, 92);
            this.printingdetailslayout.TabIndex = 5;
            this.printingdetailslayout.Paint += new System.Windows.Forms.PaintEventHandler(this.printingdetailslayout_Paint);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 25);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(212, 62);
            this.panel3.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Controls.Add(this.panel37, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel38, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel39, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel24, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.panel42, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.panel43, 2, 1);
            this.tableLayoutPanel7.Controls.Add(this.panel40, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.panel41, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(212, 62);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // panel37
            // 
            this.panel37.Controls.Add(this.prcompany);
            this.panel37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel37.Location = new System.Drawing.Point(0, 0);
            this.panel37.Margin = new System.Windows.Forms.Padding(0);
            this.panel37.Name = "panel37";
            this.panel37.Size = new System.Drawing.Size(70, 20);
            this.panel37.TabIndex = 0;
            // 
            // prcompany
            // 
            this.prcompany.AutoSize = true;
            this.prcompany.Checked = true;
            this.prcompany.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prcompany.Location = new System.Drawing.Point(1, 1);
            this.prcompany.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prcompany.Name = "prcompany";
            this.prcompany.Size = new System.Drawing.Size(94, 17);
            this.prcompany.TabIndex = 0;
            this.prcompany.TabStop = false;
            this.prcompany.Text = "Print Company";
            this.prcompany.UseVisualStyleBackColor = true;
            // 
            // panel38
            // 
            this.panel38.Controls.Add(this.prowner);
            this.panel38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel38.Location = new System.Drawing.Point(70, 0);
            this.panel38.Margin = new System.Windows.Forms.Padding(0);
            this.panel38.Name = "panel38";
            this.panel38.Size = new System.Drawing.Size(70, 20);
            this.panel38.TabIndex = 1;
            // 
            // prowner
            // 
            this.prowner.AutoSize = true;
            this.prowner.Location = new System.Drawing.Point(0, 1);
            this.prowner.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prowner.Name = "prowner";
            this.prowner.Size = new System.Drawing.Size(81, 17);
            this.prowner.TabIndex = 1;
            this.prowner.TabStop = false;
            this.prowner.Text = "Print Owner";
            this.prowner.UseVisualStyleBackColor = true;
            // 
            // panel39
            // 
            this.panel39.Controls.Add(this.prdate);
            this.panel39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel39.Location = new System.Drawing.Point(140, 0);
            this.panel39.Margin = new System.Windows.Forms.Padding(0);
            this.panel39.Name = "panel39";
            this.panel39.Size = new System.Drawing.Size(72, 20);
            this.panel39.TabIndex = 2;
            // 
            // prdate
            // 
            this.prdate.AutoSize = true;
            this.prdate.Checked = true;
            this.prdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prdate.Location = new System.Drawing.Point(1, 1);
            this.prdate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prdate.Name = "prdate";
            this.prdate.Size = new System.Drawing.Size(73, 17);
            this.prdate.TabIndex = 2;
            this.prdate.TabStop = false;
            this.prdate.Text = "Print Date";
            this.prdate.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prdate.UseVisualStyleBackColor = true;
            // 
            // panel24
            // 
            this.panel24.Controls.Add(this.prtwist);
            this.panel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel24.Location = new System.Drawing.Point(70, 40);
            this.panel24.Margin = new System.Windows.Forms.Padding(0);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(70, 22);
            this.panel24.TabIndex = 7;
            // 
            // prtwist
            // 
            this.prtwist.AutoSize = true;
            this.prtwist.Checked = true;
            this.prtwist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prtwist.Location = new System.Drawing.Point(1, 1);
            this.prtwist.Name = "prtwist";
            this.prtwist.Size = new System.Drawing.Size(75, 17);
            this.prtwist.TabIndex = 0;
            this.prtwist.TabStop = false;
            this.prtwist.Text = "Print Twist";
            this.prtwist.UseVisualStyleBackColor = true;
            // 
            // panel42
            // 
            this.panel42.Controls.Add(this.prwtps);
            this.panel42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel42.Location = new System.Drawing.Point(0, 40);
            this.panel42.Margin = new System.Windows.Forms.Padding(0);
            this.panel42.Name = "panel42";
            this.panel42.Size = new System.Drawing.Size(70, 22);
            this.panel42.TabIndex = 6;
            // 
            // prwtps
            // 
            this.prwtps.AutoSize = true;
            this.prwtps.Checked = true;
            this.prwtps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prwtps.Location = new System.Drawing.Point(1, 1);
            this.prwtps.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prwtps.Name = "prwtps";
            this.prwtps.Size = new System.Drawing.Size(87, 17);
            this.prwtps.TabIndex = 6;
            this.prwtps.TabStop = false;
            this.prwtps.Text = "Print WT/PS";
            this.prwtps.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prwtps.UseVisualStyleBackColor = true;
            // 
            // panel43
            // 
            this.panel43.Controls.Add(this.prqrcode);
            this.panel43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel43.Location = new System.Drawing.Point(140, 20);
            this.panel43.Margin = new System.Windows.Forms.Padding(0);
            this.panel43.Name = "panel43";
            this.panel43.Size = new System.Drawing.Size(72, 20);
            this.panel43.TabIndex = 5;
            // 
            // prqrcode
            // 
            this.prqrcode.AutoSize = true;
            this.prqrcode.Checked = true;
            this.prqrcode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prqrcode.Location = new System.Drawing.Point(1, 1);
            this.prqrcode.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prqrcode.Name = "prqrcode";
            this.prqrcode.Size = new System.Drawing.Size(94, 17);
            this.prqrcode.TabIndex = 5;
            this.prqrcode.TabStop = false;
            this.prqrcode.Text = "Print QR Code";
            this.prqrcode.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prqrcode.UseVisualStyleBackColor = true;
            // 
            // panel40
            // 
            this.panel40.Controls.Add(this.prhindi);
            this.panel40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel40.Location = new System.Drawing.Point(70, 20);
            this.panel40.Margin = new System.Windows.Forms.Padding(0);
            this.panel40.Name = "panel40";
            this.panel40.Size = new System.Drawing.Size(70, 20);
            this.panel40.TabIndex = 4;
            // 
            // prhindi
            // 
            this.prhindi.AutoSize = true;
            this.prhindi.Checked = true;
            this.prhindi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prhindi.Location = new System.Drawing.Point(1, 1);
            this.prhindi.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prhindi.Name = "prhindi";
            this.prhindi.Size = new System.Drawing.Size(108, 17);
            this.prhindi.TabIndex = 4;
            this.prhindi.TabStop = false;
            this.prhindi.Text = "Print Hindi Words";
            this.prhindi.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.prhindi.UseVisualStyleBackColor = true;
            // 
            // panel41
            // 
            this.panel41.Controls.Add(this.pruser);
            this.panel41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel41.Location = new System.Drawing.Point(0, 20);
            this.panel41.Margin = new System.Windows.Forms.Padding(0);
            this.panel41.Name = "panel41";
            this.panel41.Size = new System.Drawing.Size(70, 20);
            this.panel41.TabIndex = 3;
            // 
            // pruser
            // 
            this.pruser.AutoSize = true;
            this.pruser.Checked = true;
            this.pruser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pruser.Location = new System.Drawing.Point(1, 1);
            this.pruser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pruser.Name = "pruser";
            this.pruser.Size = new System.Drawing.Size(72, 17);
            this.pruser.TabIndex = 3;
            this.pruser.TabStop = false;
            this.pruser.Text = "Print User";
            this.pruser.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pruser.UseVisualStyleBackColor = true;
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
            this.printingdetailsheader.Size = new System.Drawing.Size(216, 20);
            this.printingdetailsheader.TabIndex = 107;
            this.printingdetailsheader.Paint += new System.Windows.Forms.PaintEventHandler(this.printingdetailsheader_Paint);
            this.printingdetailsheader.Resize += new System.EventHandler(this.printingdetailsheader_Resize);
            // 
            // Printinglbl
            // 
            this.Printinglbl.AutoSize = true;
            this.Printinglbl.Location = new System.Drawing.Point(2, 3);
            this.Printinglbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Printinglbl.Name = "Printinglbl";
            this.Printinglbl.Size = new System.Drawing.Size(77, 13);
            this.Printinglbl.TabIndex = 107;
            this.Printinglbl.Text = "Printing Details";
            // 
            // weighboxlayout
            // 
            this.weighboxlayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.weighboxlayout.BackColor = System.Drawing.Color.White;
            this.weighboxlayout.ColumnCount = 1;
            this.weighboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.weighboxlayout.Controls.Add(this.weighboxheader, 0, 0);
            this.weighboxlayout.Controls.Add(this.weighboxpanel, 0, 1);
            this.weighboxlayout.Location = new System.Drawing.Point(0, 180);
            this.weighboxlayout.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.weighboxlayout.Name = "weighboxlayout";
            this.weighboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.weighboxlayout.RowCount = 2;
            this.weighboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.weighboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86F));
            this.weighboxlayout.Size = new System.Drawing.Size(220, 129);
            this.weighboxlayout.TabIndex = 3;
            this.weighboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.weighboxlayout_Paint);
            // 
            // weighboxheader
            // 
            this.weighboxheader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.weighboxheader.Controls.Add(this.Weighboxlbl);
            this.weighboxheader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weighboxheader.Location = new System.Drawing.Point(2, 2);
            this.weighboxheader.Margin = new System.Windows.Forms.Padding(0);
            this.weighboxheader.Name = "weighboxheader";
            this.weighboxheader.Size = new System.Drawing.Size(216, 17);
            this.weighboxheader.TabIndex = 107;
            this.weighboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.weighboxheader_Paint);
            this.weighboxheader.Resize += new System.EventHandler(this.weighboxheader_Resize);
            // 
            // Weighboxlbl
            // 
            this.Weighboxlbl.AutoSize = true;
            this.Weighboxlbl.Location = new System.Drawing.Point(2, 1);
            this.Weighboxlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Weighboxlbl.Name = "Weighboxlbl";
            this.Weighboxlbl.Size = new System.Drawing.Size(88, 13);
            this.Weighboxlbl.TabIndex = 107;
            this.Weighboxlbl.Text = "Weigh and Label";
            // 
            // weighboxpanel
            // 
            this.weighboxpanel.Controls.Add(this.tableLayoutPanel6);
            this.weighboxpanel.Controls.Add(this.spoolwterror);
            this.weighboxpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weighboxpanel.Location = new System.Drawing.Point(4, 22);
            this.weighboxpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.weighboxpanel.Name = "weighboxpanel";
            this.weighboxpanel.Size = new System.Drawing.Size(212, 102);
            this.weighboxpanel.TabIndex = 107;
            // 
            // spoolwterror
            // 
            this.spoolwterror.AutoSize = true;
            this.spoolwterror.ForeColor = System.Drawing.Color.Red;
            this.spoolwterror.Location = new System.Drawing.Point(2, 30);
            this.spoolwterror.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.spoolwterror.Name = "spoolwterror";
            this.spoolwterror.Size = new System.Drawing.Size(0, 13);
            this.spoolwterror.TabIndex = 87;
            this.spoolwterror.Visible = false;
            // 
            // lastboxlayout
            // 
            this.lastboxlayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lastboxlayout.BackColor = System.Drawing.Color.White;
            this.lastboxlayout.ColumnCount = 1;
            this.lastboxlayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lastboxlayout.Controls.Add(this.lastboxpanel, 0, 1);
            this.lastboxlayout.Controls.Add(this.lastboxheader, 0, 0);
            this.lastboxlayout.Location = new System.Drawing.Point(0, 97);
            this.lastboxlayout.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastboxlayout.Name = "lastboxlayout";
            this.lastboxlayout.Padding = new System.Windows.Forms.Padding(2);
            this.lastboxlayout.RowCount = 2;
            this.lastboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.lastboxlayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.lastboxlayout.Size = new System.Drawing.Size(220, 76);
            this.lastboxlayout.TabIndex = 1;
            this.lastboxlayout.Paint += new System.Windows.Forms.PaintEventHandler(this.lastboxlayout_Paint);
            // 
            // lastboxpanel
            // 
            this.lastboxpanel.Controls.Add(this.tableLayoutPanel8);
            this.lastboxpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastboxpanel.Location = new System.Drawing.Point(4, 23);
            this.lastboxpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastboxpanel.Name = "lastboxpanel";
            this.lastboxpanel.Size = new System.Drawing.Size(212, 48);
            this.lastboxpanel.TabIndex = 107;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 4;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel8.Controls.Add(this.lastbxnetwtpanel, 3, 0);
            this.tableLayoutPanel8.Controls.Add(this.lastbxcopspanel, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.lastbxgrosswtpanel, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.lastbxtarepanel, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(212, 48);
            this.tableLayoutPanel8.TabIndex = 125;
            // 
            // lastbxnetwtpanel
            // 
            this.lastbxnetwtpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxnetwtpanel.Controls.Add(this.netwttxtbox);
            this.lastbxnetwtpanel.Controls.Add(this.netweight);
            this.lastbxnetwtpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastbxnetwtpanel.Location = new System.Drawing.Point(161, 3);
            this.lastbxnetwtpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastbxnetwtpanel.Name = "lastbxnetwtpanel";
            this.lastbxnetwtpanel.Size = new System.Drawing.Size(49, 42);
            this.lastbxnetwtpanel.TabIndex = 8;
            this.lastbxnetwtpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lastbxnetwtpanel_Paint);
            // 
            // netwttxtbox
            // 
            this.netwttxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.netwttxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.netwttxtbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.netwttxtbox.Location = new System.Drawing.Point(7, 23);
            this.netwttxtbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.netwttxtbox.Name = "netwttxtbox";
            this.netwttxtbox.ReadOnly = true;
            this.netwttxtbox.Size = new System.Drawing.Size(57, 13);
            this.netwttxtbox.TabIndex = 95;
            this.netwttxtbox.TabStop = false;
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
            // lastbxcopspanel
            // 
            this.lastbxcopspanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxcopspanel.Controls.Add(this.cops);
            this.lastbxcopspanel.Controls.Add(this.copstxtbox);
            this.lastbxcopspanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastbxcopspanel.Location = new System.Drawing.Point(2, 3);
            this.lastbxcopspanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastbxcopspanel.Name = "lastbxcopspanel";
            this.lastbxcopspanel.Size = new System.Drawing.Size(49, 42);
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
            this.copstxtbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.copstxtbox.Location = new System.Drawing.Point(7, 23);
            this.copstxtbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.copstxtbox.Name = "copstxtbox";
            this.copstxtbox.ReadOnly = true;
            this.copstxtbox.Size = new System.Drawing.Size(45, 13);
            this.copstxtbox.TabIndex = 3;
            this.copstxtbox.TabStop = false;
            this.copstxtbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lastbxgrosswtpanel
            // 
            this.lastbxgrosswtpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxgrosswtpanel.Controls.Add(this.grosswttxtbox);
            this.lastbxgrosswtpanel.Controls.Add(this.grossweight);
            this.lastbxgrosswtpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastbxgrosswtpanel.Location = new System.Drawing.Point(108, 3);
            this.lastbxgrosswtpanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastbxgrosswtpanel.Name = "lastbxgrosswtpanel";
            this.lastbxgrosswtpanel.Size = new System.Drawing.Size(49, 42);
            this.lastbxgrosswtpanel.TabIndex = 6;
            this.lastbxgrosswtpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lastbxgrosswtpanel_Paint);
            // 
            // grosswttxtbox
            // 
            this.grosswttxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.grosswttxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grosswttxtbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.grosswttxtbox.Location = new System.Drawing.Point(7, 23);
            this.grosswttxtbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grosswttxtbox.Name = "grosswttxtbox";
            this.grosswttxtbox.ReadOnly = true;
            this.grosswttxtbox.Size = new System.Drawing.Size(57, 13);
            this.grosswttxtbox.TabIndex = 7;
            this.grosswttxtbox.TabStop = false;
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
            this.lastbxtarepanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.lastbxtarepanel.Controls.Add(this.tarewghttxtbox);
            this.lastbxtarepanel.Controls.Add(this.tareweight);
            this.lastbxtarepanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastbxtarepanel.Location = new System.Drawing.Point(55, 3);
            this.lastbxtarepanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lastbxtarepanel.Name = "lastbxtarepanel";
            this.lastbxtarepanel.Size = new System.Drawing.Size(49, 42);
            this.lastbxtarepanel.TabIndex = 5;
            this.lastbxtarepanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lastbxtarepanel_Paint);
            // 
            // tarewghttxtbox
            // 
            this.tarewghttxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.tarewghttxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tarewghttxtbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tarewghttxtbox.Location = new System.Drawing.Point(7, 23);
            this.tarewghttxtbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tarewghttxtbox.Name = "tarewghttxtbox";
            this.tarewghttxtbox.ReadOnly = true;
            this.tarewghttxtbox.Size = new System.Drawing.Size(57, 13);
            this.tarewghttxtbox.TabIndex = 5;
            this.tarewghttxtbox.TabStop = false;
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
            this.lastboxheader.Size = new System.Drawing.Size(216, 17);
            this.lastboxheader.TabIndex = 107;
            this.lastboxheader.Paint += new System.Windows.Forms.PaintEventHandler(this.lastboxheader_Paint);
            this.lastboxheader.Resize += new System.EventHandler(this.lastboxheader_Resize);
            // 
            // Lastboxlbl
            // 
            this.Lastboxlbl.AutoSize = true;
            this.Lastboxlbl.Location = new System.Drawing.Point(4, 2);
            this.Lastboxlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lastboxlbl.Name = "Lastboxlbl";
            this.Lastboxlbl.Size = new System.Drawing.Size(80, 13);
            this.Lastboxlbl.TabIndex = 107;
            this.Lastboxlbl.Text = "Last box details";
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
            // ModifyChipsPackingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(909, 520);
            this.Controls.Add(this.rightpanel);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "ModifyChipsPackingForm";
            this.Text = "ModifyChipsPackingForm";
            this.Load += new System.EventHandler(this.ModifyChipsPackingForm_Load);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.panel35.ResumeLayout(false);
            this.panel35.PerformLayout();
            this.panel28.ResumeLayout(false);
            this.panel28.PerformLayout();
            this.panel29.ResumeLayout(false);
            this.panel29.PerformLayout();
            this.panel34.ResumeLayout(false);
            this.panel34.PerformLayout();
            this.panel45.ResumeLayout(false);
            this.panel45.PerformLayout();
            this.panel32.ResumeLayout(false);
            this.panel32.PerformLayout();
            this.panel33.ResumeLayout(false);
            this.panel33.PerformLayout();
            this.panel30.ResumeLayout(false);
            this.panel30.PerformLayout();
            this.rightpanel.ResumeLayout(false);
            this.datalistpopuppanel.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panel22.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.popuppanel.ResumeLayout(false);
            this.panel58.ResumeLayout(false);
            this.panel58.PerformLayout();
            this.buttontablelayout.ResumeLayout(false);
            this.panel21.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.rowMaterialBox.ResumeLayout(false);
            this.rowMaterialPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rowMaterial)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel44.ResumeLayout(false);
            this.packagingboxlayout.ResumeLayout(false);
            this.packagingboxheader.ResumeLayout(false);
            this.packagingboxheader.PerformLayout();
            this.packagingboxpanel.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel25.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.panel54.ResumeLayout(false);
            this.panel54.PerformLayout();
            this.panel55.ResumeLayout(false);
            this.panel55.PerformLayout();
            this.panel56.ResumeLayout(false);
            this.panel56.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.panel26.ResumeLayout(false);
            this.panel26.PerformLayout();
            this.panel50.ResumeLayout(false);
            this.panel50.PerformLayout();
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.machineboxlayout.ResumeLayout(false);
            this.machineboxheader.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel57.ResumeLayout(false);
            this.panel57.PerformLayout();
            this.machineboxpanel.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel46.ResumeLayout(false);
            this.panel46.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.tblpanl1.ResumeLayout(false);
            this.printingdetailslayout.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.panel37.ResumeLayout(false);
            this.panel37.PerformLayout();
            this.panel38.ResumeLayout(false);
            this.panel38.PerformLayout();
            this.panel39.ResumeLayout(false);
            this.panel39.PerformLayout();
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.panel42.ResumeLayout(false);
            this.panel42.PerformLayout();
            this.panel43.ResumeLayout(false);
            this.panel43.PerformLayout();
            this.panel40.ResumeLayout(false);
            this.panel40.PerformLayout();
            this.panel41.ResumeLayout(false);
            this.panel41.PerformLayout();
            this.printingdetailsheader.ResumeLayout(false);
            this.printingdetailsheader.PerformLayout();
            this.weighboxlayout.ResumeLayout(false);
            this.weighboxheader.ResumeLayout(false);
            this.weighboxheader.PerformLayout();
            this.weighboxpanel.ResumeLayout(false);
            this.weighboxpanel.PerformLayout();
            this.lastboxlayout.ResumeLayout(false);
            this.lastboxpanel.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.lastbxnetwtpanel.ResumeLayout(false);
            this.lastbxnetwtpanel.PerformLayout();
            this.lastbxcopspanel.ResumeLayout(false);
            this.lastbxcopspanel.PerformLayout();
            this.lastbxgrosswtpanel.ResumeLayout(false);
            this.lastbxgrosswtpanel.PerformLayout();
            this.lastbxtarepanel.ResumeLayout(false);
            this.lastbxtarepanel.PerformLayout();
            this.lastboxheader.ResumeLayout(false);
            this.lastboxheader.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label shadecode;
        private System.Windows.Forms.Label boxno;
        private System.Windows.Forms.Label quality;
        private System.Windows.Forms.Label packsize;
        private System.Windows.Forms.Label comport;
        private System.Windows.Forms.Label remark;
        private System.Windows.Forms.TextBox remarks;
        private System.Windows.Forms.Label scalemodel;
        private System.Windows.Forms.ComboBox QualityList;
        private System.Windows.Forms.ComboBox PackSizeList;
        private System.Windows.Forms.ComboBox ComPortList;
        private System.Windows.Forms.ComboBox WeighingList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox copyno;
        private System.Windows.Forms.Label palletwt;
        private System.Windows.Forms.TextBox palletwtno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox grosswtno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox netwt;
        private System.Windows.Forms.Label label4;
        //private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button submit;
        //private System.Windows.Forms.Button Logout;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.Label role;
        private Panel rightpanel;
        private System.Windows.Forms.Label packsizeerror;
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
        private TableLayoutPanel lastboxlayout;
        private Panel lastboxpanel;
        private Panel lastboxheader;
        private System.Windows.Forms.Label Lastboxlbl;
        private GroupBox rowMaterialBox;
        private DataGridView rowMaterial;
        private Panel rowMaterialPanel;
        private System.Windows.Forms.Label qualityerror;
        private System.Windows.Forms.Label windingerror;
        private System.Windows.Forms.Label req2;
        private System.Windows.Forms.Label req6;
        private System.Windows.Forms.Label req4;
        private System.Windows.Forms.Label req9;
        private System.Windows.Forms.Label req8;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private System.Windows.Forms.Label menu;
        private PictureBox menuBtn;
        private Button saveprint;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel tblpanl1;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private Panel panel4;
        private Panel panel13;
        private TableLayoutPanel tableLayoutPanel5;
        private Panel panel16;
        private Panel panel19;
        private TableLayoutPanel tableLayoutPanel6;
        private Panel panel28;
        private Panel panel29;
        private Panel panel30;
        private Panel panel32;
        private Panel panel33;
        private Panel panel34;
        private Panel panel35;
        //private Panel panel36;
        private Panel panel1;
        private System.Windows.Forms.Label lineno;
        private System.Windows.Forms.Label req1;
        private ComboBox LineNoList;
        private System.Windows.Forms.Label linenoerror;
        private Panel panel8;
        private System.Windows.Forms.Label department;
        private Panel panel9;
        private System.Windows.Forms.Label req3;
        private System.Windows.Forms.Label mergeno;
        private ComboBox MergeNoList;
        private System.Windows.Forms.Label mergenoerror;
        private Panel panel11;
        private System.Windows.Forms.Label item;
        private Panel panel12;
        private System.Windows.Forms.Label shade;
        private Panel panel14;
        private System.Windows.Forms.Label denier;
        private System.Windows.Forms.Label productiontype;
        private System.Windows.Forms.Label itemname;
        private System.Windows.Forms.Label shadename;
        private System.Windows.Forms.Label shadecd;
        private System.Windows.Forms.Label deniervalue;
        private System.Windows.Forms.Label prodtype;
        private Panel panel44;
        private Panel panel45;
        private System.Windows.Forms.Label copynoerror;
        private System.Windows.Forms.Label tarewt;
        //private System.Windows.Forms.Label wtpercop;
        private TableLayoutPanel buttontablelayout;
        private Panel panel21;
        private ComboBox DeptList;
        private Panel panel46;
        private System.Windows.Forms.Label salelotvalue;
        private System.Windows.Forms.Label salelot;
        private Panel panel27;
        private ComboBox OwnerList;
        private System.Windows.Forms.Label owner;
        private Panel panel15;
        private Panel panel17;
        private System.Windows.Forms.Label boxnofrmt;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel23;
        private System.Windows.Forms.Label uptodenier;
        private System.Windows.Forms.Label updenier;
        private Panel panel26;
        private System.Windows.Forms.Label fromwt;
        private System.Windows.Forms.Label frwt;
        private Panel panel50;
        private System.Windows.Forms.Label uptowt;
        private System.Windows.Forms.Label upwt;
        private Panel panel20;
        private System.Windows.Forms.Label fromdenier;
        private System.Windows.Forms.Label frdenier;
        private Panel panel25;
        private TableLayoutPanel tableLayoutPanel9;
        private Panel panel54;
        private ComboBox BoxItemList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label boxtype;
        private Panel panel55;
        private System.Windows.Forms.Label boxweight;
        private System.Windows.Forms.Label boxpalletitemwt;
        private Panel panel56;
        private System.Windows.Forms.Label boxstock;
        private System.Windows.Forms.Label boxpalletstock;
        private TableLayoutPanel printingdetailslayout;
        private Panel panel3;
        private TableLayoutPanel tableLayoutPanel7;
        private Panel panel37;
        private CheckBox prcompany;
        private Panel panel38;
        private CheckBox prowner;
        private Panel panel39;
        private CheckBox prdate;
        private Panel panel24;
        private CheckBox prtwist;
        private Panel panel42;
        private CheckBox prwtps;
        private Panel panel43;
        private CheckBox prqrcode;
        private Panel panel40;
        private CheckBox prhindi;
        private Panel panel41;
        private CheckBox pruser;
        private Panel printingdetailsheader;
        private System.Windows.Forms.Label Printinglbl;
        private Panel machineboxheader;
        private TableLayoutPanel tableLayoutPanel11;
        private Panel panel2;
        private System.Windows.Forms.Label Machinelbl;
        private Panel panel10;
        private System.Windows.Forms.Label lastbox;
        private System.Windows.Forms.Label lastboxno;
        private Panel panel57;
        private System.Windows.Forms.Label packingdate;
        private DateTimePicker dateTimePicker1;
        private Button findbtn;
        private Panel popuppanel;
        private Button searchbtn;
        private Button closepopupbtn;
        private Panel panel58;
        internal RadioButton srproddateradiobtn;
        private RadioButton srboxnoradiobtn;
        private RadioButton srdeptradiobtn;
        private RadioButton srlinenoradiobtn;
        private DateTimePicker dateTimePicker2;
        private ComboBox SrBoxNoList;
        private ComboBox SrDeptList;
        private ComboBox SrLineNoList;
        private System.Windows.Forms.Label label10;
        private Panel datalistpopuppanel;
        private TableLayoutPanel tableLayoutPanel10;
        private Button closelistbtn;
        private DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel8;
        private Panel lastbxnetwtpanel;
        private TextBox netwttxtbox;
        private System.Windows.Forms.Label netweight;
        private Panel lastbxcopspanel;
        private System.Windows.Forms.Label cops;
        private TextBox copstxtbox;
        private Panel lastbxgrosswtpanel;
        private TextBox grosswttxtbox;
        private System.Windows.Forms.Label grossweight;
        private Panel lastbxtarepanel;
        private TextBox tarewghttxtbox;
        private System.Windows.Forms.Label tareweight;
        private Panel panel18;
        private Panel panel22;
        private Button prevbtn;
        private Button nextbtn;
        private System.Windows.Forms.Label lblPageInfo;
        //private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}

