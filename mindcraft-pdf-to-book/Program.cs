using System;

namespace PdfToMindcraftBook
{
    class App
    {
        const int MAX = 99;
        static void Main(string[] strings)
        {
            Console.WriteLine("Welcome! Let's get started.");
            PDFConversion doc = new PDFConversion("D:\\OneDrive\\Documents\\C#Projects\\mindcraft-pdf-to-book\\test-examples\\7_ThereWillComeSoftRainsbyRayBradbury.pdf");
            doc.LoadPDF();
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
        }

        public static void Run()
        {
            bool run = true;
            int count = 0;

            while (run)
            {
                if (count == MAX)
                {
                    Console.WriteLine("We've done a hundred of these... I'm getting tired. Why don't we take a break???");
                    run = false;
                }

            }
        }

        private static async Task WriteTextFile(LinkedList<string> text)
        {
            await File.WriteAllLinesAsync(@"D:\OneDrive\Documents\C#Projects\mindcraft-pdf-to-book\test-examples\test-out.txt", text);
        }

    }
}

