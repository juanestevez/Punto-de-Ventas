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
        private int vistaActual = 0, maxReg, pageCount, pageSize = 10, numeroPagina = 1, idCliente = 0;
        private int idRegistro = 0;
        private int idProveedor;

        TextBoxEvent evento = new TextBoxEvent();
        Cliente cliente = new Cliente();
        List<Clientes> numCliente = new List<Clientes>();
        List<Proveedores> numProveedores = new List<Proveedores>();
        Proveedor proveedor = new Proveedor();

        public FrmPrincipal(List<Usuarios> listUsuario, List<Cajas> listCaja)
        {
            InitializeComponent();
            
            #region Clientes
            radioIngresarCliente.Checked = true;
            radioIngresarCliente.ForeColor = Color.DarkCyan;
            cliente.BuscarCliente(dataGridView_Cliente, "", 1, pageSize);
            cliente.GetReporteCliente(dataGridView_ClienteReporte, idCliente);
            #endregion

            #region Proveedores
            radioProveedorAgregar.Checked = true;
            radioProveedorAgregar.ForeColor = Color.DarkCyan;
            proveedor.ObtenerReporte(gridProveedorReportes, idProveedor);
            #endregion
        }

        #region Paginador

        private void CargarDatos()
        {
            switch (vistaActual)
            {
                case 1:
                    numCliente = cliente.GetClientes();
                    cliente.BuscarCliente(dataGridView_Cliente, "", 1, pageSize);                    
                    maxReg = numCliente.Count;
                    break;
                case 2:
                    numProveedores = proveedor.GetProveedores();
                    proveedor.BuscarProveedor(gridProveedores, "", 1, pageSize);
                    maxReg = numProveedores.Count;
                    break;
            }

            pageCount = (maxReg / pageSize);

            if ((maxReg % pageSize) > 0)
            {
                pageCount += 1;
            }
            string textoPaginas = $"Página 1 de {pageCount.ToString()}";
            switch (vistaActual)
            {
                case 1:
                    label_PaginasCliente.Text = textoPaginas;
                    break;
                case 2:
                    lblProveedorPaginas.Text = textoPaginas;
                    break;
            }            
        }

        #endregion

        #region Clientes
        private void BtnClientes_Click(object sender, EventArgs e)
        {
            vistaActual = 1;
            RestablecerCliente();
            tabControl1.SelectedIndex = 1;
            btnClientes.Enabled = false;
            btnVentas.Enabled = true;
            btnProveedores.Enabled = true;
            btnProductos.Enabled = true;
            btnCompras.Enabled = true;
            btnDepto.Enabled = true;
            CargarDatos();
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

            textBox_Id.ReadOnly = true;
            textBox_Nombre.ReadOnly = true;
            textBox_Apellido.ReadOnly = true;
            textBox_Direccion.ReadOnly = true;
            textBox_Telefono.ReadOnly = true;
            textBox_PagoscCliente.ReadOnly = false;
        }

        #region Validacion de campos
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

        private void TextBox_PagoscCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            evento.NumberDecimalKreyPress(textBox_PagoscCliente, e);
        }

        #endregion

        #region Paginacion
        private void Button_PrimeroClientes_Click(object sender, EventArgs e)
        {
            numeroPagina = 1;
            label_PaginasCliente.Text = $"Página {numeroPagina.ToString()} de {pageCount.ToString()}";
            cliente.BuscarCliente(dataGridView_Cliente, "", 1, pageSize);
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
        #endregion

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

        private void Button_Cancelar_Click(object sender, EventArgs e)
        {
            RestablecerCliente();
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

        private void DataGridView_Cliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Cliente.Rows.Count != 0) // El datagrid tiene datos
            {
                DataGridViewCliente();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView_Cliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_Cliente.Rows.Count != 0) // El datagrid tiene datos
            {
                DataGridViewCliente();
            }
        }

        private void TextBox_BuscarCliente_TextChanged(object sender, EventArgs e)
        {
            cliente.BuscarCliente(dataGridView_Cliente, textBox_BuscarCliente.Text, 1, pageSize);
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

        private void GuardarPago()
        {
            if (textBox_PagoscCliente.Text == "")
            {
                label_PagoCliente.Text = "Ingrese el pago";
                label_PagoCliente.ForeColor = Color.Red;
                textBox_PagoscCliente.Focus();
            }
            else
            {
                cliente.ActualizarReporte(deudaActual, pago, idCliente);
                RestablecerCliente();
            }
        }

        private void RestablecerCliente()
        {
            accion = "insert";
            numeroPagina = 1;
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
            label_ClienteSA.Text = "";
            label_ClienteUP.Text = "";
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

        private void button_ImprCliente_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(groupBox_ReciboCliente.Width, groupBox_ReciboCliente.Height);
            groupBox_ReciboCliente.DrawToBitmap(bm, new Rectangle(0, 0, groupBox_ReciboCliente.Width, 
                groupBox_ReciboCliente.Height));
            e.Graphics.DrawImage(bm, 50, 50);
        }
        #endregion

        #region Proveedores

        private void BtnProveedores_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            btnVentas.Enabled = true;
            btnClientes.Enabled = true;
            btnProveedores.Enabled = false;
            btnProductos.Enabled = true;
            btnDepto.Enabled = true;
            btnCompras.Enabled = true;          
            
            ReestablecerProveedor();
        }

        private void RadioProveedorAgregar_CheckedChanged(object sender, EventArgs e)
        {
            radioProveedorAgregar.ForeColor = Color.DarkCyan;
            radioProveedorPago.ForeColor = Color.Black;
            txtProveedorNombre.ReadOnly = false;
            txtProveedorTelefono.ReadOnly = false;
            txtProveedorEmail.ReadOnly = false;
            txtProveedorPago.ReadOnly = true;
            lblProveedorPago.Text = "Pago de deudas";
            txtProveedorPago.ForeColor = Color.DarkCyan;
        }

        private void BtnProveedorGuardar_Click(object sender, EventArgs e)
        {
            if (radioProveedorAgregar.Checked)
            {
                AgregarProveedor();
            }
            else
            {
                GuardarPagoProveedor();
            }            
        }

        private void BtnProveedorEliminar_Click(object sender, EventArgs e)
        {
            if (idProveedor > 0)
            {
                if (MessageBox.Show("Se eliminará el proveedor, esta acción no se puede deshacer. \n ¿Desea eliminar al proveedor?", 
                    "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    proveedor.BorrarProveedor(idProveedor, idRegistro);
                    ReestablecerProveedor();
                }
            }
        }

        private void BtnProveedorCancelar_Click(object sender, EventArgs e)
        {
            ReestablecerProveedor();
        }

        private void RadioProveedorPago_CheckedChanged(object sender, EventArgs e)
        {
            radioProveedorPago.ForeColor = Color.DarkCyan;
            radioProveedorAgregar.ForeColor = Color.Black;
            txtProveedorNombre.ReadOnly = true;
            txtProveedorTelefono.ReadOnly = true;
            txtProveedorEmail.ReadOnly = true;
            txtProveedorPago.ReadOnly = false;
        }

        private void TxtProveedorNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtProveedorNombre.Text == "")
            {
                lblProveedorNombre.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lblProveedorNombre.Text = "Proveedor";
                lblProveedorNombre.ForeColor = Color.Green;
            }
        }

        private void TxtProveedorTelefono_TextChanged(object sender, EventArgs e)
        {
            if (txtProveedorTelefono.Text == "")
            {
                lblProveedorTelefono.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lblProveedorTelefono.Text = "Teléfono";
                lblProveedorTelefono.ForeColor = Color.Green;
            }
        }

        private void TxtProveedorBuscar_TextChanged(object sender, EventArgs e)
        {
            proveedor.BuscarProveedor(gridProveedores, txtProveedorBuscar.Text, 1, pageSize);
        }

        private void BtnProveedorPrimero_Click(object sender, EventArgs e)
        {
            numeroPagina = 1;
            lblProveedorPaginas.Text = $"Página {numeroPagina.ToString()} de {pageCount.ToString()}";
            proveedor.BuscarProveedor(gridProveedores, "", 1, pageSize);
        }

        private void BtnProveedorAnterior_Click(object sender, EventArgs e)
        {
            if (numeroPagina > 1)
            {
                numeroPagina -= 1;
                lblProveedorPaginas.Text = $"Página {numeroPagina.ToString()} de {pageCount.ToString()}";
                proveedor.BuscarProveedor(gridProveedores, "", numeroPagina, pageSize);
            }            
        }

        private void BtnProveedorSiguiente_Click(object sender, EventArgs e)
        {
            if (numeroPagina < pageCount)
            {
                numeroPagina += 1;
                lblProveedorPaginas.Text = $"Página {numeroPagina.ToString()} de {pageCount.ToString()}";
                proveedor.BuscarProveedor(gridProveedores, "", numeroPagina, pageSize);
            }            
        }

        private void BtnProveedorUltimo_Click(object sender, EventArgs e)
        {
            numeroPagina = pageCount;
            lblProveedorPaginas.Text = $"Página {numeroPagina.ToString()} de {pageCount.ToString()}";
            proveedor.BuscarProveedor(gridProveedores, "", pageCount, pageSize);
        }

        private void TxtProveedorTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            evento.NumberKeyPress(e);
        }

        private void PrintDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(groupProveedorRecibo.Width, groupProveedorRecibo.Height);
            groupProveedorRecibo.DrawToBitmap(bm, new Rectangle(0, 0, groupProveedorRecibo.Width, groupProveedorRecibo.Height));
            e.Graphics.DrawImage(bm, 50, 50);
        }

        private void BtnProvedorImprimirRecibo_Click(object sender, EventArgs e)
        {
            printDocument2.Print();
        }

        private void TxtProveedorEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtProveedorEmail.Text == "")
            {
                lblProveedorEmail.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lblProveedorEmail.Text = "Email";
                lblProveedorEmail.ForeColor = Color.Green;
            }
        }

        private void TxtProveedorPago_TextChanged(object sender, EventArgs e)
        {
            if (gridProveedores.CurrentRow == null)
            {
                lblProveedorPago.Text = "Seleccione el proveedor";
                lblProveedorPago.ForeColor = Color.Red;
                txtProveedorPago.Text = "";
            }
            else
            {
                if (txtProveedorPago.Text != "")
                {
                    String deuda1;
                    Decimal deuda2, deuda3, deudaTotal;
                    label_PagoCliente.Text = "Pagos de deuda";
                    textBox_PagoscCliente.ForeColor = Color.LightSlateGray;
                    deuda1 = Convert.ToString(gridProveedorReportes.CurrentRow.Cells[2].Value);
                    deuda1 = deuda1.Replace("$", "");
                    deuda2 = Convert.ToDecimal(deuda1);
                    deuda3 = Convert.ToDecimal(txtProveedorPago.Text);

                    deudaTotal = deuda2 - deuda3;
                    deudaActual = String.Format("{0:#,###,###,##0.00####}", deudaTotal);
                    pago = String.Format("{0:#,###,###,##0.00####}", txtProveedorPago.Text);
                }                
            }
        }

        private void TxtProveedorPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            evento.NumberDecimalKreyPress(txtProveedorPago, e);
        }

        private void AgregarProveedor()
        {
            if (txtProveedorNombre.Text == "")
            {
                lblProveedorNombre.Text = "Ingrese el nombre del proveedor";
                lblProveedorNombre.ForeColor = Color.Red;
                txtProveedorNombre.Focus();
            }
            else
            {
                if (txtProveedorTelefono.Text == "")
                {
                    lblProveedorTelefono.Text = "Ingrese el número de teléfono";
                    lblProveedorTelefono.ForeColor = Color.Red;
                    txtProveedorTelefono.Focus();
                }
                else
                {
                    if (txtProveedorEmail.Text == "")
                    {
                        lblProveedorEmail.Text = "Ingrese el email";
                        lblProveedorEmail.ForeColor = Color.Red;
                        txtProveedorEmail.Focus();
                    }
                    else
                    {
                        switch (accion)
                        {
                            case "insert":
                                if (evento.ComprobarFormatoEmail(txtProveedorEmail.Text))
                                {
                                    var data = proveedor.AgregarProveedor(txtProveedorNombre.Text, txtProveedorTelefono.Text,
                                        txtProveedorEmail.Text);
                                    if (0 == data.Count)
                                    {
                                        ReestablecerProveedor();
                                    }
                                    else
                                    {
                                        if (data[0].Telefono == txtProveedorTelefono.Text)
                                        {
                                            lblProveedorTelefono.Text = "El teléfono ya está registrado";
                                            lblProveedorTelefono.ForeColor = Color.Red;
                                            txtProveedorTelefono.Focus();
                                        }
                                        if (data[0].Email == txtProveedorEmail.Text)
                                        {
                                            lblProveedorEmail.Text = "El email ya está registrado";
                                            lblProveedorEmail.ForeColor = Color.Red;
                                            txtProveedorEmail.Focus();

                                        }
                                    }
                                }
                                else
                                {
                                    lblProveedorEmail.Text = "El email no es válido";
                                    lblProveedorEmail.ForeColor = Color.Red;
                                }
                                break;
                            case "update":
                                var data1 = proveedor.ActualizarProveedor(txtProveedorNombre.Text, txtProveedorTelefono.Text, 
                                    txtProveedorEmail.Text, idProveedor);
                                if (0 == data1.Count)
                                {
                                    ReestablecerProveedor();
                                }
                                else
                                {
                                    if (data1[0].IdProveedor != idProveedor)
                                    {
                                        lblProveedorTelefono.Text = "El teléfono ya está regisrado";
                                        lblProveedorTelefono.ForeColor = Color.Red;
                                        txtProveedorTelefono.Focus();
                                    }
                                    if (2 == data1.Count && data1[1].IdProveedor != idProveedor)
                                    {
                                        lblProveedorEmail.Text = "El email ya está regisrado";
                                        lblProveedorEmail.ForeColor = Color.Red;
                                        txtProveedorEmail.Focus();
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void ReestablecerProveedor()
        {
            vistaActual = 2;
            numeroPagina = 1;
            idRegistro = 0;
            accion = "insert";
            CargarDatos();
            txtProveedorNombre.Text = "";
            txtProveedorTelefono.Text = "";
            txtProveedorEmail.Text = "";
            txtProveedorPago.Text = "";
            txtProveedorNombre.Focus();
            lblProveedorNombre.ForeColor = Color.LightSlateGray;
            lblProveedorEmail.ForeColor = Color.LightSlateGray;
            lblProveedorTelefono.ForeColor = Color.LightSlateGray;
            idProveedor = 0;
            lblProveedorReciboNombre.Text = "Proveedor";
            lblProveedorReciboDeuda.Text = "$0.0";
            lblProveedorReciboPago.Text = "$0.0";
            lblProveedorReciboFecha.Text = "dd/mm/aaaa";
            radioProveedorAgregar.Checked = true;
            radioProveedorAgregar.ForeColor = Color.DarkCyan;
            lblProveedorPago.ForeColor = Color.DarkCyan;
            proveedor.ObtenerReporte(gridProveedorReportes, idProveedor);
        }

        private void GridProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridProveedores.Rows.Count != 0)
            {
                DataGridViewProveedor();
            }
        }

        private void GridProveedores_KeyUp(object sender, KeyEventArgs e)
        {
            if (gridProveedores.Rows.Count != 0)
            {
                DataGridViewProveedor();
            }
        }

        private void DataGridViewProveedor()
        {
            accion = "update";
            idProveedor = Convert.ToInt16(gridProveedores.CurrentRow.Cells[0].Value);
            txtProveedorNombre.Text = Convert.ToString(gridProveedores.CurrentRow.Cells[1].Value);
            txtProveedorEmail.Text = Convert.ToString(gridProveedores.CurrentRow.Cells[2].Value);
            txtProveedorTelefono.Text = Convert.ToString(gridProveedores.CurrentRow.Cells[3].Value);

            proveedor.ObtenerReporte(gridProveedorReportes, idProveedor);
            idRegistro = Convert.ToInt16(gridProveedorReportes.CurrentRow.Cells[0].Value);
            lblProveedorReciboNombre.Text = Convert.ToString(gridProveedorReportes.CurrentRow.Cells[1].Value);
            lblProveedorReciboFecha.Text = Convert.ToString(gridProveedorReportes.CurrentRow.Cells[3].Value);
            lblProveedorReciboPago.Text = Convert.ToString(gridProveedorReportes.CurrentRow.Cells[4].Value);
            lblProveedorReciboDeuda.Text = Convert.ToString(gridProveedorReportes.CurrentRow.Cells[2].Value);
        }

        private void GuardarPagoProveedor()
        {
            if (txtProveedorPago.Text == "")
            {
                lblProveedorPago.Text = "Ingrese el monto a pagar";
                lblProveedorPago.ForeColor = Color.Red;
                txtProveedorPago.Focus();
            }
            else
            {
                proveedor.ActualizarReporte(deudaActual, pago, idProveedor);
                ReestablecerProveedor();
            }
        }

        #endregion

    }
}
