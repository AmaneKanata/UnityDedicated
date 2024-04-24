using Framework.Network;
using Protocol;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 300.0f;
    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;  // 임시로 점프할 때마다 isGrounded를 false로 설정
        }

        C_PLAYER_INPUT pkt = new C_PLAYER_INPUT
        {
            Hori = move,
            Jump = Input.GetButtonDown("Jump")
        };
        NetworkManager.Instance.Client.Send(PacketManager.MakeSendBuffer(pkt));
    }

    void OnCollisionEnter2D( Collision2D other )
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
