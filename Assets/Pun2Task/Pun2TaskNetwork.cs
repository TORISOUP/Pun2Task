using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;

namespace Pun2Task
{
    public static class Pun2TaskNetwork
    {
        #region ServerConnection

        /// <summary>Connect to Photon as configured in the PhotonServerSettings file.</summary>
        /// <remarks>
        /// Implement IConnectionCallbacks, to make your game logic aware of state changes.
        /// Especially, IConnectionCallbacks.ConnectedToMasterServer is useful to react when
        /// the client can do matchmaking.
        ///
        /// This method will disable OfflineMode (which won't destroy any instantiated GOs) and it
        /// will set IsMessageQueueRunning to true.
        ///
        /// Your Photon configuration is created by the PUN Wizard and contains the AppId,
        /// region for Photon Cloud games, the server address among other things.
        ///
        /// To ignore the settings file, set the relevant values and connect by calling
        /// ConnectToMaster, ConnectToRegion.
        ///
        /// To connect to the Photon Cloud, a valid AppId must be in the settings file (shown in the Photon Cloud Dashboard).
        /// https://dashboard.photonengine.com
        ///
        /// Connecting to the Photon Cloud might fail due to:
        /// - Invalid AppId
        /// - Network issues
        /// - Invalid region
        /// - Subscription CCU limit reached
        /// - etc.
        ///
        /// In general check out the <see cref="DisconnectCause"/> from the <see cref="IConnectionCallbacks.OnDisconnected"/> callback.
        ///  </remarks>
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

        public static async UniTask ConnectUsingSettingsAsync(
            AppSettings appSettings,
            bool startInOfflineMode = false,
            CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync().AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync());

            PhotonNetwork.ConnectUsingSettings(appSettings, startInOfflineMode);

            var (winIndex, _, disconnectCause) = await task.WithCancellation(token);

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }

        public static async UniTask ConnectToMasterAsync(
            string masterServerAddress,
            int port,
            string appID,
            CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync().AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync());

            PhotonNetwork.ConnectToMaster(masterServerAddress, port, appID);

