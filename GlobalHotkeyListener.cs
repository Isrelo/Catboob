using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using System.Runtime.InteropServices;

namespace CatboobGGStream
{
    public class GlobalHotkeyListener
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const uint MOD_NONE = 0x0000; //[NONE]
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS

        private int fsmodifers;
        private int current_hotkey_id;        
        private IntPtr hwnd_handle;

        public delegate void ExecuteOverlayItem(int hotkey_id);
        public ExecuteOverlayItem executeOverlayItem;

        public GlobalHotkeyListener(IntPtr hwnd_handle_p)
        {
            hwnd_handle = hwnd_handle_p;

            current_hotkey_id = 1;
        }

        public void RegisterGlobalHotkey(OverlayItem overlay_item)
        {
            int virtual_key_code = (int)MOD_NONE;
            KeyConverter key_converter = new KeyConverter();
            fsmodifers = (int)MOD_NONE;           

            string[] temp_selected_keys = overlay_item.HotKey.Split('+');
            foreach(String temp_selected_key in temp_selected_keys)
            {
                Key temp_key = (Key)key_converter.ConvertFromString(temp_selected_key);
                if(!IsModiferKey(temp_key))
                {
                    virtual_key_code |= KeyInterop.VirtualKeyFromKey(temp_key);                    
                }
            }

            RegisterHotKey(this.hwnd_handle, current_hotkey_id, (uint)fsmodifers, (uint)virtual_key_code);

            overlay_item.HotKeyID = current_hotkey_id;

            current_hotkey_id++;
        }

        public void UnRegisterGlobalHotKey(int hotkey_id)
        {
            UnregisterHotKey(this.hwnd_handle, hotkey_id);
        }

        public void UnRegisterGlobalHotkeys()
        {
            for(int count = 1; count < current_hotkey_id; count++)
            {
                UnregisterHotKey(this.hwnd_handle, count);
            }
        }

        public bool IsModiferKey(Key key_to_check_p)
        {
            // Check to seee if alt was selected.
            if (key_to_check_p == Key.RightAlt || key_to_check_p == Key.LeftAlt)
            {
                fsmodifers |= (int)MOD_ALT;
                return true;
            }

            // Check to see if ctrl was selected.
            if (key_to_check_p == Key.RightCtrl || key_to_check_p == Key.LeftCtrl)
            {
                fsmodifers |= (int)MOD_CONTROL;
                return true;
            }

            // Check to see if shift was selected.
            if (key_to_check_p == Key.RightShift || key_to_check_p == Key.LeftShift)
            {
                fsmodifers |= (int)MOD_SHIFT;
                return true;
            }

            // Check to see if windows key was selected.
            if (key_to_check_p == Key.RWin || key_to_check_p == Key.LWin)
            {
                fsmodifers |= (int)MOD_WIN;
                return true;
            }

            return false;
        }

        public IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    {
                        executeOverlayItem(wParam.ToInt32());
                        break;
                    }
            }
            return IntPtr.Zero;
        }
    }
}
