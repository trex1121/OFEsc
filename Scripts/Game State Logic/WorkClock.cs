using UnityEngine;
using System.Collections;
using System;

public class WorkClock : MonoBehaviour 
{
	public Transform seconds;
	public Transform minutes;
	public Transform hours;
	public bool analog;
	public float scale;

    private float _hours = 6;
    private float _minutes = 0;
    private float _seconds = 0;

    public float GetSeconds
    {
        get {
                
                return _seconds; 
            }
     
    }

    public float GetMinutes
    {
        get { return _minutes; }
    }

    public float GetHours
    {
        get { return _hours; }
    }

	private const float
		hoursToDegrees = 360f / 12f,
		minutesToDegrees = 360f / 60f,
		secondsToDegrees = 360f / 60f;


	void Update()
	{
        _seconds += Time.deltaTime*scale;

        if (_seconds >= 60f)
        {
            _minutes += 1;
            _seconds = 0f;
        }

        if (_minutes >= 60f)
        {
            _hours += 1;
            _minutes = 0;
        }

        if(_hours >= 24f)
        {
            _hours = 0;
        }

        GameWorldClock(_hours, _minutes, _seconds);
	}

    private void GameWorldClock(float _hours, float _minutes, float _seconds)
    {
      

        if (analog)
        {

            hours.localRotation =
                Quaternion.Euler(0f, 0f, _hours * -hoursToDegrees);
            minutes.localRotation =
                Quaternion.Euler(0f, 0f, _minutes * -minutesToDegrees);
            seconds.localRotation =
                Quaternion.Euler(0f, 0f, _seconds * -secondsToDegrees);
        }
        else
        {
            hours.localRotation = Quaternion.Euler(0f, 0f, _hours * -hoursToDegrees);
            minutes.localRotation = Quaternion.Euler(0f, 0f, _minutes * -minutesToDegrees);
            seconds.localRotation = Quaternion.Euler(0f, 0f, _seconds * -secondsToDegrees);

			if(Input.GetKey(KeyCode.X))
			{
				Debug.Log(string.Format("{0}:{1}:{2}",_hours.ToString("00"), _minutes.ToString("00"), _seconds.ToString("00")));
			}
        }
    }

    private void RealWorldClock()
    {
        if (analog)
        {
            TimeSpan timespan = DateTime.Now.TimeOfDay;
            hours.localRotation =
                Quaternion.Euler(0f, 0f, (float)(timespan.TotalHours * scale) * -hoursToDegrees);
            minutes.localRotation =
                Quaternion.Euler(0f, 0f, (float)(timespan.TotalMinutes * scale) * -minutesToDegrees);
            seconds.localRotation =
                Quaternion.Euler(0f, 0f, (float)(timespan.TotalSeconds * scale) * -secondsToDegrees);
        }
        else
        {
            DateTime time = DateTime.Now;

            hours.localRotation = Quaternion.Euler(0f, 0f, (time.Hour * scale) * -hoursToDegrees);
            minutes.localRotation = Quaternion.Euler(0f, 0f, (time.Minute * scale) * -minutesToDegrees);
            seconds.localRotation = Quaternion.Euler(0f, 0f, (time.Second * scale) * -secondsToDegrees);
        }
    }

}
