using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TRANGTRAICHANNUOI.DTO;

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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 10)
        {
            IQueryable<TrangTraiGiaCam> query;
            int skip = (page - 1) * pageSize;
            if (search == "all")
            { query = _dbContext.TrangTraiGiaCam.Where(x => x.IsDeleted != true); }
            else
            {
                query = _dbContext.TrangTraiGiaCam.Where(x => x.TenTrai.ToLower().Contains(search.ToLower()));
            }
            int totalRecords = await query.CountAsync();
            var finalQuery = await query.Skip(skip).Take(pageSize).ToListAsync();
            var pagingModel = new PagingModel<TrangTraiGiaCam>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalRecords,
                Items = finalQuery
            };
            return Ok(pagingModel);
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

        [HttpGet("GetTiemPhongTTGiaCam/{id}")]
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

        [HttpGet("TinhHinhDichTTGiaCam/{id}")]
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

        [HttpGet("GetATDBTTGiaCam/{id}")]
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

        [HttpGet("CheckDichTTGiaCam")]
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

        [HttpGet("GetVSTYTTGiaCam/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetVSTYTTGiaCam(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaCam = await _dbContext.CNVSTYTTGiaCam.Where(x => x.IdTrangTraiGiaCam == id).ToListAsync();
                if (trangTraiGiaCam == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaCam);
            }
        }

        [HttpGet("GetVietGAHPTTGiaCam/{id}")]
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

        [HttpGet("GetDKCNTTGiaCam/{id}")]
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
            return Ok(addMapRequest);
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
            return Ok(model);
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

        [HttpPost("AddVSTYTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddVSTYTTGiaCam([FromBody] CNVSTYTTGiaCam model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.CNVSTYTTGiaCam.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return Ok(model);
        }

        [HttpPost("AddDichTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddDichTTGiaCam(Guid id, [FromBody] Dich model)
        {
            var farm = await _dbContext.TrangTraiGiaCam.FindAsync(id);
            farm.IsDich = "1";
            _dbContext.Dich.Add(model);
            _dbContext.SaveChanges();
            return Ok(model);
        }

        [HttpPost("KetThucDichTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> KetThucDichTTGiaCam(Guid id, [FromBody] TinhHinhDichBenhGiaCam model)
        {
            var dich = await _dbContext.Dich.Where(x => x.IdTrangTrai == id && x.IsDeleted != true).ToListAsync();
            var lastFarmDich = dich.LastOrDefault();
            lastFarmDich.IsDeleted = true;
            var farm = _dbContext.TrangTraiGiaCam.Find(id);
            farm.IsDich = "0";
            model.ThoiDiemKetThuc = DateTime.Now;
            await _dbContext.TinhHinhDichBenhGiaCam.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("AddOrEdit")]
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

        [HttpDelete("XoaTiemPhongTTGiaCam/{id}")]
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

        [HttpDelete("XoaTinhHinhDichTTGiaCam/{id}")]
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

        [HttpDelete("Delete/{id}")]
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

        [HttpDelete("DeleteVSTYTTGiaCam/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteVSTYTTGiaCam(Guid id)
        {
            var map = await _dbContext.CNVSTYTTGiaCam.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNVSTYTTGiaCam.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }


        [HttpDelete("DeleteVietGAHPTTGiaCam/{id}")]
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

        [HttpDelete("DeleteDKCNTTGiaCam/{id}")]
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

        [HttpDelete("DeleteATDBTTGiaCam/{id}")]
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
    }
}
