using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameControl;

namespace Caro.Scenes
{
    public partial class PlayScene : Form
    {
        private CaroChess caroChess;

        public PlayScene()
        {
            InitializeComponent();
            caroChess = new CaroChess(pnlGame.CreateGraphics());
        }

        private void PlayScene_Load(object sender, EventArgs e)
        {
            
        }

        private void pnlGame_Paint(object sender, PaintEventArgs e)
        {
            caroChess.DrawBoard();
            caroChess.VeLaiQuanCo();
        }

        private void pnlGame_MouseClick(object sender, MouseEventArgs e)
        {
            caroChess.DanhCo(e.X, e.Y);

            if (caroChess.KiemTraChienThang())
                caroChess.KetThucTroChoi();
        }

        private void PvsP()
        {
           // pnlGame.CreateGraphics().Clear(pnlGame.BackColor);
            caroChess.Choi2Nguoi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            caroChess.Undo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            caroChess.Redo();
        }
    }
}
