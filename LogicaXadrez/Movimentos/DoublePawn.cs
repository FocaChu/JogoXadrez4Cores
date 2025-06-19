namespace LogicaXadrez
{
    public class DoublePawn : Movimento
    {
        public override MovimentoTipo Tipo => MovimentoTipo.DoublePawn;

        public override Posicao PosDestino { get; }
        public override Posicao PosOrigem { get; }

        private readonly Posicao posicaoPulada;

        public DoublePawn(Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            PosOrigem = posicaoOrigem;
            PosDestino = posicaoDestino;
            posicaoPulada = new Posicao((posicaoOrigem.Linha + posicaoDestino.Linha) / 2, (posicaoOrigem.Coluna));
        }

        public override void Executar(Tabuleiro tabuleiro)
        {
            Jogador jogador = tabuleiro[PosOrigem].Cor;
            tabuleiro.DefinirPosicaoPeaoPulo(jogador, posicaoPulada);
            new MovimentoNormal(PosOrigem, PosDestino).Executar(tabuleiro);
        }
    }
}
