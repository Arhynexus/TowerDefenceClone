using System;
using System.IO;
using UnityEngine;

namespace TowerDefenceClone
{
    [Serializable]
    public class Saver<T>
    {
        private static string Path(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }
        public static void TryLoad(string filename, ref T data)
        {
            var path = Path(filename);
            
            if (File.Exists(path))
            {
                var datastring = File.ReadAllText(path);
                var saver = JsonUtility.FromJson<Saver<T>>(datastring);
                data = saver.data;
            }
        }

        public static void Save(string filename, T data)
        {
            var wrapper = new Saver<T> { data = data };
            var dataString = JsonUtility.ToJson(wrapper);
            File.WriteAllText(Path(filename), dataString);
        }

        public T data;
    }
}