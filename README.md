# Pun2Task

このライブラリを用いると`Photon Unity Networking 2`で`async/await`が利用可能になります。

This library enables `async/await` in `Photon Unity Networking 2`.

## LICENSE

MIT License.

## 依存ライブラリ/dependency

- [UniTask](https://github.com/Cysharp/UniTask)

## 導入/Getting started

### 1. Download

- Release Page
- ~~UnityPackageManager~~ (準備中/in preparation)

### 2. Add UniTask

パッケージマネージャを利用せずにUniTaskを導入した場合は**PUN_TO_UNITASK_SUPPORT**を`Scripting Define Symbols`に追加してください。

If you installed UniTask without using the package manager, add **PUN_TO_UNITASK_SUPPORT** to your `Scripting Define Symbols`.

## 使い方/How to use

### Connect to Photon server

Use `Pun2TaskNetwork.ConnectUsingSettingsAsync()`.

```cs
private async UniTaskVoid ConnectionSampleAsync(CancellationToken token)
{
    try
    {
        // OnConnectedToMaster が呼び出されるのを待てる
        // You can use async/await to wait for 'OnConnectedToMaster'.
        await Pun2TaskNetwork.ConnectUsingSettingsAsync(token);

        Debug.Log("Connected to master server.");
    }
    catch (Pun2TaskNetwork.ConnectionFailedException ex)
    {
        // 接続失敗時は例外
        // Throw ConnectionFailedException when 'OnDisconnected' called.
        Debug.LogError(ex);
    }
}
```

### Create or join a room

- `Pun2TaskNetwork.JoinOrCreateRoomAsync`
- `Pun2TaskNetwork.JoinRoomAsync`
- `Pun2TaskNetwork.CreateRoomAsync`

```cs
private async UniTaskVoid CreateOrJoinRoomSampleAsync(CancellationToken token)
{
    try
    {
        // ルームへの参加または新規作成
        // 部屋を新規作成した場合はTrue
        // You can await to join or create a room.
        // Return true if you are the first user.
        var isFirstUser = await Pun2TaskNetwork.JoinOrCreateRoomAsync(
            roomName: "test_room",
            roomOptions: default,
            typedLobby: default,
            token: token);

        Debug.Log("Joined room.");
    }
    catch (Pun2TaskNetwork.InvalidRoomOperationException ex)
    {
        // サーバに繋がっていないなど、そもそもメソッドが実行できなかった
        // Not connected to the master server, etc.
        Debug.LogError(ex);
    }
    catch (Pun2TaskNetwork.FailedToCreateRoomException ex)
    {
        // 何らかの理由で部屋が作れなかった
        // Failed to create a room, etc.
        Debug.LogError(ex);
    }
}
```

### Callbacks

各種コールバックは`Pun2TaskCallback`を用いることで、async/awaitとして扱うことができます。

Each callback can be treated as an async/await by using the `Pun2TaskCallback`.

```cs
private async UniTaskVoid Callbacks(CancellationToken token)
{
    // 各種コールバックを待てる
    // You can await connection callbacks.
    await Pun2TaskCallback.OnConnectedAsync();
    await Pun2TaskCallback.OnCreatedRoomAsync();
    await Pun2TaskCallback.OnJoinedRoomAsync();
    await Pun2TaskCallback.OnLeftRoomAsync();
    // etc.

    // パラメータの取得も可能
    // You can get the parameters.
    DisconnectCause disconnectCause = await Pun2TaskCallback.OnDisconnectedAsync();
    Player newPlayer = await Pun2TaskCallback.OnPlayerEnteredRoomAsync();
    Player leftPlayer = await Pun2TaskCallback.OnPlayerLeftRoomAsync();
    // etc.

    // OnPlayerEnteredRoom and OnPlayerLeftRoomAsync can be treated as UniTaskAsyncEnumerable.
    Pun2TaskCallback
        .OnPlayerEnteredRoomAsyncEnumerable()
        .ForEachAsync(x => Debug.Log(x.NickName), cancellationToken: token);

    Pun2TaskCallback
        .OnPlayerLeftRoomAsyncEnumerable()
        .ForEachAsync(x => Debug.Log(x.NickName), cancellationToken: token);
}
```

### PhotonView

所有権の取得をasync/awaitで管理できます。

You can manage the ownership takeover with async/await.

```cs
public class SampleView : MonoBehaviourPun
{
    private void Start()
    {
        // 所有権の遷移を IUniTaskAsyncEnumerable<bool> で監視できる。
        // You can monitor ownership transitions with IUniTaskAsyncEnumerable<bool>.
        photonView
            .IsMineAsyncEnumerable()
            .Subscribe(x =>
            {
                // 所有者が変化したら通知される。
                // Notified when ownership changes.
                Debug.Log($"IsMine = {x}");
            })
            .AddTo(this.GetCancellationTokenOnDestroy());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UniTask.Void(async () =>
            {
                // 所有権の取得を要求して、それが完了するまでawaitできる。
                // You can request ownership and wait until the takeover is complete.
                await photonView.RequestOwnershipAsync();

                Debug.Log("Got ownership!");
            });
        }
    }
}
```