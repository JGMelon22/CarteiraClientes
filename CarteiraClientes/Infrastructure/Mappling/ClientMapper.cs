using CarteiraClientes.ViewModels.Client;
using Riok.Mapperly.Abstractions;

namespace CarteiraClientes.Infrastructure.Mappling;

[Mapper]
public static partial class ClientMapper
{
    public static partial ClientResultViewModel ClientToClientResultViewModel(Client client);
    public static partial Client ClientToClientInputViewModel(ClientInputViewModel client);
    public static partial void ApplyUpdate(ClientInputViewModel clientInput, Client client);
}