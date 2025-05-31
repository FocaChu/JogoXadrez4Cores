using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace LogicaXadrez
{
    public abstract class Peca
    {
        public abstract PecaTipo Tipo { get; }

        public abstract Jogador Cor { get; }

        public bool SeMoveu { get; set; } = false;

        public abstract Peca Clone();

        public abstract IEnumerable<Movimento> ObterMovimentos(Posicao posOrigem, Tabuleiro tabuleiro);

        protected IEnumerable<Posicao> PosiMovEmDirecao(Posicao posOrigem, Tabuleiro tabuleiro, Direcao dir)
        {
            for(Posicao pos = posOrigem + dir; Tabuleiro.EstaDentro(pos); pos += dir)
            {
                if (tabuleiro.EstaVazio(pos))
                {
                    yield return pos;
                    continue;
                }

                Peca peca = tabuleiro[pos];

                if(peca.Cor != Cor)
                {
                    yield return pos; // Captura a peça adversária
                }

                yield break; // Interrompe a iteração se encontrar uma peça
            }
        }

        protected IEnumerable<Posicao> PosiMovEmDirecoes(Posicao posOrigem, Tabuleiro tabuleiro, Direcao[] direcoes)
        {
            return direcoes.SelectMany(dir => PosiMovEmDirecao(posOrigem, tabuleiro, dir));
        }

        public virtual bool PodeCapturarReiAdv(Posicao posOrigem, Tabuleiro tabuleiro)
        {
            return ObterMovimentos(posOrigem, tabuleiro).Any(mov =>
            {
                Peca peca = tabuleiro[mov.PosicaoDest];
                return peca != null && peca.Tipo == PecaTipo.Rei;
            });
        }
    }
}
