 
namespace CRM.IDAL
{
    public partial interface ICrmBusinessDbSession
    {
		int SaveChanges();
	  
		IUserAccountRepository UserAccountRepository { get; }
	  
		IUserBaseInfoRepository UserBaseInfoRepository { get; }
	  
		IUserLoginInSideRepository UserLoginInSideRepository { get; }
	  
		IUserLoginOutSideRepository UserLoginOutSideRepository { get; }
	}
}