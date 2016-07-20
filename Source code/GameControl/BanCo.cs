using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GameControl.Properties;

namespace GameControl
{
    public class BanCo
    {
        private int _row;
        private int _column;

        public int Row
        {
            get { return _row; }
        }

        public int Column
        {
            get { return _column; }
        }

        private Pen Pen { get; set; }

        public Graphics Graphics { get; set; }

        public BanCo(Graphics g, int row = 0, int column = 0)
        {
            Graphics = g;
            _row = row;
            _column = column;

            Pen = new Pen(Color.FromArgb(234, 234, 234), 2);
        }

        public void Draw()
        {
            for (int i = 0; i <= Column; i++)
                Graphics.DrawLine(Pen, i * OCo.Width, 0, i * OCo.Width, Row * OCo.Height);

            for (int i = 0; i <= Row; i++)
                Graphics.DrawLine(Pen, 0, i * OCo.Height, Column * OCo.Width, i * OCo.Height);
        }

        public void DrawQuanCo(Point point, QuanCo quanCo)
        {
            switch (quanCo)
            {
                case QuanCo.O:
                    Graphics.DrawImage(Resources.O, point.X, point.Y, OCo.Width, OCo.Height);
                    break;
                case QuanCo.X:
                    Graphics.DrawImage(Resources.X, point.X, point.Y, OCo.Width, OCo.Height);
                    break;
                default:
                    break;
            }
        }

        public void XoaQuanCo(Point point)
        {
            Graphics.FillRectangle(new SolidBrush(Color.FromArgb(243, 243, 243)), point.X+1, point.Y+1, OCo.Width-2, OCo.Height-2);
        }
    }
}
