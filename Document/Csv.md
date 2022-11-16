# [回到主页](https://github.com/GGDevLee/UnityCfgService)
# Csv解决方案

### =================Csv解决方案介绍=================

#### 类转Csv，二进制Csv转类
#### 编辑器类转Csv，编辑器Csv转二进制

</br>

- 创建Csv类结构

```csharp
using LeeFramework.Cfg;

[System.Serializable]
public class TestCsv : CsvBase
{
    [Csv("名字")]
    public string name;

    [Csv("年龄")]
    public int age;

    [Csv("年份")]
    public int year;

    [Csv("测试数组")]
    public string array;

    [Csv("是否高")]
    public bool isTall;

    public override void TmpData()
    {
        name = "李慧霞";
        age = 10;
        year = 2020;
        array = "10;20;30";
        isTall = false;
    }
}
```

- 编辑器类转Csv

</br> 

#### 选中Csv类右键：Data->类转Csv

![输入图片说明](Res/%E7%B1%BB%E8%BD%ACCsv.png)
![输入图片说明](Res/%E7%B1%BB%E8%BD%ACCsv2.png)

- 编辑器Csv转二进制

</br> 

#### 选中Csv右键：Data->Csv转二进制

![输入图片说明](Res/Csv%E8%BD%AC%E4%BA%8C%E8%BF%9B%E5%88%B6.png)

</br> 

- 反序列化二进制Csv

```csharp
TextAsset csvAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/BinaryData/TestCsv.bytes");

List<TestCsv> csvFromByte = CfgSvc.instance.CsvDeserialize<TestCsv>(csvAsset.bytes);
```

- 反序列化明文Csv

```csharp
TextAsset csvAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/CsvData/TestCsv.csv");

List<TestCsv> csvFromFile = CfgSvc.instance.CsvDeserializeFile<TestCsv>(csvAsset.bytes);
```
