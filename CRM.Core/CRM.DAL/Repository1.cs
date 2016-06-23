 
using CRM.IDAL;
using CRM.Model;
namespace CRM.DAL
{ 
	
	public partial class ActionGroupRepository :BaseRepository<ActionGroup>,IActionGroupRepository
    {
    }
	
	public partial class ActionInfoRepository :BaseRepository<ActionInfo>,IActionInfoRepository
    {
    }
	
	public partial class CategoryRepository :BaseRepository<Category>,ICategoryRepository
    {
    }
	
	public partial class GoodInfoRepository :BaseRepository<GoodInfo>,IGoodInfoRepository
    {
    }
	
	public partial class GoodSKURepository :BaseRepository<GoodSKU>,IGoodSKURepository
    {
    }
	
	public partial class GoodsPropValueRepository :BaseRepository<GoodsPropValue>,IGoodsPropValueRepository
    {
    }
	
	public partial class ImageInfoRepository :BaseRepository<ImageInfo>,IImageInfoRepository
    {
    }
	
	public partial class PropertyRepository :BaseRepository<Property>,IPropertyRepository
    {
    }
	
	public partial class PropOptionRepository :BaseRepository<PropOption>,IPropOptionRepository
    {
    }
	
	public partial class R_UserInfo_ActionInfoRepository :BaseRepository<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoRepository
    {
    }
	
	public partial class R_UserInfo_RoleRepository :BaseRepository<R_UserInfo_Role>,IR_UserInfo_RoleRepository
    {
    }
	
	public partial class RoleRepository :BaseRepository<Role>,IRoleRepository
    {
    }
	
	public partial class ShopRepository :BaseRepository<Shop>,IShopRepository
    {
    }
	
	public partial class UserInfoRepository :BaseRepository<UserInfo>,IUserInfoRepository
    {
    }
}