namespace Royal_London_Code_Test.Models
{
    public class User : IUser
    {
        #region Fields

        #endregion

        #region Constructors

        public User()
        {

        }
        public User(int id, string title, string firstName, string surName, IProduct product)
        {

            Id = id;
            Title = title;
            FirstName = firstName;
            SurName = surName;
            Product = product;
        }
        #endregion

        #region Properties
        public int Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public IProduct Product { get; set; }

        #endregion

    }

}