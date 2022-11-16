using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.ComponentModel;
using Object = UnityEngine.Object;

namespace LeeFramework.Cfg
{
    public class CsvUtils
    {
        private static char[] _Chars = new char[] { ',', ';', '\n' };


        #region Editor
#if UNITY_EDITOR
        /// <summary>
        /// 编辑器加载Csv
        /// </summary>
        public static List<T> LoadEditor<T>(string path, bool strict = true) where T :CsvBase
        {
            try
            {
                using (FileStream stream = File.OpenRead(path))
                {
                    using (StreamReader redaer = new StreamReader(stream))
                    {
                        return Load<T>(redaer, strict);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 运行时加载Csv
        /// </summary>
        public static List<object> LoadEditor(string path, Type type, bool strict = true)
        {
            try
            {
                using (FileStream stream = File.OpenRead(path))
                {
                    using (StreamReader redaer = new StreamReader(stream))
                    {
                        return Load<object>(redaer, type, strict);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 编辑器加载单个Csv数据结构
        /// </summary>
        public static T LoadSingleEditor<T>(string path) where T : Object
        {
            try
            {
                using (FileStream stream = File.Open(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return LoadSingle<T>(reader);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                return default(T);
            }

        }

        /// <summary>
        /// 保存Csv
        /// </summary>
        public static void Save(List<object> objs, Type type, Type attType, string path)
        {
            try
            {
                SerializeUtils.HasDirectory(path);
                using (var stream = File.Open(path, FileMode.Create))
                {
                    using (var wtr = new StreamWriter(stream, System.Text.Encoding.UTF8))
                    {
                        Save(objs, type, attType, wtr);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        public static void Save(List<object> objs, Type type, Type attType, TextWriter write)
        {
            if (objs != null && objs.Count > 0)
            {
                FieldInfo[] fi = type.GetFields();
                WriteHeader(fi, attType, write);

                bool firstLine = true;
                foreach (var obj in objs)
                {
                    if (firstLine)
                    {
                        firstLine = false;
                    }
                    else
                    {
                        write.Write(Environment.NewLine);
                    }
                    WriteObjectToLine(obj, fi, write);
                }
            }
            else
            {
                Debug.LogError("数据结构的值为空，请检查...");
            }
        }


        /// <summary>
        /// 保存单个数据结构
        /// </summary>
        public static void SaveSingle<T>(T obj, string path)
        {
            try
            {
                using (var stream = File.Open(path, FileMode.Create))
                {
                    using (var wtr = new StreamWriter(stream, System.Text.Encoding.UTF8))
                    {
                        SaveSingle<T>(obj, wtr);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        public static void SaveSingle<T>(T obj, TextWriter write)
        {
            FieldInfo[] fi = obj.GetType().GetFields();
            bool firstLine = true;
            foreach (FieldInfo f in fi)
            {
                if (firstLine)
                {
                    firstLine = false;
                }
                else
                {
                    write.Write(Environment.NewLine);
                }
                write.Write(f.Name);
                write.Write(",");
                string val = f.GetValue(obj).ToString();
                if (val.IndexOfAny(_Chars) != -1)
                {
                    val = string.Format("\"{0}\"", val);
                }
                write.Write(val);
            }
        }
#endif 
        #endregion


        public static List<T> Load<T>(byte[] data, bool strict = true) where T : CsvBase
        {
            try
            {
                using (MemoryStream str = new MemoryStream(data))
                {
                    using (StreamReader reader = new StreamReader(str))
                    {
                        return Load<T>(reader, strict);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Csv load fail : " + e.ToString());
                return null;
            }
        }




        private static List<T> Load<T>(TextReader reader, bool strict = true) where T : CsvBase
        {
            List<T> rtn = new List<T>();
            Type type = typeof(T);
            string header = reader.ReadLine();
            Dictionary<string, int> fieldDefs = ParseHeader(header);
            FieldInfo[] fi = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo[] pi = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            bool isValueType = type.IsValueType;
            reader.ReadLine();//跳过注释
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                //T obj = null;
                object boxed = Activator.CreateInstance(type);
                if (ParseLineToObject(line, fieldDefs, fi, pi, boxed, strict))
                {
                    //if (isValueType)
                    //{
                    //    obj = boxed as T;
                    //}
                    rtn.Add(boxed as T);
                }
            }
            return rtn;
        }

        private static List<T> Load<T>(TextReader reader, Type type, bool strict = true) where T : class, new()
        {
            List<T> rtn = new List<T>();
            string header = reader.ReadLine();
            Dictionary<string, int> fieldDefs = ParseHeader(header);
            FieldInfo[] fi = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo[] pi = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            reader.ReadLine();//跳过注释
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var obj = Activator.CreateInstance(type);
                if (ParseLineToObject(line, fieldDefs, fi, pi, obj, strict))
                {
                    rtn.Add(obj as T);
                }
            }
            return rtn;
        }

        private static T LoadSingle<T>(TextReader reader) where T : Object
        {
            T rtn = default(T);
            FieldInfo[] fi = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo[] pi = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("#"))
                {
                    continue;
                }

                string[] vals = EachCsvLine(line).ToArray();
                if (vals.Length >= 2)
                {
                    SetValue(Regex.Replace(vals[0].Trim(), @"\s", string.Empty), vals[1], fi, pi, rtn);
                }
                else
                {
                    Debug.LogWarningFormat("CsvUtils:  line '{0}': 沒有足够的参数", line);
                }
            }
            return rtn;
        }

        private static void WriteHeader(FieldInfo[] fieldInfos, Type attType, TextWriter w)
        {
            bool firstCol = true;
            foreach (FieldInfo f in fieldInfos)
            {
                if (firstCol)
                {
                    firstCol = false;
                }
                else
                {
                    w.Write(",");
                }
                w.Write(f.Name);
            }
            w.Write(Environment.NewLine);
            firstCol = true;
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                if (firstCol)
                {
                    firstCol = false;
                }
                else
                {
                    w.Write(",");
                }
                CsvAttribute attribute = fieldInfo.GetCustomAttribute<CsvAttribute>();
                w.Write(attribute.des);
            }
            w.Write(Environment.NewLine);
        }

        private static void WriteObjectToLine<T>(T obj, FieldInfo[] fi, TextWriter write)
        {
            bool firstCol = true;
            foreach (FieldInfo f in fi)
            {
                if (firstCol)
                {
                    firstCol = false;
                }
                else
                {
                    write.Write(",");
                }
                string val = f.GetValue(obj).ToString();
                if (val.IndexOfAny(_Chars) != -1)
                {
                    val = string.Format("\"{0}\"", val);
                }
                write.Write(val);
            }
        }

        /// <summary>
        /// 解析头
        /// </summary>
        private static Dictionary<string, int> ParseHeader(string header)
        {
            var headers = new Dictionary<string, int>();
            int n = 0;
            foreach (string field in EachCsvLine(header))
            {
                var trimmed = field.Trim();
                if (!trimmed.StartsWith("#"))
                {
                    trimmed = Regex.Replace(trimmed, @"\s", string.Empty);
                    headers[trimmed] = n;
                }
                ++n;
            }
            return headers;
        }

        private static bool SetValue(string name, string value, FieldInfo[] fi, PropertyInfo[] pi, object tar)
        {
            bool result = false;
            foreach (PropertyInfo p in pi)
            {
                if (string.Compare(name, p.Name, true) == 0)
                {
                    object typedVal = p.PropertyType == typeof(string) ? value : ParseStr(value, p.PropertyType);
                    p.SetValue(tar, typedVal, null);
                    result = true;
                    break;
                }
            }
            foreach (FieldInfo f in fi)
            {
                if (string.Compare(name, f.Name, true) == 0)
                {
                    object typedVal = value;
                    f.SetValue(tar, Convert.ChangeType(typedVal, f.FieldType));
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 解析字符
        /// </summary>
        private static object ParseStr(string str, Type t)
        {
            TypeConverter cv = TypeDescriptor.GetConverter(t);
            return cv.ConvertFromInvariantString(str);
        }

        /// <summary>
        /// 解析每一行
        /// </summary>
        private static bool ParseLineToObject(string line, Dictionary<string, int> fieldDefs, FieldInfo[] fi, PropertyInfo[] pi, object tar, bool strict)
        {
            string[] values = EachCsvLine(line).ToArray();
            bool setAny = false;
            foreach (string field in fieldDefs.Keys)
            {
                int index = fieldDefs[field];
                if (index < values.Length)
                {
                    string val = values[index];
                    setAny = SetValue(field, val, fi, pi, tar) || setAny;
                }
                else if (strict)
                {
                    Debug.LogErrorFormat("CsvUtils: 错误 line '{0}': 参数不足", line);
                }
            }
            return setAny;
        }

        /// <summary>
        /// 遍历行
        /// </summary>
        private static IEnumerable<string> EachCsvLine(string line)
        {
            foreach (Match m in Regex.Matches(line, @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)", RegexOptions.ExplicitCapture))
            {
                yield return m.Groups[1].Value;
            }
        }
    } 
}
