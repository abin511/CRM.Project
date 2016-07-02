 
namespace CRM.IDAL
{
	//T4模板自动生成，请勿编辑代码
    public partial interface ICrmBusinessDbSession
    {
	  
		IUserAccountRepository UserAccountRepository { get; }
	  
		IUserBaseInfoRepository UserBaseInfoRepository { get; }
	  
		IUserLoginInSideRepository UserLoginInSideRepository { get; }
	  
		IUserLoginOutSideRepository UserLoginOutSideRepository { get; }
	}
}