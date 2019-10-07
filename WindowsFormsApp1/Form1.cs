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
    }
}
