using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleChromaEditor
{
    class Program
    {
        const string DLL_NAME = "CChromaEditorLibrary";

        [DllImport(DLL_NAME, CallingConvention=CallingConvention.Cdecl)]
        private static extern void PluginOpenEditorDialog(IntPtr path);

        public static void OpenEditorDialog(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                byte[] array = ASCIIEncoding.ASCII.GetBytes(fi.FullName);
                IntPtr lpData = Marshal.AllocHGlobal(array.Length);
                Marshal.Copy(array, 0, lpData, array.Length);
                PluginOpenEditorDialog(lpData);
                Marshal.FreeHGlobal(lpData);
            }
        }

        static void Main(string[] args)
        {
            OpenEditorDialog("RandomKeyboardEffect.object.gmx");

        }
    }
}
