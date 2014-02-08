using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.FaceTracking;

namespace Hackathon
{
    class FacialModel
    {
        public EnumIndexableCollection<FeaturePoint, Vector3DF> Features { get; private set; }
        private static float threshold = 0.01f;

        public FacialModel(EnumIndexableCollection<FeaturePoint, Vector3DF> features)
        {
            this.Features = features;
        }

        public FacialModel(string file)
        {
            Features = ReadDataFromFile(file);
        }

        public void Write(string file)
        {
            XElement root = new XElement("Points", new XAttribute("Elements", Features.Count));

            for (int i = 0; i < Features.Count; i++)
            {
                Vector3DF temp = Features[i];
                if (temp != null)
                    root.Add(new XElement("Vector",
                        new XAttribute("Index", i),
                        new XAttribute("XValue", temp.X),
                        new XAttribute("YValue", temp.Y),
                        new XAttribute("ZValue", temp.Z)));
            }

            using (StreamWriter writer = new StreamWriter(File.Open(file, FileMode.OpenOrCreate)))
                writer.Write(root.ToString());
        }

        private EnumIndexableCollection<FeaturePoint, Vector3DF> ReadDataFromFile(string file)
        {
            using (StreamReader reader = File.OpenText(file))
            {
                XDocument doc = XDocument.Parse(reader.ReadToEnd());
                XElement root = doc.Root;

                int numElements = int.Parse(root.Attribute("Elements").Value);

                Vector3DF[] vectors = new Vector3DF[numElements];

                foreach (XElement el in root.Elements())
                {
                    int index = int.Parse(el.Attribute("Index").Value);
                    float x = float.Parse(el.Attribute("XValue").Value);
                    float y = float.Parse(el.Attribute("YValue").Value);
                    float z = float.Parse(el.Attribute("ZValue").Value);

                    vectors[index] = new Vector3DF(x, y, z);
                }

                return new EnumIndexableCollection<FeaturePoint, Vector3DF>(vectors);
            }
        }

        public float match(FeaturePoint feature, Vector3DF feature3df)
        {
            Vector3DF reference = Features[feature];
            double dx = (double) (feature3df.X - reference.X);
            double dy = (double)(feature3df.Y - reference.Y);
            double dz = (double)(feature3df.Z - reference.Z);

            double d_2 = Math.Pow(dx, 2) + Math.Pow(dy, 2) + Math.Pow(dz, 2);
            return (float) d_2;
        }
    }
}
