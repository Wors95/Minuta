using Microsoft.Data.Sqlite;
using Minuta.Domain.Entidades;
using Minuta.Domain.Enums;

namespace Minuta.Infrastructure.Data
{
    public class Database
    {
        private const string _connectionString = "Data Source=Data/minuta.db";

        public void Initialize()
        {
            if (!Directory.Exists("Data"))
                Directory.CreateDirectory("Data");

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Relogios (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    Preco REAL NOT NULL,
                    Quantidade INTEGER NOT NULL,
                    Tipo INTEGER NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }

        public async Task AdicionarRelogioAsync(Relogio relogio)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Relogios (Nome, Preco, Quantidade, Tipo)
                VALUES ($nome, $preco, $quantidade, $tipo);
            ";

            command.Parameters.AddWithValue("$nome", relogio.Nome);
            command.Parameters.AddWithValue("$preco", (double)relogio.Preco); // Corrigido: decimal para double
            command.Parameters.AddWithValue("$quantidade", relogio.Quantidade);
            command.Parameters.AddWithValue("$tipo", (int)relogio.Tipo); // Corrigido: Enum para int

            await command.ExecuteNonQueryAsync();
        }

        public List<Relogio> ListarRelogios()
        {
            var relogios = new List<Relogio>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Nome, Preco, Quantidade, Tipo FROM Relogios";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                relogios.Add(new Relogio
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Preco = (decimal)reader.GetDouble(2), // Corrigido: double para decimal
                    Quantidade = reader.GetInt32(3),
                    Tipo = (TipoRelogio)reader.GetInt32(4) // Corrigido: inteiro para Enum
                });
            }

            return relogios;
        }
    }
}
