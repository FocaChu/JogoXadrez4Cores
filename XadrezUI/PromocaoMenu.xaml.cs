using LogicaXadrez;
using System.Windows.Controls;
using System.Windows.Input;

namespace XadrezUI
{
    /// <summary>
    /// Interação lógica para PromocaoMenu.xam
    /// </summary>
    public partial class PromocaoMenu : UserControl
    {

        public event Action<PecaTipo> PecaSelecionada;

        public PromocaoMenu(Jogador jogador)
        {
            InitializeComponent();

            ImagemRainha.Source = Imagens.ObterImagem(jogador, PecaTipo.Rainha);
            ImagemBispo.Source = Imagens.ObterImagem(jogador, PecaTipo.Bispo);
            ImagemTorre.Source = Imagens.ObterImagem(jogador, PecaTipo.Torre);
            ImagemCavalo.Source = Imagens.ObterImagem(jogador, PecaTipo.Cavalo);
        }

        private void ImagemRainha_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(PecaTipo.Rainha);
        }

        private void ImagemBispo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(PecaTipo.Bispo);
        }

        private void ImagemTorre_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(PecaTipo.Torre);
        }

        private void ImagemCavalo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(PecaTipo.Cavalo);
        }
    }
}
