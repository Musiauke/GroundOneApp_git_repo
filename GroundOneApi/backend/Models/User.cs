using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class User
    {
        // public int Id { get; set; }
        // public string Username { get; set; }
        // public string Email { get; set; }
        // public string PasswordHash { get; set; } // hash password
        // public string Role { get; set; } // e.g. "Admin", "User"

        // // Relacje
        // public List<Vehicle> Vehicles { get; set; } // users vehicles
    }
}

// implenting user class 
// requires: 
//

// JWT/Cookies for authentication 
// hashing passwords (BCrypt/PBKDF2)
// Middleware for checking permissions
// Registration/login endpoints