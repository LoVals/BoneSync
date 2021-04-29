﻿using System;
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
            bool SetPath = false;                                                                                               //Shall I actually give users custom Folders? I am thinking Fuck no
            string CurrentDir = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls";                                               //Replace this with Directory.GetCurrentDirectory
            string rootPath = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\SoundBones";                                      //Replace this with Current Directory + \Soundbones as the script resides in the same folder as pre build sync
            string path = Directory.GetCurrentDirectory();                                                                      // will likely change to the SOUNDSOUL root
            Console.WriteLine(" _________________________");
            Console.WriteLine("¦                         ¦");
            Console.WriteLine("¦       Bonesync 1.0      ¦");
            Console.WriteLine("¦_________________________¦");
            Console.WriteLine("");
            Console.WriteLine("The current directory is {0}", path);
            Console.WriteLine("Preparing to syncronize...");
            Console.WriteLine("Press any key to start");

            if (SetPath == false)
            {
                BoneSync.ChangeFolder();
            }

            Console.ReadLine();
            BoneSync.WipeOldFiles();
            BoneSync.XMLMake(rootPath);
            BoneSync.MainProjectCopy();
            Console.WriteLine("I STILL NEED TO WRITE THE SYNC TOOL - THUS THAT's IT YA CUNT!");
            Console.ReadLine();

            //NEED TO WRITE THE SYNC ALGORITHM - XML MERGE will likely be the way



        }
        public static void XMLMake(string rootPath)                                                                             //Will copy over the fdp files as xml to merge them subsequently
        {
            string sourceDir = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\SoundBones";
            string backupDir = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs";
            //should change this if the user decides to select another directory
            Console.WriteLine("XML conversion is starting");
            Console.WriteLine("Fetching SoundBones...");
            var files = Directory.GetFiles(rootPath, "*.fdp", SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                Console.WriteLine(file);
            }                                                                                                                    //spits out the file list
            Console.WriteLine("SoundBones have been Fetched, Copying...");
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
                        Console.WriteLine(fName + " copied successfully");
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

            BoneSync.ChangeBoneExtension(".fdp", ".xml");
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

        public static void ChangeBoneExtension(string OldExt, string NewExt)                                                        //should swap the extension of fdp files to XML
        {
            string XMLDIR = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs";
            var FDPFILE = Directory.GetFiles(XMLDIR, "*", SearchOption.TopDirectoryOnly);

            foreach (string file in FDPFILE)
            {
                string filename = Path.GetFileNameWithoutExtension(file);
                File.Move(XMLDIR+@"/"+filename+OldExt,XMLDIR+@"/"+filename+NewExt);
                Console.WriteLine(filename + OldExt + " has been converted to " + filename + NewExt);
            }
        }

        public static void MainProjectCopy()                                                                                     //Copies over the soundsouls project as XML
        {
            string sourceDir = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls";                                                 //will need replacement with dynamic shit
            string backupDir = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs";
            File.Copy(sourceDir + @"\SoundSouls.fdp", backupDir + @"\SoundSouls.xml");                                           //Should also convert to XML
        }
    }
}
