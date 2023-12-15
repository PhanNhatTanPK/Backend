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
    public class TrangTraiDaiGiaSucController : Controller
    {
        private readonly ILogger<TrangTraiDaiGiaSucController> _logger;
        private TrangTraiContext _dbContext;
        private readonly TrangTraiService _trangTraiService;
        public TrangTraiDaiGiaSucController(ILogger<TrangTraiDaiGiaSucController> logger,
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
            IQueryable<TrangTraiDaiGiaSuc> query;
            int skip = (page - 1) * pageSize;
            if (search == "all")
            { query = _dbContext.TrangTraiDaiGiaSuc.Where(x => x.IsDeleted != true); }
            else
            {
                query = _dbContext.TrangTraiDaiGiaSuc.Where(x => x.TenTrai.ToLower().Contains(search.ToLower()));
            }
            int totalRecords = await query.CountAsync();
            var finalQuery = await query.Skip(skip).Take(pageSize).ToListAsync();
            var pagingModel = new PagingModel<TrangTraiDaiGiaSuc>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalRecords,
                Items = finalQuery
            };
            return Ok(pagingModel);
        }

        [HttpGet("LoadTraiGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<object>> LoadTraiGiaSuc()
        {
            return await _dbContext.TrangTraiDaiGiaSuc.Where(x => x.IsDeleted != true).Select(x => new
            {
                x.Id,
                x.TenTrai,
                x.ChuTrangTrai,
                x.LongitudeNumber,
                x.LatitudeNumber,
                x.DienThoai,
                x.DiaChi,
                x.IdLoaiGiaSuc,
                x.IdLoaiTrangTrai,
                x.TongDan,
                x.DistrictId,
                x.SoDayChuongKin,
                x.SoDayChuongHo
            })
                .ToListAsync();
        }

        [HttpGet("LoaiGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<LoaiGiaSuc>> LoaiGiaSuc()
        {
            return await _dbContext.LoaiGiaSuc.ToListAsync();
        }

        [HttpGet("LoaiBenhGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<LoaiBenhGiaSuc>> LoaiBenhGiaSuc()
        {
            return await _dbContext.LoaiBenhGiaSuc.ToListAsync();
        }

        [HttpGet("ThongKeTrangTrai")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> ThongKeTrangTrai(long id)
        {
            if (id == null)
                return BadRequest();
            else
            {
                var ttGiaSuc = await _dbContext.TrangTraiDaiGiaSuc.Where(x => x.DistrictId == id && x.IsDeleted != true).ToListAsync();
                if (ttGiaSuc == null)
                {
                    return NotFound();
                }
                return Ok(ttGiaSuc);
            }
        }

        [HttpGet("AddOrEdit/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddOrEdit(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiDaiGiaSuc = await _dbContext.TrangTraiDaiGiaSuc.FirstOrDefaultAsync(x => x.Id == id);
                if (trangTraiDaiGiaSuc == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiDaiGiaSuc);
            }
        }

        [HttpGet("GetTiemPhongTTGiaSuc/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetTiemPhongTTGiaSuc(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaSuc = await _dbContext.TiemPhongDaiGiaSuc.Where(x => x.IdTrangTraiDaiGiaSuc == id).ToListAsync();
                if (trangTraiGiaSuc == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaSuc);
            }
        }

        [HttpGet("TinhHinhDichTTGiaSuc/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> TinhHinhDichTTGiaSuc(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaSuc = await _dbContext.TinhHinhDichBenhDaiGiaSuc.Where(x => x.IdTrangTraiDaiGiaSuc == id).ToListAsync();
                if (trangTraiGiaSuc == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaSuc);
            }
        }

        [HttpGet("GetATDBTTGiaSuc/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetATDBTTGiaSuc(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaSuc = await _dbContext.CNATDBTTGiaSuc.Where(x => x.IdTrangTraiDaiGiaSuc == id).ToListAsync();
                if (trangTraiGiaSuc == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaSuc);
            }
        }

        [HttpGet("GetVSTYTTGiaSuc/{id}")]
        //Vệ sinh thú y
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetVSTYTTGiaSuc(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaSuc = await _dbContext.CNVSTYTTGiaSuc.Where(x => x.IdTrangTraiDaiGiaSuc == id).ToListAsync();
                if (trangTraiGiaSuc == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaSuc);
            }
        }

        [HttpGet("GetDKCNTTGiaSuc/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetDKCNTTGiaSuc(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaSuc = await _dbContext.CNDKCNTTGiaSuc.Where(x => x.IdTrangTraiDaiGiaSuc == id).ToListAsync();
                if (trangTraiGiaSuc == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaSuc);
            }
        }

        [HttpGet("GetVietGAHPTTGiaSuc/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetVietGAHPTTGiaSuc(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaSuc = await _dbContext.CNVietGAHPTTGiaSuc.Where(x => x.IdTrangTraiDaiGiaSuc == id).ToListAsync();
                if (trangTraiGiaSuc == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaSuc);
            }
        }

        [HttpGet("CheckDichTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CheckDichTTGiaSuc(Guid id)
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

        [HttpPost("AddTrangTraiDaiGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddTrangTraiDaiGiaSuc([FromBody] TrangTraiDaiGiaSuc addMapRequest)
        {
            addMapRequest.LongitudeNumber = _trangTraiService.CoordinateToDecimal(addMapRequest.Longitude);
            addMapRequest.LatitudeNumber = _trangTraiService.CoordinateToDecimal(addMapRequest.Latitude);

            await _dbContext.TrangTraiDaiGiaSuc.AddAsync(addMapRequest);
            await _dbContext.SaveChangesAsync();
            return Ok(addMapRequest);

        }

        [HttpPost("AddTinhHinhDichDaiGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddTinhHinhDichDaiGiaSuc([FromBody] TinhHinhDichBenhDaiGiaSuc model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.TinhHinhDichBenhDaiGiaSuc.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return Ok(model);
        }

        [HttpPost("ThemTiemPhongDaiGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> ThemTiemPhongDaiGiaSuc([FromBody] TiemPhongDaiGiaSuc model)
        {
            await _dbContext.TiemPhongDaiGiaSuc.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPost("AddATDBTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddATDBTTGiaSuc([FromBody] CNATDBTTGiaSuc model)
        {

            await _dbContext.CNATDBTTGiaSuc.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok(model);

        }

        [HttpPost("AddVSTYTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddVSTYTTGiaSuc([FromBody] CNVSTYTTGiaSuc model)
        {

            await _dbContext.CNVSTYTTGiaSuc.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok(model);

        }

        [HttpPost("AddDKCNTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddDKCNTTGiaSuc([FromBody] CNDKCNTTGiaSuc model)
        {

            await _dbContext.CNDKCNTTGiaSuc.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok(model);

        }

        [HttpPost("AddVietGAHPTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddVietGAHPTTGiaSuc([FromBody] CNVietGAHPTTGiaSuc model)
        {

            await _dbContext.CNVietGAHPTTGiaSuc.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok(model);

        }

        [HttpPost("AddDichTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddDichTTGiaSuc(Guid id, [FromBody] Dich model)
        {
            var farm = _dbContext.TrangTraiDaiGiaSuc.Find(id);
            farm.IsDich = "1";
            _dbContext.Dich.Add(model);
            _dbContext.SaveChanges();
            return Ok(model);
        }

        [HttpPost("KetThucDichTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> KetThucDichTTGiaSuc(Guid id, [FromBody] TinhHinhDichBenhDaiGiaSuc model)
        {
            var dich = await _dbContext.Dich.Where(x => x.IdTrangTrai == id && x.IsDeleted != true).ToListAsync();
            var lastFarmDich = dich.LastOrDefault();
            lastFarmDich.IsDeleted = true;
            var farm = _dbContext.TrangTraiDaiGiaSuc.Find(id);
            farm.IsDich = "0";
            model.ThoiDiemKetThuc = DateTime.Now;
            await _dbContext.TinhHinhDichBenhDaiGiaSuc.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("AddOrEdit")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddOrEdit([FromBody] TrangTraiDaiGiaSuc model)
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
        public async Task<ActionResult> Delete(Guid id)
        {
            var map = await _dbContext.TrangTraiDaiGiaSuc.FindAsync(id);
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
                    _dbContext.TrangTraiDaiGiaSuc.Update(map);
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

        [HttpDelete("XoaTiemPhongTTGiaSuc/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> XoaTiemPhongTTGiaSuc(Guid id)
        {
            var map = await _dbContext.TiemPhongDaiGiaSuc.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.TiemPhongDaiGiaSuc.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpDelete("XoaTinhHinhDichTTGiaSuc/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> XoaTinhHinhDichTTGiaSuc(Guid id)
        {
            var map = await _dbContext.TinhHinhDichBenhDaiGiaSuc.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.TinhHinhDichBenhDaiGiaSuc.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpDelete("DeleteATDBTTGiaSuc/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteATDBTTGiaSuc(Guid id)
        {
            var map = await _dbContext.CNATDBTTGiaSuc.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNATDBTTGiaSuc.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpDelete("DeleteVSTYTTGiaSuc/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteVSTYTTGiaSuc(Guid id)
        {
            var map = await _dbContext.CNVSTYTTGiaSuc.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNVSTYTTGiaSuc.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpDelete("DeleteDKCNTTGiaSuc/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteDKCNTTGiaSuc(Guid id)
        {
            var map = await _dbContext.CNDKCNTTGiaSuc.FindAsync(id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNDKCNTTGiaSuc.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpDelete("DeleteVietGAHPTTGiaSuc/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteVietGAHPTTGiaSuc(Guid id)
        {
            var map = await _dbContext.CNVietGAHPTTGiaSuc.FindAsync(id);
            if (id == Guid.Empty)
            {
                return BadRequest("Vui lòng nhập ID");
            }
            try
            {
                _dbContext.CNVietGAHPTTGiaSuc.Remove(map);
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
