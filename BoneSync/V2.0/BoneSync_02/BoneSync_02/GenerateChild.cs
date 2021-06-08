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
            Console.WriteLine();
            //What needs to be done:

            //>>    Detect what has changed
            //>>    Regenerate the XML File

            //------------------------------------------------------------------------------
            // LOAD PARENT FILE INTO MEMORY
            var ParentFile = XDocument.Load(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\SoundSouls.xml");
            string BoneName = "fdlc_m12";

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
            //                             Sound Banks                                       //
            //-------------------------------------------------------------------------------//
            Console.ForegroundColor = ConsoleColor.Yellow;
            var SoundBankFolderAll = ParentFile.Descendants("soundbank");
            Console.WriteLine("Checking Soundbanks");
            foreach (var ChildElement in SoundBankFolderAll)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value;
                if (IsChildValid(TestBelonging, BoneName) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SoundBone " + BoneName + " : SoundBanks content has been found");
                    Console.WriteLine();
                    RunGenerator(ChildElement, "soundbank", BoneName, 2);
                    break;

                }
            }

            //-------------------------------------------------------------------------------//
            //                             Event Group                                       //
            //-------------------------------------------------------------------------------//
            Console.ForegroundColor = ConsoleColor.Yellow;
            var EventGroupFolderAll = ParentFile.Descendants("eventgroup");
            Console.WriteLine("Checking Event Group");
            foreach (var ChildElement in EventGroupFolderAll)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value;
                string BoneID = BoneName.Replace("frpg_", "");
                BoneID = BoneID.Replace("fdlc_", "");
                if (IsChildValid(TestBelonging, BoneID) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SoundBone " + BoneName + " : Event Group content has been found");
                    Console.WriteLine();
                    XDocument SoundBone = XDocument.Load(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + ".xml");
                    var PotentialTarget = SoundBone.Descendants("eventgroup");
                    foreach (var Element in PotentialTarget)
                    {
                        string Y = PotentialTarget.FirstOrDefault().Value;
                        Console.ForegroundColor = ConsoleColor.Green;
                        RunGenerator(ChildElement, "eventgroup", BoneName, 3);
                    }
                    break;
                }
            }

            //-------------------------------------------------------------------------------
            // LOAD ALL CHILDREN NAMES

            GenerationCleanup(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-2.xml", BoneName);
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
            XDocument SoundBone = XDocument.Load(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-2.xml");
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
                            ToReplace.ReplaceWith(ParentTargetContent);
                            SoundBone.Save(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-2.xml");
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
                            ToReplace.ReplaceWith(ParentTargetContent);
                            SoundBone.Save(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-2.xml");
                            break;
                        }
                        Console.WriteLine("no Match Detected - OOF!");
                    }
                    break;
                case 3:
                    foreach (var Element in PotentialTarget)
                    //EVENTGROUP
                    {
                        string TestBelonging = PotentialTarget.Descendants("name").FirstOrDefault().Value;
                        string BoneID;
                        BoneID = BoneName.Replace("frpg_", "");
                        BoneID = BoneID.Replace("fdlc_", "");
                        if (IsChildValid(TestBelonging, BoneID) == true)                            
                        {
                            XElement ToReplace = Element;
                            ToReplace.ReplaceWith(ParentTargetContent);
                            SoundBone.Save(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-2.xml");
                            break;
                        }
                        Console.WriteLine("no Match Detected - OOF!");
                    }
                    break;
            } 
        }

        public static void GenerationCleanup(string fileName, string BoneName)
        {
            //-------------------------------------------------------------------------------//
            //                          Cleanup Sound Def Folder                             //
            //-------------------------------------------------------------------------------//
                
            var lines = File.ReadAllLines(fileName);
            string TextCache = File.ReadAllText(fileName);
            TextCache = TextCache.Replace("/SoundSouls/NPC", "");                                                                               
            //NPC CLEANUP
            TextCache = TextCache.Replace("/SoundSouls/Global", "");    
            //GLOBAL EVENTS CLEANUP
            TextCache = TextCache.Replace("/SoundSouls/GameEvents", "");
            //GAME EVENTS CLEANUP
            TextCache = TextCache.Replace("/fdlc_smain/Player_Carving", "");
            //PLAYER CARVINGS CLEANUP
            TextCache = TextCache.Replace("/SoundSouls/World/World_Interactable", "");
            //SM CLEANUP
            TextCache = TextCache.Replace("/SoundSouls/World/World_Environment", "");
            //M CLEANUP

            //
            //New cleanup entries go here
            //
            File.WriteAllText(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\frpg_sm10cache.xml", TextCache);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Sound Def Folder Clean-Up Complete");

            //-------------------------------------------------------------------------------//
            //                            Cleanup Event Group                                //
            //-------------------------------------------------------------------------------//

            XDocument SoundBone = XDocument.Load(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\frpg_sm10cache.xml");
            SoundBone.Declaration = null;
            var EventGroupFolderAll = SoundBone.Descendants("eventgroup");
            var Events = EventGroupFolderAll.Descendants("event");
            foreach(var Event in Events)
            {
                var Type = Event.Descendants("category").FirstOrDefault();
                var TestType = Type.Value;
                if (IsMusic(TestType) == true)
                {
                    Type.Value = "music";
                }
                if (IsSE(TestType) == true)
                {
                    Type.Value = "SE";
                }
                if (IsGhost(TestType)==true)
                {
                    Type.Value = "Ghost";
                }
                if(IsVoice(TestType)==true)
                {
                    Type.Value = "Voice";
                }
                if(IsDefault(TestType)==true)
                {
                    Type.Value = "Default";
                }
            }
            Console.WriteLine("Event Group Clean-Up Complete");
            Console.WriteLine();
            SoundBone.Save(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\frpg_sm10-Regenerated.xml");
            var FinalCleanup = File.ReadAllLines(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\frpg_sm10-Regenerated.xml");
            File.WriteAllLines(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\frpg_sm10-Regenerated.xml", FinalCleanup.Skip(1).ToArray());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cleanup Complete");

        }
    }
}

