using Microsoft.EntityFrameworkCore;
using ProjetsORM.Persistence;
using Xunit;

namespace ProjetsORM.AccesDonnees
{
    public class EFEmployeRepositoryTest
    {
        private EFEmployeRepository repoEmploye;
    
        private void CreerContexteEtReposDeTests()
        {
            var builder = new DbContextOptionsBuilder<ProjetsORMContexte>();
            builder.UseInMemoryDatabase(databaseName: "employe_db");   // Database en mémoire
            var contexte = new ProjetsORMContexte(builder.Options);
            repoEmploye = new EFEmployeRepository(contexte);
        }
        

    }
}
