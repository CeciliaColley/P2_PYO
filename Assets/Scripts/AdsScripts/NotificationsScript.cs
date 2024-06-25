using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;


public class LocalNotisManager : MonoBehaviour
{
#if UNITY_ANDROID
    private static string CHANNEL_ID = "notis01";

    private void Start()
    {
        string NotiChannels_Created_Key = "NotiChannels_Created";

        // Check if notification channels are already created
        if (!PlayerPrefs.HasKey(NotiChannels_Created_Key))
        {
            // Create a notification channel group
            var group = new AndroidNotificationChannelGroup()
            {
                Id = "Main",
                Name = "Main notifications",
            };
            // Create a new notifications channel
            AndroidNotificationCenter.RegisterNotificationChannelGroup(group);
            var channel = new AndroidNotificationChannel()
            {
                Id = CHANNEL_ID,
                Name = "Default Channel",
                Importance = Importance.Default,
                Description = "Generic notifications",
                Group = "Main",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);

            // Request notification permission
            StartCoroutine(RequestPermission());

            // Mark that the notification channels are created
            PlayerPrefs.SetString(NotiChannels_Created_Key, "y");
            PlayerPrefs.Save();
        }
        else
        {
            ScheduleNotis();
        }
    }

    // Request permision to send notifications
    private IEnumerator RequestPermission()
    {
        var request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
            yield return null;

        ScheduleNotis();
    }

    // Create a new cotification to be sent out 10 minutes after the game is closed.
    private void ScheduleNotis()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        var notification10Mins = new AndroidNotification();
        notification10Mins.Title = "Cecilia Illueca";
        notification10Mins.Text = "Este es el push notification de Cecilia Illueca, PYO_Parcial 2!";
        notification10Mins.FireTime = System.DateTime.Now.AddMinutes(10);

        AndroidNotificationCenter.SendNotification(notification10Mins, CHANNEL_ID);
    }
#endif
}
