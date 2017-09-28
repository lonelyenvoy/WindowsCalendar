using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace WindowsCalendar
{
    /// <summary>
    /// 压缩与解压工具类
    /// </summary>
    public static class ZipUtil
    {
        // 以下代码出处（有删改）：http://www.cnblogs.com/yank/p/Compress.html
        // 版权归原作者所有

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileToCompress">待压缩的文件名</param>
        /// <param name="newExtension">在文件末尾追加的扩展名</param>
        /// <param name="deleteSourceFile">是否删除源文件</param>
        public static void Compress(FileInfo fileToCompress, string newExtension = ".gz", bool deleteSourceFile = false)
        {
            if (newExtension == "")
                throw new ArgumentException();

            bool isCompressSucceeded = false;

            using (FileStream originalFileStream = fileToCompress.OpenRead())
            {
                if ((System.IO.File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != newExtension)
                {
                    using (FileStream compressedFileStream = System.IO.File.Create(fileToCompress.FullName + newExtension))
                    {
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressionStream);
                            isCompressSucceeded = true;
                        }
                    }
                }
            }
            if (isCompressSucceeded && deleteSourceFile)
                System.IO.File.Delete(fileToCompress.FullName);
        }

        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="fileToDecompress">待解压缩的文件名</param>
        /// <param name="deleteZippedFile">是否删除源文件</param>
        public static void Decompress(FileInfo fileToDecompress, bool deleteZippedFile = false)
        {
            bool isDecompressSucceeded = false;

            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = System.IO.File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        isDecompressSucceeded = true;
                    }
                }
            }
            if (isDecompressSucceeded && deleteZippedFile)
                System.IO.File.Delete(fileToDecompress.FullName);
        }
    }
}
