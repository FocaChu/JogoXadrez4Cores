namespace LogicaXadrez
{
    public class Rainha : Peca
    {
        public override PecaTipo Tipo => PecaTipo.Rainha;

        public override Jogador Cor { get; }

        private static readonly Direcao[] direcoes = new Direcao[]
        {
            Direcao.Norte, Direcao.Oeste,
            Direcao.Sul, Direcao.Leste,

            Direcao.Nordeste, Direcao.Noroeste,
            Direcao.Sudeste, Direcao.Sudoeste
        };

        public Rainha(Jogador cor)
        {
            Cor = cor;
        }

        public override Peca Clone()
        {
            Rainha clone = new Rainha(Cor);
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
