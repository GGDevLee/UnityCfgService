# [回到主页](https://github.com/GGDevLee/UnityCfgService)
# Excel解决方案

### =================Excel解决方案介绍=================

#### 类转Excel，二进制Excel转类
#### 编辑器类转Excel，编辑器Excel转二进制

</br>

- 创建Excel类结构

```csharp
using LeeFramework.Cfg;

[System.Serializable]
public class TestExcel : ExcelBase
{
    [Excel("名字")]
    public string name;

    [Excel("年龄")]
    public int age;

    [Excel("性别")]
    public string gender;

    public override void TmpData()
    {
        name = "李慧霞";
        age = 20;
        gender = "Female";
    }
}
```

- 编辑器类转Excel

</br> 

#### 选中Excel类右键：Data->类转Excel

![输入图片说明](Res/%E7%B1%BB%E8%BD%ACExcel.png)
![输入图片说明](Res/%E7%B1%BB%E8%BD%ACExcel2.png)

- 编辑器Excel转二进制

</br> 

#### 选中Excel右键：Data->Excel转二进制

![输入图片说明](Res/Excel%E8%BD%AC%E4%BA%8C%E8%BF%9B%E5%88%B6.png)

</br> 

- 反序列化二进制Excel

```csharp
TextAsset excelAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/BinaryData/TestExcel.bytes");

List<TestCsv> excelFromByte = CfgSvc.instance.ExcelDeserialize<TestExcel>(excekAsset.bytes);
```

- 反序列化明文Excel

```csharp
TextAsset excelAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/ExcelData/TestExcel.xlsx");

List<TestExcel> excelFromFile = CfgSvc.instance.ExcelDeserializeFile<TestExcel>(excelAsset.bytes);
```
