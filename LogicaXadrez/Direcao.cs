namespace LogicaXadrez
{
    public class Direcao
    {
        public readonly static Direcao Norte = new Direcao(-1, 0);

        public readonly static Direcao Sul = new Direcao(1, 0);

        public readonly static Direcao Oeste = new Direcao(0, -1);

        public readonly static Direcao Leste = new Direcao(0, 1);

        public readonly static Direcao Nordeste = Norte + Leste;

        public readonly static Direcao Noroeste = Norte + Oeste;

        public readonly static Direcao Sudeste = Sul + Leste;

        public readonly static Direcao Sudoeste = Sul + Oeste;


        public int LinhaDelta { get; }

        public int ColunaDelta { get; }

        public Direcao(int linhaDelta, int colunaDelta)
        {
            LinhaDelta = linhaDelta;
            ColunaDelta = colunaDelta;
        }

        public static Direcao operator +(Direcao d1, Direcao d2)
        {
            return new Direcao(d1.LinhaDelta + d2.LinhaDelta, d1.ColunaDelta + d2.ColunaDelta);
        }

        public static Direcao operator *(int multiplicador, Direcao d)
        {
            return new Direcao(d.LinhaDelta * multiplicador, d.ColunaDelta * multiplicador);
        }
    }
}
