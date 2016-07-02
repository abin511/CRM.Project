 
namespace CRM.IDAL
{
	//T4模板自动生成，请勿编辑代码
    public partial interface ICrmManageDbSession
    {
	  
		IActionGroupRepository ActionGroupRepository { get; }
	  
		IActionInfoRepository ActionInfoRepository { get; }
	  
		IAdminInfoRepository AdminInfoRepository { get; }
	  
		IR_AdminInfo_ActionInfoRepository R_AdminInfo_ActionInfoRepository { get; }
	  
		IR_AdminInfo_RoleRepository R_AdminInfo_RoleRepository { get; }
	  
		IRoleRepository RoleRepository { get; }
	}
}