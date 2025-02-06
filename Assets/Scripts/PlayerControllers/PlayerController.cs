using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed;
    
    [SerializeField] CharacterController characterController;
    [SerializeField] Animator animator;

    [Header("Model Parametrs")]
    [SerializeField] Transform model;
    [SerializeField] float speedRotation;
    
    private Vector3 _direction;
    private Quaternion _rotation;
    private Joystick _joystick;

    private void Start()
    {
        _joystick = GameObject.FindGameObjectWithTag("MobileController").GetComponent<Joystick>();

        if (PlayerPrefs.HasKey("ProgressSpeed"))
            speed = PlayerPrefs.GetFloat("ProgressSpeed");
    }


    private void Update()
    {
        DesctopPlayerController();
    }


    private void DesctopPlayerController()
    {
        _direction.x = -Input.GetAxisRaw("Horizontal");
        _direction.z = -Input.GetAxisRaw("Vertical");

        _direction = _direction.normalized;
        characterController.Move(_direction * speed * Time.deltaTime);

        RotateModel();
        //Debug.Log(_direction);
    }

    private void MobilePlayerController()
    {
        _direction.x = -_joystick.GetDirection().x;
        _direction.z = -_joystick.GetDirection().y;

        _direction = _direction.normalized;
        characterController.Move(_direction * speed * Time.deltaTime);

        RotateModel();
    }

    private void RotateModel()
    {
        if (_direction != Vector3.zero)
        {
            _rotation = Quaternion.LookRotation(_direction);
            animator.SetBool("IsStep", true);
        }
        else
        {
            animator.SetBool("IsStep", false);
        }

        model.rotation = Quaternion.Lerp(model.rotation, _rotation, speedRotation * Time.deltaTime);
    }
}
