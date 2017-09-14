using System.Web;

namespace GlossaryWebUI.Helpers
{
    public interface ISessionHelper
    {
        T GetValue<T>(string key);

        T GetValueAndRemove<T>(string key);

        void RemoveAllKeys();
        void RemoveKey(string key);
        void SetValue(string key, object value);
    }

    public class SessionHelper : ISessionHelper
    {
        public T GetValue<T>(string key)
        {
            var data = HttpContext.Current.Session[key];
            if (data != null)
                return (T)data;
            return default(T);
        }

        public T GetValueAndRemove<T>(string key)
        {
            var data = GetValue<T>(key);

            RemoveKey(key);

            return data;
        }

        /// <summary>
        /// Add object to session
        /// </summary>
        /// <param name="key">The key to use</param>
        /// <param name="value">The object to save</param>
        /// <remarks>The object to save and if there is a derived class and all property types need to have the [Serializable] attribute</remarks>
        public void SetValue(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public void RemoveKey(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public void RemoveAllKeys()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}