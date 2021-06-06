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
            var ParentFile = XDocument.Load(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\SoundSouls.xml");
            string BoneName = "fdlc_c3471";

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

                // THERE IS AN ISSUE WITH THE VALUE OF THE TESTBELONGING HERE _ INVESTIGATE PLX

                IsChildValid(TestBelonging, BoneName);
                if (IsChildValid(TestBelonging, BoneName) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SoundBone " + BoneName + ": Sound Def content has been found!");
                    Console.WriteLine();
                    RunGenerator(ChildElement, "sounddeffolder", BoneName, 1);
                    break;
                }
            }

            //-------------------------------------------------------------------------------//
            //                             Event Group                                       //
            //-------------------------------------------------------------------------------//
            //THEORY - NOT NECESSARY
            Console.ForegroundColor = ConsoleColor.Yellow;
            var EventGroupFolderAll = ParentFile.Descendants("eventgroup");
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
                    Console.ReadLine();
                    Console.WriteLine();
                    XDocument SoundBone = XDocument.Load(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + ".xml");
                    var PotentialTarget = SoundBone.Descendants("eventgroup");
                    foreach (var Element in PotentialTarget)
                    {
                        string Y = PotentialTarget.FirstOrDefault().Value;
                        Console.WriteLine(Y);
                        Console.ReadLine();            
                    }
                    Console.ReadLine();
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

                    RunGenerator(ChildElement, "soundbank", BoneName, 2);
                    break;

                }
            }


            //-------------------------------------------------------------------------------
            // LOAD ALL CHILDREN NAMES

            GenerationCleanup(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "Generated.xml", BoneName);
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

        public static bool IsMusic(string TestedElement)
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

        public static void RunGenerator(XElement ParentTargetContent, string ReplacementTarget, string BoneName, int DataType)
        //DataType: 1 = Sound Def // 2 = SoundBank
        {
            // select node from one doc
            XDocument SoundBone = XDocument.Load(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + ".xml");
            var PotentialTarget = SoundBone.Descendants(ReplacementTarget);
            string ReplaceMe = ReplacementTarget;

            switch (DataType)
            {
                case 1:
                    foreach (var Element in PotentialTarget)
                    //SOUND DEF
                    {
                        string TestBelonging = PotentialTarget.Descendants(ReplaceMe).FirstOrDefault().Value;
                        if (IsChildValid(TestBelonging, BoneName) == true)
                        {
                            XElement ToReplace = Element.Descendants(ReplaceMe).FirstOrDefault();
                            Console.WriteLine(ToReplace);
                            ToReplace.ReplaceWith(ParentTargetContent);
                            SoundBone.Save(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "Generated.xml");
                            break;
                        }
                        Console.WriteLine("no Match Detected - OOF!");
                    }
                    break;
                case 2:
                    foreach (var Element in PotentialTarget)
                    //SOUNDBANKS
                    {
                        string TestBelonging = PotentialTarget.FirstOrDefault().Value;
                        if (IsChildValid(TestBelonging, BoneName) == true)
                        {
                            XElement ToReplace = Element;
                            Console.WriteLine(ToReplace);
                            Console.WriteLine("In theory replacement has been found");
                            Console.ReadLine();
                            ToReplace.ReplaceWith(ParentTargetContent);
                            SoundBone.Save(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "Generated.xml");
                            break;
                        }
                        Console.WriteLine("no Match Detected - OOF!");
                    }
                    break;
            } 
        }

        public static void GenerationCleanup(string fileName, string BoneName)
        {
            var lines = File.ReadAllLines(fileName);
            File.WriteAllLines(fileName, lines.Skip(1).ToArray()); //Removes the declaration from the file

            //-------------------------------------------------------------------------------//
            //                          Cleanup Sound Def Folder                             //
            //-------------------------------------------------------------------------------//

            //the issue is mismatching sound Def - Examlpe in the case of fdlc_c3471 the string "/SoundSouls/NPC""
            //needs to be removed from the <sounddef>/< name > entry
            string TextCache = File.ReadAllText(fileName);
            TextCache = TextCache.Replace("/SoundSouls/NPC", "");
            File.WriteAllText(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\fdlc_c3471gen.fdp", TextCache);           
            Console.WriteLine("COMPLETED");
            //string TextCacheB = File.ReadAllText(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\fdlc_c3471gen.fdp");
            //TextCacheB = TextCacheB.Replace("/SoundSouls/NPC", "");
            //TextCacheB = TextCacheB.Replace("/souls sfx/", "../souls sfx/");
            //File.WriteAllText(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\fdlc_c3471gen.fdp", TextCacheB);

        }
    }
}

