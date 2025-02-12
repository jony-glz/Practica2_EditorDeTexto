namespace Practica2_EditorDeTexto
{
    public partial class frmEditor : Form
    {
        public frmEditor()
        {
            InitializeComponent();
        }

        bool archivoGuardado = false;
        string filePath = null;

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            // esta condicion nos sirve para determinar si es un archivo nuevo
            // o es un archivo previo al cual se le hicieron modificaciones
            if (archivoGuardado == false && filePath == null)
            {
                Guardar();
            }
            // en dado caso que sea un archivo que abrimos, este se 
            // guardara en la misma ruta y con el mismo nombre que el inicial
            else if (archivoGuardado == true || filePath != null)
            {
                try
                {
                    string texto = rtbEditor.Text;
                    File.WriteAllText(filePath, texto);
                    MessageBox.Show("Archivo guardado correctamente");
                    archivoGuardado = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo: " + ex.Message);
                }
            }
        }
        // reciclamos el codigo para crear la funcion de guardar, e implementarla
        // en dichas condiciones para esta accion
        private void Guardar()
        {
            DialogResult resultado = saveFileDialogEditor.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                filePath = saveFileDialogEditor.FileName;
                string texto = rtbEditor.Text;

                try
                {
                    File.WriteAllText(filePath, texto);
                    MessageBox.Show("Archivo guardado correctamente");
                    archivoGuardado = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo: " + ex.Message);
                }

            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // si no se guardaron los cambios, este nos preguntara si queremos
            // guardarlos
            if (archivoGuardado == false)
            {
                // si queremos guardar cambios, los hace, sino, omite
                DialogResult res = MessageBox.Show("Quieres guardar cambios?", "Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    guardarToolStripMenuItem_Click(sender, e);
                    Abrir();
                }
                else if (res == DialogResult.No)
                {
                    Abrir();
                }
            } else
            {
                Abrir();
            }
            
        }
        // reciclamos el codigo para crear la funcion de abrir, e implementarla
        // en dichas condiciones para esta accion
        public void Abrir() {
            DialogResult resultado;
            resultado = openFileDialogEditor.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                filePath = openFileDialogEditor.FileName;

                try
                {
                    string texto = File.ReadAllText(filePath);
                    rtbEditor.Text = texto;
                    archivoGuardado = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir el archivo: " + ex.Message);
                }

            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // replica los pasos de Abrir, a diferencia que ahora es para
            // crear un nuevo archivo de texto
            if (archivoGuardado == false)
            {
                DialogResult res = MessageBox.Show("Guardo cambios?", "Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    guardarToolStripMenuItem_Click(sender,e);
                    Nuevo();
                } else if (res == DialogResult.No)
                {
                    Nuevo();
                }
            } else
            {
                Nuevo();
            }
        }
        // esta funcion nos permite crear un nuevo archivo de texto desde cero
        private void Nuevo()
        {
            archivoGuardado = false;
            filePath = null;
            rtbEditor.Text = null;
        }

        private void rtbEditor_TextChanged(object sender, EventArgs e)
        {
            // nos permite determinar si el archivo que estamos manipulando
            // tiene cambios sin guardar
            archivoGuardado = false;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // si el archivo tiene cambios, pregunta si los quiere guardar, sino tiene cambios, se cierra
            if (archivoGuardado == false)
            {
                // si se desea guardar cambios, los hace y luego sale, sino, no hace nada
                DialogResult res = MessageBox.Show("Guardo cambios?", "Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    // utiliza las acciones ya establecidas para guardar los cambios pendientes
                    guardarToolStripMenuItem_Click(sender,e);
                    this.Close();
                } else if(res == DialogResult.No)
                {
                    this.Close();
                }
            } else
            {
                this.Close();
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Guardar();
        }
    }
}
