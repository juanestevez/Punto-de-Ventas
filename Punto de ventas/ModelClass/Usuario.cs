using Punto_de_ventas.Connection;
using Punto_de_ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Punto_de_ventas.ModelClass
{
    public class Usuario : Conexion
    {
        private List<Usuarios> listUsuarios, listUsuario;
        private List<Cajas> listCajas, listCaja;
        private string fecha = DateTime.Now.ToString("dd/MM/yyyy");
        private string hora = DateTime.Now.ToString("hh:mm:ss");

        public Usuario()
        {
            listUsuario = new List<Usuarios>();
            listCaja = new List<Cajas>();
        }

        public object[] Login(string usuario, string password)
        {
            listUsuarios = TablaUsuarios.Where(u => u.Usuario == usuario).ToList();
            if (0 < listUsuarios.Count)
            {
                string pass = Encriptar.DesencriptarDatos(listUsuarios[0].Password, listUsuarios[0].Usuario);
                if (pass == password)
                {
                    listUsuario = listUsuarios;
                    int idUsuario = listUsuarios[0].IdUsuario;
                    string nombre = listUsuarios[0].Nombre;
                    string apellido = listUsuarios[0].Apellido;
                    string user = listUsuarios[0].Usuario;
                    string rol = listUsuarios[0].Rol;
                }
            }
        }
    }
}
