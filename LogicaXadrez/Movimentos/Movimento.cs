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

        public abstract Posicao PosicaoOrig { get; }

        public abstract Posicao PosicaoDest { get; }

        public abstract void Executar(Tabuleiro tabuleiro);

        public virtual bool EhLegal(Tabuleiro tabuleiro)
        {
            Jogador jogador = tabuleiro[PosicaoOrig].Cor;
            Tabuleiro clone = tabuleiro.Clone();
            Executar(clone);

            return !clone.EstaEmCheck(jogador);
        }
    }
}
