TRABALHO DE GRAFOS - C#

Arquivos:
- Grafos.cs: classe abstrata base.
- GrafoMatriz.cs: implementação por matriz de adjacência.
- GrafoLista.cs: implementação por lista de adjacência.
- Aresta.cs: estrutura auxiliar da lista.
- Algoritmos.cs: BFS, DFS e Dijkstra.
- LeitorGrafos.cs: leitura do arquivo no formato V A D P.
- Program.cs: menu para testar todas as operações.
- exemplo_grafo.txt: exemplo de arquivo.

REQUISITOS:
- .NET 8 ou superior.

EXECUÇÃO:
1. Abra a pasta no Visual Studio ou VS Code.
2. Execute:
   dotnet run

FORMATO DO ARQUIVO:
V A D P
Ao Ad [Ap]

Exemplo ponderado e não direcionado:
5 6 0 1
0 1 5
0 2 3
1 2 2
1 3 6
2 3 4
3 4 1

D = 0 não direcionado, 1 direcionado.
P = 0 não ponderado, 1 ponderado.

OBSERVAÇÃO:
Quando o arquivo não fornece labels, o programa usa os índices
0, 1, 2, ..., V-1 como labels.

Os algoritmos BFS, DFS e Dijkstra recebem a classe Grafos.
Assim, o mesmo código funciona para GrafoMatriz e GrafoLista,
usando RetornarVizinhos() e PesoAresta() por polimorfismo.
