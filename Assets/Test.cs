using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeeFramework.Cfg;
using UnityEditor;
using System;
using System.Text;
using System.IO;

public class Test : MonoBehaviour
{
    [System.Serializable]
    class Apple
    {
        public int apple;
        public int count;
    }
    private void Start()
    {
        #region MyRegion
        //TestJson jsonData = new TestJson()
        //{
        //    name = "123",
        //    age = 25
        //};

        //TextAsset jsonAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TestJson.bytes");
        //TestJson json = CfgSvc.instance.JsonBinaryDeserilize<TestJson>(jsonAsset.bytes);

        //string jsonStr = CfgSvc.instance.JsonSerialize(jsonData);

        //TextAsset jsonAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TestJson.json");
        //TestJson json = CfgSvc.instance.JsonDeserialize<TestJson>(jsonStr);
        //Debug.Log("Json : " + json.age);
        //Debug.Log("Json : " + json.name);
        //TestXml xmlData = new TestXml();
        //xmlData.TmpData();
        //CfgSvc.instance.XmlSerialize(Application.persistentDataPath + "/TestXml.xml", xmlData);
        //TextAsset xmlAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TestXml.xml");
        //TestXml xml = CfgSvc.instance.XmlDeserialize<TestXml>(xmlAsset.bytes);

        //Debug.Log("Xml : " + xml.age);
        //Debug.Log("Xml : " + xml.name);

        //foreach (XmlBBB item in xml.list)
        //{
        //    Debug.Log("XmlBBB : " + item.uiName);
        //    Debug.Log("XmlBBB : " + item.uiPath);
        //    Debug.Log("XmlBBB : " + item.uiPanelName);
        //}

        //Apple apple = new Apple();
        //apple.apple = 100;
        //apple.count = 20;

        //byte[] appleData = CfgSvc.instance.Serialize(apple);

        //Apple appleDe = CfgSvc.instance.Deserialize<Apple>(appleData);
        //Debug.Log("Apple : " + appleDe.apple);
        //Debug.Log("Apple : " + appleDe.count);

        //TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TestCsv.csv");
        //List<TestCsv> list = CfgSvc.instance.CsvDeserialize<TestCsv>(textAsset.bytes);

        //foreach (TestCsv item in list)
        //{
        //    Debug.Log(item.name);
        //    Debug.Log(item.year);
        //    Debug.Log(item.age);
        //    Debug.Log(item.array);
        //}

        //TextAsset textAsset2 = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TestExcel.xlsx.txt");
        //List<TestExcel> list2 = CfgSvc.instance.ExcelDeserialize<TestExcel>(textAsset2.bytes);

        //foreach (TestExcel item in list2)
        //{
        //    Debug.Log(item.name);
        //    Debug.Log(item.gender);
        //    Debug.Log(item.age);
        //} 
        #endregion

        Apple apple = new Apple()
        {
            apple = 100,
            count = 200
        };

        //byte[] data = CfgSvc.instance.Serialize(apple);
        
        TestProtoBuf buf = new TestProtoBuf()
        {
            name = "Name",
            age = 23
        };

        for (int i = 0; i < 10; i++)
        {
            buf.allData.Add(i * 10);
        }

        for (int i = 0; i < 100; i++)
        {
            buf.allStr.Add(i.ToString());
        }

        byte[] bufData = CfgSvc.instance.ProtoSerialize(buf);
        byte[] binaryData = CfgSvc.instance.Serialize(buf);
        byte[] jsonData = Encoding.UTF8.GetBytes(CfgSvc.instance.JsonSerialize(buf));

        Debug.Log("Buf Data : " + bufData.Length);
        Debug.Log("Binary Data : " + binaryData.Length);
        Debug.Log("Json Data : " + jsonData.Length);

        TestProtoBuf testProto = CfgSvc.instance.ProtoDeserialize<TestProtoBuf>(bufData);

        Debug.Log("Buf Data : " + testProto.age);
        Debug.Log("Buf Data : " + testProto.name);

        foreach (int item in testProto.allData)
        {
            Debug.Log("Buf allData : " + item);
        }

        foreach (string item in testProto.allStr)
        {
            Debug.Log("Buf allStr : " + item);
        }


        TextAsset csvByte = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TmpData/TestCsv.bytes");
        TextAsset csvFile = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TmpData/TestCsv.csv");

        List<TestCsv> csvFromByte = CfgSvc.instance.CsvDeserialize<TestCsv>(csvByte.bytes);

        foreach (TestCsv data in csvFromByte)
        {
            Debug.Log("csvFromByte : " + data.age);
            Debug.Log("csvFromByte : " + data.name);
        }

        List<TestCsv> csvFromFile = CfgSvc.instance.CsvDeserializeFile<TestCsv>(csvFile.bytes);

        foreach (TestCsv data in csvFromFile)
        {
            Debug.Log("csvFromFile : " + data.age);
            Debug.Log("csvFromFile : " + data.name);
        }

        TextAsset excelByte = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TmpData/TestExcel.bytes");
        List<TestExcel> excelFromByte = CfgSvc.instance.ExcelDeserialize<TestExcel>(excelByte.bytes);

        foreach (TestExcel item in excelFromByte)
        {
            Debug.Log("excelFromByte : " + item.name);
            Debug.Log("excelFromByte : " + item.age);
            Debug.Log("excelFromByte : " + item.gender);
        }

       
        //TextAsset excelFile = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TmpData/TestExcel.xlsx");
        List<TestExcel> excelFromFile = CfgSvc.instance.ExcelDeserializeFile<TestExcel>(File.ReadAllBytes(Application.dataPath + "/TmpData/TestExcel.xlsx"));
        foreach (TestExcel item in excelFromFile)
        {
            Debug.Log("excelFromFile : " +item.name);
            Debug.Log("excelFromFile : " + item.age);
            Debug.Log("excelFromFile : " + item.gender);
        }

        TextAsset xmlByte = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TmpData/TestXml.bytes");
        TextAsset xmlFile = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TmpData/TestXml.xml");
        TestXml xml = CfgSvc.instance.XmlDeserializeFile<TestXml>(xmlByte.bytes);

        Debug.Log("Xml : " + xml.name);
        Debug.Log("Xml : " + xml.age);
        Debug.Log("Xml : " + xml.year);

        foreach (int item in xml.apple)
        {
            Debug.Log("Xml Apple  : " + item);
        }

        foreach (XmlBBB item in xml.list)
        {
            Debug.Log("Xml BBB  : " + item.uiName);
            Debug.Log("Xml BBB  : " + item.uiPanelName);
            Debug.Log("Xml BBB  : " + item.uiPath);
            Debug.Log("Xml BBB  : " + item.instanceId);
        }

    }
}