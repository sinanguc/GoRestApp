using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GorestApp.Entities.Dto
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

        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
