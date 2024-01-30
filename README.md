# Gamification
Artefatos utilizado na tese de doutorado : GAMIFICAÇÃO NA EDUCAÇÃO EXECUTIVA ONLINE: EFEITOS NO ENGAJAMENTO DOS ESTUDANTES

# Documentação gamificação


## Phidelis

Abaixo está descrito os itens que compõem o recurso de gamificação na aplicação. Os arquivos estão na pasta **"Phidelis.Apurador.Gamificacao"**

### App.config
Arquivo de configuração da aplicação

### Packages.config
Pacotes utilizados na aplicação (Nugget)

### Pasta Command
Código de leitura do moodle e inserção de badges no Phidelis

 - **InserirBadgeAssistiuAulaOnlineCommand.cs:**  Consulta direta ao banco de dados Moodle para leitura quantitativa de aulas assistidas e distribuição das badges no Phidelis
 - **InserirBadgeConclusaoTopicoCommand.cs:** Consulta direta ao banco de dados Moodle para leitura quantitativa de tópicos concluídos e distribuição das badges no Phidelis
 - **InserirBadgeDesafioCommand.cs:** Consulta direta ao banco de dados Moodle para leitura quantitativa de Desafios concluídos e distribuição das badges no Phidelis
 - **InserirBadgeParticipativoCommand.cs:** Consulta direta ao banco de dados Moodle para leitura quantitativa de interações no fórum e distribuição das badges no Phidelis

#### Relatórios

 - **RelatorioEngajamentoCommand.cs:** Consulta direta ao banco de dados Moodle para criação de relatório (.xlsx) quantitativo de acessos à disciplina, por data, hora e quantidade de acessos no dia
 - **RelatorioMediaCommand.cs:** Consulta direta ao banco de dados Moodle e Phidelis para acompanhamento de notas e geração de ranking de média para distribuição das badges.

### Pasta Data
Arquivos com strings de conexão com banco de dados 

### Pasta Entities
Arquivos de mapeamento das querys. Criação strings de dados para para consultas realizadas em arquivos da pasta "Command"

## Moodle

### Querys
Arquivos de consulta ao banco de dados para coleta de dados e geração de relatório


### Fórum (Badge Participativo)
#### Localização: your_moodle_directory/mod/forum/
Descrição: Este é o diretório principal do módulo de fórum. Ele contém os arquivos PHP e outras dependências necessárias para o funcionamento do fórum. Dentro deste diretório, você encontrará scripts para criar, visualizar e gerenciar fóruns e discussões.
Arquivos de Linguagem:

#### Localização: your_moodle_directory/lang/en/forum.php (para inglês)
Descrição: Contém as strings de idioma utilizadas pelo módulo do fórum. Se você estiver usando o Moodle em outro idioma, substitua en pelo código apropriado do idioma.
Arquivos de Configuração do Módulo:

#### Localização: your_moodle_directory/mod/forum/settings.php
Descrição: Este arquivo contém as configurações administrativas do módulo de fórum, permitindo que os administradores ajustem o comportamento padrão dos fóruns.
Scripts de Atualização:

#### Localização: your_moodle_directory/mod/forum/db/
Descrição: Este diretório contém arquivos relacionados ao banco de dados, como scripts de atualização (upgrade.php) que são executados durante a atualização do módulo do fórum.
Templates e Layouts (Temas):

#### Localização: your_moodle_directory/mod/forum/styles.css (e potencialmente em subdiretórios de temas)
Descrição: Aqui você encontra os estilos CSS usados pelo módulo do fórum. Além disso, os temas do Moodle podem ter seus próprios arquivos CSS e templates que alteram a aparência dos fóruns.
Código de Libs (Bibliotecas):

#### Localização: your_moodle_directory/mod/forum/lib.php
Descrição: Este arquivo contém funções e bibliotecas essenciais para o funcionamento do módulo de fórum.
Scripts de Backup e Restauração:

#### Localização: your_moodle_directory/mod/forum/backup/moodle2/
Descrição: Contém scripts para lidar com backup e restauração de dados do fórum.
É importante notar que a estrutura exata dos arquivos e diretórios pode variar um pouco dependendo da versão do Moodle que você está usando. Além disso, qualquer modificação nos arquivos do núcleo do Moodle deve ser feita com cuidado, pois pode afetar a estabilidade e a segurança da plataforma.

### Diretório de Sessões/Tópicos do Curso (Badge tópicos e desafios concluídos)

#### Localização:
Não aplicável. As sessões ou tópicos do Moodle não estão localizados em um diretório específico no sistema de arquivos, pois são parte integrante da interface do curso no Moodle e são gerenciados através do banco de dados e da interface do usuário.

#### Descrição:
Sessões ou tópicos no Moodle são espaços organizacionais dentro de um curso. Eles permitem que os professores estruturem o curso em seções lógicas, cada uma contendo recursos, atividades e materiais de aprendizagem. A configuração pode ser por sessão (com uma sequência temporal) ou por tópico (organizados por assunto).

#### Configuração e Gerenciamento:

**Localização:** Acesso através da interface do curso no Moodle.
**Descrição:** A configuração e o gerenciamento de sessões ou tópicos são realizados dentro da interface do curso. Os professores podem adicionar, editar, ocultar ou reordenar sessões/tópicos conforme necessário. Isso é feito no modo de edição dentro do curso.

#### Arquivos e Recursos:

**Localização:** Os arquivos e recursos são carregados no contexto do curso e não estão associados a um diretório específico no sistema de arquivos.
**Descrição:** Dentro de cada sessão ou tópico, os professores podem adicionar diversos tipos de recursos (como arquivos, links, páginas) e atividades (como fóruns, questionários, tarefas). Esses elementos são gerenciados através da interface do curso.

#### Backup e Restauração:

