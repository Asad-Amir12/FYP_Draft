using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private Rigidbody rb;
    private Vector2 moveDirection;
    public float walkSpeed, jogSpeed, backJogSpeed, walkBackSpeed, rotateSpeed ;

    public InputActionReference move;
    public InputActionReference run;
    public bool _isRunning = false;
    public bool _isWalking = false;
    void Awake()
    {
        //run.action.performed += RunStart;
        
    }

    void OnEnable(){
        run.action.Enable();
    }

    void OnDisable(){
        run.action.Disable();
    }

    void OnDestroy(){
        run.action.Disable();
    }
    private void Update(){
        moveDirection = move.action.ReadValue<Vector2>();
       
    }
    
    private void FixedUpdate(){
      
        SetTriggerAnimator(moveDirection);
        
    }

    private void SetTriggerAnimator(Vector2 MoveDirection){
        if(moveDirection == Vector2.zero){
            ResetAnimatorTriggers();
            animator.SetTrigger("Idle");
           _isWalking = false;
           _isRunning = false;
          
        }
        if(moveDirection.y > 0){
            ResetAnimatorTriggers();
            animator.SetTrigger("Walk");
            _isWalking = true;
        }
        if(moveDirection.y < 0){
            _isWalking= true;
            ResetAnimatorTriggers();
            animator.SetTrigger("Walk");
            
            
        }

        
    }

    private void RunStart(InputAction.CallbackContext context){

        if (_isWalking && run.action.ReadValue<float>() == 1)
        {
            animator.SetTrigger("Jog");
            _isRunning = true;
            Debug.Log("run triggered");
        }

    }


    private void ResetAnimatorTriggers(){
        if(_isRunning == false){
            animator.ResetTrigger("Jog");
            animator.ResetTrigger("JogBack");
           
        }
        
        animator.ResetTrigger("Idle");
        
        animator.ResetTrigger("Walk");
    }
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class player : MonoBehaviour
// {
//     public Animator playerAnim;
// 	public Rigidbody playerRigid;
// 	public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
// 	public bool walking;
// 	public Transform playerTrans;
	
	
// 	void FixedUpdate(){
// 		if(Input.GetKey(KeyCode.W)){
// 			playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
// 		}
// 		if(Input.GetKey(KeyCode.S)){
// 			playerRigid.velocity = -transform.forward * wb_speed * Time.deltaTime;
// 		}
// 	}
// 	void Update(){
// 		if(Input.GetKeyDown(KeyCode.W)){
// 			playerAnim.SetTrigger("walk");
// 			playerAnim.ResetTrigger("idle");
// 			walking = true;
// 			//steps1.SetActive(true);
// 		}
// 		if(Input.GetKeyUp(KeyCode.W)){
// 			playerAnim.ResetTrigger("walk");
// 			playerAnim.SetTrigger("idle");
// 			walking = false;
// 			//steps1.SetActive(false);
// 		}
// 		if(Input.GetKeyDown(KeyCode.S)){
// 			playerAnim.SetTrigger("walkback");
// 			playerAnim.ResetTrigger("idle");
// 			//steps1.SetActive(true);
// 		}
// 		if(Input.GetKeyUp(KeyCode.S)){
// 			playerAnim.ResetTrigger("walkback");
// 			playerAnim.SetTrigger("idle");
// 			//steps1.SetActive(false);
// 		}
// 		if(Input.GetKey(KeyCode.A)){
// 			playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
// 		}
// 		if(Input.GetKey(KeyCode.D)){
// 			playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
// 		}
// 		if(walking == true){				
// 			if(Input.GetKeyDown(KeyCode.LeftShift)){
// 				//steps1.SetActive(false);
// 				//steps2.SetActive(true);
// 				w_speed = w_speed + rn_speed;
// 				playerAnim.SetTrigger("run");
// 				playerAnim.ResetTrigger("walk");
// 			}
// 			if(Input.GetKeyUp(KeyCode.LeftShift)){
// 				//steps1.SetActive(true);
// 				//steps2.SetActive(false);
// 				w_speed = olw_speed;
// 				playerAnim.ResetTrigger("run");
// 				playerAnim.SetTrigger("walk");
// 			}
// 		}
// 	}
// }

// Camera Script:
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class camLookat : MonoBehaviour
// {
//     public Transform player, cameraTrans;
	
// 	void Update(){
// 		cameraTrans.LookAt(player);
// 	}
// }