using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.FaceTracking;

namespace FaceTrackingBasics
{
    class FacialModel
    {
        private EnumIndexableCollection<FeaturePoint, Vector3DF> features;
        
        public FacialModel(EnumIndexableCollection<FeaturePoint, Vector3DF> features)
        {
            
        }

        public FacialModel(string file)
        {

        }

        public void Write(string file)
        {

        }



    }
}
