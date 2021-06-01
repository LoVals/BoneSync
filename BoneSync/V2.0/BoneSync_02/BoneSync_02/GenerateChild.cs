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
            var XParent = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\Parent.Xml");
            string BoneName = "Child1";//Path.GetFileNameWithoutExtension(file);
            IEnumerable<XElement> EventGroup = from el in XParent.Elements("eventgroup") where (string)el.Element("name") == BoneName select el;
            foreach (XElement el in EventGroup)
            {
                Console.WriteLine(el);
            }

            // LOAD ALL CHILDREN NAMES
            string SoundBonesDir = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\FDP\Children";
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
