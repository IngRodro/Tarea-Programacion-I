using AppVenta.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppVenta.VISTA.Formularios_de_Busqueda
{
    public partial class FrmBusqueda : Form
    {
        public FrmBusqueda(TextBox Busqueda)
        {
            InitializeComponent();
            txtBusqueda.Text = Busqueda.Text;
        }

        private void FrmBusqueda_Load(object sender, EventArgs e)
        {
            filtro();
        }

        void filtro()
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                String nombre = txtBusqueda.Text;

                var buscarprod = from tbprod in bd.productoes
                    where tbprod.nombreProducto.Contains(nombre)

                    select new
                    {
                        Id = tbprod.idProducto,
                        Nombre = tbprod.nombreProducto,
                        Precio = tbprod.precioProducto
                    };
                dvgProductos.DataSource = buscarprod.ToList();
            }
        }

        void envio()
        {
            String Id = dvgProductos.CurrentRow.Cells[0].Value.ToString();
            String Nombre = dvgProductos.CurrentRow.Cells[1].Value.ToString();
            String Precio = dvgProductos.CurrentRow.Cells[2].Value.ToString();
            frmMenu.FV.txtIdProd.Text = Id;
            frmMenu.FV.txtNombreProd.Text = Nombre;
            frmMenu.FV.txtPrecioProd.Text = Precio;

            frmMenu.FV.txtCantidad.Focus();
            frmMenu.FV.txtTotal.Text =
                Convert.ToString(Double.Parse(Precio) * Double.Parse(frmMenu.FV.txtCantidad.Text));
            this.Close();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filtro();
        }

        private void dvgProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            envio();
        }

        private void dvgProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                envio();
            }
        }
    }
}
