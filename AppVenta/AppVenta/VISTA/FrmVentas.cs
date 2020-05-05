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
        public String User;
        int idUsuario;
        private void FrmVentas_Load(object sender, EventArgs e)
        {
            CargarCombo();
            Retornoid();
            RetornoidUsuario();
            CargarTotal();
        }
        void RetornoidUsuario()
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                var IdUsuario = from tbusua in db.tb_usuarios
                    where tbusua.email == User

                    select new
                    {
                        Id = tbusua.Id
                    };
                foreach (var iteraridUsuario in IdUsuario)
                {
                    idUsuario = iteraridUsuario.Id;
                }
            }
        }
        void Retornoid()
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {

                var tb_v = db.tb_venta;
                txtIdVenta.Text = "1";
                foreach (var iterardatostbVentas in tb_v)
                {
                    int IdVenta = iterardatostbVentas.idVenta;
                    int suma = IdVenta + 1;
                    txtIdVenta.Text = suma.ToString();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                tb_venta tb_v = new tb_venta();

                String ComboDoc = cmbTipoDoc.SelectedValue.ToString();
                String ComboCliente = cmbClientes.SelectedValue.ToString();
                tb_v.idDocumento = Convert.ToInt32(ComboDoc);
                tb_v.iDCliente = Convert.ToInt32(ComboCliente);
                tb_v.iDUsuario = idUsuario;
                tb_v.totalVenta = Convert.ToDecimal(txtTotalPago.Text);
                tb_v.fecha = Convert.ToDateTime(dtpFecha.Text);

                bd.tb_venta.Add(tb_v);
                bd.SaveChanges();

                detalleVenta dV = new detalleVenta();

                for (int i = 0; i < dvgVentas.Rows.Count; i++)
                {
                    String idProducto = dvgVentas.Rows[i].Cells[0].Value.ToString();
                    String precio = dvgVentas.Rows[i].Cells[2].Value.ToString();
                    String cantidad = dvgVentas.Rows[i].Cells[3].Value.ToString();
                    String total = dvgVentas.Rows[i].Cells[4].Value.ToString();
;                   dV.idVenta = Convert.ToInt32(txtIdVenta.Text);
                    dV.idProducto = Convert.ToInt32(idProducto);
                    dV.cantidad = Convert.ToInt32(cantidad);
                    dV.precio = Convert.ToDecimal(precio);
                    dV.total = Convert.ToDecimal(total);

                    bd.detalleVentas.Add(dV);
                    bd.SaveChanges();
                }

            }
            dvgVentas.ClearSelection();

        }
    }
}
