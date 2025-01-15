
namespace Algoritmo_PSO_Problema_PHUB
{
    partial class frmPhub
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
            this.Pnl_Head = new System.Windows.Forms.Panel();
            this.chbMostrarSoluciones = new System.Windows.Forms.CheckBox();
            this.lblTiempoTranscurrido = new System.Windows.Forms.Label();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.lbcapacidad = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_iteraciones = new System.Windows.Forms.Label();
            this.Btn_GenerarPso = new System.Windows.Forms.Button();
            this.Txt_NumeroIteraciones = new System.Windows.Forms.TextBox();
            this.Btn_SelecciónDatos = new System.Windows.Forms.Button();
            this.dtdemandas = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.demanda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSoluciones = new System.Windows.Forms.RichTextBox();
            this.Pbx_Nodos = new System.Windows.Forms.PictureBox();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlGraficos = new System.Windows.Forms.Panel();
            this.pnlDatos = new System.Windows.Forms.Panel();
            this.pnlResultados = new System.Windows.Forms.Panel();
            this.txt_MejorSolucion = new System.Windows.Forms.RichTextBox();
            this.Pnl_Head.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtdemandas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Nodos)).BeginInit();
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlGraficos.SuspendLayout();
            this.pnlDatos.SuspendLayout();
            this.pnlResultados.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Head
            // 
            this.Pnl_Head.BackColor = System.Drawing.Color.Gainsboro;
            this.Pnl_Head.Controls.Add(this.chbMostrarSoluciones);
            this.Pnl_Head.Controls.Add(this.lblTiempoTranscurrido);
            this.Pnl_Head.Controls.Add(this.lblTiempo);
            this.Pnl_Head.Controls.Add(this.lbcapacidad);
            this.Pnl_Head.Controls.Add(this.label1);
            this.Pnl_Head.Controls.Add(this.lbl_iteraciones);
            this.Pnl_Head.Controls.Add(this.Btn_GenerarPso);
            this.Pnl_Head.Controls.Add(this.Txt_NumeroIteraciones);
            this.Pnl_Head.Controls.Add(this.Btn_SelecciónDatos);
            this.Pnl_Head.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pnl_Head.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Head.Name = "Pnl_Head";
            this.Pnl_Head.Size = new System.Drawing.Size(971, 85);
            this.Pnl_Head.TabIndex = 7;
            // 
            // chbMostrarSoluciones
            // 
            this.chbMostrarSoluciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbMostrarSoluciones.AutoSize = true;
            this.chbMostrarSoluciones.Checked = true;
            this.chbMostrarSoluciones.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbMostrarSoluciones.Location = new System.Drawing.Point(838, 61);
            this.chbMostrarSoluciones.Name = "chbMostrarSoluciones";
            this.chbMostrarSoluciones.Size = new System.Drawing.Size(132, 17);
            this.chbMostrarSoluciones.TabIndex = 8;
            this.chbMostrarSoluciones.Text = "Ver intentos al generar";
            this.chbMostrarSoluciones.UseVisualStyleBackColor = true;
            this.chbMostrarSoluciones.CheckedChanged += new System.EventHandler(this.chbMostrarSoluciones_CheckedChanged);
            // 
            // lblTiempoTranscurrido
            // 
            this.lblTiempoTranscurrido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTiempoTranscurrido.AutoSize = true;
            this.lblTiempoTranscurrido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiempoTranscurrido.Location = new System.Drawing.Point(263, 35);
            this.lblTiempoTranscurrido.Name = "lblTiempoTranscurrido";
            this.lblTiempoTranscurrido.Size = new System.Drawing.Size(16, 16);
            this.lblTiempoTranscurrido.TabIndex = 7;
            this.lblTiempoTranscurrido.Text = "0";
            // 
            // lblTiempo
            // 
            this.lblTiempo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTiempo.AutoSize = true;
            this.lblTiempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiempo.Location = new System.Drawing.Point(263, 10);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(147, 16);
            this.lblTiempo.TabIndex = 6;
            this.lblTiempo.Text = "Tiempo transcurrido";
            // 
            // lbcapacidad
            // 
            this.lbcapacidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbcapacidad.AutoSize = true;
            this.lbcapacidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbcapacidad.Location = new System.Drawing.Point(452, 35);
            this.lbcapacidad.Name = "lbcapacidad";
            this.lbcapacidad.Size = new System.Drawing.Size(16, 16);
            this.lbcapacidad.TabIndex = 5;
            this.lbcapacidad.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(452, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Capacidad de los Hubs";
            // 
            // lbl_iteraciones
            // 
            this.lbl_iteraciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_iteraciones.AutoSize = true;
            this.lbl_iteraciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_iteraciones.Location = new System.Drawing.Point(691, 10);
            this.lbl_iteraciones.Name = "lbl_iteraciones";
            this.lbl_iteraciones.Size = new System.Drawing.Size(105, 16);
            this.lbl_iteraciones.TabIndex = 3;
            this.lbl_iteraciones.Text = "N° Iteraciones";
            // 
            // Btn_GenerarPso
            // 
            this.Btn_GenerarPso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_GenerarPso.BackColor = System.Drawing.Color.PaleGreen;
            this.Btn_GenerarPso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_GenerarPso.Location = new System.Drawing.Point(836, 21);
            this.Btn_GenerarPso.Name = "Btn_GenerarPso";
            this.Btn_GenerarPso.Size = new System.Drawing.Size(104, 36);
            this.Btn_GenerarPso.TabIndex = 2;
            this.Btn_GenerarPso.Text = "Generar";
            this.Btn_GenerarPso.UseVisualStyleBackColor = false;
            this.Btn_GenerarPso.Click += new System.EventHandler(this.Btn_GenerarPso_Click);
            // 
            // Txt_NumeroIteraciones
            // 
            this.Txt_NumeroIteraciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_NumeroIteraciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_NumeroIteraciones.Location = new System.Drawing.Point(673, 29);
            this.Txt_NumeroIteraciones.Name = "Txt_NumeroIteraciones";
            this.Txt_NumeroIteraciones.Size = new System.Drawing.Size(143, 26);
            this.Txt_NumeroIteraciones.TabIndex = 1;
            this.Txt_NumeroIteraciones.Text = "10";
            this.Txt_NumeroIteraciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Btn_SelecciónDatos
            // 
            this.Btn_SelecciónDatos.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Btn_SelecciónDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_SelecciónDatos.Location = new System.Drawing.Point(12, 19);
            this.Btn_SelecciónDatos.Name = "Btn_SelecciónDatos";
            this.Btn_SelecciónDatos.Size = new System.Drawing.Size(173, 36);
            this.Btn_SelecciónDatos.TabIndex = 0;
            this.Btn_SelecciónDatos.Text = "Seleccionar datos";
            this.Btn_SelecciónDatos.UseVisualStyleBackColor = false;
            this.Btn_SelecciónDatos.Click += new System.EventHandler(this.Btn_SelecciónDatos_Click);
            // 
            // dtdemandas
            // 
            this.dtdemandas.AllowUserToAddRows = false;
            this.dtdemandas.AllowUserToDeleteRows = false;
            this.dtdemandas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtdemandas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtdemandas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.demanda});
            this.dtdemandas.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtdemandas.Location = new System.Drawing.Point(10, 10);
            this.dtdemandas.Name = "dtdemandas";
            this.dtdemandas.ReadOnly = true;
            this.dtdemandas.RowHeadersVisible = false;
            this.dtdemandas.RowHeadersWidth = 10;
            this.dtdemandas.Size = new System.Drawing.Size(108, 514);
            this.dtdemandas.TabIndex = 11;
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // demanda
            // 
            this.demanda.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.demanda.HeaderText = "Dem.";
            this.demanda.Name = "demanda";
            this.demanda.ReadOnly = true;
            // 
            // txtSoluciones
            // 
            this.txtSoluciones.BackColor = System.Drawing.Color.PowderBlue;
            this.txtSoluciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSoluciones.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.txtSoluciones.Location = new System.Drawing.Point(10, 0);
            this.txtSoluciones.Name = "txtSoluciones";
            this.txtSoluciones.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtSoluciones.Size = new System.Drawing.Size(328, 251);
            this.txtSoluciones.TabIndex = 9;
            this.txtSoluciones.Text = "";
            // 
            // Pbx_Nodos
            // 
            this.Pbx_Nodos.BackColor = System.Drawing.Color.LightCyan;
            this.Pbx_Nodos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pbx_Nodos.Location = new System.Drawing.Point(10, 10);
            this.Pbx_Nodos.Name = "Pbx_Nodos";
            this.Pbx_Nodos.Size = new System.Drawing.Size(485, 514);
            this.Pbx_Nodos.TabIndex = 8;
            this.Pbx_Nodos.TabStop = false;
            this.Pbx_Nodos.Paint += new System.Windows.Forms.PaintEventHandler(this.Pbx_Nodos_Paint);
            this.Pbx_Nodos.Resize += new System.EventHandler(this.Pbx_Nodos_Resize);
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.Pnl_Head);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(971, 85);
            this.pnlEncabezado.TabIndex = 12;
            // 
            // pnlContenido
            // 
            this.pnlContenido.Controls.Add(this.pnlGraficos);
            this.pnlContenido.Controls.Add(this.pnlDatos);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 85);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(971, 534);
            this.pnlContenido.TabIndex = 13;
            // 
            // pnlGraficos
            // 
            this.pnlGraficos.Controls.Add(this.Pbx_Nodos);
            this.pnlGraficos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGraficos.Location = new System.Drawing.Point(0, 0);
            this.pnlGraficos.Name = "pnlGraficos";
            this.pnlGraficos.Padding = new System.Windows.Forms.Padding(10);
            this.pnlGraficos.Size = new System.Drawing.Size(505, 534);
            this.pnlGraficos.TabIndex = 14;
            // 
            // pnlDatos
            // 
            this.pnlDatos.Controls.Add(this.pnlResultados);
            this.pnlDatos.Controls.Add(this.dtdemandas);
            this.pnlDatos.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDatos.Location = new System.Drawing.Point(505, 0);
            this.pnlDatos.Name = "pnlDatos";
            this.pnlDatos.Padding = new System.Windows.Forms.Padding(10);
            this.pnlDatos.Size = new System.Drawing.Size(466, 534);
            this.pnlDatos.TabIndex = 13;
            // 
            // pnlResultados
            // 
            this.pnlResultados.Controls.Add(this.txt_MejorSolucion);
            this.pnlResultados.Controls.Add(this.txtSoluciones);
            this.pnlResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultados.Location = new System.Drawing.Point(118, 10);
            this.pnlResultados.Name = "pnlResultados";
            this.pnlResultados.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pnlResultados.Size = new System.Drawing.Size(338, 514);
            this.pnlResultados.TabIndex = 12;
            // 
            // txt_MejorSolucion
            // 
            this.txt_MejorSolucion.BackColor = System.Drawing.Color.PaleGreen;
            this.txt_MejorSolucion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_MejorSolucion.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.txt_MejorSolucion.Location = new System.Drawing.Point(10, 251);
            this.txt_MejorSolucion.Name = "txt_MejorSolucion";
            this.txt_MejorSolucion.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txt_MejorSolucion.Size = new System.Drawing.Size(328, 263);
            this.txt_MejorSolucion.TabIndex = 11;
            this.txt_MejorSolucion.Text = "";
            // 
            // frmPhub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 619);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Name = "frmPhub";
            this.Text = "P-hub median";
            this.Load += new System.EventHandler(this.frmPhub_Load);
            this.Pnl_Head.ResumeLayout(false);
            this.Pnl_Head.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtdemandas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Nodos)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlGraficos.ResumeLayout(false);
            this.pnlDatos.ResumeLayout(false);
            this.pnlResultados.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Head;
        private System.Windows.Forms.Label lbcapacidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_iteraciones;
        private System.Windows.Forms.Button Btn_GenerarPso;
        private System.Windows.Forms.TextBox Txt_NumeroIteraciones;
        private System.Windows.Forms.Button Btn_SelecciónDatos;
        private System.Windows.Forms.DataGridView dtdemandas;
        private System.Windows.Forms.RichTextBox txtSoluciones;
        private System.Windows.Forms.PictureBox Pbx_Nodos;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlGraficos;
        private System.Windows.Forms.Panel pnlDatos;
        private System.Windows.Forms.Panel pnlResultados;
        private System.Windows.Forms.RichTextBox txt_MejorSolucion;
        private System.Windows.Forms.Label lblTiempoTranscurrido;
        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.CheckBox chbMostrarSoluciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn demanda;
    }
}