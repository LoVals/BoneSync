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
            var XParent = XElement.Load(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\Parent.xml");
            string BoneName = "Child1";
            var EventGroupNugget = XParent.Element("sounddeffolder");//Selects the first element with this value - works for eventcategory but not for others. WHY?
            Console.WriteLine(EventGroupNugget);
            Console.ReadLine();

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
    }
}
