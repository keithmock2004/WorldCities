using System;
using System.Collections.Generic;
using System.Linq;
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
