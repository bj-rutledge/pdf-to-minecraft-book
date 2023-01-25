using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;

namespace PdfToMindcraftBook
{
    internal class MineCraftBookFormat
    {
        public static readonly string Title = "title: ";
        public static readonly string NewPage = "pages:";
        public static readonly string Author = "author: ";
        public static readonly string StartPage = "\n#- ";
        public static readonly int MaxPageCount = 100;
        public static readonly int MaxCharCount = 256;
    }


}
