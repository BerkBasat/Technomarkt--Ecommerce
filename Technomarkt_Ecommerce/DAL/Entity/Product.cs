﻿using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Product : EntityRepository
    {
        public string ProductName { get; set; }
        public decimal SupplyCost { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public string ProductImagePath { get; set; }
        public string Description { get; set; }

        public Guid SubCategoryId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual List<UserComment> UserComments { get; set; }
    }
}
