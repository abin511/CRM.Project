 
using CRM.IDAL;
namespace CRM.DAL
{
	//T4模板自动生成，请勿编辑代码
    public partial class CrmBusinessDbSession:ICrmBusinessDbSession
    {   
		
		public ICustomConfigRepository CustomConfigRepository { get { return new CustomConfigRepository(); } }
		
		public IGoldRecordRepository GoldRecordRepository { get { return new GoldRecordRepository(); } }
		
		public IIntegralRecordRepository IntegralRecordRepository { get { return new IntegralRecordRepository(); } }
		
		public IMoneyRecordRepository MoneyRecordRepository { get { return new MoneyRecordRepository(); } }
		
		public IRoomRepository RoomRepository { get { return new RoomRepository(); } }
		
		public IRoomRecordRepository RoomRecordRepository { get { return new RoomRecordRepository(); } }
		
		public IUserAccountRepository UserAccountRepository { get { return new UserAccountRepository(); } }
		
		public IUserBaseRepository UserBaseRepository { get { return new UserBaseRepository(); } }
		
		public IUserFansRepository UserFansRepository { get { return new UserFansRepository(); } }
		
		public IUserLoginInSideRepository UserLoginInSideRepository { get { return new UserLoginInSideRepository(); } }
		
		public IUserLoginOutSideRepository UserLoginOutSideRepository { get { return new UserLoginOutSideRepository(); } }
		}
}