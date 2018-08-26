using System.ComponentModel;

namespace DataLayer.Helpers
{
    public enum NotificationTypesEnum
    {
        [DisplayName("Sensor Reading")]
        SensorReading = 1,

        [DisplayName("Motion Detection")]
        MotionDetection = 2
    }
}