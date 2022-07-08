namespace DAL.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public bool BasicRole { get; set; } = false;
        public ICollection<Credentials> Credentials { get; set; }
    }
}
