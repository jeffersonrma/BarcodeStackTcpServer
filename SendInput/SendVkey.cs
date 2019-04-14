using SendInput.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SendInput
{
    public class SendVkey
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] pInputs, int cbSize);

        public uint SendPressKey(INPUT[] inputs)
        {
            return SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(inputs[0]));
        }

        public uint SendPressKey(string keys)
        {
            INPUT[] inputs = new ConvertVKey().toArrayINPUT(keys);
            return SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(inputs[0]));
        }

        public uint SendPressKey(keymaps.WindowsVirtualKey key)
        {
            INPUT[] inputs =
            { 
                new ConvertVKey().toINPUT(key, keymaps.KeyFlags.KEYEVENTF_EXTENDEDKEY),
                new ConvertVKey().toINPUT(key, keymaps.KeyFlags.KEYEVENTF_KEYUP)
            };
            return SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(inputs[0]));
        }

        public uint SendPressKey(char key)
        {
            INPUT[] inputs =
            { 
                new ConvertVKey().toINPUT(key, keymaps.KeyFlags.KEYEVENTF_EXTENDEDKEY),
                new ConvertVKey().toINPUT(key, keymaps.KeyFlags.KEYEVENTF_KEYUP)
            };
            return SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(inputs[0]));
        }
    }
}
