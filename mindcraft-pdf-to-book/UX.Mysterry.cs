using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfToMineCraftBook
{
    internal partial class UX
    {

        private bool SelectMystery()
        {
            bool success = false;
            WriteText("You've selected the Mystery box... \nPDFs are boooorrrrring. \nLet's do something fun!");
            string path = AnsiConsole.Ask<string>("Please enter the exact path to an image file: ");
            string pathOut = AnsiConsole.Ask<string>("Please enter the exact path to folder you would like mystery to go to: ");
            string fileName = AnsiConsole.Ask<string>("What do you want to name the file?:");
            string fileOut = pathOut + "\\" + fileName;
            WriteText("Ok, I'll get started. Hang tight.");
            AnsiConsole.Status().Start("Loading image...", ctx =>
            {
                try
                {
                    AnsiConsole.MarkupLine("Getting thing ready...");
                    string? mysString = CreateMystery(path);
                    if (mysString == null)
                    {
                        AnsiConsole.MarkupLine("Ah man!!! Shit! Fuck, Fuck! Fuck! Fuck! Something went wrong. Try again.");
                        AnsiConsole.MarkupLine("Make sure you gave me an image. I'm kinda lazy, so I didn't feel like validating the file before I tried my mystery.");
                        AnsiConsole.MarkupLine("Resetting screen.");
                    }
                    else
                    {
                        WriteTextFile(mysString, fileOut);
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteLine("Dude, we hit a snag... Here's the error we encuntered.");
                    AnsiConsole.WriteException(ex);
                    success = false;
                }
            });

            return success;
        }

        private string? CreateMystery(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    Mystery mys = new Mystery(path);
                    if (mys.GetAsciiImage())
                    {
                        return mys.AsciiImage;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

    }
}
