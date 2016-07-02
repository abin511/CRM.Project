 
using CRM.IDAL;
using CRM.Model;
namespace CRM.DAL
{
	//T4模板自动生成，请勿编辑代码
	
	public partial class ActionGroupRepository :CrmManageRepository<ActionGroup>,IActionGroupRepository
    {
    }
	
	public partial class ActionInfoRepository :CrmManageRepository<ActionInfo>,IActionInfoRepository
    {
    }
	
	public partial class AdminInfoRepository :CrmManageRepository<AdminInfo>,IAdminInfoRepository
    {
    }
	
	public partial class R_AdminInfo_ActionInfoRepository :CrmManageRepository<R_AdminInfo_ActionInfo>,IR_AdminInfo_ActionInfoRepository
    {
    }
	
	public partial class R_AdminInfo_RoleRepository :CrmManageRepository<R_AdminInfo_Role>,IR_AdminInfo_RoleRepository
    {
    }
	
	public partial class RoleRepository :CrmManageRepository<Role>,IRoleRepository
    {
    }
}