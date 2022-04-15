/*
Copyright (c) 2022, Micah Schuster
All rights reserved.
Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler _instance = null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }
    public static ObjectPooler Instance()
    {
        return _instance;
    }

    public GameObject objectToPool;
    public int amountToPool;
    public bool canExpand;

    private List<GameObject> pool;

    private void Start()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject go=Instantiate(objectToPool);
            go.SetActive(false);
            pool.Add(go);
        }
    }

    public GameObject getPooledObject()
    {
        //just an available pooled object
        foreach(GameObject go in pool)
        {
            if (go.activeInHierarchy == false)
            {
                return go;
            }
        }

        //no pooled objects, should we create more?
        if (canExpand)
        {
            GameObject go = Instantiate(objectToPool);
            go.SetActive(false);
            pool.Add(go);
            return go;
        }


        return null;
    }

}
