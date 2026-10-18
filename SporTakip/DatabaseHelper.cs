using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class DatabaseHelper
{
    private static string connectionString
    {
        get
        {
            var settings = ConfigurationManager.ConnectionStrings["SporTakip"];
            if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
                throw new ConfigurationErrorsException("SporTakip bağlantı ayarı bulunamadı.");
            return settings.ConnectionString;
        }
    }

    public static void DeleteSporcu(int id)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    foreach (string query in new[] {
                        "DELETE FROM Takipler WHERE SporcuId = @id",
                        "DELETE FROM Antrenmanlar WHERE SporcuId = @id",
                        "DELETE FROM Sporcular WHERE Id = @id" })
                    {
                        using (var cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                catch
                {
                    try { transaction.Rollback(); }
                    catch (Exception rollbackError) { SporTakip.ErrorHandler.Log(rollbackError); }
                    throw;
                }
            }
        }
    }
    /// <summary>
    /// INSERT, UPDATE, DELETE gibi sorguları çalıştırır.
    /// </summary>
    public static void ExecuteNonQuery(string query, params SqlParameter[] parameters)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SELECT sorgularını çalıştırır ve sonucu DataTable olarak döner.
    /// </summary>
    public static DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddRange(parameters);
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}

