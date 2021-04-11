using UnityEngine;

namespace Komastar.Foundation
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>, new()
    {
        private static readonly object lockObj = new object();
        private static T instance = null;
        public static T Get()
        {
            lock (lockObj)
            {
                if (null == instance)
                {
                    string goName = typeof(T).ToString();
                    GameObject go = GameObject.Find(goName);
                    if (go == null)
                    {
                        go = new GameObject(goName);
                    }
                    instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }

                return instance;
            }
        }
    }
}
