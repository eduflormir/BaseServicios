using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseServicios
{
    public interface IServiciosRest<TModelo>
    {
        // operaciones para webservice con tareas asincronas

        Task<TModelo> Add(TModelo model);

        Task Update(TModelo model);

        Task Delete (TModelo model);

        List<TModelo> Get();

        List<TModelo> Get(Dictionary<String,String> param );

        TModelo Get(int id);
        
    }
}