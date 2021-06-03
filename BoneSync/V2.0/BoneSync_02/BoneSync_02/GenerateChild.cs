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
            //What needs to be done:

            //>>    Detect what has changed
            //>>    Regenerate the XML File

            //------------------------------------------------------------------------------
            // LOAD PARENT FILE INTO MEMORY
            var XParent = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\Parent.xml");
            var ParentFile = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\Parent.xml");
            string BoneName = "Child1";

            //-------------------------------------------------------------------------------
            //                             Event Category
            //-------------------------------------------------------------------------------

            var EventCategoryFolderAll = ParentFile.Descendants("eventcategory");
            var MusicCategory = EventCategoryFolderAll.Descendants("name");
            foreach (var ChildElement in MusicCategory)
            {
                string TestMatch = ChildElement.Descendants("name").FirstOrDefault().Value;
                if (IsMusic(TestMatch) == true)
                {
                    Console.WriteLine("music folder found");
                    break;
                }
            }
                //-------------------------------------------------------------------------------
                //                             Sound Def Folder
                //-------------------------------------------------------------------------------

                var SounddefFolderAll = ParentFile.Descendants("sounddeffolder");
            foreach (var ChildElement in SounddefFolderAll)
            {
                Console.WriteLine("Chicking if this element belongs to current Bone...");
                Console.WriteLine();
                Console.WriteLine(ChildElement.Descendants("name").FirstOrDefault().Value);
                var TestBelonging = ChildElement.Descendants("name").FirstOrDefault().Value;
                IsChildValid(TestBelonging, BoneName);
                if (IsChildValid(TestBelonging, BoneName) == true)
                {
                    Console.WriteLine("SoundBone" + BoneName + " :content has been found");
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
    }
}
