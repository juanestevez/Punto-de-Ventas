using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;

namespace Punto_de_ventas.ModelClass
{
    public class TextBoxEvent
    {

        public void TextKeyPress(KeyPressEventArgs e)
        {
            // Condición que sólo permite ingresar caracteres de texto.
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar)) // Permite usar backspace
            {
                e.Handled = false;
            }
            else if (char.IsSeparator(e.KeyChar)) // Permite insertar estacios
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public void NumberKeyPress(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar)) // Sólo permite ingresar números
            {
                e.Handled = false;
            }

            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void NumberDecimalKreyPress(TextBox textBox, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if ((e.KeyChar == '.') && (!textBox.Text.Contains('.')))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public bool ComprobarFormatoEmail(string email)
        {
            if (new EmailAddressAttribute().IsValid(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    
    }
}
