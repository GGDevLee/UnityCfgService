using LeeFramework.Cfg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace LeeFramework.Editor
{
    public class EditorCfg
    {
        private static string _TmpPath
        {
            get
            {
                return EditorPrefs.GetString(EditorCfgConst.tmpPathStr, Application.dataPath);
            }
        }

        private static string _AssemblyPath
        {
            get
            {
                return Application.dataPath + "/../Library/ScriptAssemblies";
            }
        }
        private static Dictionary<string, AssemblyItem> _AllAssembly = new Dictionary<string, AssemblyItem>();


        [MenuItem("Assets/Data/类转Json（多选）", false, 100)]
        public static void AssetClassToJson()
        {
            Debug.Log("开始类转Json...");

            UnityEngine.Object[] objs = Selection.objects;

            if (objs != null && objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (!IsFile(AssetDatabase.GetAssetPath(objs[i]), ".cs"))
                    {
                        Debug.LogErrorFormat("{0}并不是一个类，请检查...", AssetDatabase.GetAssetPath(objs[i]));
                        return;
                    }
                }

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("生成Json", "开始搜索：" + objs[i].name, i * 1.0f / objs.Length);
                    ClassToJson(objs[i].name);
                    Debug.LogFormat("生成Json：{0}", objs[i].name);
                }
            }

            Debug.Log("结束类转Json...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Data/类转Xml（多选）", false, 105)]
        public static void AssetClassToXml()
        {
            Debug.Log("开始类转Xml...");

            UnityEngine.Object[] objs = Selection.objects;

            if (objs != null && objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (!IsFile(AssetDatabase.GetAssetPath(objs[i]), ".cs"))
                    {
                        Debug.LogErrorFormat("{0}并不是一个类，请检查...", AssetDatabase.GetAssetPath(objs[i]));
                        return;
                    }
                }

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("生成Xml", "开始搜索：" + objs[i].name, i * 1.0f / objs.Length);
                    ClassToXml(objs[i].name);
                    Debug.LogFormat("生成Xml：{0}", objs[i].name);
                }
            }

            Debug.Log("结束类转Xml...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Data/类转Csv（多选）", false, 110)]
        public static void AssetClassToCsv()
        {
            Debug.Log("开始类转Csv...");

            UnityEngine.Object[] objs = Selection.objects;

            if (objs != null && objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (!IsFile(AssetDatabase.GetAssetPath(objs[i]), ".cs"))
                    {
                        Debug.LogErrorFormat("{0}并不是一个类，请检查...", AssetDatabase.GetAssetPath(objs[i]));
                        return;
                    }
                }

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("生成Csv", "开始搜索：" + objs[i].name, i * 1.0f / objs.Length);
                    ClassToCsv(objs[i].name);
                    Debug.LogFormat("生成Csv：{0}", objs[i].name);
                }
            }

            Debug.Log("结束类转Csv...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Data/类转Excel（多选）", false, 115)]
        public static void AssetClassToExcel()
        {
            Debug.Log("开始类转Excel...");

            UnityEngine.Object[] objs = Selection.objects;

            if (objs != null && objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (!IsFile(AssetDatabase.GetAssetPath(objs[i]), ".cs"))
                    {
                        Debug.LogErrorFormat("{0}并不是一个类，请检查...", AssetDatabase.GetAssetPath(objs[i]));
                        return;
                    }
                }

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("生成Excel", "开始搜索：" + objs[i].name, i * 1.0f / objs.Length);
                    ClassToExcel(objs[i].name);
                    Debug.LogFormat("生成Excel：{0}", objs[i].name);
                }
            }

            Debug.Log("结束类转Excel...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Data/Json转二进制（多选）", false, 130)]
        public static void AssetJsonToBinary()
        {
            Debug.Log("开始Json转二进制...");

            UnityEngine.Object[] objs = Selection.objects;

            if (objs != null && objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (!IsFile(AssetDatabase.GetAssetPath(objs[i]), ".json"))
                    {
                        Debug.LogErrorFormat("{0}并不是一个Json，请检查...", AssetDatabase.GetAssetPath(objs[i]));
                        return;
                    }
                }

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("生成Json", "开始搜索：" + objs[i].name, i * 1.0f / objs.Length);
                    string path = AssetDatabase.GetAssetPath(objs[i]).Replace("Assets", "");
                    path = Application.dataPath + path;
                    JsonToBinary(path, objs[i].name);
                    Debug.LogFormat("Json转二进制：{0}", objs[i].name);
                }
            }

            Debug.Log("结束Json转二进制...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Data/Xml转二进制（多选）", false, 135)]
        public static void AssetXmlToBinary()
        {
            Debug.Log("开始Xml转二进制...");

            UnityEngine.Object[] objs = Selection.objects;

            if (objs != null && objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (!IsFile(AssetDatabase.GetAssetPath(objs[i]), ".xml"))
                    {
                        Debug.LogErrorFormat("{0}并不是一个Xml，请检查...", AssetDatabase.GetAssetPath(objs[i]));
                        return;
                    }
                }

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("生成Xml", "开始搜索：" + objs[i].name, i * 1.0f / objs.Length);
                    string path = AssetDatabase.GetAssetPath(objs[i]).Replace("Assets", "");
                    path = Application.dataPath + path;
                    XmlToBinary(path, objs[i].name);
                    Debug.LogFormat("Xml转二进制：{0}", objs[i].name);
                }
            }

            Debug.Log("结束Xml转二进制...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Data/Csv转二进制（多选）", false, 140)]
        public static void AssetCsvToBinary()
        {
            Debug.Log("开始Csv转二进制...");

            UnityEngine.Object[] objs = Selection.objects;

            if (objs != null && objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (!IsFile(AssetDatabase.GetAssetPath(objs[i]), ".csv"))
                    {
                        Debug.LogErrorFormat("{0}并不是一个Csv文件，请检查...", AssetDatabase.GetAssetPath(objs[i]));
                        return;
                    }
                }

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("生成Csv", "开始搜索：" + objs[i].name, i * 1.0f / objs.Length);
                    string path = AssetDatabase.GetAssetPath(objs[i]).Replace("Assets", "");
                    path = Application.dataPath + path;
                    CsvToBinary(path, objs[i].name);
                    Debug.LogFormat("Csv转二进制：{0}", objs[i].name);
                }
            }

            Debug.Log("结束Csv转二进制...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Data/Excel转二进制（多选）", false, 145)]
        public static void AssetExcelToBinary()
        {
            Debug.Log("开始Excel转二进制...");

            UnityEngine.Object[] objs = Selection.objects;

            if (objs != null && objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (!IsFile(AssetDatabase.GetAssetPath(objs[i]), ".xlsx") && !IsFile(AssetDatabase.GetAssetPath(objs[i]), ".xls"))
                    {
                        Debug.LogErrorFormat("{0}并不是一个Excel文件，请检查...", AssetDatabase.GetAssetPath(objs[i]));
                        return;
                    }
                }

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("生成Excel", "开始搜索：" + objs[i].name, i * 1.0f / objs.Length);
                    string path = AssetDatabase.GetAssetPath(objs[i]).Replace("Assets", "");
                    path = Application.dataPath + path;
                    ExcelToBinary(path, objs[i].name);
                    Debug.LogFormat("Excel转二进制：{0}", objs[i].name);
                }
            }

            Debug.Log("结束Excel转二进制...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Data/全部Json转二进制（选文件夹）", false, 160)]
        public static void AllJsonToBinary()
        {
            Debug.Log("开始全部Json转二进制...");
            UnityEngine.Object[] objs = Selection.GetFiltered<UnityEngine.Object>(SelectionMode.Assets);

            if (objs != null && objs.Length > 0)
            {
                //for (int i = 0; i < objs.Length; i++)
                //{
                //    Debug.Log(AssetDatabase.GetAssetPath(objs[i]));
                //}

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    string fullPath = Application.dataPath + AssetDatabase.GetAssetPath(objs[i]).Replace("Assets", "");
                    BinaryAllJson(fullPath, string.Empty);
                }
            }

            Debug.Log("结束全部Json转二进制...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Data/全部Xml转二进制（选文件夹）", false, 165)]
        public static void AllXmlToBinary()
        {
            Debug.Log("开始全部Xml转二进制...");
            UnityEngine.Object[] objs = Selection.GetFiltered<UnityEngine.Object>(SelectionMode.Assets);

            if (objs != null && objs.Length > 0)
            {
                //for (int i = 0; i < objs.Length; i++)
                //{
                //    Debug.Log(AssetDatabase.GetAssetPath(objs[i]));
                //}

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    string fullPath = Application.dataPath + AssetDatabase.GetAssetPath(objs[i]).Replace("Assets", "");
                    BinaryAllXml(fullPath, string.Empty);
                }
            }

            Debug.Log("结束全部Xml转二进制...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Data/全部Csv转二进制（选文件夹）", false, 170)]
        public static void AllCsvToBinary()
        {
            Debug.Log("开始全部Csv转二进制...");
            UnityEngine.Object[] objs = Selection.GetFiltered<UnityEngine.Object>(SelectionMode.Assets);

            if (objs != null && objs.Length > 0)
            {
                //for (int i = 0; i < objs.Length; i++)
                //{
                //    Debug.Log(AssetDatabase.GetAssetPath(objs[i]));
                //}

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    string fullPath = Application.dataPath + AssetDatabase.GetAssetPath(objs[i]).Replace("Assets", "");
                    BinaryAllCsv(fullPath, string.Empty);
                }
            }

            Debug.Log("结束全部Csv转二进制...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Data/全部Excel转二进制（选文件夹）", false, 175)]
        public static void AllExcelToBinary()
        {
            Debug.Log("开始全部Excel转二进制...");
            UnityEngine.Object[] objs = Selection.GetFiltered<UnityEngine.Object>(SelectionMode.Assets);

            if (objs != null && objs.Length > 0)
            {
                //for (int i = 0; i < objs.Length; i++)
                //{
                //    Debug.Log(AssetDatabase.GetAssetPath(objs[i]));
                //}

                EditorPrefs.SetString(EditorCfgConst.tmpPathStr, EditorUtility.OpenFolderPanel("选择生成路径", _TmpPath, string.Empty));
                LoadAllAssembly();

                for (int i = 0; i < objs.Length; i++)
                {
                    string fullPath = Application.dataPath + AssetDatabase.GetAssetPath(objs[i]).Replace("Assets", "");
                    BinaryAllExcel(fullPath, string.Empty);
                }
            }

            Debug.Log("结束全部Excel转二进制...");
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        //[MenuItem("Assets/Data/Reset/UTF8文件", false, 190)]
        //public static void ResetFile()
        //{
        //    UnityEngine.Object[] objs = Selection.objects;
        //    if (objs != null && objs.Length > 0)
        //    {
        //        for (int i = 0; i < objs.Length; i++)
        //        {
        //            string path = AssetDatabase.GetAssetPath(objs[i]).Replace("Assets", "");
        //            path = Application.dataPath + path;
        //            string content = File.ReadAllText(path, Encoding.UTF8);

        //            StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(false));
        //            sw.Write(content);
        //            //File.WriteAllText(path, content, new UTF8Encoding(false));
        //            Debug.LogFormat("Utf8文件：{0}", objs[i].name);

        //            Debug.Log(IsBomHeader(File.ReadAllBytes(path)));
        //        }
        //    }
        //}


        /// <summary>
        /// 二进制全部Json
        /// </summary>
        public static void BinaryAllJson(string sorPath, string tarPath)
        {
            List<string> allPath = new List<string>();
            SerializeUtils.GetAllFiles(allPath, sorPath);
            if (allPath.Count > 0)
            {
                foreach (string item in allPath)
                {
                    if (item.EndsWith(".json"))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(item);
                        JsonToBinary(item, fileName, tarPath);
                    }
                }
            }
        }

        /// <summary>
        /// 二进制全部Xml
        /// </summary>
        public static void BinaryAllXml(string sorPath, string tarPath)
        {
            List<string> allPath = new List<string>();
            SerializeUtils.GetAllFiles(allPath, sorPath);
            if (allPath.Count > 0)
            {
                foreach (string item in allPath)
                {
                    if (item.EndsWith(".xml"))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(item);
                        XmlToBinary(item, fileName, tarPath);
                    }
                }
            }
        }

        /// <summary>
        /// 二进制全部Csv
        /// </summary>
        public static void BinaryAllCsv(string sorPath, string tarPath)
        {
            List<string> allPath = new List<string>();
            SerializeUtils.GetAllFiles(allPath, sorPath);
            if (allPath.Count > 0)
            {
                foreach (string item in allPath)
                {
                    if (item.EndsWith(".csv"))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(item);
                        CsvToBinary(item, fileName, tarPath);
                    }
                }
            }
        }

        /// <summary>
        /// 二进制全部Excel
        /// </summary>
        public static void BinaryAllExcel(string sorPath, string tarPath)
        {
            List<string> allPath = new List<string>();
            SerializeUtils.GetAllFiles(allPath, sorPath);
            if (allPath.Count > 0)
            {
                foreach (string item in allPath)
                {
                    if (item.EndsWith(".xlsx") || item.EndsWith(".xls"))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(item);
                        ExcelToBinary(item, fileName, tarPath);
                    }
                }
            }
        }




        /// <summary>
        /// 类转Json
        /// </summary>
        private static void ClassToJson(string name)
        {
            try
            {
                Type type = GetClassType(name);
                Type jsonType = GetClassType(EditorCfgConst.jsonClassStr);

                if (jsonType == null)
                {
                    Debug.LogErrorFormat("找不到{0}的基类，请检查...", EditorCfgConst.jsonClassStr);
                    return;
                }

                if (type != null)
                {
                    if (type.BaseType.FullName == jsonType.FullName)
                    {
                        var tmp = Activator.CreateInstance(type);

                        MethodInfo method = type.GetMethod("TmpData");

                        method.Invoke(tmp, null);

                        string jsonPath = _TmpPath + "/" + name + ".json";

                        string json = SerializeUtils.JsonSerialize(tmp);

                        SerializeUtils.WriteFile(jsonPath, Encoding.UTF8.GetBytes(json));
                        SerializeUtils.OpenFolder(_TmpPath);
                    }
                    else
                    {
                        Debug.LogErrorFormat("{0}没有继承{1}，请检查...", name, EditorCfgConst.jsonClassStr);
                        return;
                    }
                }
                else
                {
                    Debug.LogErrorFormat("没有找到对应{0}的类", name);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("类转Json失败：" + e.ToString());
            }
        }

        /// <summary>
        /// 类转Xml
        /// </summary>
        private static void ClassToXml(string name)
        {
            try
            {
                Type type = GetClassType(name);
                Type xmlType = GetClassType(EditorCfgConst.xmlClassStr);

                if (xmlType == null)
                {
                    Debug.LogErrorFormat("找不到{0}的基类，请检查...", EditorCfgConst.xmlClassStr);
                    return;
                }

                if (type != null)
                {
                    if (type.BaseType.FullName == xmlType.FullName)
                    {
                        var tmp = Activator.CreateInstance(type);

                        MethodInfo method = type.GetMethod("TmpData");

                        method.Invoke(tmp, null);

                        string xmlPath = _TmpPath + "/" + name + ".xml";

                        SerializeUtils.XmlSerialize(xmlPath, tmp);
                        SerializeUtils.OpenFolder(_TmpPath);
                    }
                    else
                    {
                        Debug.LogErrorFormat("{0}没有继承{1}，请检查...", name, EditorCfgConst.xmlClassStr);
                        return;
                    }
                }
                else
                {
                    Debug.LogErrorFormat("没有找到对应{0}的类", name);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("类转Xml失败：" + e.ToString());
            }
        }

        /// <summary>
        /// 类转Csv
        /// </summary>
        private static void ClassToCsv(string name)
        {
            try
            {
                Type type = GetClassType(name);
                Type csvType = GetClassType(EditorCfgConst.csvClassStr);
                Type attType = GetClassType(EditorCfgConst.csvClassAttStr);

                if (csvType == null)
                {
                    Debug.LogErrorFormat("找不到{0}的基类，请检查...", EditorCfgConst.csvClassStr);
                    return;
                }

                if (attType == null)
                {
                    Debug.LogErrorFormat("找不到{0}的基类，请检查...", EditorCfgConst.csvClassAttStr);
                    return;
                }

                if (type != null)
                {
                    if (type.BaseType.FullName == csvType.FullName)
                    {
                        var tmp = Activator.CreateInstance(type);

                        MethodInfo method = type.GetMethod("TmpData");

                        method.Invoke(tmp, null);

                        string csvPath = _TmpPath + "/" + name + ".csv";

                        CsvUtils.Save(new List<object> { tmp }, type, attType, csvPath);
                        SerializeUtils.OpenFolder(_TmpPath);
                    }
                    else
                    {
                        Debug.LogErrorFormat("{0}没有继承{1}，请检查...", name, EditorCfgConst.csvClassStr);
                        return;
                    }
                }
                else
                {
                    Debug.LogErrorFormat("没有找到对应{0}的类", name);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("类转Csv失败：" + e.ToString());
            }
        }

        /// <summary>
        /// 类转Excel
        /// </summary>
        private static void ClassToExcel(string name)
        {
            try
            {
                Type type = GetClassType(name);
                Type excelType = GetClassType(EditorCfgConst.excelClassStr);
                Type attType = GetClassType(EditorCfgConst.excelClassAttStr);

                if (excelType == null)
                {
                    Debug.LogErrorFormat("找不到{0}的基类，请检查...", EditorCfgConst.excelClassStr);
                    return;
                }

                if (attType == null)
                {
                    Debug.LogErrorFormat("找不到{0}的基类，请检查...", EditorCfgConst.excelClassAttStr);
                    return;
                }

                if (type != null)
                {
                    if (type.BaseType.FullName == excelType.FullName)
                    {
                        var tmp = Activator.CreateInstance(type);

                        MethodInfo method = type.GetMethod("TmpData");

                        method.Invoke(tmp, null);

                        string excelPath = _TmpPath + "/" + name + ".xlsx";

                        ExcelUtils.Save(new List<object> { tmp }, type, excelPath);

                        SerializeUtils.OpenFolder(_TmpPath);
                    }
                    else
                    {
                        Debug.LogErrorFormat("{0}没有继承{1}，请检查...", name, EditorCfgConst.excelClassStr);
                        return;
                    }
                }
                else
                {
                    Debug.LogErrorFormat("没有找到对应{0}的类", name);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("类转Csv失败：" + e.ToString());
            }
        }


        /// <summary>
        /// Json转二进制
        /// </summary>
        private static void JsonToBinary(string path, string name, string tarPath = null)
        {
            try
            {
                if (!path.EndsWith(".json"))
                {
                    Debug.LogErrorFormat("{0}不是Json文件，请检查...", path);
                    return;
                }
                Type type = GetClassType(name);

                if (type != null)
                {
                    object json = JsonMapper.ToObject(type, SerializeUtils.ReadFileStr(path));

                    if (json == null)
                    {
                        Debug.LogErrorFormat("Json ：{0}反序列化失败...", path);
                        return;
                    }

                    string binaryPath = tarPath ?? _TmpPath + "/" + name + ".bytes";

                    if (!SerializeUtils.Serialize(binaryPath, json))
                    {
                        Debug.LogErrorFormat("二进制：{0}序列化失败...", binaryPath);
                    }
                    SerializeUtils.OpenFolder(_TmpPath);
                }
                else
                {
                    Debug.LogErrorFormat("类型{0}为null，请检查类型", name);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Json转二进制失败：" + e.ToString());
            }
        }

        /// <summary>
        /// Xml转二进制
        /// </summary>
        private static void XmlToBinary(string path, string name, string tarPath = null)
        {
            try
            {
                if (!path.EndsWith(".xml"))
                {
                    Debug.LogErrorFormat("{0}不是Xml文件，请检查...", path);
                    return;
                }
                Type type = GetClassType(name);

                //path = path.Replace("Assets/", "");

                if (type != null)
                {
                    object xml = SerializeUtils.XmlDeserialize(SerializeUtils.ReadFile(path), type);

                    if (xml == null)
                    {
                        Debug.LogErrorFormat("Xml ：{0}反序列化失败...", path);
                        return;
                    }

                    string binaryPath = tarPath ?? _TmpPath + "/" + name + ".bytes";

                    if (!SerializeUtils.Serialize(binaryPath, xml))
                    {
                        Debug.LogErrorFormat("二进制：{0}序列化失败...", binaryPath);
                    }
                    SerializeUtils.OpenFolder(_TmpPath);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Xml转二进制失败：" + e.ToString());
            }
        }

        /// <summary>
        /// CSV转二进制
        /// </summary>
        private static void CsvToBinary(string path, string name, string tarPath = null)
        {
            try
            {
                if (!path.EndsWith(".csv"))
                {
                    Debug.LogErrorFormat("{0}不是CSV文件，请检查...", path);
                    return;
                }
                Type type = GetClassType(name);

                if (type != null)
                {
                    List<object> csv = CsvUtils.LoadEditor(path, type);

                    if (csv == null)
                    {
                        Debug.LogErrorFormat("Csv ：{0}读取失败...", path);
                        return;
                    }

                    string binaryPath = tarPath ?? _TmpPath + "/" + name + ".bytes";

                    if (!SerializeUtils.Serialize(binaryPath, csv))
                    {
                        Debug.LogErrorFormat("二进制：{0}序列化失败...", binaryPath);
                    }

                    SerializeUtils.OpenFolder(_TmpPath);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Csv转二进制失败：" + e.ToString());
            }
        }

        /// <summary>
        /// Excel转二进制
        /// </summary>
        private static void ExcelToBinary(string path, string name, string tarPath = null)
        {
            try
            {
                if (!path.EndsWith(".xlsx") && !path.EndsWith(".xls"))
                {
                    Debug.LogErrorFormat("{0}不是Excel文件，请检查...", path);
                    return;
                }
                Type type = GetClassType(name);
                if (type != null)
                {

                    List<object> excel = ExcelUtils.Load(path, type);

                    if (excel == null)
                    {
                        Debug.LogErrorFormat("Excel ：{0}读取失败...", path);
                        return;
                    }

                    string binaryPath = tarPath ?? _TmpPath + "/" + name + ".bytes";

                    if (!SerializeUtils.Serialize(binaryPath, excel))
                    {
                        Debug.LogErrorFormat("二进制：{0}序列化失败...", binaryPath);
                    }

                    SerializeUtils.OpenFolder(_TmpPath);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Excel转二进制失败：" + e.ToString());
            }
        }


        /// <summary>
        /// 判断是不是某种文件
        /// </summary>
        private static bool IsFile(string fullPath, string end)
        {
            if (fullPath.EndsWith(end))
            {
                return true;
            }
            return false;
        }

        private static void LoadAllAssembly()
        {
            _AllAssembly.Clear();
            List<string> allPath = new List<string>();
            SerializeUtils.GetAllFiles(allPath, _AssemblyPath);

            foreach (string item in allPath)
            {
                LoadAssembly(item);
            }
        }

        private static void LoadAssembly(string path)
        {
            string name = string.Empty;
            if (path.EndsWith(".dll"))
            {
                name = path.Replace(".dll", "");
            }
            if (!string.IsNullOrEmpty(name))
            {
                name += ".pdb";
            }

            byte[] assmbly = SerializeUtils.ReadFile(path);
            byte[] symbol = SerializeUtils.ReadFile(name);

            if (assmbly != null && symbol != null)
            {
                Assembly mainAssembly = Assembly.Load(assmbly, symbol);
                if (mainAssembly != null)
                {
                    _AllAssembly.Add(path, new AssemblyItem(mainAssembly, assmbly, symbol));
                }
                else
                {
                    Debug.LogError("load assembly fail : " + path + "symbol : " + name);
                }
            }
        }

        private static Type GetClassType(string name)
        {
            if (_AllAssembly != null && _AllAssembly.Count > 0)
            {
                foreach (AssemblyItem item in _AllAssembly.Values)
                {
                    if (item != null && item.assembly != null)
                    {
                        Type type = item.assembly.GetType(name);
                        if (type != null)
                        {
                            return type;
                        }
                    }
                }
            }
            return null;
        }

        private static bool IsBomHeader(byte[] data)
        {
            int len = data.Length;
            if (len >= 3 && data[0] == 0xEF && data[1] == 0xBB && data[2] == 0xBF)
            {
                return true;
            }
            return false;
        }

    }

    public class AssemblyItem
    {
        public Assembly assembly;
        public byte[] assemblyData;
        public byte[] symbolStore;

        public AssemblyItem(Assembly ass, byte[] assData, byte[] symbol)
        {
            assembly = ass;
            assemblyData = assData;
            symbolStore = symbol;
        }
    }
}