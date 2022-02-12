using System.Runtime.InteropServices;
using Material.WindowStyle.Demo.Interop.Structs;

namespace Material.WindowStyle.Demo.Interop
{
    // http://pinvoke.net/
    public static class WinApiMethods
    {
        private const string WinUser = "user32.dll";
        
        [DllImport(WinUser)]
        public static extern bool GetCursorPos(out WinApiPoint point);
        
        [DllImport(WinUser)]
        public static extern uint TrackPopupMenuEx(IntPtr hMenu, uint fuFlags, int x, int y, IntPtr hWnd, IntPtr lpTpm);
        
        [DllImport(WinUser)]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport(WinUser, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
    }
}