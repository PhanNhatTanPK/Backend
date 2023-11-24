using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace TRANGTRAICHANNUOI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CoSoGietMoController : Controller
    {
        private readonly ILogger<CoSoGietMoController> _logger;
        private TrangTraiContext _dbContext;
        private readonly TrangTraiService _trangTraiService;
        public CoSoGietMoController(ILogger<CoSoGietMoController> logger,
            TrangTraiContext dbContext,
            TrangTraiService trangTraiService)
        {
            
            _logger = logger;
            _dbContext = dbContext;
            _trangTraiService = trangTraiService;
        }

        [HttpGet("LoadCoSoGietMo")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IEnumerable<object>> LoadCoSoGietMo()
        {
            return await _dbContext.CoSoGietMo.Where(x=> x.IsDeleted != true)
                .Select(x => new {
                    x.Id,
                    x.TenTrai,
                    x.ChuTrangTrai,
                    x.LongitudeNumber,
                    x.LatitudeNumber,
                    x.DienThoai,
                    x.DiaChi,
                    x.IdLoaiTrangTrai,
                    x.DistrictId
                })
                .ToListAsync();
        }

        [HttpGet("ThongKeTrangTrai/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> ThongKeTrangTrai(long id)
        {
            if (id == null)
                return BadRequest();
            else
            {
                var csGietMo = await _dbContext.CoSoGietMo.Where(x => x.DistrictId == id && x.IsDeleted != true).ToListAsync();
                if (csGietMo == null)
                {
                    return NotFound();
                }
                return Ok(csGietMo);
            }
        }

        [HttpPost("AddCoSoGietMo")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddCoSoGietMo([FromBody] CoSoGietMo addMapRequest)
        {
            addMapRequest.LongitudeNumber = _trangTraiService.CoordinateToDecimal(addMapRequest.Longitude);
            addMapRequest.LatitudeNumber = _trangTraiService.CoordinateToDecimal(addMapRequest.Latitude);
            if (ModelState.IsValid)
            {
                await _dbContext.CoSoGietMo.AddAsync(addMapRequest);
                await _dbContext.SaveChangesAsync();
                return Ok(addMapRequest);
            }
            return View(addMapRequest);
        }

        //Get Info by Id
        [HttpGet("AddOrEdit/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddOrEdit(Guid id)
        {
            if (id == Guid.Empty)
                return View(new CoSoGietMo());
            else
            {
                var coSoGietMo = await _dbContext.CoSoGietMo.FirstOrDefaultAsync(x => x.Id == id);
                if (coSoGietMo == null)
                {
                    return NotFound();
                }
                return Ok(coSoGietMo);
            }
        }

        [HttpPost("AddOrEdit")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddOrEdit([FromBody] CoSoGietMo model)
        {
            try
            {
                var userInfo = _trangTraiService.GetUserInfo(User.Identity.Name);
                if (userInfo == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid && userInfo == model.DistrictId || userInfo == 0)
                {
                    _dbContext.Update(model);
                    await _dbContext.SaveChangesAsync();
                    return Ok(model);
                }
                return BadRequest("Không thuộc huyện được quản lý");
            }
            catch
            {
                return BadRequest("Không cập nhật được trang trại");
            }
        }

        [HttpGet("Delete/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> Delete(Guid id)
        {
            var map = await _dbContext.CoSoGietMo.FindAsync(id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                var userInfo = _trangTraiService.GetUserInfo(User.Identity.Name);
                if (userInfo == map.DistrictId || userInfo == 0)
                {

                    map.IsDeleted = true;
                    _dbContext.CoSoGietMo.Update(map);
                    await _dbContext.SaveChangesAsync();
                    return Ok("Xóa trang trại thành công");
                }
                return BadRequest("Lỗi");
                
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}