using System.Collections.Generic;
using System;

namespace LeeFramework.Cfg
{
    public class CfgSvc : CfgBase<CfgSvc>, ICfg
    {
        #region Json
        /// <summary>
        /// 将对象序列化成Json
        /// </summary>
        public string JsonSerialize(object obj)
        {
            return SerializeUtils.JsonSerialize(obj);
        }

        /// <summary>
        /// 将Json反序列化成类对象
        /// </summary>
        public T JsonDeserialize<T>(string str) where T : class, new()
        {
            return SerializeUtils.JsonDeserialize<T>(str);
        }

        /// <summary>
        /// 将Json反序列化成类对象
        /// </summary>
        public object JsonDeserialize(Type type, string str)
        {
            return SerializeUtils.JsonDeserialize(type, str);
        }

        /// <summary>
        /// 将Json的二进制反序列化成类对象
        /// </summary>
        public T JsonBinaryDeserilize<T>(byte[] data) where T : class, new()
        {
            return SerializeUtils.JsonBinaryDeserilize<T>(data);
        }
        #endregion

        #region Xml
        /// <summary>
        /// 将对象序列化成xml并写入
        /// </summary>
        public bool XmlSerialize(string path, object obj)
        {
            return SerializeUtils.XmlSerialize(path, obj);
        }

        /// <summary>
        /// 将二进制反序列成类对象
        /// </summary>
        public T XmlDeserializeFile<T>(byte[] data) where T : XmlBase
        {
            return SerializeUtils.XmlDeserialize<T>(data);
        }

        /// <summary>
        /// 将二进制反序列成类对象
        /// </summary>
        public T XmlDeserialize<T>(byte[] data) where T : XmlBase
        {
            return SerializeUtils.DeseriailzeForXml<T>(data);
        }

        #endregion

        #region 二进制
        /// <summary>
        /// 将类序列化成二进制
        /// </summary>
        public byte[] Serialize(object data)
        {
            return SerializeUtils.Serialize(data);
        }

        /// <summary>
        /// 将二进制反序列化成类
        /// </summary>
        public T Deserialize<T>(byte[] data) where T : class, new()
        {
            return SerializeUtils.Deseriailze<T>(data);
        }
        #endregion

        #region ProtoBuf
        /// <summary>
        /// 将对象序列化成ProtoBuf
        /// </summary>
        public byte[] ProtoSerialize(object obj)
        {
            return SerializeUtils.ProtoSerialize(obj);
        }

        /// <summary>
        /// 将ProtoBuf反序列化成类对象
        /// </summary>
        public T ProtoDeserialize<T>(byte[] msg) where T : class
        {
            return SerializeUtils.ProtoDeserialize<T>(msg);
        }

        #endregion

        #region Csv

        /// <summary>
        /// 将二进制反序列化成类对象
        /// </summary>
        public List<T> CsvDeserialize<T>(byte[] data) where T : CsvBase
        {
            return SerializeUtils.DeseriailzeForCsv<T>(data);
        }

        /// <summary>
        /// 将文本二进制反序列成类对象
        /// </summary>
        public List<T> CsvDeserializeFile<T>(byte[] data) where T : CsvBase
        {
            return CsvUtils.Load<T>(data);
        }

        #endregion

        #region Excel

        /// <summary>
        /// 将二进制反序列化成类对象
        /// </summary>
        public List<T> ExcelDeserialize<T>(byte[] data) where T : ExcelBase
        {
            return SerializeUtils.DeseriailzeForExcel<T>(data);
        }

        /// <summary>
        /// 将文本二进制反序列成类对象
        /// </summary>
        public List<T> ExcelDeserializeFile<T>(byte[] data) where T : ExcelBase
        {
            return ExcelUtils.Load<T>(data);
        }

        #endregion
    }
}