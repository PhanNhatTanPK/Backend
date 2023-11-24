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

        [HttpGet("LoadTraiGiaSuc")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IEnumerable<object>> LoadTraiGiaSuc()
        {
            return await _dbContext.TrangTraiDaiGiaSuc.Where(x => x.IsDeleted != true).Select(x => new {
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
        
        [HttpPost("AddOrEdit")]
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

        [HttpGet("Delete/{id}")]
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
        
        [HttpGet("XoaTiemPhongTTGiaSuc/{id}")]
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

        [HttpGet("XoaTinhHinhDichTTGiaSuc/{id}")]
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

        [HttpPost("AddATDBTTGiaSuc")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult> AddATDBTTGiaSuc([FromBody] CNATDBTTGiaSuc model)
        {
           
                await _dbContext.CNATDBTTGiaSuc.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
          
        }

        [HttpGet("DeleteATDBTTGiaSuc/{id}")]
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
        
         [HttpGet("GetVSTPTTGiaSuc/{id}")]
		//Vệ sinh thú y
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult> GetVSTPTTGiaSuc(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            else
            {
                var trangTraiGiaSuc = await _dbContext.CNVSTPTTGiaSuc.Where(x => x.IdTrangTraiDaiGiaSuc == id).ToListAsync();
                if (trangTraiGiaSuc == null)
                {
                    return NotFound();
                }
                return Ok(trangTraiGiaSuc);
            }
        }

        [HttpPost("AddVSTPTTGiaSuc")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult> AddVSTPTTGiaSuc([FromBody] CNVSTPTTGiaSuc model)
        {
            
                await _dbContext.CNVSTPTTGiaSuc.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
           
        }
        [HttpGet("DeleteVSTPTTGiaSuc/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult> DeleteVSTPTTGiaSuc(Guid id)
        {
            var map = await _dbContext.CNVSTPTTGiaSuc.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.CNVSTPTTGiaSuc.Remove(map);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
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

        [HttpPost("AddDKCNTTGiaSuc")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult> AddDKCNTTGiaSuc([FromBody] CNDKCNTTGiaSuc model)
        {
            
                await _dbContext.CNDKCNTTGiaSuc.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            
        }

        [HttpGet("DeleteDKCNTTGiaSuc/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult> DeleteDKCNTTGiaSuc(Guid id)
        {
            var map = await _dbContext.CNDKCNTTGiaSuc.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
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

        [HttpPost("AddVietGAHPTTGiaSuc")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult> AddVietGAHPTTGiaSuc([FromBody] CNVietGAHPTTGiaSuc model)
        {
            
                await _dbContext.CNVietGAHPTTGiaSuc.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            
        }

        [HttpGet("DeleteVietGAHPTTGiaSuc/id")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult> DeleteVietGAHPTTGiaSuc(Guid id)
        {
            var map = await _dbContext.CNVietGAHPTTGiaSuc.FindAsync(id);
            //var map = _dbContext.CoSoGietMo.Where(w => w.Id == id);
            if (id == Guid.Empty)
            {
                return BadRequest();
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
