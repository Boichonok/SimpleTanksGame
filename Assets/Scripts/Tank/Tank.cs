using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tank
{
    public abstract class Tank : MonoBehaviour
    {
        public delegate void ShootAction();
        public delegate void DeadAction();

        [SerializeField]
        private IGunModule gunModule;
        public IGunModule GunModule { get { return gunModule; } set { gunModule = value; } }

        [SerializeField]
        private Color tankColor;
        public Color TankColor { get { return tankColor; } set { tankColor = value; } }

        [SerializeField]
        private float tankSpeed;
        public float TankSpeed { get { return tankSpeed; } set { tankSpeed = value; } }

        [SerializeField]
        private float collisionAttack;
        public float CollisionAttack { get { return collisionAttack; } set { collisionAttack = value; } }

        [SerializeField]
        protected float hp;

        [SerializeField]
        private Transform spawnModule = null;

       
        private Transform spawnPoint = null;
        public Transform SpawnPoint { get; set; }



        protected enum MODULES
        {
            MODULE_1,
            MODULE_2,
            MODULE_3
        }

        #region Shooting
        public void TakeDamage(float amount)
        {
            if (hp > 200.0f)
            {
                hp = 200.0f;
            }

            this.hp -= amount;

           
        }


        protected void MakeShoot()
        {
                var shell = Instantiate(GunModule.Shell, GunModule.SpawnShell);
                shell.GetComponent<Rigidbody>().velocity = GunModule.ShootPower * GunModule.SpawnShell.forward;
        }
        #endregion

        #region Module
        protected void AddModule(MODULES gunModule)
        {
            switch (gunModule)
            {
                case MODULES.MODULE_1:
                    {
                        gameObject.AddComponent<GunModule1>();
                        GunModule = GetComponent<GunModule1>();
                        
                    }
                    break;
                case MODULES.MODULE_2:
                    {
                        gameObject.AddComponent<GunModule2>();
                        GunModule = GetComponent<GunModule2>();
                    }
                    break;
                case MODULES.MODULE_3:
                    {
                        gameObject.AddComponent<GunModule3>();
                        GunModule = GetComponent<GunModule3>();
                    }
                    break;
            }
            GunModule.SpawnModulePos = spawnModule;

        }
        #endregion

        #region Spawn_Respawn_Tank

        protected void ReSpawnTank()
        {
            StartCoroutine(ResSpawnTank());
        }

        IEnumerator ResSpawnTank()
        {
            yield return new WaitForSecondsRealtime(1);
            this.transform.position = this.transform.parent.transform.position;
            this.hp = 200.0f;
        }
        #endregion

      


    }
}
