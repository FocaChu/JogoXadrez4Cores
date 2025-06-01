using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaXadrez
{
    public class Rei : Peca
    {
        public override PecaTipo Tipo => PecaTipo.Rei;

        public override Jogador Cor { get; }

        private static readonly Direcao[] direcoes = new Direcao[]
        {
            Direcao.Norte, Direcao.Oeste,
            Direcao.Sul, Direcao.Leste,

            Direcao.Nordeste, Direcao.Noroeste,
            Direcao.Sudeste, Direcao.Sudoeste
        };

        public Rei(Jogador cor)
        {
            Cor = cor;
        }

        public override Peca Clone()
        {
            Rei clone = new Rei(Cor);
            clone.SeMoveu = this.SeMoveu;
            return clone;
        }

        private IEnumerable<Posicao> PosicaoMovimentos(Posicao posOrigem, Tabuleiro tabuleiro)
        {
            foreach (Direcao direcao in direcoes)
            {
                Posicao destino = posOrigem + direcao;

                if (!Tabuleiro.EstaDentro(destino))
                {
                    continue;
                }

                if (tabuleiro.EstaVazio(destino) || tabuleiro[destino].Cor != Cor)
                {
                    yield return destino; // Movimento válido ou captura
                }
            }
        }

        public override IEnumerable<Movimento> ObterMovimentos(Posicao posOrigem, Tabuleiro tabuleiro)
        {
            foreach(Posicao destino in PosicaoMovimentos(posOrigem, tabuleiro))
            {
                yield return new MovimentoNormal(posOrigem, destino);
            }
        }

        public override bool PodeCapturarReiAdv(Posicao posOrigem, Tabuleiro tabuleiro)
        {
            return ObterMovimentos(posOrigem, tabuleiro).Any(des =>
            {
                Peca peca = tabuleiro[des.PosDestino];
                return peca != null && peca.Tipo == PecaTipo.Rei;
            });
        }
    }
}
