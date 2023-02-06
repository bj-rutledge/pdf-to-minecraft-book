using Spectre.Console;


namespace PdfToMineCraftBook
{
    internal partial class UX
    {

        private bool ConvertPDF()
        {
            bool answerConvertPDF = AnsiConsole.Confirm("Convert a[yellow] PDF file?[/]");

            if (!answerConvertPDF)
            {
                return answerConvertPDF;
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

                if (!pdfIsLoaded)
                {
                    return false;
                }

                if (answerDisplayBookText)
                {
                    conversion.ViewPDFText();
                    bool addStringToRemove = AnsiConsole.Confirm("Would you like to remove any text from the book before we convert it?");
                    if (addStringToRemove)
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

                if (book != null)
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
            catch (Exception e)
            {
                success = false;
                WriteText("We've had a problem...");
                WriteText(e.ToString());
            }

            return success;
        }

    }
}
