using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;
[Table("contacts")]

public class ContactEntity
{
    [HiddenInput] public int Id { get; set; }

    [Required]
    [MaxLength(length:20)]
    [MinLength(length:2)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(length:50)]
    [MinLength(length:2)]

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }
    [Column("birth")]

    public DateOnly BirthDate { get; set; }
    
    public DateTime Created { get; set; }
    
    
}