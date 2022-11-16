# [回到主页](https://gitee.com/GameDevLee/CfgService)
# Xml解决方案

### =================Xml解决方案介绍=================

#### 类生成Xml，二进制Xml转类

</br>

```csharp
[System.Serializable]
public class TestXml : XmlBase
{
    [XmlAttribute]
    public string name;

    [XmlAttribute]
    public int age;

    [XmlAttribute]
    public int year;

    [XmlElement]
    public List<int> apple = new List<int>();

    [XmlElement]
    public List<XmlBBB> list = new List<XmlBBB>();

    public override void TmpData()
    {
        name = "李慧霞";
        age = 10;
        year = 23;
        apple.AddRange(new int[3] { 1, 2, 3 });

        XmlBBB bbb = new XmlBBB();
        bbb.TmpData();
        list.Add(bbb);
    }
}

[System.Serializable]
public class XmlBBB : XmlBase
{
    [XmlAttribute]
    public string uiPanelName;

    [XmlAttribute]
    public string uiName;

    [XmlAttribute]
    public string uiPath;

    [XmlAttribute]
    public string uiType;

    [XmlAttribute]
    public int instanceId;

    public override void TmpData()
    {
        uiPanelName = "uiPanelName";
        uiName = "UIName";
        uiPath = "UIPath";
        uiType = "Button";
        instanceId = 1000;
    }
}
```

- 序列化Xml，并写入

```csharp
TestXml xmlData = new TestXml();
xmlData.TmpData();
CfgSvc.instance.XmlSerialize(Application.persistentDataPath + "/TestXml.xml", xmlData);
```

- 反序列化二进制Xml

```csharp
TextAsset xmlAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/BinaryData/TestXml.bytes");

TestXml xml = CfgSvc.instance.XmlDeserialize<TestXml>(xmlByte.bytes);
```

- 反序列化明文Xml

```csharp
TextAsset xmlAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/XmlData/TestXml.xml");

TestXml xml = CfgSvc.instance.XmlDeserializeFile<TestXml>(xmlByte.bytes);
```

