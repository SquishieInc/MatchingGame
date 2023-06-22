#if UNITY_IOS
using System;
using UnityEngine;
using UnityEngine.iOS;
using System.Collections;
using System.Collections.Generic;

//Remove commenting for iOS Build.  This is a temo fix for android test build


using NotificationType = UnityEngine.iOS.NotificationType;
using NotificationServices = UnityEngine.iOS.NotificationServices;

public class Notifications : MonoBehaviour 
{


	void Awake() 
	{
		UnityEngine.iOS.NotificationServices.ClearLocalNotifications();

		UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();


		UnityEngine.iOS.NotificationServices.RegisterForNotifications
		(UnityEngine.iOS.NotificationType.Alert| 
			UnityEngine.iOS.NotificationType.Badge |  
			UnityEngine.iOS.NotificationType.Sound);
	}


	void ScheduleNotification()
	{
		// schedule notification to be delivered in 24 hours

		UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();

		notif.alertAction = "Matches!";
		notif.repeatInterval = UnityEngine.iOS.CalendarUnit.Hour;
		notif.fireDate = DateTime.Now.AddDays(1);
		notif.alertBody = "Play a New Game, and Improve your Memory!";
		//notif.alertBody = "Decover music with Calvin and Marlo!";

		UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notif);
	}

	void OnApplicationPause (bool isPause)

	{



		if( isPause ) // App going to background

		{

			// cancel all notifications first.

			//#if UNITY_IOS

			UnityEngine.iOS.NotificationServices.ClearLocalNotifications();

			UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();

			ScheduleNotification ();

			//#endif



		}

		else 
		{

			//#if UNITY_IOS

//			Debug.Log("Local notification count = " + UnityEngine.iOS.NotificationServices.localNotificationCount);

			if (UnityEngine.iOS.NotificationServices.localNotificationCount > 0) {



				Debug.Log(UnityEngine.iOS.NotificationServices.localNotifications[0].alertBody);

		}

			// cancel all notifications first.

			UnityEngine.iOS.NotificationServices.ClearLocalNotifications();

			UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();



			//#endif

		}

	}

}
#endif
