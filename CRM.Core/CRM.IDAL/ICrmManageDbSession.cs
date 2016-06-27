 
namespace CRM.IDAL
{
    public partial interface IDbSession
    {
	  
		IActionGroupRepository ActionGroupRepository { get; }
	  
		IActionInfoRepository ActionInfoRepository { get; }
	  
		IR_UserInfo_ActionInfoRepository R_UserInfo_ActionInfoRepository { get; }
	  
		IR_UserInfo_RoleRepository R_UserInfo_RoleRepository { get; }
	  
		IRoleRepository RoleRepository { get; }
	  
		IUserInfoRepository UserInfoRepository { get; }
	}
}