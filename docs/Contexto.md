# Contexto
...

## Integrantes do grupo
- Leonardo
- Gabriel
- Nicolas
- Vanessa

### Divisão de tarefas
Tarefas em C# e classes que cada um está responsável por fazer:

- `User`: Vanessa
- `Campany`: Nicolas
- `Course`: Leonardo
- `Category`: Gabriel
    - `category_course`: não é uma classe, ele está dentro de Category

## CRUD
Operações do CRUD (inserir, ler, atualizar, remover) em cada classe
- `User`: inserir, ler, atualizar, remover
    - Remoção deve ser feito em cascata (remover um usuário, remove todas as informações relacionadas a ele)
- `Campany`: inserir, ler, atualizar, remover
    - Remoção em cascata
- `Course`: inserir, ler, atualizar, remover
- `Category`: inserir, ler, atualizar, remover

## Banco de dados

- **users**: tabela de usuários
	- DIRECTOR: cliente que vai cadastrar sua empresa e seus cursos
	- ADMIN: os donos sistema
	- chave estrangeira com `campanies`
- **companies**: tabela de empresa/escola que vai postar os cursos
	- chave estrangeira com `users`
- **courses**: cursos que uma empresa cadastra
	- tem chave estrangeira com `companies` e `users`
- **categories**: categorias onde um curso se encaixa
	- Não tem chave estrangeira, eles são conectados pela tabela `cagetory_course`
- **category_course**: tabela unicamente de chaves estrangeiras que conecta `categories` e `courses`

### Observações

- **IMPORTANTE**: na tabela `users` o company_id (de `companies`) pode ser nulo, e na tabela `comapnies` o owner_id (de `users`) pode ser nulo. Isso acontece porque ambos são cadastrados juntos e depois atrelado as chaves.

- **user_category**: essa tabela existe no banco, mas **não será usada** neste sistema.

