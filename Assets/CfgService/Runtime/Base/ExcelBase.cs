using System;

namespace LeeFramework.Cfg
{
    [Serializable]
    public abstract class ExcelBase
    {
        public virtual void Init()
        {

        }

        public abstract void TmpData();
    }

    public class ExcelAttribute : Attribute
    {
        public string des => _Des;
        private string _Des;

        public ExcelAttribute(string des)
        {
            _Des = des;
        }
    }
}
