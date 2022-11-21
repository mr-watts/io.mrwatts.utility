using UnityEngine;
#if ENABLE_WINMD_SUPPORT
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
#endif

namespace MrWatts.Internal.Utilities
{
    public sealed class QuitAppOnPause : MonoBehaviour
    {
#if ENABLE_WINMD_SUPPORT
        private void Awake()
        {
            CoreApplication.EnteredBackground += EnteredBackground;
        }

        private void EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            CoreApplication.Exit();
        }
#endif
    }
}