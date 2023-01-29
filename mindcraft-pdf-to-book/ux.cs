using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using UglyToad.PdfPig.Fonts.TrueType.Names;

namespace PdfToMineCraftBook
{
    internal class UX
    {

        private FigletText homeText;
        private bool run = true;
        private const string CONVERT = "Convert PDF to MineCraft Book";
        private const string EXIT = "Exit";
        private const string MYSTERY = "Mystery Box";
        private const string RESET = "Reset Screen";
        public UX() 
        {
            homeText = new FigletText("PDF To MineCraft").Centered().Color(Color.Aqua);
        }

        public void Init()
        {
            Reset();
            WriteText("Let's get started!");
            IntroText();
            Reset();
            Run();
        }

        /// <summary>
        /// Reset the screen
        /// </summary>
        private void Reset()
        {
            Console.Clear();
            AnsiConsole.Write(homeText); 
            Console.Write("");
            
        }

        /// <summary>
        /// Silly introduction page. 
        /// </summary>
        /// <param name="text"></param>
        private void IntroText()
        {
            Console.ForegroundColor= ConsoleColor.Blue;   
            AnsiConsole.Status().Start("Getting things ready...", ctx =>
                {

                    AnsiConsole.MarkupLine("Warming up buffers ...");
                    Thread.Sleep(2000);
                    // Update the status and spinner
                    ctx.Status("Working...");
                    ctx.Spinner(Spinner.Known.Star);
                    ctx.SpinnerStyle(Style.Parse("green"));
                    AnsiConsole.MarkupLine("[yellow]Initializing flux variant capacitors...[/]");
                    Thread.Sleep(2000);
                    AnsiConsole.MarkupLine("[green]Waisting a little more of your time ...[/]");
                    Thread.Sleep(4000);

                }
            );
            /*Console.ResetColor(); */
        }

        /// <summary>
        /// Run the UX
        /// </summary>
        private void Run()
        {
            int max = 1000, i = 0;

            while (this.run && i < max)
            {
                string[] choices = { CONVERT, MYSTERY, RESET, EXIT };
                string userSelection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Select an [yellow]option[/]: ")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more options.)[/]")
                    .AddChoices(choices));

                switch (userSelection)
                {
                    case EXIT:
                        this.run= false;
                        break; 

                    case CONVERT:
                        if (ConvertPDF())
                        {
                            WriteText("PDF converted...");
                            WriteText("Resetting Screen...");
                            Reset();
                        }
                        else
                        {
                            WriteText("So... Now what?");
                        }
                        
                        break;
                    
                    case RESET: 
                        Reset();
                        break;
                    case MYSTERY:
                        
                        break;
                }
                

                i++;
            }
            if(i == max)
            {
                WriteText("We've been at this for a while now... How about you close me and let me get some rest? ");
            }

        }

        private bool ConvertPDF()
        {
            bool answerConvertPDF = AnsiConsole.Confirm("Convert a[yellow] PDF file?[/]");

            if (!answerConvertPDF)
            {
                return  answerConvertPDF;
            }

            bool answerDisplayBookText = AnsiConsole.Confirm("Would you like to [yellow]display or edit[/] the text before converting the book?");
            
            bool success = false;

            string path = AnsiConsole.Ask<string>("Please enter the exact path to the PDF file: ");
            string pathOut = AnsiConsole.Ask<string>("Please enter the exact path to folder you would like the MineCraft book to go to: ");
            string fileName = AnsiConsole.Ask<string>("What do you want to name the MineCraft book file?:");
            string fileOut = pathOut + "\\" + fileName;
            bool pdfIsLoaded = false;
            try
            {
                PDFConversion conversion = new PDFConversion(path);
                LinkedList<string>? book;
                AnsiConsole.Status().Start("Loading PDF...", ctx =>
                {
                    AnsiConsole.MarkupLine("Warming up buffer plate ...");

                    ctx.Status("Starting conversion");
                    ctx.Spinner(Spinner.Known.Star);
                    ctx.SpinnerStyle(Style.Parse("green"));
                    pdfIsLoaded = conversion.LoadPDF(); 
                    
                });

                if(!pdfIsLoaded)
                {
                    return false;
                }

                if(answerDisplayBookText) 
                {
                    conversion.ViewPDFText();
                    bool addStringToRemove = AnsiConsole.Confirm("Would you like to remove any text from the book before we convert it?");
                    if(addStringToRemove)
                    {
                        LinkedList<string> list = new LinkedList<string>();
                        while (addStringToRemove)
                        {
                            list.AddLast(AnsiConsole.Ask<string>("Enter text you would like to remove:"));
                            addStringToRemove = AnsiConsole.Confirm("Would you like to add another?");
                        }
                        
                        book = conversion.GenerateBook(list);
                    }
                    else
                    {
                        book = conversion.GenerateBook();
                    }

                }
                else
                {
                    book = conversion.GenerateBook();
                }

                if(book != null)
                {
                    AnsiConsole.Status().Start("Writing File to drive...", ctx =>
                    {
                        AnsiConsole.MarkupLine("Ready file write systems...");

                        ctx.Status("Writing...");
                        ctx.Spinner(Spinner.Known.Star);
                        ctx.SpinnerStyle(Style.Parse("green"));
                        _ = WriteTextFile(book, fileOut);
                    });

                    WriteText("It looks like everything went as expected. Check the output file.");
                    WriteText(fileOut);
                    success = true;

                }
                else
                {
                    WriteText("It looks like we've encountered a problem. I didn't get a book from what you gave me.");
                    WriteText("We can try again if you'd like...");
                    success = false;
                }

            }
            catch(Exception e)
            {
                success = false;
                WriteText("We've had a problem...");
                WriteText(e.ToString());
            }

            return success;
        }

        private void WriteText(string text)
        {

            Console.ForegroundColor= ConsoleColor.Green;   
            for(int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i].ToString(), Color.Green);
                Thread.Sleep(100);
            }
            Console.WriteLine();
            /*Console.ResetColor();*/
            
        }

        private static async Task WriteTextFile(LinkedList<string> text, string path)
        {
            await File.WriteAllLinesAsync(path, text);
        }
    }


}
