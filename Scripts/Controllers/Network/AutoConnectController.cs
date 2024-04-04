/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 04/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using Mirror;
using UnityEngine;

namespace Network
{
    public class AutoConnectController : MonoBehaviour
    {
        private bool _joined = false;

        private void Start()
        {
            Join();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!_joined) Join();
                else Leave();
            }
        }

        private void Join()
        {
            try
            {
                NetworkManager.singleton.StartHost();
            }
            catch
            {
                NetworkManager.singleton.StartClient();
            }

            _joined = true;
        }

        private void Leave()
        {
            try
            {
                NetworkManager.singleton.StopHost();
            }
            catch
            {
                NetworkManager.singleton.StopClient();
            }

            _joined = false;
        }
    }
}