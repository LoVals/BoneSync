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
            Console.WriteLine();
            int ConflictType = 0;
            //What needs to be done:

            //>>    Detect what has changed
            //>>    Regenerate the XML File

            //------------------------------------------------------------------------------
            // LOAD PARENT FILE INTO MEMORY
            File.Delete(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\fdlc_main-2.xml");
            File.Copy(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\fdlc_main.xml", @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\fdlc_main-2.xml");
            var ParentFile = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\SoundSouls.xml");
            string BoneName = "fdlc_main";

            //-------------------------------------------------------------------------------//
            //                             Sound Def Folder                                  //
            //-------------------------------------------------------------------------------//
            Console.ForegroundColor = ConsoleColor.Yellow;
            var SounddefFolderAll = ParentFile.Descendants("sounddeffolder");
            Console.WriteLine("Checking Sound Def Folder");
            string BoneID = BoneName;
            if (DetectConflicts(BoneName) == 2)
            {
                BoneID = "Player_Main_Dlc";
                ConflictType = 2;
                //Solves DLC conflicts for fdlc_sm12 and fdlc_m12
            }
            Console.WriteLine(BoneID);
            Console.ReadLine();
            foreach (var ChildElement in SounddefFolderAll)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value; //in the case of the dlc main the BoneName needs to be overwritten to ---> Player_Main_Dlc
                if (IsChildValid(TestBelonging, BoneID) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SoundBone " + BoneName + ": Sound Def content has been found!");
                    Console.WriteLine();
                    RunGenerator(ChildElement, "sounddeffolder", BoneName, 1, ConflictType);
                    break;
                }
            }
            BoneID = BoneName;
            //Resets the ID value to avoid any issues in other sections
            ConflictType = 0;
            //Resets the Conflict Value to avoid any issues in other sections

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
                    RunGenerator(ChildElement, "soundbank", BoneName, 2, ConflictType);
                    break;

                }
            }

            //-------------------------------------------------------------------------------//
            //                             Event Group                                       //
            //-------------------------------------------------------------------------------//
            Console.ForegroundColor = ConsoleColor.Yellow;
            var EventGroupFolderAll = ParentFile.Descendants("eventgroup");
            Console.WriteLine("Checking Event Group");
            //Extra bit for DLC conflict solution
            if (DetectConflicts(BoneID) == 1)
            {
                BoneID = BoneID + " - DLC";
                ConflictType = 1;
                //Solves DLC conflicts for fdlc_sm12 and fdlc_m12
            }
            //Need another solution for fdlc_main as there are conflicts in the main project folder
            //the easiest solution would be to fix the master project to some degree and separate the events from fdlc_smain and fdlc_smain in the eventgroup section
            //this could also apply to frpg_main and frpg_smain

            BoneID = BoneID.Replace("frpg_", "");
            BoneID = BoneID.Replace("fdlc_", "");
            foreach (var ChildElement in EventGroupFolderAll)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value;
                if (IsChildValid(TestBelonging, BoneID) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SoundBone " + BoneName + " : Event Group content has been found");
                    Console.WriteLine();
                    XDocument SoundBone = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + ".xml");
                    var PotentialTarget = SoundBone.Descendants("eventgroup");
                    foreach (var Element in PotentialTarget)
                    {
                        string Y = PotentialTarget.FirstOrDefault().Value;
                        Console.ForegroundColor = ConsoleColor.Green;
                        RunGenerator(ChildElement, "eventgroup", BoneName, 3, ConflictType);
                    }
                    break;
                }
            }
            //------------------------------------------------------------------------------
            GenerationCleanup(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-2.xml", BoneName);
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

        public static void RunGenerator(XElement ParentTargetContent, string ReplacementTarget, string BoneName, int DataType, int Conflict)
        //DataType: 1 = Sound Def // 2 = SoundBank
        {
            // select node from one doc
            XDocument SoundBone = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-2.xml");
            var PotentialTarget = SoundBone.Descendants(ReplacementTarget);
            string ReplaceMe = ReplacementTarget;
            string ReplacementeElement = BoneName;

            switch (DataType)
            {
                case 1:
                    foreach (var Element in PotentialTarget)
                    //SOUND DEF
                    {
                        string TestBelonging;
                        // EXEPTION HANDLER
                        switch (Conflict)
                        {
                            case 2: //fdlc_main
                                ReplacementeElement = "Player_dlcMain";
                                var master = PotentialTarget.Descendants(ReplaceMe).FirstOrDefault();
                                var fdlc_main = master.Descendants(ReplaceMe).FirstOrDefault();
                                TestBelonging = fdlc_main.Descendants(ReplaceMe).FirstOrDefault().Value;
                                Console.WriteLine("Exception 2 detected:");
                                if (IsChildValid(TestBelonging, ReplacementeElement) == true)
                                {
                                    XElement LevelA = Element.Descendants(ReplaceMe).FirstOrDefault();
                                    XElement LevelB = LevelA.Descendants(ReplaceMe).FirstOrDefault();
                                    XElement ToReplace = LevelB.Descendants(ReplaceMe).FirstOrDefault();
                                    ToReplace.ReplaceWith(ParentTargetContent);
                                    SoundBone.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-2.xml");
                                    goto LoopOut;
                                }
                                Console.WriteLine("no Match Detected - OOF!");
                                break;
                            default: //unregistered cases
                                TestBelonging = PotentialTarget.Descendants(ReplaceMe).FirstOrDefault().Value;
                                if (IsChildValid(TestBelonging, ReplacementeElement) == true)
                                {
                                    XElement ToReplace = Element.Descendants(ReplaceMe).FirstOrDefault();
                                    ToReplace.ReplaceWith(ParentTargetContent);
                                    SoundBone.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-2.xml");
                                    break;
                                }
                                Console.WriteLine("no Match Detected - OOF!");
                                break;
                        }
                    LoopOut:
                        break;
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
                            SoundBone.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-2.xml");
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
                            SoundBone.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-2.xml");
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
            TextCache = TextCache.Replace("Player_dlcMain", "Player_Main_Dlc");
            //FDLC_Main CLEANUP

            //
            //New cleanup entries go here
            //
            File.WriteAllText(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\"+BoneName+"cache.xml", TextCache);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Sound Def Folder Clean-Up Complete");

            //-------------------------------------------------------------------------------//
            //                            Cleanup Event Group                                //
            //-------------------------------------------------------------------------------//

            XDocument SoundBone = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\"+BoneName+"cache.xml");
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
            SoundBone.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-Regenerated.xml");
            var FinalCleanup = File.ReadAllLines(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-Regenerated.xml");
            File.WriteAllLines(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\" + BoneName + "-Regenerated.xml", FinalCleanup.Skip(1).ToArray());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cleanup Complete");

        }

        public static int DetectConflicts(string BoneName)
        {
            diff_match_patch IsDlc = new diff_match_patch();
            IsDlc.Match_Threshold = 0.03f;
            IsDlc.Match_Distance = 1;
            var Match = IsDlc.match_main(BoneName, "fdlc_", 0);
            if (Match == 0)
            {
                //IF it's a dlc file check if the file is M12 or SM12
                diff_match_patch IsException = new diff_match_patch();
                IsException.Match_Threshold = 0.03f;
                var sm12Confirm = IsException.match_main(BoneName, "fdlc_sm12", 1);
                var m12Confirm = IsException.match_main(BoneName, "fdlc_m12", 1);
                var mainConfirm = IsException.match_main(BoneName, "fdlc_main", 1);
                if (m12Confirm == 0|sm12Confirm ==0)
                {
                    // this exception is either fdlc_sm12 - DLC or fdlc_m12 - DLC
                    return 1;
                }
                else if (mainConfirm == 0)
                {
                    // this exception is fdlc_main
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}

