using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using QLDuAn_NgocQuy.Models;

namespace QLDuAn_NgocQuy.Data
{
    public class PhanCongCVService
    {
        private readonly string _connectionString;

        public PhanCongCVService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<PhanCongCV>> GetAllAsync()
        {
            var phanCongList = new List<PhanCongCV>();

            string query = "SELECT * FROM PhanCongCongViec";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var phanCong = new PhanCongCV
                        {
                            MaPhanCong = reader["MaPhanCong"].ToString(),
                            MaDuAn = reader["MaDuAn"].ToString(),
                            MaThanhVien = reader["MaThanhVien"].ToString(),
                            CongViec = reader["CongViec"].ToString(),
                            NgayGiao = Convert.ToDateTime(reader["NgayGiao"]),
                            HanCuoi = Convert.ToDateTime(reader["HanCuoi"]),
                            TrangThai = reader["TrangThai"].ToString()
                        };
                        phanCongList.Add(phanCong);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching data: {ex.Message}");
            }

            return phanCongList;
        }

        public async Task<List<ThanhVien>> GetDanhSachThanhVienAsync()
        {
            var thanhVienList = new List<ThanhVien>();

            string query = "SELECT * FROM ThanhVien";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var thanhVien = new ThanhVien
                        {
                            MaThanhVien = reader["MaThanhVien"].ToString(),
                            TenThanhVien = reader["TenThanhVien"].ToString()
                        };
                        thanhVienList.Add(thanhVien);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching data: {ex.Message}");
            }

            return thanhVienList;
        }

        public async Task<List<DuAn>> GetDanhSachDuAnAsync()
        {
            var duAnList = new List<DuAn>();

            string query = "SELECT * FROM DuAn";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var duAn = new DuAn
                        {
                            MaDuAn = reader["MaDuAn"].ToString(),
                            TenDuAn = reader["TenDuAn"].ToString()
                        };
                        duAnList.Add(duAn);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching data: {ex.Message}");
            }

            return duAnList;
        }

        public async Task<string> GetMaxMaPhanCong()
        {
            string maxMaPhanCong = null;
            string query = "SELECT MAX(CAST(SUBSTRING(MaPhanCong, 3, LEN(MaPhanCong)-2) AS INT)) AS MaxMaPhanCong FROM PhanCongCongViec";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    maxMaPhanCong = (await cmd.ExecuteScalarAsync()).ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching max MaPhanCong: {ex.Message}");
            }

            return maxMaPhanCong;
        }

        public async Task InsertAsync(PhanCongCV phanCong)
        {
            string query = "INSERT INTO PhanCongCongViec (MaPhanCong, MaDuAn, MaThanhVien, CongViec, NgayGiao, HanCuoi, TrangThai) " +
                           "VALUES (@MaPhanCong, @MaDuAn, @MaThanhVien, @CongViec, @NgayGiao, @HanCuoi, @TrangThai)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaPhanCong", phanCong.MaPhanCong);
                    cmd.Parameters.AddWithValue("@MaDuAn", phanCong.MaDuAn);
                    cmd.Parameters.AddWithValue("@MaThanhVien", phanCong.MaThanhVien);
                    cmd.Parameters.AddWithValue("@CongViec", phanCong.CongViec);
                    cmd.Parameters.AddWithValue("@NgayGiao", phanCong.NgayGiao);
                    cmd.Parameters.AddWithValue("@HanCuoi", phanCong.HanCuoi);
                    cmd.Parameters.AddWithValue("@TrangThai", phanCong.TrangThai);

                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error inserting data: {ex.Message}");
            }
        }

        public async Task UpdateAsync(PhanCongCV phanCong)
        {
            string query = "UPDATE PhanCongCongViec SET MaDuAn = @MaDuAn, MaThanhVien = @MaThanhVien, CongViec = @CongViec, " +
                           "NgayGiao = @NgayGiao, HanCuoi = @HanCuoi, TrangThai = @TrangThai WHERE MaPhanCong = @MaPhanCong";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaPhanCong", phanCong.MaPhanCong);
                    cmd.Parameters.AddWithValue("@MaDuAn", phanCong.MaDuAn);
                    cmd.Parameters.AddWithValue("@MaThanhVien", phanCong.MaThanhVien);
                    cmd.Parameters.AddWithValue("@CongViec", phanCong.CongViec);
                    cmd.Parameters.AddWithValue("@NgayGiao", phanCong.NgayGiao);
                    cmd.Parameters.AddWithValue("@HanCuoi", phanCong.HanCuoi);
                    cmd.Parameters.AddWithValue("@TrangThai", phanCong.TrangThai);

                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating data: {ex.Message}");
            }
        }

        public async Task DeleteAsync(string maPhanCong)
        {
            string query = "DELETE FROM PhanCongCongViec WHERE MaPhanCong = @MaPhanCong";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaPhanCong", maPhanCong);

                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting data: {ex.Message}");
            }
        }
        public async Task<List<PhanCongCV>> GetDanhSachPhanCongTheoDuAnAsync(string maDuAn)
        {
            var phanCongList = new List<PhanCongCV>();

            string query = "SELECT * FROM PhanCongCongViec WHERE MaDuAn = @MaDuAn";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaDuAn", maDuAn);
                    conn.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var phanCong = new PhanCongCV
                        {
                            MaPhanCong = reader["MaPhanCong"].ToString(),
                            MaDuAn = reader["MaDuAn"].ToString(),
                            MaThanhVien = reader["MaThanhVien"].ToString(),
                            CongViec = reader["CongViec"].ToString(),
                            NgayGiao = Convert.ToDateTime(reader["NgayGiao"]),
                            HanCuoi = Convert.ToDateTime(reader["HanCuoi"]),
                            TrangThai = reader["TrangThai"].ToString()
                        };
                        phanCongList.Add(phanCong);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching phan cong data: {ex.Message}");
            }

            return phanCongList;
        }

    }
}
