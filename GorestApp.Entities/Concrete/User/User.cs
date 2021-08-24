namespace GorestApp.Entities.Concrete.Users
{
    public class User : BaseEntity, ISoftDeleteEntity, IEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public bool Deleted { get; set; }
    }
}
