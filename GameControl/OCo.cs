using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GameControl
{
    public class OCo
    {
        private const int _height = 30;
        private const int _width = 30;
        private Point _position;

        public int Height
        {
            get { return _height; }
        }

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public int Width
        {
            get { return _width; }
        }
    }
}
