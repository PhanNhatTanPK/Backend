using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Backend.Data;
using Backend.Services;
using Backend.Models;
using ClosedXML.Excel;
using TRANGTRAICHANNUOI.DTO;

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
                                    IWebHostEnvironment webHostEnvironment, 
                                    TrangTraiService trangTraiService)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _dbContext = dbContext;
            _trangTraiService = trangTraiService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 10)
        {
            IQueryable<TrangTraiHeo> query;
            int skip = (page - 1) * pageSize;
            if (search == "all")
            { query = _dbContext.TrangTraiHeo.Where(x => x.IsDeleted != true); }
            else
            {
                query = _dbContext.TrangTraiHeo.Where(x => (x.TenTrai.ToLower().Contains(search.ToLower())
                                                    || x.DienThoai == search)
                                                    && x.IsDeleted != true);
            }
            int totalRecords = await query.CountAsync();
            var trangTraiHeoList = await query.Skip(skip).Take(pageSize).ToListAsync();
            var pagingModel = new PagingModel<TrangTraiHeo>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalRecords,
                Items = trangTraiHeoList
            };
            return Ok(pagingModel);
        }

        [HttpGet("GetPigFarms")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<object>> GetPigFarms()
        {
            return await _dbContext.TrangTraiHeo.Where(x => x.IsDeleted != true)
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

        [HttpGet("GetTiemPhongTTHeo/{id}")]
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

        [HttpGet("GetDKCNTTHeo/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetDKCNTTHeo(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Vui lòng nhập ID trang trại cần tìm");
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

        [HttpGet("GetVSTYTTHeo/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetVSTYTTHeo(Guid id)
        {
            if (id == Guid.Empty)
                return View(new TrangTraiHeo());
            else
            {
                var trangTraiHeo = await _dbContext.CNVSTYTTHeo.Where(x => x.IdTrangTraiHeo == id).ToListAsync();
                if (trangTraiHeo == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiHeo);
            }
        }

        [HttpGet("CheckDichTTHeo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CheckDichTTHeo(Guid id)
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

        [HttpPost("AddVSTYTTHeo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddVSTYTTHeo([FromBody] CNVSTYTTHeo model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.CNVSTYTTHeo.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest(model);
        }

        [HttpPost("AddDichTTHeo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddDichTTHeo(Guid id, [FromBody] Dich model)
        {
            var farm = _dbContext.TrangTraiHeo.Find(id);
            farm.IsDich = "1";
            _dbContext.Dich.Add(model);
            _dbContext.SaveChanges();
            return Ok(model);
        }

        [HttpPost("KetThucDichTTHeo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> KetThucDichTTHeo(Guid id, [FromBody] TinhHinhDichBenhTraiHeo model)
        {
            var dich = await _dbContext.Dich.Where(x => x.IdTrangTrai == id && x.IsDeleted != true).ToListAsync();
            var lastFarmDich = dich.LastOrDefault();
            lastFarmDich.IsDeleted = true;
            var farm = _dbContext.TrangTraiHeo.Find(id);
            farm.IsDich = "0";
            model.ThoiDiemKetThuc = DateTime.Now;
            await _dbContext.TinhHinhDichBenhTraiHeo.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok(model);
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

        [HttpDelete("Delete/{id}")]
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

        [HttpDelete("XoaTiemPhongTTHeo/{id}")]
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

        [HttpDelete("XoaTinhHinhDichTTHeo/{id}")]
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

        [HttpDelete("DeleteATDBTTHeo/{id}")]
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

        [HttpDelete("DeleteDKCNTTHeo/{id}")]
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

        [HttpDelete("DeleteVSTYTTHeo/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteVSTYTTHeo(Guid id)
        {
            var map = await _dbContext.CNVSTYTTHeo.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNVSTYTTHeo.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

        [HttpDelete("DeleteVietGAHPTTHeo/{id}")]
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
    }
}
