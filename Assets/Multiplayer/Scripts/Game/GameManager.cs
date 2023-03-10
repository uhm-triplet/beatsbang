using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using BeatsBang.Core.BeatsBang.Manager;

namespace Game
{
    public class GameManager : NetworkBehaviour
    {
        void Start()
        {
            NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;
            if (RelayManager.Instance.IsHost)
            {
                NetworkManager.Singleton.ConnectionApprovalCallback = ConnectionApproval;
                (byte[] allocationId, byte[] key, byte[] connectionData, string ip, int port) = RelayManager.Instance.GetHostConnectionInfo();
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(ip, (ushort)port, allocationId, key, connectionData, true);
                NetworkManager.Singleton.StartHost();
            }
            else
            {
                (byte[] allocationId, byte[] key, byte[] connectionData, byte[] hostConnectionData, string ip, int port) = RelayManager.Instance.GetClientConnectionInfo();
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(ip, (ushort)port, allocationId, key, connectionData, hostConnectionData, true);
                NetworkManager.Singleton.StartClient();

            }
        }




        private void ConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            response.Approved = true;
            response.CreatePlayerObject = false;
            response.Pending = false;
        }

    }

}