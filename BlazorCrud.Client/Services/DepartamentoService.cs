using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly HttpClient _http;

        public DepartamentoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<DepartamentoDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<DepartamentoDTO>>>("api/Departameto/List");


            if (result!.EsCorrecto)
                return result.Value!;
            else
                throw new Exception(result.Mensaje);
        }
    }
}
