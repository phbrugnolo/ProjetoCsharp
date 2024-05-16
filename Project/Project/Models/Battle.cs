namespace Project;

public class Battle
{
    public Battle(string jogada)
    {
        Jogada = jogada.ToLower().Trim();

    }

    public string Batalhar()
    {
        string[] opcoes = { "pedra", "papel", "tesoura" };
        Random random = new Random();
        int indiceMaquina = random.Next(0, 3);
        string jogadaMaquina = opcoes[indiceMaquina];

        if (Jogada == jogadaMaquina)
        {
            return "Empate!";
        }
        else if ((Jogada == "pedra" && jogadaMaquina == "tesoura") ||
                 (Jogada == "papel" && jogadaMaquina == "pedra") ||
                 (Jogada == "tesoura" && jogadaMaquina == "papel"))
        {
            return "Você venceu!";
        }
        else
        {
            return "Você perdeu!";
        }
    }

    public string Jogada { get; set; }
}
