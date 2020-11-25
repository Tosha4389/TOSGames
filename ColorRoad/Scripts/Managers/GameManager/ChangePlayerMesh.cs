using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerMesh : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] MeshFilter[] meshFilter;

    MeshFilter playerMesh;

    private void Awake()
    {
        playerMesh = player.GetComponent<MeshFilter>();
    }

    public void ChangeMesh(int mesh)
    {
        playerMesh.mesh = meshFilter[mesh].mesh;
    }


}
