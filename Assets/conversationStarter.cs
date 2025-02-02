using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using System;

public class conversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myconvo;

    private void OnTriggerStay(Collider other){
        Debug.Log(other.tag);
        if(other.CompareTag("Player"))
        {
            Debug.Log("Press F");
            //if(Input.GetKeyDown(KeyCode.F))
           // {
                ConversationManager.Instance.StartConversation(myconvo);
           // }
        }
    }
}
