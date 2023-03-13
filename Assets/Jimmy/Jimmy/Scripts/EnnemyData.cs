using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu]
    public class EnnemyData : ScriptableObject
    {
        public string type;
        public int health;
        public float moveSpeed;
        public float rangeToAttack;
        public int delayToAttack;
    }
}