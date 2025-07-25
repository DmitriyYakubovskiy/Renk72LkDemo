using Microsoft.Extensions.Logging;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.DataAccess.Extensions;
using Renk72Lk.Services.DataBase;
using Renk72Lk.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Renk72Lk.Services;

public class BidViewModelService: IBidViewModelService
{
    private readonly IBidService bidService;
    private readonly ILogger<BidViewModelService> logger;

    public BidViewModelService(IBidService userBidService, ILogger<BidViewModelService> logger)
    {
        this.bidService = userBidService;
        this.logger = logger;
    }

    public static int Validate(CreateBidViewModel viewModel)
    {
        var validationResults = new List<ValidationResult>();

        if (viewModel.Bid.Step1 == null) return 0;
        var context = new ValidationContext(viewModel.Bid.Step1!);
        bool isValid = Validator.TryValidateObject(
            viewModel.Bid.Step1!,
            context,
            validationResults,
            validateAllProperties: true
        );
        if (!isValid)
        {
            foreach (var error in validationResults)
            {
                var properties = error.MemberNames.Any()
                    ? string.Join(", ", error.MemberNames)
                : "WholeModel";

                Console.WriteLine($"Свойство: {properties}");
                Console.WriteLine($"Сообщение: {error.ErrorMessage}");
            }
            return 1;
        }

        if (viewModel.Bid.Step2 == null) return 0;
        context = new ValidationContext(viewModel.Bid.Step2!);
        isValid = Validator.TryValidateObject(
            viewModel.Bid.Step2!,
            context,
            validationResults,
            validateAllProperties: true
        );
        if (!isValid)
        {
            return 2;
        }

        if (viewModel.Bid.Step3 == null) return 0;
        context = new ValidationContext(viewModel.Bid.Step3!);
        isValid = Validator.TryValidateObject(
            viewModel.Bid.Step3!,
            context,
            validationResults,
            validateAllProperties: true
        );
        if (!isValid)
        {
            return 3;
        }

        if (viewModel.Bid.Step4 == null) return 0;
        context = new ValidationContext(viewModel.Bid.Step4!);
        isValid = Validator.TryValidateObject(
            viewModel.Bid.Step4!,
            context,
            validationResults,
            validateAllProperties: true
        );
        if (!isValid)
        {
            return 4;
        }

        if (viewModel.Bid.Step5 == null) return 0;
        context = new ValidationContext(viewModel.Bid.Step5!);
        isValid = Validator.TryValidateObject(
            viewModel.Bid.Step5!,
            context,
            validationResults,
            validateAllProperties: true
        );
        if (!isValid)
        {
            return 5;
        }

        return 0;
    }

    public CreateBidViewModel GetCreateBidViewModelByUserId(int userId, int? daysAgo = null)
    {
        var viewModel = new CreateBidViewModel();

        if (daysAgo == null)
        {
            viewModel.Bid = bidService.GetOne(userId, 0, includeBid1: true, includeBid2: true, includeBid3: true, includeBid4: true, includeBid5: true, includeUser: true);
        }
        else
        {
            DateTime oneDayAgo = DateTime.UtcNow.AddDays(-daysAgo.Value);
            viewModel.Bid = bidService.GetOne(userId, 0, oneDayAgo, includeBid1: true, includeBid2: true, includeBid3: true, includeBid4: true, includeBid5: true, includeUser: true);
        }

        return SetViewModel(viewModel);
    }

    public CreateBidViewModel GetCreateBidViewModelById(int id)
    {
        var viewModel = new CreateBidViewModel();

        viewModel.Bid = bidService.GetById(id, includeBid1: true, includeBid2: true, includeBid3: true, includeBid4: true, includeBid5: true, includeUser: true);

        return SetViewModel(viewModel);
    }

    private CreateBidViewModel SetViewModel(CreateBidViewModel viewModel)
    {
        if (viewModel.Bid == null) return viewModel;

        if (viewModel.Bid.Step1 != null)
        {
            viewModel.Bid.Step1.Role = viewModel.Bid.UserRole;
        }
        if (viewModel.Bid.Step3 != null)
        {
            viewModel.Bid.Step3.Role = viewModel.Bid.UserRole;
        }
        if (viewModel.Bid.Step4 != null)
        {
            viewModel.Bid.Step4.Role = viewModel.Bid.UserRole;
            viewModel.Bid.Step4.Reason = viewModel.Bid.Step3?.ReasonForBid;
            viewModel.Bid.Step4.Service = viewModel.Bid.Service;
        }
        if (viewModel.Bid.Step5 != null)
        {
            viewModel.Bid.Step5.Role = viewModel.Bid.UserRole;
        }

        return viewModel;
    }

    private int GetServiceByInt(string service)
    {
        var types = new[] { TechnologicalConnectionType.UpTo15kW, TechnologicalConnectionType.UpTo150kW, TechnologicalConnectionType.Temporary};

        foreach(var type in types)
        {
            if (type.ToString() == service) return (int)type;
        }
        return -1;
    }
}
