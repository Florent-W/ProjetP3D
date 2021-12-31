using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class ThreadedDataRequester : MonoBehaviour
{
    static ThreadedDataRequester instance;
    Queue<ThreadInfo> dataQueue = new Queue<ThreadInfo>();

    private void Awake()
    {
        instance = FindObjectOfType<ThreadedDataRequester>();
    }

    // On demande les informations de la map avec la file d'attente
    public static void RequestData(Func<object> generateData, Action<object> callback)
    {
        ThreadStart threadStart = delegate
        {
            instance.DataThread(generateData, callback);
        };

        new Thread(threadStart).Start(); // On lance le thread
    }

    void DataThread(Func<object> generateData, Action<object> callback)
    {
        object data = generateData();
        lock (dataQueue)
        {
            dataQueue.Enqueue(new ThreadInfo(callback, data)); // On ajoute le thread à la file d'attente
        }
    }

    private void Update()
    {
        if (dataQueue.Count > 0) // Si il y a au moins un élément dans la file d'attente
        {
            for (int i = 0; i < dataQueue.Count; i++) // On parcours la file d'attente
            {
                ThreadInfo threadInfo = dataQueue.Dequeue(); // Prochain élement de la file d'attente
                threadInfo.callback(threadInfo.parameter); // On appel le callback
            }
        }
    }

    // Informations des thread
    struct ThreadInfo
    {
        public readonly Action<object> callback;
        public readonly object parameter;

        public ThreadInfo(Action<object> callback, object parameter)
        {
            this.callback = callback;
            this.parameter = parameter;
        }
    }
}
