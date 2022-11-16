using System;

namespace LeeFramework.Cfg
{
    [Serializable]
    public abstract class CsvBase
    {
        public abstract void TmpData();

        public virtual void Init()
        {

        }
    }

    public class CsvAttribute : Attribute
    {
        public string des => _Des;
        private string _Des;

        public CsvAttribute(string des)
        {
            _Des = des;
        }

    }
}
