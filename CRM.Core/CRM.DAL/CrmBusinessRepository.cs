 
using CRM.IDAL;
using CRM.Model;
namespace CRM.DAL
{ 
	
	public partial class UserAccountRepository :CrmManageRepository<UserAccount>,IUserAccountRepository
    {
    }
	
	public partial class UserBaseInfoRepository :CrmManageRepository<UserBaseInfo>,IUserBaseInfoRepository
    {
    }
	
	public partial class UserLoginInSideRepository :CrmManageRepository<UserLoginInSide>,IUserLoginInSideRepository
    {
    }
	
	public partial class UserLoginOutSideRepository :CrmManageRepository<UserLoginOutSide>,IUserLoginOutSideRepository
    {
    }
}