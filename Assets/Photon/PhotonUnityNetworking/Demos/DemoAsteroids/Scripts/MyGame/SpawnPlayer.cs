using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject prefabGo;
    void Start()
    {
        
        PhotonNetwork.Instantiate(prefabGo.name, transform.position, Quaternion.identity);
    }

}
