using InlandMarinaData.Data;
using InlandMarinaData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public static class LeaseDB
    {
        public static List<Lease> GetLeases(InlandMarinaContext context)
        {
            List<Lease> leases = context.Leases.ToList();
            return leases;
        }
    }
}
