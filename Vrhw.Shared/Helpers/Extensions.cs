using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Vrhw.Shared.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Gets a property from an anonimous object.
        /// </summary>
        /// <param name="obj">The anonimous object.</param>
        /// <param name="porperty">The porperty to get.</param>
        /// <returns>Returns the object in the property.</returns>
        public static object GetProperty(this object obj, string porperty)
        {
            return obj.GetType().GetProperty(porperty).GetValue(obj, null);
        }

        /// <summary>
        /// Gets a string property from an anonimous object.
        /// </summary>
        /// <param name="obj">The anonimous object.</param>
        /// <param name="porperty">The porperty to get.</param>
        /// <returns>Returns the string in the property.</returns>
        public static string GetStrProperty(this object obj, string porperty)
        {
            return obj.GetType().GetProperty(porperty).GetValue(obj, null).ToString();
        }

        /// <summary>
        /// Verifies if a string is a valid Base64 string.
        /// </summary>
        /// <param name="base64String">The Base64 string.</param>
        /// <returns>Returns if a string is a valid Base64 string.</returns>
        public static bool IsBase64String(this string base64String)
        {
            base64String = base64String.Trim();
            return (base64String.Length % 4 == 0) && Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        /// <summary>
        /// Serialize an object into Json.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>Returns a Json serialized object.</returns>
        public static StringContent ConvetToJson(this object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Gets the value of a Property fron a Json serialized object
        /// </summary>
        /// <param name="obj">The Json object</param>
        /// <param name="property">The property</param>
        /// <returns></returns>
        public static string GetJsonProperty(this object obj, string property)
        {
            JObject jobject = JObject.Parse(obj.ToString());
            return (string)jobject[property];
        }
    }
}