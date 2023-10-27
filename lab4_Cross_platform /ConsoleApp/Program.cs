using McMaster.Extensions.CommandLineUtils;
using LabClassLibrary; // replace with the actual namespace of your class library

namespace ConsoleApp
{
    [Command(Name = "YourApp", Description = "Your application description")]
    [Subcommand(typeof(VersionCommand))]
    [Subcommand(typeof(RunCommand))]
    [Subcommand(typeof(SetPathCommand))]

    class Program
    {
        static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("Specify a command.");
            app.ShowHelp();
            return 1;
        }
    }

    [Command(Description = "Displays program version information")]
    class VersionCommand
    {
        private void OnExecute(IConsole console)
        {
            console.WriteLine("Author: Your Name");
            console.WriteLine("Version: 1.0.0");
        }
    }

    [Command(Description = "Runs a specific lab")]
    class RunCommand
    {
        [Option("-I|--input <INPUT>", "Input file path", CommandOptionType.SingleValue)]
        public string InputFile { get; set; }

        [Option("-o|--output <OUTPUT>", "Output file path", CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        private void OnExecute(IConsole console)
        {
            switch (this.Lab)
            {
                case "lab1":
                    Lab1.ExecuteLab1(InputFile, OutputFile);
                    break;
                case "lab2":
                    Lab2.ExecuteLab2(InputFile, OutputFile);
                    break;
                case "lab3":
                    Lab3.ExecuteLab3(InputFile, OutputFile);
                    break;
                default:
                    console.WriteLine("Invalid lab specified.");
                    break;
            }
        }

        [Argument(0, Description = "Specify lab1, lab2, or lab3")]
        public string Lab { get; set; }
    }

    [Command(Description = "Set the LAB_PATH environment variable")]
    class SetPathCommand
    {
        [Option("-p|--path <PATH>", "Path to the folder with input/output files", CommandOptionType.SingleValue)]
        public string LabPath { get; set; }
    }
}