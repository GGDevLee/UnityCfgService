# [回到主页](https://github.com/GGDevLee/UnityCfgService)
# Binary解决方案

### =================Binary解决方案介绍=================

#### 类对象序列化，反序列成类对象


</br>

- 创建Binary类结构

```csharp
[System.Serializable]
class Apple
{
    public int apple;
    public int count;
}
```

- 序列化对象

```csharp
Apple apple = new Apple()
{
    apple = 100,
    count = 200
};

byte[] data = CfgSvc.instance.Serialize(apple);
```

- 反序列化

```csharp
Apple apple = CfgSvc.instance.Deserialize<Apple>(data);
```
