 
using CRM.IDAL;
namespace CRM.DAL
{
	//T4模板自动生成，请勿编辑代码
    public partial class CrmManageDbSession:ICrmManageDbSession
    {   
		
		public IActionGroupRepository ActionGroupRepository { get { return new ActionGroupRepository(); } }
		
		public IActionInfoRepository ActionInfoRepository { get { return new ActionInfoRepository(); } }
		
		public IAdminInfoRepository AdminInfoRepository { get { return new AdminInfoRepository(); } }
		
		public IR_AdminInfo_ActionInfoRepository R_AdminInfo_ActionInfoRepository { get { return new R_AdminInfo_ActionInfoRepository(); } }
		
		public IR_AdminInfo_RoleRepository R_AdminInfo_RoleRepository { get { return new R_AdminInfo_RoleRepository(); } }
		
		public IRoleRepository RoleRepository { get { return new RoleRepository(); } }
		}
}