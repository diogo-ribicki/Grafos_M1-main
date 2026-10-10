namespace TrabalhoGrafos;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("==================================");
        Console.WriteLine("       TRABALHO DE GRAFOS");
        Console.WriteLine("==================================");
        Console.WriteLine();

        Grafos grafo = CriarGrafo();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("==================================");
            Console.WriteLine("MENU");
            Console.WriteLine("==================================");
            Console.WriteLine("1 - Inserir vértice");
            Console.WriteLine("2 - Remover vértice");
            Console.WriteLine("3 - Inserir aresta");
            Console.WriteLine("4 - Remover aresta");
            Console.WriteLine("5 - Verificar aresta");
            Console.WriteLine("6 - Consultar peso");
            Console.WriteLine("7 - Mostrar vizinhos");
            Console.WriteLine("8 - Imprimir grafo");
            Console.WriteLine("9 - BFS");
            Console.WriteLine("10 - DFS");
            Console.WriteLine("11 - Dijkstra");
            Console.WriteLine("12 - Ler grafo de arquivo");
            Console.WriteLine("13 - Criar novo grafo (reconfigurar)");
            Console.WriteLine("14 - Verificar se é conexo");
            Console.WriteLine("0 - Sair");

            int escolha = LerInteiro("Escolha: ", 0, 14);
            Console.WriteLine();

            try
            {
                switch (escolha)
                {
                    case 1:
                        InserirVertice(grafo);
                        break;

                    case 2:
                        RemoverVertice(grafo);
                        break;

                    case 3:
                        InserirAresta(grafo);
                        break;

                    case 4:
                        RemoverAresta(grafo);
                        break;

                    case 5:
                        VerificarAresta(grafo);
                        break;

                    case 6:
                        ConsultarPeso(grafo);
                        break;

                    case 7:
                        MostrarVizinhos(grafo);
                        break;

                    case 8:
                        grafo.ImprimeGrafo();
                        break;

                    case 9:
                        ExecutarBusca(grafo, false);
                        break;

                    case 10:
                        ExecutarBusca(grafo, true);
                        break;

                    case 11:
                        ExecutarDijkstra(grafo);
                        break;

                    case 12:
                        LerArquivo(grafo);
                        break;

                    case 13:
                        grafo = CriarGrafo();
                        Console.WriteLine("Novo grafo criado com sucesso.");
                        break;

                    case 14:
                        Console.WriteLine(
                            grafo.EhConexo()
                                ? "O grafo é conexo."
                                : "O grafo NÃO é conexo.");
                        break;

                    case 0:
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
        }
    }

    private static Grafos CriarGrafo()
    {
        bool direcionado = LerSimNao("O grafo é direcionado? (s/n): ");
        bool ponderado = LerSimNao("O grafo é ponderado? (s/n): ");

        Console.WriteLine();
        Console.WriteLine("Escolha a representação:");
        Console.WriteLine("1 - Matriz de adjacência");
        Console.WriteLine("2 - Lista de adjacência");

        int opcao = LerInteiro("Opção: ", 1, 2);

        return opcao == 1
            ? new GrafoMatriz(direcionado, ponderado)
            : new GrafoLista(direcionado, ponderado);
    }

    private static bool VerticeExiste(Grafos grafo, int indice)
    {
        if (indice < 0 || indice >= grafo.QuantidadeVertices)
        {
            Console.WriteLine(
                $"Vértice {indice} não existe. Índices válidos: 0 a {grafo.QuantidadeVertices - 1}.");
            return false;
        }

        return true;
    }

    private static void InserirVertice(Grafos grafo)
    {
        string label = LerTexto("Label do vértice: ");

        if (grafo.LabelsVertices().Contains(label))
        {
            Console.WriteLine($"Já existe um vértice com o label '{label}'.");
            return;
        }

        Console.WriteLine(
            grafo.InserirVertice(label)
                ? "Vértice inserido com sucesso."
                : "Não foi possível inserir o vértice.");
    }

    private static void RemoverVertice(Grafos grafo)
    {
        int indice = LerInteiro("Índice do vértice: ");

        if (!VerticeExiste(grafo, indice))
            return;

        Console.WriteLine(
            grafo.RemoverVertice(indice)
                ? "Vértice removido com sucesso."
                : "Não foi possível remover o vértice.");
    }

    private static void InserirAresta(Grafos grafo)
    {
        int origem = LerInteiro("Origem: ");
        int destino = LerInteiro("Destino: ");

        if (!VerticeExiste(grafo, origem) || !VerticeExiste(grafo, destino))
            return;

        if (grafo.ExisteAresta(origem, destino))
        {
            Console.WriteLine("Essa aresta já existe entre esses dois vértices.");
            return;
        }

        float peso = 1;

        if (grafo.Ponderado)
        {
            peso = LerFloat("Peso: ");

            if (peso < 0)
            {
                Console.WriteLine("Peso não pode ser negativo. Aresta não inserida.");
                return;
            }
        }

        Console.WriteLine(
            grafo.InserirAresta(origem, destino, peso)
                ? "Aresta inserida com sucesso."
                : "Não foi possível inserir a aresta.");
    }

    private static void RemoverAresta(Grafos grafo)
    {
        int origem = LerInteiro("Origem: ");
        int destino = LerInteiro("Destino: ");

        if (!VerticeExiste(grafo, origem) || !VerticeExiste(grafo, destino))
            return;

        Console.WriteLine(
            grafo.RemoverAresta(origem, destino)
                ? "Aresta removida com sucesso."
                : "Aresta não encontrada entre esses vértices.");
    }

    private static void VerificarAresta(Grafos grafo)
    {
        int origem = LerInteiro("Origem: ");
        int destino = LerInteiro("Destino: ");

        if (!VerticeExiste(grafo, origem) || !VerticeExiste(grafo, destino))
            return;

        Console.WriteLine(
            grafo.ExisteAresta(origem, destino)
                ? "Aresta EXISTE."
                : "Aresta NÃO existe.");
    }

    private static void ConsultarPeso(Grafos grafo)
    {
        int origem = LerInteiro("Origem: ");
        int destino = LerInteiro("Destino: ");

        if (!VerticeExiste(grafo, origem) || !VerticeExiste(grafo, destino))
            return;

        if (!grafo.ExisteAresta(origem, destino))
        {
            Console.WriteLine("Essa aresta não existe.");
            return;
        }

        Console.WriteLine($"Peso: {grafo.PesoAresta(origem, destino):0.##}");
    }

    private static void MostrarVizinhos(Grafos grafo)
    {
        int vertice = LerInteiro("Vértice: ");

        if (!VerticeExiste(grafo, vertice))
            return;

        List<int> vizinhos = grafo.RetornarVizinhos(vertice);

        Console.WriteLine("Vizinhos: " +
            (vizinhos.Count == 0
                ? "nenhum"
                : string.Join(", ", vizinhos.Select(v =>
                    $"{v} ({grafo.LabelVertice(v)})"))));
    }

    private static void ExecutarBusca(Grafos grafo, bool profundidade)
    {
        int origem = LerInteiro("Vértice de origem: ");

        if (!VerticeExiste(grafo, origem))
            return;

        List<int> ordem = profundidade
            ? Algoritmos.BuscaProfundidade(grafo, origem)
            : Algoritmos.BuscaLargura(grafo, origem);

        Console.WriteLine(
            (profundidade ? "DFS" : "BFS") + ": " +
            string.Join(" -> ", ordem.Select(v => grafo.LabelVertice(v))));
    }

    private static void ExecutarDijkstra(Grafos grafo)
    {
        int origem = LerInteiro("Vértice de origem: ");

        if (!VerticeExiste(grafo, origem))
            return;

        ResultadoDijkstra resultado = Algoritmos.Dijkstra(grafo, origem);
        resultado.Imprimir(grafo);
    }

    private static void LerArquivo(Grafos grafo)
    {
        if (grafo.QuantidadeVertices > 0)
        {
            Console.WriteLine("Atenção: o grafo atual já tem vértices.");
            bool continuar = LerSimNao(
                "Carregar o arquivo mesmo assim? Pode gerar erro de vértice duplicado (s/n): ");

            if (!continuar)
            {
                Console.WriteLine(
                    "Operação cancelada. Use a opção 13 pra criar um grafo novo antes de carregar.");
                return;
            }
        }

        string caminho = LerTexto("Caminho do arquivo: ");

        LeitorGrafos.CarregarArquivo(caminho, grafo);

        Console.WriteLine("Grafo carregado com sucesso.");
        grafo.ImprimeGrafo();
    }

    private static bool LerSimNao(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string resposta = (Console.ReadLine() ?? "").Trim().ToLower();

            if (resposta == "s" || resposta == "sim")
                return true;

            if (resposta == "n" || resposta == "nao" || resposta == "não")
                return false;

            Console.WriteLine("Digite s ou n.");
        }
    }

    private static int LerInteiro(string mensagem, int? minimo = null, int? maximo = null)
    {
        while (true)
        {
            Console.Write(mensagem);

            if (int.TryParse(Console.ReadLine(), out int valor))
            {
                if ((!minimo.HasValue || valor >= minimo.Value) &&
                    (!maximo.HasValue || valor <= maximo.Value))
                    return valor;
            }

            Console.WriteLine("Digite um número inteiro válido.");
        }
    }

    private static float LerFloat(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string texto = (Console.ReadLine() ?? "").Replace(',', '.');

            if (float.TryParse(
                texto,
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture,
                out float valor))
            {
                return valor;
            }

            Console.WriteLine("Digite um número válido.");
        }
    }

    private static string LerTexto(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string texto = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(texto))
                return texto.Trim();

            Console.WriteLine("O texto não pode ficar vazio.");
        }
    }
}