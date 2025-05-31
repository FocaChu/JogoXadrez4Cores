using System.Windows.Media;
using System.Windows.Media.Imaging;
using LogicaXadrez;

namespace XadrezUI
{
    public static class Imagens
    {
        private static readonly Dictionary<PecaTipo, ImageSource> brancoSource =  new()
        {
            { PecaTipo.Peao, CarregarImagem("Assets/PeaoBranco.png") },
            { PecaTipo.Bispo, CarregarImagem("Assets/BispoBranco.png") },
            { PecaTipo.Cavalo, CarregarImagem("Assets/CavaloBranco.png") },
            { PecaTipo.Torre, CarregarImagem("Assets/TorreBranca.png") },
            { PecaTipo.Rainha, CarregarImagem("Assets/RainhaBranca.png") },
            { PecaTipo.Rei, CarregarImagem("Assets/ReiBranco.png")}
        };

        private static readonly Dictionary<PecaTipo, ImageSource> pretoSource = new()
        {
            { PecaTipo.Peao, CarregarImagem("Assets/PeaoPreto.png") },
            { PecaTipo.Bispo, CarregarImagem("Assets/BispoPreto.png") },
            { PecaTipo.Cavalo, CarregarImagem("Assets/CavaloPreto.png") },
            { PecaTipo.Torre, CarregarImagem("Assets/TorrePreta.png") },
            { PecaTipo.Rainha, CarregarImagem("Assets/RainhaPreta.png") },
            { PecaTipo.Rei, CarregarImagem("Assets/ReiPreto.png")}
        };


        private static ImageSource CarregarImagem(string caminhoArquivo)
        {
            return new BitmapImage(new Uri(caminhoArquivo, UriKind.Relative));
        }

        public static ImageSource ObterImagem(Jogador cor, PecaTipo tipo)
        {
            return cor switch
            {
                Jogador.Branco => brancoSource[tipo],
                Jogador.Preto => pretoSource[tipo],
                _ => throw new ArgumentException("Cor inválida", nameof(cor)),
            };
        }

        public static ImageSource ObterImagem(Peca peca)
        {
            if (peca == null)
                return null;
            return ObterImagem(peca.Cor, peca.Tipo);
        }
    }
}
