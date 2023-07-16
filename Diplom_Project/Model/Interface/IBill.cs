namespace Diplom_Project
{
    public interface IBill 
    {
        string Name { get; set; }
        double Total { get; set; }
        public List<Member> Members { get; set; }
    }
}
