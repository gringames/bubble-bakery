using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Characters
{
    public class CharacterMap : MonoBehaviour
    {
        public static CharacterMap Instance;

        [SerializeField] private Character[] characters;

        private Dictionary<string, Character> _characterMap;

        private void Awake()
        {
            CheckInstance();
            InitCharacterMap();

            _characterMap.Print("characters");
        }

        #region Singleton

        private void CheckInstance()
        {
            if (ExistsOtherInstanceAlready())
            {
                Debug.LogError($"There are more than 1 CharacterMaps in the scene! Deleting {name}");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private bool ExistsOtherInstanceAlready()
        {
            return Instance != null && Instance != this;
        }

        #endregion

        private void InitCharacterMap()
        {
            _characterMap = new Dictionary<string, Character>();

            foreach (var character in characters)
            {
                _characterMap[character.name] = character;
            }
        }

        public Character GetCharacterToName(string characterName)
        {
            return _characterMap[characterName];
        }
        // TODO: create YOU character
    }
}