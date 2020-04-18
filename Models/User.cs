using System.ComponentModel;

namespace Notia.Models
{
    public class User
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Email {get;set;}
        public byte[] PasswordHash {get;set;}
        public byte[] PasswordSalt {get;set;}
        public bool? NewUser {get;set;}
        public string Birthday {get;set;}
        public string CollegeName {get;set;}
        public string Bio {get;set;} 
    }
}