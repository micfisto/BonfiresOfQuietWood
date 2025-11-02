using UnityEngine;

namespace Core
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected float healthPoint;
        [SerializeField] protected float speed;
        [SerializeField] protected float damagePoint;
        
        [SerializeField] protected float stamina;
        [SerializeField] protected float staminaMax = 100f;
        [SerializeField] protected float staminaDrainPerSecond;
        [SerializeField] protected float staminaRestoredPerSecond;
        
        //rigidbody = rb
        protected Rigidbody2D Rigidbody;
        protected Animator Animator;


        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }
        
        public virtual void TakeDamage(float amount)
        {
            healthPoint -= amount;
            if (healthPoint <= 0)
                Die();
        }

        protected virtual void Die()
        {
            Debug.Log($"{gameObject.name} погиб.");
            Destroy(gameObject);
        }
        
        protected abstract void Move(Vector2 inputVector, float moveSpeed);
    }
}
