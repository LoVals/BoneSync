using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Data;
using System.Xml;
using System.Xml.Linq;
//using Microsoft.XmlDiffPatch;

namespace BoneSync
{
    class BoneSync
    {
        static void Main()
        {                                                                                          //Shall I actually give users custom Folders? I am thinking Fuck no
            string CurrentDir = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls";                                               //Replace this with Directory.GetCurrentDirectory
            string rootPath = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\SoundBones";                                      //Replace this with Current Directory + \Soundbones as the script resides in the same folder as pre build sync
            string path = Directory.GetCurrentDirectory();
            Console.ForegroundColor = ConsoleColor.Green;
            // will likely change to the SOUNDSOUL root
            Console.WriteLine(" _________________________");
            Console.WriteLine("¦                         ¦");
            Console.WriteLine("¦       Bonesync 1.0      ¦");
            Console.WriteLine("¦_________________________¦");
            Console.WriteLine("");
            Console.WriteLine("The current directory is {0}", path);
            Console.WriteLine("Preparing to syncronize...");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Would You like to skip the XML conversion? TEST ONLY! 1 = yes 0 = no");
            Console.ForegroundColor = ConsoleColor.White;
            //File.Copy(@"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs\SoundSouls.xml", @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs\PreviousVersion\SoundSoulsCache.xml");
            int SkipSelect = int.Parse(Console.ReadLine());
            switch (SkipSelect)
            {
                case 0:
                    Console.WriteLine("Executing Full Protocol");
                    BoneSync.WipeOldFiles();
                    BoneSync.XMLMake(rootPath);
                    BoneSync.MainProjectCopy("SoundSouls.xml", "SoundSoulsCache.xml");
                    BoneSync.ParseXML();
                    Console.WriteLine("I STILL NEED TO WRITE THE SYNC TOOL - THUS THAT's IT YA CUNT!");
                    BoneSync.ParseXML();
                    Console.WriteLine("Press any key to continue with children comparison");
                    Console.ReadKey();
                    BoneDiffer.SkeletonToBone(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_A.xml", @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_B_Child.xml", "TestChild");
                    //For now this shit is static - Wil need dynamic

                    break;
                case 1:
                    Console.WriteLine("Skipping XML preconversion");
                    BoneSync.ParseXML();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Moving on to children comparison");
                    Console.WriteLine("Splitting XML into readable nuggets");
                    XMLSplitter.SkeletonSplit(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_B.xml");
                    XMLSplitter.ChildSplit(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_B_Child.xml", "TEST_B_Child");
                    Console.WriteLine("Testing Diff algoruthm for bones");
                    BoneDiffer.SkeletonToBone(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_A.xml", @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_B_Child.xml", "TestChild"); //this algorithm works but only for small files
                    //XMLSplitter.WipeOldNuggets();
                    //For now this shit is static - Wil need dynamic
                    break;
                default:
                    Console.WriteLine("Default case Detected - Failsafe redirection to full protocol");
                    Console.WriteLine("Executing Full Protocol");
                    BoneSync.WipeOldFiles();
                    BoneSync.XMLMake(rootPath);
                    BoneSync.MainProjectCopy("SoundSouls.xml", "SoundSoulsCache.xml");
                    Console.WriteLine("I STILL NEED TO WRITE THE SYNC TOOL - THUS THAT's IT YA CUNT!");
                    BoneSync.ParseXML();
                    Console.WriteLine("Press any key to continue with children comparison");
                    Console.ReadKey();
                    BoneDiffer.SkeletonToBone(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_A.xml", @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_B_Child.xml", "TestChild");
                    //For now this shit is static - Wil need dynamic
                    break;
            }
            

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
        public static void WipeOldFiles()
        {
            //Wipes any leftover file in the XML dir
            string[] filePaths = Directory.GetFiles(@"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs");
            //File.Delete(filePaths + @"\PreviousVersion\SoundSoulsCache.xml");
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }                                                                                    
        //WIPES OLD FILES FROM THE DRIVE - happens before XML conversion of FDPS
        public static void ChangeBoneExtension(string OldExt, string NewExt)                                                    
        
        {
            string XMLDIR = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs";
            var FDPFILE = Directory.GetFiles(XMLDIR, "*", SearchOption.TopDirectoryOnly);

            foreach (string file in FDPFILE)
            {
                string filename = Path.GetFileNameWithoutExtension(file);
                File.Move(XMLDIR + @"/" + filename + OldExt, XMLDIR + @"/" + filename + NewExt);
                Console.WriteLine(filename + OldExt + " has been converted to " + filename + NewExt);
            }
        }
        //Swaps the extension of fdp files to XML
        public static void MainProjectCopy(string OldFileName, string NewFileName)
        //Copies over the soundsouls project as XML
        {
            var SoundSoulsFDP = Directory.GetFiles(@"D:\Programs\Static\Dark_Souls_Mods\SoundSouls", "SoundSouls.fdp", SearchOption.TopDirectoryOnly);
            string BackUpDIR = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs\SoundSoulsXML\Cache";
            string MXMLDIR = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs\SoundSoulsXML";
            string[] CachePath = Directory.GetFiles(@"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs\SoundSoulsXML\Cache");
            foreach (string filePath in CachePath)
            {
                File.Delete(filePath);
            }

            foreach (string file in SoundSoulsFDP)
            {
                File.Move(MXMLDIR + @"/" + OldFileName, BackUpDIR + @"/" + NewFileName);
            }

            File.Copy(@"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\SoundSouls.fdp", @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs\SoundSoulsXML\SoundSouls.xml");                                           //Should also convert to XML

        }            
        public static void ParseXML()
        {
            XMLParse.ParseXML();
        }
        //Executes diff protocol
    }
}
