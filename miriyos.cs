using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ContadorPalabras
{
    public partial class Form1 : Form
    {
        // Diccionario donde guardaremos palabra -> lista de fechas
        private Dictionary<string, List<DateTime>> registro = new Dictionary<string, List<DateTime>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text.ToLower();
            DateTime fecha = datePicker.Value;

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show("Por favor escribe una descripción.");
                return;
            }

            // Separar por espacios y signos
            char[] separadores = { ' ', ',', '.', ';', ':', '!', '?', '\n', '\r' };
            string[] palabras = descripcion.Split(separadores, StringSplitOptions.RemoveEmptyEntries);

            foreach (string palabra in palabras)
            {
                if (!registro.ContainsKey(palabra))
                    registro[palabra] = new List<DateTime>();

                registro[palabra].Add(fecha);
            }

            ActualizarTabla();
            txtDescripcion.Clear();
        }

        private void ActualizarTabla()
        {
            listView1.Items.Clear();

            foreach (var item in registro)
            {
                string palabra = item.Key;
                int frecuencia = item.Value.Count;
                string fechas = string.Join(", ", item.Value.Select(f => f.ToShortDateString()));

                ListViewItem fila = new ListViewItem(palabra);
                fila.SubItems.Add(frecuencia.ToString());
                fila.SubItems.Add(fechas);

                listView1.Items.Add(fila);
            }
        }
    }
}
