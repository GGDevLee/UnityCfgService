using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeeFramework.Cfg;


[System.Serializable]
public class TestJson2 : JsonBase
{
    public string name;
    public int age;


    public override void TmpData()
    {
        name = "李慧霞2";
        age = 232;
    }
}
