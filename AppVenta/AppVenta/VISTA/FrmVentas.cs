using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppVenta.Model;
using AppVenta.VISTA.Formularios_de_Busqueda;

namespace AppVenta.VISTA
{
    public partial class FrmVentas : Form
    {
        public FrmVentas()
        {
            InitializeComponent();
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            CargarCombo();
            Retornoid();
            CargarTotal();
        }

        void Retornoid()
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                var tb_v = db.tb_venta;
                foreach (var iterardatostbVentas in tb_v)
                {
                    txtIdVenta.Text = iterardatostbVentas.idVenta.ToString();
                   // dvgUsuarios.Rows.Add(iterardatostbUsuario.Id, iterardatostbUsuario.email, iterardatostbUsuario.contrasena);
                }
            }
        }

        void CargarCombo()
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                var clientes = bd.tb_cliente.ToList();
                cmbClientes.DataSource = clientes;
                cmbClientes.DisplayMember = "nombreCliente";
                cmbClientes.ValueMember = "iDCliente";


                var doc = bd.tb_documento.ToList();
                cmbTipoDoc.DataSource = doc;
                cmbTipoDoc.DisplayMember = "nombreDocumento";
                cmbTipoDoc.ValueMember = "iDDocumento";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextBox Busqueda;
            Busqueda = txtBusqueda;
            FrmBusqueda FB = new FrmBusqueda(Busqueda);
            FB.Show();
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (txtIdProd.Text != "" && txtNombreProd.Text != "" && txtPrecioProd.Text != "")
            {
                dvgVentas.Rows.Add(txtIdProd.Text, txtNombreProd.Text,
                    txtPrecioProd.Text, txtCantidad.Text, txtTotal.Text);
                LimpiarCeldas();
            }else
            {
                MessageBox.Show("Los Campos no pueden estar vacíos");
            }
            
        }


        void CargarTotal()
        {
            double Total = 0;
            for (int i = 0; i < dvgVentas.Rows.Count; i++)
            {
                Total += Double.Parse(dvgVentas.Rows[i].Cells[4].Value.ToString());
            }

            txtTotalPago.Text = Convert.ToString(Total);
        }

        void LimpiarCeldas()
        {
            txtCantidad.Text = "1";
            txtIdProd.Text = "";
            txtNombreProd.Text = "";
            txtPrecioProd.Text = "";
            txtTotal.Text = "";
        }
        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            Calculo();
        }
        void Calculo()
        {
            try
            {
                double precioProd;
                double cantidad;
                double total;

                precioProd = Double.Parse(txtPrecioProd.Text);
                cantidad = Convert.ToDouble(txtCantidad.Text);

                total = precioProd * cantidad;
                txtTotal.Text = Convert.ToString(total);
            }
            catch (Exception ex)
            {
                txtCantidad.Text = "0";
                MessageBox.Show("No puedes operar datos menores a 0");
                txtCantidad.Select();
            }
        }

        private void dvgVentas_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            CargarTotal();
        }

    }
}
