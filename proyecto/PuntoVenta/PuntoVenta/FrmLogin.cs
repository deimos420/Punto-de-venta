using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//importa la libreria del controlador
using Controlador;
//importar la libreria del modelo
using Modelo;

namespace PuntoVenta
{
    public partial class FrmLogin : Form
    {
        //variable  para formulario principal
        private FrmPrincipal frmPrincipal;
        
        //variable para utilizar el controlador
        private ControladorADO controlador;

        //variable para el usuario
        private Usuario user;

        //método encargado de asignar la instancia del formulario principal
        public void setPrincipal(FrmPrincipal frmPrinc)
        {
            this.frmPrincipal = frmPrinc;
        }

        //constructor de formulario login
        public FrmLogin()
        {
            InitializeComponent();
            //se intacia el controlador
            this.controlador = new ControladorADO(this.obtenerStringConexion());
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //se utiliza el método cerrar principal
            this.frmPrincipal.CerrarPrincipal();
            //this.Close();
           
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //validación de autenticación
            this.intentoAutenticacion();
        }

        private void txtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            //logica de control de teclas presionadas en la caja texto
            if(e.KeyChar == (char)(Keys.Enter))
            {
                //cada vez que presione Enter se realiza un intento de autenticación
                //se llama al método encargado de validar la autenticación
                this.intentoAutenticacion();
            }
        }

        public void intentoAutenticacion()
        {
            //se intancia un usuario con los valores de la interfaz grafica 
            this.user = new Usuario() { login = this.txtUsuario.Text.Trim(), password = this.txtContrasena.Text.Trim() };
            //el controlador recibe el usuario utilizando el metodo autentificado para revisar en la base datos
            if(this.controlador.autentificado(this.user))
            {
                MessageBox.Show("Bienvenido", "Sesión Iniciada", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ///se oculta el formulario de autenticacion
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrecta", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtUsuario.Clear();
                this.txtContrasena.Clear();

            }
        }


        public void estadoInicial()
        {
            this.txtUsuario.Clear();
            this.txtContrasena.Clear();
        }
        private string obtenerStringConexion()
        {
            string cnx;
            cnx = "data source=WINDOWS-VI3R0U5;initial catalog=PuntoVenta;user id=PuntoVenta;password=123456";
            return cnx;
        }

    }//cierre de  formulario
}///cierre de namespace
