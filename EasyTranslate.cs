using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace EasyTranslate
{
    public class EasyTranslate
    {
        public static string env = null;
        public static string loc = null;
        private static bool hasInit = false;
        private static Dictionary<string,string> locales = null;
        private static string assDir = null;
        private static Dictionary<string,string> current_translation = new Dictionary<string, string>();

        static EasyTranslate()
        {
            assDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public bool Init(string environement, string locale = "EN_en")
        {
            env = environement;
            loc = locale;
            if(env != null && loc != null)
            {
                if (!Directory.Exists(assDir + "/" + env + "/"))
                {
                    Directory.CreateDirectory(assDir + "/" + env + "/");
                }
                if (File.Exists(assDir + "/" + env + "/" + loc + "/translation.txt"))
                {
                    current_translation = translationToDicitionary(File.ReadAllText(assDir + "/" + env + "/" + loc + "/translation.txt"));
                }
                hasInit = true;
            }
            return hasInit;
        }

        public bool setLocales(Dictionary<string,string> dict)
        {
            locales = dict;
            if (locales != null) return true;
            return false;
        }

        public bool SetupKeys(Dictionary<string, string> dict)
        {
            if (!hasInit || locales == null) return false;
            foreach(var dict_locale in locales)
            {
                if (!Directory.Exists(assDir + "/" + env + "/" + dict_locale.Key + "_" + dict_locale.Value + "/"))
                {
                    Directory.CreateDirectory(assDir + "/" + env + "/" + dict_locale.Key + "_" + dict_locale.Value + "/");
                }

                Dictionary<string,string> locale_translation = new Dictionary<string, string>();
                if (File.Exists(assDir + "/" + env + "/" + dict_locale.Key + "_" + dict_locale.Value + "/translation.txt"))
                {
                    locale_translation = translationToDicitionary(File.ReadAllText(assDir + "/" + env + "/" + dict_locale.Key + "_" + dict_locale.Value + "/translation.txt"));
                }
                foreach (var translation in dict)
                {
                    if (locale_translation.ContainsKey(translation.Key)) continue;
                    locale_translation.Add(translation.Key,translation.Value);
                }
                File.WriteAllText(assDir + "/" + env + "/" + dict_locale.Key + "_" + dict_locale.Value + "/translation.txt", DictionaryToTranslation(locale_translation));
            }
            Init(env,loc);
            return true;
        }

        public String getTranslation(string key, string defaultVal = "")
        {
            string out_val = defaultVal;
            if (current_translation.TryGetValue(key, out out_val)) return out_val;
            //todo get translation
            return defaultVal;
        }


        private Dictionary<string, string> translationToDicitionary(string text)
        {
            Dictionary<string, string> temp_translation = new Dictionary<string, string>();
            foreach (var val in text.Split(new string[] { Environment.NewLine + "=NL=" + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var pairs = val.Split(new string[] { "=/=" }, StringSplitOptions.None);
                if(pairs[0] != null && pairs[1] != null) temp_translation.Add(pairs[0], pairs[1].Trim());
            }
            return temp_translation;
        }

        private string DictionaryToTranslation(Dictionary<string, string> dict)
        {
            string temp_translation = "";
            foreach (var translation in dict)
            {
                temp_translation += (translation.Key +  "=/="  + translation.Value + Environment.NewLine + "=NL=" + Environment.NewLine);
            }
            return temp_translation;
        }

    }
}
