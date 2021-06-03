﻿using System;
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
using DiffMatchPatch;

namespace BoneSync_02
{
    class GenerateChild
    {
        public static void Execute()
        {
            Console.ForegroundColor = ConsoleColor.White;
            //What needs to be done:

            //>>    Detect what has changed
            //>>    Regenerate the XML File

            //------------------------------------------------------------------------------
            // LOAD PARENT FILE INTO MEMORY
            var ParentFile = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\Parent.xml");
            string BoneName = "Child1";

            //-------------------------------------------------------------------------------//
            //                             Event Category                                    //
            //-------------------------------------------------------------------------------//

            // THEORY - EVENTCATEGORIES NEED TO  BE IGNORED - they hold no definition of where their children resides. That could be defined in the EventGroup?
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Checking Event Category");
            var EventCategoryFolderAll = ParentFile.Descendants("eventcategory");
            int Shortcutvalue = 0;
            foreach (var ChildElement in EventCategoryFolderAll)
            {

                string TestMatch = ChildElement.Descendants("name").FirstOrDefault().Value;
                switch (Shortcutvalue)
                {
                    case 0:
                        goto _Music;

                    case 1:
                        goto _Ghost;

                    case 2:
                        goto _SE;

                    case 3:
                        goto _Voice;

                    case 4:
                        goto _Default;
                }


                _Music:
                if (IsMusic(TestMatch) == true)
                {
                    Console.WriteLine("music folder found");
                    Shortcutvalue = 1;
                }

                _Ghost:
                if (IsGhost(TestMatch) == true)
                {
                    Console.WriteLine("Ghost folder found");
                    Shortcutvalue = 2;
                }

                _SE:
                if (IsSE(TestMatch) == true)
                {
                    Console.WriteLine("SE (Sound Efffects) folder found");
                    Shortcutvalue = 3;
                }

                _Voice:
                if (IsVoice(TestMatch) == true)
                {
                    Console.WriteLine("Voice folder found");
                    Shortcutvalue = 4;
                }

                _Default:
                if (IsDefault(TestMatch) == true)
                {
                    Console.WriteLine("Default folder found");
                }

            }

            //-------------------------------------------------------------------------------//
            //                             Sound Def Folder                                  //
            //-------------------------------------------------------------------------------//
            Console.ForegroundColor = ConsoleColor.Yellow;
            var SounddefFolderAll = ParentFile.Descendants("sounddeffolder");
            Console.WriteLine("Checking Sound Def Folder");
            foreach (var ChildElement in SounddefFolderAll)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value;
                IsChildValid(TestBelonging, BoneName);
                if (IsChildValid(TestBelonging, BoneName) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SoundBone " + BoneName + ": Sound Def content has been found!");
                    Console.WriteLine();
                    RunGenerator(ChildElement, "sounddeffolder", BoneName);
                    break;
                }
            }

            //-------------------------------------------------------------------------------//
            //                             Event Group                                       //
            //-------------------------------------------------------------------------------//
            Console.ForegroundColor = ConsoleColor.Yellow;
            var EventGroupFolderAll = ParentFile.Descendants("soundbank");
            Console.WriteLine("Checking Event Group");
            foreach (var ChildElement in EventGroupFolderAll)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value;
                IsChildValid(TestBelonging, BoneName);
                if (IsChildValid(TestBelonging, BoneName) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SoundBone " + BoneName + " : Event Group content has been found");
                    Console.WriteLine();
                    break;
                }
            }
            //-------------------------------------------------------------------------------//
            //                             Sound Banks                                       //
            //-------------------------------------------------------------------------------//
            Console.ForegroundColor = ConsoleColor.Yellow;
            var SoundBankFolderAll = ParentFile.Descendants("soundbank");
            Console.WriteLine("Checking Soundbanks");
            foreach (var ChildElement in SoundBankFolderAll)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value;
                IsChildValid(TestBelonging, BoneName);
                if (IsChildValid(TestBelonging, BoneName) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SoundBone " + BoneName + " : SoundBanks content has been found");
                    break;
                }
            }


            //-------------------------------------------------------------------------------
            // LOAD ALL CHILDREN NAMES
            string SoundBonesDir = @"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\FDP\Children";
            var FDPFILE = Directory.GetFiles(SoundBonesDir, "*", SearchOption.TopDirectoryOnly);

            foreach (string file in FDPFILE)
            {
                //string BoneName = "Child1";//Path.GetFileNameWithoutExtension(file);
                //EVENTGROUP
                //search the eventgroup containing the element with that value
                //I am assuming the user will not change the Root Folder's name
  
                

                //currently not working - unsure why
               // Console.WriteLine("The Element found is:");
              //  Console.WriteLine(EventGroupNode);
              //  Console.ReadLine();
            }

        }

        // BOOLEANS DETERMINE IF THE FOLDER I'M IN IS SPECIFIC AND IF IT BELONGS TO THE CHILD I'M GENERATING

        public static bool IsChildValid(string TestedElement, string BoneName)
        {
            diff_match_patch MatchTest = new diff_match_patch();
            MatchTest.Match_Threshold = 0.03f;
            MatchTest.Match_Distance = 1;
            var Match = MatchTest.match_main(TestedElement, BoneName, 0);
            if (Match == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsMusic (string TestedElement)
        {
            diff_match_patch MatchTest = new diff_match_patch();
            MatchTest.Match_Threshold = 0.03f;
            MatchTest.Match_Distance = 1;
            var Match = MatchTest.match_main(TestedElement, "music", 0);
            if (Match == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsGhost(string TestedElement)
        {
            diff_match_patch MatchTest = new diff_match_patch();
            MatchTest.Match_Threshold = 0.03f;
            MatchTest.Match_Distance = 1;
            var Match = MatchTest.match_main(TestedElement, "Ghost", 0);
            if (Match == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsSE(string TestedElement)
        {
            diff_match_patch MatchTest = new diff_match_patch();
            MatchTest.Match_Threshold = 0.03f;
            MatchTest.Match_Distance = 1;
            var Match = MatchTest.match_main(TestedElement, "SE", 0);
            if (Match == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsVoice(string TestedElement)
        {
            diff_match_patch MatchTest = new diff_match_patch();
            MatchTest.Match_Threshold = 0.03f;
            MatchTest.Match_Distance = 1;
            var Match = MatchTest.match_main(TestedElement, "Voice", 0);
            if (Match == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDefault(string TestedElement)
        {
            diff_match_patch MatchTest = new diff_match_patch();
            MatchTest.Match_Threshold = 0.03f;
            MatchTest.Match_Distance = 1;
            var Match = MatchTest.match_main(TestedElement, "Default", 0);
            if (Match == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // CHILD WRITER - Code Responsible for regenerating the soundbones

        public static void RunGenerator(XElement ParentTargetContent, string ReplacementTarget, string BoneName)

            //                                  !!!NEEDS TESTING!!!

        {
            // select node from one doc
            XDocument SoundBone = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\"+BoneName+".xml");
            XElement ToReplace = SoundBone.Descendants(ReplacementTarget).First();

            // replace one xml node with another
            ToReplace.ReplaceWith(ParentTargetContent);
            SoundBone.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "Generated.xml");
        }

    }
}
