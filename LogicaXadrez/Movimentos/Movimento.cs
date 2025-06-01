using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaXadrez
{
    public abstract class Movimento
    {
        public abstract MovimentoTipo Tipo { get; }

        public abstract Posicao PosOrigem { get; }

        public abstract Posicao PosDestino { get; }

        public abstract void Executar(Tabuleiro tabuleiro);

        public virtual bool EhLegal(Tabuleiro tabuleiro)
        {
            Jogador jogador = tabuleiro[PosOrigem].Cor;
            Tabuleiro clone = tabuleiro.Clone();
            Executar(clone);

            return !clone.EstaEmCheck(jogador);
        }
    }
}
