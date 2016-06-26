 
using CRM.IDAL;
using CRM.Model;
namespace CRM.DAL
{ 
	
	public partial class ActionGroupRepository :CrmManageRepository<ActionGroup>,IActionGroupRepository
    {
    }
	
	public partial class ActionInfoRepository :CrmManageRepository<ActionInfo>,IActionInfoRepository
    {
    }
	
	public partial class R_UserInfo_ActionInfoRepository :CrmManageRepository<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoRepository
    {
    }
	
	public partial class R_UserInfo_RoleRepository :CrmManageRepository<R_UserInfo_Role>,IR_UserInfo_RoleRepository
    {
    }
	
	public partial class RoleRepository :CrmManageRepository<Role>,IRoleRepository
    {
    }
	
	public partial class UserInfoRepository :CrmManageRepository<UserInfo>,IUserInfoRepository
    {
    }
}