using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Tools.GameTools
{
    public class PlayerPrefsSingleton : Singleton<PlayerPrefsSingleton>
    { 
        public bool IsAnyDataLoaded { private set; get; } = false;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        private const string PRENAME= "RestTest.PlayerPrefsDev.";
#else
        private const string PRENAME= "RestTest.PlayerPrefs.";
#endif

        public void Initialize()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            Debugger.LogBold("Player Prefs loaded");
#endif
        }

        public void Reset()
        {
            PlayerPrefs.Save();
        }
        private T Load<T>(string valueName, T defaultValue = default)
        {
            var fullPath = PRENAME + valueName;

            var str = PlayerPrefs.GetString(fullPath, default);
            if (!string.IsNullOrEmpty(str))
            {
                IsAnyDataLoaded = true;
                try
                {
                    return JsonConvert.DeserializeObject<T>(str);

                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }

            return defaultValue;
        }
        

        private void Save(string valueName, object value)
        {
            try
            {
                PlayerPrefs.SetString(PRENAME + valueName,  JsonConvert.SerializeObject(value));
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            PlayerPrefs.Save();
        }
        
        private void Delete(string valueName)
        {
            try
            {
                PlayerPrefs.DeleteKey(PRENAME + valueName);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}