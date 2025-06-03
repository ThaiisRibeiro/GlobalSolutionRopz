namespace GlobalSolutionRopz.DTO
{
    public class VerificacaoAlertaDTO
    {
        public float temperatura { get; set; }
        public string estado { get; set; } = string.Empty;
        public float mes { get; set; }
        public float diaDaSemana { get; set; }
        public float ano { get; set; }
    }
}
