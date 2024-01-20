using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    /// <summary>
    /// ���÷� ���� Stat ������
    /// �ش� ������ ������ ������ ���ӽ����̽� �ȿ��� region�� ���� �����ϸ� �ȴ�.
    /// json ������ ���İ� Ŭ������ ���� �̸��� ��ġ�ؾ� �Ѵٴ� ���� �����ؾ� �Ѵ�.
    /// �����ʹ� Resource/Data/ ������ ����Ǿ� �ִ�.
    /// </summary>
    [Serializable]
    public class Stat
    {
        public int level;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion
    #region Fish
    /// <summary>
    /// ���� ������
    /// �ش� ������ ������ ������ ���ӽ����̽� �ȿ��� region�� ���� �����ϸ� �ȴ�.
    /// json ������ ���İ� Ŭ������ ���� �̸��� ��ġ�ؾ� �Ѵٴ� ���� �����ؾ� �Ѵ�.
    /// �����ʹ� Resource/Data/ ������ ����Ǿ� �ִ�.
    /// </summary>
    [Serializable]
    public class Fish
    {
        public int id;
        public string name;
        public int[] level;
        public int moveSpeed;
        public int[] size;
        public int health;
        public string[] lure;
        public string[] color;
        public string feature;
        public int Iscatching;
    }

    [Serializable]
    public class FishData : ILoader<int, Fish>
    {
        public List<Fish> fishes = new List<Fish>();

        public Dictionary<int, Fish> MakeDict()
        {
            Dictionary<int, Fish> dict = new Dictionary<int, Fish>();
            foreach (Fish fish in fishes)
                dict.Add(fish.id, fish);
            return dict;
        }
    }
    #endregion
}

