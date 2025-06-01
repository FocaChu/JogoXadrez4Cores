namespace LogicaXadrez
{
    public class Resultado
    {
        public Jogador Vencedor { get; }

        public FimMotivo Motivo { get; }

        public Resultado(Jogador vencedor, FimMotivo motivo)
        {
            Vencedor = vencedor;

            Motivo = motivo;
        }

        public static Resultado Vitoria(Jogador vencedor)
        {
            return new Resultado(vencedor, FimMotivo.Checkmate);
        }

        public static Resultado Empate(FimMotivo motivo)
        {
            return new Resultado(Jogador.Nenhum, motivo);
        }
    }
}
