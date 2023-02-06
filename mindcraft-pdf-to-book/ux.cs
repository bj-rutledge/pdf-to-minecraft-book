using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using UglyToad.PdfPig.Fonts.TrueType.Names;

namespace PdfToMineCraftBook
{
    internal partial class UX
    {

        private FigletText homeText;
        private bool running = true;
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
        /// Run the UX
        /// </summary>
        private void Run()
        {
            int max = 1000, i = 0;

            while (this.running && i < max)
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
                        this.running = false;
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
                        if (SelectMystery())
                        {
                            AnsiConsole.WriteLine("The mystery is where you told me to put it. Go check it out!"); 
                        }
                        else
                        {
                            AnsiConsole.WriteLine("Try again, Dude."); 
                        }
                        break;

                    default: break;
                }
                
                i++;
            }
            if(i == max)
            {
                WriteText("We've been at this for a while now... How about you close me and let me get some rest? ");
            }

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

        private static void WriteTextFile(string text, string path)
        {
            File.WriteAllText(path, text);
        }
    }


}
