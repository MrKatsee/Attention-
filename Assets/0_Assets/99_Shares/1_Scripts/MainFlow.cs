using Attention.EventModule;
using Attention.InputModule;
using Attention.ProcessModule;
using UnityEngine;

namespace Attention
{
    public class MainFlow : MonoBehaviour
    {
        private InputDispatcher _inputDispatcher;
        private EventQueue _eventQueue;
        private EventProcessor _eventProcessor;

        private void Awake()
        {
            _eventQueue = new EventQueue();
            _inputDispatcher = new InputDispatcher(_eventQueue);
        }

        private void Start()
        {

        }

        private void Update()
        {
            _inputDispatcher.Update();
            _eventQueue.Update();
            _eventProcessor.Update();
        }
    }
}