using System;
using System.Collections.Generic;
using System.IO;

namespace RepositoryScanner
{
    public class NecRepositoryReader : RepsitoryReader
    {
        private static readonly Queue<string> directoryFilesQueue = new Queue<string>();

        private string NextFile { get; set; }

        public NecRepositoryReader() : base()
        {
            OpenReader();
        }

        public override bool Read()
        {
            if (directoryFilesQueue.Count > 0)
            {
                var value = directoryFilesQueue.Dequeue();
                NextFile = value;
                return true;
            }
            return false;
        }


        public override int GetValues(object[] values)
        {
            int fieldCount = 1;
            object[] fieldValueArray = new object[fieldCount];

            fieldValueArray[0] = NextFile;

            values = fieldValueArray;
            return fieldCount;
        }

        public override object GetValue(int i)
        {
            int fieldCount = 1;
            object[] fieldValueArray = new object[fieldCount];
            fieldValueArray[0] = NextFile;

            if (NextFile is null)
            {
                Console.Write("");
            }
            return fieldValueArray[i];
        }

        private static void OpenReader()
        {
            try
            {
                QueueFiles(Directory.GetDirectories(@"D:\"));
            }
            catch (Exception) { }
        }

        private static void QueueFiles(string[] directories)
        {
            foreach (var directory in directories)
            {
                EnqueueDirectoryFiles(directory);
                try
                {
                    QueueFiles(Directory.GetDirectories(directory));
                }
                catch (Exception) { }
            }
        }

        private static void EnqueueDirectoryFiles(string directory)
        {
            try
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    try
                    {
                        directoryFilesQueue.Enqueue(file);
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
        }
    }
}
