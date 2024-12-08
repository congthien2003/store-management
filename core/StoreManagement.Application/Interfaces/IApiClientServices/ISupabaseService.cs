using Supabase;
namespace StoreManagement.Application.Interfaces.IApiClientServices
{
    public interface ISupabaseService
    {
        Client GetClient();
    }
}
