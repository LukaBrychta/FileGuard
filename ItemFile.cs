using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileGuard
{
    /// <summary>
    /// The class represents data about a file.
    /// </summary>
    public class ItemFile
    {
        /// <summary>
        /// File path.
        /// </summary>
        public string filePath;

        /// <summary>
        /// File version number.
        /// </summary>
        public int versionNumber;

        /// <summary>
        /// The date of the last change.
        /// </summary>
        public DateTime lastModified;

        public ItemFile(string filePath, int versionNumber, DateTime lastModified)
        {
            this.filePath = filePath;
            this.versionNumber = versionNumber;
            this.lastModified = lastModified;
        }
    }
}