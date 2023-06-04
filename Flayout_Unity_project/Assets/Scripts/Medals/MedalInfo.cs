using UnityEngine;

[CreateAssetMenu(fileName = "new Medal", menuName = "Medal/Create new Medal")]
public class MedalInfo : ScriptableObject
{
    [SerializeField] private int _score;
    [SerializeField] private int _reward;

    public int Score => _score;
    public int Reward => _reward;
}
