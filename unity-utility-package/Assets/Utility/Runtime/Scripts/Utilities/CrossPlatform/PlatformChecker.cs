#if ENABLE_WINMD_SUPPORT
using Windows.UI.ViewManagement;
using Windows.System.Profile;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MrWatts.Internal.Utilities
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
                    currentPlatform |= Platform.Desktop;
                }

                if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.Mobile))
                {
                    currentPlatform |= Platform.WindowsPhone;
                }

                if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.Xbox))
                {
                    currentPlatform |= Platform.WindowsXbox;
                }

                if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.Holographic))
                {
                    currentPlatform |= Platform.HoloLens2;
                }

                if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.Team))
                {
                    currentPlatform |= Platform.WindowsSurfaceHub;
                }

                if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.IoT))
                {
                    currentPlatform |= Platform.WindowsIoT;
                }

                if (PlayerSettings.WSA.GetTargetDeviceFamily(PlayerSettings.WSATargetFamily.IoTHeadless))
                {
                    currentPlatform |= Platform.WindowsIoT;
                }
#endif
#endif

#if ENABLE_WINMD_SUPPORT
                currentPlatform |= AnalyticsInfo.VersionInfo.DeviceFamily switch
                {
                    "Windows.Mobile" => Platform.WindowsPhone,
                    "Windows.Desktop" => Platform.WindowsDesktop | Platform.Desktop,
                    "Windows.Universal" => Platform.WindowsIoT,
                    "Windows.Team" => Platform.WindowsSurfaceHub,
                    "Windows.Xbox" => Platform.WindowsXbox,
                    "Windows.Holographic" => Platform.HoloLens2,
                    _ => Platform.Unknown,
                };
#endif

#if PLATFORM_ANDROID
                currentPlatform |= Platform.Android;
#endif

#if UNITY_IOS
                currentPlatform |= Platform.IOS;
#endif

#if UNITY_STANDALONE
                currentPlatform |= Platform.Desktop;

#if UNITY_STANDALONE_WIN
                currentPlatform |= Platform.WindowsDesktop;
#endif
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