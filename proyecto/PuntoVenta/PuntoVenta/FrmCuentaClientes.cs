using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//importacion de modelo
using Modelo;

//importacion de controlador
using Controlador;

namespace PuntoVenta
{
    public partial class FrmCuentaClientes : Form
    {
        //variable del objeto modelo
        private Clientes clientes;
        //variable controlador para la interfaz de usuario
        private ControladorADO controlador;

        public FrmCuentaClientes()
        {
            InitializeComponent();
        }

        private void TxtDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmCuentaClientes_Load(object sender, EventArgs e)
        {
            //se instancia el controlador
            //se enviar al construtor los datos de la conexion en un string
            this.controlador = new ControladorADO(this.obtenerStringConexion());

            //llamar al metodo estado inicial de la interfaz
            this.estadoInicial();
        }

        private string obtenerStringConexion()
        {
            string cnx;
            cnx = "data source=WINDOWS-VI3R0U5;initial catalog=PuntoVenta;user id=PuntoVenta;password=123456";
            return cnx;
        }
        public void estadoInicial()
        {
            //botones para las acciones en la interfaz
            this.BtnCrear.Enabled = false;
            this.BtnModificar.Enabled = false;
            this.BtnEliminar.Enabled = false;
            this.BtnConsultar.Enabled = true;
            this.BtnCancelar.Enabled = true;

            //controles para entradas de datos
            this.TxtCedula.Enabled = true;
            this.TxtNombreCompleto.Enabled = false;
            this.TxtTelefon.Enabled = false;
            this.TxtDireccion.Enabled = false;
            
        }

        private void consultarCliente()
        {
            //se instancia del controlador
            this.controlador = new ControladorADO(this.obtenerStringConexion());

            //por medio de controlador buscamos el producto
            this.clientes = this.controlador.consultarCliente(this.TxtCedula.Text);

            //se valida la instancia del producto
            if (this.clientes != null)
            {
                //se cargar la informacion en la interfaz
                //se toman los atributos del producto sus valores
                //son asignados a cada control de interfaz
                this.TxtCedula.Text = clientes.cedula;
                this.TxtNombreCompleto.Text = clientes.NombreCompleto;
                this.TxtTelefon.Text = clientes.telefono.ToString();
                this.TxtDireccion.Text = clientes.direccion;

                //se asigna el estado de la pantalla para modificar
                this.estadoPantallaModificar();

            }

        }

        public void estadoPantallaModificar()
        {
            this.BtnConsultar.Enabled = false;
            this.BtnCrear.Enabled = false;
            this.BtnModificar.Enabled = true;
            this.BtnEliminar.Enabled = true;
            this.BtnCancelar.Enabled = true;

            this.TxtCedula.Enabled = false;
            this.TxtNombreCompleto.Enabled = true;
            this.TxtTelefon.Enabled = true;
            this.TxtDireccion.Enabled = true;
        }
        private void estadoPantallaAgregar()
        {
            //bloquear el codigo
            this.TxtCedula.Enabled = false;            
            this.TxtNombreCompleto.Enabled = true;
            this.TxtTelefon.Enabled = true;
            this.TxtDireccion.Enabled = true;

            //Nada mas se deja activo el boton de agregar y cancelar

            this.BtnConsultar.Enabled = false;
            this.BtnCrear.Enabled = true;
            this.BtnModificar.Enabled = false;
            this.BtnEliminar.Enabled = false;
            this.BtnCancelar.Enabled = true;

        }

        private void limpiarPantalla()
        {
            this.TxtCedula.Clear();
            this.TxtNombreCompleto.Clear();
            this.TxtTelefon.Clear();
            this.TxtDireccion.Clear();
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            try
            {

                this.consultarCliente();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                //aquí en caso que no exista el producto se habilita la pantalla para agregar
                this.estadoPantallaAgregar();
            }

        }

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                //se instancia un producto
                this.clientes = new Clientes();

                //se asignan los valores ingresados por el usuario
                this.clientes.cedula = this.TxtCedula.Text;
                this.clientes.NombreCompleto = this.TxtNombreCompleto.Text;
                this.clientes.telefono = int.Parse(this.TxtTelefon.ToString());
                this.clientes.direccion = this.TxtDireccion.Text;

                //se utiliza el controlador enviando por parametro el producto
                this.controlador.registrarClientes(this.clientes);

                MessageBox.Show("Cliente registrado:\n" + this.clientes.cedula +
                    "\nNombre" + this.clientes.NombreCompleto +
                    "\nTelefono " + this.clientes.telefono +
                    "\nDireccion" + this.clientes.direccion
                    );

                //limpio la pantalla
                this.limpiarPantalla();
                this.estadoInicial();

            }
            catch (Exception ex)
            {

                MessageBox.Show("error " + ex.Message,
                    "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);


            }

        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                //se instancia un producto
                this.clientes = new Clientes();

                //se asignan los valores ingresados por el usuario
                this.clientes.cedula = this.TxtCedula.Text;
                this.clientes.NombreCompleto = this.TxtNombreCompleto.Text;
                this.clientes.telefono = int.Parse(this.TxtTelefon.ToString());
                this.clientes.direccion = this.TxtDireccion.Text;

                //se utiliza el controlador enviando por parametro el producto
                this.controlador.modificarClientes(this.clientes);

                MessageBox.Show("Cliente Modificado:\n" + this.clientes.cedula +
                    "\nNombre" + this.clientes.NombreCompleto +
                    "\nTelefono " + this.clientes.telefono +
                    "\nDireccion" + this.clientes.direccion
                    );

                //se limpiar la pantalla
                this.limpiarPantalla();
                this.estadoInicial();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //se utiliza el controlador enviando por parametro el producto
                this.controlador.eliminarClientes(this.TxtCedula.Text);

                MessageBox.Show("Cliente Eliminado", "Eliminación",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //se limpia la pantalla
                this.limpiarPantalla();
                this.estadoInicial();



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.limpiarPantalla();
            this.estadoInicial();
        }
    }//cierre formulario
} //cierre namespace
