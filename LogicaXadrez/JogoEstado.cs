using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaXadrez
{
    public class JogoEstado
    {
        public Tabuleiro Tabuleiro { get; }

        public Jogador JogadorAtual { get; private set; }

        public JogoEstado(Tabuleiro tabuleiro, Jogador jogador)
        {
            this.Tabuleiro = tabuleiro;
            this.JogadorAtual = jogador;
        }

        public IEnumerable<Movimento> ObterMovimentosLegaisPorPeca(Posicao posicao)
        {
            if(Tabuleiro.EstaVazio(posicao) || Tabuleiro[posicao].Cor != JogadorAtual)
            {
                return Enumerable.Empty<Movimento>();
            }

            Peca peca = Tabuleiro[posicao];
            return peca.ObterMovimentos(posicao, Tabuleiro);

        }

        public void MoverPeca(Movimento movimento)
        {
            movimento.Executar(Tabuleiro);
            JogadorAtual = JogadorAtual.Oponente(); // Alterna o jogador atual após o movimento
        }
    }
}