// using UnityEngine;
// using UnityEngine.AI;

// [RequireComponent(typeof(EnemyBaseStateMachine))]
// [RequireComponent(typeof(Rigidbody))]
// public class EnemyMonoDetection : MonoBehaviour
// {
//     [Tooltip("Collider used to detect when the player enters chase range")]
//     public Collider detectionZone;

//     [Tooltip("Collider used to detect when the player enters attack range")]
//     public Collider attackZone;

//     [SerializeField] private EnemyBaseStateMachine machine;

//     void Awake()
//     {
//         machine = GetComponent<EnemyBaseStateMachine>();

//         // ensure triggers work: rigidbody must be on the same GameObject
//         Rigidbody rb = GetComponent<Rigidbody>();
//         rb.isKinematic = true;

//         // wire up the machine’s references so states can read them
//         machine.DetectionZone = detectionZone;
//         machine.AttackZone = attackZone;
//     }

//     void Update()
//     {
//         // call into the state machine each frame
//         machine.CurrentState?.Tick();
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (!other.CompareTag("Player"))
//             return;

//         // Player entered *some* trigger zone on this object.
//         // Forward it to the machine so whichever state is active can react.
//         Debug.Log($"Player entered {other.name} trigger zone.");
//         machine.OnTriggerEvent(other, true);
//     }

//     void OnTriggerExit(Collider other)
//     {
//         if (!other.CompareTag("Player"))
//             return;

//         // Player exited *some* trigger zone on this object.
//         Debug.Log($"Player exited {other.name} trigger zone.");
//         machine.OnTriggerEvent(other, false);
//     }
// }
