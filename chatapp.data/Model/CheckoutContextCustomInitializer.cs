using System.Data.Entity;

namespace chatapp.data.Model
{
    public class CheckoutContextCustomInitializer : IDatabaseInitializer<DataDbContext>
    {
        public void InitializeDatabase(DataDbContext context)
        {
            if (context.Database.Exists())
            {
                if (!context.Database.CompatibleWithModel(true))
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }
            else
            {
                context.Database.Create();
            }
            context.SaveChanges();
        }
    }
}
