using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

public class Vendas
{
    public int id { get; set; }
    public Produtos produtos { get; set; } = new Produtos();
    public Usuario usuario { get; set; } = new Usuario();
    public int quantidade { get; set; }
    public DateTime criado_em { get; set; }

    public const string tabela = "vendas";

    public Vendas() { }

    public Vendas(int id, Produtos produtos, Usuario usuario, int quantidade, DateTime criado_em)
    {
        this.id = id;
        this.produtos = produtos;
        this.usuario = usuario;
        this.quantidade = quantidade;
        this.criado_em = criado_em;
    }

    public Vendas(Produtos produtos, Usuario usuario, int quantidade, DateTime criado_em)
    {
        this.produtos = produtos;
        this.usuario = usuario;
        this.quantidade = quantidade;
        this.criado_em = criado_em;
    }

    public void Mostrar()
    {
        float total = (produtos != null ? produtos.preco * quantidade : 0);
        string nomeProd = produtos != null ? produtos.nome : "Desconhecido";
        float precoProd = produtos != null ? produtos.preco : 0f;
        string nomeUsr = usuario != null ? usuario.nome : "Desconhecido";
        Console.WriteLine($"| {id,-5} | {nomeProd,-20} | {nomeUsr,-18} | {quantidade,8} | {precoProd,12:C2} | {total,12:C2} |");
    }

    public void Mostrar(List<Vendas> vendas)
    {
        Console.WriteLine("\n========================================================================================================");
        Console.WriteLine($"| {"ID",-5} | {"PRODUTO",-20} | {"COMPRADOR",-18} | {"QTD",-8} | {"V. UNIT.",-12} | {"TOTAL",-12} |");
        Console.WriteLine("--------------------------------------------------------------------------------------------------------");
        float somaTotal = 0;
        foreach (var v in vendas)
        {
            v.Mostrar();
            if (v.produtos != null)
            {
                somaTotal += v.produtos.preco * v.quantidade;
            }
        }
        Console.WriteLine("--------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"| {"Faturamento Acumulado:",-82} | {somaTotal,12:C2} |");
        Console.WriteLine("========================================================================================================\n");
    }

    public async Task InserirAsync()
    {
        string query = @$"
            INSERT INTO {tabela}
            (id_produto, id_usuario, quantidade)
            VALUES 
            (@id_produto, @id_usuario, @quantidade);
            ";
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);
        comando.Parameters.AddWithValue("id_produto", produtos.id);
        comando.Parameters.AddWithValue("id_usuario", usuario.id);
        comando.Parameters.AddWithValue("quantidade", quantidade);

        await conexao.OpenAsync();
        await comando.ExecuteNonQueryAsync();
    }

    public async Task BuscaAsync(int id)
    {
        string query = $"""
           SELECT {tabela}.*, produtos.nome AS prod_nome, produtos.preco AS prod_preco, usuarios.nome AS usr_nome, usuarios.email AS usr_email
           FROM {tabela} 
           INNER JOIN produtos ON {tabela}.id_produto = produtos.id
           INNER JOIN usuarios ON {tabela}.id_usuario = usuarios.id
           WHERE {tabela}.id = {id}
           """;
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);

        await conexao.OpenAsync();
        var dados = await comando.ExecuteReaderAsync();

        while (await dados.ReadAsync())
        {
            this.id = dados.GetInt32("id");
            this.quantidade = dados.GetInt32("quantidade");
            this.criado_em = dados.GetDateTime("criado_em");

            int id_produto = dados.GetInt32("id_produto");
            string prodNome = dados.GetString("prod_nome");
            float prodPreco = dados.GetFloat("prod_preco");
            this.produtos = new Produtos(id_produto, prodNome, prodPreco);

            int id_usuario = dados.GetInt32("id_usuario");
            string usrNome = dados.GetString("usr_nome");
            string usrEmail = dados.GetString("usr_email");
            this.usuario = new Usuario(id_usuario, usrNome, usrEmail);
        }
    }

    public async Task<List<Vendas>> BuscarTodosAsync()
    {
        string query = $"""
           SELECT vendas.*, produtos.nome AS prod_nome, produtos.preco AS prod_preco, usuarios.nome AS usr_nome, usuarios.email AS usr_email
           FROM vendas 
           INNER JOIN produtos ON vendas.id_produto = produtos.id
           INNER JOIN usuarios ON vendas.id_usuario = usuarios.id
           """;
        using var conexao = new MySqlConnection(ConfiguracaoBD.connectionString);
        using var comando = new MySqlCommand(query, conexao);

        await conexao.OpenAsync();
        var dados = await comando.ExecuteReaderAsync();

        List<Vendas> vendas = new();
        while (await dados.ReadAsync())
        {
            Vendas venda = new();
            venda.id = dados.GetInt32("id");
            venda.quantidade = dados.GetInt32("quantidade");
            venda.criado_em = dados.GetDateTime("criado_em");

            int id_produto = dados.GetInt32("id_produto");
            string prodNome = dados.GetString("prod_nome");
            float prodPreco = dados.GetFloat("prod_preco");
            venda.produtos = new Produtos(id_produto, prodNome, prodPreco);

            int id_usuario = dados.GetInt32("id_usuario");
            string usrNome = dados.GetString("usr_nome");
            string usrEmail = dados.GetString("usr_email");
            venda.usuario = new Usuario(id_usuario, usrNome, usrEmail);

            vendas.Add(venda);
        }

        return vendas;
    }
}