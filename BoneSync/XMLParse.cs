using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace BoneSync
{
    class XMLParse
    {

        public static void ParseXML()
        {
            XDocument xmlA = XDocument.Load(@"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs\SoundSoulsXML\TEST_A.xml");
            IEnumerable<XElement> xElements = xmlA.Descendants("guid");
            XDocument xmlB = XDocument.Load(@"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs\SoundSoulsXML\TEST_B.xml");
            IEnumerable<XElement> xElementsB = xmlB.Descendants("guid");
            Console.Clear();
            Console.WriteLine("Parsing GuIDs...");
            //search for missing GUID - this is to add any event to that is muissing on the specific child
            //might need to get the name of the child somehow: Possibly referencing it in FNC?

            //Need to isolate searches to the following folders: sounddeffolder; eventcategory

            foreach(XElement element in xElements)
            {
                Console.WriteLine("GuID in File A: "+element.Value);
            }

            foreach (XElement element in xElementsB)
            {
                Console.WriteLine("GuID in File B: " + element.Value);
            }
            Console.ReadLine();
        }
    }
}
