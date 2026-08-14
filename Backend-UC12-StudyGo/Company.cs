using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

public class Company
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public string cnpj { get; set; } = string.Empty;
    public DateTime foundation { get; set; }
    public string places { get; set; } = string.Empty;
    public string fundaments { get; set; } = string.Empty;
    public string methods { get; set; } = string.Empty;
    public int ranking { get; set; } = 0;
    public User owner { get; set; } = new User();
    
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }

    public const string tabela = "companies";

    public Company() { }

    public Company(int id)
    {
        this.id = id;
    }

    public Company(string name, string cnpj, DateTime foundation, string places, string fundaments, string methods, int ranking, User owner)
    {
        this.name = name;
        this.cnpj = cnpj;
        this.foundation = foundation;
        this.places = places;
        this.fundaments = fundaments;
        this.methods = methods;
        this.ranking = ranking;
        this.owner = owner;
    }

    public void Mostrar()
    {
        string ownerName = owner != null && owner.id != 0 ? owner.id.ToString() : "N/A";
        Console.WriteLine($"| {id,-5} | {name,-20} | {cnpj,-18} | {foundation.ToString("dd/MM/yyyy"),-12} | {ranking,-7} | {ownerName,-10} |");
    }

    public static void Mostrar(List<Company> companies)
    {
        Console.WriteLine("\n=========================================================================================");
        Console.WriteLine($"| {"ID",-5} | {"NOME",-20} | {"CNPJ",-18} | {"FUNDAÇÃO",-12} | {"RANKING",-7} | {"OWNER ID",-10} |");
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        foreach (var c in companies)
        {
            c.Mostrar();
        }
        Console.WriteLine("=========================================================================================\n");
    }

    public async Task InserirAsync()
    {
        string query = $@"
            INSERT INTO {tabela}
            (name, cnpj, foundation, places, fundaments, methods, ranking, owner_id, createdAt, updatedAt)
            VALUES 
            (@name, @cnpj, @foundation, @places, @fundaments, @methods, @ranking, @owner_id, @createdAt, @updatedAt);
            ";
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("name", name);
        comando.Parameters.AddWithValue("cnpj", cnpj);
        comando.Parameters.AddWithValue("foundation", foundation);
        comando.Parameters.AddWithValue("places", places);
        comando.Parameters.AddWithValue("fundaments", fundaments);
        comando.Parameters.AddWithValue("methods", methods);
        comando.Parameters.AddWithValue("ranking", ranking);
        
        if (owner == null || owner.id == 0)
            comando.Parameters.AddWithValue("owner_id", DBNull.Value);
        else
            comando.Parameters.AddWithValue("owner_id", owner.id);

        comando.Parameters.AddWithValue("createdAt", DateTime.Now);
        comando.Parameters.AddWithValue("updatedAt", DateTime.Now);

        await conexao.OpenAsync();
        await comando.ExecuteNonQueryAsync();
    }

    public async Task BuscaAsync(int id)
    {
        string query = $"""
           SELECT * FROM {tabela} WHERE id = @id
           """;
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("id", id);

        await conexao.OpenAsync();
        using var dados = await comando.ExecuteReaderAsync();

        if (await dados.ReadAsync())
        {
            this.id = dados.GetInt32("id");
            this.name = dados.GetString("name");
            this.cnpj = dados.GetString("cnpj");
            this.foundation = dados.GetDateTime("foundation");
            this.places = dados.GetString("places");
            this.fundaments = dados.GetString("fundaments");
            this.methods = dados.GetString("methods");
            this.ranking = dados.GetInt32("ranking");
            
            if (!dados.IsDBNull(dados.GetOrdinal("owner_id")))
            {
                int owner_id = dados.GetInt32("owner_id");
                this.owner = new User(owner_id);
            }
            else
            {
                this.owner = null;
            }

            this.createdAt = dados.GetDateTime("createdAt");
            this.updatedAt = dados.GetDateTime("updatedAt");
        }
    }

    public static async Task<List<Company>> BuscarTodosAsync()
    {
        string query = $"SELECT * FROM {tabela}";
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);

        await conexao.OpenAsync();
        using var dados = await comando.ExecuteReaderAsync();

        List<Company> companies = new();
        while (await dados.ReadAsync())
        {
            Company c = new();
            c.id = dados.GetInt32("id");
            c.name = dados.GetString("name");
            c.cnpj = dados.GetString("cnpj");
            c.foundation = dados.GetDateTime("foundation");
            c.places = dados.GetString("places");
            c.fundaments = dados.GetString("fundaments");
            c.methods = dados.GetString("methods");
            c.ranking = dados.GetInt32("ranking");
            
            if (!dados.IsDBNull(dados.GetOrdinal("owner_id")))
            {
                int owner_id = dados.GetInt32("owner_id");
                c.owner = new User(owner_id);
            }
            else
            {
                c.owner = null;
            }

            c.createdAt = dados.GetDateTime("createdAt");
            c.updatedAt = dados.GetDateTime("updatedAt");
            companies.Add(c);
        }

        return companies;
    }

    public async Task AlterarAsync()
    {
        string query = $@"
            UPDATE {tabela}
            SET name = @name, cnpj = @cnpj, foundation = @foundation, places = @places, 
                fundaments = @fundaments, methods = @methods, ranking = @ranking, 
                owner_id = @owner_id, updatedAt = @updatedAt
            WHERE id = @id;
            ";
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("id", id);
        comando.Parameters.AddWithValue("name", name);
        comando.Parameters.AddWithValue("cnpj", cnpj);
        comando.Parameters.AddWithValue("foundation", foundation);
        comando.Parameters.AddWithValue("places", places);
        comando.Parameters.AddWithValue("fundaments", fundaments);
        comando.Parameters.AddWithValue("methods", methods);
        comando.Parameters.AddWithValue("ranking", ranking);
        
        if (owner == null || owner.id == 0)
            comando.Parameters.AddWithValue("owner_id", DBNull.Value);
        else
            comando.Parameters.AddWithValue("owner_id", owner.id);

        comando.Parameters.AddWithValue("updatedAt", DateTime.Now);

        await conexao.OpenAsync();
        await comando.ExecuteNonQueryAsync();
    }

    public async Task RemoverAsync()
    {
        string query = $"DELETE FROM {tabela} WHERE id = @id;";
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("id", id);

        await conexao.OpenAsync();
        await comando.ExecuteNonQueryAsync();
    }
}
