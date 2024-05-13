using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        // Use this Random object to choose random icons for the squares
        Random random = new Random();

        // Each of these letters is an interesting icon
        // in the Webdings font,
        // and each icon appears twice in this list
        List<string> icons = new List<string>()
        {
            "!","!","N","N",",",",","k","k",
            "b","b","v","v","w","w","z","z"
        };

        /// <summary>
        /// Assign each icon from the list of icons to a random square
        /// </summary>
        private void AssignIconsTosquares()
        {
            // The TableLayoutPanel has 16 labels,
            // and the icon list has 16 icons,
            // so an icon is pulled at random from the list
            // and added to each label
            foreach (Control control in tableLayoutPanel1.Controls) // I assume a control is a block in the tableLAyoutPanel
            {
                Label iconLabel = control as Label; // Converts control variable to a label named iconLabel
                if (iconLabel != null) // Checks if conversion worked
                {
                    int randomNumber = random.Next(icons.Count); // Creates a random number within the range of the icon list
                    iconLabel.Text = icons[randomNumber]; // Assings one of the icons to the text property of the label
                     iconLabel.ForeColor = iconLabel.BackColor; // Used to hide the icon
                    icons.RemoveAt(randomNumber); // Removes the icon that has been addedd to the form from the list
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            AssignIconsTosquares();
        }

        /// <summary>
        /// Every label's Click event is handled by this event handler
        /// </summary>
        /// <param name="sender">The label that was clicked</param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // If the clicked label is black, the player clicked
                // an icon that's already been revealerd --
                // ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                {
                    return;
                }

                clickedLabel.ForeColor = Color.Black;
            }
        }
    }
}
