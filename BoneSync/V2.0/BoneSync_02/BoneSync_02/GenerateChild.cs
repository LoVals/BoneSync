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
            //var EventGroupNugget = XParent.Element("sounddeffolder");//Selects the first element with this value - works for eventcategory but not for others. WHY?
            //XAttribute TargetElement = EventGroupNugget.Attribute(BoneName);//XParent.Elements("sounddeffolder").Where(child => child.Attribute("name").Value == BoneName).First();
            var SounddefFolderAll = ParentFile.Descendants("sounddeffolder");
            foreach (var ChildElement in SounddefFolderAll)
            {
                Console.WriteLine("Chicking if this element belongs to current Bone...");
                Console.WriteLine();
                Console.WriteLine(ChildElement.Descendants("name").FirstOrDefault());
                var TestBelonging = ChildElement.Descendants("name").FirstOrDefault();
                IsChildValid(TestBelonging);
                Console.ReadLine();
            }

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

        public static bool IsChildValid(XElement TestedElement)
        {

            //Boolean to determine if the child is the one we're trying to generate
            //if it is - then return true - this will cause the loop to break

            //if ()
            // {
            //   return true;
            //}
            return false;
        }
    }
}
