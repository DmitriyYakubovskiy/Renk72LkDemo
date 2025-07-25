using Renk72Lk.Models.DataBase;

namespace Renk72Lk.ViewModels;

public class CreateBidViewModel
{
    public BidModel Bid { get; set; } = null!;

    public bool IsRepresentativeData { get; set; } = false;
    public bool IsConsumerData { get; set; } = false;
}
