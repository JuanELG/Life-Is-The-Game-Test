using System;
using Managers;
using UI;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private CharacterController characterController;

        [Header("PLAYER OPTIONS")]
        [SerializeField] private float walkSpeed = 6.0f;
        [SerializeField] private float runSpeed = 10.0f;
        [SerializeField] private float jumpSpeed = 8.0f;
        [SerializeField] private float gravity = 20.0f;

        [Header("CAMERA OPTIONS")]
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float mouseHorizontal = 3.0f;
        [SerializeField] private float mouseVertical = 2.0f;

        [SerializeField] private float minRotation = -65.0f;
        [SerializeField] private float maxRotation = 60.0f;
        private float _horizontalMouse, _verticalMouse;

        [Header("Reference")] 
        [SerializeField] private Transform weaponLocation;
    
        private Vector3 move = Vector3.zero;
        private bool _canPickUpWeapon = true;
        private string _nearbyWeaponName;
    
        void Start()
        {
            characterController = GetComponent<CharacterController>();

            Cursor.lockState = CursorLockMode.Locked; 
        
        }

        void Update()
        {
            RotateCamera();
            Move();
            
            if (Input.GetKeyDown(KeyCode.E) && _canPickUpWeapon)
            {
                if(_nearbyWeaponName == null)
                    return;
                
                PickUpWeapon();
            }
        }

        private void Move()
        {
            if (characterController.isGrounded)
            {
                move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

                if (Input.GetKey(KeyCode.LeftShift))
                    move = transform.TransformDirection(move) * runSpeed;
                else
                    move = transform.TransformDirection(move) * walkSpeed;

                if (Input.GetKey(KeyCode.Space))

                    move.y = jumpSpeed;
            }
            move.y -= gravity * Time.deltaTime;

            characterController.Move(move * Time.deltaTime);
        }

        private void RotateCamera()
        {
            _horizontalMouse = mouseHorizontal * Input.GetAxis("Mouse X");
            _verticalMouse += mouseVertical * Input.GetAxis("Mouse Y");

            _verticalMouse = Mathf.Clamp(_verticalMouse, minRotation, maxRotation);
        

            playerCamera.transform.localEulerAngles = new Vector3(-_verticalMouse, 0, 0);
            weaponLocation.localEulerAngles = new Vector3(-_verticalMouse, 0, 0);

            transform.Rotate(0, _horizontalMouse, 0);
        }

        private void PickUpWeapon()
        {
            if (GameManager.Instance.CheckSelectedWeapon() != null)
            {
                GameManager.Instance.DestroyCurrentPlayerWeapon();
            }
                
            GameObject myNewWeapon = GameManager.Instance.SetWeaponFromList(_nearbyWeaponName);
            if (myNewWeapon != null)
                myNewWeapon.transform.SetParent(weaponLocation, false);
            else
                Debug.LogError("THE WEAPON DOES NOT EXIST IN THE GAME MANAGER");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gun"))
            {
                GameUI.Instance.DisplayPickUpTutorial();
                _canPickUpWeapon = true;
                _nearbyWeaponName = other.name;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Gun"))
            {
                GameUI.Instance.HidePickUpTutorial();
                _canPickUpWeapon = false;
                _nearbyWeaponName = null;
            }
        }
    }
}
