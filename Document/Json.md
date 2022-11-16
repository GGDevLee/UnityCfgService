# [回到主页](https://github.com/GGDevLee/UnityCfgService)
# Json解决方案

### =================Json解决方案介绍=================

#### 类转Json，二进制Json转类，Json转类
#### 编辑器类转Json，编辑器Json转二进制

</br>

```csharp
[System.Serializable]
public class TestJson : JsonBase
{
    public string name;
    public int age;

    public override void TmpData()
    {
        name = "李慧霞";
        age = 23;
    }
}
```

- 编辑器类转Json

</br> 

#### 选中Json类右键：Data->类转Json

![输入图片说明](Res/%E7%B1%BB%E8%BD%ACJson.png)
![输入图片说明](Res/%E7%B1%BB%E8%BD%ACJson2.png)

- 编辑器Json转二进制

</br> 

#### 选中Json右键：Data->Json转二进制

![输入图片说明](Res/Json%E8%BD%AC%E4%BA%8C%E8%BF%9B%E5%88%B6.png)

</br> 

- 序列化Json

```csharp
TestJson jsonData = new TestJson()
{
	name = "123",
	age = 25
};

string jsonStr = CfgSvc.instance.JsonSerialize(jsonData);
```

- 反序列化Json

```csharp
TestJson json = CfgSvc.instance.JsonDeserialize<TestJson>(jsonStr);
```

- 反序列化二进制Json

```csharp
TextAsset jsonAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/TestJson.bytes");
TestJson json = CfgSvc.instance.JsonBinaryDeserilize<TestJson>(jsonAsset.bytes);
```
