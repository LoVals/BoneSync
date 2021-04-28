using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Data;

namespace BoneSync
{
    class BoneSync
    {
        static void Main()
        {

            string TestFile = "Blank";
            bool SetPath = false;
            string rootPath = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\SoundBones";      //Replace this with Current Directory + \Soundbones as the script resides in the same folder as pre build sync
            string path = Directory.GetCurrentDirectory();
            Console.WriteLine(" _________________________");
            Console.WriteLine("¦                         ¦");
            Console.WriteLine("¦       Bonesync 1.0      ¦");
            Console.WriteLine("¦_________________________¦");
            Console.WriteLine("");
            Console.WriteLine("The current directory is {0}", path);
            Console.WriteLine("Preparing to syncronize...");

            if (SetPath = false)
            {
                BoneSync.ChangeFolder();
            }

            Console.ReadLine();
            //Console.WriteLine("You Selected " + TestFile);
            BoneSync.WipeOldFiles();
            BoneSync.XMLMake(rootPath);
            Console.ReadLine();

            //NEED TO WRITE THE SYNC ALGORITHM - XML MERGE will likely be the way



        }
        public static void XMLMake(string rootPath)
        //Will copy over the fdp files as xml to merge them subsequently
        {

            string sourceDir = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\SoundBones";
            string backupDir = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs";
            string fileName = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs";


            //should change this if the user decides to select another directory
            Console.WriteLine("XML conversion is starting");
            Console.WriteLine("Fetching SoundBones...");
            var files = Directory.GetFiles(rootPath, "*", SearchOption.TopDirectoryOnly);


            foreach (string file in files)
            {
                Console.WriteLine(file);
            }

            try
            {
                string[] BoneList = Directory.GetFiles(sourceDir, "*.fdp");
                foreach (string f in BoneList)
                {
                    // Remove path from the file name.
                    string fName = f.Substring(sourceDir.Length + 1);

                    try
                    {
                        // Will not overwrite if the destination file already exists.
                        File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName));
                        //File.Move((Path.Combine(backupDir, fName), Path.Combine(backupDir, fName);
                    }

                    // Catch exception if the file was already copied.
                    catch (IOException copyError)
                    {
                        Console.WriteLine(copyError.Message);
                    }
                

                }
            }

            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
        }

        public static void ChangeFolder()
        {
            //Code goes here
        }
        public static void WipeOldFiles()
        {
            //Wipes any leftover file in the XML dir


            string[] filePaths = Directory.GetFiles(@"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs");
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }
    }
}
