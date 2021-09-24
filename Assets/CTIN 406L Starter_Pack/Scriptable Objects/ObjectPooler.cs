using System;
using System.Collections.Generic;
using UnityEngine;

namespace CTIN_406L_Starter_Pack.Scriptable_Objects
{
	public class ObjectPooler : MonoBehaviour
	{
		[Serializable]
		public class Pool
		{
			public string tag;
			public GameObject prefab;
			public int size;

		}

		public List<Pool> pools;
		public Dictionary<string, Queue<GameObject>> poolDictionary;
		// Start is called before the first frame update
		void Start()
		{
			poolDictionary = new Dictionary<string, Queue<GameObject>>();
			foreach (var pool in pools)
			{
				Queue<GameObject> objectPool = new Queue<GameObject>();

				for (int i = 0; i < pool.size; i++)
				{
					GameObject obj = Instantiate(pool.prefab);
					obj.SetActive(false);
					objectPool.Enqueue(obj);
				}
				poolDictionary.Add(pool.tag,objectPool);
			}
		}

	}
}
