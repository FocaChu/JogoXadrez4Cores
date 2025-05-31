using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicaXadrez;

namespace XadrezUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int qtdColunasTabuleiro = 8;
        private static int qtdLinhasTabuleiro = 8;

        private readonly Image[,] pecasImagens = new Image[qtdColunasTabuleiro, qtdLinhasTabuleiro];

        private readonly Rectangle[,] destaques = new Rectangle[qtdColunasTabuleiro, qtdLinhasTabuleiro];

        private readonly Dictionary<Posicao, Movimento>  movimentoCache = new Dictionary<Posicao, Movimento>();

        private JogoEstado jogoEstado;

        private Posicao posSelecionada = null;

        public MainWindow()
        {
            InitializeComponent();
            IniciarTabuleiro();

            jogoEstado = new JogoEstado(Tabuleiro.Iniciar(), Jogador.Branco);
            DesenharTabuleiro(jogoEstado.Tabuleiro);

            DefinirCursor(jogoEstado.JogadorAtual);
        }

        private void IniciarTabuleiro()
        {
            for (int i = 0; i < qtdColunasTabuleiro; i++)
            {
                for (int j = 0; j < qtdLinhasTabuleiro; j++)
                {
                    Image image = new Image();
                    pecasImagens[i, j] = image;
                    PecasGrid.Children.Add(image);

                    Rectangle destaque = new Rectangle();
                    destaques[i, j] = destaque;
                    DestaqueGrid.Children.Add(destaque);
                }
            }
        }

        private void DesenharTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < qtdColunasTabuleiro; i++)
            {
                for (int j = 0; j < qtdLinhasTabuleiro; j++)
                {
                    Peca peca = tabuleiro[i, j];
                    pecasImagens[i, j].Source = Imagens.ObterImagem(peca);
                }
            }
        }

        private void TabuleiroGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point ponto = e.GetPosition(TabuleiroGrid);
            Posicao posicao = ParaPosicaoQuadrado(ponto);

            if(posSelecionada == null)
            {
                SelecionarOrigem(posicao);
            }
            else
            {
                SelecionarDestino(posicao);
            }
        }

        private Posicao ParaPosicaoQuadrado(Point ponto)
        {
            double larguraQuadrado = TabuleiroGrid.ActualWidth / qtdLinhasTabuleiro;

            int linha = (int)(ponto.Y / larguraQuadrado);

            int coluna = (int)(ponto.X / larguraQuadrado);

            return new Posicao(linha, coluna);
        }

        private void SelecionarOrigem(Posicao posicao)
        {
            IEnumerable<Movimento> movimentos = jogoEstado.ObterMovimentosLegaisPorPeca(posicao);

            if(movimentos.Any())
            {
                posSelecionada = posicao;
                CacheMovimentos(movimentos);
                MostrarDestaques();
            }
        }

        private void SelecionarDestino(Posicao posicao)
        {
            posSelecionada = null;
            EsconderDestaques();

            if(movimentoCache.TryGetValue(posicao, out Movimento movimento))
            {
                RealizarMovimento(movimento);
            }
        }

        private void RealizarMovimento(Movimento movimento)
        {
            jogoEstado.MoverPeca(movimento);

            DesenharTabuleiro(jogoEstado.Tabuleiro);

            DefinirCursor(jogoEstado.JogadorAtual);
        }

        private void CacheMovimentos(IEnumerable<Movimento> movimentos)
        {
            movimentoCache.Clear();

            foreach(Movimento mov in movimentos)
            {
                movimentoCache[mov.PosicaoDest] = mov;
            }
        }

        private void MostrarDestaques()
        {
            Color color = Color.FromArgb(150, 125, 255, 155); // Verde claro semi transparente

            foreach(Posicao destino in movimentoCache.Keys)
            {
                destaques[destino.Linha, destino.Coluna].Fill = new SolidColorBrush(color);
            }
        }

        private void EsconderDestaques()
        {
            foreach (Posicao destino in movimentoCache.Keys)
            {
                destaques[destino.Linha, destino.Coluna].Fill = Brushes.Transparent;
            }
        }

        private void DefinirCursor(Jogador jogador)
        {
            if(jogador == Jogador.Branco)
            {
                Cursor = CursoresXadrez.CursorBranco; // Cursor para o jogador branco
            }
            else
            {
                Cursor = CursoresXadrez.CursorPreto; // Cursor para o jogador preto
            }
        }
    }
}