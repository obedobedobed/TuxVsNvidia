using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Buff
{
    [SerializeField] private float buffTime = 1f;
    [SerializeField] private float healthMod = 1;
    [SerializeField] private float speedMod = 1f;
    [SerializeField] private float bulletsMod = 1;

    private GameObject player;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Use()
    {
        player.GetComponent<PlayerController>().TakeBuff
        (
            buffTime, healthMod, speedMod, bulletsMod
        );
    }
}