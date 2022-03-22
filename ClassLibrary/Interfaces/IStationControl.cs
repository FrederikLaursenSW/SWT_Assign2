namespace ClassLibrary.Models;

public interface IStationControl
{
    void RfidDetected(int id);
}