# Couch Coop base project

Repositório para iniciar o desenvolvimento de jogos do estilo Couch coop com facilidade

## Configurações de projeto:

Versão do Unity utilizada: 2022.3.5f1

Dependências de projeto:

- Probuilder: utilizado para construir o cenário, ótima ferramenta para geometrias complexas
- InputSystem: utilizado para configurar controles de teclado e gamepad, também usado como método para implementar multijogador local
- TMPro: utilizado nos textos, dado que nas versões mais novas do Unity, TMPro se tornou padrão

## Compilando e executando:

Projeto possui somente uma cena que é o jogo em sua completude, ao importar os arquivos do repositório em um projeto local e inserir a cena na hierarquia, "Build and Run" irá compilar e executar.

## Assembly Definitions

Três definições foram construidas
- Uma para agrupar os scripts responsáveis pelo sistema de salvar / carregar
- Uma para agrupar os scripts do jogo base, que regulam movimentação, interface, multijogador, etc...
- Uma para agrupar os scripts de unit tests, gerada automaticamente pelo unity testrunner

## Descrição do projeto

A implementação atual é de um jogo de coletar moedas, onde cada jogador possui seu próprio placar conforme coleta moedas que aparecem aleatoriamente. Qualquer gamepad ou teclado pode ser utilizado
para jogar, configurado para ser "plug and play".
