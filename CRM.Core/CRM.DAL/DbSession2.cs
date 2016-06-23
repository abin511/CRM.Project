 
using System.Data.Objects;
namespace CRM.DAL
{
    public partial class DbSession:IDAL.IDbSession  
    {   
	
	public IDAL.IActionGroupRepository ActionGroupRepository { get { return new ActionGroupRepository(); } }
	
	public IDAL.IActionInfoRepository ActionInfoRepository { get { return new ActionInfoRepository(); } }
	
	public IDAL.ICategoryRepository CategoryRepository { get { return new CategoryRepository(); } }
	
	public IDAL.IGoodInfoRepository GoodInfoRepository { get { return new GoodInfoRepository(); } }
	
	public IDAL.IGoodSKURepository GoodSKURepository { get { return new GoodSKURepository(); } }
	
	public IDAL.IGoodsPropValueRepository GoodsPropValueRepository { get { return new GoodsPropValueRepository(); } }
	
	public IDAL.IImageInfoRepository ImageInfoRepository { get { return new ImageInfoRepository(); } }
	
	public IDAL.IPropertyRepository PropertyRepository { get { return new PropertyRepository(); } }
	
	public IDAL.IPropOptionRepository PropOptionRepository { get { return new PropOptionRepository(); } }
	
	public IDAL.IR_UserInfo_ActionInfoRepository R_UserInfo_ActionInfoRepository { get { return new R_UserInfo_ActionInfoRepository(); } }
	
	public IDAL.IR_UserInfo_RoleRepository R_UserInfo_RoleRepository { get { return new R_UserInfo_RoleRepository(); } }
	
	public IDAL.IRoleRepository RoleRepository { get { return new RoleRepository(); } }
	
	public IDAL.IShopRepository ShopRepository { get { return new ShopRepository(); } }
	
	public IDAL.IUserInfoRepository UserInfoRepository { get { return new UserInfoRepository(); } }
	}
}