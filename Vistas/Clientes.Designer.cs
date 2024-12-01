namespace WinFormsApp1
{
    partial class Clientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Clientes));
            btnBuscar = new Button();
            label1 = new Label();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            label2 = new Label();
            button1 = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnAñadirC = new Button();
            btnAñadirE = new Button();
            dtgClientes = new DataGridView();
            dgvEmpleado = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dtgClientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEmpleado).BeginInit();
            SuspendLayout();
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(168, 227);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(91, 31);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Ordenar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(34, 176);
            label1.Name = "label1";
            label1.Size = new Size(74, 15);
            label1.TabIndex = 2;
            label1.Text = "Ordenar por:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(114, 176);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(145, 23);
            comboBox1.TabIndex = 3;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(536, 96);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(145, 23);
            comboBox2.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(459, 79);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 6;
            label2.Text = "Ordenar por:";
            // 
            // button1
            // 
            button1.Location = new Point(680, 150);
            button1.Name = "button1";
            button1.Size = new Size(91, 31);
            button1.TabIndex = 5;
            button1.Text = "Ordenar";
            button1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Sitka Display", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(12, 19);
            label3.Name = "label3";
            label3.Size = new Size(420, 100);
            label3.TabIndex = 8;
            label3.Text = "Tragamonedas Nicaragua";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Sitka Display", 11.249999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.Window;
            label4.Location = new Point(161, 125);
            label4.Name = "label4";
            label4.Size = new Size(58, 21);
            label4.TabIndex = 9;
            label4.Text = "Clientes";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Sitka Display", 11.249999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.Window;
            label5.Location = new Point(570, 42);
            label5.Name = "label5";
            label5.Size = new Size(76, 21);
            label5.TabIndex = 10;
            label5.Text = "Empleados";
            // 
            // btnAñadirC
            // 
            btnAñadirC.Location = new Point(279, 243);
            btnAñadirC.Name = "btnAñadirC";
            btnAñadirC.Size = new Size(91, 31);
            btnAñadirC.TabIndex = 11;
            btnAñadirC.Text = "Añadir Cliente";
            btnAñadirC.UseVisualStyleBackColor = true;
            btnAñadirC.Click += btnAñadirC_Click;
            // 
            // btnAñadirE
            // 
            btnAñadirE.Location = new Point(570, 150);
            btnAñadirE.Name = "btnAñadirE";
            btnAñadirE.Size = new Size(91, 41);
            btnAñadirE.TabIndex = 12;
            btnAñadirE.Text = "Aladir Empleado";
            btnAñadirE.UseVisualStyleBackColor = true;
            // 
            // dtgClientes
            // 
            dtgClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgClientes.Location = new Point(22, 333);
            dtgClientes.Name = "dtgClientes";
            dtgClientes.Size = new Size(369, 252);
            dtgClientes.TabIndex = 13;
            // 
            // dgvEmpleado
            // 
            dgvEmpleado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEmpleado.Location = new Point(459, 333);
            dgvEmpleado.Name = "dgvEmpleado";
            dgvEmpleado.Size = new Size(369, 252);
            dgvEmpleado.TabIndex = 14;
            // 
            // Clientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(860, 608);
            Controls.Add(dgvEmpleado);
            Controls.Add(dtgClientes);
            Controls.Add(btnAñadirE);
            Controls.Add(btnAñadirC);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(comboBox2);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(btnBuscar);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "Clientes";
            Text = "Clientes";
            Load += Clientes_Load;
            ((System.ComponentModel.ISupportInitialize)dtgClientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEmpleado).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnBuscar;
        private Label label1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Label label2;
        private Button button1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnAñadirC;
        private Button btnAñadirE;
        private DataGridView dtgClientes;
        private DataGridView dgvEmpleado;
    }
}