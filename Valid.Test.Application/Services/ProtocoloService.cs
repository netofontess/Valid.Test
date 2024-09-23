using AutoMapper;
using Microsoft.Extensions.Logging;
using Valid.Test.Application.Amqp.MessageObjects;
using Valid.Test.Application.Services.Interfaces;
using Valid.Test.Domain.Models;
using Valid.Test.UOW;

namespace Valid.Test.Application.Services
{
    public class ProtocoloService : IProtocoloService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProtocoloService> _logger;

        public ProtocoloService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProtocoloService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task GravarProtocoloAsync(GravarProtocoloMessage gravarProtocoloMessage)
        {
            try
            {
                var protocolo = _mapper.Map<Protocolo>(gravarProtocoloMessage);

                _logger.LogInformation("Gravando protocolo no banco de dados: {Protocolo}", protocolo);

                await _unitOfWork.ProtocoloRepository.Insert(protocolo);
                await _unitOfWork.Commit();
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gravar protocolo: {Exception}", ex.Message);

                throw;
            }
        }

        public async Task ConsultarProtocolo(GravarProtocoloMessage gravarProtocoloMessage)
        {
            try
            {
                var protocolo = _mapper.Map<Protocolo>(gravarProtocoloMessage);

                _logger.LogInformation("Gravando protocolo no banco de dados: {Protocolo}", protocolo);

                await _unitOfWork.ProtocoloRepository.Insert(protocolo);
                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gravar protocolo: {Exception}", ex.Message);

                throw;
            }
        }
    }
}
