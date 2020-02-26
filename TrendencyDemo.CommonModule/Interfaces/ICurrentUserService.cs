
namespace TrendencyDemo.CommonModule.Interfaces
{
    public interface ICurrentUserService
    {
        int GetUserId();
        bool IsInRole(string role);
    }
}
