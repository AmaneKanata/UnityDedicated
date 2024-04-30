using Framework.Network;
using Protocol;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 300.0f;
    private Rigidbody2D rb;
    private bool isGrounded = false;

    private Client Owner;

    public Action<string> OnItemOccupy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        if (Owner != null)
            Owner.packetHandler.RemoveHandler(Handle_C_PLAYER_INPUT);
    }

    public void SetOwner( Client owner )
    {
        Owner = owner;
        Owner.packetHandler.AddHandler(Handle_C_PLAYER_INPUT);
    }

    public void Handle_C_PLAYER_INPUT( C_PLAYER_INPUT pkt )
    {
        rb.velocity = new Vector2(pkt.Hori * speed, rb.velocity.y);

        if (pkt.Jump && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;  // �ӽ÷� ������ ������ isGrounded�� false�� ����
        }
    }

    void OnCollisionEnter2D( Collision2D other )
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            OnItemOccupy?.Invoke(Owner.Id);
        }
    }
}
