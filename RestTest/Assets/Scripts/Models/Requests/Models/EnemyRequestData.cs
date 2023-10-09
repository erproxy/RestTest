using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models.Requests.Models
{
    [Serializable]
    public struct EnemyRequestData
    {
        public List<EnemyResults> results;
    }
        
    [Serializable]
    public struct EnemyResults
    {
        public Name name;
        public PictureURL picture;
    }
    [Serializable]
    public struct Name
    {
        public string first;
    }
    
    [Serializable]
    public struct PictureURL
    {
        public string large;
    }
    
    public struct EnemyRequest
    {
        public string Name;
        public Sprite Icon;
    }
}