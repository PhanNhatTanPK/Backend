using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Backend.Data;
using Backend.Services;
using Backend.Models;

namespace TRANGTRAICHANNUOI.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TrangTraiHeoController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly ILogger<TrangTraiHeoController> _logger;
        private TrangTraiContext _dbContext;
        private readonly TrangTraiService _trangTraiService;
        public TrangTraiHeoController(ILogger<TrangTraiHeoController> logger,
            TrangTraiContext dbContext,
            IWebHostEnvironment webHostEnvironment, TrangTraiService trangTraiService)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _dbContext = dbContext;
            _trangTraiService = trangTraiService;
        }
        
        [HttpGet("GetPigFarms")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IEnumerable<object>> GetPigFarms()
        {
            return await _dbContext.TrangTraiHeo.Where(x => x.IsDeleted != true)
                .Select(x => new {
                    x.Id,
                    x.TenTrai,
                    x.ChuTrangTrai,
                    x.LongitudeNumber,
                    x.LatitudeNumber,
                    x.DienThoai,
                    x.DiaChi,
                    x.IdLoaiTrangTrai,
                    x.QuyMo,
                    x.DistrictId,
                    x.SoDayChuongKin,
                    x.SoDayChuongHo
                })
                .ToListAsync();
        }

        [HttpGet("LoaiBenhHeo")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IEnumerable<LoaiBenhHeo>> LoaiBenhHeo()
        {
            return await _dbContext.LoaiBenhHeo.ToListAsync();
        }

        [HttpPost("AddTrangTraiHeo")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddTrangTraiHeo([FromBody] TrangTraiHeo addMapRequest)
        {
            addMapRequest.LongitudeNumber = _trangTraiService.CoordinateToDecimal(addMapRequest.Longitude);
            addMapRequest.LatitudeNumber = _trangTraiService.CoordinateToDecimal(addMapRequest.Latitude);

            if (ModelState.IsValid)
            {
                _dbContext.TrangTraiHeo.Add(addMapRequest);
                await _dbContext.SaveChangesAsync();
                return Ok(addMapRequest);
            }

            return Ok(addMapRequest);
        }

        [HttpPost("AddTinhHinhDichTraiHeo")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddTinhHinhDichTraiHeo([FromBody] TinhHinhDichBenhTraiHeo model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.TinhHinhDichBenhTraiHeo.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest(model);
        }

        [HttpPost("ThemTiemPhongDichTraiHeo")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> ThemTiemPhongDichTraiHeo([FromBody] TiemPhongTrangTraiHeo model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.TiemPhongTrangTraiHeo.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest(model);
        }

		[HttpPost("GetTiemPhongTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> GetTiemPhongTTHeo(Guid id)
        {
            if (id == Guid.Empty)
                return View(new TrangTraiHeo());
            else
            {
                var trangTraiHeo = await _dbContext.TiemPhongTrangTraiHeo.Where(x => x.IdTrangTraiHeo == id).ToListAsync();
                if (trangTraiHeo == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiHeo);
            }
        }

        [HttpGet("ThongKeTrangTrai/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> ThongKeTrangTrai(long id)
        {
            if (id == null)
                return BadRequest();
            else
            {
                var trangTraiHeo = await _dbContext.TrangTraiHeo.Where(x => x.DistrictId == id && x.IsDeleted != true).ToListAsync();
                if (trangTraiHeo == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiHeo);
            }
        }

        [HttpGet("TinhHinhDichTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> TinhHinhDichTTHeo(Guid id)
        {
            if (id == Guid.Empty)
                return View(new TrangTraiHeo());
            else
            {
                var trangTraiGiaSuc = await _dbContext.TinhHinhDichBenhTraiHeo.Where(x => x.IdTrangTraiHeo == id).ToListAsync();
                if (trangTraiGiaSuc == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaSuc);
            }
        }

		[HttpGet("AddOrEdit/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddOrEdit(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();
            else
            {
                var trangTraiHeo = await _dbContext.TrangTraiHeo.FirstOrDefaultAsync(x => x.Id == id);
                if (trangTraiHeo == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiHeo);
            }
        }

        [HttpPut("AddOrEdit")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddOrEdit([FromBody] TrangTraiHeo model)
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
            var map = await _dbContext.TrangTraiHeo.FindAsync(id);
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
                    _dbContext.TrangTraiHeo.Update(map);
                    await _dbContext.SaveChangesAsync();
                    return Ok("Xóa trang trại thành công");
                }
                return BadRequest("Lỗi");
            }
            catch
            {
                return BadRequest("Không xóa được trang trại");
            }
        }

        [HttpGet("XoaTiemPhongTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> XoaTiemPhongTTHeo(Guid id)
        {
            var map = await _dbContext.TiemPhongTrangTraiHeo.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.TiemPhongTrangTraiHeo.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpGet("XoaTinhHinhDichTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> XoaTinhHinhDichTTHeo(Guid id)
        {
            var map = await _dbContext.TinhHinhDichBenhTraiHeo.FindAsync(id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.TinhHinhDichBenhTraiHeo.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpGet("GetATDBTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> GetATDBTTHeo(Guid id)
        {
            if (id == Guid.Empty)
                return View(new TrangTraiHeo());
            else
            {
                var trangTraiHeo = await _dbContext.CNATDBTTHeo.Where(x => x.IdTrangTraiHeo == id).ToListAsync();
                if (trangTraiHeo == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiHeo);
            }
        }

        [HttpPost("AddATDBTTHeo")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddATDBTTHeo([FromBody] CNATDBTTHeo model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.CNATDBTTHeo.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest(model);
        }

        [HttpGet("DeleteATDBTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> DeleteATDBTTHeo(Guid id)
        {
            var map = await _dbContext.CNATDBTTHeo.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNATDBTTHeo.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

		[HttpGet("GetDKCNTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> GetDKCNTTHeo(Guid id)
        {
            if (id == Guid.Empty)
                return View(new TrangTraiHeo());
            else
            {
                var trangTraiHeo = await _dbContext.CNDKCNTTHeo.Where(x => x.IdTrangTraiHeo == id).ToListAsync();
                if (trangTraiHeo == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiHeo);
            }
        }

        [HttpPost("AddDKCNTTHeo")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddDKCNTTHeo([FromBody] CNDKCNTTHeo model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.CNDKCNTTHeo.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest(model);
        }

        [HttpGet("DeleteDKCNTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> DeleteDKCNTTHeo(Guid id)
        {
            var map = await _dbContext.CNDKCNTTHeo.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNDKCNTTHeo.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
		
        [HttpGet("GetVietGAHPTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> GetVietGAHPTTHeo(Guid id)
        {
            if (id == Guid.Empty)
                return View(new TrangTraiHeo());
            else
            {
                var trangTraiHeo = await _dbContext.CNVietGAHPTTHeo.Where(x => x.IdTrangTraiHeo == id).ToListAsync();
                if (trangTraiHeo == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiHeo);
            }
        }

        [HttpPost("AddVietGAHPTTHeo")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddVietGAHPTTHeo([FromBody] CNVietGAHPTTHeo model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.CNVietGAHPTTHeo.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest(model);
        }
        
        [HttpGet("DeleteVietGAHPTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> DeleteVietGAHPTTHeo(Guid id)
        {
            var map = await _dbContext.CNVietGAHPTTHeo.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNVietGAHPTTHeo.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
		
        [HttpGet("GetVSTPTTHeo/{id}")]    
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> GetVSTPTTHeo(Guid id)
        {
            if (id == Guid.Empty)
                return View(new TrangTraiHeo());
            else
            {
                var trangTraiHeo = await _dbContext.CNVSTPTTHeo.Where(x => x.IdTrangTraiHeo == id).ToListAsync();
                if (trangTraiHeo == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiHeo);
            }
        }

        [HttpPost("AddVSTPTTHeo")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddVSTPTTHeo([FromBody] CNVSTPTTHeo model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.CNVSTPTTHeo.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest(model);
        }

        [HttpGet("DeleteVSTPTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> DeleteVSTPTTHeo(Guid id)
        {
            var map = await _dbContext.CNVSTPTTHeo.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNVSTPTTHeo.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

		[HttpPost("AddDichTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> AddDichTTHeo(Guid id, [FromBody]Dich model)
        {
            var farm = _dbContext.TrangTraiHeo.Find(id);
            farm.IsDich = "1";
            _dbContext.Dich.Add(model);
            _dbContext.SaveChanges();
            return Ok(farm);
        }

        [HttpGet("CheckDichTTHeo/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> CheckDichTTHeo(Guid id) {
            if (id == Guid.Empty)
                return NotFound();
            else
            {
                var farm = await _dbContext.Dich.Where(x => x.IdTrangTrai == id && x.IsDeleted != true).ToListAsync();
                if (farm == null)
                {
                    return NotFound();
                }
                var lastFarm = farm.LastOrDefault();
                return Ok(lastFarm);
            }
        }
        
        [HttpPost("KetThucDich/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> KetThucDich(Guid id, [FromBody] TinhHinhDichBenhTraiHeo model)
        {
            var dich = await _dbContext.Dich.Where(x => x.IdTrangTrai == id && x.IsDeleted != true).ToListAsync();
            var lastFarmDich = dich.LastOrDefault();
            lastFarmDich.IsDeleted = true;
            var farm = _dbContext.TrangTraiHeo.Find(id);
            farm.IsDich = "0";
            model.ThoiDiemKetThuc = DateTime.Now;
            _dbContext.TinhHinhDichBenhTraiHeo.Add(model);
            _dbContext.SaveChangesAsync();
            return Ok(farm);
        }

    }
}
