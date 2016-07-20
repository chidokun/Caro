using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GameControl
{
    public enum QuanCo { Blank, X, O }

    public class OCo
    {
        private const int _height = 30;
        private const int _width = 30;


        public static int Height
        {
            get { return _height; }
        }

        public static int Width
        {
            get { return _width; }
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public Point Position { get; set; }

        public QuanCo QuanCo { get; set; } = QuanCo.Blank;

    }
}
