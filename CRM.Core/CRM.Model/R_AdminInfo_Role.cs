//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRM.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class R_AdminInfo_Role
    {
        public int ID { get; set; }
        public int UserInfoID { get; set; }
        public int RoleID { get; set; }
        public System.DateTime SubTime { get; set; }
    
        public virtual AdminInfo AdminInfo { get; set; }
        public virtual Role Role { get; set; }
    }
}
