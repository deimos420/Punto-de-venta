using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class FrmPrincipal : Form
    {
        //declaracion de variables dentro del formulario
        //variable de formulario login
        private FrmLogin ventanaLogin;

        //variable para formulario producto
        private FrmProductos ventanaProductos;

        private FrmCuentaClientes mantenimientoClientes;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //sintaxis para llamar una herramienta
            System.Diagnostics.Process.Start("Excel.exe");

        }

        private void wordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Winword.exe");
        }

        private void calciuladoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void powerPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("PowerPnt.exe");
        }



        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            //vamos a mostrar la notificación de inicio
            this.notifyIcon1.ShowBalloonTip(25);
            
            //variable de tipo formulario(Interfaz Grafica)
            //se instancia una ventana de login
            this.ventanaLogin = new FrmLogin();

            

            //se asigna al formulario login la referencia del formulario principa
            this.ventanaLogin.setPrincipal(this);

            //mostrar la ventana de login
            this.ventanaLogin.ShowDialog();




        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void salirToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Close();

        }


        public  void CerrarPrincipal()
        {
            this.Close();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //se muestra la ventana de login para autenticarse
            //se llama al método estado inicial
            this.ventanaLogin.estadoInicial();
            this.ventanaLogin.Visible = true;

        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //se intancia la ventana para los productos
            this.ventanaProductos = new FrmProductos();

            //se muestra la interfaz para productos
            this.ventanaProductos.ShowDialog();

        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mantenimientoClientes =new  FrmCuentaClientes();
            this.mantenimientoClientes.ShowDialog();
        }
    }//cierre formulario
}//cierre de namespace
