using UnityEngine;

[CreateAssetMenu(fileName = "new ADInfo", menuName = "AD/Create AD Info")]
public class ADInfo : ScriptableObject
{
    [SerializeField] private int _requireRaces;
    [SerializeField] private int _requireRestarts;

    public int RequireRaces => _requireRaces;
    public int RequireRestarts => _requireRestarts;
}
