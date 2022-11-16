using LeeFramework.Cfg;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using UnityEngine;

namespace LeeFramework.Cfg
{
    public class SerializeUtils
    {
        #region Json
        /// <summary>
        /// 类转成Json字符串
        /// </summary>
        public static string JsonSerialize(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return string.Empty;
                }
                return Regex.Unescape(JsonMapper.ToJson(obj));
            }
            catch (Exception e)
            {
                Debug.LogError("json serialize fail : " + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// Json反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string str) where T : class
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                return JsonMapper.ToObject<T>(str);
            }
            catch (Exception e)
            {
                Debug.LogError("json deserialize fail : " + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// Json反序列化
        /// </summary>
        public static object JsonDeserialize(Type type, string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                return JsonMapper.ToObject(type, str);
            }
            catch (Exception e)
            {
                Debug.LogError("json deserialize fail : " + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// Json克隆
        /// </summary>
        public static T JsonClone<T>(T t) where T : class
        {
            return JsonDeserialize<T>(JsonSerialize(t));
        }

        /// <summary>
        /// 二进制反序列化
        /// </summary>
        public static T JsonBinaryDeserilize<T>(byte[] data) where T : class, new()
        {
            T rtn = null;
            try
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    rtn = bf.Deserialize(ms) as T;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("json deserialize fail : " + e.ToString());
            }
            return rtn;
        }
        #endregion

        #region Xml

        /// <summary>
        /// 类序列化成xml
        /// </summary>
        public static bool XmlSerialize(string path, object obj)
        {
            try
            {
                if (!File.Exists(path))
                {
                    HasDirectory(path);
                    CreateFile(path, null);
                }
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                    {
                        XmlSerializer xs = new XmlSerializer(obj.GetType());
                        xs.Serialize(sw, obj);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("此类无法转换成xml " + obj.GetType() + "," + e.ToString());
            }
            return false;
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        public static T XmlDeserialize<T>(byte[] data) where T : class
        {
            T rtn = null;

            if (data == null)
            {
                Debug.LogError("xml deserialize fail : data is null...");
                return null;
            }
            try
            {
                using (MemoryStream stream = new MemoryStream(data))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    rtn = (T)xs.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("xml deserialize fail : " + e.ToString());
            }
            return rtn;
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        public static object XmlDeserialize(byte[] data,Type type) 
        {
            object rtn = null;

            if (data == null)
            {
                Debug.LogError("xml deserialize fail : data is null...");
                return null;
            }
            try
            {
                using (MemoryStream stream = new MemoryStream(data))
                {
                    XmlSerializer xs = new XmlSerializer(type);
                    rtn = xs.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("xml deserialize fail : " + e.ToString());
            }
            return rtn;
        }
        #endregion

        #region 二进制
        public static byte[] Serialize(object data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, data);
                ms.Seek(0, SeekOrigin.Begin);
                return ms.ToArray();
            }
        }

        public static T Deseriailze<T>(byte[] bs)
        {
            using (MemoryStream ms = new MemoryStream(bs))
            {
                BinaryFormatter bf = new BinaryFormatter();
                T data = (T)bf.Deserialize(ms);
                return data;
            }
        }
        public static T DeseriailzeForXml<T>(byte[] bs) where T : XmlBase
        {
            T rtn = null;
            using (MemoryStream ms = new MemoryStream(bs))
            {
                BinaryFormatter bf = new BinaryFormatter();
                rtn = (T)bf.Deserialize(ms);
            }
            return rtn;
        }

        public static List<T> DeseriailzeForCsv<T>(byte[] bs) where T : CsvBase
        {
            List<T> rtn = new List<T>();
            using (MemoryStream ms = new MemoryStream(bs))
            {
                BinaryFormatter bf = new BinaryFormatter();
                List<object> data = (List<object>)bf.Deserialize(ms);

                foreach (object item in data)
                {
                    rtn.Add((T)item);
                }
            }
            return rtn;
        }

        public static List<T> DeseriailzeForExcel<T>(byte[] bs) where T : ExcelBase
        {
            List<T> rtn = new List<T>();
            using (MemoryStream ms = new MemoryStream(bs))
            {
                BinaryFormatter bf = new BinaryFormatter();
                List<object> data = (List<object>)bf.Deserialize(ms);

                foreach (object item in data)
                {
                    rtn.Add((T)item);
                }
            }
            return rtn;
        }

        public static bool Serialize(string path, object obj)
        {
            try
            {
                if (!File.Exists(path))
                {
                    HasDirectory(path);
                    CreateFile(path, null);
                }
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, obj);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("此类无法转换成二进制 " + obj.GetType() + "," + e.ToString());
            }
            return false;
        }

        #endregion

        #region ProtoBuf
        public static byte[] ProtoSerialize(object obj)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Serializer.Serialize(ms, obj);
                    byte[] result = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(result, 0, result.Length);
                    return result;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("ProtoBuf serialize fail : " + e.ToString());
                return null;
            }
        }

        public static T ProtoDeserialize<T>(byte[] msg) where T : class
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(msg, 0, msg.Length);
                    ms.Position = 0;
                    return Serializer.Deserialize<T>(ms);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("ProtoBuf deserialize fail : " + e.ToString());
                return null;
            }
        }
        #endregion

        #region IO
        public static bool HasDirectory(string path, bool isCreate = true)
        {
            path = Path.GetDirectoryName(path);
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                if (isCreate)
                {
                    Directory.CreateDirectory(path);
                    return false;
                }
            }
            return false;
        }
        public static void CreateFile(string path, byte[] bytes)
        {
            if (HasFile(path))
            {
                DeleteFile(path);
            }
            if (bytes == null)
            {
                File.Create(path).Dispose();
                return;
            }
            FileInfo fileInfo = new FileInfo(path);
            using (FileStream fs = fileInfo.Create())
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                fs.Dispose();
            }
        }
        public static bool HasFile(string path)
        {
            return File.Exists(path);
        }
        public static void DeleteFile(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogError("删除失败，该路径不存在：" + path);
                return;
            }
            File.Delete(path);
        }
        public static void WriteFile(string path, byte[] bytes)
        {
            HasDirectory(path);
            if (HasFile(path))
            {
                DeleteFile(path);
            }
            File.WriteAllBytes(path, bytes);
        }
        public static void OpenFolder(string path)
        {
            if (HasDirectory(path))
            {
                System.Diagnostics.Process.Start(path);
            }
        }
        public static void GetAllFiles(List<string> files, string dir)
        {
            string[] fls = Directory.GetFiles(dir);
            foreach (string fl in fls)
            {
                files.Add(fl);
            }

            string[] subDirs = Directory.GetDirectories(dir);

            foreach (string subDir in subDirs)
            {
                GetAllFiles(files, subDir);
            }
        }
        public static byte[] ReadFile(string path)
        {
            if (!HasFile(path))
            {
                return null;
            }

           return File.ReadAllBytes(path);
        }
        public static string ReadFileStr(string path)
        {
            string data;
            using (StreamReader sr = new StreamReader(path))
            {
                data = sr.ReadToEnd().Trim();
            }
            return data;
        }
        #endregion
    } 
}
