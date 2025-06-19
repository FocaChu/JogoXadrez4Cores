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

        public Resultado Resultado { get; private set; } = null;

        public JogoEstado(Tabuleiro tabuleiro, Jogador jogador)
        {
            this.Tabuleiro = tabuleiro;
            this.JogadorAtual = jogador;
        }

        public IEnumerable<Movimento> ObterMovimentosLegaisPorPeca(Posicao posicao)
        {
            if (Tabuleiro.EstaVazio(posicao) || Tabuleiro[posicao].Cor != JogadorAtual)
            {
                return Enumerable.Empty<Movimento>();
            }

            Peca peca = Tabuleiro[posicao];
            IEnumerable<Movimento> movCandidatos = peca.ObterMovimentos(posicao, Tabuleiro);

            return movCandidatos.Where(mov => mov.EhLegal(Tabuleiro));

        }

        public void RealizarTurno(Movimento movimento)
        {
            Tabuleiro.DefinirPosicaoPeaoPulo(JogadorAtual, null);
            movimento.Executar(Tabuleiro);
            JogadorAtual = JogadorAtual.Oponente(); 
            VerificarFimDeJogo();
        }

        public IEnumerable<Movimento> ObterMovimentosLegaisPorJogador(Jogador jogador)
        {
            IEnumerable<Movimento> movimentosCandidatos = Tabuleiro.PosicaoPecasPorCor(jogador).SelectMany(pos =>
            {
                Peca peca = Tabuleiro[pos];
                return peca.ObterMovimentos(pos, Tabuleiro);
            });

            return movimentosCandidatos.Where(mov => mov.EhLegal(Tabuleiro));
        }

        private void VerificarFimDeJogo()
        {
            if (!ObterMovimentosLegaisPorJogador(JogadorAtual).Any())
            {
                if (Tabuleiro.EstaEmCheck(JogadorAtual))
                {
                    Resultado = Resultado.Vitoria(JogadorAtual.Oponente()); // Checkmate
                }
                else
                {
                    Resultado = Resultado.Empate(FimMotivo.Stalemate); // Xeque-mate
                }
            }
        }

        public bool FimDeJogo()
        {
            return Resultado != null;
        }
    }
}