using System;


namespace FileProcessor
{
    internal class ConsoleOutPut
    {
        internal void output(string parsedRows)
        {
            Console.WriteLine(parsedRows);
            //Environment.Exit(1);
            return;
        }

        public void Error(string error)
        {
            Console.WriteLine(error);
            if (Program.checkEnv == "Dev")
            {
                Environment.Exit(1);
            }
            else
            {
                return;
            }
        }
    }
}