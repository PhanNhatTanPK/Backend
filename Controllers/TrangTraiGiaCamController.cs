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
    public class TrangTraiGiaCamController : Controller
    {

        private readonly ILogger<TrangTraiGiaCamController> _logger;
        private TrangTraiContext _dbContext;
        private TrangTraiService _trangTraiService;
        public TrangTraiGiaCamController(ILogger<TrangTraiGiaCamController> logger,
            TrangTraiContext dbContext,
            TrangTraiService trangTraiService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _trangTraiService = trangTraiService;
        }

        [HttpGet("LoadTraiGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<object>> LoadTraiGiaCam()
        {
            return await _dbContext.TrangTraiGiaCam.Where(x => x.IsDeleted != true)
                .Select(x => new
                {
                    x.Id,
                    x.TenTrai,
                    x.ChuTrangTrai,
                    x.LongitudeNumber,
                    x.LatitudeNumber,
                    x.DienThoai,
                    x.DiaChi,
                    x.IdLoaiTrangTrai,
                    x.IdLoaiGiaCam,
                    x.QuyMo,
                    x.DistrictId,
                    x.SoDayChuongKin,
                    x.SoDayChuongHo
                })
                .ToListAsync();
        }

        [HttpGet("LoaiGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<LoaiGiaCam>> LoaiGiaCam()
        {
            return await _dbContext.LoaiGiaCam.ToListAsync();
        }

        [HttpGet("LoaiBenhGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<LoaiBenhGiaCam>> LoaiBenhGiaCam()
        {
            return await _dbContext.LoaiBenhGiaCam.ToListAsync();
        }

        [HttpGet("ThongKeTrangTrai/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ThongKeTrangTrai(long id)
        {
            if (id == null)
                return BadRequest();
            else
            {
                var ttGiaCam = await _dbContext.TrangTraiGiaCam.Where(x => x.DistrictId == id && x.IsDeleted != true).ToListAsync();
                if (ttGiaCam == null)
                {
                    return NotFound();
                }
                return Ok(ttGiaCam);
            }
        }

        [HttpPost("AddTrangTraiGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddTrangTraiGiaCam([FromBody] TrangTraiGiaCam addMapRequest)
        {
            addMapRequest.LongitudeNumber = _trangTraiService.CoordinateToDecimal(addMapRequest.Longitude);
            addMapRequest.LatitudeNumber = _trangTraiService.CoordinateToDecimal(addMapRequest.Latitude);
            if (ModelState.IsValid)
            {
                await _dbContext.TrangTraiGiaCam.AddAsync(addMapRequest);
                await _dbContext.SaveChangesAsync();
                return Ok(addMapRequest);
            }
            return View(addMapRequest);
        }

        [HttpPost("AddTinhHinhDichGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddTinhHinhDichGiaCam([FromBody] TinhHinhDichBenhGiaCam model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.TinhHinhDichBenhGiaCam.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest();
        }

        [HttpPost("ThemTiemPhongGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ThemTiemPhongGiaCam([FromBody] TiemPhongGiaCam model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.TiemPhongGiaCam.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest();
        }

        [HttpGet("GetTiemPhongTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetTiemPhongTTGiaCam(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaCam = await _dbContext.TiemPhongGiaCam.Where(x => x.IdTrangTraiGiaCam == id).ToListAsync();
                if (trangTraiGiaCam == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaCam);
            }
        }

        [HttpGet("TinhHinhDichTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> TinhHinhDichTTGiaCam(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaCam = await _dbContext.TinhHinhDichBenhGiaCam.Where(x => x.IdTrangTraiGiaCam == id).ToListAsync();
                if (trangTraiGiaCam == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaCam);
            }
        }

        [HttpGet("AddOrEdit/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddOrEdit(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaCam = await _dbContext.TrangTraiGiaCam.FirstOrDefaultAsync(x => x.Id == id);
                if (trangTraiGiaCam == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaCam);
            }
        }

        [HttpPost("AddOrEdit")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddOrEdit([FromBody] TrangTraiGiaCam model)
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

        [HttpGet("Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var map = await _dbContext.TrangTraiGiaCam.FindAsync(id);
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
                    _dbContext.TrangTraiGiaCam.Update(map);
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

        [HttpGet("XoaTiemPhongTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> XoaTiemPhongTTGiaCam(Guid id)
        {
            var map = await _dbContext.TiemPhongGiaCam.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.TiemPhongGiaCam.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpGet("XoaTinhHinhDichTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> XoaTinhHinhDichTTGiaCam(Guid id)
        {
            var map = await _dbContext.TinhHinhDichBenhGiaCam.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.TinhHinhDichBenhGiaCam.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpGet("GetATDBTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetATDBTTGiaCam(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaCam = await _dbContext.CNATDBTTGiaCam.Where(x => x.IdTrangTraiGiaCam == id).ToListAsync();
                if (trangTraiGiaCam == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaCam);
            }
        }

        [HttpPost("AddATDBTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddATDBTTGiaCam([FromBody] CNATDBTTGiaCam model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.CNATDBTTGiaCam.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest();
        }

        [HttpGet("DeleteATDBTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteATDBTTGiaCam(Guid id)
        {
            var map = await _dbContext.CNATDBTTGiaCam.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNATDBTTGiaCam.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpGet("GetDKCNTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetDKCNTTGiaCam(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaCam = await _dbContext.CNDKCNTTGiaCam.Where(x => x.IdTrangTraiGiaCam == id).ToListAsync();
                if (trangTraiGiaCam == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaCam);
            }
        }

        [HttpPost("AddDKCNTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddDKCNTTGiaCam([FromBody] CNDKCNTTGiaCam model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.CNDKCNTTGiaCam.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return View(model);
        }

        [HttpGet("DeleteDKCNTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteDKCNTTGiaCam(Guid id)
        {
            var map = await _dbContext.CNDKCNTTGiaCam.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNDKCNTTGiaCam.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpGet("GetVietGAHPTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetVietGAHPTTGiaCam(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaCam = await _dbContext.CNVietGAHPTTGiaCam.Where(x => x.IdTrangTraiGiaCam == id).ToListAsync();
                if (trangTraiGiaCam == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaCam);
            }
        }

        [HttpPost("AddVietGAHPTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddVietGAHPTTGiaCam([FromBody] CNVietGAHPTTGiaCam model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.CNVietGAHPTTGiaCam.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest();
        }

        [HttpGet("DeleteVietGAHPTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteVietGAHPTTGiaCam(Guid id)
        {
            var map = await _dbContext.CNVietGAHPTTGiaCam.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNVietGAHPTTGiaCam.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpGet("GetVSTPTTGiaCam/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //VSTP
        public async Task<IActionResult> GetVSTPTTGiaCam(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaCam = await _dbContext.CNVSTPTTGiaCam.Where(x => x.IdTrangTraiGiaCam == id).ToListAsync();
                if (trangTraiGiaCam == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaCam);
            }
        }

        [HttpPost("AddVSTPTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddVSTPTTGiaCam([FromBody] CNVSTPTTGiaCam model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.CNVSTPTTGiaCam.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return View(model);
        }

        [HttpGet("DeleteVSTPTTGiaCam/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteVSTPTTGiaCam(Guid id)
        {
            var map = await _dbContext.CNVSTPTTGiaCam.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNVSTPTTGiaCam.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        
        [HttpPost("AddDichTTGiaCam/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddDichTTGiaCam(Guid id, [FromBody] Dich model)
        {
            var farm = await _dbContext.TrangTraiGiaCam.FindAsync(id);
            farm.IsDich = "1";
            _dbContext.Dich.Add(model);
            _dbContext.SaveChanges();
            return Ok(farm);
        }

        [HttpGet("CheckDichTTGiaCam/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CheckDichTTGiaCam(Guid id)
        {
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
        public async Task<IActionResult> KetThucDich(Guid id, [FromBody] TinhHinhDichBenhGiaCam model)
        {
            var dich = await _dbContext.Dich.Where(x => x.IdTrangTrai == id && x.IsDeleted != true).ToListAsync();
            var lastFarmDich = dich.LastOrDefault();
            lastFarmDich.IsDeleted = true;
            var farm = _dbContext.TrangTraiGiaCam.Find(id);
            farm.IsDich = "0";
            model.ThoiDiemKetThuc = DateTime.Now;
            _dbContext.TinhHinhDichBenhGiaCam.Add(model);
            await _dbContext.SaveChangesAsync();
            return Ok(farm);
        }
    }
    
}
