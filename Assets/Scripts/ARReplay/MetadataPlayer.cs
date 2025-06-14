using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace ota.ndi
{
    public class MetadataPlayer : IDisposable
    {

        public MetadataPlayer(string path)
        {
            //This is a test purpose
            //uint byteCount = (uint)Encoding.UTF8.GetByteCount(path) + 1;
            //OtaMP4MetadataLib.testLoadMetadata(path, byteCount);
            uint framesize = OtaMP4MetadataLib.loadMetadata(path);
            Debug.Log("Frame size: " + framesize);
            
            //とりあえずは使わない実装
            uint metadatasize = OtaMP4MetadataLib.getBufferSize();
            Debug.Log("Metadata size: " + metadatasize);

            //Avfi.LoadMetadata(path);
            //// Get max size of metadata
            //uint size = Avfi.GetBufferSize();
            //_buffer = new byte[size];
        }

        public void Dispose()
        {
            //ライブラリがVectorを使っているのでUnloadする必要なし
            //Avfi.UnloadMetadata();
        }

        public int PeekMetadata(double time)
        {
            //とりあえずIntPtrで実装
            //パフォーマンスが出なかったら固定長のbyte配列で実装し直し
            IntPtr bufferPtr = IntPtr.Zero;
            uint length = OtaMP4MetadataLib.peekMetadata(time, out bufferPtr);
            if (length > int.MaxValue)
                throw new OverflowException();
            byte[] buffer = new byte[length];
            Marshal.Copy(bufferPtr, buffer, 0, (int)length);
            string metadataStr = Encoding.UTF8.GetString(buffer);
            //Debug.Log("time:" + time);
            //Debug.Log("length:" + length);
            //Debug.Log(metadataStr);
            return 0;
            //fixed (byte* ptr = _buffer)
            //{
            //    uint size = OtaMP4MetadataLib.peekMetadata(time, out ptr);
            //    if (size == 0)
            //    {
            //        return ReadOnlySpan<byte>.Empty;
            //    }
            //    return new ReadOnlySpan<byte>(ptr, (int)size);
            //}
            //return null;
        }
    }
}
