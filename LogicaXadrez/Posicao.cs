namespace LogicaXadrez
{
    public class Posicao
    {
        public int Linha { get; }

        public int Coluna { get; }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public Jogador CorCasa()
        {
            // Retorna a cor da casa com base na posição
            // Se a soma da linha e coluna for par, é uma casa branca; se ímpar, é uma casa preta
            if ((Linha + Coluna) % 2 == 0)
            {
                return Jogador.Branco;
            }
            else
            {
                return Jogador.Preto;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Posicao posicao &&
                   Linha == posicao.Linha &&
                   Coluna == posicao.Coluna;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Linha, Coluna);
        }

        public static bool operator ==(Posicao left, Posicao right)
        {
            return EqualityComparer<Posicao>.Default.Equals(left, right);
        }

        public static bool operator !=(Posicao left, Posicao right)
        {
            return !(left == right);
        }

        public static Posicao operator +(Posicao posicao, Direcao direcao)
        {
            return new Posicao(posicao.Linha + direcao.LinhaDelta, posicao.Coluna + direcao.ColunaDelta);
        }
    }
}
