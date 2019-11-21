using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CSScript
{
    public class Singleton<T> where T : class, new()
    {
        private static T _Instance;
        public static void CreateInstance()
        {
            if (Singleton<T>._Instance == null)
            {
                Singleton<T>._Instance = Activator.CreateInstance<T>();
            }
        }

        public static void DestoryInstance()
        {
            if(Singleton<T>._Instance != null)
            {
                Singleton<T>._Instance = null;
            }
        }

        public static T Instence
        {
            get
            {
                if (Singleton<T>._Instance == null)
                {
                    Singleton<T>._Instance = Activator.CreateInstance<T>();
                }
                return Singleton<T>._Instance;
            }
        }
    }
}
