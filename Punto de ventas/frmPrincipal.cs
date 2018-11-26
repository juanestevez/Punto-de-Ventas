using Punto_de_ventas.ModelClass;
using Punto_de_ventas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Punto_de_ventas
{
    public partial class FrmPrincipal : Form
    {
        private string accion = "insert", deudaActual, pago, día, fecha;
        private int paginas = 4, maxReg, pageCount, pageSize = 10, numeroPagina = 1, idCliente = 0;
        private int idRegistro = 0;

        TextBoxEvent evento = new TextBoxEvent();
        Cliente cliente = new Cliente();
        List<Clientes> numCliente = new List<Clientes>();

        public FrmPrincipal()
        {
            InitializeComponent();

            #region Clientes
            radioIngresarCliente.Checked = true;
            radioIngresarCliente.ForeColor = Color.DarkCyan;
            cliente.BuscarCliente(dataGridView_Cliente, "", 1, pageSize);
            cliente.GetReporteCliente(dataGridView_ClienteReporte, idCliente);
            #endregion
        }

        #region Paginador

        private void CargarDatos()
        {
            switch (paginas)
            {
                case 1:
                    numCliente = cliente.GetClientes();
                    cliente.BuscarCliente(dataGridView_Cliente, "", 1, pageSize);                    
                    maxReg = numCliente.Count;
                    break;
            }

            pageCount = (maxReg / pageSize);

            if ((maxReg % pageSize) > 0)
            {
                pageCount += 1;
            }
            label_PaginasCliente.Text = $"Página 1 de {pageCount.ToString()}";

        }

        #endregion

        #region Clientes
        private void BtnClientes_Click(object sender, EventArgs e)
        {
            paginas = 1;
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

        private void Button_PrimeroClientes_Click(object sender, EventArgs e)
        {
            numeroPagina = 1;
            label_PaginasCliente.Text = $"Página {numeroPagina.ToString()} de {pageCount.ToString()}";
            cliente.BuscarCliente(dataGridView_Cliente, "", 1, pageSize);
        }

        private void Button_Cancelar_Click(object sender, EventArgs e)
        {
            RestablecerCliente();
        }

        private void DataGridView_Cliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Cliente.Rows.Count != 0) // El datagrid tiene datos
            {
                DataGridViewCliente();
            }
        }

        private void DataGridView_Cliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_Cliente.Rows.Count != 0) // El datagrid tiene datos
            {
                DataGridViewCliente();
            }
        }

        private void Button_AnteriorClientes_Click(object sender, EventArgs e)
        {
            if (numeroPagina > 1)
            {
                numeroPagina -= 1;
                label_PaginasCliente.Text = $"Página {numeroPagina.ToString()} de {pageCount.ToString()}";
                cliente.BuscarCliente(dataGridView_Cliente, "", numeroPagina, pageSize);
            }
        }

        private void Button_EliminarClientes_Click(object sender, EventArgs e)
        {
            if (idCliente > 0) // ¿Hay un registro seleccionado?
            {
                if (MessageBox.Show("Se eliminará el registro, esta acción no se puede deshacer. ¿Desea continuar?", 
                    "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cliente.EliminarCliente(idCliente, idRegistro);
                    RestablecerCliente();
                }
            }
        }

        private void TextBox_BuscarCliente_TextChanged(object sender, EventArgs e)
        {
            cliente.BuscarCliente(dataGridView_Cliente, textBox_BuscarCliente.Text, 1, pageSize);
        }

        private void Button_SiguienteClientes_Click(object sender, EventArgs e)
        {
            if (numeroPagina < pageCount)
            {
                numeroPagina += 1;
                label_PaginasCliente.Text = $"Página {numeroPagina.ToString()} de {pageCount.ToString()}";
                cliente.BuscarCliente(dataGridView_Cliente, "", numeroPagina, pageSize);
            }
        }

        private void Button_UltimoClientes_Click(object sender, EventArgs e)
        {
            numeroPagina = pageCount;
            label_PaginasCliente.Text = $"Página {numeroPagina.ToString()} de {pageCount.ToString()}";
            cliente.BuscarCliente(dataGridView_Cliente, "", pageCount, pageSize);
        }

        private void TextBox_Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            evento.NumberKeyPress(e);
        }

        private void TextBox_PagoscCliente_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView_ClienteReporte.CurrentRow == null)
            {
                label_PagoCliente.Text = "Selecciona el cliente";
                label_PagoCliente.ForeColor = Color.Red;
                textBox_PagoscCliente.Text = "";
            }
            else
            {
                if (textBox_PagoscCliente.Text != "")
                {
                    String deuda1;
                    Decimal deuda2, deuda3, deudaTotal;
                    label_PagoCliente.Text = "Pagos de deuda";
                    textBox_PagoscCliente.ForeColor = Color.LightSlateGray;
                    deuda1 = Convert.ToString(dataGridView_ClienteReporte.CurrentRow.Cells[3].Value);
                    deuda1 = deuda1.Replace("$", "");
                    deuda2 = Convert.ToDecimal(deuda1);

                    deuda3 = Convert.ToDecimal(textBox_PagoscCliente.Text);

                    deudaTotal = deuda2 - deuda3;
                    deudaActual = String.Format("{0:#,###,###,##0.00####}", deudaTotal);
                    pago = String.Format("{0:#,###,###,##0.00####}", textBox_PagoscCliente.Text);
                }
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
            else
            {
                GuardarPago();
            }
        }

        private void GuardarPago()
        {
            if (textBox_PagoscCliente.Text == "")
            {
                label_PagoCliente.Text = "Ingresee el pago";
                label_PagoCliente.ForeColor = Color.Red;
                textBox_PagoscCliente.Focus();
            }
            else
            {
                cliente.ActualizarReporte(deudaActual, pago, idCliente);
                RestablecerCliente();
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
                                else if (accion == "update")
                                {
                                    cliente.ActualizarCliente(textBox_Id.Text, textBox_Nombre.Text, textBox_Apellido.Text,
                                        textBox_Direccion.Text, textBox_Telefono.Text, idCliente);
                                }  
                                RestablecerCliente();
                            }
                        }
                    }
                }
            }
        }

        private void RestablecerCliente()
        {
            accion = "insert";
            idCliente = 0;
            idRegistro = 0;
            CargarDatos();
            textBox_Id.Text = "";
            textBox_Nombre.Text = "";
            textBox_Apellido.Text = "";
            textBox_Direccion.Text = "";
            textBox_Telefono.Text = "";
            textBox_PagoscCliente.Text = "";
            textBox_Id.Focus();
            textBox_BuscarCliente.Text = "";
            label_Id.ForeColor = Color.LightSlateGray;
            label_Nombre.ForeColor = Color.LightSlateGray;
            label_Apellido.ForeColor = Color.LightSlateGray;
            label_Direccion.ForeColor = Color.LightSlateGray;
            label_Telefono.ForeColor = Color.LightSlateGray;
            label_PagoCliente.ForeColor = Color.LightSlateGray;
            label_PagoCliente.Text = "Pagos de deudas";
            radioIngresarCliente.Checked = true;
            radioIngresarCliente.ForeColor = Color.DarkCyan;
            label_NombreRB.Text = "";
            label_ApellidoRB.Text = "";
            label_ClienteSA.Text = "0.00";
            label_ClienteUP.Text = "0.00";
            label_FechaPG.Text = "";
            cliente.GetReporteCliente(dataGridView_ClienteReporte, idCliente);
        }

        private void DataGridViewCliente()
        {
            accion = "update";
            idCliente = Convert.ToInt16(dataGridView_Cliente.CurrentRow.Cells[0].Value);
            textBox_Id.Text = Convert.ToString(dataGridView_Cliente.CurrentRow.Cells[1].Value);
            textBox_Nombre.Text = Convert.ToString(dataGridView_Cliente.CurrentRow.Cells[2].Value);
            textBox_Apellido.Text = Convert.ToString(dataGridView_Cliente.CurrentRow.Cells[3].Value);
            textBox_Direccion.Text = Convert.ToString(dataGridView_Cliente.CurrentRow.Cells[4].Value);
            textBox_Telefono.Text = Convert.ToString(dataGridView_Cliente.CurrentRow.Cells[5].Value);
            cliente.GetReporteCliente(dataGridView_ClienteReporte, idCliente);

            idRegistro = Convert.ToInt16(dataGridView_ClienteReporte.CurrentRow.Cells[0].Value);
            label_NombreRB.Text = Convert.ToString(dataGridView_ClienteReporte.CurrentRow.Cells[1].Value);
            label_ApellidoRB.Text = Convert.ToString(dataGridView_ClienteReporte.CurrentRow.Cells[2].Value);
            label_ClienteSA.Text = Convert.ToString(dataGridView_ClienteReporte.CurrentRow.Cells[3].Value);
            label_FechaPG.Text = Convert.ToString(dataGridView_ClienteReporte.CurrentRow.Cells[4].Value);
            label_ClienteUP.Text = Convert.ToString(dataGridView_ClienteReporte.CurrentRow.Cells[5].Value);
        }
        #endregion


    }
}
