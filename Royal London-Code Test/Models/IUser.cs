namespace Royal_London_Code_Test.Models
{
    public interface IUser
    {
        int Id { get; }

        string Title { get; }

        string FirstName { get; }

        string SurName { get; }

        IProduct Product { get; }
    }
}