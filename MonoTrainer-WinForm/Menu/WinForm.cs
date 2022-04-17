
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Color = System.Drawing.Color;
using UnityEngine;

namespace Trainer.Menu
{
    public class WinFormGUI
    {
        #region[Declarations]

        #region[WinForm]

        private static Form form = null;
        public Form FormBase = null;


        #endregion

        public System.Windows.Forms.Button AlwaysWinButton;




        #endregion

        #region[Primary]

        public void CreateForm()
        {
            form = new Form();
            FormBase = form;

            form.BackColor = Color.FromArgb(37, 37, 38);
            form.Text = "wh0am15533 Trainer";

            // Create your UI in the the Designer form and in copy the generated code here for your controls.

            AlwaysWinButton = new System.Windows.Forms.Button();
            AlwaysWinButton.Name = "AlwaysWinButton";
            AlwaysWinButton.Text = "Easy $$/Always Win Table";
            AlwaysWinButton.Location = new Point(10, 10);
            AlwaysWinButton.Width = 120;
            AlwaysWinButton.BackColor = Color.CornflowerBlue;
            AlwaysWinButton.FlatStyle = FlatStyle.Flat;
            AlwaysWinButton.Click += AlwaysWinButton_Click;

            form.Controls.Add(AlwaysWinButton);
        }

        public void ShowDialog()
        {
            form.ShowDialog();
        }

        public void Close()
        {
            form.Close();
        }


        #endregion

        private void AlwaysWinButton_Click(object sender, EventArgs e)
        {
            // Do Something....
        }





    }
}
