# BankruptGame

# Sobre

Como esta é uma prova de estagio, decidi fazer em duas linguagens diferentes usando comunicação de subprocessos.

- C# .NET CORE - Utilizado para fazer o jogo
- Python 3 - Utilizado para fazer o jogador, comunica-se atraves de um subprocesso.

### Importante

- O jogo pode ser rodado independentemente do jogador.
- O jogador gera um arquivo de log `log.txt` e um arquvo `result.csv` com os resultados.
- A pasta `game` se encontra o jogo e a pasta `players` os jogadores.
- Os programas utilizam **seeds** para garantir reprodutibilidade. É possivel alterar em `settings.py` para receber reultados diferentes.
- O arquivo `settings.py` contem os filepaths para a comunicação entre o player e o jogo.

## Requsitos

- .NET CORE 5 e **dotnet cli**
- Python 3+


## Como Utilizar
Execulte os comandos no terminal:
```sh
cd game
dotnet publish -c Release -o Release --self-contained false
cd ../player
python3 main.py
```
***Obs:*** Se estiver utilizando Windows traduza as instruções conforme seu computador esta configurado (Ex: python3 pode nao corresponder ao execultavel do python)

> No Windows deve-se mudar a variavel GAME_PATH em `settings.py` e adicionar .exe ao final de game.