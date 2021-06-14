using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NLayerProject.Data.Seeds
{
    public class CategorySeed:IEntityTypeConfiguration<Category>
    {
        private readonly int[] _ids;
        public CategorySeed(int[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category { Id = _ids[0], Name = "Kalemler" },
                new Category { Id = _ids[1], Name = "Defterler" }
                );
        }
    }
}
