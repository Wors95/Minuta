using Microsoft.Data.Sqlite;
using Minuta.Domain;
using Minuta.Domain.Enums; // Se estiver usando enums
using Minuta.Domain.Entidades; // Se a classe Relogio estiver nesse namespace


namespace Minuta.Infrastructure.Data
{
    public class Database
    {
        private const string _connectionString = "Data Source=Data/minuta.db";

        // Método que cria a tabela de Relogios (mantido)
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

        // Método para adicionar um relógio (assíncrono)
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
            command.Parameters.AddWithValue("$preco", relogio.Preco);
            command.Parameters.AddWithValue("$quantidade", relogio.Quantidade);
            command.Parameters.AddWithValue("$tipo", relogio.Tipo);

            await command.ExecuteNonQueryAsync();
        }

        // Método para listar todos os relógios (adicionado)
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
                    Preco = reader.GetInt32(2),
                    Quantidade = reader.GetInt32(3),
                    Tipo = reader.GetString(10)
                });
            }

            return relogios;
        }
    }
}
