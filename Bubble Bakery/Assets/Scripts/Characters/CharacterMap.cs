using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Characters
{
    public class CharacterMap : MonoBehaviour
    {
        public static CharacterMap Instance;

        [SerializeField] private Character[] _characters;

        private Dictionary<string, Character> _characterMap;

        private void Awake()
        {
            CheckInstance();
            InitCharacterMap();
            
            _characterMap.Print("characters");
        }

        private void CheckInstance()
        {
            if (ExistsOtherInstanceAlready())
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private bool ExistsOtherInstanceAlready()
        {
            return Instance != null && Instance != this;
        }

        private void InitCharacterMap()
        {
            _characterMap = new Dictionary<string, Character>();

            foreach (var character in _characters)
            {
                _characterMap[character.name] = character;
            }
        }
    }
}