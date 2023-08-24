using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly HttpClient _http;

        public EmpleadoService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<EmpleadoDTO>> List()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<EmpleadoDTO>>>("api/Empleado/List");


            if (result!.EsCorrecto)
                return result.Value!;
            else
                throw new Exception(result.Mensaje);
        }


        public async Task<EmpleadoDTO> GetById(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<EmpleadoDTO>>($"api/Empleado/Get/{id}");

            if (result!.EsCorrecto)
                return result.Value!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> Guardar(EmpleadoDTO empleado)
        {
            var result = await _http.PostAsJsonAsync("api/Empleado/Get/save", empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Value!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Edit(EmpleadoDTO empleado)
        {
            var result = await _http.PutAsJsonAsync($"api/Empleado/Get/Edit/{empleado.IdEmpleado}", empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Value!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> DeleteById(int id)
        {
            var result = await _http.DeleteAsync($"api/Empleado/Get/Delete/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }


}

