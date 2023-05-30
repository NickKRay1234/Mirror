using UnityEngine;
using System;
using System.Threading;
using JetBrains.Annotations;

namespace Thread
{
    public class ThreadTest : MonoBehaviour
    {
        private System.Threading.Thread myThread1, myThread2, myThread3;
        private System.Threading.Thread myThread;
        private void Start()
        {
            myThread1 = new System.Threading.Thread(new ParameterizedThreadStart(Print2));
            myThread2 = new System.Threading.Thread(Print2);
            myThread3 = new System.Threading.Thread(message => Debug.Log(message));
            // получаем текущий поток
            // System.Threading.Thread currentThread = System.Threading.Thread.CurrentThread;
            // // получаем имя потока
            // Debug.Log($"Имя потока: {currentThread.Name}");
            // if (string.IsNullOrEmpty(currentThread.Name))
            // {
            //     currentThread.Name = "Main";
            // }
            // Debug.Log($"Имя потока: {currentThread.Name}");
            //
            // Debug.Log($"Thread.ExecutionContext:  {currentThread.ExecutionContext}");
            // Debug.Log($"Thread.IsAlive:  {currentThread.IsAlive}");
            // Debug.Log($"Thread.IsBackground:  {currentThread.IsBackground}");
            //
            // Debug.Log($"Запущен ли поток: {currentThread.IsAlive}");
            // Debug.Log($"ID потока: {currentThread.ManagedThreadId}");
            // Debug.Log($"Приоритет потока: {currentThread.Priority}");
            // Debug.Log($"Статус потока: {currentThread.ThreadState}");


            myThread = new System.Threading.Thread(Print1);
            
            myThread1.Start("Hello 1");
            myThread2.Start("Hello 2");
            myThread3.Start("Salut");
        }

        private void Print1([CanBeNull] object message)
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log($"Второй поток: {i}");
                System.Threading.Thread.Sleep(400);
            }
        }
        
        private void Print2([CanBeNull] object message)
        {
            Debug.Log(message);
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.L))
            // {
            //     myThread1.Start();
            //     for (int i = 0; i < 5; i++)
            //     {
            //         Debug.Log($"Главный поток: {i}");
            //         System.Threading.Thread.Sleep(300);
            //     }
            // }
        }
    }
}
