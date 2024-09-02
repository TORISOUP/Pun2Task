#if PUN_TO_UNITASK_SUPPORT

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace Pun2Task
{
    public static class Pun2TaskNetwork
    {
        #region ServerConnection

        /// <summary>
        /// PhotonNetwork.ConnectUsingSettings
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="InvalidNetworkOperationException">Throws when PhotonNetwork.ConnectUsingSettings returns false.</exception>
        /// <exception cref="ConnectionFailedException">Throw when connection to server fails.</exception>
        public static async UniTask ConnectUsingSettingsAsync(CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync(token));

            var result = PhotonNetwork.ConnectUsingSettings();
            if (!result)
            {
                throw new InvalidNetworkOperationException(nameof(ConnectUsingSettingsAsync) +
                                                           " is not ready to connect.");
            }

            var (winIndex, _, disconnectCause) = await task;

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }

        /// <summary>
        /// PhotonNetwork.ConnectUsingSettings
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="startInOfflineMode"></param>
        /// <param name="token"></param>
        /// <exception cref="InvalidNetworkOperationException">Throws when PhotonNetwork.ConnectUsingSettings returns false.</exception>
        /// <exception cref="ConnectionFailedException">Throw when connection to server fails.</exception>
        public static async UniTask ConnectUsingSettingsAsync(
            AppSettings appSettings,
            bool startInOfflineMode = false,
            CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync(token));

            var result = PhotonNetwork.ConnectUsingSettings(appSettings, startInOfflineMode);
            if (!result)
            {
                throw new InvalidNetworkOperationException(nameof(ConnectUsingSettingsAsync) +
                                                           " is not ready to connect.");
            }

            var (winIndex, _, disconnectCause) = await task;

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterServerAddress"></param>
        /// <param name="port"></param>
        /// <param name="appID"></param>
        /// <param name="token"></param>
        /// <exception cref="InvalidNetworkOperationException">Throws when PhotonNetwork.ConnectToMaster returns false.</exception>
        /// <exception cref="ConnectionFailedException">Throw when connection to server fails.</exception>
        public static async UniTask ConnectToMasterAsync(
            string masterServerAddress,
            int port,
            string appID,
            CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync(token));

            var result = PhotonNetwork.ConnectToMaster(masterServerAddress, port, appID);
            if (!result)
            {
                throw new InvalidNetworkOperationException(nameof(ConnectToMasterAsync) + " is not ready to connect.");
            }

            var (winIndex, _, disconnectCause) = await task;

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }

        /// <summary>
        /// PhotonNetwork.ConnectToBestCloudServer
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="InvalidNetworkOperationException">Throws when PhotonNetwork.ConnectToBestCloudServer returns false.</exception>
        /// <exception cref="ConnectionFailedException">Throw when connection to server fails.</exception>
        public static async UniTask ConnectToBestCloudServerAsync(CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync(token));

            var result = PhotonNetwork.ConnectToBestCloudServer();
            if (!result)
            {
                throw new InvalidNetworkOperationException(nameof(ConnectToBestCloudServerAsync) +
                                                           " is not ready to connect.");
            }

            var (winIndex, _, disconnectCause) = await task;

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }

        /// <summary>
        /// PhotonNetwork.ConnectToRegion
        /// </summary>
        /// <param name="region"></param>
        /// <param name="token"></param>
        /// <exception cref="InvalidNetworkOperationException">Throws when PhotonNetwork.ConnectToRegion returns false.</exception>
        /// <exception cref="ConnectionFailedException">Throw when connection to server fails.</exception>
        public static async UniTask ConnectToRegionAsync(string region, CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync(token));

            var result = PhotonNetwork.ConnectToRegion(region);
            if (!result)
            {
                throw new InvalidNetworkOperationException(nameof(ConnectToRegionAsync) + " is not ready to connect.");
            }

            var (winIndex, _, disconnectCause) = await task;

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }

        /// <summary>
        /// PhotonNetwork.Disconnect
        /// </summary>
        public static async UniTask DisconnectAsync()
        {
            if (PhotonNetwork.NetworkClientState == ClientState.Disconnected) return;
            PhotonNetwork.Disconnect();
            await Pun2TaskCallback.OnDisconnectedAsync();
        }

        /// <summary>
        /// PhotonNetwork.Reconnect
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="InvalidNetworkOperationException">Throws when PhotonNetwork.Reconnect returns false.</exception>
        /// <exception cref="ConnectionFailedException">Throw when connection to server fails.</exception>
        public static async UniTask ReconnectAsync(CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync(token));

            var result = PhotonNetwork.Reconnect();
            if (!result) throw new InvalidNetworkOperationException("It is not ready to reconnect.");

            var (winIndex, _, disconnectCause) = await task;

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }

        #endregion

        #region RoomConnection

        /// <summary>
        /// PhotonNetwork.CreateRoom
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="roomOptions"></param>
        /// <param name="typedLobby"></param>
        /// <param name="expectedUsers"></param>
        /// <param name="token"></param>
        /// <exception cref="InvalidRoomOperationException">Throws when PhotonNetwork.CreateRoom returns false.</exception>
        /// <exception cref="FailedToCreateRoomException">Throw when room creation fails.</exception>
        public static async UniTask CreateRoomAsync(
            string roomName,
            RoomOptions roomOptions = null,
            TypedLobby typedLobby = null,
            string[] expectedUsers = null,
            CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnJoinedRoomAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnCreateRoomFailedAsync(token));

            var valid = PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, expectedUsers);
            if (!valid) throw new InvalidRoomOperationException("It is not ready to create a room.");

            var (winIndex, _, (returnCode, message)) = await task;
            if (winIndex == 0) return;
            throw new FailedToCreateRoomException(returnCode, message);
        }


        /// <summary>
        /// PhotonNetwork.JoinOrCreateRoom
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="roomOptions"></param>
        /// <param name="typedLobby"></param>
        /// <param name="expectedUsers"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="InvalidRoomOperationException">Throws when PhotonNetwork.JoinOrCreateRoom returns false.</exception>
        /// <exception cref="FailedToCreateRoomException">Throw when room creation fails.</exception>
        /// <exception cref="FailedToJoinRoomException">Throw when you fail to join the room.</exception>
        public static async UniTask<bool> JoinOrCreateRoomAsync(
            string roomName,
            RoomOptions roomOptions,
            TypedLobby typedLobby,
            string[] expectedUsers = null,
            CancellationToken token = default)
        {
            var createdRoomTask = Pun2TaskCallback.OnCreatedRoomAsync(token).GetAwaiter();
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnJoinedRoomAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnCreateRoomFailedAsync(token),
                Pun2TaskCallback.OnJoinRoomFailedAsync(token));

            var valid = PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby, expectedUsers);
            if (!valid) throw new InvalidRoomOperationException("It is not ready to join a room.");

            var (winIndex,
                _,
                (createFailedCode, createFailedMessage),
                _) = await task;
            return winIndex switch
            {
                0 => createdRoomTask.IsCompleted,
                1 => throw new FailedToCreateRoomException(createFailedCode, createFailedMessage),
                _ => throw new FailedToJoinRoomException(createFailedCode, createFailedMessage)
            };
        }

        /// <summary>
        /// PhotonNetwork.JoinRoom
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="expectedUsers"></param>
        /// <param name="token"></param>
        /// <exception cref="InvalidRoomOperationException">Throws when PhotonNetwork.JoinRoom returns false.</exception>
        /// <exception cref="FailedToJoinRoomException">Throw when you fail to join the room.</exception>
        public static async UniTask JoinRoomAsync(string roomName,
            string[] expectedUsers = null,
            CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnJoinedRoomAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnJoinRoomFailedAsync(token));

            var valid = PhotonNetwork.JoinRoom(roomName, expectedUsers);
            if (!valid) throw new InvalidRoomOperationException("It is not ready to join a room.");

            var (winIndex, _, (returnCode, message)) = await task;
            if (winIndex == 0) return;
            throw new FailedToJoinRoomException(returnCode, message);
        }

        /// <summary>
        /// PhotonNetwork.JoinRandomRoom
        /// </summary>
        /// <param name="expectedCustomRoomProperties"></param>
        /// <param name="expectedMaxPlayers"></param>
        /// <param name="matchingType"></param>
        /// <param name="typedLobby"></param>
        /// <param name="sqlLobbyFilter"></param>
        /// <param name="expectedUsers"></param>
        /// <param name="token"></param>
        /// <exception cref="InvalidRoomOperationException">Throws when PhotonNetwork.JoinRandomRoom returns false.</exception>
        /// <exception cref="FailedToJoinRoomException">Throw when you fail to join the room.</exception>
        public static async UniTask JoinRandomRoomAsync(
            Hashtable expectedCustomRoomProperties,
            byte expectedMaxPlayers,
            MatchmakingMode matchingType,
            TypedLobby typedLobby,
            string sqlLobbyFilter,
            string[] expectedUsers = null,
            CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnJoinedRoomAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnJoinRandomFailedAsync(token));

            var valid = PhotonNetwork.JoinRandomRoom(
                expectedCustomRoomProperties,
                expectedMaxPlayers,
                matchingType,
                typedLobby,
                sqlLobbyFilter,
                expectedUsers);
            if (!valid) throw new InvalidRoomOperationException("It is not ready to join a room.");

            var (winIndex, _, (returnCode, message)) = await task;
            if (winIndex == 0) return;
            throw new FailedToJoinRoomException(returnCode, message);
        }

        /// <summary>
        /// PhotonNetwork.RejoinRoom
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="token"></param>
        /// <exception cref="InvalidRoomOperationException">Throws when PhotonNetwork.RejoinRoom returns false.</exception>
        /// <exception cref="FailedToJoinRoomException">Throw when you fail to join the room.</exception>
        public static async UniTask RejoinRoomAsync(string roomName, CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnJoinedRoomAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnJoinRoomFailedAsync(token));

            var valid = PhotonNetwork.RejoinRoom(roomName);
            if (!valid) throw new InvalidRoomOperationException("It is not ready to join a room.");

            var (winIndex, _, (returnCode, message)) = await task;
            if (winIndex == 0) return;
            throw new FailedToJoinRoomException(returnCode, message);
        }

        /// <summary>
        /// PhotonNetwork.ReconnectAndRejoin
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="InvalidRoomOperationException">Throws when PhotonNetwork.ReconnectAndRejoin returns false.</exception>
        /// <exception cref="FailedToJoinRoomException">Throw when you fail to join the room.</exception>
        public static async UniTask ReconnectAndRejoinAsync(CancellationToken token)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnJoinedRoomAsync(token).AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnJoinRoomFailedAsync(token));

            var valid = PhotonNetwork.ReconnectAndRejoin();
            if (!valid) throw new InvalidRoomOperationException("It is not ready to join a room.");

            var (winIndex, _, (returnCode, message)) = await task;
            if (winIndex == 0) return;
            throw new FailedToJoinRoomException(returnCode, message);
        }

        /// <summary>
        /// PhotonNetwork.LeaveRoom
        /// </summary>
        /// <param name="becomeInactive"></param>
        /// <exception cref="InvalidRoomOperationException">Throws when PhotonNetwork.LeaveRoom returns false.</exception>
        public static async UniTask LeaveRoomAsync(bool becomeInactive = true, CancellationToken token = default)
        {
            if (!PhotonNetwork.LeaveRoom(becomeInactive))
            {
                throw new InvalidRoomOperationException("Failed to leave room.");
            }

            await Pun2TaskCallback.OnLeftRoomAsync(token);
        }

        /// <summary>
        /// PhotonNetwork.JoinLobby
        /// </summary>
        /// <param name="typedLobby"></param>
        /// <param name="token"></param>
        /// <exception cref="InvalidNetworkOperationException">Throws when PhotonNetwork.JoinLobbyAsync returns false.</exception>
        public static async UniTask JoinLobbyAsync(TypedLobby typedLobby = null,
            CancellationToken token = default)
        {
            if (!PhotonNetwork.JoinLobby(typedLobby))
            {
                throw new InvalidNetworkOperationException("Failed to join lobby.");
            }

            await Pun2TaskCallback.OnJoinedLobbyAsync(token);
        }

        /// <summary>
        /// PhotonNetwork.LeaveLobby
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="InvalidNetworkOperationException">Throws when PhotonNetwork.OnLeftLobbyAsync returns false.</exception>
        public static async UniTask LeaveLobbyAsync(CancellationToken token = default)
        {
            if (!PhotonNetwork.LeaveLobby())
            {
                throw new InvalidNetworkOperationException("Failed to leave lobby.");
            }

            await Pun2TaskCallback.OnLeftLobbyAsync(token);
        }

        #endregion

        /// <summary>
        /// PhotonNetwork.GetCustomRoomList
        /// </summary>
        /// <param name="typedLobby"></param>
        /// <param name="sqlLobbyFilter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="InvalidNetworkOperationException">Throws when PhotonNetwork.GetCustomRoomList returns false.</exception>
        public static async UniTask<IList<RoomInfo>> GetCustomRoomListAsync(TypedLobby typedLobby,
            string sqlLobbyFilter,
            CancellationToken token = default)
        {
            if (!PhotonNetwork.GetCustomRoomList(typedLobby, sqlLobbyFilter))
            {
                throw new InvalidNetworkOperationException("Failed to get custom room list.");
            }

            return await Pun2TaskCallback.OnRoomListUpdateAsync(token);
        }

        #region Exceptions

        public class ConnectionFailedException : Pun2TaskException
        {
            public DisconnectCause DisconnectCause { get; }

            public ConnectionFailedException(DisconnectCause disconnectCause)
            {
                DisconnectCause = disconnectCause;
            }
        }

        public class InvalidNetworkOperationException : Pun2TaskException
        {
            public InvalidNetworkOperationException(string message) : base(message)
            {
            }
        }

        public class InvalidRoomOperationException : InvalidNetworkOperationException
        {
            public InvalidRoomOperationException(string message) : base(message)
            {
            }
        }

        public class FailedToJoinRoomException : Pun2TaskException
        {
            public short ReturnCode { get; }

            public FailedToJoinRoomException(short returnCode, string message) : base(message)
            {
                ReturnCode = returnCode;
            }
        }

        public class FailedToJoinLobbyException : Pun2TaskException
        {
            public short ReturnCode { get; }

            public FailedToJoinLobbyException(short returnCode, string message) : base(message)
            {
                ReturnCode = returnCode;
            }
        }

        public class FailedToCreateRoomException : Pun2TaskException
        {
            public short ReturnCode { get; }

            public FailedToCreateRoomException(short returnCode, string message) : base(message)
            {
                ReturnCode = returnCode;
            }
        }

        public class Pun2TaskException : Exception
        {
            public Pun2TaskException()
            {
            }

            public Pun2TaskException(string message) : base(message)
            {
            }
        }

        #endregion
    }
}
#endif