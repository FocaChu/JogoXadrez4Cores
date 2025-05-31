namespace LogicaXadrez
{
    public class Cavalo : Peca
    {
        public override PecaTipo Tipo => PecaTipo.Cavalo;

        public override Jogador Cor { get; }

        public Cavalo(Jogador cor)
        {
            Cor = cor;
        }

        public override Peca Clone()
        {
            Cavalo clone = new Cavalo(Cor);
            clone.SeMoveu = this.SeMoveu;
            return clone;
        }

        private static IEnumerable<Posicao> PosicoesPotenciais(Posicao posOrigem)
        {
            foreach (Direcao verticalDir in new Direcao[] { Direcao.Norte, Direcao.Sul })
            {
                foreach (Direcao horizontalDir in new Direcao[] { Direcao.Leste, Direcao.Oeste })
                {
                    yield return posOrigem + 2 * verticalDir + horizontalDir;
                    yield return posOrigem + 2 * horizontalDir + verticalDir;
                }
            }
        }

        private IEnumerable<Posicao> PosicoesMovimento(Posicao posOrigem, Tabuleiro tabuleiro)
        {
            return PosicoesPotenciais(posOrigem)
                .Where(pos => Tabuleiro.EstaDentro(pos) && (tabuleiro.EstaVazio(pos) || tabuleiro[pos].Cor != Cor));
        }

        public override IEnumerable<Movimento> ObterMovimentos(Posicao posOrigem, Tabuleiro tabuleiro)
        {
            return PosicoesMovimento(posOrigem, tabuleiro)
                .Select(destino => new MovimentoNormal(posOrigem, destino));
        }

    }
}
