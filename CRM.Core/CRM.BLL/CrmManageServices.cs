 

using CRM.Model;
namespace CRM.BLL
{
	
	public partial class ActionGroupService:BaseCrmManageService<ActionGroup>,IBLL.IActionGroupService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.ActionGroupRepository;
        }  
    }
	
	public partial class ActionInfoService:BaseCrmManageService<ActionInfo>,IBLL.IActionInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.ActionInfoRepository;
        }  
    }
	
	public partial class R_UserInfo_ActionInfoService:BaseCrmManageService<R_UserInfo_ActionInfo>,IBLL.IR_UserInfo_ActionInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.R_UserInfo_ActionInfoRepository;
        }  
    }
	
	public partial class R_UserInfo_RoleService:BaseCrmManageService<R_UserInfo_Role>,IBLL.IR_UserInfo_RoleService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.R_UserInfo_RoleRepository;
        }  
    }
	
	public partial class RoleService:BaseCrmManageService<Role>,IBLL.IRoleService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.RoleRepository;
        }  
    }
	
	public partial class UserInfoService:BaseCrmManageService<UserInfo>,IBLL.IUserInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.UserInfoRepository;
        }  
    }
	
}