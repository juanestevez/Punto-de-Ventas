using Punto_de_ventas.ModelClass;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Punto_de_ventas
{
    public partial class FrmLogin : Form
    {
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

        private void btnIngresar_Click(object sender, EventArgs e)
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
                }
            }
        }

        
    }
}
