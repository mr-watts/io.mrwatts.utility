using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public static class PoseExtensions
    {
        public static Pose CorrectRotation(this Pose pose)
        {
            return new Pose(pose.position, Quaternion.Euler(0, pose.rotation.eulerAngles.y, 0));
        }
    }
}