﻿using System.Data.Entity;

namespace chatapp.data.Model
{
    public class DataDbContext : DbContext
    {
        public DataDbContext()
            : base("name=ChatConn")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        #region Models Declaration 

        public virtual DbSet<ChatRoom> ChatRooms { get; set; }
        public virtual DbSet<User> Users { get; set; }

        #endregion
    }
}
