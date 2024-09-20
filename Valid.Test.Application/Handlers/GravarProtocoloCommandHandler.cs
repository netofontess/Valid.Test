using AutoMapper;
using Azure.Core;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Valid.Test.Application.Commands;
using Valid.Test.Domain.Models;
using Valid.Test.Domain.Notification;
using Valid.Test.UOW;

namespace Valid.Test.Application.Handlers
{
    public class GravarProtocoloCommandHandler : IRequestHandler<GravarProtocoloCommand>
    {
        private readonly ILogger<GravarProtocoloCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INotificationContext _notificationContext;
        private readonly IEnumerable<IValidator<GravarProtocoloCommand>> _validators;

        public GravarProtocoloCommandHandler(ILogger<GravarProtocoloCommandHandler> logger,
                                             IUnitOfWork unitOfWork,
                                             IMapper mapper,
                                             INotificationContext notificationContext,
                                             IEnumerable<IValidator<GravarProtocoloCommand>> validators)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationContext = notificationContext;
            _validators = validators;
        }

        public async Task Handle(GravarProtocoloCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GravarProtocolo object: {Request}", request);

            var isValid = await Validate(request);
            if (!isValid)
                return;

            var protocolo = _mapper.Map<Protocolo>(request);

            await _unitOfWork.ProtocoloRepository.Insert(protocolo);
            await _unitOfWork.Commit();
        }

        private async Task<bool> Validate(GravarProtocoloCommand request)
        {
            var protocoloExistente = await _unitOfWork.ProtocoloRepository.FindByNumeroProtocoloAsync(request.NumeroProtocolo);
            if (protocoloExistente != null)
            {
                _notificationContext.AddNotification("NumeroProtocolo", "Número de protocolo já existe.");
                return false;
            }

            var protocoloComCpfRgExistente = await _unitOfWork.ProtocoloRepository.FindByCpfRgAndNumeroViaAsync(request.Cpf, request.Rg, request.NumeroVia);
            if (protocoloComCpfRgExistente != null)
            {
                _notificationContext.AddNotification("CPF/RG/NumeroVia", "O CPF ou RG já possuem um protocolo com o mesmo número de via.");
                return false;
            }

            return true;
        }
        
        
        public async Task PublicarProtocolo(IEnumerable<GravarProtocoloCommand> protocolosDto)
        {
            _logger.LogInformation("PublicarProtocolo object: {ProtocoloDto}", protocolosDto);
        }

        public async Task ConsumirProtocolo(GravarProtocoloCommand protocoloDto)
        {
            _logger.LogInformation("ConsumirProtocolo object: {ProtocoloDto}", protocoloDto);
        }
    }
}
