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

        /// <summary>
        /// Silly introduction page. 
        /// </summary>
        /// <param name="text"></param>
        private void IntroText()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            AnsiConsole.Status().Start("Getting things ready...", ctx =>
            {

                AnsiConsole.MarkupLine("Warming up buffers ...");
                //Thread.Sleep(2000);
                // Update the status and spinner
                ctx.Status("Working...");
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
                AnsiConsole.MarkupLine("[yellow]Initializing flux variant capacitors...[/]");
                //Thread.Sleep(2000);
                AnsiConsole.MarkupLine("[green]Waisting a little more of your time ...[/]");
                //Thread.Sleep(4000);

            }
            );
            /*Console.ResetColor(); */
        }
    }
}
