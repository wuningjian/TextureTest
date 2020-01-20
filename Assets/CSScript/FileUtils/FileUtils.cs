using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.BZip2;

namespace Assets.CSScript
{
    public class FileUtils
    {
        public string m_TestFilePath = Application.streamingAssetsPath + "/script_list.cfg"; 
        public byte[] GetTestFile()
        {
            byte[] data = GetFileData(m_TestFilePath);
            return data;
        }

        public static byte[] GetFileData(string path)
        {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            path = "file:///" + path;
#elif UNITY_IPHONE
            path = "file://" + path;
#endif
            using (WWW www = new WWW(path))
            {
                while (!www.isDone) ;
                return www.bytes;
            }
        }

        public static byte[] UnzipData(byte[] data)
        {
            try
            {
                using (MemoryStream dms = new MemoryStream())
                {
                    using (MemoryStream cms = new MemoryStream(data))
                    {
                        using (BZip2InputStream gzip = new BZip2InputStream(cms))
                        {
                            byte[] bytes = new byte[1024];
                            int len = 0;
                            while ((len = gzip.Read(bytes, 0, bytes.Length)) > 0)
                            {
                                dms.Write(bytes, 0, len);
                            }
                        }
                    }
                    return dms.ToArray();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[ZipUtility.UnzipData]: " + e.ToString());
            }
            return null;
        }
    }
}
