using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;


public class Weapon : MonoBehaviour
{
    public GameObject WeaponGO;
    private bool _isWeaponLaunched = false;
    private PlayerInputActions playerInput;
    private Coroutine _weaponLaunchedCoroutine;
    private CharacterController characterController;

    private GameObject _instance;

    Transform _mainCamera = null;
    float _cameraAngle = 0f;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerInput.Player.Enable();
        //playerInput.Player.Fire.performed += LaunchWeapon;
        _mainCamera = Camera.main.transform;
        _cameraAngle = CameraAngleCalculation();
    }


    private void LaunchWeapon(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!_isWeaponLaunched)
            {
                Vector2 lookingDirection = playerInput.Player.Look.ReadValue<Vector2>();
                lookingDirection = RecalculateMovementVector(lookingDirection);
                Vector3 launchVector = new Vector3(lookingDirection.x, 0, lookingDirection.y);
                if(launchVector.magnitude > 0.5f)
                {
                    _instance = Instantiate(WeaponGO, transform.position + 3 * launchVector
                    , Quaternion.identity);
                }
                else
                {
                    _instance = Instantiate(WeaponGO, transform.position + 3 * transform.forward
                    , Quaternion.identity);
                }
                _isWeaponLaunched = true;
                StartCoroutine(WeaponTimerCoroutine());
            }
        }    
    }

    private IEnumerator WeaponTimerCoroutine()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        _isWeaponLaunched = false;
        if (_instance != null)
        {
            Destroy(_instance);
        }
            
    }


    protected float CameraAngleCalculation()
    {
        //Vector3 cameraYProjected = Vector3.ProjectOnPlane(_mainCamera.up, Vector3.up);
        Vector3 cameraXProjected = Vector3.ProjectOnPlane(_mainCamera.right, Vector3.up);

        return Vector3.SignedAngle(cameraXProjected, Vector3.right, Vector3.up) * Mathf.Deg2Rad;
    }

    protected virtual Vector2 RecalculateMovementVector(Vector2 inputVector)
    {
        Vector2 newVector = new Vector2();
        newVector.x = (inputVector.x * Mathf.Cos(_cameraAngle) - inputVector.y * Mathf.Sin(_cameraAngle));
        newVector.y = (inputVector.x * Mathf.Sin(_cameraAngle) + inputVector.y * Mathf.Cos(_cameraAngle));
        return newVector;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Vector3 forceDirection = collision.gameObject.transform.position - transform.position;
            characterController = collision.gameObject.GetComponent<CharacterController>();
            StartCoroutine(AddForceToPlayerCoroutine(forceDirection));
        }

    }

    private IEnumerator AddForceToPlayerCoroutine(Vector3 impact)
    {
        if (impact.magnitude > 0.2)
        {
            characterController.Move(impact * Time.deltaTime);
        }
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        yield return null;
    }
}
