using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;

namespace Pun2Task
{
    public static class Pun2TaskNetwork
    {
        public static async UniTask ConnectUsingSettingsAsync(CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync().AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync());

            PhotonNetwork.ConnectUsingSettings();

            var (winIndex, _, disconnectCause) = await task.WithCancellation(token);

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }


        public class ConnectionFailedException : Pun2TaskException
        {
            public DisconnectCause DisconnectCause { get; }

            public ConnectionFailedException(DisconnectCause disconnectCause)
            {
                DisconnectCause = disconnectCause;
            }
        }

        public class Pun2TaskException : Exception
        {
        }
    }
}