using System;
using System.Collections.Generic;
using System.IO;

namespace SetAndMap
{
    public class FileOperation
    {
        public static bool ReadFromFile(string filename, List<string> wordList)
        {
            if (filename == null || wordList == null)
            {
                Console.WriteLine("filename is null or words is null");
                return false;
            }

            try
            {
                // 简单分词
                StreamReader file = new StreamReader(filename);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] words = line.Split(" ");
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (isWord(words[i]))
                        {
                            wordList.Add(words[i].ToLower());
                        }
                    }
                }
            }
            catch (IOException ioe)
            {
                throw new ArgumentException("Could not open " + filename, ioe);
            }

            return true;
        }

        private static bool isWord(string word)
        {
            foreach (char c in word)
            {
                if (c < 'A' || c > 'z')
                    return false;
            }

            return true;
        }
    }
}