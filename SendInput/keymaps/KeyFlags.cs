using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendInput.keymaps
{
    public enum KeyFlags
    {
        [Description("If specified, the scan code was preceded by a prefix byte that has the value 0xE0 (224).")]
        KEYEVENTF_EXTENDEDKEY = 0x0001,

        [Description("If specified, the key is being released. If not specified, the key is being pressed.")]
        KEYEVENTF_KEYUP = 0x0002,

        [Description("If specified, wScan identifies the key and wVk is ignored.")]
        KEYEVENTF_UNICODE = 0x0004,

        [Description("If specified, the system synthesizes a VK_PACKET keystroke. The wVk parameter must be zero. This flag can only be combined with the KEYEVENTF_KEYUP flag. For more information, see the Remarks section.")]
        KEYEVENTF_SCANCODE = 0x0008,

    }
}
