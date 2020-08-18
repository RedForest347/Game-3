﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace RangerV
{
    public class GlobalSystemStorage : MonoBehaviour
    {
        public static GlobalSystemStorage Instance { get => Singleton<GlobalSystemStorage>.Instance; }
        Dictionary<Type, ProcessingBase> Processings;


        public static void Init()
        {
            Instance.Processings = new Dictionary<Type, ProcessingBase>();
        }

        public static T Add<T>() where T : ProcessingBase, new()
        {
            T processing = new T(); 
            Instance.Processings.Add(typeof(T), processing);

            if (processing is ICustomAwake)
                (processing as ICustomAwake).OnAwake();   
            ManagerUpdate.Instance.AddTo(processing);

            return processing;
        }

        public static T Get<T>() where T : ProcessingBase
        {
            ProcessingBase resolve;
            Instance.Processings.TryGetValue(typeof(T), out resolve);
            return (T)resolve;
        }

        public void StartProcessings()
        {
            ProcessingBase[] values = new ProcessingBase[Processings.Count];
            Processings.Values.CopyTo(values, 0);

            for (int i = 0; i < Processings.Count; i++)
            {
                if(values[i] is ICustomStart)
                    (values[i] as ICustomStart).OnStart();
            }
        }

        public static void StopProcessings()
        {
            Dictionary<Type, ProcessingBase> processings = Instance.Processings;
            ProcessingBase[] values = new ProcessingBase[processings.Count];/// 
            processings.Values.CopyTo(values, 0);

            for (int i = 0; i < processings.Count; i++)
                if (values[i] is ICustomDisable)
                    (values[i] as ICustomDisable).OnDisable();

            processings = new Dictionary<Type, ProcessingBase>();
        }
    }
}
