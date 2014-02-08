using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceTrackingBasics
{
    internal class FacialModelCreatedEventArgs
    {
        public FacialModel FacialModel {get; private set;}

        public FacialModelCreatedEventArgs(FacialModel model)
        {
            FacialModel = model;
        }
    }
}
