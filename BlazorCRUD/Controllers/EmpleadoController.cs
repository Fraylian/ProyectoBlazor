using BlazorCrud.Shared;
using BlazorCRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly CompanyContext _dbContext;

        public EmpleadoController(CompanyContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("List")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<EmpleadoDTO>>();
            var ListaEmpleadoDTOs = new List<EmpleadoDTO>();

            try
            {

                foreach (var item in await _dbContext.Empleados.Include(d => d.IdDepartamentoNavigation).ToListAsync())
                {
                    ListaEmpleadoDTOs.Add(new EmpleadoDTO
                    {
                        IdEmpleado = item.IdEmpleado,
                        NomrbreCompleto = item.NomrbreCompleto,
                        IdDepartamento = item.IdDepartamento,
                        Sueldo = item.Sueldo,
                        FechaContrato = item.FechaContrato,
                        Departamento = new DepartamentoDTO
                        {
                            IdDepartamento = item.IdDepartamentoNavigation.IdDepartamento,
                            Nombre = item.IdDepartamentoNavigation.Nombre
                        }
                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.Value = ListaEmpleadoDTOs;

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Get/{id}")]

        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<EmpleadoDTO>();
            var EmpleadoDTO = new EmpleadoDTO();

            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    EmpleadoDTO.IdEmpleado = dbEmpleado.IdEmpleado;
                    EmpleadoDTO.NomrbreCompleto = dbEmpleado.NomrbreCompleto;
                    EmpleadoDTO.IdDepartamento = dbEmpleado?.IdDepartamento;
                    EmpleadoDTO.Sueldo = dbEmpleado.Sueldo;
                    EmpleadoDTO.FechaContrato = dbEmpleado.FechaContrato;

                    responseApi.EsCorrecto = true;
                    responseApi.Value = EmpleadoDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }
            }

            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }


        [HttpPost]
        [Route("save")]

        public async Task<IActionResult> Guardar(EmpleadoDTO empleado)
        {
            var responseApi = new ResponseAPI<int>();
            var EmpleadoDTO = new EmpleadoDTO();

            try
            {
                var dbEmpleado = new Empleado
                {
                    NomrbreCompleto = empleado.NomrbreCompleto,
                    IdDepartamento = empleado.IdDepartamento,
                    Sueldo = empleado.Sueldo,
                    FechaContrato = empleado.FechaContrato,
                };

                _dbContext.Empleados.Add(dbEmpleado);
                await _dbContext.SaveChangesAsync();

                if (dbEmpleado.IdEmpleado != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Value = dbEmpleado.IdEmpleado;
                }
                else
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "No guardado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Edit/{id}")]

        public async Task<IActionResult> Editar(EmpleadoDTO empleado, int id)
        {
            var responseApi = new ResponseAPI<int>();
    

            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);


                if (dbEmpleado != null)
                {
                    dbEmpleado.NomrbreCompleto = empleado.NomrbreCompleto;
                    dbEmpleado.IdDepartamento = empleado.IdDepartamento;
                    dbEmpleado.Sueldo = empleado.Sueldo;
                    dbEmpleado.FechaContrato = empleado.FechaContrato;  

                    _dbContext.Empleados.Update(dbEmpleado);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                    responseApi.Value = dbEmpleado.IdEmpleado;
                }
                else
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Empleado no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("Delete/{id}")]
                                                                        
        public async Task<IActionResult> Eliminar ( int id)
                {
            var responseApi = new ResponseAPI<int>();


            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);


                if (dbEmpleado != null)
                {
                    _dbContext.Empleados.Remove(dbEmpleado);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                  
                }
                else
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Empleado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

    }
}
