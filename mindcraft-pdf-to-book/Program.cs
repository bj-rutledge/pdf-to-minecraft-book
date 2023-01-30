using System;
using System.IO;

namespace PdfToMineCraftBook
{
    class App
    {
        const int MAX = 99;
        static void Main(string[] strings)
        {
            UX ux = new UX();
            ux.Init();
        }


/*        private static void Test()
        {
            var ll = new LinkedList<string>();
            ll.AddFirst("1/25/13Ray Bradbury: There Will Come Soft Rainswww.dennissylvesterhurd.com/blog/softrain.htm");
            var doc = new PDFConversion(@"D:\OneDrive\Documents\C#Projects\mindcraft-pdf-to-book\test-examples\pdf.pdf");
            doc.LoadPDF();
            File.WriteAllLinesAsync(@"D:\OneDrive\Documents\C#Projects\mindcraft-pdf-to-book\test-examples\test.txt", doc.GenerateBook(ll));

        }*/
    }
}