using MediatR;
using Microsoft.AspNetCore.Http;

namespace Valid.Test.Application.UseCases.ProtocoloCase.Create
{
    public class GravarProtocoloCommand : IRequest
    {
        public string? NumeroProtocolo { get; set; }
        public int NumeroVia { get; set; }
        public string? Cpf { get; set; }
        public string? Rg { get; set; }
        public string? Nome { get; set; }
        public string? NomeMae { get; set; }
        public string? NomePai { get; set; }
        public IFormFile? Foto { get; set; }
    }
}
