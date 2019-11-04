using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Threading;
using System.IO.Ports;

namespace WindowsFormsApp1{

    public partial class Form1 : Form{
        string figura = "";
        string color = "";
        string fuente = "";
        int temp = 0;
        bool controlCoordenadas = false;
        Button[,] boton2 = new Button[11, 19];
        ArrayList coordenadas = new ArrayList();
        SerialPort puerto = new SerialPort();
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
            //Cambios de prueba para los commits
            //PASAR EL CONTENIDO GENERADO DEL DIBUJO:
            MessageBox.Show("hello");
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
            abrir_documento("usuario.pdf");
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
            try
            {
                puerto.BaudRate = 8;
                puerto.DataBits = 7;
                puerto.Parity = Parity.None;
                puerto.DiscardNull = true;
                puerto.StopBits = StopBits.One;
                puerto.PortName = "COM1";
                puerto.Open();
                MessageBox.Show("Puerto abierto");
            }
            catch (Exception)
            {

                MessageBox.Show(".");
            }
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Imprimir()
        {
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            temp = 0;
            string nombre = "", aux_coor = "";
            if(coordenadas.Count > 0)
            {
                coordenadas.Clear();
            }
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        nombre = boton2[i, j].Text;
                        if (nombre.Equals("1"))
                        {
                            aux_coor = i + "," + j;
                            coordenadas.Add(aux_coor);
                        }
                    }
                }
                MessageBox.Show("La cantidad de coordenadas captadas son: " + coordenadas.Count);
            }
            catch(Exception ex)
            {
                MessageBox.Show("error: " + ex);
            }
            Imprimir();
            Mandar_Coordenadas(temp);
        }

        public void Mandar_Coordenadas(int pos)
        {
            if (pos == coordenadas.Count)
            {
                MessageBox.Show("Ya se Imprimieron todas las coordenadas");
            }
            else if (pos + 3 > coordenadas.Count)
            {
                for (int i = pos; i < coordenadas.Count; i++)
                {
                    //MessageBox.Show("Las Coordenadas son: (" + coordenadas[i] + ")");
                    Thread.Sleep(4000);
                    com_serial(Convert.ToString(coordenadas[i]));
                    temp++;
                }
                MessageBox.Show("Se Concluyo con exito");
            }
            else
            {
                for (int i = pos; i < pos+3; i++)
                {
                    //MessageBox.Show("Las Coordenadas son: (" + coordenadas[i] + ")");
                    Thread.Sleep(4000);
                    com_serial(Convert.ToString(coordenadas[i]));
                    temp++;
                }
                MessageBox.Show("Presione \"Continuar Impresion\" cuando este listo para continuar");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controlCoordenadas = true;
            Mandar_Coordenadas(temp);
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void com_serial(string coor123)
        {
            string[] datos = coor123.Split(',');
            byte[] num = new byte[1];
            int coordenaFinal = 0;
            try
            {
                int X_1 = Convert.ToInt32(datos[0]);
                int Y_1 = Convert.ToInt32(datos[1]);
                switch (X_1)
                {
                    case 0:
                        coordenaFinal = Y_1;
                        break;
                    case 1:
                        coordenaFinal = 16 + Y_1;
                        break;
                    case 2:
                        coordenaFinal = 32 + Y_1;
                        break;
                    case 3:
                        coordenaFinal = 32 + 16 + Y_1;
                        break;
                    case 4:
                        coordenaFinal = 64 + Y_1;
                        break;
                    case 5:
                        coordenaFinal = 64 + 16 + Y_1;
                        break;
                    case 6:
                        coordenaFinal = 64 + 32 + Y_1;
                        break;
                    case 7:
                        coordenaFinal = 64 + 32 + 16 + Y_1;
                        break;
                    default:
                        MessageBox.Show("Rango de para X: 0<X<7");
                        break;
                }
                num[0] = Convert.ToByte(coordenaFinal);
                //MessageBox.Show("La Coordenada Mandada fue: (" + coor123 + ") \n Su Numero Binario en decimal: " + num[0].ToString());
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            try
            {
                if (puerto.IsOpen)
                {
                    puerto.Write(num, 0, 1);
                    //MessageBox.Show(num[0].ToString());
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                throw;
            }
        }

        private void btnCorrimiento_Click(object sender, EventArgs e)
        {
            byte[] num = new byte[1];
            num[0] = Convert.ToByte(0);
            try
            {
                if (puerto.IsOpen)
                {
                    puerto.Write(num, 0, 1);
                    MessageBox.Show(num[0].ToString());
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                throw;
            }
        }
    }
}
