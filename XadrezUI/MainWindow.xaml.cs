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
        private const int qtdColunasTabuleiro = 8;
        private const int qtdLinhasTabuleiro = 8;

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
            if(MenuEstaNaTela())
            {
                return; // Ignora cliques se o menu de fim de jogo estiver visível
            }

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
                if (movimento.Tipo == MovimentoTipo.PromacaoPeao)
                {
                    RealizarPromocao(movimento.PosOrigem, movimento.PosDestino);
                }
                else
                {
                    RealizarMovimento(movimento);
                }
            }
        }

        private void RealizarPromocao(Posicao posOrigem, Posicao posDestino)
        {
            pecasImagens[posDestino.Linha, posDestino.Coluna].Source = Imagens.ObterImagem(jogoEstado.JogadorAtual,PecaTipo.Peao);
            pecasImagens[posOrigem.Linha, posOrigem.Coluna].Source = null;

            PromocaoMenu promocaoMenu = new PromocaoMenu(jogoEstado.JogadorAtual);

            MenuContainer.Content = promocaoMenu;

            promocaoMenu.PecaSelecionada += tipoPeca =>
            {
                MenuContainer.Content = null; // Limpa o menu de promoção

                Movimento promocao = new PromocaoPeao(posOrigem, posDestino, tipoPeca);

                RealizarMovimento(promocao);
            };
        }

        private void RealizarMovimento(Movimento movimento)
        {
            jogoEstado.RealizarTurno(movimento);

            DesenharTabuleiro(jogoEstado.Tabuleiro);

            DefinirCursor(jogoEstado.JogadorAtual);

            if (jogoEstado.FimDeJogo())
            {
                MostrarFimDeJogo();
            }
        }

        private void CacheMovimentos(IEnumerable<Movimento> movimentos)
        {
            movimentoCache.Clear();

            foreach(Movimento mov in movimentos)
            {
                movimentoCache[mov.PosDestino] = mov;
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

        private bool MenuEstaNaTela()
        {
            return MenuContainer.Content != null;
        }

        private void MostrarFimDeJogo()
        {
            FimDeJogoMenu fimDeJogoMenu = new FimDeJogoMenu(jogoEstado);
            MenuContainer.Content = fimDeJogoMenu;

            fimDeJogoMenu.OpcaoSelecionada += opcao =>
            {
                if(opcao == Opcao.Reiniciar)
                {
                    MenuContainer.Content = null;

                    ReiniciarJogo();
                }
                else
                {
                    Application.Current.Shutdown(); // Fecha o aplicativo se outra opção for selecionada
                }
            };
        }

        private void ReiniciarJogo()
        {
            EsconderDestaques();
            movimentoCache.Clear();
            jogoEstado = new JogoEstado(Tabuleiro.Iniciar(), Jogador.Branco);
            DesenharTabuleiro(jogoEstado.Tabuleiro);
            DefinirCursor(jogoEstado.JogadorAtual);
        }
    }
}