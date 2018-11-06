using UnityEngine;
using System.Collections;
namespace Tank
{
    public class GunModule3 : MonoBehaviour,IGunModule
    {
        [SerializeField]
        private float attackValue;
        public float AttackValue { get { return attackValue; } set { attackValue = value; } }

        [SerializeField]
        private GameObject shell;
        public GameObject Shell { get { return shell; } set { shell = value; } }

        [SerializeField]
        private Transform spawnShell;
        public Transform SpawnShell { get { return spawnShell; } set { spawnShell = value; } }

        [SerializeField]
        private GameObject tower;
        public GameObject Tower { get { return tower; } set { tower = value; } }

        [SerializeField]
        private GameObject gun;
        public GameObject Gun { get { return gun; } set { gun = value; } }

        [SerializeField]
        private float shootPower;
        public float ShootPower { get { return shootPower; } set { shootPower = value; } }

        public Transform SpawnModulePos { get; set; }

        private void Start()
        {
            InitModule();
        }

        public void InitModule()
        {
            ShootPower = 60.0f;
            AttackValue = 10.0f;

            var modulePrefub = Resources.Load("Tower") as GameObject;
            var moduleGo = Instantiate<GameObject>(modulePrefub, SpawnModulePos.transform,false);

            SpawnShell = moduleGo.transform.FindChild("SpawnShell");
            Shell = Resources.Load("Shell") as GameObject;
            Tower = moduleGo;
            Gun = moduleGo.transform.FindChild("Gun").gameObject;

            Shell.GetComponent<Shell>().AttackValue += this.AttackValue;
            tower.GetComponent<Renderer>().material.color = Color.green;
            gun.GetComponent<Renderer>().material.color = Color.green;
           
        }
    }
}
