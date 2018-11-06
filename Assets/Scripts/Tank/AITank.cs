
using UnityEngine;
using System.Collections;
namespace Tank
{
    //RequireComponent(typeof(Rigidbody))]
    public class AITank : Tank
    {
        [SerializeField]
        private GameObject[] wayPoints;

        private GameObject playerTank;

        [SerializeField]
        private float shotDistance = 15.0f;
        private float delaySHoting = 20.0f;

        private MODULES[] tankModules = { MODULES.MODULE_1, MODULES.MODULE_2, MODULES.MODULE_3 };


        private ShootAction EventShootAITankAction;
        private DeadAction EventDeadAction;

        private void Start()
        {
            this.GetComponentInChildren<MeshRenderer>().material.color = TankColor;
            this.AddModule(tankModules[Random.Range(0, 2)]);
            this.hp = 200.0f;
            this.playerTank = FindObjectOfType<PlayerTank>().gameObject;
            this.CollisionAttack = 110.0f;
            wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        }

        private void FixedUpdate()
        {
            ObservingAITankStates();
            ObservingFoHP();
        }

        #region AI
        private void ObservingFoHP()
        {
            if (hp < 0)
            {
                EventDeadAction();
            }
        }

        private void ObservingAITankStates()
        {
            var direction = this.playerTank.transform.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            if (Vector3.Distance(playerTank.transform.position, this.transform.position) < shotDistance * 2.0f && angle < 30)
            {
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                if (direction.magnitude > shotDistance)
                {
                    this.transform.Translate(0, 0, 0.15f);
                }
                else
                {
                    Debug.Log("AIATTACK!");
                    EventShootAITankAction();

                }
            }
            else
            {
                StartCoroutine(Wait(7));

                RideToPoint(wayPoints[Random.RandomRange(0, wayPoints.Length - 1)].transform.position);

            }
        }

        void RideToPoint(Vector3 pointInFront)
        {
            var directionToPoint = pointInFront - this.transform.position;

            float angleBetweanPoint = Vector3.Angle(directionToPoint, this.transform.forward);
            if (Vector3.Distance(pointInFront, this.transform.position) < 100.0f && angleBetweanPoint < 360)
            {
                directionToPoint.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(directionToPoint), 0.1f);
                if (directionToPoint.magnitude > 5.0f)
                {
                    this.transform.Translate(0, 0, 0.15f);

                }
                else
                {

                }
            }
        }

        IEnumerator Wait(int seconds)
        {
            print(Time.time);
            yield return new WaitForSeconds(seconds);
            print(Time.time);

        }

        private void Shoot()
        {

            delaySHoting -= 1 + Time.fixedDeltaTime;
            if (delaySHoting < 0.0f)
            {
                delaySHoting = 20.0f;
                MakeShoot();
            }
        }
        #endregion

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag.Equals("Wall") || other.gameObject.tag.Equals("Enemy"))
            {
                Debug.Log("AITank coll: " + other.collider.tag);
                RideToPoint(wayPoints[Random.RandomRange(0, wayPoints.Length - 1)].transform.position);
            }
            else if (other.gameObject.tag == "Player")
            {
                TakeDamage(CollisionAttack);
            }
        }

        void OnEnable()
        {
            EventShootAITankAction += Shoot;
            EventDeadAction += ReSpawnTank;
        }

        private void OnDisable()
        {
            EventShootAITankAction -= Shoot;
            EventDeadAction -= ReSpawnTank;
        }


    }


}
