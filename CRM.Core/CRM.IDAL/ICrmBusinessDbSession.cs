 
namespace CRM.IDAL
{
    public partial interface IDbSession
    {
	  
		IUserAccountRepository UserAccountRepository { get; }
	  
		IUserBaseInfoRepository UserBaseInfoRepository { get; }
	  
		IUserLoginInSideRepository UserLoginInSideRepository { get; }
	  
		IUserLoginOutSideRepository UserLoginOutSideRepository { get; }
	}
}