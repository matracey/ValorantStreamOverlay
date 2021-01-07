using System;
using System.Drawing.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ValorantOverlay.App.Helpers
{
    public static class FontHelpers
    {
        public static void AddFontFromResource(this PrivateFontCollection privateFontCollection, Assembly assembly, string fontResourceName)
        {
            var fontBytes = GetFontResourceBytes(assembly, fontResourceName);
            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            privateFontCollection.AddMemoryFont(fontData, fontBytes.Length);
        }

        private static byte[] GetFontResourceBytes(Assembly assembly, string fontResourceName)
        {
            var resources = assembly.GetManifestResourceNames();
            var resourceStream = assembly.GetManifestResourceStream(fontResourceName);
            if (resourceStream == null)
                throw new Exception(string.Format("Unable to find font '{0}' in embedded resources.", fontResourceName));
            var fontBytes = new byte[resourceStream.Length];
            resourceStream.Read(fontBytes, 0, (int)resourceStream.Length);
            resourceStream.Close();
            return fontBytes;
        }
    }
}
