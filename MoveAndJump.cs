using UnityEngine;
using UnityEngine.EventSystems;

public class MoveAndJump : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float accelerationSpeed;
    private float currentSpeed;
    [Header("�������")]
    [SerializeField] private float stamina;
    [SerializeField] private float staminaConsumption;
    [SerializeField] private float staminaRegenerate;
    
    [Header("������")]
    [SerializeField] private float gravity;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float timeJump;
    private Transform tr;
    private Rigidbody rb;
    private CharacterController controller;
    private float height;
    private bool isJump;
    private bool isGround;
    
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
      if (height <= 1 && isJump == false)
        {
            isGround = true;
        }else
        {
            isGround = false;
        }
    }
    private float timerun;
   
    void Update()
    {
        
            // �������� ���� �� ������������
            float horizontalInput = Input.GetAxis("Horizontal");
             float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = Vector3.zero;
      
        
        if (Input.GetKey(KeyCode.LeftShift) && stamina >0 && timerun >= 1 )
        {
            if (horizontalInput != 0 || verticalInput != 0)
            {
                currentSpeed += 0 + accelerationSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed = 0;
            }
            if (currentSpeed < runSpeed)
            {
                movement = new Vector3(horizontalInput, 0f, verticalInput) * currentSpeed;
            }
            else
            {
                currentSpeed = runSpeed;
                movement = new Vector3(horizontalInput, 0f, verticalInput) * runSpeed;
            }

            if (horizontalInput != 0 || verticalInput != 0)
            {
                stamina -= staminaConsumption * Time.deltaTime;
            }
         if (stamina <= 0.3)
            {
                timerun = 0;
            }
            
        }
        else
        {

            if (horizontalInput != 0 || verticalInput !=0)
            {
                currentSpeed += 0 + accelerationSpeed * Time.deltaTime;
            }
            else{
                currentSpeed = 0;
            }
           
            // ��������� ������ �������� �� ������ ����� ������������
            if (currentSpeed < walkSpeed)
            {
                movement = new Vector3(horizontalInput, 0f, verticalInput) * currentSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
                movement = new Vector3(horizontalInput, 0f, verticalInput) * walkSpeed;
                
            }

            if (stamina <= 10)
            {
                stamina += staminaRegenerate * Time.deltaTime;
                
            }
            //timerun = 0;
            timerun += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            timerun = 0;
        }
       
       


        // ����������� ������ �������� � ������� ������������
        movement = transform.TransformDirection(movement);
        // float time = Mathf.Sqrt(-2 * transform.position.y / gravity);
        // ��������� ����������




        #region ������
        if (Input.GetKeyDown(KeyCode.Space)) // ���� ����� ����� ������
        {
            
            if (isGround)
            {
                isJump = true;
            }
            // ��������� ���� ������ � ������������ ���
        }
        if (timeJump <= 0)
        {
            isJump = false;
            timeJump= 0.5f;
            height = 0;
        }
        if (isJump)
        {
            movement.y = 0 + jumpSpeed* timeJump + ((1/2)*gravity*(timeJump*timeJump));
            timeJump -= Time.deltaTime;
        }
            else
            {
                height += (2 * (gravity / 2) / gravity);
                if (!controller.isGrounded)
                {
                    movement.y -= gravity * (2 * height / gravity);
                }
                else
                {
                    height = 0;

                }
                 
            }
        #endregion
        // ����������� ���������
        controller.Move(movement * Time.deltaTime);
        

    }

    

    

}
