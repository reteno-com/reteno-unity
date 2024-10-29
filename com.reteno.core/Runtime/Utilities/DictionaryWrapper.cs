using System;
using System.Collections.Generic;

namespace Reteno.Utilities
{
    [Serializable]
    public class DictionaryWrapper
    {
        public List<string> keys = new List<string>();
        public List<string> values = new List<string>();

        public DictionaryWrapper(Dictionary<string, string> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public Dictionary<string, string> ToDictionary()
        {
            var dictionary = new Dictionary<string, string>();
            for (int i = 0; i < keys.Count; i++)
            {
                dictionary[keys[i]] = values[i];
            }
            return dictionary;
        }
    }
}