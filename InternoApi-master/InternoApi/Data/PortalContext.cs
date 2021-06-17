using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using InternoApi.Models;

namespace InternoApi.Data
{
    public class PortalContext : DbContext
    {
        public PortalContext() : base("name=PortalConnection")
        {
        }

        public DbSet<Transport> Transports { get; set; }
        public DbSet<Transport_c> Transports_c { get; set; }
    }
}