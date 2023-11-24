using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DistrictId = table.Column<long>(type: "bigint", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dich",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoGiaSucBenh = table.Column<int>(type: "int", nullable: true),
                    TenBenhNghiNgo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IdTrangTrai = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dich", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiBenhGiaCam",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiGiaCam = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiBenhGiaCam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiBenhGiaSuc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiBenh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiBenhGiaSuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiBenhHeo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiBenh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiBenhHeo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiGiaCam",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiGiaCam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiGiaCam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiGiaSuc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiGiaSuc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiGiaSuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiTrangTrai",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiTrangTraiChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiTrangTrai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaveBufferPandemic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distance = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatitudeNumber = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    LongitudeNumber = table.Column<decimal>(type: "decimal(18,10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaveBufferPandemic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrangTraiTheoHuyen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Xa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<long>(type: "bigint", nullable: true),
                    TenTrangTrai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LonThit = table.Column<int>(type: "int", nullable: true),
                    LonNai = table.Column<int>(type: "int", nullable: true),
                    LonDucGiong = table.Column<int>(type: "int", nullable: true),
                    Bo = table.Column<int>(type: "int", nullable: true),
                    BoCaiSinhSan = table.Column<int>(type: "int", nullable: true),
                    BoDuc = table.Column<int>(type: "int", nullable: true),
                    Trau = table.Column<int>(type: "int", nullable: true),
                    Ngua = table.Column<int>(type: "int", nullable: true),
                    De = table.Column<int>(type: "int", nullable: true),
                    Cuu = table.Column<int>(type: "int", nullable: true),
                    Tho = table.Column<int>(type: "int", nullable: true),
                    GaThit = table.Column<int>(type: "int", nullable: true),
                    GaTrung = table.Column<int>(type: "int", nullable: true),
                    VitThit = table.Column<int>(type: "int", nullable: true),
                    VitTrung = table.Column<int>(type: "int", nullable: true),
                    Ngan = table.Column<int>(type: "int", nullable: true),
                    Ngong = table.Column<int>(type: "int", nullable: true),
                    DaDieu = table.Column<int>(type: "int", nullable: true),
                    ChimCut = table.Column<int>(type: "int", nullable: true),
                    BoCau = table.Column<int>(type: "int", nullable: true),
                    HuouSao = table.Column<int>(type: "int", nullable: true),
                    Yen = table.Column<int>(type: "int", nullable: true),
                    Cho = table.Column<int>(type: "int", nullable: true),
                    Meo = table.Column<int>(type: "int", nullable: true),
                    VitTroi = table.Column<int>(type: "int", nullable: true),
                    OngMat = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangTraiTheoHuyen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrangTraiTheoHuyen_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CoSoGietMo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenTrai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuTrangTrai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<long>(type: "bigint", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongitudeNumber = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    LatitudeNumber = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    CanBoDieuTra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayBaoCao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoBaoCao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDongVat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CongSuatGietMo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoHoThamGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoNguoiThamGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGiayPhepGietMo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGiayChungNhanGiamSat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGiayKiemDich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTuongRaoBaoQuanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLamLongTachBiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTuongLat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDoBaoHo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsViTriGietMo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHinhThucSoHuu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNguonGocDongVat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMuaGiaSuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNoiTieuThu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCungCapSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLoaiHinhGietMo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPhuongThucGietMo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsChon = table.Column<bool>(type: "bit", nullable: true),
                    IsDot = table.Column<bool>(type: "bit", nullable: true),
                    IsLuocChinChoCa = table.Column<bool>(type: "bit", nullable: true),
                    IsHamTuHuy = table.Column<bool>(type: "bit", nullable: true),
                    IsBanPhan = table.Column<bool>(type: "bit", nullable: true),
                    IsThaiTrucTiepRaVuon = table.Column<bool>(type: "bit", nullable: true),
                    IsThaiRaSuoi = table.Column<bool>(type: "bit", nullable: true),
                    IsThaiRaHoCa = table.Column<bool>(type: "bit", nullable: true),
                    IsBiogas = table.Column<bool>(type: "bit", nullable: true),
                    IsThaiXuongHoThu = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsDich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLoaiTrangTrai = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoSoGietMo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoSoGietMo_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoSoGietMo_LoaiTrangTrai_IdLoaiTrangTrai",
                        column: x => x.IdLoaiTrangTrai,
                        principalTable: "LoaiTrangTrai",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrangTraiDaiGiaSuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenTrai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuTrangTrai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<long>(type: "bigint", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongitudeNumber = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    LatitudeNumber = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    CanBoDieuTra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLoaiGiaSuc = table.Column<long>(type: "bigint", nullable: true),
                    DucGiong = table.Column<int>(type: "int", nullable: true),
                    Sua = table.Column<int>(type: "int", nullable: true),
                    SinhSan = table.Column<int>(type: "int", nullable: true),
                    Giong = table.Column<int>(type: "int", nullable: true),
                    TongDan = table.Column<int>(type: "int", nullable: true),
                    HinhThucChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhThucNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTuSanXuat = table.Column<bool>(type: "bit", nullable: true),
                    IsNhapDiaPhuong = table.Column<bool>(type: "bit", nullable: true),
                    IsNhapNgoaiTinh = table.Column<bool>(type: "bit", nullable: true),
                    NguonNuocQuaXuLy = table.Column<bool>(type: "bit", nullable: true),
                    IsNguonNuocKhongQuaXuLy = table.Column<bool>(type: "bit", nullable: true),
                    IsGiengKhoan = table.Column<bool>(type: "bit", nullable: true),
                    IsGiengDao = table.Column<bool>(type: "bit", nullable: true),
                    IsChon = table.Column<bool>(type: "bit", nullable: true),
                    IsDot = table.Column<bool>(type: "bit", nullable: true),
                    IsLuocChinChoCa = table.Column<bool>(type: "bit", nullable: true),
                    IsHamTuHuy = table.Column<bool>(type: "bit", nullable: true),
                    IsHeThongBiogas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsThuGom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsThaiTrucTiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUPhanViSinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsKhaiBaoNhap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsKhaiBaoXuat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDanhGiaTacDongMoiTruong = table.Column<bool>(type: "bit", nullable: true),
                    IsKeHoachBaoVeMoiTruong = table.Column<bool>(type: "bit", nullable: true),
                    IsXacNhanDKCN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoChungNhanDKCN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayXacNhanDKCN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsTruyXuatNguonGoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSoTheoDoiChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsQuyHoachChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsADTB = table.Column<bool>(type: "bit", nullable: true),
                    ATDBBenhKhac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoADTB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayADTB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoVIETGAHP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVIETGAHP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsBanTuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsThaiXuongHo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsQuyHoachTrong = table.Column<bool>(type: "bit", nullable: true),
                    IsQuyHoachNgoai = table.Column<bool>(type: "bit", nullable: true),
                    IsSanXuatGiong = table.Column<bool>(type: "bit", nullable: true),
                    IsSanXuatThit = table.Column<bool>(type: "bit", nullable: true),
                    IsSanXuatSua = table.Column<bool>(type: "bit", nullable: true),
                    IsHangRaoKin = table.Column<bool>(type: "bit", nullable: true),
                    SoDayChuongKin = table.Column<int>(type: "int", nullable: true),
                    IsHangRaoHo = table.Column<bool>(type: "bit", nullable: true),
                    SoDayChuongHo = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsDich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLoaiTrangTrai = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangTraiDaiGiaSuc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrangTraiDaiGiaSuc_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrangTraiDaiGiaSuc_LoaiGiaSuc_IdLoaiGiaSuc",
                        column: x => x.IdLoaiGiaSuc,
                        principalTable: "LoaiGiaSuc",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrangTraiDaiGiaSuc_LoaiTrangTrai_IdLoaiTrangTrai",
                        column: x => x.IdLoaiTrangTrai,
                        principalTable: "LoaiTrangTrai",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrangTraiGiaCam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenTrai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuTrangTrai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenCongTyDauTu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<long>(type: "bigint", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongitudeNumber = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    LatitudeNumber = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    HinhThucChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CongTyThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoCauGiong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyMo = table.Column<long>(type: "bigint", nullable: true),
                    IsGaLongMau = table.Column<bool>(type: "bit", nullable: true),
                    IsGaLongTrang = table.Column<bool>(type: "bit", nullable: true),
                    IdLoaiGiaCam = table.Column<long>(type: "bigint", nullable: true),
                    GiaCam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTichTrangTrai = table.Column<long>(type: "bigint", nullable: true),
                    DienTichXayDungChuong = table.Column<long>(type: "bigint", nullable: true),
                    IsTuongXay = table.Column<bool>(type: "bit", nullable: true),
                    IsTuongLuoiB40 = table.Column<bool>(type: "bit", nullable: true),
                    IsKhongCoTuong = table.Column<bool>(type: "bit", nullable: true),
                    IsHangRaoKin = table.Column<bool>(type: "bit", nullable: true),
                    SoDayChuongKin = table.Column<int>(type: "int", nullable: true),
                    IsHangRaoHo = table.Column<bool>(type: "bit", nullable: true),
                    SoDayChuongHo = table.Column<int>(type: "int", nullable: true),
                    IsQuyetDinhChuTruongDauTu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyetDinhChuTruongDauTu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyetDinhPheDuyetMoiTruong = table.Column<bool>(type: "bit", nullable: true),
                    KeHoachBaoVeMoiTruong = table.Column<bool>(type: "bit", nullable: true),
                    IsGiayPhepXayDungTrai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSoTheoDoiChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsChungNhanDieuKienChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoChungNhanDKCN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayChungNhanDKCN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoChungNhanVSTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayChungNhanVSTP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoChungNhanATDB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayChungNhanATDB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsChuoiGaATBDXuatKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsThuocVungATDB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVietGAHP = table.Column<bool>(type: "bit", nullable: true),
                    SoVietGAHP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVietGAHP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsNewcastle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsH5N1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHoSatTrung = table.Column<bool>(type: "bit", nullable: true),
                    IsPhongKhuTrung = table.Column<bool>(type: "bit", nullable: true),
                    IsMayPhunThuocTrung = table.Column<bool>(type: "bit", nullable: true),
                    IsKhongCoHeThongTieuDoc = table.Column<bool>(type: "bit", nullable: true),
                    IsThucAnChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguonNuocQuaXuLy = table.Column<bool>(type: "bit", nullable: true),
                    IsNguonNuocKhongQuaXuLy = table.Column<bool>(type: "bit", nullable: true),
                    IsGiengKhoan = table.Column<bool>(type: "bit", nullable: true),
                    IsGiengDao = table.Column<bool>(type: "bit", nullable: true),
                    IsKiemTraDinhKy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsChon = table.Column<bool>(type: "bit", nullable: true),
                    IsDot = table.Column<bool>(type: "bit", nullable: true),
                    IsLuocChinChoCa = table.Column<bool>(type: "bit", nullable: true),
                    Khac = table.Column<bool>(type: "bit", nullable: true),
                    BienPhapKhac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHopDongXuLyRac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBanTuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsThaiXuongHo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsQuyHoachTrong = table.Column<bool>(type: "bit", nullable: true),
                    IsQuyHoachNgoai = table.Column<bool>(type: "bit", nullable: true),
                    IsSanXuatGiong = table.Column<bool>(type: "bit", nullable: true),
                    IsSanXuatThit = table.Column<bool>(type: "bit", nullable: true),
                    IsSanXuatTrung = table.Column<bool>(type: "bit", nullable: true),
                    IsBiogas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTichKhuXuLy = table.Column<double>(type: "float", nullable: true),
                    HamBiogas = table.Column<double>(type: "float", nullable: true),
                    HoSinhHoc = table.Column<double>(type: "float", nullable: true),
                    IsNhaChuaPhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBaoCaoSoLieuChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H5N1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Newcastle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BenhKhac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsDich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLoaiTrangTrai = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangTraiGiaCam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrangTraiGiaCam_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrangTraiGiaCam_LoaiGiaCam_IdLoaiGiaCam",
                        column: x => x.IdLoaiGiaCam,
                        principalTable: "LoaiGiaCam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrangTraiGiaCam_LoaiTrangTrai_IdLoaiTrangTrai",
                        column: x => x.IdLoaiTrangTrai,
                        principalTable: "LoaiTrangTrai",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrangTraiHeo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenTrai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuTrangTrai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<long>(type: "bigint", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongitudeNumber = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    LatitudeNumber = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    TenCongTyDauTu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhThucChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CongTyThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoCauGiong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyMo = table.Column<int>(type: "int", nullable: true),
                    HeoNai = table.Column<int>(type: "int", nullable: true),
                    HeoThit = table.Column<int>(type: "int", nullable: true),
                    DucGiong = table.Column<int>(type: "int", nullable: true),
                    HeoConTheoMe = table.Column<int>(type: "int", nullable: true),
                    HeoCaiSua = table.Column<int>(type: "int", nullable: true),
                    IsQuyetDinhChuTruongDauTu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyetDinhChuTruongDauTu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyetDinhPheDuyetMoiTruong = table.Column<bool>(type: "bit", nullable: true),
                    KeHoachBaoVeMoiTruong = table.Column<bool>(type: "bit", nullable: true),
                    IsGiayPhepXayDungTrai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSoTheoDoiChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsChungNhanDieuKienChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoChungNhanDKCN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayChungNhanDKCN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoChungNhanVSTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayChungNhanVSTP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoChungNhanATDB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayChungNhanATDB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsATDBLMLM = table.Column<bool>(type: "bit", nullable: true),
                    IsATDBDichTa = table.Column<bool>(type: "bit", nullable: true),
                    IsATDBDichTaChauPhi = table.Column<bool>(type: "bit", nullable: true),
                    IsVietGAHP = table.Column<bool>(type: "bit", nullable: true),
                    SoVietGAHP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVietGAHP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DienTichTrangTrai = table.Column<double>(type: "float", nullable: true),
                    DienTichChuong = table.Column<double>(type: "float", nullable: true),
                    IsTuongXay = table.Column<bool>(type: "bit", nullable: true),
                    IsTuongLuoiB40 = table.Column<bool>(type: "bit", nullable: true),
                    IsKhongCoTuong = table.Column<bool>(type: "bit", nullable: true),
                    IsHangRaoKin = table.Column<bool>(type: "bit", nullable: true),
                    SoDayChuongKin = table.Column<int>(type: "int", nullable: true),
                    IsHangRaoHo = table.Column<bool>(type: "bit", nullable: true),
                    SoDayChuongHo = table.Column<int>(type: "int", nullable: true),
                    IsHoSatTrung = table.Column<bool>(type: "bit", nullable: true),
                    IsPhongKhuTrung = table.Column<bool>(type: "bit", nullable: true),
                    IsMayPhunThuocTrung = table.Column<bool>(type: "bit", nullable: true),
                    IsKhongCoHeThongTieuDoc = table.Column<bool>(type: "bit", nullable: true),
                    IsSanXuatGiong = table.Column<bool>(type: "bit", nullable: true),
                    IsSanXuatThit = table.Column<bool>(type: "bit", nullable: true),
                    IsSanXuatHonHop = table.Column<bool>(type: "bit", nullable: true),
                    IsTuSanXuat = table.Column<bool>(type: "bit", nullable: true),
                    IsNhapDiaPhuong = table.Column<bool>(type: "bit", nullable: true),
                    IsNhapNgoaiTinh = table.Column<bool>(type: "bit", nullable: true),
                    IsKhaiBaoNhap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsKhaiBaoXuat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsThucAnChanNuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguonNuocQuaXuLy = table.Column<bool>(type: "bit", nullable: true),
                    IsNguonNuocKhongQuaXuLy = table.Column<bool>(type: "bit", nullable: true),
                    IsGiengKhoan = table.Column<bool>(type: "bit", nullable: true),
                    IsGiengDao = table.Column<bool>(type: "bit", nullable: true),
                    IsKiemTraDinhKy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsChon = table.Column<bool>(type: "bit", nullable: true),
                    IsDot = table.Column<bool>(type: "bit", nullable: true),
                    IsLuocChinChoCa = table.Column<bool>(type: "bit", nullable: true),
                    Khac = table.Column<bool>(type: "bit", nullable: true),
                    BienPhapKhac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHopDongXuLyRac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBiogas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsThaiXuongHo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBanTuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsQuyHoachTrong = table.Column<bool>(type: "bit", nullable: true),
                    IsQuyHoachNgoai = table.Column<bool>(type: "bit", nullable: true),
                    DienTichKhuXuLy = table.Column<double>(type: "float", nullable: true),
                    HamBiogas = table.Column<double>(type: "float", nullable: true),
                    HoSinhHoc = table.Column<double>(type: "float", nullable: true),
                    IsBeXuLyHoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNhaChuaPhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBaoCaoSoLieu = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsDich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLoaiTrangTrai = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangTraiHeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrangTraiHeo_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrangTraiHeo_LoaiTrangTrai_IdLoaiTrangTrai",
                        column: x => x.IdLoaiTrangTrai,
                        principalTable: "LoaiTrangTrai",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CNATDBTTGiaSuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoADTB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayATDB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiDaiGiaSuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNATDBTTGiaSuc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNATDBTTGiaSuc_TrangTraiDaiGiaSuc_IdTrangTraiDaiGiaSuc",
                        column: x => x.IdTrangTraiDaiGiaSuc,
                        principalTable: "TrangTraiDaiGiaSuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNDKCNTTGiaSuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoDKCN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayDKCN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiDaiGiaSuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNDKCNTTGiaSuc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNDKCNTTGiaSuc_TrangTraiDaiGiaSuc_IdTrangTraiDaiGiaSuc",
                        column: x => x.IdTrangTraiDaiGiaSuc,
                        principalTable: "TrangTraiDaiGiaSuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNVietGAHPTTGiaSuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoVietGAHP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVietGAHP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiDaiGiaSuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVietGAHPTTGiaSuc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVietGAHPTTGiaSuc_TrangTraiDaiGiaSuc_IdTrangTraiDaiGiaSuc",
                        column: x => x.IdTrangTraiDaiGiaSuc,
                        principalTable: "TrangTraiDaiGiaSuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNVSTPTTGiaSuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoVSTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVSTP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiDaiGiaSuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVSTPTTGiaSuc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVSTPTTGiaSuc_TrangTraiDaiGiaSuc_IdTrangTraiDaiGiaSuc",
                        column: x => x.IdTrangTraiDaiGiaSuc,
                        principalTable: "TrangTraiDaiGiaSuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiemPhongDaiGiaSuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTiemPhong = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdLoaiGiaSuc = table.Column<long>(type: "bigint", nullable: true),
                    LoaiGiaSucTiemPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLoaiBenh = table.Column<long>(type: "bigint", nullable: true),
                    LoaiBenh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDuocTiemPhong = table.Column<int>(type: "int", nullable: true),
                    TenVaccine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiCungCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KetQuaGiamSat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IdTrangTraiDaiGiaSuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiemPhongDaiGiaSuc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiemPhongDaiGiaSuc_LoaiBenhGiaSuc_IdLoaiBenh",
                        column: x => x.IdLoaiBenh,
                        principalTable: "LoaiBenhGiaSuc",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TiemPhongDaiGiaSuc_LoaiGiaSuc_IdLoaiGiaSuc",
                        column: x => x.IdLoaiGiaSuc,
                        principalTable: "LoaiGiaSuc",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TiemPhongDaiGiaSuc_TrangTraiDaiGiaSuc_IdTrangTraiDaiGiaSuc",
                        column: x => x.IdTrangTraiDaiGiaSuc,
                        principalTable: "TrangTraiDaiGiaSuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TinhHinhDichBenhDaiGiaSuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThoiDiemPhatBenh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiDiemKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoBenh = table.Column<int>(type: "int", nullable: true),
                    IdLoaiBenh = table.Column<long>(type: "bigint", nullable: true),
                    LoaiBenh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KetQuaXetNghiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiVaccine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoKhoiBenh = table.Column<int>(type: "int", nullable: true),
                    SoChet = table.Column<int>(type: "int", nullable: true),
                    BienPhapXuLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTrangTraiDaiGiaSuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhHinhDichBenhDaiGiaSuc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TinhHinhDichBenhDaiGiaSuc_LoaiBenhGiaSuc_IdLoaiBenh",
                        column: x => x.IdLoaiBenh,
                        principalTable: "LoaiBenhGiaSuc",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TinhHinhDichBenhDaiGiaSuc_TrangTraiDaiGiaSuc_IdTrangTraiDaiGiaSuc",
                        column: x => x.IdTrangTraiDaiGiaSuc,
                        principalTable: "TrangTraiDaiGiaSuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNATDBTTGiaCam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoATDB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayATDB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiGiaCam = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNATDBTTGiaCam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNATDBTTGiaCam_TrangTraiGiaCam_IdTrangTraiGiaCam",
                        column: x => x.IdTrangTraiGiaCam,
                        principalTable: "TrangTraiGiaCam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNDKCNTTGiaCam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoDKCN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayDKCN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiGiaCam = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNDKCNTTGiaCam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNDKCNTTGiaCam_TrangTraiGiaCam_IdTrangTraiGiaCam",
                        column: x => x.IdTrangTraiGiaCam,
                        principalTable: "TrangTraiGiaCam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNVietGAHPTTGiaCam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoVietGAHP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVietGAHP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiGiaCam = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVietGAHPTTGiaCam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVietGAHPTTGiaCam_TrangTraiGiaCam_IdTrangTraiGiaCam",
                        column: x => x.IdTrangTraiGiaCam,
                        principalTable: "TrangTraiGiaCam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNVSTPTTGiaCam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoVSTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVSTP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiGiaCam = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVSTPTTGiaCam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVSTPTTGiaCam_TrangTraiGiaCam_IdTrangTraiGiaCam",
                        column: x => x.IdTrangTraiGiaCam,
                        principalTable: "TrangTraiGiaCam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiemPhongGiaCam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTiemPhong = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdLoaiGiaCam = table.Column<long>(type: "bigint", nullable: true),
                    LoaiGiaCamTiemPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLoaiBenh = table.Column<long>(type: "bigint", nullable: true),
                    LoaiBenh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDuocTiemPhong = table.Column<int>(type: "int", nullable: true),
                    TenVaccine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiCungCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KetQuaGiamSat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IdTrangTraiGiaCam = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiemPhongGiaCam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiemPhongGiaCam_LoaiBenhGiaCam_IdLoaiBenh",
                        column: x => x.IdLoaiBenh,
                        principalTable: "LoaiBenhGiaCam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TiemPhongGiaCam_LoaiGiaCam_IdLoaiGiaCam",
                        column: x => x.IdLoaiGiaCam,
                        principalTable: "LoaiGiaCam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TiemPhongGiaCam_TrangTraiGiaCam_IdTrangTraiGiaCam",
                        column: x => x.IdTrangTraiGiaCam,
                        principalTable: "TrangTraiGiaCam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TinhHinhDichBenhGiaCam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThoiDiemPhatBenh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiDiemKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoBenh = table.Column<int>(type: "int", nullable: true),
                    IdLoaiBenh = table.Column<long>(type: "bigint", nullable: true),
                    LoaiBenh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KetQuaXetNghiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiVaccine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoKhoiBenh = table.Column<int>(type: "int", nullable: true),
                    SoChet = table.Column<int>(type: "int", nullable: true),
                    BienPhapXuLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTrangTraiGiaCam = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhHinhDichBenhGiaCam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TinhHinhDichBenhGiaCam_LoaiBenhGiaCam_IdLoaiBenh",
                        column: x => x.IdLoaiBenh,
                        principalTable: "LoaiBenhGiaCam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TinhHinhDichBenhGiaCam_TrangTraiGiaCam_IdTrangTraiGiaCam",
                        column: x => x.IdTrangTraiGiaCam,
                        principalTable: "TrangTraiGiaCam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNATDBTTHeo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoATDB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayATDB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiHeo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNATDBTTHeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNATDBTTHeo_TrangTraiHeo_IdTrangTraiHeo",
                        column: x => x.IdTrangTraiHeo,
                        principalTable: "TrangTraiHeo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNDKCNTTHeo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoDKCN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayDKCN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiHeo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNDKCNTTHeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNDKCNTTHeo_TrangTraiHeo_IdTrangTraiHeo",
                        column: x => x.IdTrangTraiHeo,
                        principalTable: "TrangTraiHeo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNVietGAHPTTHeo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoVietGAHP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVietGAHP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiHeo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVietGAHPTTHeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVietGAHPTTHeo_TrangTraiHeo_IdTrangTraiHeo",
                        column: x => x.IdTrangTraiHeo,
                        principalTable: "TrangTraiHeo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNVSTPTTHeo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoVSTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVSTP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiHeo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVSTPTTHeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVSTPTTHeo_TrangTraiHeo_IdTrangTraiHeo",
                        column: x => x.IdTrangTraiHeo,
                        principalTable: "TrangTraiHeo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiemPhongTrangTraiHeo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTiemPhong = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdLoaiBenh = table.Column<long>(type: "bigint", nullable: true),
                    LoaiBenh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDuocTiemPhong = table.Column<int>(type: "int", nullable: true),
                    TenVaccine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiCungCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KetQuaGiamSat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IdTrangTraiHeo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiemPhongTrangTraiHeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiemPhongTrangTraiHeo_LoaiBenhHeo_IdLoaiBenh",
                        column: x => x.IdLoaiBenh,
                        principalTable: "LoaiBenhHeo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TiemPhongTrangTraiHeo_TrangTraiHeo_IdTrangTraiHeo",
                        column: x => x.IdTrangTraiHeo,
                        principalTable: "TrangTraiHeo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TinhHinhDichBenhTraiHeo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThoiDiemPhatBenh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiDiemKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoBenh = table.Column<int>(type: "int", nullable: true),
                    IdLoaiBenh = table.Column<long>(type: "bigint", nullable: true),
                    LoaiBenh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KetQuaXetNghiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiVaccine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoKhoiBenh = table.Column<int>(type: "int", nullable: true),
                    SoChet = table.Column<int>(type: "int", nullable: true),
                    BienPhapXuLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTrangTraiHeo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhHinhDichBenhTraiHeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TinhHinhDichBenhTraiHeo_LoaiBenhHeo_IdLoaiBenh",
                        column: x => x.IdLoaiBenh,
                        principalTable: "LoaiBenhHeo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TinhHinhDichBenhTraiHeo_TrangTraiHeo_IdTrangTraiHeo",
                        column: x => x.IdTrangTraiHeo,
                        principalTable: "TrangTraiHeo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CNATDBTTGiaCam_IdTrangTraiGiaCam",
                table: "CNATDBTTGiaCam",
                column: "IdTrangTraiGiaCam");

            migrationBuilder.CreateIndex(
                name: "IX_CNATDBTTGiaSuc_IdTrangTraiDaiGiaSuc",
                table: "CNATDBTTGiaSuc",
                column: "IdTrangTraiDaiGiaSuc");

            migrationBuilder.CreateIndex(
                name: "IX_CNATDBTTHeo_IdTrangTraiHeo",
                table: "CNATDBTTHeo",
                column: "IdTrangTraiHeo");

            migrationBuilder.CreateIndex(
                name: "IX_CNDKCNTTGiaCam_IdTrangTraiGiaCam",
                table: "CNDKCNTTGiaCam",
                column: "IdTrangTraiGiaCam");

            migrationBuilder.CreateIndex(
                name: "IX_CNDKCNTTGiaSuc_IdTrangTraiDaiGiaSuc",
                table: "CNDKCNTTGiaSuc",
                column: "IdTrangTraiDaiGiaSuc");

            migrationBuilder.CreateIndex(
                name: "IX_CNDKCNTTHeo_IdTrangTraiHeo",
                table: "CNDKCNTTHeo",
                column: "IdTrangTraiHeo");

            migrationBuilder.CreateIndex(
                name: "IX_CNVietGAHPTTGiaCam_IdTrangTraiGiaCam",
                table: "CNVietGAHPTTGiaCam",
                column: "IdTrangTraiGiaCam");

            migrationBuilder.CreateIndex(
                name: "IX_CNVietGAHPTTGiaSuc_IdTrangTraiDaiGiaSuc",
                table: "CNVietGAHPTTGiaSuc",
                column: "IdTrangTraiDaiGiaSuc");

            migrationBuilder.CreateIndex(
                name: "IX_CNVietGAHPTTHeo_IdTrangTraiHeo",
                table: "CNVietGAHPTTHeo",
                column: "IdTrangTraiHeo");

            migrationBuilder.CreateIndex(
                name: "IX_CNVSTPTTGiaCam_IdTrangTraiGiaCam",
                table: "CNVSTPTTGiaCam",
                column: "IdTrangTraiGiaCam");

            migrationBuilder.CreateIndex(
                name: "IX_CNVSTPTTGiaSuc_IdTrangTraiDaiGiaSuc",
                table: "CNVSTPTTGiaSuc",
                column: "IdTrangTraiDaiGiaSuc");

            migrationBuilder.CreateIndex(
                name: "IX_CNVSTPTTHeo_IdTrangTraiHeo",
                table: "CNVSTPTTHeo",
                column: "IdTrangTraiHeo");

            migrationBuilder.CreateIndex(
                name: "IX_CoSoGietMo_DistrictId",
                table: "CoSoGietMo",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_CoSoGietMo_IdLoaiTrangTrai",
                table: "CoSoGietMo",
                column: "IdLoaiTrangTrai");

            migrationBuilder.CreateIndex(
                name: "IX_TiemPhongDaiGiaSuc_IdLoaiBenh",
                table: "TiemPhongDaiGiaSuc",
                column: "IdLoaiBenh");

            migrationBuilder.CreateIndex(
                name: "IX_TiemPhongDaiGiaSuc_IdLoaiGiaSuc",
                table: "TiemPhongDaiGiaSuc",
                column: "IdLoaiGiaSuc");

            migrationBuilder.CreateIndex(
                name: "IX_TiemPhongDaiGiaSuc_IdTrangTraiDaiGiaSuc",
                table: "TiemPhongDaiGiaSuc",
                column: "IdTrangTraiDaiGiaSuc");

            migrationBuilder.CreateIndex(
                name: "IX_TiemPhongGiaCam_IdLoaiBenh",
                table: "TiemPhongGiaCam",
                column: "IdLoaiBenh");

            migrationBuilder.CreateIndex(
                name: "IX_TiemPhongGiaCam_IdLoaiGiaCam",
                table: "TiemPhongGiaCam",
                column: "IdLoaiGiaCam");

            migrationBuilder.CreateIndex(
                name: "IX_TiemPhongGiaCam_IdTrangTraiGiaCam",
                table: "TiemPhongGiaCam",
                column: "IdTrangTraiGiaCam");

            migrationBuilder.CreateIndex(
                name: "IX_TiemPhongTrangTraiHeo_IdLoaiBenh",
                table: "TiemPhongTrangTraiHeo",
                column: "IdLoaiBenh");

            migrationBuilder.CreateIndex(
                name: "IX_TiemPhongTrangTraiHeo_IdTrangTraiHeo",
                table: "TiemPhongTrangTraiHeo",
                column: "IdTrangTraiHeo");

            migrationBuilder.CreateIndex(
                name: "IX_TinhHinhDichBenhDaiGiaSuc_IdLoaiBenh",
                table: "TinhHinhDichBenhDaiGiaSuc",
                column: "IdLoaiBenh");

            migrationBuilder.CreateIndex(
                name: "IX_TinhHinhDichBenhDaiGiaSuc_IdTrangTraiDaiGiaSuc",
                table: "TinhHinhDichBenhDaiGiaSuc",
                column: "IdTrangTraiDaiGiaSuc");

            migrationBuilder.CreateIndex(
                name: "IX_TinhHinhDichBenhGiaCam_IdLoaiBenh",
                table: "TinhHinhDichBenhGiaCam",
                column: "IdLoaiBenh");

            migrationBuilder.CreateIndex(
                name: "IX_TinhHinhDichBenhGiaCam_IdTrangTraiGiaCam",
                table: "TinhHinhDichBenhGiaCam",
                column: "IdTrangTraiGiaCam");

            migrationBuilder.CreateIndex(
                name: "IX_TinhHinhDichBenhTraiHeo_IdLoaiBenh",
                table: "TinhHinhDichBenhTraiHeo",
                column: "IdLoaiBenh");

            migrationBuilder.CreateIndex(
                name: "IX_TinhHinhDichBenhTraiHeo_IdTrangTraiHeo",
                table: "TinhHinhDichBenhTraiHeo",
                column: "IdTrangTraiHeo");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTraiDaiGiaSuc_DistrictId",
                table: "TrangTraiDaiGiaSuc",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTraiDaiGiaSuc_IdLoaiGiaSuc",
                table: "TrangTraiDaiGiaSuc",
                column: "IdLoaiGiaSuc");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTraiDaiGiaSuc_IdLoaiTrangTrai",
                table: "TrangTraiDaiGiaSuc",
                column: "IdLoaiTrangTrai");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTraiGiaCam_DistrictId",
                table: "TrangTraiGiaCam",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTraiGiaCam_IdLoaiGiaCam",
                table: "TrangTraiGiaCam",
                column: "IdLoaiGiaCam");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTraiGiaCam_IdLoaiTrangTrai",
                table: "TrangTraiGiaCam",
                column: "IdLoaiTrangTrai");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTraiHeo_DistrictId",
                table: "TrangTraiHeo",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTraiHeo_IdLoaiTrangTrai",
                table: "TrangTraiHeo",
                column: "IdLoaiTrangTrai");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTraiTheoHuyen_DistrictId",
                table: "TrangTraiTheoHuyen",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CNATDBTTGiaCam");

            migrationBuilder.DropTable(
                name: "CNATDBTTGiaSuc");

            migrationBuilder.DropTable(
                name: "CNATDBTTHeo");

            migrationBuilder.DropTable(
                name: "CNDKCNTTGiaCam");

            migrationBuilder.DropTable(
                name: "CNDKCNTTGiaSuc");

            migrationBuilder.DropTable(
                name: "CNDKCNTTHeo");

            migrationBuilder.DropTable(
                name: "CNVietGAHPTTGiaCam");

            migrationBuilder.DropTable(
                name: "CNVietGAHPTTGiaSuc");

            migrationBuilder.DropTable(
                name: "CNVietGAHPTTHeo");

            migrationBuilder.DropTable(
                name: "CNVSTPTTGiaCam");

            migrationBuilder.DropTable(
                name: "CNVSTPTTGiaSuc");

            migrationBuilder.DropTable(
                name: "CNVSTPTTHeo");

            migrationBuilder.DropTable(
                name: "CoSoGietMo");

            migrationBuilder.DropTable(
                name: "Dich");

            migrationBuilder.DropTable(
                name: "SaveBufferPandemic");

            migrationBuilder.DropTable(
                name: "TiemPhongDaiGiaSuc");

            migrationBuilder.DropTable(
                name: "TiemPhongGiaCam");

            migrationBuilder.DropTable(
                name: "TiemPhongTrangTraiHeo");

            migrationBuilder.DropTable(
                name: "TinhHinhDichBenhDaiGiaSuc");

            migrationBuilder.DropTable(
                name: "TinhHinhDichBenhGiaCam");

            migrationBuilder.DropTable(
                name: "TinhHinhDichBenhTraiHeo");

            migrationBuilder.DropTable(
                name: "TrangTraiTheoHuyen");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LoaiBenhGiaSuc");

            migrationBuilder.DropTable(
                name: "TrangTraiDaiGiaSuc");

            migrationBuilder.DropTable(
                name: "LoaiBenhGiaCam");

            migrationBuilder.DropTable(
                name: "TrangTraiGiaCam");

            migrationBuilder.DropTable(
                name: "LoaiBenhHeo");

            migrationBuilder.DropTable(
                name: "TrangTraiHeo");

            migrationBuilder.DropTable(
                name: "LoaiGiaSuc");

            migrationBuilder.DropTable(
                name: "LoaiGiaCam");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "LoaiTrangTrai");
        }
    }
}
