namespace LogicaXadrez
{
    public enum Jogador
    {
        Nenhum,
        Branco,
        Preto
    }

    public static class JogadorExtensions
    {
        public static Jogador Oponente(this Jogador jogador)
        {
            return jogador switch
            {
                Jogador.Branco => Jogador.Preto,
                Jogador.Preto => Jogador.Branco,
                _ => Jogador.Nenhum,
            };
        }
    }
}
