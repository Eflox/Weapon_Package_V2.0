/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 04/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        public float speed = 5f;

        private void Update()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);

            if (moveHorizontal != 0 && moveVertical != 0)
            {
                movement /= Mathf.Sqrt(2);
            }

            transform.position += movement * speed * Time.deltaTime;
        }
    }
}