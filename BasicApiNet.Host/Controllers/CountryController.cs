using BasicApiNet.Core.Dtos;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BasicApiNet.Host.Controllers;

[Route("api/[controller]/[action]")]
[Authorize]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICommonService<Country> _service;

    public CountryController(ICommonService<Country> service)
    {
        _service = service;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all countries", Description = "Retrieve all countries.")]
    [SwaggerResponse(200, "Success", typeof(IEnumerable<Country>))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public Task<IActionResult> GetAll()
    {
        var response = _service.GetAll().ToList();
        return Task.FromResult<IActionResult>(Ok(response));
    }
    
    /// <summary>
    /// Retrieve a country by its id.
    /// </summary>
    /// <param name="id">The id of the country to retrieve.</param>
    /// <returns>Returns the country with the specified id.</returns>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get country by id", Description = "Retrieve a country by id.")]
    [SwaggerResponse(200, "Success", typeof(Country))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public async Task<IActionResult> GetCountry(int id)
    {
        if (id is 0 or < 0)
        {
            return BadRequest("Invalid id");
        }

        try
        {
            var response = await _service.FinByIdAsync(id);
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create country", Description = "Create a country.")]
    [SwaggerResponse(200, "Success", typeof(bool))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public async Task<IActionResult> Create(Country country)
    {
        if (string.IsNullOrEmpty(country.Name) || string.IsNullOrWhiteSpace(country.Name))
        {
            return BadRequest("Invalid country");
        }

        try
        {
            await _service.CreateAsync(country);
            return Ok(true);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create countries with their cities(optional)",
        Description = "Create a country with its cities(optional).")]
    [SwaggerResponse(200, "Success", typeof(bool))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public async Task<IActionResult> CreateWithCities([FromBody] CountryWithCitiesRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Country.Name) || string.IsNullOrWhiteSpace(request.Country.Name))
        {
            return BadRequest("Invalid country");
        }

        try
        {
            //La logicas de negocio para crear un pais con ciudades deberia ir en un manager service.
            request.Country.Cities.AddRange(request.Cities);

            await _service.CreateAsync(request.Country);

            return Ok(true);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update country", Description = "Update a country.")]
    [SwaggerResponse(200, "Success", typeof(Country))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public async Task<IActionResult> Update(Country country)
    {
        if (country.Id == 0 || country.Id < 0 || string.IsNullOrEmpty(country.Name) ||
            string.IsNullOrWhiteSpace(country.Name))
        {
            return BadRequest("Invalid country");
        }

        try
        {
            var countryUpdated = await _service.UpdateAsync(country);
            return Ok(countryUpdated);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }
    

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete country by id", Description = "Delete a country by id.")]
    [SwaggerResponse(200, "Success", typeof(bool))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public async Task<IActionResult> Delete(int id)
    {
        if (id is 0 or < 0)
        {
            return BadRequest("Invalid id");
        }

        return Ok(await _service.DeleteById(id));
    }
}