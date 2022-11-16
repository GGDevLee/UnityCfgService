using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace LeeFramework.Cfg
{
    public class ExcelUtils
    {

        #region Editor
#if UNITY_EDITOR
        /// <summary>
        /// 保存Excel
        /// </summary>
        public static void Save(List<object> objs, Type type, string path)
        {
            try
            {
                SerializeUtils.HasDirectory(path);
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(type.Name);
                    for (int i = 0; i < objs.Count; i++)
                    {
                        FieldInfo[] fi = type.GetFields();
                        WriteHead(fi, worksheet);
                        WriteBody(objs[i], fi, worksheet);
                    }
                    FileInfo fileInfo = new FileInfo(path);
                    package.SaveAs(fileInfo);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        public static List<object> Load(string path, Type type)
        {
            List<object> data = new List<object>();
            using (ExcelPackage package = new ExcelPackage(new FileStream(path, FileMode.Open, FileAccess.Read)))
            {
                for (int i = 1; i <= package.Workbook.Worksheets.Count; i++)
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[i];

                    //第一行属性
                    List<string> attList = new List<string>();

                    for (int row = sheet.Dimension.Start.Row; row <= sheet.Dimension.End.Row; row++)
                    {
                        //跳过第二行的注释
                        if (row == sheet.Dimension.Start.Row + 1)
                        {
                            continue;
                        }
                        List<object> allList = new List<object>();

                        for (int column = sheet.Dimension.Start.Column; column <= sheet.Dimension.End.Column; column++)
                        {
                            object obj = sheet.GetValue(row, column);
                            if (obj != null)
                            {
                                //第一行
                                if (row == sheet.Dimension.Start.Row)
                                {
                                    attList.Add(obj.ToString());
                                }
                                else
                                {
                                    allList.Add(obj);
                                }
                            }
                        }

                        if (allList != null && allList.Count > 0)
                        {
                            object item = Activator.CreateInstance(type);
                            SetValue(item, attList, allList, type);
                            data.Add(item);
                        }
                    }
                }
            }
            return data;
        }
#endif 
        #endregion


        public static List<T> Load<T>(byte[] data) where T : ExcelBase
        {
            List<T> rtn = new List<T>();
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    for (int i = 1; i <= package.Workbook.Worksheets.Count; i++)
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets[i];

                        //第一行属性
                        List<string> attList = new List<string>();

                        for (int row = sheet.Dimension.Start.Row; row <= sheet.Dimension.End.Row; row++)
                        {
                            //跳过第二行的注释
                            if (row == sheet.Dimension.Start.Row + 1)
                            {
                                continue;
                            }

                            List<object> allList = new List<object>();

                            for (int column = sheet.Dimension.Start.Column; column <= sheet.Dimension.End.Column; column++)
                            {
                                object obj = sheet.GetValue(row, column);
                                if (obj != null)
                                {
                                    //第一行
                                    if (row == sheet.Dimension.Start.Row)
                                    {
                                        attList.Add(obj.ToString());
                                    }
                                    else
                                    {
                                        allList.Add(obj);
                                    }
                                }
                            }

                            if (allList != null && allList.Count > 0)
                            {
                                object item = Activator.CreateInstance(typeof(T));
                                SetValue(item, attList, allList, typeof(T));
                                rtn.Add(item as T);
                            }
                        }
                    }
                }
            }
            return rtn;
        }

        private static void SetValue(object tar, List<string> attList, List<object> allList,Type type)
        {
            FieldInfo[] fi = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo[] pi = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (tar == null)
            {
                return;
            }

            for (int i = 0; i < attList.Count; i++)
            {
                foreach (FieldInfo f in fi)
                {
                    if (string.Compare(attList[i], f.Name, true) == 0)
                    {
                        f.SetValue(tar, Convert.ChangeType(allList[i], f.FieldType));
                        break;
                    }
                }

                foreach (PropertyInfo p in pi)
                {
                    if (string.Compare(attList[i], p.Name, true) == 0)
                    {
                        object typedVal = p.PropertyType == typeof(string) ? allList[i] : ParseStr(allList[i].ToString(), p.PropertyType);
                        p.SetValue(tar, typedVal, null);
                        break;
                    }
                }
            }
        }

        private static void WriteHead(FieldInfo[] fi, ExcelWorksheet worksheet)
        {
            for (int i = 0; i < fi.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = fi[i].Name;
                ExcelAttribute attribute = fi[i].GetCustomAttribute<ExcelAttribute>();
                worksheet.Cells[2, i + 1].Value = attribute.des;
            }
        }

        private static void WriteBody(object obj, FieldInfo[] fi, ExcelWorksheet worksheet)
        {
            for (int i = 0; i < fi.Length; i++)
            {
                worksheet.Cells[3, i + 1].Value = fi[i].GetValue(obj);
            }
        }

        /// <summary>
        /// 解析字符
        /// </summary>
        private static object ParseStr(string str, Type t)
        {
            TypeConverter cv = TypeDescriptor.GetConverter(t);
            return cv.ConvertFromInvariantString(str);
        }
    } 
}
