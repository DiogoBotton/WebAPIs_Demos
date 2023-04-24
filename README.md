# WebApi_Produtos_DotNetCore3.1
Criação de API de produtos com fim de estudar as diferenças de implementação com .NET Core 3.1

Edit 2023:
Agora com demo de API com .NET Core 5 utilizando banco de dados PostgreSQL.

**Avisos**
* Antes de tudo, instalar o framework "Microsoft.EntityFrameWorkCore.Relational"
* Para funcionar o método UseSqlServer (StartUp) na versão 3.1 do .NET Core, é preciso instalar o framework "Microsoft.EntityFrameWorkCore.SqlServer"
* Para funcionar o Code First com os comandos "enable-migrations", "add-migration" e "update-database", é preciso instalar o framework "Microsoft.EntityFrameWorkCore.Tools"

**Com PostGreSQL**
* Necessário instalar o framework "Npgsql.EntityFrameworkCore.PostgreSQL"
* Da mesma forma com o SQL Server, para funcionar Code First é necessário instalar o framework "Microsoft.EntityFrameWorkCore.Tools"
* A diferença está na Start Up, ao adicionar a connection string, com os métodos AddEntityFrameworkNpgsql() e UseNpgsql(). Segue código abaixo:

### `services.AddEntityFrameworkNpgsql().AddDbContext<ProductContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));`