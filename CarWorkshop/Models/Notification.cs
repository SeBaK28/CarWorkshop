using System;
namespace CarWorkshop.Models
{
	public class Notification
	{
        public Notification(string message, string type)
        {
            Type = type;
            Message = message;
        }

        public string Type { get; set; }
		public string Message { get; set; }
    }
}

