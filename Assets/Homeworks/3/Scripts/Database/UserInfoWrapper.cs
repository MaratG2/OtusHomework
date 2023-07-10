using Lessons.Architecture.PM;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Homework3.Database
{
    [RequireComponent(typeof(DataContainer))]
    [ExecuteAlways]
    public class UserInfoWrapper : SerializedMonoBehaviour, ISaveLoad
    {
        private readonly string _fileName = "UserInfo";
        [OdinSerialize] private UserInfo _userInfo;
        public UserInfo UserInfo => _userInfo;

        [Button("Load")]
        public void Load()
        {
            _userInfo = new UserInfo();
            
            if(SaveLoad.HaveSave(_fileName))
            {
                string data = SaveLoad.Load(_fileName);
                UserInfoStruct dataStruct = JsonUtility.FromJson<UserInfoStruct>(data);
                _userInfo.ChangeName(dataStruct.name);
                _userInfo.ChangeDescription(dataStruct.desc);
                _userInfo.ChangeIcon(dataStruct.icon);
            }
        }

        [Button("Save")]
        public void Save()
        {
            var dataStruct = new UserInfoStruct();
            dataStruct.name = _userInfo.Name;
            dataStruct.desc = _userInfo.Description;
            dataStruct.icon = _userInfo.Icon;
            var data = JsonUtility.ToJson(dataStruct);
            SaveLoad.Save(data, _fileName);
        }

        private struct UserInfoStruct
        {
            public string name;
            public string desc;
            public Sprite icon;
        }
    }
}