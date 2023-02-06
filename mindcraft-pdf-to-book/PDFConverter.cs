using System.Text;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;

namespace PdfToMineCraftBook
{
    internal class PDFConversion 
    {

        private string path;
        /*private string text;*/
        private PdfDocument? pdfDocument;
        private string? title;
        private string? author;
        private bool loaded = false;
        private LinkedList<string>? removeStrings;
        private IEnumerable<Page>? pages;
        /// <summary>
        /// Book title. 
        /// </summary>
        public string? Title
        {
            get { return title; }
        }
        /// <summary>
        /// Book author. 
        /// </summary>
        public string? Author
        {
            get { return author; }
        }

        /// <summary>
        /// Constructor. 
        /// Pass path to pdf file and array of strings to delete from pdf.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="stringsToRemove"></param>
        public PDFConversion(string path, LinkedList<string> stringsToRemove)
        {
            this.path = path;
            this.removeStrings = stringsToRemove;
        }

        public PDFConversion(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Load PDF document, sets properties: 
        /// pages, Title, Author, and sets documentLoaded to true.
        /// Throws exception if document cannot be loaded. 
        /// This method must be run before calling any subsequent 
        /// method. Available after calling will be
        /// </summary>
        /// <returns>true on success</returns>
        public bool LoadPDF()
        {

            try
            {
                pdfDocument = PdfDocument.Open(path);
                this.pages = pdfDocument.GetPages();
                this.title = pdfDocument.Information.Title != null ? pdfDocument.Information.Title : "Unknown";
                this.author = pdfDocument.Information.Author != null ? pdfDocument.Information.Author : "Unknown";
                if(this.pages == null || this.pages.Count() == 0)
                {
                    throw new Exception("No pages detected in PDF");
                }
                this.loaded = true;
            }
            catch (Exception error)
            {
                Console.WriteLine("Sorry, Dude, we've encountered an error. Either you messed up, or I suck as a programmer! HA! Try again and let me know what happend.\nError:");
                Console.WriteLine(error.Message);
            }
            return this.loaded;
        }

        /// <summary>
        /// View text of PDF
        /// </summary>
        public void ViewPDFText()
        {
            if (!this.loaded || this.pages == null)
            {
                Console.WriteLine("PDF File not loaded. You need to load a pdf, Dude! If you've already done that, something went wrong... Oops. One of us messed up! HAHAHAHA! That's what you get with freeeeeeware. \nHere chicky chicky, bork bork!");
                return;
            }

            Console.WriteLine("Title: {0}", Author);
            Console.WriteLine("Author: {0}\n", Author);

            foreach(Page page in this.pages)
            {
                Console.WriteLine(page.Text + '\n');   
            }
        }

        /// <summary>
        /// Verifies that pdf is loaded, then calls 
        /// ParseText
        /// </summary>
        /// <returns></returns>
        public LinkedList<string>? GenerateBook()
        {
            if (!loaded)
            {
                return null;
            }
            //need to create first page 


            LinkedList<string> pagesList = ParseConvertAndRemoveUnwantedText(pdfDocument);
            return pagesList;
        }

        /// <summary>
        /// Generates book with string removal. 
        /// </summary>
        /// <param name="stringsToRemove"></param>
        /// <returns></returns>
        public LinkedList<string>? GenerateBook(LinkedList<string> stringsToRemove)
        {
            if (!loaded)
            {
                return null;
            }
            //need to create first page 

            this.removeStrings = stringsToRemove;
            LinkedList<string> pagesList = ParseConvertAndRemoveUnwantedText(pdfDocument);
            return pagesList;
        }


        /// <summary>
        /// Removes user provided strings of text from the text of the document. 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        private string RemoveText(string text, LinkedList<string> strings)
        {
            
            foreach (string s in strings)
            {
                int maxIterations = 10000;
                int i = 0;
                while(i < maxIterations)
                {

                    int textIndex = text.IndexOf(s);

                    if(textIndex == -1)
                    {
                        break;
                    }
                    StringBuilder sb = new StringBuilder();

                    sb.Append(text.Substring(0, textIndex));
                    sb.Append(text.Substring(textIndex + s.Length));
                    text = sb.ToString();
                    i++;
                }
            }

            return text;
        }

        /// <summary>
        /// Determine the number of letters we need to subtract to get to a space. 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="index"></param>
        /// <returns>Number of letters to space going backwards. 0 if current index is space or no space found.</returns>
        private int NumLettersToSpace(string text, int index)
        {
            char space = ' ';
            int result = 0;
            
            for(int i =  index, numLetters = 0; i > 0; i--, numLetters++)
            {
                if (text[i] == space)
                {
                    result = numLetters;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Parse the text retrieved from the PDF file, remove any unwanted text, and convert remaining text to *.stenhal 
        /// format. 
        /// </summary>
        /// <param name="pdfDocument"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private LinkedList<string> ParseConvertAndRemoveUnwantedText(PdfDocument? pdfDocument)
        {

            var result = InitBookText();
            // Dereference of a possibly null reference.
            // pages were validated at instintiation. 
            #pragma warning disable CS8604
            if (this.pages.Count() > MineCraftBookFormat.MaxPageCount)
            {
                throw new Exception("Oops... This book is tooooooooo long. We're only doin' books 100 pages or less right now. Sorry, not sorry! Hahahahahaha! ");
            }
            // Dereference of a possibly null reference. 
            // pages were validated at instintiation. 
            #pragma warning restore CS8604

            foreach (Page page in pages)
            {
                /*Console.WriteLine(page.Text + "\n");*/

                StringBuilder stringBuilder = new StringBuilder();

                /* Remove any user provided strings if we have any. Otherwise, procede with 
                 * the text as is*/
                string text = (removeStrings != null && removeStrings != null) ? 
                    RemoveText(page.Text, removeStrings): 
                    page.Text;

                if (text.Length < MineCraftBookFormat.MaxCharCount)
                {
                    stringBuilder.Append(MineCraftBookFormat.StartPage);
                    stringBuilder.Append(text);
                    result.AddLast(stringBuilder.ToString());
                }
                else
                {

                    int i = 0, end = 0, start = 0;
                    while (i < text.Length)
                    {

                        /*Sometimes when we remove a space, there's a leading space. 
                            * Get rid of leading space if it's present.*/
                        if (text[i] == ' ' && (i + 1) < text.Length)
                        {
                            i++;
                        }

                        if ((i + MineCraftBookFormat.MaxCharCount) > (text.Length - 1))
                        {
                            end = text.Length - i;
                            start = i;
                            i = text.Length;
                        }
                        else
                        {
                            int numLettersToSpace = this.NumLettersToSpace(text, i + MineCraftBookFormat.MaxCharCount);
                            
                            end = MineCraftBookFormat.MaxCharCount - numLettersToSpace;
                            start = i;
                            i += end;
                        }

                        string subString = MineCraftBookFormat.StartPage + text.Substring(start, end);
                        stringBuilder.Append(subString);
                    }

                    //Get rid of unwanted new line at the end of stringbuilder. 
                    string output = stringBuilder.ToString().Trim();
                    result.AddLast(output);
                }

            }

            return result;
        }

        /// <summary>
        /// Initializes the linked list that will hold the formated 
        /// text of the book in text/*.stenhal format. 
        /// Inserts the Title, Author, and Pages tags and subsequent values. 
        /// </summary>
        /// <returns></returns>
        private LinkedList<string> InitBookText()
        {
            LinkedList<string> result = new LinkedList<string>();

            result.AddLast(MineCraftBookFormat.Title + Title);
            result.AddLast(MineCraftBookFormat.Author + Author);
            result.AddLast(MineCraftBookFormat.Pages);
            
            return result;
        }
    }
}
