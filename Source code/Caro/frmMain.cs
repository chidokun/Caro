using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Caro.Scenes;

namespace Caro
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (GlobalScene.MenuScene == null)
                GlobalScene.MenuScene = new MenuScene()
                {
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                    TopLevel = false,
                    Parent = this.pnlScene
                };

            pnlScene.Controls.Clear();
            pnlScene.Controls.Add(GlobalScene.MenuScene);
            GlobalScene.MenuScene.Show();
        }
    }
}
