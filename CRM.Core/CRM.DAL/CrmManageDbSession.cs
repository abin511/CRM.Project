 
using CRM.IDAL;
namespace CRM.DAL
{
    public partial class DbSession:IDbSession
    {   
		
		public IActionGroupRepository ActionGroupRepository { get { return new ActionGroupRepository(); } }
		
		public IActionInfoRepository ActionInfoRepository { get { return new ActionInfoRepository(); } }
		
		public IR_UserInfo_ActionInfoRepository R_UserInfo_ActionInfoRepository { get { return new R_UserInfo_ActionInfoRepository(); } }
		
		public IR_UserInfo_RoleRepository R_UserInfo_RoleRepository { get { return new R_UserInfo_RoleRepository(); } }
		
		public IRoleRepository RoleRepository { get { return new RoleRepository(); } }
		
		public IUserInfoRepository UserInfoRepository { get { return new UserInfoRepository(); } }
		}
}