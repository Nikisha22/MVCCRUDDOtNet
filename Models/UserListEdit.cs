using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeMVCProject.Models
{
    public class UserListEdit
    {
        public int UserId { get; set; }

        [NotMapped]
        public string EncrptedId { get; set; } = null!;
        public string UserName { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public string Email { get; set; } = null!;
    

}
}
