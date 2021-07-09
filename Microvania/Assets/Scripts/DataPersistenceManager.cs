using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField]
    List<Object> persistentData;
    private void Start()
    {
        //var json = JsonConvert.SerializeObject(persistentData);
        //persistentData = JsonConvert.DeserializeObject<List<Object>>(json);
    }
}
