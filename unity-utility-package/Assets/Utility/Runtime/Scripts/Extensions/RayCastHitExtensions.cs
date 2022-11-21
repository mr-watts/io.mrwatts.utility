using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public static class RayCastHitExtensions
    {
        public static IEnumerable<RaycastHit> OrderByDistance(this RaycastHit[] list, OrderMode orderMode = OrderMode.Ascending)
        {
            if (orderMode == OrderMode.Ascending)
            {
                return list.OrderBy(x => x.distance);
            }

            return list.OrderByDescending(x => x.distance);
        }
    }
}