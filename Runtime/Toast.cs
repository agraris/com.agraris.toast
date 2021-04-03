using UnityEngine;

namespace Agraris.Tools
{
    public enum ToastLength
    {
        LENGTH_SHORT, LENGTH_LONG
    }

    public static class Toast
    {
        static string m_ToastMessage;
        static ToastLength m_ToastLength;

        static AndroidJavaClass m_UnityPlayer;
        static AndroidJavaClass unityPlayer
        {
            get
            {
                if (m_UnityPlayer == null)
                    m_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

                return m_UnityPlayer;
            }
        }

        public static void ShowToast(string message)
        {
            ShowToast(message, ToastLength.LENGTH_SHORT);
        }

        public static void ShowToast(string message, ToastLength length)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            m_ToastMessage = message;
            m_ToastLength = length;
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(ShowToast));
#endif
        }

#if UNITY_ANDROID
        static void ShowToast()
        {
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var context = activity.Call<AndroidJavaObject>("getApplicationContext");
            AndroidJavaClass theToast = new AndroidJavaClass("android.widget.Toast");
            AndroidJavaObject mString = new AndroidJavaObject("java.lang.String", m_ToastMessage);
            AndroidJavaObject toast = theToast.CallStatic<AndroidJavaObject>("makeText", context, mString, theToast.GetStatic<int>(m_ToastLength.ToString()));
            toast.Call("show");
        }
#endif
    }
}
