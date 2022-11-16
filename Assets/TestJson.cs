using LeeFramework.Cfg;


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
