using System;
using System.Collections.Generic;

namespace Tretimi
{
    [Serializable]
    public class SaveData
    {
        public float Coins;
        public string TimeToOpenGift;
        public List<int> AvailableBalls;
        public List<int> AvailableMaps;
        public List<int> AvailableBackgrounds;
        public List<(int, int, int)> MySets;
    }
}

