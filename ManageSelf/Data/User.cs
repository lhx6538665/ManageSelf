//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Role = new HashSet<Role>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public System.DateTime RegistrationTime { get; set; }
        public Nullable<System.DateTime> LoginTime { get; set; }
        public string LoginIP { get; set; }
    
        public virtual ICollection<Role> Role { get; set; }
    }
}
