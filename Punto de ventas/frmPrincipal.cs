using Punto_de_ventas.Connection;
using Punto_de_ventas.ModelClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas
{
    public partial class FrmPrincipal : Form
    {
        private string accion = "insert", paginas = "4", deudaActual, pago, día, fecha;
        TextBoxEvent evento = new TextBoxEvent();
        Cliente cliente = new Cliente();

        public FrmPrincipal()
        {
            InitializeComponent();

            #region Clientes
            radioIngresarCliente.Checked = true;
            radioIngresarCliente.ForeColor = Color.DarkCyan;
            #endregion
        }

        #region Clientes
        private void BtnClientes_Click(object sender, EventArgs e)
        {
            paginas = "1";
            accion = "insert";
            tabControl1.SelectedIndex = 1;
            CargarDatos();
            btnClientes.Enabled = false;
            btnVentas.Enabled = true;
            btnProductos.Enabled = true;
            btnCompras.Enabled = true;
            btnDepto.Enabled = true;
            btnCompras.Enabled = true;
        }

        private void RadioIngresarCliente_CheckedChanged(object sender, EventArgs e)
        {
            radioIngresarCliente.ForeColor = Color.DarkCyan;
            radioPagosDeudas.ForeColor = Color.Black;

            textBox_Id.ReadOnly = false;
            textBox_Nombre.ReadOnly = false;
            textBox_Apellido.ReadOnly = false;
            textBox_Direccion.ReadOnly = false;
            textBox_Telefono.ReadOnly = false;
            textBox_PagoscCliente.ReadOnly = true;

            label_PagoCliente.Text = "Pago de deudas";
            label_PagoCliente.ForeColor = Color.DarkCyan;
        }

        private void RadioPagosDeudas_CheckedChanged(object sender, EventArgs e)
        {
            radioPagosDeudas.ForeColor = Color.DarkCyan;
            radioIngresarCliente.ForeColor = Color.Black;

            textBox_Id.ReadOnly = false;
            textBox_Nombre.ReadOnly = true;
            textBox_Apellido.ReadOnly = true;
            textBox_Direccion.ReadOnly = true;
            textBox_Telefono.ReadOnly = true;
            textBox_PagoscCliente.ReadOnly = false;
        }        

        private void CargarDatos()
        {

        }

        private void TextBox_Id_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Id.Text == "")
            {
                label_Id.ForeColor = Color.LightSlateGray;
            }
            else
            {
                label_Id.ForeColor = Color.Green;
            }
        }

        private void TextBox_Id_KeyPress(object sender, KeyPressEventArgs e)
        {
            evento.NumberKeyPress(e);
        }

        private void TextBox_Nombre_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Nombre.Text == "")
            {
                label_Nombre.ForeColor = Color.LightSlateGray;
            }
            else
            {
                label_Nombre.Text = "Nombre completo";
                label_Nombre.ForeColor = Color.Green;
            }
        }

        private void TextBox_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            evento.TextKeyPress(e);
        }

        private void TextBox_Apellido_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Apellido.Text == "")
            {
                label_Apellido.ForeColor = Color.LightSlateGray;
            }
            else
            {
                label_Apellido.Text = "Apellido";
                label_Apellido.ForeColor = Color.Green;
            }
        }

        private void TextBox_Apellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            evento.TextKeyPress(e);
        }

        private void TextBox_Direccion_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Direccion.Text == "")
            {
                label_Direccion.ForeColor = Color.LightSlateGray;
            }
            else
            {
                label_Direccion.Text = "Dirección";
                label_Direccion.ForeColor = Color.Green;
            }
        }


        private void TextBox_Telefono_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Telefono.Text == "")
            {
                label_Telefono.ForeColor = Color.LightSlateGray;
            }
            else
            {
                label_Telefono.Text = "Teléfono";
                label_Telefono.ForeColor = Color.Green;
            }
        }

        private void TextBox_Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            evento.NumberKeyPress(e);
        }

        private void TextBox_PagoscCliente_TextChanged(object sender, EventArgs e)
        {
            if (textBox_PagoscCliente.Text == "")
            {
                textBox_PagoscCliente.ForeColor = Color.LightSlateGray;
            }
            else
            {
                textBox_PagoscCliente.Text = "Pago de deudas";
                textBox_PagoscCliente.ForeColor = Color.Green;
            }
        }

        private void TextBox_PagoscCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            evento.NumberDecimalKreyPress(textBox_PagoscCliente, e);
        }

        private void Button_GuardarCliente_Click(object sender, EventArgs e)
        {
            if (radioIngresarCliente.Checked)
            {
                GuardarCliente();
            }
            
        }

        private void GuardarCliente()
        {
            if (textBox_Id.Text == "")
            {
                label_Id.ForeColor = Color.Red;
                textBox_Id.Focus();
            }
            else
            {
                if (textBox_Nombre.Text == "")
                {
                    label_Nombre.ForeColor = Color.Red;
                    textBox_Nombre.Focus();
                }
                else
                {
                    if (textBox_Apellido.Text == "")
                    {
                        label_Apellido.ForeColor = Color.Red;
                        textBox_Apellido.Focus();
                    }
                    else
                    {
                        if (textBox_Direccion.Text == "")
                        {
                            label_Direccion.ForeColor = Color.Red;
                            textBox_Direccion.Focus();
                        }
                        else
                        {
                            if (textBox_Telefono.Text == "")
                            {
                                label_Telefono.ForeColor = Color.Red;
                                textBox_Telefono.Focus();
                            }
                            else
                            {
                                if (accion == "insert")
                                {
                                    cliente.InsertCliente(textBox_Id.Text, textBox_Nombre.Text, textBox_Apellido.Text,
                                        textBox_Direccion.Text, textBox_Telefono.Text);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion


    }
}
