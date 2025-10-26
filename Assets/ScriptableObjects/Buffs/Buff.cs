using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "Scriptable Objects/Buff")]
public class Buff : ScriptableObject
{
    public Sprite buffSprite;
    public string buffName;
    public string buffDescription;
    public int buffCost;
    public BuffCodeName buffCodeName;
}

public enum BuffCodeName
{
    BulMod, FreSped, KerChg, MIT, Pcman, HapPeng, NvDrv
}