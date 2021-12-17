using Core.Entities.Abstract;
using System;

namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Session { get; set; }
        public DateTime SessionExpireDate { get; set; }
    }
}