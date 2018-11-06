using System;
using UnityEngine;
namespace Tank
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerTank : Tank
    {

        private GameObject cameraObject;

        private ShootAction EventShootAction;

        private DeadAction EventDeadAction;

        private Rigidbody rb;

        private Vector3 inputVec;

        private Vector3 newVelocity;

        private float rotateSpeed = 50.0f;

        private void Start()
        {
            this.GetComponentInChildren<MeshRenderer>().material.color = TankColor;
            AddModule(MODULES.MODULE_1);
            this.hp = 200.0f;
            this.CollisionAttack = 110.0f;
            rb = GetComponent<Rigidbody>();

        }

        private void FixedUpdate()
        {
            PlayerTankMoving();
            PlayerTankShoting();
            ObservingFoHP();
        }

        #region PlayerHaracterisicObservingRegion
        private void ObservingFoHP()
        {
            if (hp < 0)
            {
                EventDeadAction();
            }
        }
        #endregion

        #region PlayerMoveningRegion
        private void PlayerTankMoving()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            cameraObject = GameObject.FindWithTag("MainCamera");

            var move = UpdateMovement(-moveHorizontal, -moveVertical);
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, move);
            Quaternion deltaRotation = Quaternion.Euler(TankSpeed * movement * Time.deltaTime);
            rb.AddForce(movement * TankSpeed);

        }

        void CameraRelativeMovement(float h, float v)
        {

            Transform cameraTransform = cameraObject.transform;

            Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;

            Vector3 right = new Vector3(forward.z, 0, -forward.x);

            inputVec = h * right + v * forward;
        }

        void RotateTowardsMovementDir()
        {
            if (inputVec != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputVec), Time.deltaTime * rotateSpeed);
            }
        }

        float UpdateMovement(float h, float v)
        {

            CameraRelativeMovement(h, v);

            Vector3 motion = inputVec;

            if (motion.magnitude > 1)
            {
                motion.Normalize();
            }

            newVelocity = motion * TankSpeed;

            RotateTowardsMovementDir();

            newVelocity.y = rb.velocity.y;

            rb.velocity = newVelocity;

            return inputVec.magnitude;
        }
        #endregion

        #region ShootActionRegion
        private void PlayerTankShoting()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                EventShootAction();
            }
        }

        private void Shoot()
        {
            MakeShoot();
        }

        #endregion



        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Enemy")
            {
                TakeDamage(CollisionAttack);
            }
        }

        private void OnEnable()
        {
            EventShootAction += Shoot;
            EventDeadAction += ReSpawnTank;
        }

        private void OnDisable()
        {
            EventShootAction -= Shoot;
            EventDeadAction -= ReSpawnTank;
        }
    }
}
