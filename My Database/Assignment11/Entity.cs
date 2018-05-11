namespace Assignment11
{
    internal class Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmailProduction { get; set; }

        public Entity()
        {
            this.FirstName = "Arthur";
            this.LastName = "Sedrakyan";
            this.EmailProduction = 7895;
        }

        public Entity(string firstName,string lastName, int email)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailProduction = email;
        }
    }
}