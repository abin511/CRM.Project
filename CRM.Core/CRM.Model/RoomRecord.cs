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
    
    public partial class RoomRecord
    {
        public int ID { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public bool IsOnline { get; set; }
        public int OnlineMinutes { get; set; }
        public System.DateTime InsertTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
    
        public virtual Room Room { get; set; }
        public virtual UserBase UserBase { get; set; }
    }
}
