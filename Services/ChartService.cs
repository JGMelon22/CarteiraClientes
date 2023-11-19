using CarteiraClientes.ViewModels.Chart;

namespace CarteiraClientes.Services;

public class ChartService
{
    private readonly AppDbContext _dbContext;

    public ChartService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ChartViewModel>> GenerateChart()
    {
        var listModel = new List<ChartViewModel>();
        listModel.Add(new ChartViewModel
        {
            DimensionOne = "Empresas",
            Quantity = await _dbContext.Companies.CountAsync()
        });

        listModel.Add(new ChartViewModel
        {
            DimensionOne = "Clientes",
            Quantity = await _dbContext.Clients.CountAsync()
        });

        listModel.Add(new ChartViewModel
        {
            DimensionOne = "Cliente Inadimpletes",
            Quantity = await _dbContext.Clients
                .Where(x => x.IsOverdue == true)
                .CountAsync()
        });

        return listModel;
    }
}