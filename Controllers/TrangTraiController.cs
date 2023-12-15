using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Backend.Data;
using Backend.Models;
using Backend.DTO;

namespace TRANGTRAICHANNUOI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class TrangTraiController : Controller
    {
        private readonly ILogger<TrangTraiController> _logger;
        private TrangTraiContext _dbContext;
        public TrangTraiController(ILogger<TrangTraiController> logger, TrangTraiContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("Huyen")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<District>> Huyen()
        {
            return await _dbContext.District.ToListAsync();
        }

        [HttpGet("GetNumberForEachFarmType")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetNumberForEachFarmType()
        {
            var tong = new ThongKeTTDashboard
            {
                TongTraiHeo = _dbContext.TrangTraiHeo.Where(x => x.IsDeleted != true).Count(),
                TongTraiGiaSuc = _dbContext.TrangTraiDaiGiaSuc.Where(x => x.IsDeleted != true).Count(),
                TongCoSoGietMo = _dbContext.CoSoGietMo.Where(x => x.IsDeleted != true).Count(),
                TongTraiGiaCam = _dbContext.TrangTraiGiaCam.Where(x => x.IsDeleted != true).Count(),
                TongTrangTraiDich = _dbContext.Dich.Where(x => x.IsDeleted != true).Count(),
            };
            return Ok(tong);
        }

        [HttpGet("GetAllTrangTrai")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllTrangTrai()
        {
            var listTraiHeo = _dbContext.TrangTraiHeo.ToList();
            var listTraiGiaSuc = _dbContext.TrangTraiDaiGiaSuc.ToList();
            var listCoSoGietMo = _dbContext.CoSoGietMo.ToList();
            var listTrangTraiGiaCam = _dbContext.TrangTraiGiaCam.ToList();
            var genericClass = new List<DanhSachTrangTrai>();

            foreach (var item in listTraiHeo)
            {
                var heo = new DanhSachTrangTrai
                {
                    Id = item.Id,
                    TenTrai = item.TenTrai,
                    ChuTrangTrai = item.ChuTrangTrai,
                    DiaChi = item.DiaChi,
                    DienThoai = item.DienThoai,
                    LongitudeNumber = item.LongitudeNumber,
                    LatitudeNumber = item.LatitudeNumber,
                    IdLoaiTrangTrai = item.IdLoaiTrangTrai,
                    IsDich = item.IsDich
                };
                genericClass.Add(heo);
            }

            foreach (var item in listTraiGiaSuc)
            {
                var giaSuc = new DanhSachTrangTrai
                {
                    Id = item.Id,
                    TenTrai = item.TenTrai,
                    ChuTrangTrai = item.ChuTrangTrai,
                    DiaChi = item.DiaChi,
                    DienThoai = item.DienThoai,
                    LongitudeNumber = item.LongitudeNumber,
                    LatitudeNumber = item.LatitudeNumber,
                    IdLoaiTrangTrai = item.IdLoaiTrangTrai,
                    IsDich = item.IsDich
                };
                genericClass.Add(giaSuc);
            }

            foreach (var item in listCoSoGietMo)
            {
                var coSoGietMo = new DanhSachTrangTrai
                {
                    Id = item.Id,
                    TenTrai = item.TenTrai,
                    ChuTrangTrai = item.ChuTrangTrai,
                    DiaChi = item.DiaChi,
                    DienThoai = item.DienThoai,
                    LongitudeNumber = item.LongitudeNumber,
                    LatitudeNumber = item.LatitudeNumber,
                    IdLoaiTrangTrai = item.IdLoaiTrangTrai,
                    IsDich = item.IsDich
                };
                genericClass.Add(coSoGietMo);
            }
            foreach (var item in listTrangTraiGiaCam)
            {
                var giaSuc = new DanhSachTrangTrai
                {
                    Id = item.Id,
                    TenTrai = item.TenTrai,
                    ChuTrangTrai = item.ChuTrangTrai,
                    DiaChi = item.DiaChi,
                    DienThoai = item.DienThoai,
                    LongitudeNumber = item.LongitudeNumber,
                    LatitudeNumber = item.LatitudeNumber,
                    IdLoaiTrangTrai = item.IdLoaiTrangTrai,
                    IsDich = item.IsDich
                };
                genericClass.Add(giaSuc);
            }
            return Ok(genericClass);
        }

        [HttpGet("GetAllPandemicFarm")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<DanhSachTTDich> GetAllPandemicFarm()
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var listTraiHeo = from tth in _dbContext.TrangTraiHeo
                              join thd in _dbContext.TinhHinhDichBenhTraiHeo
                              on tth.Id equals thd.IdTrangTraiHeo
                              where tth.IsDeleted != true
                              select new DanhSachTTDich
                              {
                                  Id = tth.Id,
                                  TenTrai = tth.TenTrai,
                                  LongitudeNumber = tth.LongitudeNumber,
                                  LatitudeNumber = tth.LatitudeNumber,
                                  ThoiDiemPhatBenh = (long)(DateTime.SpecifyKind((DateTime)thd.ThoiDiemPhatBenh, DateTimeKind.Utc) - unixEpoch).TotalSeconds * 1000,
                                  ThoiDiemKetThuc = (long)(DateTime.SpecifyKind((DateTime)thd.ThoiDiemKetThuc, DateTimeKind.Utc) - unixEpoch).TotalSeconds * 1000,
                                  LoaiBenh = thd.LoaiBenh,
                                  SoBenh = thd.SoBenh,
                                  IdLoaiTrangTrai = 1
                              };
            var result1 = listTraiHeo.ToList();
            var listTraiGiaSuc = from ttgs in _dbContext.TrangTraiDaiGiaSuc
                                 join thd in _dbContext.TinhHinhDichBenhDaiGiaSuc
                                 on ttgs.Id equals thd.IdTrangTraiDaiGiaSuc
                                 where ttgs.IsDeleted != true
                                 select new DanhSachTTDich
                                 {
                                     Id = ttgs.Id,
                                     TenTrai = ttgs.TenTrai,
                                     LongitudeNumber = ttgs.LongitudeNumber,
                                     LatitudeNumber = ttgs.LatitudeNumber,
                                     ThoiDiemPhatBenh = (long)(DateTime.SpecifyKind((DateTime)thd.ThoiDiemPhatBenh, DateTimeKind.Utc) - unixEpoch).TotalSeconds * 1000,
                                     ThoiDiemKetThuc = (long)(DateTime.SpecifyKind((DateTime)thd.ThoiDiemKetThuc, DateTimeKind.Utc) - unixEpoch).TotalSeconds * 1000,
                                     LoaiBenh = thd.LoaiBenh,
                                     SoBenh = thd.SoBenh,
                                     IdLoaiTrangTrai = 2
                                 };
            var result2 = listTraiGiaSuc.ToList();
            var listTraiGiaCam = from ttgc in _dbContext.TrangTraiGiaCam
                                 join thd in _dbContext.TinhHinhDichBenhGiaCam
                                 on ttgc.Id equals thd.IdTrangTraiGiaCam
                                 where ttgc.IsDeleted != true
                                 select new DanhSachTTDich
                                 {
                                     Id = ttgc.Id,
                                     TenTrai = ttgc.TenTrai,
                                     LongitudeNumber = ttgc.LongitudeNumber,
                                     LatitudeNumber = ttgc.LatitudeNumber,
                                     ThoiDiemPhatBenh = (long)(DateTime.SpecifyKind((DateTime)thd.ThoiDiemPhatBenh, DateTimeKind.Utc) - unixEpoch).TotalSeconds * 1000,
                                     ThoiDiemKetThuc = (long)(DateTime.SpecifyKind((DateTime)thd.ThoiDiemKetThuc, DateTimeKind.Utc) - unixEpoch).TotalSeconds * 1000,
                                     LoaiBenh = thd.LoaiBenh,
                                     SoBenh = thd.SoBenh,
                                     IdLoaiTrangTrai = 4
                                 };
            var result3 = listTraiGiaCam.ToList();
            var result = result1.Concat(result2).Concat(result3);

            return result.ToList();
        }

        [HttpGet("GetCurrentPandemicFarm")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetCurrentPandemicFarm()
        {
            var listTraiHeo = from tt in _dbContext.TrangTraiHeo
                              join d in _dbContext.Dich on tt.Id equals d.IdTrangTrai
                              where tt.IsDich == "1" && tt.IsDeleted != true && d.IsDeleted != true
                              select new
                              {
                                  tt.Id,
                                  tt.TenTrai,
                                  tt.ChuTrangTrai,
                                  tt.DiaChi,
                                  tt.DienThoai,
                                  tt.LongitudeNumber,
                                  tt.LatitudeNumber,
                                  tt.IdLoaiTrangTrai,
                                  tt.IsDich,
                                  d.ThoiGianBatDau,
                                  d.TenBenhNghiNgo,
                                  d.SoBenh
                              };
            var listTraiGiaSuc = from tt in _dbContext.TrangTraiDaiGiaSuc
                                 join d in _dbContext.Dich on tt.Id equals d.IdTrangTrai
                                 where tt.IsDich == "1" && tt.IsDeleted != true && d.IsDeleted != true
                                 select new
                                 {
                                     tt.Id,
                                     tt.TenTrai,
                                     tt.ChuTrangTrai,
                                     tt.DiaChi,
                                     tt.DienThoai,
                                     tt.LongitudeNumber,
                                     tt.LatitudeNumber,
                                     tt.IdLoaiTrangTrai,
                                     tt.IsDich,
                                     d.ThoiGianBatDau,
                                     d.TenBenhNghiNgo,
                                     d.SoBenh
                                 };
            var listCoSoGietMo = from tt in _dbContext.CoSoGietMo
                                 join d in _dbContext.Dich on tt.Id equals d.IdTrangTrai
                                 where tt.IsDich == "1" && tt.IsDeleted != true && d.IsDeleted != true
                                 select new
                                 {
                                     tt.Id,
                                     tt.TenTrai,
                                     tt.ChuTrangTrai,
                                     tt.DiaChi,
                                     tt.DienThoai,
                                     tt.LongitudeNumber,
                                     tt.LatitudeNumber,
                                     tt.IdLoaiTrangTrai,
                                     tt.IsDich,
                                     d.ThoiGianBatDau,
                                     d.TenBenhNghiNgo,
                                     d.SoBenh
                                 };
            var listTrangTraiGiaCam = from tt in _dbContext.TrangTraiGiaCam
                                      join d in _dbContext.Dich on tt.Id equals d.IdTrangTrai
                                      where tt.IsDich == "1" && tt.IsDeleted != true && d.IsDeleted != true
                                      select new
                                      {
                                          tt.Id,
                                          tt.TenTrai,
                                          tt.ChuTrangTrai,
                                          tt.DiaChi,
                                          tt.DienThoai,
                                          tt.LongitudeNumber,
                                          tt.LatitudeNumber,
                                          tt.IdLoaiTrangTrai,
                                          tt.IsDich,
                                          d.ThoiGianBatDau,
                                          d.TenBenhNghiNgo,
                                          d.SoBenh
                                      };
            var result = listTraiHeo.Concat(listTraiGiaSuc).Concat(listCoSoGietMo).Concat(listTrangTraiGiaCam);

            return Ok(result.ToList());
        }

        [HttpGet("BaoCaoCoCauTTHeo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoCoCauTTHeo()
        {
            var results = from d in _dbContext.District
                          join t in _dbContext.TrangTraiHeo.Where(x => x.IsDeleted != true) on d.Id equals t.DistrictId into g
                          from t in g.DefaultIfEmpty()
                          group t by new { d.Id, d.DistrictName } into grp

                          select new BaoCaoCoCau
                          {
                              TenHuyen = grp.Key.DistrictName,
                              SoTrangTraiHeo = grp.Where(x => x != null).Count(),
                              TongDanHeo = (long)grp.Where(x => x != null).Sum(x => x.QuyMo)
                          };

            var finalResults = results.ToList();



            return Ok(finalResults);

        }

        [HttpGet("BaoCaoCoCauTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoCoCauTTGiaSuc()
        {
            var results = from d in _dbContext.District
                          join b in _dbContext.TrangTraiDaiGiaSuc.Where(t => t.IdLoaiGiaSuc == 1 && t.IsDeleted != true) on d.Id equals b.DistrictId into gb
                          from b in gb.DefaultIfEmpty()
                          join v in _dbContext.TrangTraiDaiGiaSuc.Where(t => t.IdLoaiGiaSuc == 2 && t.IsDeleted != true) on d.Id equals v.DistrictId into gv
                          from v in gv.DefaultIfEmpty()
                          join c in _dbContext.TrangTraiDaiGiaSuc.Where(t => t.IdLoaiGiaSuc == 3 && t.IsDeleted != true) on d.Id equals c.DistrictId into gc
                          from c in gc.DefaultIfEmpty()
                          join x in _dbContext.TrangTraiDaiGiaSuc.Where(t => t.IdLoaiGiaSuc == 4 && t.IsDeleted != true) on d.Id equals x.DistrictId into gx
                          from x in gx.DefaultIfEmpty()
                          join y in _dbContext.TrangTraiDaiGiaSuc.Where(t => t.IdLoaiGiaSuc == 5 && t.IsDeleted != true) on d.Id equals y.DistrictId into gy
                          from y in gy.DefaultIfEmpty()
                          group new { b, v, c, x, y } by new { d.Id, d.DistrictName } into grp
                          select new BaoCaoCoCau
                          {
                              TenHuyen = grp.Key.DistrictName,
                              SoTrangTraiBo = grp.Where(x => x.b != null && x.b.IdLoaiGiaSuc == 1).Count(),
                              TongDanBo = (long)grp.Where(x => x.b != null && x.b.IdLoaiGiaSuc == 1).Sum(x => x.b.TongDan),
                              SoTrangTraiDe = grp.Where(x => x.v != null && x.v.IdLoaiGiaSuc == 2).Count(),
                              TongDanDe = (long)grp.Where(x => x.v != null && x.v.IdLoaiGiaSuc == 2).Sum(x => x.v.TongDan),
                              SoTrangTraiTrau = grp.Where(x => x.c != null && x.c.IdLoaiGiaSuc == 3).Count(),
                              TongDanTrau = (long)grp.Where(x => x.c != null && x.c.IdLoaiGiaSuc == 3).Sum(x => x.c.TongDan),
                              SoTrangTraiNgua = grp.Where(x => x.x != null && x.x.IdLoaiGiaSuc == 4).Count(),
                              TongDanNgua = (long)grp.Where(x => x.x != null && x.x.IdLoaiGiaSuc == 4).Sum(x => x.x.TongDan),
                              SoTrangTraiCuu = grp.Where(x => x.y != null && x.y.IdLoaiGiaSuc == 5).Count(),
                              TongDanCuu = (long)grp.Where(x => x.y != null && x.y.IdLoaiGiaSuc == 5).Sum(x => x.y.TongDan)
                          };

            var finalResults = results.ToList();
            return Ok(finalResults);

        }

        [HttpGet("BaoCaoCoCauTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoCoCauTTGiaCam()
        {
            var results = from d in _dbContext.District
                          join b in _dbContext.TrangTraiGiaCam.Where(t => t.IdLoaiGiaCam == 1 && t.IsDeleted != true) on d.Id equals b.DistrictId into gb
                          from b in gb.DefaultIfEmpty()
                          join v in _dbContext.TrangTraiGiaCam.Where(t => t.IdLoaiGiaCam == 2 && t.IsDeleted != true) on d.Id equals v.DistrictId into gv
                          from v in gv.DefaultIfEmpty()
                          join c in _dbContext.TrangTraiGiaCam.Where(t => t.IdLoaiGiaCam == 3 && t.IsDeleted != true) on d.Id equals c.DistrictId into gc
                          from c in gc.DefaultIfEmpty()
                          group new { b, v, c } by new { d.Id, d.DistrictName } into grp
                          select new BaoCaoCoCau
                          {
                              TenHuyen = grp.Key.DistrictName,
                              SoTrangTraiGa = grp.Where(x => x.b != null && x.b.IdLoaiGiaCam == 1).Count(),
                              TongDanGa = (long)grp.Where(x => x.b != null && x.b.IdLoaiGiaCam == 1).Sum(x => x.b.QuyMo),
                              SoTrangTraiVit = grp.Where(x => x.v != null && x.v.IdLoaiGiaCam == 2).Count(),
                              TongDanVit = (long)grp.Where(x => x.v != null && x.v.IdLoaiGiaCam == 2).Sum(x => x.v.QuyMo),
                              SoTrangTraiCut = grp.Where(x => x.c != null && x.c.IdLoaiGiaCam == 3).Count(),
                              TongDanCut = (long)grp.Where(x => x.c != null && x.c.IdLoaiGiaCam == 3).Sum(x => x.c.QuyMo),
                          };

            var finalResults = results.ToList();
            return Ok(finalResults);

        }

        [HttpGet("BaoCaoATDBTTHeo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoATDBTTHeo()
        {
            var query = from d in _dbContext.District
                        join t in _dbContext.TrangTraiHeo.Where(x => x.IsDeleted != true) on d.Id equals t.DistrictId into tGroup
                        from t in tGroup.DefaultIfEmpty()
                        join cnvstyt in _dbContext.CNATDBTTHeo on t.Id equals cnvstyt.IdTrangTraiHeo into cnvstytGroup
                        from cnvstyt in cnvstytGroup.DefaultIfEmpty()
                        group new { t, cnvstyt } by new { d.DistrictName } into g
                        select new BaoCaoATDB
                        {
                            TenHuyen = g.Key.DistrictName,
                            SoLuongTrangTraiHeo = g.Count(x => x.t != null),
                            SoLuongTrangTraiATDBHeo = g.Count(x => x.t != null && x.cnvstyt != null),
                        };
            var result = query.ToList();
            return Ok(result);
        }

        [HttpGet("BaoCaoATDBTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoATDBTTGiaSuc()
        {
            var query = from d in _dbContext.District
                        join t in _dbContext.TrangTraiDaiGiaSuc.Where(x => x.IsDeleted != true) on d.Id equals t.DistrictId into tGroup
                        from t in tGroup.DefaultIfEmpty()
                        join cnvstyt in _dbContext.CNATDBTTGiaSuc on t.Id equals cnvstyt.IdTrangTraiDaiGiaSuc into cnvstytGroup
                        from cnvstyt in cnvstytGroup.DefaultIfEmpty()
                        group new { t, cnvstyt } by new { d.DistrictName, t.IdLoaiGiaSuc } into g
                        select new BaoCaoATDB
                        {
                            TenHuyen = g.Key.DistrictName,
                            SoLuongTrangTraiBo = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 1),
                            SoLuongTrangTraiDe = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 2),
                            SoLuongTrangTraiTrau = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 3),
                            SoLuongTrangTraiNgua = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 4),
                            SoLuongTrangTraiCuu = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 5),
                            SoLuongTrangTraiATDBBo = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 1 && x.cnvstyt != null),
                            SoLuongTrangTraiATDBDe = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 2 && x.cnvstyt != null),
                            SoLuongTrangTraiATDBTrau = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 3 && x.cnvstyt != null),
                            SoLuongTrangTraiATDBNgua = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 2 && x.cnvstyt != null),
                            SoLuongTrangTraiATDBCuu = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 3 && x.cnvstyt != null),
                        };
            var result = query.ToList();
            return Ok(result);
        }

        [HttpGet("BaoCaoATDBTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoATDBTTGiaCam()
        {
            var query = from d in _dbContext.District
                        join t in _dbContext.TrangTraiGiaCam.Where(x => x.IsDeleted != true) on d.Id equals t.DistrictId into tGroup
                        from t in tGroup.DefaultIfEmpty()
                        join cnvstyt in _dbContext.CNVSTYTTGiaCam on t.Id equals cnvstyt.IdTrangTraiGiaCam into cnvstytGroup
                        from cnvstyt in cnvstytGroup.DefaultIfEmpty()
                        group new { t, cnvstyt } by new { d.DistrictName, t.IdLoaiGiaCam } into g
                        select new BaoCaoATDB
                        {
                            TenHuyen = g.Key.DistrictName,
                            SoLuongTrangTraiGa = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 1),
                            SoLuongTrangTraiVit = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 2),
                            SoLuongTrangTraiCut = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 3),
                            SoLuongTrangTraiATDBGa = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 1 && x.cnvstyt != null),
                            SoLuongTrangTraiATDBVit = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 2 && x.cnvstyt != null),
                            SoLuongTrangTraiATDBCut = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 3 && x.cnvstyt != null),
                        };
            var result = query.ToList();
            return Ok(result);
        }

        [HttpGet("BaoCaoVSTYTTHeo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoVSTYTTHeo()
        {
            var distinctIds = _dbContext.CNVSTYTTHeo.Select(x => x.IdTrangTraiHeo).Distinct();

            var query = from d in _dbContext.District
                        join t in _dbContext.TrangTraiHeo.Where(x => x.IsDeleted != true) on d.Id equals t.DistrictId into tGroup
                        from t in tGroup.DefaultIfEmpty()
                        join cnvstyt in distinctIds on t.Id equals cnvstyt into cnvstytGroup
                        from cnvstyt in cnvstytGroup.DefaultIfEmpty()
                        group new { t, cnvstyt } by new { d.DistrictName } into g
                        select new BaoCaoVSTY
                        {
                            TenHuyen = g.Key.DistrictName,
                            SoLuongTrangTraiHeo = g.Count(x => x.t != null),
                            SoLuongTrangTraiVSTYHeo = g.Sum(x => x.cnvstyt != null ? 1 : 0),
                        };

            var result = query.ToList();

            return Ok(result);
        }

        [HttpGet("BaoCaoVSTYTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoVSTYTTGiaSuc()
        {
            var distinctIds = _dbContext.CNVSTYTTGiaSuc.Select(x => x.IdTrangTraiDaiGiaSuc).Distinct();

            var query = from d in _dbContext.District
                        join t in _dbContext.TrangTraiDaiGiaSuc.Where(x => x.IsDeleted != true) on d.Id equals t.DistrictId into tGroup
                        from t in tGroup.DefaultIfEmpty()
                        join cnvstyt in distinctIds on t.Id equals cnvstyt into cnvstytGroup
                        from cnvstyt in cnvstytGroup.DefaultIfEmpty()
                        group new { t, cnvstyt } by new { d.DistrictName, t.IdLoaiGiaSuc } into g
                        select new BaoCaoVSTY
                        {
                            TenHuyen = g.Key.DistrictName,
                            SoLuongTrangTraiBo = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 1),
                            SoLuongTrangTraiDe = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 2),
                            SoLuongTrangTraiTrau = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 3),
                            SoLuongTrangTraiNgua = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 4),
                            SoLuongTrangTraiCuu = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 5),
                            SoLuongTrangTraiVSTYBo = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 1 && x.cnvstyt != null),
                            SoLuongTrangTraiVSTYDe = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 2 && x.cnvstyt != null),
                            SoLuongTrangTraiVSTYTrau = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 3 && x.cnvstyt != null),
                            SoLuongTrangTraiVSTYNgua = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 2 && x.cnvstyt != null),
                            SoLuongTrangTraiVSTYCuu = g.Count(x => x.t != null && x.t.IdLoaiGiaSuc == 3 && x.cnvstyt != null),
                        };
            var result = query.ToList();
            return Ok(result);
        }

        [HttpGet("BaoCaoVSTYTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoVSTYTTGiaCam()
        {

            var distinctIds = _dbContext.CNVSTYTTGiaCam.Select(x => x.IdTrangTraiGiaCam).Distinct();

            var query = from d in _dbContext.District
                        join t in _dbContext.TrangTraiGiaCam.Where(x => x.IsDeleted != true) on d.Id equals t.DistrictId into tGroup
                        from t in tGroup.DefaultIfEmpty()
                        join cnvstyt in distinctIds on t.Id equals cnvstyt into cnvstytGroup
                        from cnvstyt in cnvstytGroup.DefaultIfEmpty()
                        group new { t, cnvstyt } by new { d.DistrictName, t.IdLoaiGiaCam } into g
                        select new BaoCaoVSTY
                        {
                            TenHuyen = g.Key.DistrictName,
                            SoLuongTrangTraiGa = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 1),
                            SoLuongTrangTraiVit = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 2),
                            SoLuongTrangTraiCut = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 3),
                            SoLuongTrangTraiVSTYGa = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 1 && x.cnvstyt != null),
                            SoLuongTrangTraiVSTYVit = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 2 && x.cnvstyt != null),
                            SoLuongTrangTraiVSTYCut = g.Count(x => x.t != null && x.t.IdLoaiGiaCam == 3 && x.cnvstyt != null),
                        };
            var result = query.ToList();

            return Ok(result);
        }

        [HttpGet("BaoCaoSoHuuTTHeo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoSoHuuTTHeo()
        {
            var farmStatistics = _dbContext.TrangTraiHeo.Where(x => x.IsDeleted != true)
                    .Join(_dbContext.District, t => t.DistrictId, d => d.Id, (t, d) => new { TrangTrai = t, District = d }).DefaultIfEmpty()
                    .GroupBy(td => td.District.DistrictName, td => td.TrangTrai, (key, g) => new BaoCaoSoHuu
                    {
                        TenHuyen = key,
                        TongTraiHeoTN = g.Count(t => t.HinhThucChanNuoi == "0"),
                        TongTraiHeoCT = g.Count(t => t.HinhThucChanNuoi == "2"),
                        TongTraiHeoGC = g.Count(t => t.HinhThucChanNuoi == "1"),

                    })
                    .ToList();
            return Ok(farmStatistics);
        }

        [HttpGet("BaoCaoSoHuuTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoSoHuuTTGiaSuc()
        {
            var farmStatistics = _dbContext.TrangTraiDaiGiaSuc.Where(x => x.IsDeleted != true)
                    .Join(_dbContext.District, t => t.DistrictId, d => d.Id, (t, d) => new { TrangTrai = t, District = d })
                    .GroupBy(td => td.District.DistrictName, td => td.TrangTrai, (key, g) => new BaoCaoSoHuu
                    {
                        TenHuyen = key,
                        TongTraiBoTN = g.Count(t => t.HinhThucChanNuoi == "0" && t.IdLoaiGiaSuc == 1),
                        TongTraiDeTN = g.Count(t => t.HinhThucChanNuoi == "0" && t.IdLoaiGiaSuc == 2),
                        TongTraiTrauTN = g.Count(t => t.HinhThucChanNuoi == "0" && t.IdLoaiGiaSuc == 3),
                        TongTraiNguaTN = g.Count(t => t.HinhThucChanNuoi == "0" && t.IdLoaiGiaSuc == 4),
                        TongTraiCuuTN = g.Count(t => t.HinhThucChanNuoi == "0" && t.IdLoaiGiaSuc == 5),
                        TongTN = g.Count(t => t.HinhThucChanNuoi == "0"),
                        TongTraiBoCT = g.Count(t => t.HinhThucChanNuoi == "2" && t.IdLoaiGiaSuc == 1),
                        TongTraiDeCT = g.Count(t => t.HinhThucChanNuoi == "2" && t.IdLoaiGiaSuc == 2),
                        TongTraiTrauCT = g.Count(t => t.HinhThucChanNuoi == "2" && t.IdLoaiGiaSuc == 3),
                        TongTraiNguaCT = g.Count(t => t.HinhThucChanNuoi == "2" && t.IdLoaiGiaSuc == 4),
                        TongTraiCuuCT = g.Count(t => t.HinhThucChanNuoi == "2" && t.IdLoaiGiaSuc == 5),
                        TongCT = g.Count(t => t.HinhThucChanNuoi == "2"),
                        TongTraiBoGC = g.Count(t => t.HinhThucChanNuoi == "1" && t.IdLoaiGiaSuc == 1),
                        TongTraiDeGC = g.Count(t => t.HinhThucChanNuoi == "1" && t.IdLoaiGiaSuc == 2),
                        TongTraiTrauGC = g.Count(t => t.HinhThucChanNuoi == "1" && t.IdLoaiGiaSuc == 3),
                        TongTraiNguaGC = g.Count(t => t.HinhThucChanNuoi == "1" && t.IdLoaiGiaSuc == 4),
                        TongTraiCuuGC = g.Count(t => t.HinhThucChanNuoi == "1" && t.IdLoaiGiaSuc == 5),
                        TongGC = g.Count(t => t.HinhThucChanNuoi == "1")
                    })
                    .ToList();


            return Ok(farmStatistics); ; // Trả về Ok nếu header Accept là application/Ok
        }

        [HttpGet("BaoCaoSoHuuTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult BaoCaoSoHuuTTGiaCam()
        {
            var farmStatistics = _dbContext.TrangTraiGiaCam.Where(x => x.IsDeleted != true)
                    .Join(_dbContext.District, t => t.DistrictId, d => d.Id, (t, d) => new { TrangTrai = t, District = d })
                    .GroupBy(td => td.District.DistrictName, td => td.TrangTrai, (key, g) => new BaoCaoSoHuu
                    {
                        TenHuyen = key,
                        TongTraiGaTN = g.Count(t => t.HinhThucChanNuoi == "0" && t.IdLoaiGiaCam == 1),
                        TongTraiVitTN = g.Count(t => t.HinhThucChanNuoi == "0" && t.IdLoaiGiaCam == 2),
                        TongTraiCutTN = g.Count(t => t.HinhThucChanNuoi == "0" && t.IdLoaiGiaCam == 3),
                        TongTN = g.Count(t => t.HinhThucChanNuoi == "0"),
                        TongTraiGaCT = g.Count(t => t.HinhThucChanNuoi == "2" && t.IdLoaiGiaCam == 1),
                        TongTraiVitCT = g.Count(t => t.HinhThucChanNuoi == "2" && t.IdLoaiGiaCam == 2),
                        TongTraiCutCT = g.Count(t => t.HinhThucChanNuoi == "2" && t.IdLoaiGiaCam == 3),
                        TongCT = g.Count(t => t.HinhThucChanNuoi == "2"),
                        TongTraiGaGC = g.Count(t => t.HinhThucChanNuoi == "1" && t.IdLoaiGiaCam == 1),
                        TongTraiVitGC = g.Count(t => t.HinhThucChanNuoi == "1" && t.IdLoaiGiaCam == 2),
                        TongTraiCutGC = g.Count(t => t.HinhThucChanNuoi == "1" && t.IdLoaiGiaCam == 3),
                        TongGC = g.Count(t => t.HinhThucChanNuoi == "1")
                    })
            .ToList();

            return Ok(farmStatistics);
        }

        [HttpGet("BaoCaoDacDiemTTHeo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoDacDiemTTHeo()
        {
            var animalCounts = _dbContext.TrangTraiHeo.Where(x => x.IsDeleted != true)
            .Join(
                    _dbContext.District,
                    tt => tt.DistrictId,
                    d => d.Id,
                    (tt, d) => new { TrangTraiHeo = tt, DistrictName = d.DistrictName }
                    )
            .GroupBy(tt => tt.TrangTraiHeo.DistrictId)
            .Select(g => new DistrictAnimalCount
            {
                DistrictId = g.Key,
                DistrictName = g.Select(x => x.DistrictName).FirstOrDefault() ?? "Unknown",
                ChuongKin = g.Count(tt => tt.TrangTraiHeo.IsHangRaoKin == true),
                ChuongHo = g.Count(tt => tt.TrangTraiHeo.IsHangRaoHo == true),
                Biogas = g.Count(tt => tt.TrangTraiHeo.IsBiogas == "1"),
                ThaiXuongHo = g.Count(tt => tt.TrangTraiHeo.IsThaiXuongHo == "1"),
                BanTuoi = g.Count(tt => tt.TrangTraiHeo.IsBanTuoi == "1"),
                QuyHoachTrong = g.Count(tt => tt.TrangTraiHeo.IsQuyHoachTrong == true),
                QuyHoachNgoai = g.Count(tt => tt.TrangTraiHeo.IsQuyHoachNgoai == true),
                SinhSan = g.Count(tt => tt.TrangTraiHeo.IsSanXuatGiong == true),
                Thit = g.Count(tt => tt.TrangTraiHeo.IsSanXuatThit == true),
                HonHop = g.Count(tt => tt.TrangTraiHeo.IsSanXuatHonHop == true),

            })
            .ToList();
            return Ok(animalCounts);
        }

        [HttpGet("BaoCaoDacDiemTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoDacDiemTTGiaCam()
        {
            var animalCounts = _dbContext.TrangTraiGiaCam.Where(x => x.IsDeleted != true)
            //.Where(tt => tt.IsDeleted == false)
            .Join(
              _dbContext.District,
              tt => tt.DistrictId,
              d => d.Id,
              (tt, d) => new { TrangTraiGiaCam = tt, DistrictName = d.DistrictName }
          )
           .GroupBy(tt => tt.TrangTraiGiaCam.DistrictId)
           .Select(g => new DistrictAnimalCount
           {
               DistrictId = g.Key,
               DistrictName = g.Select(x => x.DistrictName).FirstOrDefault() ?? "Unknown",
               ChuongKin = g.Count(tt => tt.TrangTraiGiaCam.IsHangRaoKin == true),
               ChuongHo = g.Count(tt => tt.TrangTraiGiaCam.IsHangRaoHo == true),
               Biogas = g.Count(tt => tt.TrangTraiGiaCam.IsBiogas == "1"),
               ThaiXuongHo = g.Count(tt => tt.TrangTraiGiaCam.IsThaiXuongHo == "1"),
               BanTuoi = g.Count(tt => tt.TrangTraiGiaCam.IsBanTuoi == "1"),
               QuyHoachTrong = g.Count(tt => tt.TrangTraiGiaCam.IsQuyHoachTrong == true),
               QuyHoachNgoai = g.Count(tt => tt.TrangTraiGiaCam.IsQuyHoachNgoai == true),
               SinhSan = g.Count(tt => tt.TrangTraiGiaCam.IsSanXuatGiong == true),
               Thit = g.Count(tt => tt.TrangTraiGiaCam.IsSanXuatThit == true),
               Trung = g.Count(tt => tt.TrangTraiGiaCam.IsSanXuatTrung == true),
           })
           .ToList();
            return Ok(animalCounts);
        }

        [HttpGet("BaoCaoDacDiemTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoDacDiemTTGiaSuc()
        {
            var animalCounts = _dbContext.TrangTraiDaiGiaSuc.Where(x => x.IsDeleted != true)
            //.Where(tt => tt.IsDeleted == false)
            .Join(
              _dbContext.District,
              tt => tt.DistrictId,
              d => d.Id,
              (tt, d) => new { TrangTraiDaiGiaSuc = tt, DistrictName = d.DistrictName }
          )
           .GroupBy(tt => tt.TrangTraiDaiGiaSuc.DistrictId)
           .Select(g => new DistrictAnimalCount
           {
               DistrictId = g.Key,
               DistrictName = g.Select(x => x.DistrictName).FirstOrDefault() ?? "Unknown",
               ChuongKin = g.Count(tt => tt.TrangTraiDaiGiaSuc.IsHangRaoKin == true),
               ChuongHo = g.Count(tt => tt.TrangTraiDaiGiaSuc.IsHangRaoHo == true),
               Biogas = g.Count(tt => tt.TrangTraiDaiGiaSuc.IsHeThongBiogas == "1"),
               ThaiXuongHo = g.Count(tt => tt.TrangTraiDaiGiaSuc.IsThaiXuongHo == "1"),
               BanTuoi = g.Count(tt => tt.TrangTraiDaiGiaSuc.IsBanTuoi == "1"),
               QuyHoachTrong = g.Count(tt => tt.TrangTraiDaiGiaSuc.IsQuyHoachTrong == true),
               QuyHoachNgoai = g.Count(tt => tt.TrangTraiDaiGiaSuc.IsQuyHoachNgoai == true),
               SinhSan = g.Count(tt => tt.TrangTraiDaiGiaSuc.IsSanXuatGiong == true),
               Thit = g.Count(tt => tt.TrangTraiDaiGiaSuc.IsSanXuatThit == true),
               Sua = g.Count(tt => tt.TrangTraiDaiGiaSuc.IsSanXuatSua == true),
           })
           .ToList();
            return Ok(animalCounts);
        }

        [HttpGet("BaoCaoTiemPhongTTHeo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoTiemPhongTTHeo()
        {
            var query = from d in _dbContext.District
                        join tth in _dbContext.TrangTraiHeo.Where(x => x.IsDeleted != true) on d.Id equals tth.DistrictId into gjtth
                        from tth in gjtth.DefaultIfEmpty()
                        join tp in _dbContext.TiemPhongTrangTraiHeo on tth.Id equals tp.IdTrangTraiHeo into gjtp
                        from tp in gjtp.DefaultIfEmpty()
                        group new { tth, d, tp } by d.DistrictName into g
                        select new TiemPhong
                        {
                            DistrictName = g.Key,
                            TongDan = g.Sum(x => x.tth != null ? x.tth.QuyMo : 0),
                            SoTiemLMLM = g.Where(x => x.tp.LoaiBenh == "Bệnh LMLM").Sum(x => x.tp.SoDuocTiemPhong),
                            SoTiemDTH = g.Where(x => x.tp.LoaiBenh == "Bệnh DTH").Sum(x => x.tp.SoDuocTiemPhong),
                        };


            var result = query.ToList();

            foreach (var x in result)
            {
                if (x.TongDan != 0)
                {
                    x.TyLeTiemLMLM = Math.Round((double)((double)x.SoTiemLMLM / x.TongDan * 100), 2);
                    x.TyLeTiemDTH = Math.Round((double)((double)x.SoTiemDTH / x.TongDan * 100), 2);
                }
            }

            return Ok(result);
        }

        [HttpGet("BaoCaoTiemPhongTTGiaCam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoTiemPhongTTGiaCam()
        {
            var query = from d in _dbContext.District
                        join ttgc in _dbContext.TrangTraiGiaCam.Where(x => x.IsDeleted != true) on d.Id equals ttgc.DistrictId into gjttgc
                        from ttgc in gjttgc.DefaultIfEmpty()
                        join tp in _dbContext.TiemPhongGiaCam on ttgc.Id equals tp.IdTrangTraiGiaCam into gjtp
                        from tp in gjtp.DefaultIfEmpty()
                        group new { ttgc, d, tp } by d.DistrictName into g
                        select new TiemPhong
                        {
                            DistrictName = g.Key,
                            TongDan = g.Sum(x => x.ttgc != null ? x.ttgc.QuyMo : 0),
                            SoTiemCumGiaCam = g.Where(x => x.tp.LoaiBenh == "Bệnh cúm gia cầm").Sum(x => x.tp.SoDuocTiemPhong),
                            SoTiemNewcastle = g.Where(x => x.tp.LoaiBenh == "Bệnh Newcastle").Sum(x => x.tp.SoDuocTiemPhong),
                            SoTiemGumboro = g.Where(x => x.tp.LoaiBenh == "Gumboro").Sum(x => x.tp.SoDuocTiemPhong),
                        };
            var result = query.ToList();

            foreach (var x in result)
            {
                if (x.TongDan != 0)
                {
                    x.TyLeTiemCumGiaCam = Math.Round((double)((double)x.SoTiemCumGiaCam / x.TongDan * 100), 2);
                    x.TyLeTiemNewcastle = Math.Round((double)((double)x.SoTiemNewcastle / x.TongDan * 100), 2);
                    x.TyLeTiemGumboro = Math.Round((double)((double)x.SoTiemGumboro / x.TongDan * 100), 2);
                }
            }

            return Ok(result);
        }

        [HttpGet("BaoCaoTiemPhongTTGiaSuc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BaoCaoTiemPhongTTGiaSuc()
        {
            var query = from d in _dbContext.District
                        join ttgs in _dbContext.TrangTraiDaiGiaSuc.Where(x => x.IsDeleted != true) on d.Id equals ttgs.DistrictId into gjttgs
                        from ttgs in gjttgs.DefaultIfEmpty()
                        join tp in _dbContext.TiemPhongDaiGiaSuc on ttgs.Id equals tp.IdTrangTraiDaiGiaSuc into gjtp
                        from tp in gjtp.DefaultIfEmpty()
                        group new { ttgs, d, tp } by d.DistrictName into g
                        select new TiemPhong
                        {
                            DistrictName = g.Key,
                            TongDan = g.Sum(x => x.ttgs != null ? x.ttgs.TongDan : 0),
                            SoTiemLMLMTrau = g.Where(x => x.tp.LoaiBenh == "Bệnh LMLM" && x.tp.LoaiGiaSucTiemPhong == "Bò").Sum(x => x.tp.SoDuocTiemPhong),
                            SoTiemLMLMDe = g.Where(x => x.tp.LoaiBenh == "Bệnh LMLM" && x.tp.LoaiGiaSucTiemPhong == "Dê").Sum(x => x.tp.SoDuocTiemPhong),
                            SoTiemTHT = g.Where(x => x.tp.LoaiBenh == "Bệnh tụ huyết trùng trâu bò").Sum(x => x.tp.SoDuocTiemPhong),
                        };

            var result = query.ToList();
            foreach (var x in result)
            {
                if (x.TongDan != 0)
                {
                    x.TyLeTiemLMLMTrau = Math.Round((double)((double)x.SoTiemLMLMTrau / x.TongDan * 100), 2);
                    x.TyLeTiemLMLMDe = Math.Round((double)((double)x.SoTiemLMLMDe / x.TongDan * 100), 2);
                    x.TyLeTiemTHT = Math.Round((double)((double)x.SoTiemTHT / x.TongDan * 100), 2);
                }
            }

            return Ok(result);
        }

        [HttpGet("GetSaveBuffer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<SaveBufferPandemic>> GetSaveBuffer()
        {
            return await _dbContext.SaveBufferPandemic.ToListAsync();
        }

        [HttpGet("DeleteSaveBuffer/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteSaveBuffer(Guid id)
        {
            var savedBuffer = await _dbContext.SaveBufferPandemic.FindAsync(id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                _dbContext.SaveBufferPandemic.Remove(savedBuffer);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("TrangTraiChanNuoi", "GIS", new { area = "" });
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

        private async Task<IActionResult> Delete(Guid id)
        {
            var map = await _dbContext.CoSoGietMo.FindAsync(id);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {
                map.IsDeleted = true;
                _dbContext.CoSoGietMo.Update(map);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpPost("AddSaveBuffer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddSaveBuffer([FromBody] SaveBufferPandemic model)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.SaveBufferPandemic.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok(model);
            }
            return BadRequest(model);
        }
    }
}
