using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject onScreenControllers;
    void Start()
    {
#if UNITY_ANDROID
        onScreenControllers.SetActive(true);
#else
        onScreenControllers.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
