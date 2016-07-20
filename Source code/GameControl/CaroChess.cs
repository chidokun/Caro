using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameControl
{
    public enum Winner { Draw, Player1, Player2, Computer }
    public class CaroChess
    {
        private BanCo _banCo;
        private OCo[,] _mangOCo;
        private QuanCo luotDi = QuanCo.X;
        private Stack<OCo> cacNuocDaDi = new Stack<OCo>();
        private Stack<OCo> cacNuocUndo = new Stack<OCo>();
        private Winner winner;
        public Graphics Graphics { get; set; }

        public CaroChess(Graphics g)
        {
            Graphics = g;
            _banCo = new BanCo(this.Graphics, 20, 27);
            KhoiTaoMangOCo();
        }

        public void DrawBoard()
        {
            _banCo.Draw();
        }

        public void KhoiTaoMangOCo()
        {
            _mangOCo = new OCo[_banCo.Row, _banCo.Column];

            for (int i = 0; i < _banCo.Row; i++)
                for (int j = 0; j < _banCo.Column; j++)
                    _mangOCo[i, j] = new OCo()
                    {
                        Row = i,
                        Column = j,
                        Position = new Point(j * OCo.Width, i * OCo.Height)
                    };
        }

        public void DanhCo(int mouseX, int mouseY)
        {
            if (mouseX % OCo.Width == 0 || mouseY % OCo.Height == 0)
                return;
            int column = mouseX / OCo.Width;
            int row = mouseY / OCo.Height;

            if (_mangOCo[row, column].QuanCo != QuanCo.Blank)
                return;

            _mangOCo[row, column].QuanCo = luotDi;
            _banCo.DrawQuanCo(_mangOCo[row, column].Position, _mangOCo[row, column].QuanCo);

            cacNuocDaDi.Push(new OCo()
            {
                Column = _mangOCo[row, column].Column,
                Row = _mangOCo[row, column].Row,
                Position = _mangOCo[row, column].Position,
                QuanCo = _mangOCo[row, column].QuanCo
            });

            cacNuocUndo.Clear();

            if (luotDi == QuanCo.X)
                luotDi = QuanCo.O;
            else
                luotDi = QuanCo.X;
        }

        public void VeLaiQuanCo()
        {
            foreach (var i in cacNuocDaDi)
                _banCo.DrawQuanCo(i.Position, i.QuanCo);
        }

        public void Choi2Nguoi()
        {
            KhoiTaoMangOCo();
            cacNuocDaDi.Clear();
            DrawBoard();
        }

        public void Undo()
        {
            if (cacNuocDaDi.Count != 0)
            {
                OCo oco = cacNuocDaDi.Pop();
                cacNuocUndo.Push(oco);
                _mangOCo[oco.Row, oco.Column].QuanCo = QuanCo.Blank;
                _banCo.XoaQuanCo(_mangOCo[oco.Row, oco.Column].Position);

                luotDi = luotDi == QuanCo.X ? QuanCo.O : QuanCo.X;
            }
        }

        public void Redo()
        {
            if (cacNuocUndo.Count != 0)
            {
                OCo oco = cacNuocUndo.Pop();
                cacNuocDaDi.Push(oco);
                _mangOCo[oco.Row, oco.Column].QuanCo = oco.QuanCo;
                _banCo.DrawQuanCo(_mangOCo[oco.Row, oco.Column].Position, _mangOCo[oco.Row, oco.Column].QuanCo);

                luotDi = luotDi == QuanCo.X ? QuanCo.O : QuanCo.X;
            }
        }

        #region CHiến thắng
        public bool KiemTraChienThang()
        {
            if (cacNuocDaDi.Count == _banCo.Row * _banCo.Column)
            {
                winner = Winner.Draw;
                return true;
            }

            foreach (var oco in cacNuocDaDi)
            {
                if (DuyetDoc(oco) || DuyetNgang(oco)||DuyetCheoXuoi(oco)||DuyetCheoLen(oco))
                {
                    switch (oco.QuanCo)
                    {
                        case QuanCo.X:
                            winner = Winner.Player1;
                            break;
                        case QuanCo.O:
                            winner = Winner.Player2;
                            break;
                    }
                    return true;
                }
            }



            return false;
        }

        public void KetThucTroChoi()
        {
            switch (winner)
            {
                case Winner.Draw:
                    MessageBox.Show("Hòa");
                    break;
                case Winner.Player1:
                    MessageBox.Show("player1");
                    break;
                case Winner.Player2:
                    MessageBox.Show("player2");
                    break;
                case Winner.Computer:
                    MessageBox.Show("computer");
                    break;
            }
        }

        public bool DuyetDoc(OCo currOco)
        {
            if (currOco.Row > _banCo.Row - 5)
                return false;

            int dem;
            for (dem = 1; dem < 5; dem++)
                if (_mangOCo[currOco.Row + dem, currOco.Column].QuanCo != currOco.QuanCo)
                    return false;

            if (currOco.Row == 0 || currOco.Row + dem == _banCo.Row)
                return true;

            if (_mangOCo[currOco.Row - 1, currOco.Column].QuanCo == QuanCo.Blank || _mangOCo[currOco.Row + dem, currOco.Column].QuanCo == QuanCo.Blank)
                return true;

            return false;

            //int count;
            //int begin = currOco.Row - 4 >= 0 ? currOco.Row - 4 : 0;
            //int final = currOco.Row + 4 < _banCo.Row ? currOco.Row + 4 : _banCo.Row - 1;

            //for (int i = begin; i <= final; i += count)
            //{
            //    for (count = 1; count < 5; count++)
            //        if (_mangOCo[i + count, currOco.Column].QuanCo != currOco.QuanCo)
            //            break;
            //    if (count == 5)
            //        return true;
            //}


        }

        public bool DuyetNgang(OCo currOco)
        {
            if (currOco.Column > _banCo.Column - 5)
                return false;

            int dem;
            for (dem = 1; dem < 5; dem++)
                if (_mangOCo[currOco.Row, currOco.Column + dem].QuanCo != currOco.QuanCo)
                    return false;

            if (currOco.Row == 0 || currOco.Row + dem == _banCo.Row || currOco.Column == 0 || currOco.Column + dem == _banCo.Column)
                return true;

            if (_mangOCo[currOco.Row - 1, currOco.Column - 1].QuanCo == QuanCo.Blank || _mangOCo[currOco.Row + dem, currOco.Column + dem].QuanCo == QuanCo.Blank)
                return true;

            return false;

            //int count;
            //int begin = currOco.Column - 4 >= 0 ? currOco.Column - 4 : 0;
            //int final = currOco.Column + 4 < _banCo.Column ? currOco.Column + 4 : _banCo.Column - 1;

            //for (int i = begin; i <= final; i += count)
            //{
            //    for (count = 1; count < 5; count++)
            //        if (_mangOCo[currOco.Row, i + count].QuanCo != currOco.QuanCo)
            //            break;
            //    if (count == 5)
            //        return true;
            //}

            //return false;
        }

        public bool DuyetCheoXuoi(OCo currOco)
        {
            if (currOco.Row > _banCo.Row - 5 || currOco.Column > _banCo.Column - 5)
                return false;

            int dem;
            for (dem = 1; dem < 5; dem++)
                if (_mangOCo[currOco.Row + dem, currOco.Column + dem].QuanCo != currOco.QuanCo)
                    return false;

            if (currOco.Column == 0 || currOco.Column + dem == _banCo.Column)
                return true;

            if (_mangOCo[currOco.Row, currOco.Column - 1].QuanCo == QuanCo.Blank || _mangOCo[currOco.Row, currOco.Column + dem].QuanCo == QuanCo.Blank)
                return true;

            return false;
            //int count;
            //int beginX = currOco.Column - 4 >= 0 ? currOco.Column - 4 : 0;
            //int beginY = currOco.Row - 4 >= 0 ? currOco.Row - 4 : 0;
            //int finalX = currOco.Column + 4 < _banCo.Column ? currOco.Column + 4 : _banCo.Column - 1;
            //int finalY = currOco.Row + 4 < _banCo.Row ? currOco.Row + 4 : _banCo.Row - 1;

            //for (int i = beginX, j = beginY; i <= finalX; i += count, j+=count)
            //{
            //    for (count = 1; count < 5; count++)
            //        if (_mangOCo[i+count, j+ count].QuanCo != currOco.QuanCo)
            //            break;
            //    if (count == 5)
            //        return true;
            //}

            //return false;
        }

        public bool DuyetCheoLen(OCo currOco)
        {
            if (currOco.Row < 4 || currOco.Column > _banCo.Column - 5)
                return false;

            int dem;
            for (dem = 1; dem < 5; dem++)
                if (_mangOCo[currOco.Row-dem, currOco.Column + dem].QuanCo != currOco.QuanCo)
                    return false;

            if (currOco.Row == 4 || currOco.Row  == _banCo.Row -1 || currOco.Column == 0 || currOco.Column + dem == _banCo.Column)
                return true;

            if (_mangOCo[currOco.Row + 1, currOco.Column - 1].QuanCo == QuanCo.Blank || _mangOCo[currOco.Row - dem, currOco.Column + dem].QuanCo == QuanCo.Blank)
                return true;

            return false;

            //int count;
            //int beginX = currOco.Column - 4 >= 0 ? currOco.Column - 4 : 0;
            //int beginY = currOco.Row + 4 < _banCo.Row ? currOco.Row + 4 : _banCo.Row - 1;
            //int finalX = currOco.Column + 4 < _banCo.Column ? currOco.Column + 4 : _banCo.Column - 1;
            //int finalY = currOco.Row - 4 >= 0 ? currOco.Row - 4 : 0;

            //for (int i = beginX, j = beginY; i <= finalX; i += count, j -= count)
            //{
            //    for (count = 1; count < 5; count++)
            //        if (_mangOCo[j - count,i + count ].QuanCo != currOco.QuanCo)
            //            break;
            //    if (count == 5)
            //        return true;
            //}

            //return false;
        }

        public void PvsC()
        { }

        #endregion
    }
}
