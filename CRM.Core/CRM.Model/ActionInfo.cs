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
    
    public partial class ActionInfo
    {
        public ActionInfo()
        {
            this.R_AdminInfo_ActionInfo = new HashSet<R_AdminInfo_ActionInfo>();
            this.ActionGroup = new HashSet<ActionGroup>();
            this.Role = new HashSet<Role>();
        }
    
        public int ID { get; set; }
        public string RequestUrl { get; set; }
        public string RequestHttpType { get; set; }
        public string ActionName { get; set; }
        public System.DateTime SubTime { get; set; }
        public short ActionType { get; set; }
    
        public virtual ICollection<R_AdminInfo_ActionInfo> R_AdminInfo_ActionInfo { get; set; }
        public virtual ICollection<ActionGroup> ActionGroup { get; set; }
        public virtual ICollection<Role> Role { get; set; }
    }
}
