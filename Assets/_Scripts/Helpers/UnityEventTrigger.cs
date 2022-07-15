using UnityEngine;
using UnityEngine.Events;

namespace GMTK2022
{
    public class UnityEventTrigger : MonoBehaviour
    {
        public UnityEvent OnAwakeEvent;
        public UnityEvent OnStartEvent;
        public UnityEvent OnEnableEvent;
        public UnityEvent OnDisableEvent;

        private void Awake() {
            OnAwakeEvent?.Invoke();
        }

        private void Start() {
            OnStartEvent?.Invoke();
        }

        private void OnEnable() {
            OnEnableEvent?.Invoke();
        }

        private void OnDisable() {
            OnDisableEvent?.Invoke();
        }
    }
}
