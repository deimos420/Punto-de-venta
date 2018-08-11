using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//driver para SQL Server
using System.Data.SqlClient;

//importacion del modelo
using Modelo;

//importacion de DataSet
using System.Data;

namespace Controlador
{
    public class ControladorADO
    {
        //ADO  objetos Acceso Datos 
        //Access Data Objects
        //Este objeto permite crear la conexion con el servidor
        private SqlConnection conexion;

        //Este objeto permite realizar los comandos de insercion
        private SqlCommand comando;

        //El string de conexion almacena el nombre del servidor-bd-user-password
        private String stringConexion;

        //método encargado de almacenar un producto
        public void registrar(Productos pProducto)
        {
            try//controlar un error en caso que exista
            {
                //se crea una instancia de una conexion con el servidor
                this.conexion = new SqlConnection(this.stringConexion);

                //se intenta abrir la conexion
                this.conexion.Open();

                //se instancia el comando TRANSAC-SQL
                this.comando = new SqlCommand();

                ///se debe asignar la conexion al comando
                this.comando.Connection = this.conexion;

                //se indica el tipo de comando
                this.comando.CommandType = System.Data.CommandType.Text;

                //asignar el comando transac-sql a realizar
                this.comando.CommandText = "insert into [Productos](codigo,descripcion,precioCompra," +
                    "descuento,impuesto,fechaRegistro,estado)values('"
                    +pProducto.codigo + "','" + pProducto.descripcion +"',"+ pProducto.precioCompra +","+ 
                    pProducto.descuento +","+ pProducto.impuesto +",'"+ pProducto.fechaRegistro +"','"+
                    pProducto.estado +"')";
                //se inserta en la base datos la información del producto
                this.comando.ExecuteNonQuery();

                //liberacion de la memoria
                this.conexion.Close();
                this.conexion.Dispose();
                this.comando.Dispose();
            }
            catch (Exception  ex) //Retorna el error existente
            {

                throw ex;
            }

        }

        //constructor de clase recibe como parametro los datos de la conexion
        public ControladorADO(String pStringCnx)
        {
            this.stringConexion = pStringCnx;
        }
        

