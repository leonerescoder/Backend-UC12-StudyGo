using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

public class Course
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string url_img { get; set; } = "";
    public float? workload { get; set; }
    public int ranking { get; set; } = 0;
    public string Field_of_study { get; set; }
    
    // Relacionamentos como no exemplo.cs
    public Company company { get; set; } = new Company();
    public User owner { get; set; } = new User();

    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }

    public const string tabela = "courses";

    public Course() { }

    public Course(string name, string description, string url_img, float? workload, int ranking, string field_of_study, Company company, User owner)
    {
        this.name = name;
        this.description = description;
        this.url_img = url_img;
        this.workload = workload;
        this.ranking = ranking;
        this.Field_of_study = field_of_study;
        this.company = company;
        this.owner = owner;
    }

    public void Mostrar()
    {
        string compName = company != null && company.id != 0 ? company.id.ToString() : "N/A";
        string ownerName = owner != null && owner.id != 0 ? owner.id.ToString() : "N/A";
        Console.WriteLine($"| {id,-5} | {name,-20} | {description,-30} | {compName,-10} | {ownerName,-10} |");
    }

    public static void Mostrar(List<Course> courses)
    {
        Console.WriteLine("\n=======================================================================================");
        Console.WriteLine($"| {"ID",-5} | {"NOME",-20} | {"DESCRIÇÃO",-30} | {"COMPANY ID",-10} | {"OWNER ID",-10} |");
        Console.WriteLine("---------------------------------------------------------------------------------------");
        foreach (var c in courses)
        {
            c.Mostrar();
        }
        Console.WriteLine("=======================================================================================\n");
    }

    public async Task InserirAsync()
    {
        string query = $@"
            INSERT INTO {tabela}
            (name, description, url_img, workload, ranking, Field_of_study, company_id, owner_id, createdAt, updatedAt)
            VALUES 
            (@name, @description, @url_img, @workload, @ranking, @field_of_study, @company_id, @owner_id, @createdAt, @updatedAt);
            ";
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("name", name);
        comando.Parameters.AddWithValue("description", description);
        comando.Parameters.AddWithValue("url_img", url_img);
        comando.Parameters.AddWithValue("workload", workload.HasValue ? (object)workload.Value : DBNull.Value);
        comando.Parameters.AddWithValue("ranking", ranking);
        comando.Parameters.AddWithValue("field_of_study", Field_of_study);
        comando.Parameters.AddWithValue("company_id", company.id);
        
        // Se o owner_id for nulo ou 0, salva null
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
            this.description = dados.GetString("description");
            this.url_img = dados.GetString("url_img");
            this.workload = dados.IsDBNull("workload") ? null : dados.GetFloat("workload");
            this.ranking = dados.GetInt32("ranking");
            this.Field_of_study = dados.GetString("Field_of_study");
            
            int company_id = dados.GetInt32("company_id");
            this.company = new Company(company_id);

            if (!dados.IsDBNull("owner_id"))
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

    public static async Task<List<Course>> BuscarTodosAsync()
    {
        string query = $"SELECT * FROM {tabela}";
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);

        await conexao.OpenAsync();
        using var dados = await comando.ExecuteReaderAsync();

        List<Course> courses = new();
        while (await dados.ReadAsync())
        {
            Course c = new();
            c.id = dados.GetInt32("id");
            c.name = dados.GetString("name");
            c.description = dados.GetString("description");
            c.url_img = dados.GetString("url_img");
            c.workload = dados.IsDBNull("workload") ? null : dados.GetFloat("workload");
            c.ranking = dados.GetInt32("ranking");
            c.Field_of_study = dados.GetString("Field_of_study");
            
            int company_id = dados.GetInt32("company_id");
            c.company = new Company(company_id);

            if (!dados.IsDBNull("owner_id"))
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
            courses.Add(c);
        }

        return courses;
    }

    public async Task AlterarAsync()
    {
        string query = $@"
            UPDATE {tabela}
            SET name = @name, description = @description, url_img = @url_img, workload = @workload, 
                ranking = @ranking, Field_of_study = @field_of_study, company_id = @company_id, 
                owner_id = @owner_id, updatedAt = @updatedAt
            WHERE id = @id;
            ";
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("id", id);
        comando.Parameters.AddWithValue("name", name);
        comando.Parameters.AddWithValue("description", description);
        comando.Parameters.AddWithValue("url_img", url_img);
        comando.Parameters.AddWithValue("workload", workload.HasValue ? (object)workload.Value : DBNull.Value);
        comando.Parameters.AddWithValue("ranking", ranking);
        comando.Parameters.AddWithValue("field_of_study", Field_of_study);
        comando.Parameters.AddWithValue("company_id", company.id);
        
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