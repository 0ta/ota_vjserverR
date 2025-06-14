using System;
using System.Runtime.InteropServices;

namespace ota.ndi
{
    public static partial class OtaMP4MetadataLib
    {
        [DllImport("otaMP4MetadataLibrary", EntryPoint = "testLoadMetadata", CharSet = CharSet.Ansi)]
        internal static extern uint testLoadMetadata([MarshalAs(UnmanagedType.LPStr)] string input, uint size);


        //C#で生成された文字列なのでUTF-16渡しで多分OK
        [DllImport("otaMP4MetadataLibrary", EntryPoint = "loadMetadata", CharSet = CharSet.Ansi)]
        internal static extern uint loadMetadata([MarshalAs(UnmanagedType.LPStr)] string input);


        [DllImport("otaMP4MetadataLibrary", EntryPoint = "getBufferSize")]
        internal static extern uint getBufferSize();

        [DllImport("otaMP4MetadataLibrary", EntryPoint = "peekMetadata")]
        internal static extern uint peekMetadata(double time, out IntPtr buffer);

        [DllImport("otaMP4MetadataLibrary", EntryPoint = "peekMetadata")]
        internal static extern void freePeekMetadataBuffer(IntPtr buffer);
    }
}
