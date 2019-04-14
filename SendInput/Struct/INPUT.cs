using System.Runtime.InteropServices;

namespace SendInput.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        internal uint Type;
        internal MOUSEKEYBDHARDWAREINPUT Data;
    }

}
