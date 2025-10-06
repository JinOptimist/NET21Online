namespace ReflectionExample
{
    public class User
    {
        public User()
        {
            _birthday = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        private DateTime _birthday;

        public bool IsAdult => DateTime.Now.AddYears(-18) > _birthday;
    }
}
