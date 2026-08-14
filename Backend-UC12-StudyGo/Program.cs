using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n--- MENU PRINCIPAL ---");
            Console.WriteLine("1. Gerenciar Cursos");
            Console.WriteLine("2. Gerenciar Categorias");
            Console.WriteLine("3. Gerenciar Empresas");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            
            var option = Console.ReadLine();

            if (option == "0") break;

            switch (option)
            {
                case "1":
                    await MenuCourse();
                    break;
                case "2":
                    await MenuCategory();
                    break;
                case "3":
                    await MenuCompany();
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static async Task MenuCourse()
    {
        while (true)
        {
            Console.WriteLine("\n--- MENU COURSE ---");
            Console.WriteLine("1. Inserir");
            Console.WriteLine("2. Consultar todos");
            Console.WriteLine("3. Consultar específico");
            Console.WriteLine("4. Alterar");
            Console.WriteLine("5. Remover");
            Console.WriteLine("0. Voltar");
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

    static async Task MenuCategory()
    {
        while (true)
        {
            Console.WriteLine("\n--- MENU CATEGORY ---");
            Console.WriteLine("1. Inserir");
            Console.WriteLine("2. Consultar todos");
            Console.WriteLine("3. Consultar específico");
            Console.WriteLine("4. Alterar");
            Console.WriteLine("5. Remover");
            Console.WriteLine("0. Voltar");
            Console.Write("Escolha uma opção: ");
            
            var option = Console.ReadLine();

            if (option == "0") break;

            try
            {
                switch (option)
                {
                    case "1":
                        await InserirCategoria();
                        break;
                    case "2":
                        await ConsultarTodasCategorias();
                        break;
                    case "3":
                        await ConsultarCategoriaEspecifica();
                        break;
                    case "4":
                        await AlterarCategoria();
                        break;
                    case "5":
                        await RemoverCategoria();
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

    // --- Métodos Course ---
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

    // --- Métodos Category ---
    static async Task InserirCategoria()
    {
        Console.Write("Nome: ");
        string name = Console.ReadLine();
        Console.Write("Descrição: ");
        string desc = Console.ReadLine();
        
        Category c = new Category(name, desc);
        await c.InserirAsync();
        Console.WriteLine("Categoria inserida com sucesso!");
    }

    static async Task ConsultarTodasCategorias()
    {
        Category cAux = new Category();
        var categorias = await cAux.BuscarTodosAsync();
        cAux.Mostrar(categorias);
    }

    static async Task ConsultarCategoriaEspecifica()
    {
        Console.Write("ID da Categoria: ");
        int id = int.Parse(Console.ReadLine());
        Category c = new Category();
        await c.BuscaAsync(id);
        if (c.id != 0)
            c.Mostrar();
        else
            Console.WriteLine("Categoria não encontrada.");
    }

    static async Task AlterarCategoria()
    {
        Console.Write("ID da Categoria a alterar: ");
        int id = int.Parse(Console.ReadLine());
        Category c = new Category();
        await c.BuscaAsync(id);
        if (c.id == 0)
        {
            Console.WriteLine("Categoria não encontrada.");
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

        await c.AtualizarAsync();
        Console.WriteLine("Categoria alterada com sucesso!");
    }

    static async Task RemoverCategoria()
    {
        Console.Write("ID da Categoria a remover: ");
        int id = int.Parse(Console.ReadLine());
        Category c = new Category();
        c.id = id;
        await c.RemoverAsync();
        Console.WriteLine("Categoria removida com sucesso!");
    }

    // --- Métodos Company ---
    static async Task MenuCompany()
    {
        while (true)
        {
            Console.WriteLine("\n--- MENU COMPANY ---");
            Console.WriteLine("1. Inserir");
            Console.WriteLine("2. Consultar todas");
            Console.WriteLine("3. Consultar específica");
            Console.WriteLine("4. Alterar");
            Console.WriteLine("5. Remover");
            Console.WriteLine("0. Voltar");
            Console.Write("Escolha uma opção: ");
            
            var option = Console.ReadLine();

            if (option == "0") break;

            try
            {
                switch (option)
                {
                    case "1":
                        await InserirCompany();
                        break;
                    case "2":
                        await ConsultarTodasCompanies();
                        break;
                    case "3":
                        await ConsultarCompanyEspecifica();
                        break;
                    case "4":
                        await AlterarCompany();
                        break;
                    case "5":
                        await RemoverCompany();
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

    static async Task InserirCompany()
    {
        Console.Write("Nome: ");
        string name = Console.ReadLine();
        Console.Write("CNPJ: ");
        string cnpj = Console.ReadLine();
        Console.Write("Data de Fundação (yyyy-mm-dd): ");
        DateTime foundation = DateTime.Parse(Console.ReadLine());
        Console.Write("Lugares (Places): ");
        string places = Console.ReadLine();
        Console.Write("Fundamentos: ");
        string fundaments = Console.ReadLine();
        Console.Write("Métodos: ");
        string methods = Console.ReadLine();
        Console.Write("Ranking (inteiro): ");
        int ranking = int.Parse(Console.ReadLine());

        Console.Write("ID Dono (vazio para null): ");
        string ownerStr = Console.ReadLine();
        User owner = string.IsNullOrEmpty(ownerStr) ? null : new User(int.Parse(ownerStr));

        Company c = new Company(name, cnpj, foundation, places, fundaments, methods, ranking, owner);
        await c.InserirAsync();
        Console.WriteLine("Empresa inserida com sucesso!");
    }

    static async Task ConsultarTodasCompanies()
    {
        var companies = await Company.BuscarTodosAsync();
        Company.Mostrar(companies);
    }

    static async Task ConsultarCompanyEspecifica()
    {
        Console.Write("ID da Empresa: ");
        int id = int.Parse(Console.ReadLine());
        Company c = new Company();
        await c.BuscaAsync(id);
        if (c.id != 0)
            c.Mostrar();
        else
            Console.WriteLine("Empresa não encontrada.");
    }

    static async Task AlterarCompany()
    {
        Console.Write("ID da Empresa a alterar: ");
        int id = int.Parse(Console.ReadLine());
        Company c = new Company();
        await c.BuscaAsync(id);
        if (c.id == 0)
        {
            Console.WriteLine("Empresa não encontrada.");
            return;
        }

        Console.WriteLine($"Nome atual: {c.name}");
        Console.Write("Novo Nome (ou enter para manter): ");
        string name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name)) c.name = name;

        Console.WriteLine($"CNPJ atual: {c.cnpj}");
        Console.Write("Novo CNPJ (ou enter para manter): ");
        string cnpj = Console.ReadLine();
        if (!string.IsNullOrEmpty(cnpj)) c.cnpj = cnpj;

        await c.AlterarAsync();
        Console.WriteLine("Empresa alterada com sucesso!");
    }

    static async Task RemoverCompany()
    {
        Console.Write("ID da Empresa a remover: ");
        int id = int.Parse(Console.ReadLine());
        Company c = new Company();
        c.id = id;
        await c.RemoverAsync();
        Console.WriteLine("Empresa removida com sucesso!");
    }
}
