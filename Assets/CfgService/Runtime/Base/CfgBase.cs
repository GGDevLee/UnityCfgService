namespace LeeFramework.Cfg
{
    public class CfgBase<T> where T : new()
    {
        private static T _Instance;
        public static T instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new T();
                }
                return _Instance;
            }
        }
    }
}
