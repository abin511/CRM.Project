 
using CRM.Model;
using CRM.IBLL;
namespace CRM.BLL
{
	//T4模板自动生成，请勿编辑代码
	
	public partial class ActionGroupService:BaseCrmManageServices<ActionGroup>,IActionGroupService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.ActionGroupRepository;
        }  
    }
	
	public partial class ActionInfoService:BaseCrmManageServices<ActionInfo>,IActionInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.ActionInfoRepository;
        }  
    }
	
	public partial class AdminInfoService:BaseCrmManageServices<AdminInfo>,IAdminInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.AdminInfoRepository;
        }  
    }
	
	public partial class R_AdminInfo_ActionInfoService:BaseCrmManageServices<R_AdminInfo_ActionInfo>,IR_AdminInfo_ActionInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.R_AdminInfo_ActionInfoRepository;
        }  
    }
	
	public partial class R_AdminInfo_RoleService:BaseCrmManageServices<R_AdminInfo_Role>,IR_AdminInfo_RoleService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.R_AdminInfo_RoleRepository;
        }  
    }
	
	public partial class RoleService:BaseCrmManageServices<Role>,IRoleService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.RoleRepository;
        }  
    }
	
}