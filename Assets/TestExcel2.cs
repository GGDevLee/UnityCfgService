using LeeFramework.Cfg;

[System.Serializable]
public class TestExcel2 : ExcelBase
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
