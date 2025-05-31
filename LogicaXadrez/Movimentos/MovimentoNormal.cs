using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaXadrez
{
    public class MovimentoNormal : Movimento
    {
        public override MovimentoTipo Tipo => MovimentoTipo.Normal;

        public override Posicao PosicaoOrig { get; }

        public override Posicao PosicaoDest { get; }

        public MovimentoNormal(Posicao posicaoOrig, Posicao posicaoDest)
        {
            PosicaoOrig = posicaoOrig;
            PosicaoDest = posicaoDest;
        }
        public override void Executar(Tabuleiro tabuleiro)
        {
            Peca peca = tabuleiro[PosicaoOrig]; // Obtém a peça na posição original

            tabuleiro[PosicaoDest] = peca; // Move a peça para a nova posição

            tabuleiro[PosicaoOrig] = null; // Remove a peça da posição original

            peca.SeMoveu = true; // Marca a peça como movida
        }
    }
}
