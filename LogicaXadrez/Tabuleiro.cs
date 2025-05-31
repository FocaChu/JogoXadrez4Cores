using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaXadrez
{
    public class Tabuleiro
    {
        private readonly Peca[,] pecas = new Peca[8, 8];

        public Peca this[int linha, int coluna]
        {
            get
            {
                if (!EstaDentro(linha, coluna))
                {
                    throw new ArgumentOutOfRangeException("Índices fora do intervalo do tabuleiro.");
                }
                return pecas[linha, coluna];
            }
            set
            {
                if (!EstaDentro(linha, coluna))
                {
                    throw new ArgumentOutOfRangeException("Índices fora do intervalo do tabuleiro.");
                }
                pecas[linha, coluna] = value;
            }
        }

        public Peca this[Posicao posicao]
        {
            get { return this[posicao.Linha, posicao.Coluna]; }

            set { this[posicao.Linha, posicao.Coluna] = value; }
        }

        public static Tabuleiro Iniciar() 
        { 
            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.AdcPecasIniciais();

            return tabuleiro;
        }

        private void AdcPecasIniciais()
        {
            this[0, 0] = new Torre(Jogador.Preto);
            this[0, 1] = new Cavalo(Jogador.Preto);
            this[0, 2] = new Bispo(Jogador.Preto);
            this[0, 3] = new Rainha(Jogador.Preto);
            this[0, 4] = new Rei(Jogador.Preto);
            this[0, 5] = new Bispo(Jogador.Preto);
            this[0, 6] = new Cavalo(Jogador.Preto);
            this[0, 7] = new Torre(Jogador.Preto);


            this[7, 0] = new Torre(Jogador.Branco);
            this[7, 1] = new Cavalo(Jogador.Branco);
            this[7, 2] = new Bispo(Jogador.Branco);
            this[7, 3] = new Rainha(Jogador.Branco);
            this[7, 4] = new Rei(Jogador.Branco);
            this[7, 5] = new Bispo(Jogador.Branco);
            this[7, 6] = new Cavalo(Jogador.Branco);
            this[7, 7] = new Torre(Jogador.Branco);

            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Peao(Jogador.Preto);
                this[6, i] = new Peao(Jogador.Branco);
            }

        }

        public static bool EstaDentro(Posicao posicao)
        {
            return posicao.Linha >= 0 && posicao.Linha < 8 && posicao.Coluna >= 0 && posicao.Coluna < 8;
        }

        public static bool EstaDentro(int linha, int coluna)
        {
            return linha >= 0 && linha < 8 && coluna >= 0 && coluna < 8;
        }

        public bool EstaVazio(Posicao posicao)
        {
            return this[posicao] == null;
        }

        public IEnumerable<Posicao> PosicaoPecas()
        {
            for(int l = 0; l < 8; l++)
            {
                for(int c = 0; c < 8; c++)
                {
                    Posicao posicao = new Posicao(l, c);

                    if(!EstaVazio(posicao))
                    {
                        yield return posicao;
                    }
                }
            }
        }

        public IEnumerable<Posicao> PosicaoPecasPorCor(Jogador cor)
        {
            return PosicaoPecas().Where(posicao => this[posicao].Cor == cor);
        }
    }
}