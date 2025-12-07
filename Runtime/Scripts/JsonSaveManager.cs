using System.IO;
using UnityEngine;

namespace Ferreira.JsonSave
{
    public static class JsonSaveManager<T> where T : IJsonSave
    {
        private static string GetFolder()
        {
            string folder = Path.Combine(Application.persistentDataPath, "JsonSaves");
            Directory.CreateDirectory(folder);

            return folder;
        }

        private static string GeneratePath()
        {
            var className = typeof(T).Name;
            var fileName = $"{className}.json";

            var folder = GetFolder();
            var path = Path.Combine(folder, fileName);
            return path;
        }

        public static void Save(T jsonSave)
        {
            var path = GeneratePath();
            var json = JsonUtility.ToJson(jsonSave);

            File.WriteAllText(path, json);
            Debug.Log(path);
        }

        public static bool Has()
        {
            var path = GeneratePath();
            var exists = File.Exists(path);

            return exists;
        }

        public static T Load()
        {
            var path = GeneratePath();
            var text = File.ReadAllText(path);

            var json = JsonUtility.FromJson<T>(text);

            return json;
        }
    }
}
