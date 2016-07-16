using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Caro;

namespace Caro.Scenes
{
    public partial class MenuScene : Form
    {
        public MenuScene()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GlobalScene.PlayScene == null)
                GlobalScene.PlayScene = new PlayScene()
                {
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                    TopLevel = false,
                    Parent = this.Parent
                };

            var parent = Parent;
            parent.Controls.Clear();
            parent.Controls.Add(GlobalScene.PlayScene);
            GlobalScene.PlayScene.Show();
        }
    }
}
