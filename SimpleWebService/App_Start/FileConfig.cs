using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SimpleWebService.App_Start
{
    /// <summary>
    /// This class handles interaction with file.json, which contains all the key value pairs.
    /// </summary>
    public class FileConfig
    {
        /// <summary>
        /// Project directory.
        /// </summary>
        private string dir = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        /// <summary>
        /// Path to file.json in project folder.
        /// </summary>
        private string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\Data\\file.json";

        /// <summary>
        /// Dictionary object for elements in file.json.
        /// </summary>
        public Dictionary<string, string> fileKeyValues;
        
        /// <summary>
        /// Reading from file.json to form the dictionary object.
        /// </summary>
        public void ReadFromFile()
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                fileKeyValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
        }

        /// <summary>
        /// Writing the dictionary object to file.json.
        /// </summary>
        public void WriteToFile()
        {
            string json = JsonConvert.SerializeObject(fileKeyValues);
            System.IO.File.WriteAllText(path, json);
        }
    }
}