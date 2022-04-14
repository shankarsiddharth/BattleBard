using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameSignalReceiver : MonoBehaviour, INotificationReceiver
{
    public Text unitNameText;
    public Text dialogText;

	public void OnNotify(Playable origin, INotification notification, object context)
	{
        if (notification is SetTextSignal)
        {
            SetTextSignal textVal = notification as SetTextSignal;
            dialogText.text = textVal.text;
            unitNameText.text = textVal.speakerName;
        }
	}
}
