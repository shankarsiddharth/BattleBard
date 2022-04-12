using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

class SetTextSignal : Marker, INotification
{
	public PropertyName id => "SetText";

	public string speakerName;
	[TextArea]
	public string text;
}