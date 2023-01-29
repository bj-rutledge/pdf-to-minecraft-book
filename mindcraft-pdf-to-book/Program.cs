using System;

namespace PdfToMineCraftBook
{
    class App
    {
        const int MAX = 99;
        static void Main(string[] strings)
        {
            Console.WriteLine("Welcome! Let's get started.");
            TestPDF();
        }

        public static void TestPDF()
        {
            UX ux = new UX();
            ux.Init();

            /*PDFConversion doc= new PDFConversion(Test.Path, Test.Remove);
            doc.LoadPDF();
            doc.ViewPDFText();
            var book = doc.GenerateBook();

            if (book != null)
            {
                _ = WriteTextFile(book);
            }
            else 
            {
                Console.WriteLine("We didn't make a book!");
            }

            Console.WriteLine("Done!");
*/
        }

        


    }

/*    internal class Test
    {
        public static string Path = @"D:\OneDrive\Documents\C#Projects\MineCraft-pdf-to-book\test-examples\7_ThereWillComeSoftRainsbyRayBradbury.pdf";
        public static string[] Remove = { "1/25/13Ray Bradbury: There Will Come Soft Rainswww.dennissylvesterhurd.com/blog/softrain.htm" };
    }*/
}

