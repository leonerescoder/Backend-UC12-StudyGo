using System;
using MySqlConnector;


// ============================================================
// ENUMS DO SISTEMA
// ============================================================


public enum UserType
{
    ADMIN,
    DIRECTOR
}

public enum UserStatus
{
    ATIVO,
    INATIVO
}


// ============================================================
// USUÁRIO
// ============================================================


public class User
{
    public int Id { get; set; }

    public int id
    {
        get => Id;
        set => Id = value;
    }

    public UserType? Type { get; private set; }

    public string Name { get; private set; }

    public string Cpf { get; private set; }

    public string Email { get; private set; }

    public UserStatus? Status { get; private set; }

    public DateTime? BirthDate { get; private set; }

    public string Password { get; private set; }

    public int? CompanyId { get; private set; }


    // ============================================================
    // CONSTRUTOR PARA NOVO USUÁRIO VAZIO
    // ============================================================

    public User() { }

    // ============================================================
    // CONSTRUTOR APENAS COM ID
    // ============================================================

    public User(int id)
    {
        Id = id;
    }

    // ============================================================
    // CONSTRUTOR PARA NOVO USUÁRIO
    // ============================================================

    public User(
        string name,
        string cpf,
        string email,
        string password = "123",
        UserType? type = null,
        UserStatus? status = null,
        DateTime? birthDate = null,
        int? companyId = null)
    {
        ValidarDados(
            name,
            cpf,
            email,
            password,
            birthDate,
            companyId
        );

        Name = name.Trim();

        Cpf = LimparCpf(cpf);

        Email = email.Trim().ToLower();

        Password = password;

        Type = type;

        Status = status;

        BirthDate = birthDate;

        CompanyId = companyId;
    }


    // ============================================================
    // CONSTRUTOR PARA USUÁRIO VINDO DO BANCO
    // ============================================================

    private User(
        int id,
        UserType? type,
        string name,
        string cpf,
        string email,
        UserStatus? status,
        DateTime? birthDate,
        string password,
        int? companyId)
    {
        Id = id;

        Type = type;

        Name = name.Trim();

        Cpf = LimparCpf(cpf);

        Email = email.Trim().ToLower();

        Status = status;

        BirthDate = birthDate;

        Password = password;

        CompanyId = companyId;
    }


    // ============================================================
    // VALIDAÇÃO DOS DADOS
    // ============================================================

    private static void ValidarDados(
        string name,
        string cpf,
        string email,
        string password,
        DateTime? birthDate,
        int? companyId)
    {
        // --------------------------------------------------------
        // NOME
        // --------------------------------------------------------

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception(
                "O nome não pode ser vazio."
            );
        }

        if (name.Trim().Length < 3)
        {
            throw new Exception(
                "O nome deve possuir pelo menos 3 caracteres."
            );
        }

        if (name.Trim().Length > 100)
        {
            throw new Exception(
                "O nome não pode possuir mais de 100 caracteres."
            );
        }


        // --------------------------------------------------------
        // CPF
        // --------------------------------------------------------

        if (!CpfValido(cpf))
        {
            throw new Exception(
                "CPF inválido."
            );
        }


        // --------------------------------------------------------
        // E-MAIL
        // --------------------------------------------------------

        if (!EmailValido(email))
        {
            throw new Exception(
                "E-mail inválido."
            );
        }


