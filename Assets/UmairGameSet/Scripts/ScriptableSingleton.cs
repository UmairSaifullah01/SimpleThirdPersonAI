using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UMGS
{
    public sealed class ScriptableSingleton: SingletonPersistent<ScriptableSingleton>
    {
        #region Fields

        [SerializeField] private CustomDic[] objects;


        #endregion

        #region Properties
        
        #endregion

        public ScriptableObject GetObject(string name)
        {
            return (from o in objects where o.key == null select o.element).FirstOrDefault();
        }
        
    }
    [Serializable]
    public class CustomDic
    {
        public string key;
        public ScriptableObject element;
    }
}

