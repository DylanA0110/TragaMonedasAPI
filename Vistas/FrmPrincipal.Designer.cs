namespace WinFormsApp1
{
    partial class FrmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            label2 = new Label();
            label1 = new Label();
            Menu = new ToolStrip();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel2 = new ToolStripLabel();
            btnCli_Emp = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripLabel3 = new ToolStripLabel();
            btnMant = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripLabel4 = new ToolStripLabel();
            btnSala = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            Menu.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Sitka Display", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(324, 124);
            label2.Name = "label2";
            label2.Size = new Size(537, 82);
            label2.TabIndex = 7;
            label2.Text = "Bienvenidos a";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Sitka Display", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(324, 180);
            label1.Name = "label1";
            label1.Size = new Size(537, 100);
            label1.TabIndex = 6;
            label1.Text = "Tragamonedas Nicaragua";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Menu
            // 
            Menu.BackColor = Color.DarkBlue;
            Menu.Items.AddRange(new ToolStripItem[] { toolStripSeparator2, toolStripLabel2, btnCli_Emp, toolStripSeparator1, toolStripLabel3, btnMant, toolStripSeparator3, toolStripLabel4, btnSala, toolStripSeparator4 });
            Menu.Location = new Point(0, 0);
            Menu.Name = "Menu";
            Menu.Size = new Size(1247, 25);
            Menu.TabIndex = 5;
            Menu.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.ForeColor = SystemColors.ButtonHighlight;
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(112, 22);
            toolStripLabel2.Text = "Clientes/Empleados";
            // 
            // btnCli_Emp
            // 
            btnCli_Emp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnCli_Emp.Image = (Image)resources.GetObject("btnCli_Emp.Image");
            btnCli_Emp.ImageTransparentColor = Color.Magenta;
            btnCli_Emp.Name = "btnCli_Emp";
            btnCli_Emp.Size = new Size(23, 22);
            btnCli_Emp.Text = "Datos de Clientes";
            btnCli_Emp.Click += btnCli_Emp_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.ForeColor = SystemColors.ButtonHighlight;
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(89, 22);
            toolStripLabel3.Text = "Mantenimiento";
            // 
            // btnMant
            // 
            btnMant.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnMant.Image = (Image)resources.GetObject("btnMant.Image");
            btnMant.ImageTransparentColor = Color.Magenta;
            btnMant.Name = "btnMant";
            btnMant.Size = new Size(23, 22);
            btnMant.Text = "Mantenimiento";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // toolStripLabel4
            // 
            toolStripLabel4.ForeColor = SystemColors.ButtonHighlight;
            toolStripLabel4.Name = "toolStripLabel4";
            toolStripLabel4.Size = new Size(33, 22);
            toolStripLabel4.Text = "Salas";
            // 
            // btnSala
            // 
            btnSala.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSala.Image = (Image)resources.GetObject("btnSala.Image");
            btnSala.ImageTransparentColor = Color.Magenta;
            btnSala.Name = "btnSala";
            btnSala.Size = new Size(23, 22);
            btnSala.Text = "Info Salas";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1247, 633);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Menu);
            ForeColor = SystemColors.ButtonHighlight;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmPrincipal";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmPrincipal";
            Menu.ResumeLayout(false);
            Menu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private Label label1;
        private ToolStrip Menu;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel toolStripLabel2;
        private ToolStripButton btnCli_Emp;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel toolStripLabel3;
        private ToolStripButton btnMant;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripLabel toolStripLabel4;
        private ToolStripButton btnSala;
        private ToolStripSeparator toolStripSeparator4;
    }
}