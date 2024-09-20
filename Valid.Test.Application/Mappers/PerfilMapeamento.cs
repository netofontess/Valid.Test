using AutoMapper;
using Microsoft.AspNetCore.Http;
using Valid.Test.Application.Commands;
using Valid.Test.Domain.Models;

namespace Valid.Test.Application.Mappers
{
    public class PerfilMapeamento : Profile
    {
        public PerfilMapeamento()
        {
            CreateMap<Protocolo, GravarProtocoloCommand>();
            CreateMap<GravarProtocoloCommand, Protocolo>()
                .ForMember(dest => dest.Foto, opt => opt.MapFrom(src => ConvertIFormFileToByteArray(src.Foto)));
        }

        private static byte[]? ConvertIFormFileToByteArray(IFormFile? formFile)
        {
            if (formFile == null)
            {
                return null;
            }

            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