        public Productos consultar(string codigo)
        {
            try
            {
                //Pasos para consultar en la base datos
                //1) Crear la conexion
                this.conexion = new SqlConnection(this.stringConexion);
                //¿Que funcion cumple el string de conexion?
                //Se encarga de indicar el nombre del servidor- la base datos - usuario-contraseña
                //estos datos son necesarios para conectarse al Servidor SQL 

                //2) Abrir la conexion
                this.conexion.Open();

                //3) Instancia un comando para ejecutar TRANSAC-SQL
                this.comando = new SqlCommand();

                //4) Asignar al comando la conexion sql
                this.comando.Connection = this.conexion;

                //5) indicar que tipo de comando va ejecutar
                this.comando.CommandType = System.Data.CommandType.Text;

                //6) Escribir el TRANSAC-SQL para consultar
                this.comando.CommandText = "select c.codigo,c.descripcion,c.precioCompra,c.descuento, c.impuesto,c.fechaRegistro,c.estado from Productos c where codigo= '" + codigo + "'";

                //7) declarar un objeto de lectura datos
                SqlDataReader lectorDatos;

                //8) Asignar el resultado del comando al lector
                lectorDatos = this.comando.ExecuteReader();

                //declaracion de variable producto
                Productos varTemporal = null;

                //9) Leer los datos obtenidos del servidor
                if (lectorDatos.Read())
                {
                    //se instancia una objeto producto
                     varTemporal = new Productos();
                    //0 es la posicion donde se ubica el valor del codigo en la consulta
                    varTemporal.codigo = lectorDatos.GetValue(0).ToString();
                    varTemporal.descripcion = lectorDatos.GetValue(1).ToString();
                    varTemporal.precioCompra = decimal.Parse(lectorDatos.GetValue(2).ToString());
                    varTemporal.descuento = int.Parse(lectorDatos.GetValue(3).ToString());
                    varTemporal.impuesto = int.Parse(lectorDatos.GetValue(4).ToString());
                    varTemporal.fechaRegistro = DateTime.Parse(lectorDatos.GetValue(5).ToString());
                    varTemporal.estado = char.Parse(lectorDatos.GetValue(6).ToString());
                 }
                else  //Si la funcion .Read() devuelve un falso muestra q no existe
                {
                    throw new Exception("No existe ningún producto con dicho código");
                }

                //se retorna el objeto en la interfaz grafica.
                return varTemporal;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public  void modificar(Productos varProducto)
        {
            try
            {
                //se crea una instancia de una conexion con el servidor
                this.conexion = new SqlConnection(this.stringConexion);

                //se intenta abrir la conexion
                this.conexion.Open();

                //se instancia el comando TRANSAC-SQL
                this.comando = new SqlCommand();

                ///se debe asignar la conexion al comando
                this.comando.Connection = this.conexion;

                //se indica el tipo de comando
                this.comando.CommandType = System.Data.CommandType.Text;

                //se asigna el TRASAC-SQL para modificar
                this.comando.CommandText = "update Productos set descripcion = '" + varProducto.descripcion + "', precioCompra = " + varProducto.precioCompra + ", descuento = " + varProducto.descuento + ", impuesto = " + varProducto.impuesto + ", fechaRegistro= '" + varProducto.fechaRegistro + "', estado = '" + varProducto.estado + "' where codigo = '" + varProducto.codigo + "'";

                //se ejecuta el comando
                this.comando.ExecuteNonQuery();

                //liberacion de memoria
                this.conexion.Close();
                this.conexion.Dispose();
                this.comando.Dispose();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public  void eliminar(string codigo)
        {
            try
            {
                //se crea una instancia de una conexion con el servidor
                this.conexion = new SqlConnection(this.stringConexion);

                //se intenta abrir la conexion
                this.conexion.Open();

                //se instancia el comando TRANSAC-SQL
                this.comando = new SqlCommand();

                ///se debe asignar la conexion al comando
                this.comando.Connection = this.conexion;

                //se indica el tipo de comando
                this.comando.CommandType = System.Data.CommandType.Text;

                //se asigna el TRANSAC-SQL para eliminar
                this.comando.CommandText = "update productos set estado = 'E' where codigo = '" + codigo + "'";

                //se ejecuta el comando
                this.comando.ExecuteNonQuery();

                //se libera la memoria
                this.conexion.Close();
                this.conexion.Dispose();
                this.comando.Dispose();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataSet busquedaProducto(string descrip)
        {
            try
            {
                //cada ves que se necesite ir ha realizar alguna funcion con la base datos
                //se debe instanciar una conexion
                this.conexion = new SqlConnection(this.stringConexion);

                //cuando se tiene una instancia de conexion se debe intentar abrir la conexion
                this.conexion.Open();

                //se  instancia un comando
                this.comando = new SqlCommand();

                //se asigna la conexion
                this.comando.Connection = this.conexion;

                //se indica el tipo de comando
                this.comando.CommandType = CommandType.StoredProcedure;

                //se  indica el procedimiento almacenado que ejecutará el comando
                this.comando.CommandText = "[Sp_Cns_Productos]";

                //se debe asignar los parámetros del comando
                this.comando.Parameters.AddWithValue("@pDescrip", descrip);

                //DataSet es el contenedor de los datos que provienen de la base datos
                //se instancia un contenedor
                DataSet datos = new DataSet();

                //se instancia un adaptador, este se encarga de llenar el dataset con los datos
                //obtenidos por medio del comando provenientes de la base datos
                SqlDataAdapter adaptador = new SqlDataAdapter();

                //se asigna el comando de seleccion
                adaptador.SelectCommand = this.comando;

                //se llena el dataset con los datos seleccionados por el comando
                //metodo FILL (  DataSet ) recibe un dataset y lo llena de datos
                adaptador.Fill(datos);

                //se cierre la conexion
                this.conexion.Close();
                this.conexion.Dispose();
                this.comando.Dispose();
                adaptador.Dispose();

                return datos;

           }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool autentificado(Usuario pUser)
        {
            try
            {
                bool autorizado = false;

                //se intancia la conexion
                this.conexion = new SqlConnection(this.stringConexion);

                // se abre la conexion
                this.conexion.Open();
                //se intancia el comando
                this.comando = new SqlCommand();
                //se asigna la conexion
                this.comando.Connection = this.conexion;
                //se asigna el comando TRANSAC-SQL
                this.comando.CommandText = "select login from Usuario where login = '"+pUser.login+"' and dbo.fn_DesencryptarPassword(password) = '"+pUser.password+"'";
                //indicar el tipo de comando
                this.comando.CommandType = CommandType.Text;

                SqlDataReader lectordatos = this.comando.ExecuteReader();

                if (lectordatos.Read())
                {
                    autorizado= true;
                }
                else
                {
                    autorizado = false;
                }
                //libera la memoria
                this.conexion.Close();
                this.conexion.Dispose();
                this.comando.Dispose();
                lectordatos = null;

                return autorizado;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /////////////////////////////////////////////////////////////////////////////////


        public void registrarClientes(Clientes cClientes)
        {
            try//controlar un error en caso que exista
            {
                //se crea una instancia de una conexion con el servidor
                this.conexion = new SqlConnection(this.stringConexion);

                //se intenta abrir la conexion
                this.conexion.Open();

                //se instancia el comando TRANSAC-SQL
                this.comando = new SqlCommand();

                ///se debe asignar la conexion al comando
                this.comando.Connection = this.conexion;

                //se indica el tipo de comando
                this.comando.CommandType = System.Data.CommandType.Text;

                //asignar el comando transac-sql a realizar
                this.comando.CommandText = "insert into [Clientes](cedula,nombreCompleto,telefono,direccion )values('"
                    + cClientes.cedula + "','" + cClientes.NombreCompleto + "'," + cClientes.telefono + ",'" + cClientes.direccion + "' )";
                //se inserta en la base datos la información del producto
                this.comando.ExecuteNonQuery();

                //liberacion de la memoria
                this.conexion.Close();
                this.conexion.Dispose();
                this.comando.Dispose();
            }
            catch (Exception ex) //Retorna el error existente
            {

                throw ex;
            }

        }

        public Clientes consultarCliente(string cedula)
        {
            try
            {
                //Pasos para consultar en la base datos
                //1) Crear la conexion
                this.conexion = new SqlConnection(this.stringConexion);
                //¿Que funcion cumple el string de conexion?
                //Se encarga de indicar el nombre del servidor- la base datos - usuario-contraseña
                //estos datos son necesarios para conectarse al Servidor SQL 

                //2) Abrir la conexion
                this.conexion.Open();

                //3) Instancia un comando para ejecutar TRANSAC-SQL
                this.comando = new SqlCommand();

                //4) Asignar al comando la conexion sql
                this.comando.Connection = this.conexion;

                //5) indicar que tipo de comando va ejecutar
                this.comando.CommandType = System.Data.CommandType.Text;

                //6) Escribir el TRANSAC-SQL para consultar
                this.comando.CommandText = "select c.cedula,c.nombreCompleto,c.telefono,c.direccion from Clientes c where cedula= '" + cedula + "'";

                //7) declarar un objeto de lectura datos
                SqlDataReader lectorDatos;

                //8) Asignar el resultado del comando al lector
                lectorDatos = this.comando.ExecuteReader();

                //declaracion de variable producto
                Clientes varTemporal = null;

                //9) Leer los datos obtenidos del servidor
                if (lectorDatos.Read())
                {
                    //se instancia una objeto producto
                    varTemporal = new Clientes();
                    //0 es la posicion donde se ubica el valor del codigo en la consulta
                    varTemporal.cedula = lectorDatos.GetValue(0).ToString();
                    varTemporal.NombreCompleto = lectorDatos.GetValue(1).ToString();
                    varTemporal.telefono = int.Parse(lectorDatos.GetValue(2).ToString());
                    varTemporal.direccion = lectorDatos.GetValue(3).ToString();
                }
                else  //Si la funcion .Read() devuelve un falso muestra q no existe
                {
                    throw new Exception("No existe ningún Cliente con dicho código");
                }

                //se retorna el objeto en la interfaz grafica.
                return varTemporal;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void modificarClientes(Clientes varClientes)
        {
            try
            {
                //se crea una instancia de una conexion con el servidor
                this.conexion = new SqlConnection(this.stringConexion);

                //se intenta abrir la conexion
                this.conexion.Open();

                //se instancia el comando TRANSAC-SQL
                this.comando = new SqlCommand();

                ///se debe asignar la conexion al comando
                this.comando.Connection = this.conexion;

                //se indica el tipo de comando
                this.comando.CommandType = System.Data.CommandType.Text;

                //se asigna el TRASAC-SQL para modificar
                this.comando.CommandText = "update Clientes set nombreCompleto = '" + varClientes.NombreCompleto + "', telefono = " + varClientes.telefono + ", Direccion= '" + varClientes.direccion + "' where cedula = '" + varClientes.cedula + "'";

                //se ejecuta el comando
                this.comando.ExecuteNonQuery();

                //liberacion de memoria
                this.conexion.Close();
                this.conexion.Dispose();
                this.comando.Dispose();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void eliminarClientes(string cedula)
        {
            try
            {
                //se crea una instancia de una conexion con el servidor
                this.conexion = new SqlConnection(this.stringConexion);

                //se intenta abrir la conexion
                this.conexion.Open();

                //se instancia el comando TRANSAC-SQL
                this.comando = new SqlCommand();

                ///se debe asignar la conexion al comando
                this.comando.Connection = this.conexion;

                //se indica el tipo de comando
                this.comando.CommandType = System.Data.CommandType.Text;

                //se asigna el TRANSAC-SQL para eliminar
                this.comando.CommandText = " delete from [Clientes]where cedula ='" + cedula + "'";

                //se ejecuta el comando
                this.comando.ExecuteNonQuery();

                //se libera la memoria
                this.conexion.Close();
                this.conexion.Dispose();
                this.comando.Dispose();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
              

    }//cierre de clase
}//cierre de name space
