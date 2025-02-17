using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

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
        Vector3 desktopDirection = GetDesktopInput();
        Vector3 mobileDirection = GetMobileInput();

        _direction = desktopDirection + mobileDirection; // Объединяем два ввода

        if (_direction.magnitude > 1f) // Ограничиваем скорость нормализацией
            _direction.Normalize();

        characterController.Move(_direction * speed * Time.deltaTime);
        RotateModel();
    }

    private Vector3 GetDesktopInput()
    {
        return new Vector3(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
    }

    private Vector3 GetMobileInput()
    {
        return new Vector3(-_joystick.GetDirection().x, 0, -_joystick.GetDirection().y);
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
