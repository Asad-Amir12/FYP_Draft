using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyReferenceHolder : MonoBehaviour
{
    public static CowboyReferenceHolder Instance { get; private set; }
    [SerializeField] private Transform cowboyTransform;
    public Transform CowboyTransform => cowboyTransform;
    public
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
