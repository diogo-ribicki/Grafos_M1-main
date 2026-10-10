namespace TrabalhoGrafos;

public static class LeitorGrafos
{
    public static void CarregarArquivo(string caminho, Grafos grafo)
    {
        if (!File.Exists(caminho))
            throw new FileNotFoundException("Arquivo de grafo não encontrado.", caminho);

        using var arquivo = new StreamReader(caminho);

        string? primeiraLinha = ProximaLinhaValida(arquivo);

        if (primeiraLinha == null)
            throw new InvalidDataException("Arquivo vazio.");

        string[] cabecalho = primeiraLinha
            .Split(new[] { ' ', '\t', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

        if (cabecalho.Length < 4)
            throw new InvalidDataException(
                "A primeira linha deve conter: V A D P.");

        int v = int.Parse(cabecalho[0]);
        int a = int.Parse(cabecalho[1]);
        int d = int.Parse(cabecalho[2]);
        int p = int.Parse(cabecalho[3]);

        if (d != (grafo.Direcionado ? 1 : 0))
            throw new InvalidDataException(
                "O tipo de direcionamento do arquivo não corresponde ao grafo criado.");

        if (p != (grafo.Ponderado ? 1 : 0))
            throw new InvalidDataException(
                "O tipo de ponderação do arquivo não corresponde ao grafo criado.");

        // O arquivo fornece apenas a quantidade de vértices.
        // Os rótulos padrão serão 0, 1, 2, ..., V-1.
        for (int i = 0; i < v; i++)
        {
            if (!grafo.InserirVertice(i.ToString()))
                throw new InvalidDataException($"Não foi possível inserir o vértice {i}.");
        }

        for (int i = 0; i < a; i++)
        {
            string? linha = ProximaLinhaValida(arquivo);

            if (linha == null)
                throw new InvalidDataException(
                    $"O arquivo informa {a} arestas, mas terminou antes da aresta {i + 1}.");

            string[] partes = linha
                .Split(new[] { ' ', '\t', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

            int quantidadeEsperada = grafo.Ponderado ? 3 : 2;

            if (partes.Length < quantidadeEsperada)
                throw new InvalidDataException(
                    $"Formato inválido na aresta {i + 1}.");

            int origem = int.Parse(partes[0]);
            int destino = int.Parse(partes[1]);
            float peso = 1;

            if (grafo.Ponderado)
                peso = float.Parse(
                    partes[2],
                    System.Globalization.CultureInfo.InvariantCulture);

            if (!grafo.InserirAresta(origem, destino, peso))
                throw new InvalidDataException(
                    $"Não foi possível inserir a aresta {origem} -> {destino}.");
        }
    }

    private static string? ProximaLinhaValida(StreamReader arquivo)
    {
        while (!arquivo.EndOfStream)
        {
            string? linha = arquivo.ReadLine();

            if (linha == null)
                return null;

            linha = linha.Trim();

            if (linha.Length == 0 || linha.StartsWith("#"))
                continue;

            return linha;
        }

        return null;
    }
}
