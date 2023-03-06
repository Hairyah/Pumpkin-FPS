using UnityEngine;

namespace Assets.Scripts
{
    public class GhostData : ScriptableObject
    {
        int health = 150;
        float moveSpeed = 3.5f;
        float rangeToAttack = 5;
        int delayToAttack = 1;
    }
}