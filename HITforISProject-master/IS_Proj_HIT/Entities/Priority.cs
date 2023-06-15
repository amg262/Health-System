using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Entities
{
    public partial class Priority
    {
        public Priority()
        {
            OrderInfos = new HashSet<OrderInfo>();
        }

        public int PriorityId { get; set; }
        public string PriorityName { get; set; }

        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
    }
}
