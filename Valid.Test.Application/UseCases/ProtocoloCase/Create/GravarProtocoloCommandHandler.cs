using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Valid.Test.Application.Amqp.MessageObjects;
using Valid.Test.Application.Amqp.Publisher;
using Valid.Test.Domain.Notification;
using Valid.Test.UOW;

namespace Valid.Test.Application.UseCases.ProtocoloCase.Create
{
    public class GravarProtocoloCommandHandler(ILogger<GravarProtocoloCommandHandler> logger,
                                         IUnitOfWork unitOfWork,
                                         IMapper mapper,
                                         INotificationContext notificationContext,
                                         IPublisherService publisherService) : IRequestHandler<GravarProtocoloCommand>
    {
        private readonly ILogger<GravarProtocoloCommandHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly INotificationContext _notificationContext = notificationContext;
        private readonly IPublisherService _publisherService = publisherService;

        public async Task Handle(GravarProtocoloCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GravarProtocolo object: {Request}", request);

            var isValid = await Validate(request);
            if (!isValid)
                return;

            var protocoloMessage = _mapper.Map<GravarProtocoloMessage>(request);

            await _publisherService.PublicarProtocoloAsync(protocoloMessage);
        }

        private async Task<bool> Validate(GravarProtocoloCommand request)
        {
            var protocoloExistente = await _unitOfWork.ProtocoloRepository.ConsultaPorNumeroProtocoloAsync(request.NumeroProtocolo);
            if (protocoloExistente != null)
            {
                _notificationContext.AddNotification("NumeroProtocolo", "Número de protocolo já existe.");
                return false;
            }

            var protocoloComCpfRgExistente = await _unitOfWork.ProtocoloRepository.ConsultaPorCpfRgEhNumeroViaAsync(request.Cpf, request.Rg, request.NumeroVia);
            if (protocoloComCpfRgExistente != null)
            {
                _notificationContext.AddNotification("CPF/RG/NumeroVia", "O CPF ou RG já possuem um protocolo com o mesmo número de via.");
                return false;
            }

            return true;
        }
    }
}
