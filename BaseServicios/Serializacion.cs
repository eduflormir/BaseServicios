using System;
using System.Web.Script.Serialization;

namespace BaseServicios
{
    public class Serializacion<T>
    {
        /// <summary>
        /// Tranforma de JSON a Objeto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Deserializar(String obj)
        {
            var ser = new JavaScriptSerializer();
            var data = ser.Deserialize<T>(obj);
            return data;
        }

        /// <summary>
        /// Tranforma de Objeto a String (Genera JSON)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String Serializar(T obj)
        {
            var ser = new JavaScriptSerializer();
            var data = ser.Serialize(obj);
            return data;
        }
    }
}