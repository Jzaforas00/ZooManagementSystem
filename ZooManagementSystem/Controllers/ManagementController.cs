using Microsoft.AspNetCore.Mvc;
using ZooManagementSystem.Models.ViewModels;
using ZooManagementSystem.Services.Interfaces;

namespace ZooMvc.Controllers;

public class ManagementController : Controller
{
    private readonly IFeedingService _feedingService;
    private readonly IManagementService _managementService;

    public ManagementController(IFeedingService feedingService, IManagementService managementService)
    {
        _feedingService = feedingService;
        _managementService = managementService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var vm = new ManagementDashboardViewModel
        {
            InversionTotalComida = await _managementService.GetFoodInvestmentAsync(cancellationToken),
            CostePorAnimal = await _managementService.GetAnimalCostsAsync(cancellationToken),
        };

        return View(vm);
    }

    public async Task<IActionResult> PedidosSugeridos(CancellationToken cancellationToken)
    {
        var pedidos = await _feedingService.GetFoodOrderSuggestionsAsync(cancellationToken);
        return View(pedidos);
    }
}
