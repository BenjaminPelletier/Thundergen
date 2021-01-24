using Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen
{
    public class Serialization : ITranslatorExtensions
    {
        public static JsonTranslator Translator = new JsonTranslator(new Serialization());

        #region Serialization to JSON

        public JsonTranslator.JsonMaker MakeJsonMaker(Type objectType)
        {
            if (objectType == typeof(HashSet<Vector3>))
            {
                return SerializeHashSetOfVector3;
            }
            else if (objectType == typeof(Dictionary<Vector3, double>))
            {
                return SerializeDictOfVector3ToDouble;
            }
            else if (objectType == typeof(Vector3[]))
            {
                return SerializeVector3Array;
            }
            return null;
        }

        private static JsonObject SerializeEnumerableVector(IEnumerable<Vector3> vectors, int n, bool intMode)
        {
            int s = intMode ? sizeof(int) : sizeof(float);
            var bytes = new byte[n * s * 3];
            int i = 0;
            if (intMode)
            {
                foreach (Vector3 v in vectors)
                {
                    Array.Copy(BitConverter.GetBytes((int)v.X), 0, bytes, i, s);
                    i += s;
                    Array.Copy(BitConverter.GetBytes((int)v.Y), 0, bytes, i, s);
                    i += s;
                    Array.Copy(BitConverter.GetBytes((int)v.Z), 0, bytes, i, s);
                    i += s;
                }
            }
            else
            {
                foreach (Vector3 v in vectors)
                {
                    Array.Copy(BitConverter.GetBytes(v.X), 0, bytes, i, s);
                    i += s;
                    Array.Copy(BitConverter.GetBytes(v.Y), 0, bytes, i, s);
                    i += s;
                    Array.Copy(BitConverter.GetBytes(v.Z), 0, bytes, i, s);
                    i += s;
                }
            }
            return new JsonObject(new Dictionary<string, JsonObject>()
            {
                { "VectorStorageMode", new JsonObject(intMode ? "Int" : "Double") },
                { "Content", new JsonObject(Convert.ToBase64String(bytes)) }
            });
        }

        private static JsonObject SerializeEnumerableDoubles(IEnumerable<double> values, int n, bool intMode)
        {
            int s = intMode ? sizeof(int) : sizeof(double);
            var bytes = new byte[n * s];
            int i = 0;
            if (intMode)
            {
                foreach (double v in values)
                {
                    Array.Copy(BitConverter.GetBytes((int)v), 0, bytes, i, s);
                    i += s;
                }
            }
            else
            {
                foreach (double v in values)
                {
                    Array.Copy(BitConverter.GetBytes(v), 0, bytes, i, s);
                    i += s;
                }
            }
            return new JsonObject(new Dictionary<string, JsonObject>()
            {
                { "StorageMode", new JsonObject(intMode ? "Int" : "Double") },
                { "Content", new JsonObject(Convert.ToBase64String(bytes)) }
            });
        }

        private static JsonObject SerializeHashSetOfVector3(object obj)
        {
            var vectors = obj as HashSet<Vector3>;
            bool intMode = !vectors.Any(v => v.X % 1 != 0 || v.Y % 1 != 0 || v.Z % 1 != 0);
            return SerializeEnumerableVector(vectors, vectors.Count, intMode);
        }

        private static JsonObject SerializeDictOfVector3ToDouble(object obj)
        {
            var candidates = obj as Dictionary<Vector3, double>;
            bool intMode = !candidates.Any(kvp => kvp.Key.X % 1 != 0 || kvp.Key.Y % 1 != 0 || kvp.Key.Z % 1 != 0);
            int s = intMode ? sizeof(int) : sizeof(double);
            var bytes = new byte[candidates.Count * (s * 3 + sizeof(double))];
            int i = 0;
            foreach (var candidate in candidates)
            {
                if (intMode)
                {
                    Array.Copy(BitConverter.GetBytes((int)candidate.Key.X), 0, bytes, i, s);
                    i += s;
                    Array.Copy(BitConverter.GetBytes((int)candidate.Key.Y), 0, bytes, i, s);
                    i += s;
                    Array.Copy(BitConverter.GetBytes((int)candidate.Key.Z), 0, bytes, i, s);
                    i += s;
                }
                else
                {
                    Array.Copy(BitConverter.GetBytes(candidate.Key.X), 0, bytes, i, s);
                    i += s;
                    Array.Copy(BitConverter.GetBytes(candidate.Key.Y), 0, bytes, i, s);
                    i += s;
                    Array.Copy(BitConverter.GetBytes(candidate.Key.Z), 0, bytes, i, s);
                    i += s;
                }
                Array.Copy(BitConverter.GetBytes(candidate.Value), 0, bytes, i, sizeof(double));
                i += sizeof(double);
            }
            return new JsonObject(new Dictionary<string, JsonObject>()
            {
                { "Keys", SerializeEnumerableVector(candidates.Keys, candidates.Count, intMode) },
                { "Values", SerializeEnumerableDoubles(candidates.Values, candidates.Count, false) }
            });
        }

        private static JsonObject SerializeVector3Array(object obj)
        {
            var vectors = obj as Vector3[];
            bool intMode = !vectors.Any(v => v.X % 1 != 0 || v.Y % 1 != 0 || v.Z % 1 != 0);
            return SerializeEnumerableVector(vectors, vectors.Length, intMode);
        }

        #endregion

        #region Serialization from JSON

        public JsonTranslator.ObjectMaker MakeObjectMaker(Type objectType)
        {
            if (objectType == typeof(HashSet<Vector3>))
            {
                return MakeHashSetOfVector3;
            }
            else if (objectType == typeof(Dictionary<Vector3, double>))
            {
                return MakeDictOfVector3ToDouble;
            }
            else if (objectType == typeof(Vector3[]))
            {
                return MakeVector3Array;
            }
            return null;
        }

        private static IEnumerable<Vector3> MakeEnumerableVector3(JsonObject json)
        {
            bool intMode = json.Dictionary["VectorStorageMode"].String == "Int";
            byte[] bytes = Convert.FromBase64String(json.Dictionary["Content"].String);
            int i = 0;
            while (i < bytes.Length)
            {
                if (intMode)
                {
                    int x = BitConverter.ToInt32(bytes, i);
                    i += sizeof(int);
                    int y = BitConverter.ToInt32(bytes, i);
                    i += sizeof(int);
                    int z = BitConverter.ToInt32(bytes, i);
                    i += sizeof(int);
                    yield return new Vector3(x, y, z);
                }
                else
                {
                    float x = BitConverter.ToSingle(bytes, i);
                    i += sizeof(float);
                    float y = BitConverter.ToSingle(bytes, i);
                    i += sizeof(float);
                    float z = BitConverter.ToSingle(bytes, i);
                    i += sizeof(float);
                    yield return new Vector3(x, y, z);
                }
            }
        }

        private static IEnumerable<double> MakeEnumerableDouble(JsonObject json)
        {
            bool intMode = json.Dictionary["StorageMode"].String == "Int";
            byte[] bytes = Convert.FromBase64String(json.Dictionary["Content"].String);
            int i = 0;
            while (i < bytes.Length)
            {
                if (intMode)
                {
                    int v = BitConverter.ToInt32(bytes, i);
                    i += sizeof(int);
                    yield return v;
                }
                else
                {
                    double v = BitConverter.ToDouble(bytes, i);
                    i += sizeof(double);
                    yield return v;
                }
            }
        }

        private static object MakeHashSetOfVector3(JsonObject json)
        {
            return MakeEnumerableVector3(json).ToHashSet();
        }

        private static object MakeDictOfVector3ToDouble(JsonObject json)
        {
            return Enumerable.Zip
            (
                MakeEnumerableVector3(json.Dictionary["Keys"]),
                MakeEnumerableDouble(json.Dictionary["Values"]),
                (k, v) => new KeyValuePair<Vector3, double>(k, v)
            ).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private static object MakeVector3Array(JsonObject json)
        {
            return MakeEnumerableVector3(json).ToArray();
        }

        #endregion

        public static void Write<T>(T obj, string filename)
        {
            var json = Export(obj);
            using (var w = new StreamWriter(filename))
            {
                w.Write(json.ToMultilineString());
            }
        }

        public static JsonObject Export<T>(T obj)
        {
            return Translator.MakeJson(obj);
        }

        public static T Import<T>(JsonObject json)
        {
            return Translator.MakeObject<T>(json);
        }

        public static T Read<T>(string filename)
        {
            return Import<T>(JsonObject.Parse(File.ReadAllText(filename)));
        }
    }
}
