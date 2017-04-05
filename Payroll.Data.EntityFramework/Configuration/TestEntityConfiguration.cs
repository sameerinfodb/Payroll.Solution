using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Domain.Entities;

namespace Payroll.Data.EntityFramework.Configuration
{
    internal class TestEntityConfiguration:EntityTypeConfiguration<TestEntity>
    {
        internal TestEntityConfiguration()
        {
            ToTable("TestEntityTable");

            HasKey(x => x.id);
        }
    }
}
