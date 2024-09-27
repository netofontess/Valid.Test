using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Valid.Test.Application.Amqp.MessageObjects;
using Valid.Test.Application.Amqp.Publisher;
using Valid.Test.Application.UseCases.ProtocoloCase.Create;
using Valid.Test.Domain.Models;
using Valid.Test.Domain.Notification;
using Valid.Test.UOW;

namespace Valid.Test.Application.Tests.Handlers
{
    public class GravarProtocoloCommandHandlerTests
    {
        private readonly Mock<ILogger<GravarProtocoloCommandHandler>> _loggerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<INotificationContext> _notificationContextMock;
        private readonly Mock<IPublisherService> _publisherServiceMock;
        private readonly GravarProtocoloCommandHandler _handler;

        public GravarProtocoloCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<GravarProtocoloCommandHandler>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _notificationContextMock = new Mock<INotificationContext>();
            _publisherServiceMock = new Mock<IPublisherService>();

            _handler = new GravarProtocoloCommandHandler(
                _loggerMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _notificationContextMock.Object,
                _publisherServiceMock.Object
            );
        }

        [Fact]
        public async Task Handle_ProtocoloJaExiste_DeveAdicionarNotificacao()
        {
            // Arrange
            var command = new GravarProtocoloCommand
            {
                NumeroProtocolo = "12345",
                Cpf = "11111111111",
                Rg = "1234567",
                NumeroVia = 1
            };

            _unitOfWorkMock.Setup(x => x.ProtocoloRepository.ConsultaPorNumeroProtocoloAsync(It.IsAny<string>()))
                .ReturnsAsync(new Protocolo());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _notificationContextMock.Verify(x => x.AddNotification("NumeroProtocolo", "Número de protocolo já existe."), Times.Once);
            _publisherServiceMock.Verify(x => x.PublicarProtocoloAsync(It.IsAny<GravarProtocoloMessage>()), Times.Never);
        }

        [Fact]
        public async Task Handle_CpfRgComNumeroViaJaExiste_DeveAdicionarNotificacao()
        {
            // Arrange
            var command = new GravarProtocoloCommand
            {
                NumeroProtocolo = "12345",
                Cpf = "11111111111",
                Rg = "1234567",
                NumeroVia = 1
            };

            _unitOfWorkMock.Setup(x => x.ProtocoloRepository.ConsultaPorCpfRgEhNumeroViaAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new Protocolo());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _notificationContextMock.Verify(x => x.AddNotification("CPF/RG/NumeroVia", "O CPF ou RG já possuem um protocolo com o mesmo número de via."), Times.Once);
            _publisherServiceMock.Verify(x => x.PublicarProtocoloAsync(It.IsAny<GravarProtocoloMessage>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ProtocoloValido_DevePublicarProtocolo()
        {
            // Arrange
            var command = new GravarProtocoloCommand
            {
                NumeroProtocolo = "12345",
                Cpf = "11111111111",
                Rg = "1234567",
                NumeroVia = 1
            };

            _unitOfWorkMock.Setup(x => x.ProtocoloRepository.ConsultaPorNumeroProtocoloAsync(It.IsAny<string>()))
                .ReturnsAsync((Protocolo)null);

            _unitOfWorkMock.Setup(x => x.ProtocoloRepository.ConsultaPorCpfRgEhNumeroViaAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync((Protocolo)null);

            _mapperMock.Setup(x => x.Map<GravarProtocoloMessage>(It.IsAny<GravarProtocoloCommand>()))
                .Returns(new GravarProtocoloMessage());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _publisherServiceMock.Verify(x => x.PublicarProtocoloAsync(It.IsAny<GravarProtocoloMessage>()), Times.Once);
            _notificationContextMock.Verify(x => x.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }

}