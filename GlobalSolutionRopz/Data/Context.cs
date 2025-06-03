using GlobalSolutionRopz.Model;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolutionRopz.Data
{
   
       public class Context : DbContext
        {
            public Context(DbContextOptions<Context> options) : base(options)


            {
                //          Database.EnsureCreated();
            }
            public DbSet<Usuario> Api_Global_Dotnet_Usuario { set; get; }
            public DbSet<Mensagem> Api_Global_Dotnet_Mensagem { set; get; }
         
            public DbSet<Alerta> Api_Global_Dotnet_Alerta { set; get; }



            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // Se não estiver configurado no projeto IU pega deginição de chame do json configurado
                if (!optionsBuilder.IsConfigured)
                    //optionsBuilder.UseSqlServer(GetStringConectionConfig());
                    optionsBuilder.UseOracle(GetStringConectionConfig());

                base.OnConfiguring(optionsBuilder);
            }


            private string GetStringConectionConfig()
            {
                //string strCon = "Data Source=ORACLE.FIAP.COM.BR:1521/ORCL;Initial Catalog=fiap;Integrated Security=False;User ID=;Password=;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
                string strCon = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))) (CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=;Password=;";
                return strCon;
            }



        }
    }

