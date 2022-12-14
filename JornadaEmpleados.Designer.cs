namespace ControlDeRegistroDeEmpleados
{
    partial class JornadaEmpleados
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
            this.dataGridViewRegistros = new System.Windows.Forms.DataGridView();
            this.labelNombre = new System.Windows.Forms.Label();
            this.labelDNI = new System.Windows.Forms.Label();
            this.labelHorasSemanales = new System.Windows.Forms.Label();
            this.BotonSalir = new System.Windows.Forms.Button();
            this.BotonVerCalendario = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelFaltas = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegistros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewRegistros
            // 
            this.dataGridViewRegistros.AllowUserToAddRows = false;
            this.dataGridViewRegistros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRegistros.BackgroundColor = System.Drawing.Color.BurlyWood;
            this.dataGridViewRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRegistros.Location = new System.Drawing.Point(12, 153);
            this.dataGridViewRegistros.MultiSelect = false;
            this.dataGridViewRegistros.Name = "dataGridViewRegistros";
            this.dataGridViewRegistros.ReadOnly = true;
            this.dataGridViewRegistros.RowHeadersVisible = false;
            this.dataGridViewRegistros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRegistros.Size = new System.Drawing.Size(394, 287);
            this.dataGridViewRegistros.TabIndex = 0;
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNombre.Location = new System.Drawing.Point(3, 9);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(70, 25);
            this.labelNombre.TabIndex = 2;
            this.labelNombre.Text = "label1";
            // 
            // labelDNI
            // 
            this.labelDNI.AutoSize = true;
            this.labelDNI.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDNI.Location = new System.Drawing.Point(3, 38);
            this.labelDNI.Name = "labelDNI";
            this.labelDNI.Size = new System.Drawing.Size(70, 25);
            this.labelDNI.TabIndex = 2;
            this.labelDNI.Text = "label1";
            // 
            // labelHorasSemanales
            // 
            this.labelHorasSemanales.AutoSize = true;
            this.labelHorasSemanales.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHorasSemanales.Location = new System.Drawing.Point(3, 70);
            this.labelHorasSemanales.Name = "labelHorasSemanales";
            this.labelHorasSemanales.Size = new System.Drawing.Size(70, 25);
            this.labelHorasSemanales.TabIndex = 2;
            this.labelHorasSemanales.Text = "label1";
            // 
            // BotonSalir
            // 
            this.BotonSalir.BackColor = System.Drawing.Color.Gold;
            this.BotonSalir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BotonSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonSalir.Location = new System.Drawing.Point(12, 446);
            this.BotonSalir.Name = "BotonSalir";
            this.BotonSalir.Size = new System.Drawing.Size(98, 23);
            this.BotonSalir.TabIndex = 3;
            this.BotonSalir.Text = "Atras";
            this.BotonSalir.UseVisualStyleBackColor = false;
            this.BotonSalir.Click += new System.EventHandler(this.BotonSalir_Click);
            // 
            // BotonVerCalendario
            // 
            this.BotonVerCalendario.BackColor = System.Drawing.Color.Gold;
            this.BotonVerCalendario.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BotonVerCalendario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonVerCalendario.Location = new System.Drawing.Point(308, 446);
            this.BotonVerCalendario.Name = "BotonVerCalendario";
            this.BotonVerCalendario.Size = new System.Drawing.Size(98, 23);
            this.BotonVerCalendario.TabIndex = 4;
            this.BotonVerCalendario.Text = "Ver calendario";
            this.BotonVerCalendario.UseVisualStyleBackColor = false;
            this.BotonVerCalendario.Click += new System.EventHandler(this.BotonVerCalendario_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ControlDeRegistroDeEmpleados.Properties.Resources.foto_usuario;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelNombre);
            this.panel1.Controls.Add(this.labelDNI);
            this.panel1.Controls.Add(this.labelFaltas);
            this.panel1.Controls.Add(this.labelHorasSemanales);
            this.panel1.Location = new System.Drawing.Point(138, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 135);
            this.panel1.TabIndex = 5;
            // 
            // labelFaltas
            // 
            this.labelFaltas.AutoSize = true;
            this.labelFaltas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFaltas.Location = new System.Drawing.Point(3, 106);
            this.labelFaltas.Name = "labelFaltas";
            this.labelFaltas.Size = new System.Drawing.Size(70, 25);
            this.labelFaltas.TabIndex = 2;
            this.labelFaltas.Text = "label1";
            // 
            // JornadaEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(418, 481);
            this.Controls.Add(this.BotonVerCalendario);
            this.Controls.Add(this.BotonSalir);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridViewRegistros);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JornadaEmpleados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JornadaEmpleados";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.JornadaEmpleados_FormClosed_1);
            this.Load += new System.EventHandler(this.JornadaEmpleados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegistros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewRegistros;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.Label labelDNI;
        private System.Windows.Forms.Label labelHorasSemanales;
        private System.Windows.Forms.Button BotonSalir;
        private System.Windows.Forms.Button BotonVerCalendario;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelFaltas;
    }
}