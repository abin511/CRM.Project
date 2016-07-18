 
using CRM.Model;
using CRM.IBLL;
namespace CRM.BLL
{
	//T4模板自动生成，请勿编辑代码
	
	public partial class CustomConfigService:BaseCrmBusinessServices<CustomConfig>,ICustomConfigService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.CustomConfigRepository;
        }  
    }
	
	public partial class GoldRecordService:BaseCrmBusinessServices<GoldRecord>,IGoldRecordService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.GoldRecordRepository;
        }  
    }
	
	public partial class MoneyRecordService:BaseCrmBusinessServices<MoneyRecord>,IMoneyRecordService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.MoneyRecordRepository;
        }  
    }
	
	public partial class RoomService:BaseCrmBusinessServices<Room>,IRoomService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.RoomRepository;
        }  
    }
	
	public partial class RoomRecordService:BaseCrmBusinessServices<RoomRecord>,IRoomRecordService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.RoomRecordRepository;
        }  
    }
	
	public partial class UserAccountService:BaseCrmBusinessServices<UserAccount>,IUserAccountService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.UserAccountRepository;
        }  
    }
	
	public partial class UserBaseService:BaseCrmBusinessServices<UserBase>,IUserBaseService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.UserBaseRepository;
        }  
    }
	
	public partial class UserFansService:BaseCrmBusinessServices<UserFans>,IUserFansService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.UserFansRepository;
        }  
    }
	
	public partial class UserLoginInSideService:BaseCrmBusinessServices<UserLoginInSide>,IUserLoginInSideService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.UserLoginInSideRepository;
        }  
    }
	
	public partial class UserLoginOutSideService:BaseCrmBusinessServices<UserLoginOutSide>,IUserLoginOutSideService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = DbSession.UserLoginOutSideRepository;
        }  
    }
}