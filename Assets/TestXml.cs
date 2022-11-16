using LeeFramework.Cfg;
using System.Collections.Generic;
using System.Xml.Serialization;

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
    public string uiPanelName;//UI 所属的Panel名字

    [XmlAttribute]
    public string uiName;//UI 的名字

    [XmlAttribute]
    public string uiPath;//UI 相对Panel的路径

    [XmlAttribute]
    public string uiType;//UI 的类型

    [XmlAttribute]
    public int instanceId;//UI 的唯一标识  并且 重名后也不会变化

    public override void TmpData()
    {
        uiPanelName = "uiPanelName";
        uiName = "UIName";
        uiPath = "UIPath";
        uiType = "Button";
        instanceId = 1000;
    }
}