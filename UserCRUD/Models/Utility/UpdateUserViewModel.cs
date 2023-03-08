namespace UserCRUD.Models.Utility
{
    public class UpdateUserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
