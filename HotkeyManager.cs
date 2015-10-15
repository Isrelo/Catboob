using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatboobGGStream
{
    public class HotkeyManager
    {
        private String keys_string;
        private List<String> pressed_keys;

        public HotkeyManager()
        {
            pressed_keys = new List<String>();
        }

        public void Reset()
        {
            pressed_keys = new List<String>();
        }

        public String GetPressedKeysString()
        {
            keys_string = "";

            for (int count = 0; count < pressed_keys.Count; count++)
            {
                String tempKey = pressed_keys[count];

                if (count > 0 && count < pressed_keys.Count)
                    keys_string = String.Format("{0} + {1}", keys_string, tempKey);
                else
                    keys_string += tempKey;
            }

            return keys_string;
        }


        public bool RemovePressedKey(String key_to_remove)
        {
            for (int count = 0; count < pressed_keys.Count; count++)
            {
                if (pressed_keys[count] == key_to_remove)
                {
                    pressed_keys.Remove(key_to_remove);
                    return true;
                }
            }

            return false;
        }

        public bool AddPressedKey(String key_to_add)
        {
            if (!pressed_keys.Contains(key_to_add))
            {
                pressed_keys.Add(key_to_add);
                return true;
            }

            return false;
        }

        public bool CheckForPressedHotkey(Dictionary<String, OverlayItem> hot_key_to_check)
        {
            if (hot_key_to_check.ContainsKey(GetPressedKeysString()))
                return true;

            return false;
        }
    }
}
