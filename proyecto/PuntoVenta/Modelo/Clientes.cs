using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Clientes
    {
        private string strCedula;
        private string strNombreCompleto;
        private int intTelefono;
        private string strDireccion;

        public string cedula
        {
            set
            {
                if (value.Trim().Equals(""))
                    throw new Exception("No se permite el cedula en blanco");
                else
                    this.strCedula = value;
            }
            get
            {
                return this.strCedula;
            }
        }
        public string NombreCompleto
        {
            set
            {
                if (value.Trim().Equals(""))
                    throw new Exception("No se permite el nombre en blanco");
                else
                    this.strNombreCompleto = value;
            }
            get
            {
                return this.strNombreCompleto;
            }
        }

        public int telefono
        {
            set
            {
                if (value <= 0)
                    throw new Exception("No se permite el telefono en blanco");
                else
                    this.intTelefono = value;
            }
            get
            {
                return this.intTelefono;
            }
        }

        public string direccion
        {
            set
            {
                if (value.Trim().Equals(""))
                    throw new Exception("No se permite el direccion en blanco");
                else
                    this.strDireccion = value;
            }
            get
            {
                return this.strDireccion;
            }
        }

    }///cierre de clase
}//cierre NameSpace
