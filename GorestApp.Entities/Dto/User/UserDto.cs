namespace GorestApp.Entities.Dto.Users
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public bool Deleted { get; set; }

        public class WithId : UserDto
        {
            public int Id { get; set; }
        }

        public class InsertOrUpdate : UserDto
        {
            public int Id { get; set; }
        }
    }
}
