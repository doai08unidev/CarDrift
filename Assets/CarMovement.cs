
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float torqueForce = -200f; // Lực xoay xe (lực mômen xoắn)
    private float speed = 2f; //Tốc độ di chuyển
    public Rigidbody2D car;
    
    private float driftFactorSticky = 0.8f; //Hệ số hãm xe để xe không bị văng đột ngột ra khỏi cua ( đột ngột = Speed đang lớn, rồi bất ngờ giảm nhanh về 0)
    private float driftFactorSlippy = 1f; //Hệ số drift trôi dạt
    //  => Slippy > Sticky (Để khống chế lực văng đột ngột khỏi cua, thì driftFactorSlippy phải đột ngột lớn hơn nhiều so với driftFactorSticky)
    private float maxStickyVelocity = .2f; //vị trí mà xe đột ngột văng ra khỏi cua -> Vận tốc đạt tối đa

    void FixedUpdate(){
            car = GetComponent<Rigidbody2D>();

            float driftFactor = driftFactorSticky; //Hệ số drift ban đầu 
            if(RightVelocity().magnitude >maxStickyVelocity){    
                driftFactor = driftFactorSlippy; 
            }
            car.velocity = ForwardVelocity() + RightVelocity() * driftFactor; //vận tốc của xe vào cua
            if(Input.GetKey(KeyCode.U)){     //Nhấn U trên bàn phím để xe chạy
                 car.AddForce(transform.up * speed); //Xe di chuyển hướng về trước
            }
           
            car.angularVelocity = Input.GetAxis("Horizontal") * torqueForce; //vận tốc xoay của xe

    }

    Vector2 ForwardVelocity(){
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up); //Độ lớn vận tốc hướng trước khi vào cua
    }

    Vector2 RightVelocity(){
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right); //Độ lớn vận tốc hướng phải khi vào cua
    }
    // => Khi vào cua, vận tốc xe = v xe hướng trước + v xe hướng phải * hệ số drift
    //Dot -> Dot Products (tích vô hướng của 2 vector)
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarMovement : MonoBehaviour
// {
//     public float MaxSpeed;
//     public float acc;
//     public float steering;
//     public Rigidbody2D rb;
//     float X;
//     float Y;
//     // Start is called before the first frame update
//     void Start()
//     {
//         Y = 1;
//         rb = GetComponent<Rigidbody2D>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         X = Input.GetAxis("Horizontal");
//         Vector2 speed = transform.up * (Y*acc);
//         if(Input.GetKey(KeyCode.U)){
//             rb.AddForce(speed);
//         }
        
//         float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
//         if(acc>0){
//             if(direction > 0){
//                 rb.rotation -= X * steering * (rb.velocity.magnitude/ MaxSpeed);
//             }else{
//                 rb.rotation += X * steering * (rb.velocity.magnitude/ MaxSpeed);
//             }
//         }
//         float driftForce = Vector2.Dot(rb.velocity,rb.GetRelativeVector(Vector2.left)) * 2.0f;
//         Vector2 relativeForce = Vector2.right * driftForce;
//         rb.AddForce(rb.GetRelativeVector(relativeForce));
//         if(rb.velocity.magnitude> MaxSpeed){
//             rb.velocity = rb.velocity.normalized * MaxSpeed;
//         }
//         Debug.DrawLine(rb.position, rb.GetRelativeVector(relativeForce), Color.green);
//     }
// }


//   if((transform.rotation.eulerAngles.z > 140 && transform.rotation.eulerAngles.z < 180)|| transform.rotation.eulerAngles.z >190){
//         transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
//     }