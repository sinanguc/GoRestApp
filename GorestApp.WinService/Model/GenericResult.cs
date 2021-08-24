using System;

namespace GorestApp.WinService.Model
{
    public class GenericResult
    {
        private readonly DateTime _currentTime;
        public GenericResult()
        {
            _currentTime = DateTime.Now;
        }
        public string Version { get; set; }
        public bool Success
        {
            get { return Data != null; }
        }

        public Object Data { get; set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                ExecutionTime = (int)(DateTime.Now - _currentTime).TotalMilliseconds;
            }
        }

        public int ExecutionTime { get; set; }
    }
}
