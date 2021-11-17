using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.Entity;

namespace Vishal_NimapInfotech.Models
{
    public class CFADBContext: DbContext
    {

        public CFADBContext():base("StringDBContext")
        {

        }

        public DbSet<CategoryMst> categoryMst { get; set; }
        public DbSet<ProductMst> productMst { get; set; }
    }
}