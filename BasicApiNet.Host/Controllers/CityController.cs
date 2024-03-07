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
    
    [HttpPost]
    public async Task<ActionResult> Create(City city,
        CancellationToken cancellationToken)
    {
        try
        {
            await _service.CreateCity(city);
            return Ok(true);
        }
        catch (Exception e)
        {
            return Ok(false);
        }

    }
    
    [HttpGet]
    public async Task<ActionResult<City>> GetById(int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _service.FindCityById(id);
            return Ok(response);
        }
        catch (Exception e)
        {
            return Ok(false);
        }
    }
    
    [HttpPut]
    public async Task<ActionResult> Update(CityDto city,
        CancellationToken cancellationToken)
    {
        try
        {
            await _service.UpdateCity(city);
            return Ok(true);
        }
        catch (Exception e)
        {
            return Ok(false);
        }
    }
    [HttpDelete]
    public async Task<ActionResult> Delete(int id,
        CancellationToken cancellationToken)
    {
        try
        {
            _service.DeleteCityById(id);
            return Ok(true);
        }
        catch (Exception e)
        {
            return Ok(false);
        }
    }
    
}