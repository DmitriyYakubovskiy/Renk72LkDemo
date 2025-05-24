using Renk72Lk.ViewModels;

namespace Renk72Lk.Services;

public interface IBidViewModelService
{
    public CreateBidViewModel GetCreateBidViewModelByUserId(int userId, int? daysAgo = null);
    public CreateBidViewModel GetCreateBidViewModelById(int id);
}
