﻿namespace Fortificar.Models
{
    public class Parametro
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public float? ValorMax { get; set; }
        public float? ValorMin { get; set; }
        public int? Tipo { get; set; }
        public string? Ativo { get; set; }
    }
}
