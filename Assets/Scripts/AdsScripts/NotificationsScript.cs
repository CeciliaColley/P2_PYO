using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class LocalNotisManager : MonoBehaviour
{
    private static string CHANNEL_ID = "notis01";

    private void Start()
    {
        string NotiChannels_Created_Key = "NotiChannels_Created";
        if (!PlayerPrefs.HasKey(NotiChannels_Created_Key))
        {
            var group = new AndroidNotificationChannelGroup()
            {
                Id = "Main",
                Name = "Main notifications",
            };
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

            StartCoroutine(RequestPermission());

            PlayerPrefs.SetString(NotiChannels_Created_Key, "y");
            PlayerPrefs.Save();
        }
        else
        {
            ScheduleNotis();
        }
    }

    private IEnumerator RequestPermission()
    {
        var request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
            yield return null;

        ScheduleNotis();
    }

    private void ScheduleNotis()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        var notification10Mins = new AndroidNotification();
        notification10Mins.Title = "Cecilia Illueca";
        notification10Mins.Text = "Este es el push notification de Cecilia Illueca, PYO_Parcial 2!";
        notification10Mins.FireTime = System.DateTime.Now.AddMinutes(10);

        AndroidNotificationCenter.SendNotification(notification10Mins, CHANNEL_ID);
    }
}
