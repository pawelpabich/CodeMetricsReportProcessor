using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;
using RazorEngine.Templating;

namespace CodeMetricsReportProcessor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var system = Console.ForegroundColor;

            try
            {
                var pathToInputFile = "";
                var pathToOutputFolder = "";
                var showHelp = false;

                var options = new OptionSet() {
                    { "i|input=", "path to the input file", input => pathToInputFile = input },
                    { "o|output=", "path to the output *folder*", o => pathToOutputFolder = o},
                    { "h|help",  "show this message and exit", v => showHelp = v != null },
                };

                options.Parse(args);
                if (args.Length < 2) showHelp = true;

                if (showHelp)
                {
                    options.WriteOptionDescriptions(Console.Out);   
                    return;
                }


                var reportGenerator = new ReportGenerator();
                reportGenerator.GenerateFullReport(pathToInputFile, pathToOutputFolder);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("All good");

            }
            catch(TemplateCompilationException ex)
            {
                var message = "Errors: ";
                foreach (var compilerError in ex.Errors)
                {
                    message += "-----------------" + Environment.NewLine + compilerError;
                }

                LogException(ex, message);
            }
            catch (Exception e)
            {
                LogException(e, "Something went bad.");
            }
            finally
            {
                Console.ForegroundColor = system;
            }
        }

        private static void LogException(Exception e, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message + Environment.NewLine + e.ToString());
        }
    }
}
