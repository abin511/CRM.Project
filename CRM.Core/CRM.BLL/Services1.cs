 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.BLL;
using CRM.IDAL;
using CRM.Model;

namespace CRM.BLL
{
	
	public partial class ActionGroupService:BaseService<ActionGroup>,IBLL.IActionGroupService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.ActionGroupRepository;
        }  
    }
	
	public partial class ActionInfoService:BaseService<ActionInfo>,IBLL.IActionInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.ActionInfoRepository;
        }  
    }
	
	public partial class CategoryService:BaseService<Category>,IBLL.ICategoryService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.CategoryRepository;
        }  
    }
	
	public partial class GoodInfoService:BaseService<GoodInfo>,IBLL.IGoodInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.GoodInfoRepository;
        }  
    }
	
	public partial class GoodSKUService:BaseService<GoodSKU>,IBLL.IGoodSKUService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.GoodSKURepository;
        }  
    }
	
	public partial class GoodsPropValueService:BaseService<GoodsPropValue>,IBLL.IGoodsPropValueService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.GoodsPropValueRepository;
        }  
    }
	
	public partial class ImageInfoService:BaseService<ImageInfo>,IBLL.IImageInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.ImageInfoRepository;
        }  
    }
	
	public partial class PropertyService:BaseService<Property>,IBLL.IPropertyService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.PropertyRepository;
        }  
    }
	
	public partial class PropOptionService:BaseService<PropOption>,IBLL.IPropOptionService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.PropOptionRepository;
        }  
    }
	
	public partial class R_UserInfo_ActionInfoService:BaseService<R_UserInfo_ActionInfo>,IBLL.IR_UserInfo_ActionInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.R_UserInfo_ActionInfoRepository;
        }  
    }
	
	public partial class R_UserInfo_RoleService:BaseService<R_UserInfo_Role>,IBLL.IR_UserInfo_RoleService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.R_UserInfo_RoleRepository;
        }  
    }
	
	public partial class RoleService:BaseService<Role>,IBLL.IRoleService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.RoleRepository;
        }  
    }
	
	public partial class ShopService:BaseService<Shop>,IBLL.IShopService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.ShopRepository;
        }  
    }
	
	public partial class UserInfoService:BaseService<UserInfo>,IBLL.IUserInfoService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.UserInfoRepository;
        }  
    }
	
}