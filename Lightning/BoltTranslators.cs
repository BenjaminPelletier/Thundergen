using Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen.Lightning
{
    public class BoltTranslators : Json.Serialization.ITranslatorExtensions
    {
        public JsonTranslator.JsonMaker MakeJsonMaker(Type objectType)
        {
            if (objectType == typeof(HashSet<Vector3>))
            {
                return obj =>
                {
                    var vectors = obj as HashSet<Vector3>;
                    var bytes = new byte[vectors.Count * sizeof(int) * 3];
                    int i = 0;
                    foreach (Vector3 v in vectors)
                    {
                        Array.Copy(BitConverter.GetBytes((int)v.X), 0, bytes, i, sizeof(int));
                        i += sizeof(int);
                        Array.Copy(BitConverter.GetBytes((int)v.Y), 0, bytes, i, sizeof(int));
                        i += sizeof(int);
                        Array.Copy(BitConverter.GetBytes((int)v.Z), 0, bytes, i, sizeof(int));
                        i += sizeof(int);
                    }
                    return new JsonObject(Convert.ToBase64String(bytes));
                };
            }
            else if (objectType == typeof(Dictionary<Vector3, double>))
            {
                return obj =>
                {
                    var candidates = obj as Dictionary<Vector3, double>;
                    var bytes = new byte[candidates.Count * (sizeof(int) * 3 + sizeof(double))];
                    int i = 0;
                    foreach (var candidate in candidates)
                    {
                        Array.Copy(BitConverter.GetBytes((int)candidate.Key.X), 0, bytes, i, sizeof(int));
                        i += sizeof(int);
                        Array.Copy(BitConverter.GetBytes((int)candidate.Key.Y), 0, bytes, i, sizeof(int));
                        i += sizeof(int);
                        Array.Copy(BitConverter.GetBytes((int)candidate.Key.Z), 0, bytes, i, sizeof(int));
                        i += sizeof(int);
                        Array.Copy(BitConverter.GetBytes(candidate.Value), 0, bytes, i, sizeof(double));
                        i += sizeof(double);
                    }
                    return new JsonObject(Convert.ToBase64String(bytes));
                };
            }
            else if (objectType == typeof(Vector3[]))
            {
                return obj =>
                {
                    var vectors = obj as Vector3[];
                    var bytes = new byte[vectors.Length * sizeof(int) * 3];
                    int i = 0;
                    foreach (Vector3 v in vectors)
                    {
                        Array.Copy(BitConverter.GetBytes((int)v.X), 0, bytes, i, sizeof(int));
                        i += sizeof(int);
                        Array.Copy(BitConverter.GetBytes((int)v.Y), 0, bytes, i, sizeof(int));
                        i += sizeof(int);
                        Array.Copy(BitConverter.GetBytes((int)v.Z), 0, bytes, i, sizeof(int));
                        i += sizeof(int);
                    }
                    return new JsonObject(Convert.ToBase64String(bytes));
                };
            }
            return null;
        }

        public JsonTranslator.ObjectMaker MakeObjectMaker(Type objectType)
        {
            if (objectType == typeof(HashSet<Vector3>))
            {
                return json =>
                {
                    byte[] bytes = Convert.FromBase64String(json.String);
                    var vectors = new HashSet<Vector3>();
                    int i = 0;
                    while (i < bytes.Length)
                    {
                        int x = BitConverter.ToInt32(bytes, i);
                        i += sizeof(int);
                        int y = BitConverter.ToInt32(bytes, i);
                        i += sizeof(int);
                        int z = BitConverter.ToInt32(bytes, i);
                        i += sizeof(int);
                        vectors.Add(new Vector3(x, y, z));
                    }
                    return vectors;
                };
            }
            else if (objectType == typeof(Dictionary<Vector3, double>))
            {
                return json =>
                {
                    byte[] bytes = Convert.FromBase64String(json.String);
                    var candidates = new Dictionary<Vector3, double>();
                    int i = 0;
                    while (i < bytes.Length)
                    {
                        int x = BitConverter.ToInt32(bytes, i);
                        i += sizeof(int);
                        int y = BitConverter.ToInt32(bytes, i);
                        i += sizeof(int);
                        int z = BitConverter.ToInt32(bytes, i);
                        i += sizeof(int);
                        double v = BitConverter.ToDouble(bytes, i);
                        i += sizeof(double);
                        candidates[new Vector3(x, y, z)] = v;
                    }
                    return candidates;
                };
            }
            else if (objectType == typeof(Vector3[]))
            {
                return json =>
                {
                    byte[] bytes = Convert.FromBase64String(json.String);
                    var vectors = new List<Vector3>();
                    int i = 0;
                    while (i < bytes.Length)
                    {
                        int x = BitConverter.ToInt32(bytes, i);
                        i += sizeof(int);
                        int y = BitConverter.ToInt32(bytes, i);
                        i += sizeof(int);
                        int z = BitConverter.ToInt32(bytes, i);
                        i += sizeof(int);
                        vectors.Add(new Vector3(x, y, z));
                    }
                    return vectors.ToArray();
                };
            }
            return null;
        }

        public static void Write<T>(T obj, string filename)
        {
            var translator = new JsonTranslator(new BoltTranslators());
            var json = translator.MakeJson(obj);
            using (var w = new StreamWriter(filename))
            {
                w.Write(json.ToMultilineString());
            }
        }

        public static T Read<T>(string filename)
        {
            var translator = new JsonTranslator(new BoltTranslators());
            return translator.MakeObject<T>(JsonObject.Parse(File.ReadAllText(filename)));
        }
    }
}
