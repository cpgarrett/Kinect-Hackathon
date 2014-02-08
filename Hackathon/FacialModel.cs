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
        public string Name { get; private set; }

        public FacialModel(EnumIndexableCollection<FeaturePoint, Vector3DF> features)
        {
            this.Features = features;
        }

        public FacialModel(EnumIndexableCollection<FeaturePoint, Vector3DF> features, string name)
        {
            this.Features = features;
            this.Name = name;
        } 

        public FacialModel(string file, string name)
        {
            Features = ReadDataFromFile(file);
            this.Name = name;
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

        public bool match(FeaturePoint feature, Vector3DF feature3df)
        {
            Vector3DF reference = Features[feature];
            double dx = (double)(feature3df.X - reference.X);
            double dy = (double)(feature3df.Y - reference.Y);
            double dz = (double)(feature3df.Z - reference.Z);

            return ((dx <= threshold) && (dy <= threshold) && (dz <= threshold));
        }
        public static bool operator ==(FacialModel lhs, FacialModel rhs)
        {
            if (!lhs.match(FeaturePoint.RightOfRightEyebrow, rhs.Features[FeaturePoint.RightOfRightEyebrow]))
                return false;
            if (!lhs.match(FeaturePoint.MiddleTopOfRightEyebrow, rhs.Features[FeaturePoint.MiddleTopOfRightEyebrow]))
                return false;
            if (!lhs.match(FeaturePoint.LeftOfRightEyebrow, rhs.Features[FeaturePoint.LeftOfRightEyebrow]))
                return false;
            if (!lhs.match(FeaturePoint.MiddleBottomOfRightEyebrow, rhs.Features[FeaturePoint.MiddleBottomOfRightEyebrow]))
                return false;

            if (!lhs.match(FeaturePoint.OuterCornerOfRightEye, rhs.Features[FeaturePoint.OuterCornerOfRightEye]))
                return false;
            if (!lhs.match(FeaturePoint.MiddleTopRightEyelid, rhs.Features[FeaturePoint.MiddleTopRightEyelid]))
                return false;
            if (!lhs.match(FeaturePoint.MiddleBottomRightEyelid, rhs.Features[FeaturePoint.MiddleBottomRightEyelid]))
                return false;
            if (!lhs.match(FeaturePoint.InnerCornerRightEye, rhs.Features[FeaturePoint.InnerCornerRightEye]))
                return false;

            if (!lhs.match(FeaturePoint.OutsideRightCornerMouth, rhs.Features[FeaturePoint.OutsideRightCornerMouth]))
                return false;
            if (!lhs.match(FeaturePoint.OutsideLeftCornerMouth, rhs.Features[FeaturePoint.OutsideLeftCornerMouth]))
                return false;
            if (!lhs.match(FeaturePoint.RightTopDipUpperLip, rhs.Features[FeaturePoint.RightTopDipUpperLip]))
                return false;
            if (!lhs.match(FeaturePoint.LeftTopDipUpperLip, rhs.Features[FeaturePoint.LeftTopDipUpperLip]))
                return false;

            if (!lhs.match(FeaturePoint.RightOfLeftEyebrow, rhs.Features[FeaturePoint.RightOfLeftEyebrow]))
                return false;
            if (!lhs.match(FeaturePoint.MiddleTopOfLeftEyebrow, rhs.Features[FeaturePoint.MiddleTopOfLeftEyebrow]))
                return false;
            if (!lhs.match(FeaturePoint.LeftOfLeftEyebrow, rhs.Features[FeaturePoint.LeftOfLeftEyebrow]))
                return false;
            if (!lhs.match(FeaturePoint.MiddleBottomOfLeftEyebrow, rhs.Features[FeaturePoint.MiddleBottomOfLeftEyebrow]))
                return false;

            if (!lhs.match(FeaturePoint.OuterCornerOfLeftEye, rhs.Features[FeaturePoint.OuterCornerOfLeftEye]))
                return false;
            if (!lhs.match(FeaturePoint.MiddleTopLeftEyelid, rhs.Features[FeaturePoint.MiddleTopLeftEyelid]))
                return false;
            if (!lhs.match(FeaturePoint.MiddleBottomLeftEyelid, rhs.Features[FeaturePoint.MiddleBottomLeftEyelid]))
                return false;
            if (!lhs.match(FeaturePoint.InnerCornerLeftEye, rhs.Features[FeaturePoint.InnerCornerLeftEye]))
                return false;

            if (!lhs.match(FeaturePoint.RightTopUpperLip, rhs.Features[FeaturePoint.RightTopUpperLip]))
                return false;
            if (!lhs.match(FeaturePoint.LeftTopUpperLip, rhs.Features[FeaturePoint.LeftTopUpperLip]))
                return false;
            if (!lhs.match(FeaturePoint.RightBottomUpperLip, rhs.Features[FeaturePoint.RightBottomUpperLip]))
                return false;
            if (!lhs.match(FeaturePoint.LeftBottomUpperLip, rhs.Features[FeaturePoint.LeftBottomUpperLip]))
                return false;

            if (!lhs.match(FeaturePoint.RightTopLowerLip, rhs.Features[FeaturePoint.LeftTopLowerLip]))
                return false;
            if (!lhs.match(FeaturePoint.RightBottomLowerLip, rhs.Features[FeaturePoint.RightBottomLowerLip]))
                return false;
            if (!lhs.match(FeaturePoint.LeftBottomLowerLip, rhs.Features[FeaturePoint.LeftBottomLowerLip]))
                return false;
            if (!lhs.match(FeaturePoint.MiddleBottomUpperLip, rhs.Features[FeaturePoint.MiddleBottomUpperLip]))
                return false;

            if (!lhs.match(FeaturePoint.LeftCornerMouth, rhs.Features[FeaturePoint.LeftCornerMouth]))
                return false;
            if (!lhs.match(FeaturePoint.RightCornerMouth, rhs.Features[FeaturePoint.RightCornerMouth]))
                return false;

            return true;
        }
    }
}
