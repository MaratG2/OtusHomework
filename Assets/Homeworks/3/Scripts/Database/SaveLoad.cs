using System.IO;

namespace Homework3.Database
{
    public static class SaveLoad
    {
        private static readonly string _path = "Assets/Homeworks/3/Resources/GameJSONData/";

        public static bool HaveSave(string fileName)
        {
            string fullPath = _path + fileName + ".json";
            return File.Exists(fullPath);
        }
        
        public static string Load(string fileName)
        {
            string fullPath = _path + fileName + ".json";
            string data = "";
            using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                using (StreamReader reader = new StreamReader(fs))
                    data += reader.ReadLine() + "\n";
            return data;
        }
        public static void Save(string data, string fileName)
        {
            string fullPath = _path + fileName + ".json";
            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(fs))
                    writer.Write(data);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh ();
#endif
        }
    }
}