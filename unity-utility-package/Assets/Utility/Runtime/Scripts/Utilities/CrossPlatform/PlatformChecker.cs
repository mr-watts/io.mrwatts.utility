#if ENABLE_WINMD_SUPPORT
using Windows.UI.ViewManagement;
using Windows.System.Profile;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MrWatts.CommScope.EnclosureVisualizer
{
    public static class PlatformChecker
    {
        public static Platform CurrentPlatform
        {
            get
            {
                Platform currentPlatform = Platform.Unknown;

#if UNITY_EDITOR
                currentPlatform |= Platform.Editor;

#if UNITY_WSA_10_0
                if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.Desktop))
                {
                    currentPlatform |= Platform.WindowsDesktop;
                }
                else if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.Mobile))
                {
                    currentPlatform |= Platform.WindowsPhone;
                    currentPlatform |= Platform.WindowsTablet;
                }
                else if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.Xbox))
                {
                    currentPlatform |= Platform.WindowsXbox;
                }
                else if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.Holographic))
                {
                    currentPlatform |= Platform.HoloLens2;
                }
                else if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.Team))
                {
                    currentPlatform |= Platform.WindowsSurfaceHub;
                }
                else if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.IoT))
                {
                    currentPlatform |= Platform.WindowsIoT;
                }
                else if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.IoTHeadless))
                {
                    currentPlatform |= Platform.WindowsIoT;
                }
#endif
#endif

#if ENABLE_WINMD_SUPPORT
                currentPlatform |= AnalyticsInfo.VersionInfo.DeviceFamily switch
                {
                    "Windows.Mobile" => Platform.WindowsPhone;
                    "Windows.Desktop" => UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Mouse ? Platform.WindowsDesktop : Platform.WindowsTablet;
                    "Windows.Universal" => Platform.WindowsIoT;
                    "Windows.Team" => Platform.WindowsSurfaceHub;
                    "Windows.Xbox" => Platform.WindowsXbox;
                    "Windows.Holographic" => Platform.HoloLens2;
                    _ => Platform.Unknown;
                };
#endif

#if PLATFORM_ANDROID
                currentPlatform |= Platform.Android;
#endif

#if UNITY_IOS
                currentPlatform |= Platform.IOS;
#endif

                return currentPlatform;
            }
        }

        public static bool Check(Platform platform)
        {
            return (platform & CurrentPlatform) != 0;
        }
    }
}