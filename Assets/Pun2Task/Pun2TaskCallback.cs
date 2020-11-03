using Cysharp.Threading.Tasks;
using Pun2Task.Callbacks;
using UnityEngine;

namespace Pun2Task
{
    public static class Pun2TaskCallback
    {
        public static UniTask<AsyncUnit> OnConnectedAsync()
        {
            return GetOrCreate().OnConnectedAsync;
        }

        
        public static UniTask<AsyncUnit> OnLeftRoomAsync()
        {
            return GetOrCreate().OnLeftRoomAsync;
        }
        
        public static UniTask<AsyncUnit> OnCreatedRoomAsync()
        {
            return GetOrCreate().OnCreatedRoomAsync;
        }

        
        public static UniTask<AsyncUnit> OnConnectedToMasterAsync()
        {
            return GetOrCreate().OnConnectedToMasterAsync;
        }

        

        #region GetOrCreate

        private static PunCallbacksBridge _instance;

        private static PunCallbacksBridge GetOrCreate()
        {
            if (_instance != null) return _instance;

            var gameObject = new GameObject {name = "Pun2TaskCallback"};
            Object.DontDestroyOnLoad(gameObject);
            _instance = gameObject.AddComponent<PunCallbacksBridge>();
            return _instance;
        }

        #endregion
    }
}