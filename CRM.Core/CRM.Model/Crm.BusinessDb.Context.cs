﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRM.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CrmBusinessDbContainer : DbContext
    {
        public CrmBusinessDbContainer()
            : base("name=CrmBusinessDbContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CustomConfig> CustomConfig { get; set; }
        public DbSet<GoldRecord> GoldRecord { get; set; }
        public DbSet<IntegralRecord> IntegralRecord { get; set; }
        public DbSet<MoneyRecord> MoneyRecord { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomRecord> RoomRecord { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<UserBase> UserBase { get; set; }
        public DbSet<UserFans> UserFans { get; set; }
        public DbSet<UserLoginInSide> UserLoginInSide { get; set; }
        public DbSet<UserLoginOutSide> UserLoginOutSide { get; set; }
    }
}
