using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaXadrez
{
    public class PromocaoPeao : Movimento
    {
        public override MovimentoTipo Tipo => MovimentoTipo.PromacaoPeao;

        public override Posicao PosOrigem { get; }

        public override Posicao PosDestino { get; }

        private readonly PecaTipo novoTipo;

        public PromocaoPeao(Posicao posOrigem, Posicao posDestino, PecaTipo novoTipo)
        {
            PosOrigem = posOrigem;
            PosDestino = posDestino;
            this.novoTipo = novoTipo;
        }

        private Peca CriarPecaPromovida(Jogador cor)
        {
            return novoTipo switch
            {
                PecaTipo.Rainha => new Rainha(cor),
                PecaTipo.Torre => new Torre(cor),
                PecaTipo.Cavalo => new Cavalo(cor),
                PecaTipo.Bispo => new Bispo(cor),
                _ => throw new ArgumentException("Tipo de peça inválido para promoção.")
            };
        }

        public override void Executar(Tabuleiro tabuleiro)
        {
            Peca peao = tabuleiro[PosOrigem];

            tabuleiro[PosOrigem] = null; // Remove o peão da posição original   

            Peca pecaPromovida = CriarPecaPromovida(peao.Cor);
            pecaPromovida.SeMoveu = true; // Marca a peça promovida como já tendo se movido

            tabuleiro[PosDestino] = pecaPromovida; // Coloca a peça promovida na nova posição
        }

    }
}
