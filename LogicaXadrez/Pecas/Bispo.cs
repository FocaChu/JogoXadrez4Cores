namespace LogicaXadrez
{
    public class Bispo : Peca
    {
        public override PecaTipo Tipo => PecaTipo.Bispo;

        public override Jogador Cor { get; }

        private static readonly Direcao[] direcoes = new Direcao[]
        {
            Direcao.Nordeste, Direcao.Noroeste,
            Direcao.Sudeste, Direcao.Sudoeste
        };

        public Bispo(Jogador cor)
        {
            Cor = cor;
        }

        public override Peca Clone()
        {
            Bispo clone = new Bispo(Cor);
            clone.SeMoveu = this.SeMoveu;
            return clone;
        }

        public override IEnumerable<Movimento> ObterMovimentos(Posicao posOrigem, Tabuleiro tabuleiro)
        {
            return PosiMovEmDirecoes(posOrigem, tabuleiro, direcoes)
                .Select(dest => new MovimentoNormal(posOrigem, dest));
        }

    }
}