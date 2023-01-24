using System;
using System.IO;
using UglyToad.PdfPig;
using UglyToad;
using UglyToad.PdfPig.Content;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PdfToMindcraftBook
{
    class App
    {
        const int MAX = 99;
        static void Main(string[] strings)
        {
            Console.WriteLine("Welcome! Let's get started.");
            PDF doc = new PDF("D:\\OneDrive\\Documents\\C#Projects\\mindcraft-pdf-to-book\\test-examples\\7_ThereWillComeSoftRainsbyRayBradbury.pdf");
            doc.LoadPDF();
            var book = doc.GenerateBook();
            WriteTextFile(book); 
            
            Console.WriteLine("Done!");
        }
        
        public static void Run()
        {
            bool run = true;
            int count = 0;

            while (run)
            {
                if(count == MAX)
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

    /** Book format for minecraft.*/
    class MineCraftBookFormat
    {
        public static readonly string Title = "title: ";
        public static readonly string NewPage = "pages:";
        public static readonly string Author = "author: ";
        public static readonly string StartPage = "\n#- ";
        public static readonly int MaxPageCount = 100;
        public static readonly int MaxCharCount = 256;
    }

    class PDF
    {
        
        private string path;
        /*private string text;*/
        private PdfDocument? pdfDocument;
        private string? title; 
        private string? author;
        private bool loaded = false;


        public string? Title
        {
            get { return title; }
        }

        public string? Author
        {
            get { return author; }
        }

        public PDF(string path)
        {
            this.path = path;
        }

        public bool LoadPDF()
        {

            try
            {
                pdfDocument = PdfDocument.Open(path);
                this.title = pdfDocument.Information.Title !=null? pdfDocument.Information.Title: "Unknown";
                this.author = pdfDocument.Information.Author != null ? pdfDocument.Information.Author : "Unknown";
                this.loaded = true;
                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine("Sorry, Dude, we've encountered an error. Either you messed up, or I suck as a programmer! HA! Try again and let me know what happend.\nError:");
                Console.WriteLine(error.Message);
                return false;
            }
        }

        public LinkedList<string>? GenerateBook()
        {
            if (!this.loaded)
            {
                return null;
            }
            //need to create first page 

            
            LinkedList<string> pages = this.ParceText(GetPdfDocument());
            /** Add the beginning of the page in the following format: 
             * title: title
             * author: author name 
             * pages: \n
             **/
            pages.AddFirst(MineCraftBookFormat.NewPage);
            //first check (if !loaded) gaurintees no null value. 
            //when loading, we set a value. 
            #pragma warning disable CS8604 // Dereference of a possibly null reference.
            pages.AddFirst(Author);
            pages.AddFirst(Title);
            #pragma warning restore CS8604 // Dereference of a possibly null reference.

            return pages; 
        }

        private PdfDocument? GetPdfDocument()
        {
            return pdfDocument;
        }

        private LinkedList<string> ParceText(PdfDocument? pdfDocument)
        {
            
            var result = new LinkedList<string>();
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            var pages = pdfDocument.GetPages();
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (pages.Count() > MineCraftBookFormat.MaxPageCount)
            {
                throw new Exception("Oops... This book is tooooooooo long. We're only doin' books 100 pages or less right now. Sorry, not sorry! Hahahahahaha! ");
            }
            
            //int pageCount = 0;
            foreach (Page page in pages)
            {
                Console.WriteLine(page.Text + "\n");

                StringBuilder stringBuilder = new StringBuilder();
                //pageCount++;
                //int charCount = 0;
                string text = page.Text;
                if (text.Length < 255)
                {
                    stringBuilder.Append(MineCraftBookFormat.StartPage);
                    stringBuilder.Append(text);
                    result.AddLast(stringBuilder.ToString());
                }
                else
                {

                    //for(int i = 0, end = 0; i < text.Length - MineCraftBookFormat.MaxCharCount; )
                    int i = 0, end = 0, start = 0; 
                    while (i < text.Length) 
                    {
                        //int end = ((i + 255) > (text.Length - 1)) ? 
                        //   text.Length  - i: 
                        //  255; 


                        if((i + MineCraftBookFormat.MaxCharCount) > (text.Length - 1)) 
                        { 
                            end = text.Length - i;
                            start = i;
                            i = text.Length; 
                        }
                        else
                        {
                            end = MineCraftBookFormat.MaxCharCount;
                            start = i;
                            i += MineCraftBookFormat.MaxCharCount;
                        }

                        string subString = MineCraftBookFormat.StartPage + text.Substring(start, end);

                        //stringBuilder.Append(MineCraftBookFormat.StartPage);
                        stringBuilder.Append(subString);
                    }

                        string output = stringBuilder.ToString();
                        Debug.WriteLine("Current state.", output);

                        result.AddLast(output);

                }


            }

            return result;
        }   

        private StringBuilder CreateFirstPage()
        {
            string[] sections = {MineCraftBookFormat.StartPage, MineCraftBookFormat.Title, MineCraftBookFormat.Author, MineCraftBookFormat.NewPage };
            StringBuilder result = new StringBuilder(); 
            foreach (string section in sections)
            {
                result.AppendLine(section);
            }
            //Debug.WriteLine("DEBUG - Create First page:\n", result.ToString());
            return result; 
        }
    }


    
}