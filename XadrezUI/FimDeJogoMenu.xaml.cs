using LogicaXadrez;
using System.Windows;
using System.Windows.Controls;

namespace XadrezUI
{
    /// <summary>
    /// Interação lógica para FimDeJogoMenu.xam
    /// </summary>
    public partial class FimDeJogoMenu : UserControl
    {
        public event Action<Opcao> OpcaoSelecionada;

        public FimDeJogoMenu(JogoEstado jogoEstado)
        {
            InitializeComponent();

            Resultado resultado = jogoEstado.Resultado;

            TextoVencedor.Text = ObterTextoVencedor(resultado.Vencedor);
            TextoMotivo.Text = MotivoParaTexto(resultado.Motivo, jogoEstado.JogadorAtual);
        }

        public static string ObterTextoVencedor(Jogador vencedor)
        {
            return vencedor switch
            {
                Jogador.Branco => "Rei Branco Vence!",
                Jogador.Preto => "Rei Preto Vence!",
                _ => "EMPATE",
            };
        }

        private static string JogadorParaTexto(Jogador jogador)
        {
            return jogador switch
            {
                Jogador.Branco => "Branco",
                Jogador.Preto => "Preto",
                _ => "",
            };
        }

        public static string MotivoParaTexto(FimMotivo motivo, Jogador jogador)
        {
            return motivo switch
            {
                FimMotivo.Checkmate => $"CHECKMATE - {JogadorParaTexto(jogador)} não pode se mover!",
                FimMotivo.Stalemate => $"STALEMATE - {JogadorParaTexto(jogador)} não pode se mover!",
                FimMotivo.InsufficientMaterial => $"Material Insuficiente",
                FimMotivo.ThreefoldRepetition => $"Repetição Tríplice",
                FimMotivo.FiftyMoveRule => $"Regra dos 50 Movimentos",
                _ => "",
            };
        }

        private void Reiniciar_Click(object sender, RoutedEventArgs e)
        {
            OpcaoSelecionada?.Invoke(Opcao.Reiniciar);
        }

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            OpcaoSelecionada?.Invoke(Opcao.Sair);
        }
    }
}
