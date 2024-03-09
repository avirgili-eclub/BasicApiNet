using BasicApiNet.Core.Dtos;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicApiNet.Host.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly ICityService _service;

    public CityController(ICityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<City>>> GetAll(CancellationToken cancellationToken)
    {
        Task<IEnumerable<City?>> response = _service.GetAllCities();
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<ActionResult<City>> GetById(int id)
    {
        var response = await _service.FindCityById(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Create(City city)
    {
        await _service.CreateCity(city);
        return Ok(true);
    }
    

    [HttpPut]
    public async Task<ActionResult<bool>> Update(CityDto city)
    {
        await _service.UpdateCity(city);
        return Ok(true);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        //TODO: retornar boleando si efectivamente se elimino
        await _service.DeleteCityByIdAsync(id);
        return Ok(true);
    }
}