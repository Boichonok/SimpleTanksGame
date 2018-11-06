using System;
using UnityEngine;
namespace Tank
{
    [RequireComponent(typeof(Rigidbody))]
    public class Shell : MonoBehaviour, IShell
    {

        [SerializeField]
        public float AttackValue { get; set; }

        [SerializeField]
        public float LifeTime { get; set; }

        private float age;
        private bool isLive = true;

        private void Start()
        {
            AttackValue = 10.0f;
            LifeTime = 5.0f;
            this.GetComponent<Renderer>().material.color = Color.yellow;
        }

        private void Update()
        {
            ShellController();
        }
        public void ShellController()
        {
            age += Time.deltaTime;
            if (age > LifeTime)
            {
                Destroy(this.gameObject);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (!isLive)
                return;
            isLive = false;
            Destroy(this.gameObject);
            var target = collision.gameObject.GetComponent<Tank>();
            if (target != null)
            {
                target.TakeDamage(AttackValue);
            }
        }
    }
}
