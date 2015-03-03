using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catboob_GGStream
{
    public class ActionButton
    {
        private String image_source;
        private String tool_tip;

        public String ImageSource
        {
            get { return image_source; }
            set { image_source = value; }
        }

        public String ToolTip
        {
            get { return tool_tip; }
            set { tool_tip = value; }
        }
    }
}
