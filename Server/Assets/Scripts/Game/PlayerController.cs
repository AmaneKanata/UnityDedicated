using Framework.Network;
using Protocol;
using System.Buffers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 300.0f;
    private Rigidbody2D rb;
    private bool isGrounded = false;

    private Client Owner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetOwner( Client owner )
    {
        Owner = owner;
        Owner.packetHandler.AddHandler(Handle_C_PLAYER_INPUT);
    }

    public void Handle_C_PLAYER_INPUT(C_PLAYER_INPUT pkt)
    {
        rb.velocity = new Vector2(pkt.Hori * speed, rb.velocity.y);

        if (pkt.Jump && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;  // 임시로 점프할 때마다 isGrounded를 false로 설정
        }
    }

    void OnCollisionEnter2D( Collision2D other )
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
