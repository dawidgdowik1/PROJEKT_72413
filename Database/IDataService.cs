using System.Data;

namespace PROJEKT_72413.Database
{
    public interface IDataService
    {
        DataTable GetTable(string sql);
        int ExecuteCommand(string sql);
        bool TestConnection();
    }
}