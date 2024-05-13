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
        // firstclicked points to the first Label control
        // that the player clicks, but it will be null
        // if the player hasn't clicked a label yet
        Label firstClicked = null;

        // secondClicked points to the second Label control
        // that the player clicks
        Label secondClicked = null;

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
            // the timer is only on after two non-matching
            // icons have been shown to the player,
            // so ignore any clicks if the timer is running
            if (timer1.Enabled == true)
            {
                return;
            }

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // If the clicked label is black, the player clicked
                // an icon that's already been revealed --
                // ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                {
                    return;
                }

                // If firstClicked is null, this is the first icon 
                // in the pair that the player clicked, 
                // so set firstclicked to the label that the player
                // clicked, change its color to black and return
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // If the player gets this far, the timer isn't
                // running and firstclicked ins't null,
                // so this must be the second icon the player clicked
                // Set its color to black
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Check to see if the player won
                CheckForwinner();

                // If the player clicked two matching icons, keep them
                // black and reset firstClicked and secondClicked
                // so the player can click another icon
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // If the player gets this far, the player
                // clicked two different icons, so start the
                // timer (which will wait three quarters of
                // a second, and then hode the icons)
                timer1.Start();
                
            }
        }
        /// <summary>
        /// This timer is started when the plater clicks
        /// two icons that don't match,
        /// so it counts three quarters of a second
        /// and then turn itself off and hides both icons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Stop the timer
            timer1.Stop();

            //Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Reset firstClicked and secondClicked
            // so the next time a label is
            // clicked, the program knows it's the first click
            firstClicked = null;
            secondClicked = null;
        }
        /// <summary>
        /// Check every icon to see if it is matched, by
        /// comparing its foreground color to its background color.
        /// If all of the icons are matched, the player wins
        /// </summary>
        private void CheckForwinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                    {
                        return;
                    }
                }
            }

            // If the loop didn't return, it didn't find
            // any unmatched icons
            // that means the user won. Show a message and close the form
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }
    }
}
