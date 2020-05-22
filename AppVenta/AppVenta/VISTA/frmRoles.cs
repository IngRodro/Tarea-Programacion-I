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

namespace AppVenta.VISTA
{
    public partial class frmRoles : Form
    {
        public frmRoles()
        {
            InitializeComponent();
        }

        private void frmRoles_Load(object sender, EventArgs e)
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                var jointables = from tbusua in bd.tb_usuarios
                                 from rolesusuarios in bd.roles_usuario
                                 where tbusua.Id == rolesusuarios.id_usuario

                                 select new
                                 {
                                     Id = tbusua.Id,
                                     Email = tbusua.email,
                                     Tipo_Rol = rolesusuarios.tipo_rol
                                 };
                dtVistaRoles.DataSource = jointables.ToList();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
