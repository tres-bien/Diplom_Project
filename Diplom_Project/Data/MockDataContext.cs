namespace Diplom_Project
{
    public class MockDataContext : DataContext
    {
        public MockDataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
