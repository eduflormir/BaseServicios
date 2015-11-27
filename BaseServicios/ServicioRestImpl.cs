using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BaseServicios
{
    public class ServicioRestImpl<TModelo>: IServiciosRest<TModelo>
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


        public Task<TModelo> Add(TModelo model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TModelo model)
        {
            throw new NotImplementedException();
        }

        public List<TModelo> Get()
        {
            throw new NotImplementedException();
        }

        public TModelo Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<TModelo> Get(Dictionary<string, string> param)
        {
            throw new NotImplementedException();
        }

        public Task Update(TModelo model)
        {
            throw new NotImplementedException();
        }
    }
}