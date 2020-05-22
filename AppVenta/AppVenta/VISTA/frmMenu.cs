using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppVenta.VISTA
{
    public partial class frmMenu : Form
    {
        public frmMenu(String User)
        {
            InitializeComponent();

            this.User = User;
        }

        String User;

        public static frmRoles rol = new frmRoles();
        private void rOLESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rol.MdiParent = this;
            rol.Show();
        }

        public static frmUsuarios user = new frmUsuarios();
        private void uSUARIOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            user.MdiParent = this;
            user.Show();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
        }

        public static FrmVentas FV = new FrmVentas();
        private void vENDERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FV.User = User;
            FV.MdiParent = this;
            FV.Show();
        }
    }
}
