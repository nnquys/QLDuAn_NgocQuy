using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using QLDuAn_NgocQuy.Models;

namespace QLDuAn_NgocQuy.Data
{
    public class ThanhVienService
    {
        private readonly string _connection;

        public ThanhVienService(string connectionString)
        {
            _connection = connectionString;
        }

        public async Task<List<ThanhVien>> GetDanhSachThanhVienAsync()
        {
            var thanhVienList = new List<ThanhVien>();

            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    await connection.OpenAsync();

                    var sql = "SELECT * FROM ThanhVien";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var thanhVien = new ThanhVien
                                {
                                    MaThanhVien = reader["MaThanhVien"].ToString(),
                                    TenThanhVien = reader["TenThanhVien"].ToString(),
                                    ChucVu = reader["ChucVu"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    SDT = reader["SDT"].ToString()
                                };

                                thanhVienList.Add(thanhVien);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching members: {ex.Message}");
                throw;
            }

            return thanhVienList;
        }
        public async Task<string> GetMaxMaThanhVien()
        {
            string maxMaThanhVien = null;
            string query = "SELECT MAX(CAST(SUBSTRING(MaThanhVien, 3, LEN(MaThanhVien)-2) AS INT)) AS MaxMaThanhVien FROM ThanhVien";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    maxMaThanhVien = (await cmd.ExecuteScalarAsync()).ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching max MaThanhVien: {ex.Message}");
            }

            return maxMaThanhVien;
        }
        public async Task AddThanhVienAsync(ThanhVien newThanhVien)
        {
            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    await connection.OpenAsync();

                    var sql = "INSERT INTO ThanhVien (MaThanhVien, TenThanhVien, ChucVu, Email, SDT) " +
                              "VALUES (@MaThanhVien, @TenThanhVien, @ChucVu, @Email, @SDT)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MaThanhVien", newThanhVien.MaThanhVien);
                        command.Parameters.AddWithValue("@TenThanhVien", newThanhVien.TenThanhVien);
                        command.Parameters.AddWithValue("@ChucVu", newThanhVien.ChucVu);
                        command.Parameters.AddWithValue("@Email", newThanhVien.Email);
                        command.Parameters.AddWithValue("@SDT", newThanhVien.SDT);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding member: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateThanhVienAsync(ThanhVien updatedThanhVien)
        {
            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    await connection.OpenAsync();

                    var sql = "UPDATE ThanhVien SET TenThanhVien = @TenThanhVien, ChucVu = @ChucVu, " +
                              "Email = @Email, SDT = @SDT WHERE MaThanhVien = @MaThanhVien";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MaThanhVien", updatedThanhVien.MaThanhVien);
                        command.Parameters.AddWithValue("@TenThanhVien", updatedThanhVien.TenThanhVien);
                        command.Parameters.AddWithValue("@ChucVu", updatedThanhVien.ChucVu);
                        command.Parameters.AddWithValue("@Email", updatedThanhVien.Email);
                        command.Parameters.AddWithValue("@SDT", updatedThanhVien.SDT);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating member: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteThanhVienAsync(string maThanhVien)
        {
            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    await connection.OpenAsync();

                    var sql = "DELETE FROM ThanhVien WHERE MaThanhVien = @MaThanhVien";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MaThanhVien", maThanhVien);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting member: {ex.Message}");
                throw;
            }
        }
        public async Task<ThanhVien> GetThanhVienByIdAsync(string maThanhVien)
        {
            ThanhVien thanhVien = null;
            string query = "SELECT * FROM ThanhVien WHERE MaThanhVien = @MaThanhVien";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaThanhVien", maThanhVien);
                    conn.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        thanhVien = new ThanhVien
                        {
                            MaThanhVien = reader["MaThanhVien"].ToString(),
                            TenThanhVien = reader["TenThanhVien"].ToString(),
                            ChucVu = reader["ChucVu"].ToString(),
                            Email = reader["Email"].ToString(),
                            SDT = reader["SDT"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching thanh vien data: {ex.Message}");
            }

            return thanhVien;
        }

    }
}
