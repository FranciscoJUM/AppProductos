using AppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductosApp.Formularios
{
    public partial class FrmProductos : Form
    {
        private Form activeForm;
        private IEmpleadoServices empleadoServices;
        private iProductoservice productoservice;
        public FrmProductos(IEmpleadoServices empleadoServices)
        {
            this.empleadoServices = empleadoServices;
            this.productoservice = productoservice;
          
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            FrmGestionEmpleados frmGestionEmpleados = new FrmGestionEmpleados(empleadoServices);
            ShowActiveForm(frmGestionEmpleados);
        }

        private void ShowActiveForm(Form form)
        {
            activeForm = form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            pnlContent.Controls.Add(form);
            activeForm.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
