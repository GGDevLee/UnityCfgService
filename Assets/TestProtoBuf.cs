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
