
namespace ControlDeRegistroDeEmpleados
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.BotonCargarArchivo = new System.Windows.Forms.Button();
            this.BotonCrearArchivoExcel = new System.Windows.Forms.Button();
            this.dataGridViewPlanilla = new System.Windows.Forms.DataGridView();
            this.BotonGenerarRegistrosJornada = new System.Windows.Forms.Button();
            this.panelFiltrado = new System.Windows.Forms.Panel();
            this.textBoxFiltradoPorDNI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewEscondido = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlanilla)).BeginInit();
            this.panelFiltrado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEscondido)).BeginInit();
            this.SuspendLayout();
            // 
            // BotonCargarArchivo
            // 
            this.BotonCargarArchivo.BackColor = System.Drawing.Color.Gold;
            this.BotonCargarArchivo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BotonCargarArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonCargarArchivo.Location = new System.Drawing.Point(13, 13);
            this.BotonCargarArchivo.Name = "BotonCargarArchivo";
            this.BotonCargarArchivo.Size = new System.Drawing.Size(99, 23);
            this.BotonCargarArchivo.TabIndex = 0;
            this.BotonCargarArchivo.Text = "Cargar Archivo";
            this.BotonCargarArchivo.UseVisualStyleBackColor = false;
            this.BotonCargarArchivo.Click += new System.EventHandler(this.BotonCargarArchivo_Click);
            // 
            // BotonCrearArchivoExcel
            // 
            this.BotonCrearArchivoExcel.BackColor = System.Drawing.Color.Gold;
            this.BotonCrearArchivoExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BotonCrearArchivoExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonCrearArchivoExcel.Location = new System.Drawing.Point(217, 283);
            this.BotonCrearArchivoExcel.Name = "BotonCrearArchivoExcel";
            this.BotonCrearArchivoExcel.Size = new System.Drawing.Size(176, 23);
            this.BotonCrearArchivoExcel.TabIndex = 1;
            this.BotonCrearArchivoExcel.Text = "Ver lista de Empleados";
            this.BotonCrearArchivoExcel.UseVisualStyleBackColor = false;
            this.BotonCrearArchivoExcel.Visible = false;
            this.BotonCrearArchivoExcel.Click += new System.EventHandler(this.BotonCrearArchivoExcel_Click);
            // 
            // dataGridViewPlanilla
            // 
            this.dataGridViewPlanilla.AllowUserToAddRows = false;
            this.dataGridViewPlanilla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPlanilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlanilla.Location = new System.Drawing.Point(13, 40);
            this.dataGridViewPlanilla.Name = "dataGridViewPlanilla";
            this.dataGridViewPlanilla.Size = new System.Drawing.Size(380, 237);
            this.dataGridViewPlanilla.TabIndex = 2;
            // 
            // BotonGenerarRegistrosJornada
            // 
            this.BotonGenerarRegistrosJornada.BackColor = System.Drawing.Color.Gold;
            this.BotonGenerarRegistrosJornada.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BotonGenerarRegistrosJornada.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonGenerarRegistrosJornada.Location = new System.Drawing.Point(13, 283);
            this.BotonGenerarRegistrosJornada.Name = "BotonGenerarRegistrosJornada";
            this.BotonGenerarRegistrosJornada.Size = new System.Drawing.Size(176, 23);
            this.BotonGenerarRegistrosJornada.TabIndex = 3;
            this.BotonGenerarRegistrosJornada.Text = "Generar registros de jornada";
            this.BotonGenerarRegistrosJornada.UseVisualStyleBackColor = false;
            this.BotonGenerarRegistrosJornada.Click += new System.EventHandler(this.BotonGenerarRegistrosJornada_Click_1);
            // 
            // panelFiltrado
            // 
            this.panelFiltrado.BackColor = System.Drawing.Color.Gold;
            this.panelFiltrado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFiltrado.Controls.Add(this.textBoxFiltradoPorDNI);
            this.panelFiltrado.Controls.Add(this.label1);
            this.panelFiltrado.Location = new System.Drawing.Point(13, 313);
            this.panelFiltrado.Name = "panelFiltrado";
            this.panelFiltrado.Size = new System.Drawing.Size(380, 31);
            this.panelFiltrado.TabIndex = 4;
            // 
            // textBoxFiltradoPorDNI
            // 
            this.textBoxFiltradoPorDNI.Location = new System.Drawing.Point(90, 4);
            this.textBoxFiltradoPorDNI.Name = "textBoxFiltradoPorDNI";
            this.textBoxFiltradoPorDNI.Size = new System.Drawing.Size(163, 20);
            this.textBoxFiltradoPorDNI.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtrar por DNI";
            // 
            // dataGridViewEscondido
            // 
            this.dataGridViewEscondido.AllowUserToAddRows = false;
            this.dataGridViewEscondido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEscondido.BackgroundColor = System.Drawing.Color.PeachPuff;
            this.dataGridViewEscondido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEscondido.Enabled = false;
            this.dataGridViewEscondido.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridViewEscondido.Location = new System.Drawing.Point(13, 40);
            this.dataGridViewEscondido.Name = "dataGridViewEscondido";
            this.dataGridViewEscondido.RowHeadersVisible = false;
            this.dataGridViewEscondido.Size = new System.Drawing.Size(380, 237);
            this.dataGridViewEscondido.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(408, 353);
            this.Controls.Add(this.dataGridViewEscondido);
            this.Controls.Add(this.panelFiltrado);
            this.Controls.Add(this.BotonGenerarRegistrosJornada);
            this.Controls.Add(this.dataGridViewPlanilla);
            this.Controls.Add(this.BotonCrearArchivoExcel);
            this.Controls.Add(this.BotonCargarArchivo);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control de Registro de Empleados";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlanilla)).EndInit();
            this.panelFiltrado.ResumeLayout(false);
            this.panelFiltrado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEscondido)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BotonCargarArchivo;
        private System.Windows.Forms.Button BotonCrearArchivoExcel;
        private System.Windows.Forms.DataGridView dataGridViewPlanilla;
        private System.Windows.Forms.Button BotonGenerarRegistrosJornada;
        private System.Windows.Forms.Panel panelFiltrado;
        private System.Windows.Forms.TextBox textBoxFiltradoPorDNI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewEscondido;
    }
}

