using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Band_Organizer
{
    class Validation
    {
        public bool IsInList(TextBox textBox, ListBox listBox)
        {
            // returns true if textbox text is in listbox

            foreach (string item in listBox.Items)
            {
                if (item.ToUpper() == textBox.Text.ToUpper())
                {
                    MessageBox.Show("This already exists in the database.", "Entry Error");
                    textBox.Focus();
                    return false;
                }
            }
            return true;
        }

        public bool IsInt(TextBox textBox)
        {
            // returns true if parameter is integer

            int x;
            if (int.TryParse(textBox.Text, out x))
                return true;
            else
            {
                MessageBox.Show("Must be a numerical value.", "Value Error");
                return false;
            }
        }

        public bool IsPresent(TextBox textBox, string name)
        {
            // checks if a textbox is empty, notifies user if false

            if (textBox.Text == "")
            {
                MessageBox.Show(name + " is a required field.", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        public bool IsSelected(ListBox listBox, string name)
        {
            // checks to make sure a band or album is selected before entering more input
            if (listBox.SelectedIndex == -1)
            {
                MessageBox.Show("You must make a selection from " + name, "Entry Error");
                return false;
            }
            else
                return true;
        }
    }
}
