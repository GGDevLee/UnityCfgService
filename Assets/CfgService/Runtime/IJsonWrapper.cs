using System;
using System.Collections.Generic;

namespace LeeFramework.Cfg
{
    public interface ICfg2
    {
        public string JsonSerialize(object obj);
        public T JsonDeserialize<T>(string str) where T : class, new();
        public object JsonDeserialize(Type type, string str);
        public T JsonBinaryDeserilize<T>(byte[] data) where T : class, new();


        public bool XmlSerialize(string path, object obj);

        public T XmlDeserialize<T>(byte[] data) where T : XmlBase;


        public byte[] Serialize(object data);
        public T Deserialize<T>(byte[] data) where T : class, new();


        public byte[] ProtoSerialize(object obj);
        public T ProtoDeserialize<T>(byte[] msg) where T : class;


        public List<T> CsvDeserialize<T>(byte[] data) where T : CsvBase;


        public List<T> ExcelDeserialize<T>(byte[] data) where T : ExcelBase;
    }
}