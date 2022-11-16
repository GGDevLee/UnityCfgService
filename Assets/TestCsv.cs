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