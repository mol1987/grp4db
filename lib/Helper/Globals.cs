using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    /// <summary>
    /// Global scope, reachable from everywhere. Is limited to strings for now
    /// </summary>
    public static class Globals
    {
        private static Dictionary<string, string> items = new Dictionary<string, string> { };
        
        /// <summary>
        /// Check if global value is set
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns True or False</returns>
        public static bool Exists(string key)
        {
            return items.ContainsKey(key);
        }
        /// <summary>
        /// Get stored string value
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns string</returns>
        public static string Get(string key)
        {
            return items[key];
        }
        /// <summary>
        /// Set value to global
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void Set(string key, string val)
        {
            items.Add(key, val);
        }
    }
}
