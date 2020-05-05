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
using AppVenta.VISTA;

namespace AppVenta
{
    public partial class frmLogueo : Form
    {
        public frmLogueo()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {
                var lista = from usuario in db.tb_usuarios
                            where usuario.email == txtUsuario.Text
                            && usuario.contrasena == txtPass.Text
                            select usuario;

                if (lista.Count() > 0)
                {
                    String User = txtUsuario.Text;
                    frmMenu menu = new frmMenu(User);
                    menu.Show();
                    this.Hide();
                }
                else { MessageBox.Show("El Usuario no existe"); 
                }
            }

            
        }
    }
}
