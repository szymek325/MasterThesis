using System.ComponentModel;

namespace DataLayer.Helpers
{
    public enum NotificationTypes
    {
        [DisplayName("Sensor Reading")]
        SensorReading = 1,

        [DisplayName("Motion Detection")]
        MotionDetection = 2
    }
}