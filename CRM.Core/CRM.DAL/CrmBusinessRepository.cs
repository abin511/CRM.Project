 
using CRM.IDAL;
using CRM.Model;
namespace CRM.DAL
{ 
	//T4模板自动生成，请勿编辑代码
	
	public partial class UserAccountRepository :CrmBusinessRepository<UserAccount>,IUserAccountRepository
    {
    }
	
	public partial class UserBaseInfoRepository :CrmBusinessRepository<UserBaseInfo>,IUserBaseInfoRepository
    {
    }
	
	public partial class UserLoginInSideRepository :CrmBusinessRepository<UserLoginInSide>,IUserLoginInSideRepository
    {
    }
	
	public partial class UserLoginOutSideRepository :CrmBusinessRepository<UserLoginOutSide>,IUserLoginOutSideRepository
    {
    }
}