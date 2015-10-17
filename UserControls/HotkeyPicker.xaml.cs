using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CatboobGGStream
{
    /// <summary>
    /// Interaction logic for HotkeyPicker.xaml
    /// </summary>
    public partial class HotkeyPicker : UserControl
    {
        public delegate void SavePickedHotkey(String selected_hotkey);
        public SavePickedHotkey savePickedHotkey;

        public delegate void ClosePickedHotkey();
        public ClosePickedHotkey closePickedHotkey;

        private HotkeyManager hotkey_manager;

        public HotkeyPicker()
        {
            InitializeComponent();
        }

        public void ResetHotkeyDialog()
        {
            // Setup the user selected hotkey managment.
            hotkey_manager = new HotkeyManager();

            hotkey_manager.Reset();

            // Reset the hotkey dialog.
            pressed_key_tb.Text = "";
            pressed_key_tb.Focus();
        }

        private void HotkeyDiscard_Click(object sender, RoutedEventArgs e)
        {
            closePickedHotkey();
        }

        private void HotkeySave_Click(object sender, RoutedEventArgs e)
        {
            String selected_hotkey = "";

            savePickedHotkey(selected_hotkey);
        }

        private void HotKey_KeyDown(object sender, KeyEventArgs e)
        {
            // Add pressed keys to the hotkey manager.
            hotkey_manager.AddPressedKey(e.Key.ToString());

            // Display the list of key in a hotkey.
            pressed_key_tb.Text = hotkey_manager.GetPressedKeysString();
        }

        private void HotKey_KeyUP(object sender, KeyEventArgs e)
        {
            // Remove key from hotkey manager.
            hotkey_manager.RemovePressedKey(e.Key.ToString());
        }
    }
}
