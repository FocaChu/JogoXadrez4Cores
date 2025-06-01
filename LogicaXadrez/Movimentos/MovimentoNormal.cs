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

        public override Posicao PosOrigem { get; }

        public override Posicao PosDestino { get; }

        public MovimentoNormal(Posicao posicaoOrig, Posicao posicaoDest)
        {
            PosOrigem = posicaoOrig;
            PosDestino = posicaoDest;
        }
        public override void Executar(Tabuleiro tabuleiro)
        {
            Peca peca = tabuleiro[PosOrigem]; // Obtém a peça na posição original

            tabuleiro[PosDestino] = peca; // Move a peça para a nova posição

            tabuleiro[PosOrigem] = null; // Remove a peça da posição original

            peca.SeMoveu = true; // Marca a peça como movida
        }
    }
}
