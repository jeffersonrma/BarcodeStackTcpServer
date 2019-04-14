using SendInput.keymaps;
using SendInput.Struct;
using System;
using System.Runtime.InteropServices;

namespace SendInput
{
    public class ConvertVKey
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern short VkKeyScan(char ch);

        [DllImport("user32.dll")]
        static extern IntPtr GetMessageExtraInfo();



        public INPUT[] toArrayINPUT(string word)
        {
            INPUT[] inputs = new INPUT[word.Length * 2];
            int i = 0;
            foreach (char c in word)
            {
                if (c == '/') 
                {
                    inputs[i] = toINPUT(WindowsVirtualKey.VK_DIVIDE, KeyFlags.KEYEVENTF_EXTENDEDKEY);
                    i++;
                    inputs[i] = toINPUT(WindowsVirtualKey.VK_DIVIDE, KeyFlags.KEYEVENTF_KEYUP);
                    i++;
                }
                else
                {
                    inputs[i] = toINPUT(c, KeyFlags.KEYEVENTF_EXTENDEDKEY);
                    i++;
                    inputs[i] = toINPUT(c, KeyFlags.KEYEVENTF_KEYUP);
                    i++;
                }
                
            }


            return inputs;
        }

        public INPUT toINPUT(char key, KeyFlags flag)
        {
            return new INPUT()
            {
                Type = 1,
                Data = new MOUSEKEYBDHARDWAREINPUT()
                {
                    Keyboard = new KEYBDINPUT()
                    {
                        ExtraInfo = GetMessageExtraInfo(),
                        Flags = (uint)flag,
                        Time = 0,
                        Scan = 0,
                        Vk = (ushort)VkKeyScan(key)
                    }
                }
            };
        }

        public INPUT toINPUT(WindowsVirtualKey key, KeyFlags flag)
        {
            return new INPUT()
            {
                Type = 1,
                Data = new MOUSEKEYBDHARDWAREINPUT()
                {
                    Keyboard = new KEYBDINPUT()
                    {
                        ExtraInfo = GetMessageExtraInfo(),
                        Flags = (uint)flag,
                        Time = 0,
                        Scan = 0,
                        Vk = (ushort)key
                    }
                }
            };
        }
    }
}