**Localização:** Funcionalidades acessíveis através da interface administrativa do Moodle.
**Descrição:** O Moodle permite realizar backup e restauração não apenas do curso inteiro, mas também de seções específicas dele. Isso inclui todos os recursos, atividades e configurações contidas nessas sessões ou tópicos.

#### Templates e Layouts:

**Descrição:** A aparência e a estrutura das sessões ou tópicos são definidas pelo tema do Moodle. Os temas controlam como os cursos e suas seções são exibidos para os usuários, mas essa configuração é gerenciada a nível global do site, não a nível de diretório ou arquivo.

É importante ressaltar que, ao contrário dos módulos como Fórum ou Dialogue, as sessões ou tópicos do Moodle são parte integrante da estrutura do curso e são gerenciados inteiramente através da interface web do Moodle, sem um diretório específico no sistema de arquivos para seu gerenciamento direto.

### Diretório do Módulo Dialogue (Badge participativo)

#### Localização: your_moodle_directory/mod/dialogue/
**Descrição:** Este é o diretório principal do módulo Dialogue. Ele contém os arquivos PHP e outras dependências necessárias para o funcionamento do diálogo interativo entre usuários. Dentro deste diretório, você encontrará scripts para iniciar, gerenciar e participar de diálogos individuais ou em grupo.

#### Arquivos de Linguagem:

#### Localização: your_moodle_directory/lang/en/dialogue.php (para inglês)
Descrição: Este arquivo contém as strings de idioma usadas pelo módulo Dialogue. Assim como no módulo Fórum, para idiomas diferentes do inglês, substitua 'en' pelo código apropriado do idioma.

#### Arquivos de Configuração do Módulo:

#### Localização: your_moodle_directory/mod/dialogue/settings.php
Descrição: Este arquivo configura as opções administrativas do módulo Dialogue, permitindo aos administradores ajustar como os diálogos são iniciados e gerenciados.

#### Scripts de Atualização:

#### Localização: your_moodle_directory/mod/dialogue/db/
**Descrição:** Este diretório contém arquivos relacionados ao banco de dados, incluindo scripts de atualização (upgrade.php) que são usados durante a atualização do módulo Dialogue.

#### Templates e Layouts (Temas):

#### Localização: your_moodle_directory/mod/dialogue/styles.css (e potencialmente em subdiretórios de temas)
**Descrição:** Contém os estilos CSS específicos para o módulo Dialogue. Os temas do Moodle também podem incluir arquivos CSS e templates que modificam a aparência dos diálogos.

#### Código de Libs (Bibliotecas):

#### Localização: your_moodle_directory/mod/dialogue/lib.php
Descrição: Este arquivo inclui funções e bibliotecas cruciais para a operação do módulo Dialogue, facilitando a criação e gestão de diálogos.

#### Scripts de Backup e Restauração:
#### Localização: your_moodle_directory/mod/dialogue/backup/moodle2/
**Descrição:** Aqui estão localizados os scripts para backup e restauração dos dados relacionados aos diálogos.
Assim como o módulo Fórum, a estrutura de arquivos e diretórios do módulo Dialogue pode variar conforme a versão do Moodle utilizada. Modificações nos arquivos do Moodle devem ser feitas com cautela para não comprometer a funcionalidade e segurança da plataforma.

### Diretório de Armazenamento de Arquivos de Vídeo (Badge Aulas assistidas)

#### Localização: your_moodle_directory/moodledata/filedir/
**Descrição:** Os arquivos de vídeo carregados diretamente no Moodle são armazenados em moodledata, um diretório fora do diretório público do Moodle para segurança. Dentro de moodledata, a pasta filedir contém os arquivos, incluindo vídeos, em uma estrutura de diretório gerenciada pelo Moodle, onde os arquivos são nomeados e organizados em pastas baseadas em um hash de seu conteúdo.

#### Código de Inserção de Vídeo:

#### Localização: O código para inserir vídeos em um curso está disperso em vários arquivos PHP, principalmente relacionados aos recursos do curso e ao editor de texto do Moodle.
**Descrição:** Arquivos PHP como moodle_directory/course/modedit.php e arquivos relacionados ao editor de texto (como TinyMCE ou Atto, dependendo do editor configurado no Moodle) tratam da inserção e configuração de recursos de vídeo.
Scripts de Renderização de Vídeo:

#### Localização: your_moodle_directory/lib/filelib.php
**Descrição:** Este arquivo contém funções para manipulação e exibição de arquivos, incluindo vídeos. Ele lida com a forma como os arquivos são servidos para o navegador.

#### Plugins de Vídeo (Se Instalados):

#### Localização: your_moodle_directory/mod/ (para plugins de atividade) ou your_moodle_directory/local/ (para plugins locais).
**Descrição:** Se você estiver usando plugins específicos para vídeos, como um player de vídeo personalizado ou integrações com plataformas de vídeo, os arquivos desses plugins estarão localizados aqui.

#### Arquivos de Configuração:

#### Localização: your_moodle_directory/config.php
**Descrição:** Este arquivo contém configurações gerais do Moodle, incluindo o caminho para o moodledata.

#### Templates e Layouts (Temas):

#### Localização: your_moodle_directory/theme/
**Descrição:** Os temas do Moodle podem ter estilos específicos e templates que afetam a forma como os vídeos são exibidos. Estes são encontrados nos diretórios dos temas.
É importante notar que o Moodle é uma plataforma complexa e seu código-fonte pode ser bastante extenso e interconectado. A estrutura exata e a localização dos arquivos podem variar dependendo da versão específica do Moodle e de quaisquer personalizações ou plugins adicionais. Alterações nos arquivos de código-fonte do Moodle devem ser realizadas com cautela para não afetar a funcionalidade e segurança da plataforma.
