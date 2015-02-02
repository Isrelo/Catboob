using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Catboob_GGStream
{
    public class Action
    {
        private String imageSource;
        private String shortCut;
        private String moreImageSource;

        public String ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }

        public String ShortCut
        {
            get { return shortCut; }
            set { shortCut = value; }
        }

        public String MoreImageSource
        {
            get { return moreImageSource; }
            set { moreImageSource = value; }
        }
    }
}