            var (winIndex, _, disconnectCause) = await task.WithCancellation(token);

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }

        /// <summary>
        /// Connect to the Photon Cloud region with the lowest ping (on platforms that support Unity's Ping).
        /// </summary>
        /// <remarks>
        /// Will save the result of pinging all cloud servers in PlayerPrefs. Calling this the first time can take +-2 seconds.
        /// The ping result can be overridden via PhotonNetwork.OverrideBestCloudServer(..)
        /// This call can take up to 2 seconds if it is the first time you are using this, all cloud servers will be pinged to check for the best region.
        ///
        /// The PUN Setup Wizard stores your appID in a settings file and applies a server address/port.
        /// To connect to the Photon Cloud, a valid AppId must be in the settings file (shown in the Photon Cloud Dashboard).
        /// https://dashboard.photonengine.com
        ///
        /// Connecting to the Photon Cloud might fail due to:
        /// - Invalid AppId
        /// - Network issues
        /// - Invalid region
        /// - Subscription CCU limit reached
        /// - etc.
        ///
        /// In general check out the <see cref="DisconnectCause"/> from the <see cref="IConnectionCallbacks.OnDisconnected"/> callback.
        /// </remarks>
        /// <returns>If this client is going to connect to cloud server based on ping. Even if true, this does not guarantee a connection but the attempt is being made.</returns>
        public static async UniTask ConnectToBestCloudServerAsync(CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync().AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync());

            PhotonNetwork.ConnectToBestCloudServer();

            var (winIndex, _, disconnectCause) = await task.WithCancellation(token);

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }

        /// <summary>
        /// Connects to the Photon Cloud region of choice.
        /// </summary>
        /// <remarks>
        /// It's typically enough to define the region code ("eu", "us", etc).
        /// Connecting to a specific cluster may be necessary, when regions get sharded and you support friends / invites.
        ///
        /// In all other cases, you should not define a cluster as this allows the Name Server to distribute
        /// clients as needed. A random, load balanced cluster will be selected.
        ///
        /// The Name Server has the final say to assign a cluster as available.
        /// If the requested cluster is not available another will be assigned.
        ///
        /// Once connected, check the value of CurrentCluster.
        /// </remarks>
        public static async UniTask ConnectToRegionAsync(string region, CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync().AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync());

            PhotonNetwork.ConnectToRegion(region);

            var (winIndex, _, disconnectCause) = await task.WithCancellation(token);

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }

        /// <summary>
        /// Makes this client disconnect from the photon server, a process that leaves any room and calls OnDisconnected on completion.
        /// </summary>
        /// <remarks>
        /// When you disconnect, the client will send a "disconnecting" message to the server. This speeds up leave/disconnect
        /// messages for players in the same room as you (otherwise the server would timeout this client's connection).
        /// When used in OfflineMode, the state-change and event-call OnDisconnected are immediate.
        /// Offline mode is set to false as well.
        /// Once disconnected, the client can connect again. Use ConnectUsingSettings.
        /// </remarks>
        public static async UniTask DisconnectAsync()
        {
            if (PhotonNetwork.NetworkClientState == ClientState.Disconnected) return;
            PhotonNetwork.Disconnect();
            await Pun2TaskCallback.OnDisconnectedAsync();
        }

        /// <summary>Can be used to reconnect to the master server after a disconnect.</summary>
        /// <remarks>
        /// After losing connection, you can use this to connect a client to the region Master Server again.
        /// Cache the room name you're in and use RejoinRoom(roomname) to return to a game.
        /// Common use case: Press the Lock Button on a iOS device and you get disconnected immediately.
        /// </remarks>
        public static async UniTask ReconnectAsync(CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnConnectedToMasterAsync().AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnDisconnectedAsync());

            PhotonNetwork.Reconnect();

            var (winIndex, _, disconnectCause) = await task.WithCancellation(token);

            if (winIndex == 0) return;
            throw new ConnectionFailedException(disconnectCause);
        }

        #endregion

        #region RoomConnection

        /// <summary>
        /// Creates a new room. Will callback: OnCreatedRoom and OnJoinedRoom or OnCreateRoomFailed.
        /// </summary>
        /// <remarks>
        /// When successful, this calls the callbacks OnCreatedRoom and OnJoinedRoom (the latter, cause you join as first player).
        /// In all error cases, OnCreateRoomFailed gets called.
        ///
        /// Creating a room will fail if the room name is already in use or when the RoomOptions clashing
        /// with one another. Check the EnterRoomParams reference for the various room creation options.
        ///
        /// If you don't want to create a unique room-name, pass null or "" as name and the server will assign a roomName (a GUID as string).
        ///
        /// This method can only be called while the client is connected to a Master Server so you should
        /// implement the callback OnConnectedToMaster.
        /// Check the return value to make sure the operation will be called on the server.
        /// Note: There will be no callbacks if this method returned false.
        ///
        /// More about PUN matchmaking:
        /// https://doc.photonengine.com/en-us/pun/v2/lobby-and-matchmaking/matchmaking-and-lobby
        /// </remarks>
        /// <param name="roomName">Unique name of the room to create. Pass null or "" to make the server generate a name.</param>
        /// <param name="roomOptions">Common options for the room like MaxPlayers, initial custom room properties and similar. See RoomOptions type..</param>
        /// <param name="typedLobby">If null, the room is automatically created in the currently used lobby (which is "default" when you didn't join one explicitly).</param>
        /// <param name="expectedUsers">Optional list of users (by UserId) who are expected to join this game and who you want to block a slot for.</param>
        public static async UniTask CreateRoomAsync(
            string roomName,
            RoomOptions roomOptions = null,
            TypedLobby typedLobby = null,
            string[] expectedUsers = null,
            CancellationToken token = default)
        {
            var task = UniTask.WhenAny(
                Pun2TaskCallback.OnJoinedRoomAsync().AsAsyncUnitUniTask(),
                Pun2TaskCallback.OnCreateRoomFailedAsync());

            var valid = PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, expectedUsers);
            if (!valid) throw new InvalidRoomOperationException("It is not ready to create a room.");

            var (winIndex, _, (returnCode, message)) = await task.WithCancellation(token);
            if (winIndex == 0) return;
            throw new FailedToCreateRoomException(returnCode, message);
        }

        #endregion

        #region Exceptions

        public class ConnectionFailedException : Pun2TaskException
        {
            public DisconnectCause DisconnectCause { get; }

            public ConnectionFailedException(DisconnectCause disconnectCause)
            {
                DisconnectCause = disconnectCause;
            }
        }

        public class InvalidRoomOperationException : Pun2TaskException
        {
            public InvalidRoomOperationException(string message) : base(message)
            {
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