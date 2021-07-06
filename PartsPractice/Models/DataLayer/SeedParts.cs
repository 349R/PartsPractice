using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PartsPractice.Models.DataLayer
{
    internal class SeedParts : IEntityTypeConfiguration<Parts>
    {
        public void Configure(EntityTypeBuilder<Parts> entity)
        {
            entity.HasData(
                new Parts { PartId = 1,
                    Name = "Hammer",
                    Description = "Ball pein Hammer",
                    QtyOnHand = 100,
                    Price = 44.44m,
                    Available = true
                },
                new Parts
                {
                    PartId = 2,
                    Name = "ScrewDriver",
                    Description = "Flat head",
                    QtyOnHand = 50,
                    Price = 10.44m,
                    Available = true
                }

                ) ;
        
        }
    }
}
