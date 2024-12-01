namespace WinFormsApp1
{
    partial class FrmAñadirEmpleado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAñadirEmpleado));
            txtNombres = new TextBox();
            txtApellidos = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtPuesto = new TextBox();
            label5 = new Label();
            label6 = new Label();
            txtTurno = new TextBox();
            txtSala = new TextBox();
            button1 = new Button();
            btnAggE = new Button();
            label7 = new Label();
            SuspendLayout();
            // 
            // txtNombres
            // 
            txtNombres.Location = new Point(124, 113);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(175, 23);
            txtNombres.TabIndex = 0;
            // 
            // txtApellidos
            // 
            txtApellidos.Location = new Point(124, 181);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(175, 23);
            txtApellidos.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(37, 121);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 2;
            label1.Text = "Nombre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Location = new Point(37, 189);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 3;
            label2.Text = "Apellido:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(37, 263);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 4;
            label3.Text = "Puesto:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(37, 329);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 5;
            label4.Text = "Turno:";
            // 
            // txtPuesto
            // 
            txtPuesto.Location = new Point(124, 255);
            txtPuesto.Name = "txtPuesto";
            txtPuesto.Size = new Size(175, 23);
            txtPuesto.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Location = new Point(190, 312);
            label5.Name = "label5";
            label5.Size = new Size(0, 15);
            label5.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Location = new Point(37, 384);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 8;
            label6.Text = "Sala:";
            // 
            // txtTurno
            // 
            txtTurno.Location = new Point(124, 321);
            txtTurno.Name = "txtTurno";
            txtTurno.Size = new Size(175, 23);
            txtTurno.TabIndex = 9;
            // 
            // txtSala
            // 
            txtSala.Location = new Point(124, 376);
            txtSala.Name = "txtSala";
            txtSala.Size = new Size(175, 23);
            txtSala.TabIndex = 10;
            // 
            // button1
            // 
            button1.ForeColor = SystemColors.ActiveCaptionText;
            button1.Location = new Point(14, 475);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 11;
            button1.Text = "Volver";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnAggE
            // 
            btnAggE.ForeColor = SystemColors.ActiveCaptionText;
            btnAggE.Location = new Point(357, 451);
            btnAggE.Name = "btnAggE";
            btnAggE.Size = new Size(75, 47);
            btnAggE.TabIndex = 12;
            btnAggE.Text = "Ingresar";
            btnAggE.UseVisualStyleBackColor = true;
            btnAggE.Click += btnAggE_Click;
            // 
            // label7
            // 
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Sitka Display", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.ButtonHighlight;
            label7.Location = new Point(12, 9);
            label7.Name = "label7";
            label7.Size = new Size(365, 68);
            label7.TabIndex = 13;
            label7.Text = "Tragamonedas Nicaragua";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmAñadirEmpleado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(464, 516);
            Controls.Add(label7);
            Controls.Add(btnAggE);
            Controls.Add(button1);
            Controls.Add(txtSala);
            Controls.Add(txtTurno);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtPuesto);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtApellidos);
            Controls.Add(txtNombres);
            ForeColor = SystemColors.ButtonHighlight;
            Name = "FrmAñadirEmpleado";
            Text = "FrmAñadirEmpleado";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombres;
        private TextBox txtApellidos;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtPuesto;
        private Label label5;
        private Label label6;
        private TextBox txtTurno;
        private TextBox txtSala;
        private Button button1;
        private Button btnAggE;
        private Label label7;
    }
}