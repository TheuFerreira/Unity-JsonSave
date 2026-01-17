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

            var path = Internal_GeneratePath(fileName);

            return path;
        }

        private static string GeneratePathByFileName(string fileName)
        {
            var fileNameWithouthExtension = $"{fileName}.json";
            var path = Internal_GeneratePath(fileNameWithouthExtension);

            return path;
        }

        private static string Internal_GeneratePath(string fileName)
        {
            var folder = GetFolder();
            var path = Path.Combine(folder, fileName);

#if UNITY_EDITOR
            Debug.Log("Json in Path: " + folder);
#endif

            return path;
        }

        public static void Save(T jsonSave)
        {
            var path = GeneratePath();
            Internal_Save(jsonSave, path);
        }

        public static void Save(T jsonSave, string fileName)
        {
            var path = GeneratePathByFileName(fileName);
            Internal_Save(jsonSave, path);
        }

        private static void Internal_Save(T jsonSave, string path)
        {
            var json = JsonUtility.ToJson(jsonSave);

            File.WriteAllText(path, json);
            Debug.Log(path);
        }

        public static bool Has()
        {
            var path = GeneratePath();
            var exists = Internal_Has(path);

            return exists;
        }

        public static bool Has(string fileName)
        {
            var path = GeneratePathByFileName(fileName);
            var exists = Internal_Has(path);

            return exists;
        }

        private static bool Internal_Has(string path)
        {
            var exists = File.Exists(path);

            return exists;
        }

        public static T Load()
        {
            var path = GeneratePath();
            var json = Internal_Load(path);

            return json;
        }

        public static T Load(string fileName)
        {
            var path = GeneratePathByFileName(fileName);
            var json = Internal_Load(path);

            return json;
        }

        public static T Internal_Load(string path)
        {
            var text = File.ReadAllText(path);
            var json = JsonUtility.FromJson<T>(text);

            return json;
        }
    }
}
