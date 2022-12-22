﻿namespace AirFishLab.ScrollingList.Demo
{
    public class IntListBank : BaseListBank
    {
        private readonly int[] _contents = {
            1, 2, 3,
        };

        public override object GetListContent(int index)
        {
            return _contents[index];
        }

        public override int GetListLength()
        {
            return _contents.Length;
        }
    }
}
