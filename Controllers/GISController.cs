using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class GISController : Controller
    {
        private readonly ILogger<GISController> _logger;
        private TrangTraiContext _dbContext;
       
     
        public GISController(ILogger<GISController> logger,
            TrangTraiContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("Huyen")]
        public async Task<IEnumerable<District>> Huyen()
        {
            return await _dbContext.District.ToListAsync();
        }      
    }
}