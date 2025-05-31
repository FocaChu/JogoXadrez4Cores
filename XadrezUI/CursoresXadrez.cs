using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace XadrezUI
{
    public static class CursoresXadrez
    {
        public static readonly Cursor CursorBranco = CarregarCursor("Assets/CursorBranco.cur");
        public static readonly Cursor CursorPreto = CarregarCursor("Assets/CursorPreto.cur");

        private static Cursor CarregarCursor(string caminhoCursor)
        {
            Stream stream = Application.GetResourceStream(new Uri(caminhoCursor, UriKind.Relative)).Stream;
            return new Cursor(stream, true);
        }
    }
}
