using BasicApiNet.Core.Dtos;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicApiNet.Host.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICommonService<Country> _service;

    public CountryController(ICommonService<Country> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Country>>> GetAll(CancellationToken cancellationToken)
    {
        var response = _service.GetAll().ToList();
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(Country country,
        CancellationToken cancellationToken)
    {
        try
        {
            _service.Create(country);
            return Ok(true);
        }
        catch (Exception e)
        {
            return Ok(false);
        }

    }
    [HttpPost]
    public async Task<ActionResult> CreateWithCities([FromBody] CountryWithCitiesRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            //La logicas de negocio para crear un pais con ciudades deberia ir en un manager service.
            request.Country.Cities.AddRange(request.Cities);
            
            _service.Create(request.Country);
            
            return Ok(true);
        }
        catch (Exception e)
        {
            return Ok(false);
        }

    }
    [HttpPut]
    public async Task<ActionResult> Update(Country country,
        CancellationToken cancellationToken)
    {
        try
        {
            _service.Update(country);
            return Ok(true);
        }
        catch (Exception e)
        {
            return Ok(false);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetById(int id,
        CancellationToken cancellationToken)
    {
        var response = _service.FinById(id);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var country = _service.FinById(id);
            _service.Delete(country);
            return Ok(true);
        }
        catch (Exception e)
        {
            return Ok(false);
        }

    }
}