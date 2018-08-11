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
    public partial class FrmProductos : Form
    {
        //variable del objeto modelo
        private Productos producto;
        //variable controlador para la interfaz de usuario
        private ControladorADO controlador;
        
        public FrmProductos()
        {
            InitializeComponent();
        }

       
        private void FrmProductos_Load(object sender, EventArgs e)
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //se instancia un producto
                this.producto = new Productos();

                //se asignan los valores ingresados por el usuario
                this.producto.codigo = this.txtCodigo.Text;
                this.producto.descripcion = this.txtDescripcion.Text;
                this.producto.precioCompra = decimal.Parse(
                    this.txtPrecioCompra.Text.Trim());

                this.producto.descuento = int.Parse(
                    this.nudDescuento.Value.ToString());

                this.producto.impuesto = int.Parse(
                    this.nudImpuesto.Value.ToString());

                this.producto.fechaRegistro = this.dtpFechaRegistro.Value;

                this.producto.estado = char.Parse(
                    this.cbxEstado.Text.Substring(0, 1));

                //se utiliza el controlador enviando por parametro el producto
                this.controlador.registrar(this.producto);

                MessageBox.Show("Producto registrado:\n" + this.producto.codigo +
                    "\nDescripcion " + this.producto.descripcion +
                    "\nPrecio " + this.producto.precioCompra
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

        ///Metodo estado inicial de la pantalla
        public void estadoInicial()
        {
            //botones para las acciones en la interfaz
            this.btnAgregar.Enabled = false;
            this.btnModificar.Enabled = false;
            this.btnEliminar.Enabled = false;
            this.btnConsultar.Enabled = true;
            this.btnCancelar.Enabled = true;

            //controles para entradas de datos
             this.txtCodigo.Enabled = true;
            this.txtDescripcion.Enabled = false;
            this.txtPrecioCompra.Enabled = false;

            //controles numericos
            this.nudDescuento.Enabled = false;
            this.nudImpuesto.Enabled = false;

            //controles de fecha
            this.dtpFechaRegistro.Enabled = false;

            //Se marca seleccionado por default el primer Item
            this.cbxEstado.SelectedIndex = 0;
            this.cbxEstado.Enabled = false;


        }

        private void consultarProducto()
        {
            //se instancia del controlador
            this.controlador = new ControladorADO(this.obtenerStringConexion());

            //por medio de controlador buscamos el producto
            this.producto = this.controlador.consultar(this.txtCodigo.Text);

            //se valida la instancia del producto
            if (this.producto != null)
            {
                //se cargar la informacion en la interfaz
                //se toman los atributos del producto sus valores
                //son asignados a cada control de interfaz
                this.txtCodigo.Text = producto.codigo;
                this.txtDescripcion.Text = producto.descripcion;
                this.txtPrecioCompra.Text = producto.precioCompra.ToString();
                this.nudDescuento.Value = this.producto.descuento;
                this.nudImpuesto.Value = this.producto.impuesto;
                this.dtpFechaRegistro.Value = this.producto.fechaRegistro;

                //se valida con un case para el estado del producto
                switch (producto.estado)
                {
                    case 'A':
                        //se realiza la seleccion de un item en el combobox
                        this.cbxEstado.SelectedIndex = 0;
                        break;
                    case 'I':
                        //se marca inactivo
                        this.cbxEstado.SelectedIndex = 1;
                        break;
                }//cierre de case

                //se asigna el estado de la pantalla para modificar
                this.estadoPantallaModificar();

            }

        }




        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {

                this.consultarProducto();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                //aquí en caso que no exista el producto se habilita la pantalla para agregar
                this.estadoPantallaAgregar();
            }
        }



        public  void estadoPantallaModificar()
        {
            this.btnConsultar.Enabled = false;
            this.btnAgregar.Enabled = false;
            this.btnModificar.Enabled = true;
            this.btnEliminar.Enabled = true;

            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Enabled = true;
            this.txtPrecioCompra.Enabled = true;
            this.nudDescuento.Enabled = true;
            this.nudImpuesto.Enabled = true;
            this.dtpFechaRegistro.Enabled = true;
            this.cbxEstado.Enabled = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                //se instancia un producto
                this.producto = new Productos();

                //se asignan los valores ingresados por el usuario
                this.producto.codigo = this.txtCodigo.Text;
                this.producto.descripcion = this.txtDescripcion.Text;
                this.producto.precioCompra = decimal.Parse(
                    this.txtPrecioCompra.Text.Trim());

                this.producto.descuento = int.Parse(
                    this.nudDescuento.Value.ToString());

                this.producto.impuesto = int.Parse(
                    this.nudImpuesto.Value.ToString());

                this.producto.fechaRegistro = this.dtpFechaRegistro.Value;

                this.producto.estado = char.Parse(
                    this.cbxEstado.Text.Substring(0, 1));

                //se utiliza el controlador enviando por parametro el producto
                this.controlador.modificar(this.producto);

                MessageBox.Show("Producto Modificado:\n" + this.producto.codigo +
                    "\nDescripcion " + this.producto.descripcion +
                    "\nPrecio " + this.producto.precioCompra
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //se utiliza el controlador enviando por parametro el producto
                this.controlador.eliminar(this.txtCodigo.Text);

                MessageBox.Show("Producto Eliminado","Eliminación",
                    MessageBoxButtons.OK,MessageBoxIcon.Information);

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.limpiarPantalla();
            this.estadoInicial();

        }

        private  void limpiarPantalla()
        {
            this.txtCodigo.Clear();
            this.txtDescripcion.Clear();

            this.txtPrecioCompra.Text = "0";
            
            //se asigna la fecha del sistema  DateTime.today
            this.dtpFechaRegistro.Value = DateTime.Today;

            //se asigna los spinner en  cero
            this.nudDescuento.Value=0;
            this.nudImpuesto.Value = 0;

            //seleccionar el primer item de la lista del combo
            this.cbxEstado.SelectedIndex = 0;

        }

        private void estadoPantallaAgregar()
        {
            //bloquear el codigo
            this.txtCodigo.Enabled = false;
            
            //se habilitan las demas entradas de datos corresponden a la informacion del producto
            this.txtDescripcion.Enabled = true;
            this.txtPrecioCompra.Enabled = true;
            this.dtpFechaRegistro.Enabled = true;
            this.nudDescuento.Enabled = true;
            this.nudImpuesto.Enabled = true;
            this.cbxEstado.Enabled = true;

            //Nada mas se deja activo el boton de agregar y cancelar
            this.btnConsultar.Enabled = false;
            this.btnAgregar.Enabled = true;
            this.btnModificar.Enabled = false;
            this.btnEliminar.Enabled = false;
            this.btnCancelar.Enabled = true;
        }

        private void txtCnsDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.dtgDatos.DataSource = this.controlador.busquedaProducto(this.txtCnsDescripcion.Text.Trim()).Tables[0];
                this.dtgDatos.ReadOnly = true;
                this.dtgDatos.AutoResizeColumns();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDatos_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.txtCodigo.Text = this.dtgDatos.SelectedRows[0].Cells[0].Value.ToString();
                this.consultarProducto();
                this.tabControl1.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }//cierre formulario
} //cierre namespace
