using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Manejo de Ficheros
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApp1{

    public partial class Form1 : Form{
        string figura = "";
        string color = "";
        string fuente = "";
        Button[,] boton2 = new Button[11, 19];
        public Form1(){
            InitializeComponent();
            
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e){
            Application.Exit();
        }

        //MANEJO DE ARCHIVOS DRAW:

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e){
            /*ABRIR ARCHIVO .draw Y ASIGNAR CONTENIDO*/
            string contenido = abrir_Draw();
            Console.WriteLine(contenido);
        }

        private String abrir_Draw(){
            string contenido = "";
            String ruta_archivo = "";
            String linea_recorredora = "";
            bool extension;
            bool abierto = false;
            OpenFileDialog selector_archivos = new OpenFileDialog();
            selector_archivos.InitialDirectory = "c:\\";
            selector_archivos.Filter = "Archivos Draw (*.draw)|*.draw";
            selector_archivos.RestoreDirectory = false;
            selector_archivos.Title = "Archivos Draw de Entrada";
            if (selector_archivos.ShowDialog() == DialogResult.OK){
                //VALIDA SI LA EXTENSIÓN ES .DRAW:
                extension = selector_archivos.SafeFileName.ToLower().EndsWith(".draw");
                //Console.WriteLine("ext: " + extension);
                if (extension == true){
                    //DEFINE LA RUTA DEL ARCHIVO A LEER:
                    ruta_archivo = selector_archivos.FileName;
                    /*VERIFICA SI EL ARCHIVO SELECCIONADO YA ESTA PREVIAMENTE ABIERTO, SI 
                    NO ESTA ABIERTO LO ABRE, SI ESTA ABIERTO SIMPLEMENTE ENFOCA LA PÁGINA EN ÉL.*/
                    for(int recorredor = 0; recorredor < tabControl1.TabCount; recorredor ++){
                        String nombre_pagina = tabControl1.TabPages[recorredor].Name;
                        if (nombre_pagina == selector_archivos.SafeFileName){
                            MessageBox.Show("El archivo ya esta abierto", "Archivo Abierto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            abierto = true;
                            tabControl1.SelectedIndex = recorredor;
                            break;
                        }
                    }
                    try{
                        if (abierto == false){
                        //SI EL ARCHIVO NO ESTA ABIERTO LO LEE Y ABRE UNA NUEVA PÁGINA PARA VER EL DIBUJO
                        StreamReader lector = new StreamReader(ruta_archivo);
                        linea_recorredora = lector.ReadLine();
                        //CONTENIDO DRAW:
                        while (linea_recorredora != null){
                            //Console.WriteLine(contenido);
                            contenido = contenido + linea_recorredora + Environment.NewLine;
                            linea_recorredora = lector.ReadLine();
                        }
                        lector.Close();
                        Console.ReadLine();
                        MessageBox.Show("El archivo fue reconocido sin problema, generando dibujo...", "Archivo Reconocido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        /* CREAR PESTAÑA DE VISUALIZACIÓN DEL DIBUJO.*/
                        TabPage nuevaPagina = new TabPage(selector_archivos.SafeFileName);
                        nuevaPagina.Name = selector_archivos.SafeFileName;
                        tabControl1.Controls.Add(nuevaPagina);
                        //ACTIVA LA PÁGINA DEL ARCHIVO RECIEN ABIERTO:
                        tabControl1.SelectedIndex = tabControl1.TabCount-1;
                        }
                        

                    }
                    catch (Exception ex){
                        MessageBox.Show("No se pudo  leer el contenido del archivo debido a su estructura/formato de escritura, intentelo nuevamente", "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else{
                    MessageBox.Show("El archivo no posee una extensión .draw para ser abierto", "Extensión Desconocida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
                return contenido;
            }
            else{
                // NO ABRE NINGÚN ARCHIVO
                return null;
            }
            
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guardar_en_Draw("contenido de prueba");
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PASAR EL CONTENIDO GENERADO DEL DIBUJO:
            guardar_como("texto de prueba");
        }


        public void guardar_en_Draw(String contenido)
        {
                try
                {
                    int seleccionado = tabControl1.SelectedIndex;
                    String nombre_archivo_seleccionado = tabControl1.TabPages[seleccionado].Name;
                    StreamWriter escritor = new StreamWriter(Directory.GetCurrentDirectory() + "\\DRAWS\\" + nombre_archivo_seleccionado);
                    escritor.Write(contenido);
                    escritor.Close();
                    MessageBox.Show("Archivo guardado", "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                catch (Exception exc)
                {
                    MessageBox.Show("No se pudo guardar el contenido debido a un fallo en el contenido o ubicación elegida", "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            
        }

        public void guardar_como(String contenido){

            SaveFileDialog selector_destino = new SaveFileDialog();
            selector_destino.Filter = "Archivos Draw (*.draw)|*.draw";
            selector_destino.FilterIndex = 1;
            selector_destino.RestoreDirectory = true; 
            if (selector_destino.ShowDialog() == DialogResult.OK)
            {
                try{
                    StreamWriter escritor = new StreamWriter(selector_destino.FileName);
                    escritor.Write(contenido);
                    escritor.Close();
                    MessageBox.Show("Archivo guardado", "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exc){
                    MessageBox.Show("No se pudo guardar el contenido debido a un fallo en el contenido o ubicación elegida", "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("*Desarrolladores" + Environment.NewLine + "Cristian Suy -[Coordinador] 201700918." + Environment.NewLine + "Yelstin de León - 201602836. " + Environment.NewLine + "Ricardo Pérez - 201700524. " + Environment.NewLine + "Byron Gómez - 201700544. " + Environment.NewLine + "Diego Méndez - 201712680", "Acerca De", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        public void abrir_documento(String nombre_archivo){
            Process aperturar = new Process();
            aperturar.StartInfo.UseShellExecute = true;
            aperturar.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\Documentacion\\" + nombre_archivo;
            aperturar.StartInfo.CreateNoWindow = true;
            aperturar.Start();
        }

        private void manualDeUsoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manualTécnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ABRIR MANUAL TÉCNICO
            abrir_documento(".pdf");
        }

        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ABRIR MANUAL DE USUARIO
            abrir_documento(".pdf");
        }

        private void documentaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ABRIR DOCUMENTACIÓN DE CIRCUITOS
            abrir_documento(".pdf");
        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TabPage tabpage1 = new TabPage();
            int x = 140, y = 0;
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (j < 16 && i < 8)
                    {
                        boton2[i, j] = new Button();
                        boton2[i, j].SetBounds(x, y, 35, 35);
                        boton2[i, j].Click += new EventHandler(button1_MouseClick);
                        tabpage1.Controls.Add(boton2[i, j]);
                        boton2[i, j].Location = new Point(x,y);
                        y = y + 35;
                    }
                    else
                    {
                        boton2[i, j] = new Button();
                    }
                }
                y = 0;
                x = x + 35;
            }
            tabControl1.Controls.Add(tabpage1);
        }

        private void button1_MouseClick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            limpiar();
            for (int i = 0; i < 8; i++)
            {
                boton2[i, 10].BackColor = Color.Black;
            }
            boton2[0, 9].BackColor = Color.Black;
            boton2[7, 9].BackColor = Color.Black;
            boton2[1, 8].BackColor = Color.Black;
            boton2[6, 8].BackColor = Color.Black;
            boton2[2, 7].BackColor = Color.Black;
            boton2[5, 7].BackColor = Color.Black;
            boton2[3, 6].BackColor = Color.Black;
            boton2[4, 6].BackColor = Color.Black;
            for (int i = 0; i < 8; i++)
            {
                boton2[i, 10].Text = "1";
            }
            boton2[0, 9].Text = "1";
            boton2[7, 9].Text = "1";
            boton2[1, 8].Text = "1";
            boton2[6, 8].Text = "1";
            boton2[2, 7].Text = "1";
            boton2[5, 7].Text = "1";
            boton2[3, 6].Text = "1";
            boton2[4, 6].Text = "1";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            limpiar();
            for (int i = 2; i < 6; i++)
            {
                boton2[i, 3].BackColor = Color.Black;
                boton2[i, 10].BackColor = Color.Black;
            }
            for (int i = 4; i < 8; i++)
            {
                boton2[0, i + 1].BackColor = Color.Black;
                boton2[7, i + 1].BackColor = Color.Black;
            }
            boton2[1, 4].BackColor = Color.Black;
            boton2[6, 4].BackColor = Color.Black;
            boton2[1, 9].BackColor = Color.Black;
            boton2[6, 9].BackColor = Color.Black;
            for (int i = 2; i < 6; i++)
            {
                boton2[i, 3].Text = "1";
                boton2[i, 10].Text = "1";
            }
            for (int i = 4; i < 8; i++)
            {
                boton2[0, i + 1].Text = "1";
                boton2[7, i + 1].Text = "1";
            }
            boton2[1, 4].Text = "1";
            boton2[6, 4].Text = "1";
            boton2[1, 9].Text = "1";
            boton2[6, 9].Text = "1";
        }

        private void limpiar()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    boton2[i, j].BackColor = Color.Transparent;
                    boton2[i, j].Text = "";
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            limpiar();
            for (int i = 1; i < 7; i++)
            {
                boton2[i, 5].BackColor = Color.Black;
                boton2[i, 10].BackColor = Color.Black;
            }
            for (int i = 5; i < 11; i++)
            {
                boton2[1, i].BackColor = Color.Black;
                boton2[6, i].BackColor = Color.Black;
            }
            for (int i = 1; i < 7; i++)
            {
                boton2[i, 5].Text = "1";
                boton2[i, 10].Text = "1";
            }
            for (int i = 5; i < 11; i++)
            {
                boton2[1, i].Text = "1";
                boton2[6, i].Text = "1";
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            limpiar();
            for (int i = 3; i < 13; i++)
            {
                boton2[3, i].BackColor = Color.Black;
                boton2[4, i].BackColor = Color.Black;
            }
            for (int i = 3; i < 13; i++)
            {
                boton2[3, i].Text = "1";
                boton2[4, i].Text = "1";
            }
        }
    }
}
