namespace Algoritmo_PSO_Problema_PHUB
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
            this.Pnl_Head = new System.Windows.Forms.Panel();
            this.lbcapacidad = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_iteraciones = new System.Windows.Forms.Label();
            this.Btn_GenerarPso = new System.Windows.Forms.Button();
            this.Txt_NumeroIteraciones = new System.Windows.Forms.TextBox();
            this.Btn_SelecciónDatos = new System.Windows.Forms.Button();
            this.Pbx_Nodos = new System.Windows.Forms.PictureBox();
            this.txtSoluciones = new System.Windows.Forms.RichTextBox();
            this.Txt_MejorSolucion = new System.Windows.Forms.RichTextBox();
            this.dtdemandas = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.demanda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pnl_Head.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Nodos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdemandas)).BeginInit();
            this.SuspendLayout();
            // 
            // Pnl_Head
            // 
            this.Pnl_Head.BackColor = System.Drawing.Color.Gainsboro;
            this.Pnl_Head.Controls.Add(this.lbcapacidad);
            this.Pnl_Head.Controls.Add(this.label1);
            this.Pnl_Head.Controls.Add(this.lbl_iteraciones);
            this.Pnl_Head.Controls.Add(this.Btn_GenerarPso);
            this.Pnl_Head.Controls.Add(this.Txt_NumeroIteraciones);
            this.Pnl_Head.Controls.Add(this.Btn_SelecciónDatos);
            this.Pnl_Head.Location = new System.Drawing.Point(0, 2);
            this.Pnl_Head.Name = "Pnl_Head";
            this.Pnl_Head.Size = new System.Drawing.Size(965, 78);
            this.Pnl_Head.TabIndex = 0;
            // 
            // lbcapacidad
            // 
            this.lbcapacidad.AutoSize = true;
            this.lbcapacidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbcapacidad.Location = new System.Drawing.Point(399, 35);
            this.lbcapacidad.Name = "lbcapacidad";
            this.lbcapacidad.Size = new System.Drawing.Size(16, 16);
            this.lbcapacidad.TabIndex = 5;
            this.lbcapacidad.Text = "0";
            this.lbcapacidad.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(399, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Capacidad de los Hubs";
            // 
            // lbl_iteraciones
            // 
            this.lbl_iteraciones.AutoSize = true;
            this.lbl_iteraciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_iteraciones.Location = new System.Drawing.Point(616, 10);
            this.lbl_iteraciones.Name = "lbl_iteraciones";
            this.lbl_iteraciones.Size = new System.Drawing.Size(105, 16);
            this.lbl_iteraciones.TabIndex = 3;
            this.lbl_iteraciones.Text = "N° Iteraciones";
            // 
            // Btn_GenerarPso
            // 
            this.Btn_GenerarPso.BackColor = System.Drawing.Color.PaleGreen;
            this.Btn_GenerarPso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_GenerarPso.Location = new System.Drawing.Point(775, 19);
            this.Btn_GenerarPso.Name = "Btn_GenerarPso";
            this.Btn_GenerarPso.Size = new System.Drawing.Size(104, 36);
            this.Btn_GenerarPso.TabIndex = 2;
            this.Btn_GenerarPso.Text = "Generar";
            this.Btn_GenerarPso.UseVisualStyleBackColor = false;
            this.Btn_GenerarPso.Click += new System.EventHandler(this.Btn_GenerarPso_Click);
            // 
            // Txt_NumeroIteraciones
            // 
            this.Txt_NumeroIteraciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_NumeroIteraciones.Location = new System.Drawing.Point(598, 29);
            this.Txt_NumeroIteraciones.Name = "Txt_NumeroIteraciones";
            this.Txt_NumeroIteraciones.Size = new System.Drawing.Size(143, 26);
            this.Txt_NumeroIteraciones.TabIndex = 1;
            this.Txt_NumeroIteraciones.Text = "100";
            this.Txt_NumeroIteraciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Btn_SelecciónDatos
            // 
            this.Btn_SelecciónDatos.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Btn_SelecciónDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_SelecciónDatos.Location = new System.Drawing.Point(83, 19);
            this.Btn_SelecciónDatos.Name = "Btn_SelecciónDatos";
            this.Btn_SelecciónDatos.Size = new System.Drawing.Size(173, 36);
            this.Btn_SelecciónDatos.TabIndex = 0;
            this.Btn_SelecciónDatos.Text = "Seleccionar datos";
            this.Btn_SelecciónDatos.UseVisualStyleBackColor = false;
            this.Btn_SelecciónDatos.Click += new System.EventHandler(this.Btn_SelecciónDatos_Click);
            // 
            // Pbx_Nodos
            // 
            this.Pbx_Nodos.BackColor = System.Drawing.Color.LightCyan;
            this.Pbx_Nodos.Location = new System.Drawing.Point(3, 86);
            this.Pbx_Nodos.Name = "Pbx_Nodos";
            this.Pbx_Nodos.Size = new System.Drawing.Size(547, 521);
            this.Pbx_Nodos.TabIndex = 3;
            this.Pbx_Nodos.TabStop = false;
            this.Pbx_Nodos.Paint += new System.Windows.Forms.PaintEventHandler(this.Pbx_Nodos_Paint);
            // 
            // txtSoluciones
            // 
            this.txtSoluciones.BackColor = System.Drawing.Color.PowderBlue;
            this.txtSoluciones.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.txtSoluciones.Location = new System.Drawing.Point(655, 86);
            this.txtSoluciones.Name = "txtSoluciones";
            this.txtSoluciones.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtSoluciones.Size = new System.Drawing.Size(310, 285);
            this.txtSoluciones.TabIndex = 4;
            this.txtSoluciones.Text = "";
            // 
            // Txt_MejorSolucion
            // 
            this.Txt_MejorSolucion.BackColor = System.Drawing.Color.PaleGreen;
            this.Txt_MejorSolucion.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Txt_MejorSolucion.Location = new System.Drawing.Point(655, 377);
            this.Txt_MejorSolucion.Name = "Txt_MejorSolucion";
            this.Txt_MejorSolucion.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.Txt_MejorSolucion.Size = new System.Drawing.Size(310, 230);
            this.Txt_MejorSolucion.TabIndex = 5;
            this.Txt_MejorSolucion.Text = "";
            // 
            // dtdemandas
            // 
            this.dtdemandas.AllowUserToAddRows = false;
            this.dtdemandas.AllowUserToDeleteRows = false;
            this.dtdemandas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtdemandas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.demanda});
            this.dtdemandas.Location = new System.Drawing.Point(557, 87);
            this.dtdemandas.Name = "dtdemandas";
            this.dtdemandas.ReadOnly = true;
            this.dtdemandas.RowHeadersVisible = false;
            this.dtdemandas.RowHeadersWidth = 10;
            this.dtdemandas.Size = new System.Drawing.Size(92, 520);
            this.dtdemandas.TabIndex = 6;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 43;
            // 
            // demanda
            // 
            this.demanda.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.demanda.HeaderText = "Dem.";
            this.demanda.Name = "demanda";
            this.demanda.ReadOnly = true;
            this.demanda.Width = 57;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 619);
            this.Controls.Add(this.dtdemandas);
            this.Controls.Add(this.Txt_MejorSolucion);
            this.Controls.Add(this.txtSoluciones);
            this.Controls.Add(this.Pbx_Nodos);
            this.Controls.Add(this.Pnl_Head);
            this.MaximumSize = new System.Drawing.Size(987, 658);
            this.MinimumSize = new System.Drawing.Size(987, 658);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Pnl_Head.ResumeLayout(false);
            this.Pnl_Head.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Nodos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtdemandas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Head;
        private System.Windows.Forms.Button Btn_GenerarPso;
        private System.Windows.Forms.TextBox Txt_NumeroIteraciones;
        private System.Windows.Forms.Button Btn_SelecciónDatos;
        private System.Windows.Forms.PictureBox Pbx_Nodos;
        private System.Windows.Forms.Label lbl_iteraciones;
        private System.Windows.Forms.RichTextBox txtSoluciones;
        private System.Windows.Forms.RichTextBox Txt_MejorSolucion;
        private System.Windows.Forms.DataGridView dtdemandas;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn demanda;
        private System.Windows.Forms.Label lbcapacidad;
        private System.Windows.Forms.Label label1;
    }
}

