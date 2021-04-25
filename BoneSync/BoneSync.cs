using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoneSync
{
    class BoneSync
    {
        static void Main(string[] args)
        {
            string TestFile = "Blank";
            bool PathSet = false;

            Console.WriteLine(" _________________________");
            Console.WriteLine("¦                         ¦");
            Console.WriteLine("¦       Bonesync 1.0      ¦");
            Console.WriteLine("¦_________________________¦");
            Console.WriteLine("");
            Console.WriteLine("Preparing to syncronize...");
            if (PathSet = false)
            {
                Console.WriteLine("Filepath Not Found: ");
                Console.WriteLine("Please, select the SoundSouls Filepath ");
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
                //TODO - ADD A FUCKING FILE SELECTION IF PATH NOT SET
            }
            Console.WriteLine("Would you like to select a filepath for the ");
            Console.ReadLine();
            Console.WriteLine("You Selected " + TestFile);
            Console.ReadLine();

            //NEED TO WRITE THE SYNC ALGORITHM - NO CLUE HOW THE FUCK TO DO THAT YET


        }
    }
}
