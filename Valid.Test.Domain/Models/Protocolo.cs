﻿using Valid.Test.Domain.Models.Base;

namespace Valid.Test.Domain.Models
{
    public class Protocolo : Entity<Guid>
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