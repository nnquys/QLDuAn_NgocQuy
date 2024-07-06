using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using QLDuAn_NgocQuy.Models;

namespace QLDuAn_NgocQuy.Data
{
    public class DuAnService
    {
        private readonly string _connectionString;

        public DuAnService(string ConnectionStrings)
        {
            _connectionString = ConnectionStrings;
        }

        public async Task<List<DuAn>> GetDanhSachDuAnAsync()
        {
            var duAnList = new List<DuAn>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var sql = "SELECT * FROM DuAn";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var duAn = new DuAn
                                {
                                    MaDuAn = reader["MaDuAn"].ToString(),
                                    TenDuAn = reader["TenDuAn"].ToString(),
                                    MoTa = reader["MoTa"].ToString(),
                                    NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                                    NgayKetThuc = reader.GetDateTime(reader.GetOrdinal("NgayKetThuc")),
                                    TrangThai = reader["TrangThai"].ToString()
                                };

                                duAnList.Add(duAn);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching projects: {ex.Message}");
                throw; // Rethrow the exception to propagate it for debugging purposes
            }

            return duAnList;
        }

        public async Task AddDuAnAsync(DuAn newDuAn)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var sql = "INSERT INTO DuAn (MaDuAn, TenDuAn, MoTa, NgayBatDau, NgayKetThuc, TrangThai) " +
                              "VALUES (@MaDuAn, @TenDuAn, @MoTa, @NgayBatDau, @NgayKetThuc, @TrangThai)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MaDuAn", newDuAn.MaDuAn);
                        command.Parameters.AddWithValue("@TenDuAn", newDuAn.TenDuAn);
                        command.Parameters.AddWithValue("@MoTa", newDuAn.MoTa);
                        command.Parameters.AddWithValue("@NgayBatDau", newDuAn.NgayBatDau);
                        command.Parameters.AddWithValue("@NgayKetThuc", newDuAn.NgayKetThuc);
                        command.Parameters.AddWithValue("@TrangThai", newDuAn.TrangThai);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding project: {ex.Message}");
                throw; // Rethrow the exception to propagate it for debugging purposes
            }
        }
        public async Task DeleteDuAnAsync(string maDuAn)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var sql = "DELETE FROM DuAn WHERE MaDuAn = @MaDuAn";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MaDuAn", maDuAn);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting project: {ex.Message}");
                throw; // Rethrow the exception to propagate it for debugging purposes
            }
        }

        public async Task UpdateDuAnAsync(DuAn updatedDuAn)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var sql = "UPDATE DuAn SET TenDuAn = @TenDuAn, MoTa = @MoTa, NgayBatDau = @NgayBatDau, " +
                              "NgayKetThuc = @NgayKetThuc, TrangThai = @TrangThai WHERE MaDuAn = @MaDuAn";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MaDuAn", updatedDuAn.MaDuAn);
                        command.Parameters.AddWithValue("@TenDuAn", updatedDuAn.TenDuAn);
                        command.Parameters.AddWithValue("@MoTa", updatedDuAn.MoTa);
                        command.Parameters.AddWithValue("@NgayBatDau", updatedDuAn.NgayBatDau);
                        command.Parameters.AddWithValue("@NgayKetThuc", updatedDuAn.NgayKetThuc);
                        command.Parameters.AddWithValue("@TrangThai", updatedDuAn.TrangThai);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating project: {ex.Message}");
                throw; // Rethrow the exception to propagate it for debugging purposes
            }
        }
        public async Task<List<DuAn>> GetDanhSachDuAnTheoThangNamAsync(int month, int year)
        {
            var duAnList = new List<DuAn>();

            string query = "SELECT * FROM DuAn " +
                           "WHERE MONTH(NgayBatDau) = @Month AND YEAR(NgayBatDau) = @Year";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);

                    await conn.OpenAsync();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var duAn = new DuAn
                        {
                            MaDuAn = reader["MaDuAn"].ToString(),
                            TenDuAn = reader["TenDuAn"].ToString(),
                            MoTa = reader["MoTa"].ToString(),
                            NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                            NgayKetThuc = reader.GetDateTime(reader.GetOrdinal("NgayKetThuc")),
                            TrangThai = reader["TrangThai"].ToString()
                        };

                        duAnList.Add(duAn);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching projects: {ex.Message}");
                throw;
            }

            return duAnList;
        }
    }
}
