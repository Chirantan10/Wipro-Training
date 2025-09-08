using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SecureNotesAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string? DisplayName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}