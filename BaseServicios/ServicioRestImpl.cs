using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BaseServicios
{
    public class ServicioRestImpl<TModelo> : IServiciosRest<TModelo>
    {
        private String url; // url donde esta el servicio
        private bool auth; // indicar si este servicio esta autenticado o no
        private String user; // para indicar cual es el user
        private String pass; // para indicar cual es el pwd

        public ServicioRestImpl(String url, bool auth = false, String user = null, String pass = null)
        {
            this.url = url;
            this.auth = auth;
            this.user = user;
            this.pass = pass;
        }


        public async Task<TModelo> Add(TModelo model)
        {
            // await: bloquea hasta que termine
            var datos = Serializacion<TModelo>.Serializar(model);


            using (var handler = new HttpClientHandler())
            {
                // define la cabecera de autenticacion
                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }
                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(datos); // mi objeto serializado
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var r = await client.PostAsync(new Uri(url), contenido); // no cierra hasta que termine
                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fallo gordo");
                    var objSerializado = await r.Content.ReadAsStringAsync();
                    return Serializacion<TModelo>.Deserializar(objSerializado);
                }
            }
        }

        public async Task Delete(int id)
        {
            // await: bloquea hasta que termine
            //var datos = Serializacion<TModelo>.Serializar(id);
            using (var handler = new HttpClientHandler())
            {
                // define la cabecera de Autenticación
                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }
                using (var client = new HttpClient(handler))
                {
                    var r = await client.DeleteAsync(new Uri(url + "/" + id));

                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fallo Gordo");

                }
            }
        }

        public List<TModelo> Get(String paramUrl = null)
        {
            List<TModelo> lista;
            var urlDest = url;

            if (paramUrl != null)
                urlDest += paramUrl;

            var request = WebRequest.Create(urlDest);
            if (auth)
            {
                request.Credentials = new NetworkCredential(user, pass);
            }
            request.Method = "GET";
            var response = request.GetResponse();
            // el stream es la tuberia, es el canal que permite obtener datos
            // dentro lleva la respuesta
            // stream se usa para la secuencia de datos, 
            // son bytes, array de bytes

            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream)) // convierte
                {
                    var serializado = reader.ReadToEnd(); // lea desde actual posicion hasta al final
                    lista = Serializacion<List<TModelo>>.Deserializar(serializado);
                }
            }

            return lista;
        }
        /// <summary>
        /// Busqueda por clave primeria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TModelo Get(int id)
        {
            TModelo objeto;
            var request = WebRequest.Create(url + "/" + id);
            if (auth)
            {
                request.Credentials = new NetworkCredential(user, pass);
            }

            request.Method = "GET";
            var response = request.GetResponse();
            // el stream es la tuberia, es el canal que permite obtener datos
            // dentro lleva la respuesta
            // stream se usa para la secuencia de datos, 
            // son bytes, array de bytes

            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream)) // convierte
                {
                    var serializado = reader.ReadToEnd(); // lea desde actual posicion hasta al final
                    objeto = Serializacion<TModelo>.Deserializar(serializado);
                }
            }

            return objeto;
        }

        public List<TModelo> Get(Dictionary<string, string> param)
        {
            // parametro = valor
            var paramsurl = "";
            var primero = true;

            // esta funcionalidad podria quedar en un metodo de extension
            foreach (var key in param.Keys)
            {
                if (primero)
                {
                    paramsurl += "?";
                    primero = false;
                }
                else
                    paramsurl += "&";
                paramsurl += key + "=" + param[key];
            }

            return Get(paramsurl);
        }

        public async Task Update(TModelo model)
        {
            // await: bloquea hasta que termine
            var datos = Serializacion<TModelo>.Serializar(model);


            using (var handler = new HttpClientHandler())
            {
                // define la cabecera de auenticación
                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }
                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(datos); // mi objeto serializado
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var r = await client.PutAsync(new Uri(url), contenido); // no cierra hasta que termine
                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fallo gordo");
                }
            }

        }
    }
}