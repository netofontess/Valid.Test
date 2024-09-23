﻿namespace Valid.Test.Application.Amqp.MessageObjects
{
    public class GravarProtocoloMessage
    {
        public string? NumeroProtocolo { get; set; }
        public int NumeroVia { get; set; }
        public string? Cpf { get; set; }
        public string? Rg { get; set; }
        public string? Nome { get; set; }
        public string? NomeMae { get; set; }
        public string? NomePai { get; set; }
        public byte[]? Foto { get; set; }
    }
}
