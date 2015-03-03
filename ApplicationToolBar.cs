using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Catboob_GGStream
{
    public class ApplicationToolBar
    {
        private String working_dir;
        private String title_txt;

        // Toolbar Icons
        private String menu_btn_img = "\\Images\\ic_menu_white_48dp.png";
        private String back_btn_img = "\\Images\\ic_arrow_back_white_48dp.png";
        private String more_btn_img = "\\Images\\ic_more_vert_white_48dp.png";
        private String clear_btn_img = "\\Images\\ic_clear_white_48dp.png";

        public String TitleText
        {
            get { return title_txt; }
            set { title_txt = value; }
        }

        public ApplicationToolBar(String working_dir_p)
        {
            working_dir = working_dir_p;
        }
    }
}
