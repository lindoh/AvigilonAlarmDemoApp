namespace AvigilonAlarmDemoApp.UI.Model
{
    /// <summary>
    /// Avigilon Web Endpoint media parameter for actions on Alarm 
    /// Either 'ACKNOWLEDGE' or 'PURGE' or 'CLAIM' or 'UNCLAIM'. 
    /// </summary>
    public enum ActionTypes
    {
        ACKNOWLEDGE,
        PURGE,
        CLAIM,
        UNCLAIM,
        TRIGGER
    };
}