#if PUN_TO_UNITASK_SUPPORT
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace Pun2Task.Callbacks
{
    internal sealed class PunCallbacksBridge : MonoBehaviourPunCallbacks
    {
        #region OnConnected

        private AsyncReactiveProperty<AsyncUnit> _onConnected;

        public UniTask GetOnConnectedAsync(CancellationToken ct = default)
        {
            if (_onConnected == null)
            {
                _onConnected = new AsyncReactiveProperty<AsyncUnit>(AsyncUnit.Default);
                _onConnected.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onConnected.WaitAsync(ct);
        }

        public override void OnConnected()
        {
            if (_onConnected != null)
            {
                _onConnected.Value = AsyncUnit.Default;
            }
        }

        #endregion

        #region OnLeftRoom

        private AsyncReactiveProperty<AsyncUnit> _onLeftRoom;

        public UniTask GetOnLeftRoomAsync(CancellationToken ct = default)
        {
            if (_onLeftRoom == null)
            {
                _onLeftRoom = new AsyncReactiveProperty<AsyncUnit>(AsyncUnit.Default);
                _onLeftRoom.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onLeftRoom.WaitAsync(ct);
        }


        public override void OnLeftRoom()
        {
            if (_onLeftRoom != null)
            {
                _onLeftRoom.Value = AsyncUnit.Default;
            }
        }

        #endregion

        #region OnMasterClientSwitched

        private AsyncReactiveProperty<Player> _onMasterClientSwitched;

        public UniTask<Player> GetOnMasterClientSwitchedAsync(CancellationToken ct = default)
        {
            if (_onMasterClientSwitched == null)
            {
                _onMasterClientSwitched = new AsyncReactiveProperty<Player>(default);
                _onMasterClientSwitched.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onMasterClientSwitched.WaitAsync(ct);
        }


        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            if (_onMasterClientSwitched != null)
            {
                _onMasterClientSwitched.Value = newMasterClient;
            }
        }

        #endregion

        #region OnCreateRoomFailed

        private AsyncReactiveProperty<(short returnCode, string message)> _onCreateRoomFailed;

        public UniTask<(short returnCode, string message)> GetOnCreateRoomFailedAsync(CancellationToken ct = default)
        {
            if (_onCreateRoomFailed == null)
            {
                _onCreateRoomFailed = new AsyncReactiveProperty<(short returnCode, string message)>(default);
                _onCreateRoomFailed.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onCreateRoomFailed.WaitAsync(ct);
        }


        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            if (_onCreateRoomFailed != null)
            {
                _onCreateRoomFailed.Value = (returnCode, message);
            }
        }

        #endregion

        #region OnJoinRoomFailed

        private AsyncReactiveProperty<(short returnCode, string message)> _onJoinRoomFailed;

        public UniTask<(short returnCode, string message)> GetOnJoinRoomFailedAsync(CancellationToken ct = default)
        {
            if (_onJoinRoomFailed == null)
            {
                _onJoinRoomFailed = new AsyncReactiveProperty<(short returnCode, string message)>(default);
                _onJoinRoomFailed.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onJoinRoomFailed.WaitAsync(ct);
        }


        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            if (_onJoinRoomFailed != null)
            {
                _onJoinRoomFailed.Value = (returnCode, message);
            }
        }

        #endregion

        #region OnCreatedRoom

        private AsyncReactiveProperty<AsyncUnit> _onCreatedRoom;

        public UniTask GetOnCreatedRoomAsync(CancellationToken ct = default)
        {
            if (_onCreatedRoom == null)
            {
                _onCreatedRoom = new AsyncReactiveProperty<AsyncUnit>(AsyncUnit.Default);
                _onCreatedRoom.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onCreatedRoom.WaitAsync(ct);
        }


        public override void OnCreatedRoom()
        {
            if (_onCreatedRoom != null)
            {
                _onCreatedRoom.Value = AsyncUnit.Default;
            }
        }

        #endregion

        #region OnJoinedLobby

        private AsyncReactiveProperty<AsyncUnit> _onJoinedLobby;

        public UniTask GetOnJoinedLobbyAsync(CancellationToken ct = default)
        {
            if (_onJoinedLobby == null)
            {
                _onJoinedLobby = new AsyncReactiveProperty<AsyncUnit>(AsyncUnit.Default);
                _onJoinedLobby.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onJoinedLobby.WaitAsync(ct);
        }


        public override void OnJoinedLobby()
        {
            if (_onJoinedLobby != null)
            {
                _onJoinedLobby.Value = AsyncUnit.Default;
            }
        }

        #endregion

        #region OnLeftLobby

        private AsyncReactiveProperty<AsyncUnit> _onLeftLobby;

        public UniTask GetOnLeftLobbyAsync(CancellationToken ct = default)
        {
            if (_onLeftLobby == null)
            {
                _onLeftLobby = new AsyncReactiveProperty<AsyncUnit>(AsyncUnit.Default);
                _onLeftLobby.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onLeftLobby.WaitAsync(ct);
        }


        public override void OnLeftLobby()
        {
            if (_onLeftLobby != null)
            {
                _onLeftLobby.Value = AsyncUnit.Default;
            }
        }

        #endregion

        #region OnDisconnected

        private AsyncReactiveProperty<DisconnectCause> _onDisconnected;

        public UniTask<DisconnectCause> GetOnDisconnectedAsync(CancellationToken ct = default)
        {
            if (_onDisconnected == null)
            {
                _onDisconnected = new AsyncReactiveProperty<DisconnectCause>(default);
                _onDisconnected.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onDisconnected.WaitAsync(ct);
        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            if (_onDisconnected != null)
            {
                _onDisconnected.Value = cause;
            }
        }

        #endregion

        #region OnRegionListReceived

        private AsyncReactiveProperty<RegionHandler> _onRegionListReceived;

        public UniTask<RegionHandler> GetOnRegionListReceivedAsync(CancellationToken ct = default)
        {
            if (_onRegionListReceived == null)
            {
                _onRegionListReceived = new AsyncReactiveProperty<RegionHandler>(default);
                _onRegionListReceived.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onRegionListReceived.WaitAsync(ct);
        }

        public override void OnRegionListReceived(RegionHandler regionHandler)
        {
            if (_onRegionListReceived != null)
            {
                _onRegionListReceived.Value = regionHandler;
            }
        }

        #endregion

        #region OnRoomListUpdate

        private AsyncReactiveProperty<IList<RoomInfo>> _onRoomListUpdate;

        public UniTask<IList<RoomInfo>> GetOnRoomListUpdateAsync(CancellationToken ct = default)
        {
            if (_onRoomListUpdate == null)
            {
                _onRoomListUpdate = new AsyncReactiveProperty<IList<RoomInfo>>(default);
                _onRoomListUpdate.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onRoomListUpdate.WaitAsync(ct);
        }


        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            if (_onRoomListUpdate != null)
            {
                _onRoomListUpdate.Value = roomList;
            }
        }

        #endregion

        #region OnJoinedRoom

        private AsyncReactiveProperty<AsyncUnit> _onJoinedRoom;

        public UniTask GetOnJoinedRoomAsync(CancellationToken ct = default)
        {
            if (_onJoinedRoom == null)
            {
                _onJoinedRoom = new AsyncReactiveProperty<AsyncUnit>(AsyncUnit.Default);
                _onJoinedRoom.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onJoinedRoom.WaitAsync(ct);
        }


        public override void OnJoinedRoom()
        {
            if (_onJoinedRoom != null)
            {
                _onJoinedRoom.Value = AsyncUnit.Default;
            }
        }

        #endregion

        #region OnPlayerEnteredRoom

        private AsyncReactiveProperty<Player> _onPlayerEnteredRoom;

        public UniTask<Player> GetOnPlayerEnteredRoomAsync(CancellationToken ct = default)
        {
            if (_onPlayerEnteredRoom == null)
            {
                _onPlayerEnteredRoom = new AsyncReactiveProperty<Player>(default);
                _onPlayerEnteredRoom.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onPlayerEnteredRoom.WaitAsync(ct);
        }

        public IUniTaskAsyncEnumerable<Player> OnPlayerEnteredRoomAsyncEnumerable
        {
            get
            {
                if (_onPlayerEnteredRoom == null)
                {
                    _onPlayerEnteredRoom = new AsyncReactiveProperty<Player>(default);
                    _onPlayerEnteredRoom.AddTo(this.GetCancellationTokenOnDestroy());
                    return _onPlayerEnteredRoom.WithoutCurrent();
                }

                return _onPlayerEnteredRoom;
            }
        }


        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (_onPlayerEnteredRoom != null)
            {
                _onPlayerEnteredRoom.Value = newPlayer;
            }
        }

        #endregion

        #region OnPlayerLeftRoom

        private AsyncReactiveProperty<Player> _onPlayerLeftRoom;

        public UniTask<Player> GetOnPlayerLeftRoomAsync(CancellationToken ct = default)
        {
            if (_onPlayerLeftRoom == null)
            {
                _onPlayerLeftRoom = new AsyncReactiveProperty<Player>(default);
                _onPlayerLeftRoom.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onPlayerLeftRoom.WaitAsync(ct);
        }

        public IUniTaskAsyncEnumerable<Player> OnPlayerLeftRoomAsyncEnumerable
        {
            get
            {
                if (_onPlayerLeftRoom == null)
                {
                    _onPlayerLeftRoom = new AsyncReactiveProperty<Player>(default);
                    _onPlayerLeftRoom.AddTo(this.GetCancellationTokenOnDestroy());
                    return _onPlayerLeftRoom.WithoutCurrent();
                }

                return _onPlayerLeftRoom;
            }
        }


        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (_onPlayerLeftRoom != null)
            {
                _onPlayerLeftRoom.Value = otherPlayer;
            }
        }

        #endregion

        #region OnJoinRandomFailed

        private AsyncReactiveProperty<(short returnCode, string message)> _onJoinRandomFailed;

        public UniTask<(short returnCode, string message)> GetOnJoinRandomFailedAsync(CancellationToken ct = default)
        {
            if (_onJoinRandomFailed == null)
            {
                _onJoinRandomFailed = new AsyncReactiveProperty<(short returnCode, string message)>(default);
                _onJoinRandomFailed.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onJoinRandomFailed.WaitAsync(ct);
        }


        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            if (_onJoinRandomFailed != null)
            {
                _onJoinRandomFailed.Value = (returnCode, message);
            }
        }

        #endregion

        #region OnConnectedToMasterAsync

        private AsyncReactiveProperty<AsyncUnit> _onConnectedToMaster;

        public UniTask GetOnConnectedToMasterAsync(CancellationToken ct = default)
        {
            if (_onConnectedToMaster == null)
            {
                _onConnectedToMaster = new AsyncReactiveProperty<AsyncUnit>(AsyncUnit.Default);
                _onConnectedToMaster.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onConnectedToMaster.WaitAsync(ct);
        }


        public override void OnConnectedToMaster()
        {
            if (_onConnectedToMaster != null)
            {
                _onConnectedToMaster.Value = AsyncUnit.Default;
            }
        }

        #endregion

        #region OnRoomPropertiesUpdate

        private AsyncReactiveProperty<Hashtable> _onRoomPropertiesUpdate;

        public UniTask<Hashtable> GetOnRoomPropertiesUpdateAsync(CancellationToken ct = default)
        {
            if (_onRoomPropertiesUpdate == null)
            {
                _onRoomPropertiesUpdate = new AsyncReactiveProperty<Hashtable>(default);
                _onRoomPropertiesUpdate.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onRoomPropertiesUpdate.WaitAsync(ct);
        }


        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if (_onRoomPropertiesUpdate != null)
            {
                _onRoomPropertiesUpdate.Value = propertiesThatChanged;
            }
        }

        #endregion

        #region OnPlayerPropertiesUpdate

        private AsyncReactiveProperty<(Player targetPlayer, Hashtable changedProps)> _onPlayerPropertiesUpdate;

        public UniTask<(Player targetPlayer, Hashtable changedProps)> GetOnPlayerPropertiesUpdateAsync(
            CancellationToken ct = default)
        {
            if (_onPlayerPropertiesUpdate == null)
            {
                _onPlayerPropertiesUpdate =
                    new AsyncReactiveProperty<(Player targetPlayer, Hashtable changedProps)>(default);
                _onPlayerPropertiesUpdate.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onPlayerPropertiesUpdate.WaitAsync(ct);
        }


        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (_onPlayerPropertiesUpdate != null)
            {
                _onPlayerPropertiesUpdate.Value = (targetPlayer, changedProps);
            }
        }

        #endregion

        #region OnFriendListUpdate

        private AsyncReactiveProperty<IList<FriendInfo>> _onFriendListUpdate;

        public UniTask<IList<FriendInfo>> GetOnFriendListUpdateAsync(CancellationToken ct = default)
        {
            if (_onFriendListUpdate == null)
            {
                _onFriendListUpdate = new AsyncReactiveProperty<IList<FriendInfo>>(default);
                _onFriendListUpdate.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onFriendListUpdate.WaitAsync(ct);
        }


        public override void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            if (_onFriendListUpdate != null)
            {
                _onFriendListUpdate.Value = friendList;
            }
        }

        #endregion

        #region OnCustomAuthenticationResponse

        private AsyncReactiveProperty<IReadOnlyDictionary<string, object>> _onCustomAuthenticationResponse;

        public UniTask<IReadOnlyDictionary<string, object>> GetOnCustomAuthenticationResponseAsync(
            CancellationToken ct = default)
        {
            if (_onCustomAuthenticationResponse == null)
            {
                _onCustomAuthenticationResponse =
                    new AsyncReactiveProperty<IReadOnlyDictionary<string, object>>(default);
                _onCustomAuthenticationResponse.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onCustomAuthenticationResponse.WaitAsync(ct);
        }


        public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            if (_onCustomAuthenticationResponse != null)
            {
                _onCustomAuthenticationResponse.Value = data;
            }
        }

        #endregion

        #region OnCustomAuthenticationFailed

        private AsyncReactiveProperty<string> _onCustomAuthenticationFailed;

        public UniTask<string> GetOnCustomAuthenticationFailedAsync(CancellationToken ct = default)
        {
            if (_onCustomAuthenticationFailed == null)
            {
                _onCustomAuthenticationFailed = new AsyncReactiveProperty<string>(default);
                _onCustomAuthenticationFailed.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onCustomAuthenticationFailed.WaitAsync(ct);
        }


        public override void OnCustomAuthenticationFailed(string debugMessage)
        {
            if (_onCustomAuthenticationFailed != null)
            {
                _onCustomAuthenticationFailed.Value = debugMessage;
            }
        }

        #endregion

        #region OnWebRpcResponse

        private AsyncReactiveProperty<OperationResponse> _onWebRpcResponse;

        public UniTask<OperationResponse> GetOnWebRpcResponseAsync(CancellationToken ct = default)
        {
            if (_onWebRpcResponse == null)
            {
                _onWebRpcResponse = new AsyncReactiveProperty<OperationResponse>(default);
                _onWebRpcResponse.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onWebRpcResponse.WaitAsync(ct);
        }


        public override void OnWebRpcResponse(OperationResponse response)
        {
            if (_onWebRpcResponse != null)
            {
                _onWebRpcResponse.Value = response;
            }
        }

        #endregion

        #region OnLobbyStatisticsUpdate

        private AsyncReactiveProperty<IList<TypedLobbyInfo>> _onLobbyStatisticsUpdate;

        public UniTask<IList<TypedLobbyInfo>> GetOnLobbyStatisticsUpdateAsync(CancellationToken ct = default)
        {
            if (_onLobbyStatisticsUpdate == null)
            {
                _onLobbyStatisticsUpdate = new AsyncReactiveProperty<IList<TypedLobbyInfo>>(default);
                _onLobbyStatisticsUpdate.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onLobbyStatisticsUpdate.WaitAsync(ct);
        }


        public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            if (_onLobbyStatisticsUpdate != null)
            {
                _onLobbyStatisticsUpdate.Value = lobbyStatistics;
            }
        }

        #endregion

        #region OnErrorInfo

        private AsyncReactiveProperty<ErrorInfo> _onErrorInfo;

        public UniTask<ErrorInfo> GetOnErrorInfoAsync(CancellationToken ct = default)
        {
            if (_onErrorInfo == null)
            {
                _onErrorInfo = new AsyncReactiveProperty<ErrorInfo>(default);
                _onErrorInfo.AddTo(this.GetCancellationTokenOnDestroy());
            }

            return _onErrorInfo.WaitAsync(ct);
        }

        public override void OnErrorInfo(ErrorInfo errorInfo)
        {
            if (_onErrorInfo != null)
            {
                _onErrorInfo.Value = errorInfo;
            }
        }

        #endregion
    }
}
#endif