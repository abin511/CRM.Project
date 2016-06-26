 

using CRM.Model;
namespace CRM.BLL
{
	
	public partial class ActionGroupService:BaseService<ActionGroup>,IBLL.IActionGroupService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.ActionGroupRepository;
        }  
    }
	
	public partial class ActionInfoService:BaseService<ActionInfo>,IBLL.IActionInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.ActionInfoRepository;
        }  
    }
	
	public partial class R_UserInfo_ActionInfoService:BaseService<R_UserInfo_ActionInfo>,IBLL.IR_UserInfo_ActionInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.R_UserInfo_ActionInfoRepository;
        }  
    }
	
	public partial class R_UserInfo_RoleService:BaseService<R_UserInfo_Role>,IBLL.IR_UserInfo_RoleService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.R_UserInfo_RoleRepository;
        }  
    }
	
	public partial class RoleService:BaseService<Role>,IBLL.IRoleService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.RoleRepository;
        }  
    }
	
	public partial class UserInfoService:BaseService<UserInfo>,IBLL.IUserInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.UserInfoRepository;
        }  
    }
	
}