namespace LogicaXadrez
{
    public class Torre : Peca
    {
        public override PecaTipo Tipo => PecaTipo.Torre;

        public override Jogador Cor { get; }

        private static readonly Direcao[] direcoes = new Direcao[]
        {
            Direcao.Norte, Direcao.Oeste,
            Direcao.Sul, Direcao.Leste
        };

        public Torre(Jogador cor)
        {
            Cor = cor;
        }

        public override Peca Clone()
        {
            Torre clone = new Torre(Cor);
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
