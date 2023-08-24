using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCRUD.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartametoController : ControllerBase
    {
         private readonly CompanyContext _dbContext;

        public DepartametoController(CompanyContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<DepartamentoDTO>>();
            var listaDepartamentoDTO = new List<DepartamentoDTO>();

            try
            {
                foreach(var item in await _dbContext.Departamentos.ToListAsync())
                {
                    listaDepartamentoDTO.Add(new DepartamentoDTO
                    {
                        IdDepartamento=item.IdDepartamento,
                            Nombre=item.Nombre,
                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.Value = listaDepartamentoDTO; 

            }catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje=ex.Message;
            }
            return Ok(responseApi); 
        }
    }
}
