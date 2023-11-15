namespace CarteiraClientes.ViewModels.Chart;

public record ChartViewModel
{
    public required string DimensionOne { get; init; }
    public int Quantity { get; init; }
}