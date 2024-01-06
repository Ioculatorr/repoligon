using UnityEngine;
using UnityEngine.Events;
public class RadioSignalChange : MonoBehaviour
{
    [SerializeField] private UnityEvent SignalChangeOn;
    [SerializeField] private UnityEvent SignalChangeOff;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            SignalChangeOn.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            SignalChangeOff.Invoke();
        }
    }
}
