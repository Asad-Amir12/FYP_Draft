using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public enum ZoneType { Detection, Attack }
    [SerializeField] public ZoneType zoneType;
    public EnemyBaseStateMachine machine;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        machine.HandleZoneTrigger(zoneType, true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        machine.HandleZoneTrigger(zoneType, false);
    }
}
