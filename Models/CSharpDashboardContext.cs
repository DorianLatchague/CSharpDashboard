using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CSharpDashboard.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<User> Users { get;set; }
        public DbSet<Messages> Messages { get;set; }
        public DbSet<Comments> Comments { get;set; }
    }
    public class User
    {
        [Key]
        public int UserId { get; set;}
        [Required]
        [MinLength(2)]
        public string FirstName { get;set; }
        [Required]
        [MinLength(2)]
        public string LastName { get;set; }
        [Required]
        [EmailAddress]
        public string Email { get;set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get;set; }
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PassConf { get;set; }
        public int Level {get;set;}
        public DateTime CreatedAt { get;set; } = DateTime.Now;
        public DateTime UpdatedAt { get;set; } = DateTime.Now;
        [InverseProperty("User")]
        public List<Messages> Messages { get;set; }
        [InverseProperty("Receiver")]
        public List<Messages> MessagesReceived { get;set; }
        public List<Comments> Comments { get;set; }
        [MinLength(5)]
        public string Description { get;set; }
    }
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string Email { get;set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get;set; }
    }
    public class EditUserPassword
    {
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get;set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PassConf { get;set; }
        public DateTime UpdatedAt { get;set; } = DateTime.Now;
    }
    public class EditUser
    {
        [Required]
        [MinLength(2)]
        public string FirstName { get;set; }
        [Required]
        [MinLength(2)]
        public string LastName { get;set; }
        [Required]
        [EmailAddress]
        public string Email { get;set; }
        public int Level {get;set;}
        public DateTime UpdatedAt { get;set; } = DateTime.Now;
    }
    public class EditUserDescription
    {
        [MinLength(5)]
        public string Description { get;set; }
        public DateTime UpdatedAt { get;set; } = DateTime.Now;
    }
    public class Messages
    {
        [Key]
        public int MessageId { get;set; }
        [Required]
        [MinLength(5)]
        public string Message { get;set; }
        public DateTime CreatedAt { get;set; } = DateTime.Now;
        public DateTime UpdatedAt { get;set; } = DateTime.Now;
        public int UserId { get;set; }
        public User User { get;set; }
        public int ReceiverId { get;set; }  
        public User Receiver { get;set; }
        public List<Comments> Comments { get;set; }
    }
    public class Comments
    {
        [Key]
        public int CommentId { get;set; }
        [Required]
        [MinLength(5)]
        public string Comment { get;set; }
        public DateTime CreatedAt { get;set; } = DateTime.Now;
        public DateTime UpdatedAt { get;set; } = DateTime.Now;
        public int UserId { get;set; }
        public User Creator { get;set; }
        public int MessageId { get;set; }
        public Messages Message { get;set; }
    }
}