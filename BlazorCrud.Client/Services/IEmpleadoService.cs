using BlazorCrud.Shared;


namespace BlazorCrud.Client.Services
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDTO>> List();
        Task<EmpleadoDTO> GetById(int id);
        Task<int> Guardar(EmpleadoDTO empleado);
        Task<int> Edit(EmpleadoDTO empleado);
        Task <bool> DeleteById(int id);

       
    }
}
