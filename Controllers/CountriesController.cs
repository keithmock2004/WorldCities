using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorldCities.Data;
using WorldCities.Data.Models;
namespace WorldCities.Controllers


{
    [Route("api/[controller]")]
    [ApiController]


    public class CountriesController : Controller

    {
        private readonly ApplicationDbContext _context;
        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);


        }

       


      [HttpPost]
[Route("IsDupeField")]
public bool IsDupeField(
 int countryId,
 string fieldName,
 string fieldValue)
        {
            // Default approach (using strongly-typed LAMBA expressions)
            //switch (fieldName)
            //{
            // case "name":
            // return _context.Countries.Any(c => c.Name == fieldValue);
            // case "iso2":
            // return _context.Countries.Any(c => c.ISO2 == fieldValue);
            // case "iso3":
            // return _context.Countries.Any(c => c.ISO3 == fieldValue);
            // default:
            // return false;
            //}
            // Alternative approach (using System.Linq.Dynamic.Core)
            return (ApiResult<Country>.IsValidProperty(fieldName, true))
            ? _context.Countries.Any(
            String.Format("{0} == @0 && Id != @1", fieldName),
            fieldValue,
            countryId)
            : false;
        }


        [HttpGet]
        public async Task<ActionResult<ApiResult<Country>>> GetCountries(
 int pageIndex = 0,
 int pageSize = 10,
 string sortColumn = null,
 string sortOrder = null,
 string filterColumn = null,
 string filterQuery = null)
        {
            return await ApiResult<Country>.CreateAsync(
            _context.Countries,
            pageIndex,
            pageSize,
            sortColumn,
            sortOrder,
            filterColumn,
            filterQuery);
        }
    }
}
