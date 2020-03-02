using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LearningCS3
{
    public class App
    {
        public void Run()
        {
            bool quit;

            do
            {
                Console.Clear();
                Console.WriteLine("Welcome! This is my LinkedIn Learning C# Console PRactice App!\n Please select from the following menu");




                var key = userSelect();
                quit = UserChoice(key);
            } while (!quit);
        }

        private ConsoleKey userSelect()
        {
            ConsoleKey;

            key = input();

            return key;
        }

        private ConsoleKey input()
        {
            return Console.ReadKey().Key;
        }

        public bool UserChoice(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey
            }
        }
    }
}
