namespace LogicaXadrez
{
    public class EnPassant : Movimento
    {
        public override MovimentoTipo Tipo => MovimentoTipo.EnPassant;

        public override Posicao PosDestino { get; }

        public override Posicao PosOrigem { get; }

        private readonly Posicao posCaptura;

        public EnPassant(Posicao posOrigem, Posicao posDestino)
        {
            PosOrigem = posOrigem;
            PosDestino = posDestino;
            this.posCaptura = new Posicao(posOrigem.Linha, posDestino.Coluna);
        }

        public override void Executar(Tabuleiro tabuleiro)
        {
            new MovimentoNormal(PosOrigem, PosDestino).Executar(tabuleiro);
            tabuleiro[posCaptura] = null;
        }
    }
}
