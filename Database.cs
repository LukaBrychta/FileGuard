using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileGuard
{
    /// <summary>
    /// The class represents a database for working with file data.
    /// </summary>
    class Database
    {
        /// <summary>
        /// The path to the saved data.
        /// </summary>
        public string pathToFileWithData;

        /// <summary>
        /// List of saved data.
        /// </summary>
        public List<ItemFile> archiveFileList;

        /// <summary>
        /// The path to analyze folder.
        /// </summary>
        public string pathToAnalyzeFolder;

        /// <summary>
        /// List of analyzed file data.
        /// </summary>
        public List<ItemFile> anylyzedFileList;

        /// <summary>
        /// Database status report.
        /// </summary>
        public string message;
        public Database(string pathToAnalyzeFolder)
        {
            pathToFileWithData = CreatePathForFileWithData(pathToAnalyzeFolder);
            this.pathToAnalyzeFolder = pathToAnalyzeFolder;

            archiveFileList = new List<ItemFile>();
            anylyzedFileList = new List<ItemFile>();

            message = "";

        }

        /// <summary>
        /// Loads data from the file with data to the database.
        /// </summary>
        public void LoadDataFromFileWithData()
        {

            string[] list = File.ReadAllLines(pathToFileWithData);
            foreach (string line in list)
            {
                string[] split = line.Split(';');
                ItemFile itemFile = new ItemFile(split[0].ToString(), int.Parse(split[1]), DateTime.Parse(split[2]));
                archiveFileList.Add(itemFile);
            }

        }

        /// <summary>
        /// Loads data from the analyzed folder to the database.
        /// </summary>
        public void LoadAnalyzedFileData()
        {
            string[] paths = Directory.GetFiles(pathToAnalyzeFolder, "*", SearchOption.AllDirectories);
            foreach (string filePath in paths)
            {
                ItemFile itemFile = new ItemFile(filePath, 1, File.GetLastWriteTime(filePath));
                anylyzedFileList.Add(itemFile);
            }

        }

        /// <summary>
        /// Compares the analyzed files and files from the archive.
        /// </summary>
        public void CompareFiles()
        {
            bool changes = false;

            // search for deleted files
            foreach (ItemFile itemFile in archiveFileList)
            {
                ItemFile searchedFile = anylyzedFileList.Find(s => s.filePath == itemFile.filePath);
                if (searchedFile == null)
                {
                    message += itemFile.filePath.Replace(pathToAnalyzeFolder + "\\", "[D] ") + "\n";
                    changes = true;
                }

            }

            foreach (ItemFile itemFile in anylyzedFileList)
            {
                ItemFile searchedFile = archiveFileList.Find(s => s.filePath == itemFile.filePath);

                // search for new files
                if (searchedFile == null)
                {
                    message += itemFile.filePath.Replace(pathToAnalyzeFolder + "\\", "[A] ") + "\n";
                    changes = true;
                }
                else if (searchedFile != null)
                {
                    itemFile.versionNumber = searchedFile.versionNumber;


                    // search for changed files
                    if (searchedFile.lastModified.ToShortDateString() != itemFile.lastModified.ToShortDateString() || searchedFile.lastModified.ToLongTimeString() != itemFile.lastModified.ToLongTimeString())
                    {
                        itemFile.versionNumber++;

                        message += itemFile.filePath.Replace(pathToAnalyzeFolder + "\\", "[M] ") + " (ve verzi " + itemFile.versionNumber + ")\n";
                        changes = true;
                    }
                }

            }
            if (!changes) message = "Žádná změna.";
        }

        /// <summary>
        /// Saves the data of the analyzed file to the file with data.
        /// </summary>
        public void SaveDatabase()
        {
            using (StreamWriter sw = new StreamWriter(pathToFileWithData))
            {
                //foreach (ItemFile itemFile in fileList)
                foreach (ItemFile itemFile in anylyzedFileList)
                {
                    string[] values = { itemFile.filePath, itemFile.versionNumber.ToString(), itemFile.lastModified.ToString() };
                    string line = String.Join(";", values);
                    sw.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// Creates a path and file name for the file with data.
        /// </summary>
        /// <param name="pathToAnalyzeFolder">The path to the analyzed folder.</param>
        /// <returns>The string with the path for file with data.</returns>
        public string CreatePathForFileWithData(string pathToAnalyzeFolder)
        {
            string nameForFileWithData = pathToAnalyzeFolder;
            string[] forbiddenCharacters = { " ", ":", "-", "/", "\\", "*", "?", "'", "\"", "<", ">", "|", "č", "š", "ř", "ž", "ý", "á", "í", "é", "ú", "ů", "ď", "ť", "ň", "ě", "ó", "ú", "Č", "Š", "Ř", "Ž", "Ý", "Á", "Í", "É", "Ú", "Ů", "Ď", "Ť", "Ň", "Ě", "Ó", "@", "#", "$", "%", "&", "(", ")", "[", "]", "{", "}", "^", ";", "," };

            foreach (string c in forbiddenCharacters)
            {
                nameForFileWithData = nameForFileWithData.Replace(c, "");
            }

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $"FileGuard" + "-" + nameForFileWithData + ".txt");
        }
    }
}