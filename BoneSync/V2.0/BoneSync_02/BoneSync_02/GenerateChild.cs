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
        public static void Execute(string BoneInput, string CurrentDirInput, XDocument ParentFile)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            int ConflictType = 0;
            //------------------------------------------------------------------------------
            // LOAD PARENT FILE INTO MEMORY
            string BoneName = BoneInput;
            string BuildDir = CurrentDirInput+ @"\XML\Bones\";
            string CacheDir = CurrentDirInput + @"\XML\RegeneratedFiles\Cache\";
            string RegenerationDir = CurrentDirInput + @"\XML\RegeneratedFiles\";
            Console.WriteLine("Directories Set");
            var BoneCache = Directory.GetFiles(CacheDir, "*.xml", SearchOption.TopDirectoryOnly);
            Console.WriteLine("Clearing Cache");
            foreach (var file in BoneCache)
            {
                File.Delete(file);
            }
            Console.WriteLine("Generating cache file...");
            File.Copy(BuildDir + BoneName+".xml", CacheDir + BoneName + "-2.xml");
            //XDocument Backup = XDocument.Load(BuildDir + BoneName + "-2.xml");
            //Backup.Save(CacheDir + "BoneBackup.xml");


            //-------------------------------------------------------------------------------//
            //                             Sound Def Folder                                  //
            //-------------------------------------------------------------------------------//
            Console.ForegroundColor = ConsoleColor.Yellow;
            var SounddefFolderAll = ParentFile.Descendants("sounddeffolder");
            Console.WriteLine("Checking Sound Def Folder");
            string BoneID = BoneName;
            switch (DetectConflicts(BoneName))
            {
                case 2:
                    BoneID = "Player_Dlc_Main";
                    ConflictType = 2;
                    break;
                case 3:
                    BoneID = "Player_Dlc_Smain";
                    ConflictType = 3;
                    break;
                case 4:
                    BoneID = "Player_Main";
                    ConflictType = 4;
                    break;
                case 5:
                    BoneID = "Player_Smain";
                    ConflictType = 5;
                    break;
            }
            foreach (var ChildElement in SounddefFolderAll)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value; //in the case of the dlc main the BoneName needs to be overwritten to ---> Player_Main_Dlc
                if (IsChildValid(TestBelonging, BoneID) == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("SoundBone " + BoneName + ": Sound Def content has been found!");
                    Console.WriteLine();
                    RunGenerator(ChildElement, "sounddeffolder", BoneName, 1, ConflictType, CacheDir);
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
                    RunGenerator(ChildElement, "soundbank", BoneName, 2, ConflictType, CacheDir);
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
            switch (DetectConflicts(BoneID))
            {
                case 1:
                    BoneID = BoneID + " - DLC";
                    ConflictType = 1;
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
                            XDocument SoundBone = XDocument.Load(BuildDir + BoneName + ".xml");
                            var PotentialTarget = SoundBone.Descendants("eventgroup");
                            foreach (var Element in PotentialTarget)
                            {
                                string Y = PotentialTarget.FirstOrDefault().Value;
                                Console.ForegroundColor = ConsoleColor.Green;
                                RunGenerator(ChildElement, "eventgroup", BoneName, 3, ConflictType, CacheDir);
                            }
                            break;
                        }
                    }
                    break;
                case 2:
                    ConflictType = 2;
                    foreach (var ChildElement in EventGroupFolderAll)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value;
                        Console.WriteLine(TestBelonging);
                        if (IsChildValid(TestBelonging, BoneID) == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("SoundBone " + BoneName + " : Event Group content has been found");
                            Console.WriteLine();
                            XDocument SoundBone = XDocument.Load(BuildDir + BoneName + ".xml");
                            var PotentialTarget = SoundBone.Descendants("eventgroup");
                            foreach (var Element in PotentialTarget)
                            {
                                string Y = PotentialTarget.FirstOrDefault().Value;
                                Console.ForegroundColor = ConsoleColor.Green;
                                RunGenerator(ChildElement, "eventgroup", BoneName, 3, ConflictType, CacheDir);
                            }
                            break;
                        }
                    }
                    break;
                case 3:
                    ConflictType = 3;
                    foreach (var ChildElement in EventGroupFolderAll)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value;
                        if (IsChildValid(TestBelonging, BoneID) == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("SoundBone " + BoneName + " : Event Group content has been found");
                            Console.WriteLine();
                            XDocument SoundBone = XDocument.Load(BuildDir + BoneName + ".xml");
                            var PotentialTarget = SoundBone.Descendants("eventgroup");
                            foreach (var Element in PotentialTarget)
                            {
                                string Y = PotentialTarget.FirstOrDefault().Value;
                                Console.ForegroundColor = ConsoleColor.Green;
                                RunGenerator(ChildElement, "eventgroup", BoneName, 3, ConflictType, CacheDir);
                            }
                            break;
                        }
                    }
                    break;
                case 4:
                    ConflictType = 4;
                    BoneID = "frpg_main";
                    foreach (var ChildElement in EventGroupFolderAll)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value;
                        if (IsChildValid(TestBelonging, BoneID) == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("SoundBone " + BoneName + " : Event Group content has been found");
                            Console.WriteLine();
                            XDocument SoundBone = XDocument.Load(BuildDir + BoneName + ".xml");
                            var PotentialTarget = SoundBone.Descendants("eventgroup");
                            foreach (var Element in PotentialTarget)
                            {
                                string Y = PotentialTarget.FirstOrDefault().Value;
                                Console.ForegroundColor = ConsoleColor.Green;
                                RunGenerator(ChildElement, "eventgroup", BoneName, 3, ConflictType, CacheDir);
                            }
                            break;
                        }
                    }
                    break;
                case 5:
                    ConflictType = 5;
                    BoneID = "frpg_smain";
                    foreach (var ChildElement in EventGroupFolderAll)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value;
                        Console.WriteLine(TestBelonging);
                        if (IsChildValid(TestBelonging, BoneID) == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("SoundBone " + BoneName + " : Event Group content has been found");
                            Console.WriteLine();
                            XDocument SoundBone = XDocument.Load(BuildDir + BoneName + ".xml");
                            var PotentialTarget = SoundBone.Descendants("eventgroup");
                            foreach (var Element in PotentialTarget)
                            {
                                string Y = PotentialTarget.FirstOrDefault().Value;
                                Console.ForegroundColor = ConsoleColor.Green;
                                RunGenerator(ChildElement, "eventgroup", BoneName, 3, ConflictType, CacheDir);
                            }
                            break;
                        }
                    }
                    break;
                default:
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
                            XDocument SoundBone = XDocument.Load(BuildDir + BoneName + ".xml");
                            var PotentialTarget = SoundBone.Descendants("eventgroup");
                            foreach (var Element in PotentialTarget)
                            {
                                string Y = PotentialTarget.FirstOrDefault().Value;
                                Console.ForegroundColor = ConsoleColor.Green;
                                RunGenerator(ChildElement, "eventgroup", BoneName, 3, ConflictType, CacheDir);
                            }
                            break;
                        }
                    }
                    break;
            }
            //Need another solution for fdlc_main as there are conflicts in the main project folder
            //the easiest solution would be to fix the master project to some degree and separate the events from fdlc_smain and fdlc_smain in the eventgroup section
            //this could also apply to frpg_main and frpg_smain


            //------------------------------------------------------------------------------
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Beginning Generation Clean-up");
            GenerationCleanup(CacheDir + BoneName + "-2.xml", BoneName, ConflictType, CacheDir, RegenerationDir);
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

        public static void RunGenerator(XElement ParentTargetContent, string ReplacementTarget, string BoneName, int DataType, int Conflict, string CacheDirInput)
        //DataType: 1 = Sound Def // 2 = SoundBank // 3 = Eventgroup
        {
            string BuildDir = CacheDirInput;
            // select node from one doc
            XDocument SoundBone = XDocument.Load(BuildDir + BoneName + "-2.xml");
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
                                ReplacementeElement = "fdlc_main";
                                var master = PotentialTarget.Descendants(ReplaceMe).FirstOrDefault();
                                var fdlc_main = master.Descendants(ReplaceMe).FirstOrDefault();
                                TestBelonging = fdlc_main.Descendants(ReplaceMe).FirstOrDefault().Value;
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Exception 2 detected");
                                Console.ForegroundColor = ConsoleColor.Green;
                                if ((IsChildValid(TestBelonging, ReplacementeElement) == true)|(IsChildValid(TestBelonging, "Player_Dlc_Main")==true))
                                {
                                    XElement LevelA = Element.Descendants(ReplaceMe).FirstOrDefault();
                                    XElement LevelB = LevelA.Descendants(ReplaceMe).FirstOrDefault();
                                    XElement ToReplace = LevelB.Descendants(ReplaceMe).FirstOrDefault();
                                    ToReplace.ReplaceWith(ParentTargetContent);
                                    SoundBone.Save(BuildDir + BoneName + "-2.xml");
                                    goto LoopOut;
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR - no Match Detected - See Log for Details");
                                LogWriter.WriteError("ERROR - no match Detected for "+BoneName+" on Sound Definition");
                                break;
                            case 3: //fdlc_smain
                                ReplacementeElement = "fdlc_smain";                               
                                TestBelonging = PotentialTarget.Descendants(ReplaceMe).FirstOrDefault().Value;
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Exception 3 detected");
                                Console.ForegroundColor = ConsoleColor.Green;
                                if ((IsChildValid(TestBelonging, ReplacementeElement) == true) | (IsChildValid(TestBelonging, "Player_Dlc_Smain") == true))
                                {
                                    XElement ToReplace = Element.Descendants(ReplaceMe).FirstOrDefault();
                                    ToReplace.ReplaceWith(ParentTargetContent);
                                    SoundBone.Save(BuildDir + BoneName + "-2.xml");
                                    goto LoopOut;   
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR - no Match Detected - See Log for Details");
                                LogWriter.WriteError("ERROR - no match Detected for " + BoneName + " on Sound Definition");
                                break;
                            case 4: //FRPG_MAIN
                                TestBelonging = PotentialTarget.Descendants(ReplaceMe).FirstOrDefault().Value;
                                if (IsChildValid(TestBelonging, ReplacementeElement) == true)
                                {                                    
                                    XElement ToReplace = Element.Descendants(ReplaceMe).FirstOrDefault();
                                    ToReplace.ReplaceWith(ParentTargetContent);
                                    SoundBone.Save(BuildDir + BoneName + "-2.xml");
                                    goto LoopOut;
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR - no Match Detected - See Log for Details");
                                LogWriter.WriteError("ERROR - no match Detected for " + BoneName + " on Sound Definition");
                                break;
                            case 5: //FRPG_SMAIN
                                TestBelonging = PotentialTarget.Descendants(ReplaceMe).FirstOrDefault().Value;
                                if (IsChildValid(TestBelonging, ReplacementeElement) == true)
                                {
                                    XElement ToReplace = Element.Descendants(ReplaceMe).FirstOrDefault();
                                    ToReplace.ReplaceWith(ParentTargetContent);
                                    SoundBone.Save(BuildDir + BoneName + "-2.xml");
                                    goto LoopOut;
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR - no Match Detected - See Log for Details");
                                LogWriter.WriteError("ERROR - no match Detected for " + BoneName + " on Sound Definition");
                                break;
                            default: //unregistered cases
                                TestBelonging = PotentialTarget.Descendants(ReplaceMe).FirstOrDefault().Value;
                                if (IsChildValid(TestBelonging, ReplacementeElement) == true)
                                {
                                    XElement ToReplace = Element.Descendants(ReplaceMe).FirstOrDefault();
                                    ToReplace.ReplaceWith(ParentTargetContent);
                                    SoundBone.Save(BuildDir + BoneName + "-2.xml");
                                    break;
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR - no Match Detected - See Log for Details");
                                LogWriter.WriteError("ERROR - no match Detected for " + BoneName + " on Sound Definition");
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
                            SoundBone.Save(BuildDir + BoneName + "-2.xml");
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR - no Match Detected - See Log for Details");
                        LogWriter.WriteError("ERROR - no match Detected for " + BoneName + " on SoundBanks");
                    }
                    break;
                case 3:
                    switch (Conflict)
                    {
                        case 2:
                            foreach (var Element in PotentialTarget)
                            //EVENTGROUP FDLC_MAIN
                            {
                                string TestBelonging = PotentialTarget.Descendants("name").FirstOrDefault().Value;
                                string BoneID;
                                BoneID = BoneName;
                                if (IsChildValid(TestBelonging, BoneID) == true)
                                {
                                    XElement ToReplace = Element;
                                    ToReplace.ReplaceWith(ParentTargetContent);
                                    SoundBone.Save(BuildDir + BoneName + "-2.xml");
                                    break;
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR - no Match Detected - See Log for Details");
                                LogWriter.WriteError("ERROR - no match Detected for " + BoneName + " on SoundBanks");
                            }
                            break;
                        case 3:
                            foreach (var Element in PotentialTarget)
                            //EVENTGROUP FDLC_SMAIN
                            {
                                string TestBelonging = PotentialTarget.Descendants("name").FirstOrDefault().Value;
                                string BoneID;
                                BoneID = BoneName;
                                if (IsChildValid(TestBelonging, BoneID) == true)
                                {
                                    XElement ToReplace = Element;
                                    ToReplace.ReplaceWith(ParentTargetContent);
                                    SoundBone.Save(BuildDir + BoneName + "-2.xml");
                                    break;
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR - no Match Detected - See Log for Details");
                                LogWriter.WriteError("ERROR - no match Detected for " + BoneName + " on SoundBanks");
                            }
                            break;
                        case 4:
                            foreach (var Element in PotentialTarget)
                    //EVENTGROUP FRPG_MAIN
                    {
                        string TestBelonging = PotentialTarget.Descendants("name").FirstOrDefault().Value;
                        string BoneID;
                        BoneID = BoneName;                   
                        if (IsChildValid(TestBelonging, BoneID) == true)                            
                        {
                            XElement ToReplace = Element;
                            ToReplace.ReplaceWith(ParentTargetContent);
                            SoundBone.Save(BuildDir + BoneName + "-2.xml");
                            break;
                        }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR - no Match Detected - See Log for Details");
                                LogWriter.WriteError("ERROR - no match Detected for " + BoneName + " on EventGroup");
                            }
                            break;
                        case 5:
                            foreach (var Element in PotentialTarget)
                            //EVENTGROUP FRPG_SMAIN
                            {
                                string TestBelonging = PotentialTarget.Descendants("name").FirstOrDefault().Value;
                                string BoneID;
                                BoneID = BoneName;
                                if (IsChildValid(TestBelonging, BoneID) == true)
                                {
                                    XElement ToReplace = Element;
                                    ToReplace.ReplaceWith(ParentTargetContent);
                                    SoundBone.Save(BuildDir + BoneName + "-2.xml");
                                    break;
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR - no Match Detected - See Log for Details");
                                LogWriter.WriteError("ERROR - no match Detected for " + BoneName + " on EventGroup");
                            }
                            break;

                        default:
                            foreach (var Element in PotentialTarget)
                            //EVENTGROUP_DEFAULT
                            {
                                string TestBelonging = PotentialTarget.Descendants("name").FirstOrDefault().Value;
                                string BoneID;
                                BoneID = BoneName.Replace("frpg_", "");
                                BoneID = BoneID.Replace("fdlc_", "");
                                if (IsChildValid(TestBelonging, BoneID) == true)
                                {
                                    XElement ToReplace = Element;
                                    ToReplace.ReplaceWith(ParentTargetContent);
                                    SoundBone.Save(BuildDir + BoneName + "-2.xml");
                                    break;
                                }
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR - no Match Detected - See Log for Details");
                                LogWriter.WriteError("ERROR - no match Detected for " + BoneName + " on EventGroup");
                            }
                            break;
                    }
                    break;
            } 
        }

        public static void GenerationCleanup(string fileName, string BoneName, int exceptiontype, string CacheDirInput, string RegeneratedDirInput)
        {
            string BuildDir = CacheDirInput;
            string RegenDir = RegeneratedDirInput;
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
            switch (exceptiontype)
            {
                case 2:
                    TextCache = TextCache.Replace("/SoundSouls/Player", "fdlc_main");
                    break;
                case 4:
                    TextCache = TextCache.Replace("/SoundSouls/Player", "frpg_main");
                    TextCache = TextCache.Replace("frpg_main-Player_Main-Player", "");
                    TextCache = TextCache.Replace("-Player_Souls-", "");
                    TextCache = TextCache.Replace("-Player_Movement-", "");
                    TextCache = TextCache.Replace("-Player_Actions-", "");
                    TextCache = TextCache.Replace("-Player_Spells-", "");
                    TextCache = TextCache.Replace("-Player_States-", "");
                    TextCache = TextCache.Replace("-Player_Damage-", "");
                    TextCache = TextCache.Replace("-SpiritForm-", "");
                    TextCache = TextCache.Replace("-Host-", "");
                    TextCache = TextCache.Replace("-Player_Voice-", "");
                    TextCache = TextCache.Replace("-Player_Combat-Weapons_Physics-", "");
                    //TextCache = TextCache.Replace("Player_Main", "frpg_main");
                    //It should build anyway with the slug in it. I doubt the user will go in the pre-existing soundbones anyway
                    //RIGHT?!

                    break;
                case 5:
                    TextCache = TextCache.Replace("-SoundSouls-Player-Player_Smain-Global-", "");
                    TextCache = TextCache.Replace("Music-","");
                    TextCache = TextCache.Replace("Environment-Bonefire-", "");
                    TextCache = TextCache.Replace("EnemyDeaths-", "");
                    TextCache = TextCache.Replace("-SoundSouls-Player-Player_Smain-Player-", "");
                    TextCache = TextCache.Replace("Player_Smain", "frpg_smain");
                    break;
            }
            
            //FDLC SMain CLEANUP

            //
            //New cleanup entries go here
            //
            File.WriteAllText(BuildDir+ BoneName+"cache.xml", TextCache);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Sound Def Folder Clean-Up Complete");

            //-------------------------------------------------------------------------------//
            //                            Cleanup Event Group                                //
            //-------------------------------------------------------------------------------//

            XDocument SoundBone = XDocument.Load(BuildDir + BoneName+"cache.xml");
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
            SoundBone.Save(RegenDir + BoneName + ".xml");
            var FinalCleanup = File.ReadAllLines(RegenDir + BoneName + ".xml"); 
            File.WriteAllLines(RegenDir + BoneName + ".xml", FinalCleanup.Skip(1).ToArray());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cleanup Complete");
            Console.WriteLine();

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
                var smainConfirm = IsException.match_main(BoneName, "fdlc_smain", 1);
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
                else if (smainConfirm == 0)
                {
                    // this exception is for fdlc_smain
                    return 3;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                diff_match_patch IsException = new diff_match_patch();
                IsException.Match_Threshold = 0.03f;
                var mainConfirm = IsException.match_main(BoneName, "frpg_main", 1);
                var smainConfirm = IsException.match_main(BoneName, "frpg_smain", 1);
                if (mainConfirm == 0)
                {
                    return 4; //FRPG_MAIN
                }
                else if(smainConfirm == 0)
                {
                    return 5; //FRPG_SMAIN
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}

