 
namespace CRM.IDAL
{
	//T4模板自动生成，请勿编辑代码
    public partial interface ICrmBusinessDbSession
    {
	  
		ICustomConfigRepository CustomConfigRepository { get; }
	  
		IGoldRecordRepository GoldRecordRepository { get; }
	  
		IIntegralRecordRepository IntegralRecordRepository { get; }
	  
		IMoneyRecordRepository MoneyRecordRepository { get; }
	  
		IRoomRepository RoomRepository { get; }
	  
		IRoomRecordRepository RoomRecordRepository { get; }
	  
		IUserAccountRepository UserAccountRepository { get; }
	  
		IUserBaseRepository UserBaseRepository { get; }
	  
		IUserFansRepository UserFansRepository { get; }
	  
		IUserLoginInSideRepository UserLoginInSideRepository { get; }
	  
		IUserLoginOutSideRepository UserLoginOutSideRepository { get; }
	}
}