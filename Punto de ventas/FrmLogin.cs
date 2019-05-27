using Punto_de_ventas.ModelClass;
using Punto_de_ventas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Punto_de_ventas
{
    public partial class FrmLogin : Form
    {
        private Usuario usuario = new Usuario();      

        public FrmLogin()
        {
            InitializeComponent();
            lblMensaje.Text = "";
        }

        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                lblUsuario.ForeColor = Color.Red;
            }
            else
            {
                lblUsuario.Text = "Usuario";
                lblUsuario.ForeColor = Color.Green;
            }
        }

        private void TxtContrasena_TextChanged(object sender, EventArgs e)
        {
            if (txtContrasena.Text == "")
            {
                lblContrasena.ForeColor = Color.Red;
            }
            else
            {
                lblContrasena.Text = "Contraseña";
                lblContrasena.ForeColor = Color.Green;
            }
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            if (txtUsuario.Text == "")
            {
                lblUsuario.Text = "Ingrese su usuario";
                lblUsuario.ForeColor = Color.Red;
                txtUsuario.Focus();
            }
            else
            {
                if (txtContrasena.Text == "")
                {
                    lblContrasena.Text = "Ingrese su contraseña";
                    lblContrasena.ForeColor = Color.Red;
                    txtContrasena.Focus();
                }
                else
                {
                    object[] objects = usuario.Login(txtUsuario.Text, txtContrasena.Text);
                    List<Usuarios> listUsuario = (List<Usuarios>)objects[0];
                    List<Cajas> listCaja = (List<Cajas>)objects[1];

                    if (listUsuario.Count > 0) // Contiene un usuario
                    {
                        if ("Administrador" == listUsuario[0].Rol)
                        {
                            FrmPrincipal _form = new FrmPrincipal(listUsuario, listCaja);
                            _form.Show();
                            Visible = false;
                        }
                        else
                        {
                            if (listCaja.Count > 0) // ¿Hay caja disponible?
                            {
                                FrmPrincipal _form = new FrmPrincipal(listUsuario, listCaja);
                                _form.Show();
                                Visible = false;
                            }
                            else
                            {
                                lblMensaje.Text = "No hay cajas disponibles";
                            }
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "Datos incorrectos";
                    }
                }
            }
        }

        
    }
}
