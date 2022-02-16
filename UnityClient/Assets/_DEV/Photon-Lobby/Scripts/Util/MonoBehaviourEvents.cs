using UnityEngine;

namespace Platformer.Utils
{
    public class MonoBehaviourEvents : MonoBehaviour
    {
        [SerializeField] private SimpleEvent _pauseEv;
        [SerializeField] private SimpleEvent _resumeEv;
        [SerializeField] private SimpleEvent _quitEvent;
        [SerializeField] private SimpleEvent _awakeEvent;
        [SerializeField] private SimpleEvent _startEvent;

        protected void Awake()
        {
            _awakeEvent.Invoke(); 
        }

        protected void Start()
        {
            _startEvent.Invoke();
        }

        protected void OnApplicationPause(bool pause)
        {
            if(pause) 
                _pauseEv.Invoke();
            else 
                _resumeEv.Invoke();
        }

        protected void OnApplicationQuit()
        {
            _quitEvent.Invoke(); 
        }
    }
}