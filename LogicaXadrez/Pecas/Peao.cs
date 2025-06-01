using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaXadrez
{
    public class Peao : Peca
    {
        public override PecaTipo Tipo => PecaTipo.Peao;

        public override Jogador Cor { get; }

        private readonly Direcao frente;

        public Peao(Jogador cor)
        {
            Cor = cor;

            if (Cor == Jogador.Branco)
            {
                frente = Direcao.Norte;
            }
            else if (Cor == Jogador.Preto)
            {
                frente = Direcao.Sul;
            }
            else
            {
                throw new ArgumentException("Cor inválida para o Peão.");
            }
        }

        public override Peca Clone()
        {
            Peao clone = new Peao(Cor);
            clone.SeMoveu = this.SeMoveu;

            return clone;
        }

        private static bool PodeMoverPara(Posicao posicao, Tabuleiro tabuleiro)
        {
            return Tabuleiro.EstaDentro(posicao) && tabuleiro.EstaVazio(posicao);
        }

        private bool PodeCapturar(Posicao posicao, Tabuleiro tabuleiro)
        {
            if (!Tabuleiro.EstaDentro(posicao) || tabuleiro.EstaVazio(posicao))
            {
                return false;
            }

            return tabuleiro[posicao].Cor != Cor;
        }

        private static IEnumerable<Movimento> MovimentosPromocao(Posicao posOrigem, Posicao posDestino)
        {
            yield return new PromocaoPeao(posOrigem, posDestino, PecaTipo.Cavalo);
            yield return new PromocaoPeao(posOrigem, posDestino, PecaTipo.Bispo);
            yield return new PromocaoPeao(posOrigem, posDestino, PecaTipo.Torre);
            yield return new PromocaoPeao(posOrigem, posDestino, PecaTipo.Rainha);
        }

        private IEnumerable<Movimento> MovimentosFrente(Posicao posOrigem, Tabuleiro tabuleiro)
        {
            Posicao movUnico = posOrigem + frente;

            if (PodeMoverPara(movUnico, tabuleiro))
            {
                if (movUnico.Linha == 0 || movUnico.Linha == 7)
                {
                    foreach(Movimento movimentoPromocao in MovimentosPromocao(posOrigem, movUnico))
                    {
                        yield return movimentoPromocao;
                    }
                }
                else
                {
                    yield return new MovimentoNormal(posOrigem, movUnico);
                }

                Posicao movDuplo = movUnico + frente;

                if (!SeMoveu && PodeMoverPara(movDuplo, tabuleiro))
                {
                    yield return new MovimentoNormal(posOrigem, movDuplo);
                }

            }
        }

        private IEnumerable<Movimento> MovimentosDiagonal(Posicao posOrigem, Tabuleiro tabuleiro)
        {
            foreach (Direcao dir in new Direcao[] { Direcao.Oeste, Direcao.Leste })
            {
                Posicao destino = posOrigem + frente + dir;

                if (PodeCapturar(destino, tabuleiro))
                {
                    if (destino.Linha == 0 || destino.Linha == 7)
                    {
                        foreach (Movimento movimentoPromocao in MovimentosPromocao(posOrigem, destino))
                        {
                            yield return movimentoPromocao;
                        }
                    }
                    else
                    {
                        yield return new MovimentoNormal(posOrigem, destino);
                    }
                }
            }
        }

        public override IEnumerable<Movimento> ObterMovimentos(Posicao posOrigem, Tabuleiro tabuleiro)
        {
            return MovimentosFrente(posOrigem, tabuleiro)
                .Concat(MovimentosDiagonal(posOrigem, tabuleiro));
        }

        public override bool PodeCapturarReiAdv(Posicao posOrigem, Tabuleiro tabuleiro)
        {
            return MovimentosDiagonal(posOrigem, tabuleiro).Any(mov =>
            {
                Peca peca = tabuleiro[mov.PosDestino];
                return peca != null && peca.Tipo == PecaTipo.Rei;
            });
        }
    }
}
