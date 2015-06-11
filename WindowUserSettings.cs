using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace CatboobGGStream
{
    public class WindowUserSettings
    {
        public double WindowTop;
        public double WindowLeft;
        public double WindowHeight;
        public double WindowWidth;
        public WindowState WindowState;

        public WindowUserSettings()
        {
            LoadWindow();

            SizeWindowToFit();

            MoveWindowIntoView();
        }

        public void SaveWindow()
        {
            if (WindowState != WindowState.Minimized)
            {
                Properties.Settings.Default.OverlayWindowTop = WindowTop;
                Properties.Settings.Default.OverlayWindowLeft = WindowLeft;
                Properties.Settings.Default.OverlayWindowHeight = WindowHeight;
                Properties.Settings.Default.OverlayWindowWidth = WindowWidth;
                Properties.Settings.Default.OverlayWindowState = WindowState;

                Properties.Settings.Default.Save();
            }
        }

        private void LoadWindow()
        {
            WindowTop = Properties.Settings.Default.OverlayWindowTop;
            WindowLeft = Properties.Settings.Default.OverlayWindowLeft;
            WindowHeight = Properties.Settings.Default.OverlayWindowHeight;
            WindowWidth = Properties.Settings.Default.OverlayWindowWidth;
            WindowState = Properties.Settings.Default.OverlayWindowState;
        }

        private void SizeWindowToFit()
        {
            if (WindowHeight > SystemParameters.VirtualScreenHeight)
                WindowHeight = SystemParameters.VirtualScreenHeight;

            if (WindowWidth > SystemParameters.VirtualScreenWidth)
                WindowWidth = SystemParameters.VirtualScreenWidth;
        }

        private void MoveWindowIntoView()
        {
            if((WindowTop + WindowHeight / 2) > SystemParameters.VirtualScreenHeight)
                WindowTop = SystemParameters.VirtualScreenHeight - WindowHeight;

            if ((WindowLeft + WindowWidth / 2) > SystemParameters.VirtualScreenWidth)
                WindowLeft = SystemParameters.VirtualScreenWidth - WindowWidth;

            if (WindowTop < 0)
                WindowTop = 0;

            if (WindowLeft < 0)
                WindowLeft = 0;
        }
    }
}
