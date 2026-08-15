using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

public class Category
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }

    public List<Course> courses { get; set; } = new List<Course>();

    public const string tabela = "categories";

    public Category() { }

    public Category(int id, string name, string description, DateTime createdAt, DateTime updatedAt)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
    }

    public Category(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void Mostrar()
    {
        Console.WriteLine($"| {id,-5} | {name,-20} | {description,-40} |");
    }

    public void Mostrar(List<Category> categories)
    {
        Console.WriteLine("\n===========================================================================");
        Console.WriteLine($"| {"ID",-5} | {"NOME",-20} | {"DESCRIÇÃO",-40} |");
        Console.WriteLine("---------------------------------------------------------------------------");
        foreach (var c in categories)
        {
            c.Mostrar();
        }
        Console.WriteLine("===========================================================================\n");
    }

    public async Task InserirAsync()
    {
        string query = @$"
            INSERT INTO {tabela}
            (name, description)
            VALUES 
            (@name, @description);
            ";
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("name", name);
        comando.Parameters.AddWithValue("description", description);

        await conexao.OpenAsync();
        await comando.ExecuteNonQueryAsync();
    }

    public async Task BuscaAsync(int id)
    {
        string query = $"""
           SELECT * FROM {tabela} 
           WHERE id = @id
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
            this.createdAt = dados.GetDateTime("createdAt");
            this.updatedAt = dados.GetDateTime("updatedAt");
        }
    }

    public async Task<List<Category>> BuscarTodosAsync()
    {
        string query = $"""
           SELECT * FROM {tabela}
           """;
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);

        await conexao.OpenAsync();
        using var dados = await comando.ExecuteReaderAsync();

        List<Category> categories = new();
        while (await dados.ReadAsync())
        {
            Category category = new();
            category.id = dados.GetInt32("id");
            category.name = dados.GetString("name");
            category.description = dados.GetString("description");
            category.createdAt = dados.GetDateTime("createdAt");
            category.updatedAt = dados.GetDateTime("updatedAt");

            categories.Add(category);
        }

        return categories;
    }

    public async Task AtualizarAsync()
    {
        string query = @$"
            UPDATE {tabela}
            SET name = @name, description = @description
            WHERE id = @id;
            ";
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("name", name);
        comando.Parameters.AddWithValue("description", description);
        comando.Parameters.AddWithValue("id", id);

        await conexao.OpenAsync();
        await comando.ExecuteNonQueryAsync();
    }

    public async Task RemoverAsync()
    {
        string query = $"""
           DELETE FROM {tabela}
           WHERE id = @id
           """;
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("id", id);

        await conexao.OpenAsync();
        await comando.ExecuteNonQueryAsync();
    }

    public async Task VincularCursoAsync(int courseId)
    {
        string query = $"""
            INSERT INTO category_course (category_id, course_id)
            VALUES (@categoryId, @courseId);
            """;
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("categoryId", this.id);
        comando.Parameters.AddWithValue("courseId", courseId);

        await conexao.OpenAsync();
        await comando.ExecuteNonQueryAsync();
    }

    public async Task DesvincularCursoAsync(int courseId)
    {
        string query = $"""
            DELETE FROM category_course 
            WHERE category_id = @categoryId AND course_id = @courseId;
            """;
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("categoryId", this.id);
        comando.Parameters.AddWithValue("courseId", courseId);

        await conexao.OpenAsync();
        await comando.ExecuteNonQueryAsync();
    }

    public async Task CarregarCursosAsync()
    {
        string query = $"""
            SELECT c.* FROM courses c
            INNER JOIN category_course cc ON c.id = cc.course_id
            WHERE cc.category_id = @categoryId;
            """;
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("categoryId", this.id);

        await conexao.OpenAsync();
        using var dados = await comando.ExecuteReaderAsync();

        this.courses.Clear();
        while (await dados.ReadAsync())
        {
            Course course = new Course();
            course.id = dados.GetInt32("id");
            course.name = dados.GetString("name");
            course.description = dados.GetString("description");
            course.url_img = dados.GetString("url_img");
            this.courses.Add(course);
        }
    }
}
