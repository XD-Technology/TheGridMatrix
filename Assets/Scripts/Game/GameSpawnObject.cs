using UnityEngine;
using UnityEngine.Events;

namespace TheGridMatrix
{
    public abstract class GameSpawnObject : MonoBehaviour
    {
        private UnityAction onDestroySpawn;
        [SerializeField] private float age;

        public UnityAction OnDestroySpawn { get => onDestroySpawn; set => onDestroySpawn = value; }
        public float Age { get => age; internal set { age = value; if (age > 10) Destroy(gameObject); } }

        private void OnDestroy()
        {
            if (OnDestroySpawn != null) OnDestroySpawn.Invoke();
        }

        public virtual void OnCollideSpawn()
        {

        }
    }
}
