# WebAPI's_Demos
Criação de API's com o fim de estudar as diferenças de implementação com .NET Core 3.1 e 5, assim como, integração com banco de dados Microsoft SQL Server, PostgreSQL e MongoDB

****Com Microsoft SQL Server****
* Antes de tudo, instalar o framework "Microsoft.EntityFrameWorkCore.Relational"
* Para funcionar o método UseSqlServer (StartUp) na versão 3.1 do .NET Core, é preciso instalar o framework "Microsoft.EntityFrameWorkCore.SqlServer"
* Para funcionar o Code First com os comandos "enable-migrations", "add-migration" e "update-database", é preciso instalar o framework "Microsoft.EntityFrameWorkCore.Tools"

**Com PostGreSQL**
* Necessário instalar o framework "Npgsql.EntityFrameworkCore.PostgreSQL"
* Da mesma forma com o SQL Server, para funcionar Code First é necessário instalar o framework "Microsoft.EntityFrameWorkCore.Tools"
* A diferença está na Start Up, ao adicionar a connection string, com os métodos AddEntityFrameworkNpgsql() e UseNpgsql(). Segue código abaixo:

### `services.AddEntityFrameworkNpgsql().AddDbContext<ProductContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));`

* Exemplo código Db Scaffold utlizando Postgres (Database First)
### `Scaffold-DbContext "Host=localhost;Port=5432;Database=splay;Username=postgres;Password=Elefante@132;" Npgsql.EntityFrameworkCore.PostgreSQL -o Models`
* Entre aspas, logo após o scaffold, a connection string para o banco, em seguida, o pacote nuget do banco de dados e o nome da pasta que será criada as entidades e context

**Autenticação JWT Assimétrica (Projeto POC_Auth)**

* Realizado com apoio abaixo:
https://dev.to/eduardstefanescu/jwt-authentication-with-asymmetric-encryption-using-certificates-in-asp-net-core-2o7e

https://github.com/StefanescuEduard/JwtAuthentication