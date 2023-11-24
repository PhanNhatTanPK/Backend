using Backend.Data;
using Microsoft.EntityFrameworkCore;


namespace Backend.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TrangTraiContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TrangTraiContext>>()))
            {

                if (context.LoaiTrangTrai.Any())
                {
                    return;
                }
                context.LoaiTrangTrai.AddRange(
                    new Models.LoaiTrangTrai()
                    {
                        LoaiTrangTraiChanNuoi = "Trang trại heo"
                    },
                    new Models.LoaiTrangTrai()
                    {
                        LoaiTrangTraiChanNuoi = "Trang trại đại gia súc"
                    },
                    new Models.LoaiTrangTrai()
                    {
                        LoaiTrangTraiChanNuoi = "Cơ sở giết mổ"
                    }, new Models.LoaiTrangTrai()
                    {
                        LoaiTrangTraiChanNuoi = "Trang trại gia cầm"
                    }

                    );
                context.LoaiGiaSuc.AddRange(
                    new Models.LoaiGiaSuc()
                    {
                        TenLoaiGiaSuc = "Bò"
                    }, new Models.LoaiGiaSuc()
                    {
                        TenLoaiGiaSuc = "Dê"
                    }
                    , new Models.LoaiGiaSuc()
                    {
                        TenLoaiGiaSuc = "Trâu"
                    }
                    , new Models.LoaiGiaSuc()
                    {
                        TenLoaiGiaSuc = "Ngựa"
                    }
                    , new Models.LoaiGiaSuc()
                    {
                        TenLoaiGiaSuc = "Cừu"
                    });
                context.LoaiBenhHeo.AddRange(
                    new Models.LoaiBenhHeo()
                    {
                        TenLoaiBenh = "Bệnh LMLM"
                    }, new Models.LoaiBenhHeo()
                    {
                        TenLoaiBenh = "Bệnh DTH"
                    });
                context.LoaiGiaCam.AddRange(
                    new Models.LoaiGiaCam()
                    {
                        TenLoaiGiaCam = "Gà"
                    }, new Models.LoaiGiaCam()
                    {
                        TenLoaiGiaCam = "Vịt"
                    }, new Models.LoaiGiaCam()
                    {
                        TenLoaiGiaCam = "Cút"
                    });
                context.LoaiBenhGiaSuc.AddRange(
                    new Models.LoaiBenhGiaSuc()
                    {
                        TenLoaiBenh = "Bệnh LMLM"
                    },
                    new Models.LoaiBenhGiaSuc()
                    {
                        TenLoaiBenh = "Bệnh tụ huyết trùng trâu bò"
                    }

                    );
                context.LoaiBenhGiaCam.AddRange(
                    new Models.LoaiBenhGiaCam()
                    {
                        TenLoaiGiaCam = "Bệnh cúm gia cầm"
                    },
                    new Models.LoaiBenhGiaCam()
                    {
                        TenLoaiGiaCam = "Bệnh Newcastle"
                    },
                    new Models.LoaiBenhGiaCam()
                    {
                        TenLoaiGiaCam = "Gumboro"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
