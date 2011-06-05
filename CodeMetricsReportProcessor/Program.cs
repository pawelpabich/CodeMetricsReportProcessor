using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorEngine.Templating;

namespace CodeMetricsReportProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = Console.ForegroundColor;

            try
            {
                var reportGenerator = new ReportGenerator();
                reportGenerator.GenerateFullReport(@"C:\Temp\MetricTest\out.xml", @"C:\Temp\MetricTest\Results");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("All good");

            }
            catch(TemplateCompilationException ex)
            {
                var message = "Errors: ";
                foreach (var compilerError in ex.Errors)
                {
                    message += Environment.NewLine + compilerError.ToString();
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
