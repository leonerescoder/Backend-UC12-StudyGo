using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n--- MENU COURSE ---");
            Console.WriteLine("1. Inserir");
            Console.WriteLine("2. Consultar todos");
            Console.WriteLine("3. Consultar específico");
            Console.WriteLine("4. Alterar");
            Console.WriteLine("5. Remover");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            
            var option = Console.ReadLine();

            if (option == "0") break;

            try
            {
                switch (option)
                {
                    case "1":
                        await Inserir();
                        break;
                    case "2":
                        await ConsultarTodos();
                        break;
                    case "3":
                        await ConsultarEspecifico();
                        break;
                    case "4":
                        await Alterar();
                        break;
                    case "5":
                        await Remover();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }

    static async Task Inserir()
    {
        Console.Write("Nome: ");
        string name = Console.ReadLine();
        Console.Write("Descrição: ");
        string desc = Console.ReadLine();
        Console.Write("URL Imagem: ");
        string url = Console.ReadLine();
        Console.Write("Carga Horária (vazio para null): ");
        string wlStr = Console.ReadLine();
        float? workload = string.IsNullOrEmpty(wlStr) ? null : float.Parse(wlStr);
        Console.Write("Ranking (inteiro): ");
        int ranking = int.Parse(Console.ReadLine());
        Console.Write("Área de Estudo (Field_of_study): ");
        string field = Console.ReadLine();
        
        Console.Write("ID Empresa (inteiro): ");
        int companyId = int.Parse(Console.ReadLine());
        Company comp = new Company(companyId);

        Console.Write("ID Dono (vazio para null): ");
        string ownerStr = Console.ReadLine();
        User owner = string.IsNullOrEmpty(ownerStr) ? null : new User(int.Parse(ownerStr));

        Course c = new Course(name, desc, url, workload, ranking, field, comp, owner);
        await c.InserirAsync();
        Console.WriteLine("Curso inserido com sucesso!");
    }

    static async Task ConsultarTodos()
    {
        var cursos = await Course.BuscarTodosAsync();
        Course.Mostrar(cursos);
    }

    static async Task ConsultarEspecifico()
    {
        Console.Write("ID do Curso: ");
        int id = int.Parse(Console.ReadLine());
        Course c = new Course();
        await c.BuscaAsync(id);
        if (c.id != 0)
            c.Mostrar();
        else
            Console.WriteLine("Curso não encontrado.");
    }

    static async Task Alterar()
    {
        Console.Write("ID do Curso a alterar: ");
        int id = int.Parse(Console.ReadLine());
        Course c = new Course();
        await c.BuscaAsync(id);
        if (c.id == 0)
        {
            Console.WriteLine("Curso não encontrado.");
            return;
        }

        Console.WriteLine($"Nome atual: {c.name}");
        Console.Write("Novo Nome (ou enter para manter): ");
        string name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name)) c.name = name;

        Console.WriteLine($"Descrição atual: {c.description}");
        Console.Write("Nova Descrição (ou enter para manter): ");
        string desc = Console.ReadLine();
        if (!string.IsNullOrEmpty(desc)) c.description = desc;

        await c.AlterarAsync();
        Console.WriteLine("Curso alterado com sucesso!");
    }

    static async Task Remover()
    {
        Console.Write("ID do Curso a remover: ");
        int id = int.Parse(Console.ReadLine());
        Course c = new Course();
        c.id = id;
        await c.RemoverAsync();
        Console.WriteLine("Curso removido com sucesso!");
    }
}
