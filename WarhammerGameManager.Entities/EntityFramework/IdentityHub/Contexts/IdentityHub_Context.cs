using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.IdentityHub.Contexts
{
    public class IdentityHub_Context : IdentityDbContext
    {
        public IdentityHub_Context(DbContextOptions<IdentityHub_Context> options)
            : base(options)
        {
        }
    }
}
