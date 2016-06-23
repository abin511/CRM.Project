 
namespace CRM.IDAL
{
    public partial interface IDbSession
    {
	  
		IActionGroupRepository ActionGroupRepository { get; }
	  
		IActionInfoRepository ActionInfoRepository { get; }
	  
		ICategoryRepository CategoryRepository { get; }
	  
		IGoodInfoRepository GoodInfoRepository { get; }
	  
		IGoodSKURepository GoodSKURepository { get; }
	  
		IGoodsPropValueRepository GoodsPropValueRepository { get; }
	  
		IImageInfoRepository ImageInfoRepository { get; }
	  
		IPropertyRepository PropertyRepository { get; }
	  
		IPropOptionRepository PropOptionRepository { get; }
	  
		IR_UserInfo_ActionInfoRepository R_UserInfo_ActionInfoRepository { get; }
	  
		IR_UserInfo_RoleRepository R_UserInfo_RoleRepository { get; }
	  
		IRoleRepository RoleRepository { get; }
	  
		IShopRepository ShopRepository { get; }
	  
		IUserInfoRepository UserInfoRepository { get; }
	}
}