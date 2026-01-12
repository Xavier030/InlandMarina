using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public static class SlipDB
    {
        public static List<Slip> Getslips(InlandMarinaContext context)
        {
            List<Slip> slips = context.Slips.ToList();
            return slips;
        }
    }
}
