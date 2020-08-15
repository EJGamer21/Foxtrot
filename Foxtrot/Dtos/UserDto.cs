using System;

namespace Foxtrot.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Dni { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
    }
}