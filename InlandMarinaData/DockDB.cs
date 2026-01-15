using InlandMarinaData.Data;
using InlandMarinaData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public static class DockDB
    {
        public static List<Dock> GetDocks(InlandMarinaContext context)
        {
            List<Dock> docks = context.Docks.ToList();
            return docks;
        }
    }
}
