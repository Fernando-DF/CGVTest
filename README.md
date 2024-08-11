# CGVTest
 
## Preface
Esta é a minha solução da prova de .NET proposta, criada em .NET Framework 4.6.1, e procurando seguir as intruções da maneira mais precisa possível.

## Featues
Enums, CRUD de advogados, tabela de endereço para facilitar normalização de dados, máscaras e validações com JQuery, mensagens de validação nos modelos relevantes.

## Setup
A maneira mais fácil de rodar o projeto é simplesmente buildando e rodando ele pelo Visual Studio, você pode encontrar 2 arquivos `.sql` no diretório `DatabaseExports`, eles estão aqui somente para agilizar a criação das tables.O nome padrão da base de dados é cgv, mas isso pode ser mudado facilmente no web config que está no root da camada web.

## Pontos a melhorar neste projeto
<ul>
    <li>Talvez eu não tenha entendido direito como que o padrão dos arquivos .js deveria ser seguido, mas de qualquer maneira eu ainda tentei manter esta parte organizada</li>
    <li>Eu certamente poderia ter feito viewmodels mais limpos e granulares, atualmente eles são monstros com vários campos que não são usados em todas as ações que referenciam ele</li>
    <li>A implementação das máscaras e validações do JQuery está bugada em alguns lugares</li>
    <li>Eu poderia ter implementado um middleware para lidar com exceções e erros, atualmente estes estão borbulhando corrente acima até chegarem ao cliente, o que não é algo que eu deixaria acontecer normalmente.Geralmente eu retornaria um status http e talvez loggaria algo no console para não deixar o cliente totalmente no escuro do que está acontecendo(se ele for apto o suficiente para saber onde procurar isso)</li>
</ul>