        // --------------------------------------------------------
        // SENHA
        // --------------------------------------------------------

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new Exception(
                "A senha não pode ser vazia."
            );
        }


        // --------------------------------------------------------
        // DATA DE NASCIMENTO
        // --------------------------------------------------------

        if (birthDate.HasValue &&
            birthDate.Value.Date > DateTime.Today)
        {
            throw new Exception(
                "A data de nascimento não pode estar no futuro."
            );
        }


        // --------------------------------------------------------
        // COMPANY ID
        // --------------------------------------------------------

        if (companyId.HasValue &&
            companyId.Value <= 0)
        {
            throw new Exception(
                "Company ID inválido."
            );
        }
    }


    // ============================================================
    // MOSTRAR USUÁRIO
    // ============================================================

    public void Mostrar()
    {
        Console.WriteLine(
            $"ID: {Id}"
        );

        Console.WriteLine(
            $"Nome: {Name}"
        );

        Console.WriteLine(
            $"CPF: {Cpf}"
        );

        Console.WriteLine(
            $"E-mail: {Email}"
        );

        Console.WriteLine(
            $"Tipo: {(Type.HasValue ? Type.ToString() : "Não informado")}"
        );

        Console.WriteLine(
            $"Status: {(Status.HasValue ? Status.ToString() : "Não informado")}"
        );


        if (BirthDate.HasValue)
        {
            Console.WriteLine(
                $"Data de nascimento: {BirthDate.Value:dd/MM/yyyy}"
            );
        }
        else
        {
            Console.WriteLine(
                "Data de nascimento: Não informado"
            );
        }


        if (CompanyId.HasValue)
        {
            Console.WriteLine(
                $"Company ID: {CompanyId.Value}"
            );
        }
        else
        {
            Console.WriteLine(
                "Company ID: Não informado"
            );
        }
    }


    // ============================================================
    // SALVAR NOVO USUÁRIO
    // ============================================================

    public void Salvar()
    {
        using MySqlConnection conexao =
            new MySqlConnection(
                ConfiguracaoBD.connectionString
            );

        conexao.Open();


        // Verifica se CPF já existe
        string sqlCpf = @"
            SELECT COUNT(*)
            FROM users
            WHERE cpf = @cpf;
        ";

        using MySqlCommand comandoCpf =
            new MySqlCommand(
                sqlCpf,
                conexao
            );

        comandoCpf.Parameters.AddWithValue(
            "@cpf",
            Cpf
        );

        int quantidadeCpf =
            Convert.ToInt32(
                comandoCpf.ExecuteScalar()
            );


        if (quantidadeCpf > 0)
        {
            throw new Exception(
                "Este CPF já está cadastrado."
            );
        }


        // Verifica se e-mail já existe
        string sqlEmail = @"
            SELECT COUNT(*)
            FROM users
            WHERE email = @email;
        ";

        using MySqlCommand comandoEmail =
            new MySqlCommand(
                sqlEmail,
                conexao
            );

        comandoEmail.Parameters.AddWithValue(
            "@email",
            Email
        );

        int quantidadeEmail =
            Convert.ToInt32(
                comandoEmail.ExecuteScalar()
            );


        if (quantidadeEmail > 0)
        {
            throw new Exception(
                "Este e-mail já está cadastrado."
            );
        }


        // --------------------------------------------------------
        // INSERT
        // --------------------------------------------------------

        string sql = @"
            INSERT INTO users
            (
                type,
                name,
                cpf,
                email,
                status,
                birth_date,
                password,
                company_id
            )
            VALUES
            (
                @type,
                @name,
                @cpf,
                @email,
                @status,
                @birthDate,
                @password,
                @companyId
            );
        ";


        using MySqlCommand comando =
            new MySqlCommand(
                sql,
                conexao
            );


        comando.Parameters.AddWithValue(
            "@type",
            Type.HasValue
                ? Type.Value.ToString()
                : DBNull.Value
        );


        comando.Parameters.AddWithValue(
            "@name",
            Name
        );


        comando.Parameters.AddWithValue(
            "@cpf",
            Cpf
        );


        comando.Parameters.AddWithValue(
            "@email",
            Email
        );


        comando.Parameters.AddWithValue(
            "@status",
            Status.HasValue
                ? Status.Value.ToString()
                : DBNull.Value
        );


        comando.Parameters.AddWithValue(
            "@birthDate",
            BirthDate.HasValue
                ? BirthDate.Value
                : DBNull.Value
        );


        comando.Parameters.AddWithValue(
            "@password",
            Password
        );


        comando.Parameters.AddWithValue(
            "@companyId",
            CompanyId.HasValue
                ? CompanyId.Value
                : DBNull.Value
        );


        comando.ExecuteNonQuery();


        Id = Convert.ToInt32(
            comando.LastInsertedId
        );
    }


    // ============================================================
    // ATUALIZAR
    // ============================================================

    public void Atualizar(
        string? novoNome = null,
        string? novoEmail = null,
        string? novaSenha = null,
        UserType? novoTipo = null,
        UserStatus? novoStatus = null,
        DateTime? novaDataNascimento = null,
        int? novoCompanyId = null)
    {
        // --------------------------------------------------------
        // NOME
        // --------------------------------------------------------

        if (!string.IsNullOrWhiteSpace(novoNome))
        {
            if (novoNome.Trim().Length < 3)
            {
                throw new Exception(
                    "O nome deve possuir pelo menos 3 caracteres."
                );
            }

            if (novoNome.Trim().Length > 100)
            {
                throw new Exception(
                    "O nome não pode possuir mais de 100 caracteres."
                );
            }

            Name = novoNome.Trim();
        }


        // --------------------------------------------------------
        // E-MAIL
        // --------------------------------------------------------

        if (!string.IsNullOrWhiteSpace(novoEmail))
        {
            if (!EmailValido(novoEmail))
            {
                throw new Exception(
                    "E-mail inválido."
                );
            }

            Email = novoEmail.Trim().ToLower();
        }


        // --------------------------------------------------------
        // SENHA
        // --------------------------------------------------------

        if (!string.IsNullOrWhiteSpace(novaSenha))
        {
            Password = novaSenha;
        }


        // --------------------------------------------------------
        // TIPO
        // --------------------------------------------------------

        if (novoTipo.HasValue)
        {
            Type = novoTipo;
        }


        // --------------------------------------------------------
        // STATUS
        // --------------------------------------------------------

        if (novoStatus.HasValue)
        {
            Status = novoStatus;
        }


        // --------------------------------------------------------
        // DATA DE NASCIMENTO
        // --------------------------------------------------------

        if (novaDataNascimento.HasValue)
        {
            if (novaDataNascimento.Value.Date > DateTime.Today)
            {
                throw new Exception(
                    "A data de nascimento não pode estar no futuro."
                );
            }

            BirthDate = novaDataNascimento;
        }


        // --------------------------------------------------------
        // COMPANY ID
        // --------------------------------------------------------

        if (novoCompanyId.HasValue)
        {
            if (novoCompanyId.Value <= 0)
            {
                throw new Exception(
                    "Company ID inválido."
                );
            }

            CompanyId = novoCompanyId;
        }


        SalvarAtualizacao();
    }


    // ============================================================
    // SALVAR ATUALIZAÇÃO
    // ============================================================

    private void SalvarAtualizacao()
    {
        using MySqlConnection conexao =
            new MySqlConnection(
                ConfiguracaoBD.connectionString
            );

        conexao.Open();


        string sql = @"
            UPDATE users SET
                type = @type,
                name = @name,
                cpf = @cpf,
                email = @email,
                status = @status,
                birth_date = @birthDate,
                password = @password,
                company_id = @companyId
            WHERE id = @id;
        ";


        using MySqlCommand comando =
            new MySqlCommand(
                sql,
                conexao
            );


        comando.Parameters.AddWithValue(
            "@type",
            Type.HasValue
                ? Type.Value.ToString()
                : DBNull.Value
        );


        comando.Parameters.AddWithValue(
            "@name",
            Name
        );


        comando.Parameters.AddWithValue(
            "@cpf",
            Cpf
        );


        comando.Parameters.AddWithValue(
            "@email",
            Email
        );


        comando.Parameters.AddWithValue(
            "@status",
            Status.HasValue
                ? Status.Value.ToString()
                : DBNull.Value
        );


        comando.Parameters.AddWithValue(
            "@birthDate",
            BirthDate.HasValue
                ? BirthDate.Value
                : DBNull.Value
        );


        comando.Parameters.AddWithValue(
            "@password",
            Password
        );


        comando.Parameters.AddWithValue(
            "@companyId",
            CompanyId.HasValue
                ? CompanyId.Value
                : DBNull.Value
        );


        comando.Parameters.AddWithValue(
            "@id",
            Id
        );


        int linhasAfetadas =
            comando.ExecuteNonQuery();


        if (linhasAfetadas == 0)
        {
            throw new Exception(
                "Nenhuma alteração foi realizada."
            );
        }
    }


    // ============================================================
    // ALTERAR SENHA
    // ============================================================

    public void AlterarSenha(
        string novaSenha)
    {
        if (string.IsNullOrWhiteSpace(novaSenha))
        {
            throw new Exception(
                "A senha não pode ser vazia."
            );
        }

        Password = novaSenha;

        SalvarAtualizacao();
    }


    // ============================================================
    // EDITAR
    // ============================================================

    public void Editar(
        string name,
        string email,
        DateTime? birthDate)
    {
        Atualizar(
            name,
            email,
            null,
            null,
            null,
            birthDate,
            null
        );
    }


    // ============================================================
    // ALTERAR TIPO
    // ============================================================

    public void AlterarTipo(
        UserType tipo)
    {
        Type = tipo;

        SalvarAtualizacao();
    }


    // ============================================================
    // ALTERAR STATUS
    // ============================================================

    public void AlterarStatus(
        UserStatus status)
    {
        Status = status;

        SalvarAtualizacao();
    }


    // ============================================================
    // DELETAR
    // ============================================================

    public void Deletar()
    {
        using MySqlConnection conexao =
            new MySqlConnection(
                ConfiguracaoBD.connectionString
            );

        conexao.Open();


        string sql = @"
            DELETE FROM users
            WHERE id = @id;
        ";


        using MySqlCommand comando =
            new MySqlCommand(
                sql,
                conexao
            );


        comando.Parameters.AddWithValue(
            "@id",
            Id
        );


        int linhasAfetadas =
            comando.ExecuteNonQuery();


        if (linhasAfetadas == 0)
        {
            throw new Exception(
                "Usuário não encontrado ou já foi deletado."
            );
        }
    }


    // ============================================================
    // BUSCAR POR ID
    // ============================================================

    public static User? BuscarPorId(
        int id)
    {
        using MySqlConnection conexao =
            new MySqlConnection(
                ConfiguracaoBD.connectionString
            );

        conexao.Open();


        string sql = @"
            SELECT
                id,
                type,
                name,
                cpf,
                email,
                status,
                birth_date,
                password,
                company_id
            FROM users
            WHERE id = @id;
        ";


        using MySqlCommand comando =
            new MySqlCommand(
                sql,
                conexao
            );


        comando.Parameters.AddWithValue(
            "@id",
            id
        );


        using MySqlDataReader leitor =
            comando.ExecuteReader();


        if (leitor.Read())
        {
            return CriarUsuarioDoBanco(
                leitor
            );
        }


        return null;
    }


    // ============================================================
    // LISTAR TODOS
    // ============================================================

    public static List<User> ListarTodos()
    {
        List<User> usuarios =
            new List<User>();


        using MySqlConnection conexao =
            new MySqlConnection(
                ConfiguracaoBD.connectionString
            );

        conexao.Open();


        string sql = @"
            SELECT
                id,
                type,
                name,
                cpf,
                email,
                status,
                birth_date,
                password,
                company_id
            FROM users
            ORDER BY name;
        ";


        using MySqlCommand comando =
            new MySqlCommand(
                sql,
                conexao
            );


        using MySqlDataReader leitor =
            comando.ExecuteReader();


        while (leitor.Read())
        {
            usuarios.Add(
                CriarUsuarioDoBanco(
                    leitor
                )
            );
        }


        return usuarios;
    }


    // ============================================================
    // BUSCAR POR E-MAIL
    // ============================================================

    public static User? BuscarPorEmail(
        string email)
    {
        if (!EmailValido(email))
        {
            return null;
        }


        using MySqlConnection conexao =
            new MySqlConnection(
                ConfiguracaoBD.connectionString
            );

        conexao.Open();


        string sql = @"
            SELECT
                id,
                type,
                name,
                cpf,
                email,
                status,
                birth_date,
                password,
                company_id
            FROM users
            WHERE email = @email;
        ";


        using MySqlCommand comando =
            new MySqlCommand(
                sql,
                conexao
            );


        comando.Parameters.AddWithValue(
            "@email",
            email.Trim().ToLower()
        );


        using MySqlDataReader leitor =
            comando.ExecuteReader();


        if (leitor.Read())
        {
            return CriarUsuarioDoBanco(
                leitor
            );
        }


        return null;
    }


    // ============================================================
    // LOGIN
    // ============================================================

    public static User? Login(
        string email,
        string password)
    {
        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            return null;
        }


        User? usuario =
            BuscarPorEmail(email);


        if (usuario == null)
        {
            return null;
        }


        if (usuario.Password != password)
        {
            return null;
        }


        if (usuario.Status != UserStatus.ATIVO)
        {
            return null;
        }


        return usuario;
    }


    // ============================================================
    // RECONSTRUIR USUÁRIO DO BANCO
    // ============================================================

    private static User CriarUsuarioDoBanco(
        MySqlDataReader leitor)
    {
        UserType? type = null;


        if (!leitor.IsDBNull(1))
        {
            string valorTipo =
                leitor.GetString(1);

            if (Enum.TryParse(
                    valorTipo,
                    true,
                    out UserType tipoConvertido))
            {
                type = tipoConvertido;
            }
        }


        UserStatus? status = null;


        if (!leitor.IsDBNull(5))
        {
            string valorStatus =
                leitor.GetString(5);

            if (Enum.TryParse(
                    valorStatus,
                    true,
                    out UserStatus statusConvertido))
            {
                status = statusConvertido;
            }
        }


        DateTime? birthDate =
            leitor.IsDBNull(6)
                ? null
                : leitor.GetDateTime(6);


        int? companyId =
            leitor.IsDBNull(8)
                ? null
                : leitor.GetInt32(8);


        return new User(
            leitor.GetInt32(0),
            type,
            leitor.GetString(2),
            leitor.GetString(3),
            leitor.GetString(4),
            status,
            birthDate,
            leitor.GetString(7),
            companyId
        );
    }


    // ============================================================
    // LIMPAR CPF
    // ============================================================

    private static string LimparCpf(
        string cpf)
    {
        return cpf
            .Replace(".", "")
            .Replace("-", "")
            .Replace(" ", "")
            .Trim();
    }


    // ============================================================
    // CPF VÁLIDO
    // ============================================================

    public static bool CpfValido(
        string cpf)
    {
        cpf = LimparCpf(cpf);


        if (cpf.Length != 11)
        {
            return false;
        }


        if (!long.TryParse(
                cpf,
                out _))
        {
            return false;
        }


        // --------------------------------------------------------
        // CPFs com todos os números iguais são inválidos
        // --------------------------------------------------------

        bool todosIguais = true;


        for (int i = 1; i < cpf.Length; i++)
        {
            if (cpf[i] != cpf[0])
            {
                todosIguais = false;
                break;
            }
        }


        if (todosIguais)
        {
            return false;
        }


        int[] numeros =
            new int[11];


        for (int i = 0; i < 11; i++)
        {
            numeros[i] =
                cpf[i] - '0';
        }


        // --------------------------------------------------------
        // PRIMEIRO DÍGITO
        // --------------------------------------------------------

        int soma = 0;


        for (int i = 0; i < 9; i++)
        {
            soma +=
                numeros[i] *
                (10 - i);
        }


        int resto =
            soma % 11;


        int primeiroDigito =
            resto < 2
                ? 0
                : 11 - resto;


        if (numeros[9] != primeiroDigito)
        {
            return false;
        }


        // --------------------------------------------------------
        // SEGUNDO DÍGITO
        // --------------------------------------------------------

        soma = 0;


        for (int i = 0; i < 10; i++)
        {
            soma +=
                numeros[i] *
                (11 - i);
        }


        resto =
            soma % 11;


        int segundoDigito =
            resto < 2
                ? 0
                : 11 - resto;


        if (numeros[10] != segundoDigito)
        {
            return false;
        }


        return true;
    }


    // ============================================================
    // E-MAIL VÁLIDO
    // ============================================================

    public static bool EmailValido(
        string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }


        email =
            email.Trim();


        if (!email.Contains("@"))
        {
            return false;
        }


        if (!email.Contains("."))
        {
            return false;
        }


        if (email.StartsWith("@"))
        {
            return false;
        }


        if (email.EndsWith("@"))
        {
            return false;
        }


        if (email.Contains(" "))
        {
            return false;
        }


        return true;
    }
}