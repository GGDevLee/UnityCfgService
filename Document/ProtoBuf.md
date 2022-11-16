# [回到主页](https://github.com/GGDevLee/UnityCfgService)
# ProtoBuf解决方案

### =================ProtoBuf解决方案介绍=================

#### 类对象序列化ProtoBuf数据，ProtoBuf数据反序列成类对象
#### 配合[TcpNetwork](https://gitee.com/GameDevLee/TcpNetwork)可高效进行网络传输


</br>

- 创建ProtoBuf类结构

```csharp
using ProtoBuf;
using System.Collections.Generic;

[System.Serializable, ProtoContract]
public class TestProtoBuf
{
    [ProtoMember(1)]
    public string name;

    [ProtoMember(2)]
    public int age;

    [ProtoMember(3)]
    public List<int> allData = new List<int>();

    [ProtoMember(4)]
    public List<string> allStr = new List<string>();
}
```

- 序列化ProtoBuf

```csharp
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
```

- 反序列化ProtoBuf

```csharp
TestProtoBuf testProto = CfgSvc.instance.ProtoDeserialize<TestProtoBuf>(bufData);
```
