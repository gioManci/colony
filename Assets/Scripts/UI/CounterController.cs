using System;
using UnityEngine;
using UnityEngine.UI;

namespace Colony.UI
{
    public class CounterController : MonoBehaviour
    {
        public float StartTime { get; set; }
        public bool IsRunning { get; private set; }
        public event Action<CounterController> TimeExpired;

        private Text label;
        private float elapsedTime;

        void Awake()
        {
            elapsedTime = 0.0f;
            for(int i=0;i<gameObject.transform.childCount; i++)
            {
                GameObject obj = gameObject.transform.GetChild(i).gameObject;
                if (obj.name == "Counter")
                {
                    label = obj.GetComponent<Text>();
                }
            }
        }

        void Update()
        {
            if (IsRunning)
            {
                elapsedTime -= Time.deltaTime;
                if (elapsedTime <= 0)
                {
                    if (TimeExpired != null)
                    {
                        TimeExpired(this);
                    }
                }
                else
                {
                    if (elapsedTime <= 30.0f)
                    {
                        label.color = Color.red;
                    }
                    TimeSpan t = TimeSpan.FromSeconds(elapsedTime);
                    label.text = string.Format("{0:00}:{1:00}", t.Minutes, t.Seconds);
                }
            }
            else
            {
                label.text = StartTime.ToString();
            }
        }

        public void StartCountdown()
        {
            elapsedTime = StartTime;
            IsRunning = true;
        }

        public void Reset()
        {
            elapsedTime = StartTime;
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
