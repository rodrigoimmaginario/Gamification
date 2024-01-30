<?php
// This file is part of Moodle - http://moodle.org/
//
// Moodle is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Moodle is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Moodle.  If not, see <http://www.gnu.org/licenses/>.

/**
 * Dialogue version.
 *
 * @package mod_dialogue
 * @copyright 1999 onwards Martin Dougiamas  {@link http://moodle.com}
 * @license   http://www.gnu.org/copyleft/gpl.html GNU GPL v3 or later
 */
defined('MOODLE_INTERNAL') || die();

$plugin->version   = 2023041100;
$plugin->requires  = 2021051700;  // Requires 3.11 or higher.
$plugin->component = 'mod_dialogue';    // Full name of the plugin (used for diagnostics).
$plugin->release   = '3.11.3';             // Semantic version name.
$plugin->maturity  = MATURITY_STABLE;    // This version's maturity level.
$plugin->dependencies = [];

// Versão do plugin utilizada para modificação: 2021062902

// Versão do plugin após a modificação: 202211801
// [] Modificação realizada: Filtro dos usuários mostrados com opção de configurar os perfis por atividade cadastrada

// Versão do plugin após a modificação: 202212401
// [] Adicionado o grupo do usuário na lista de diálogos

// Versão do plugin após a modificação: 202212502
// [] Adicionado o arquivo js/select-user.js para mostrar a lista preenchida de usuários
// [] Modificação no arquivo conversation.php para adicionar o script acima na página

//  Versão do plugin após a modificação: 2022112800
// [] Criado a função get_user_from_course no lib.php
// [] Ajustado classes/conversation.php para adicionar todos os usuários caso esteja criando um novo diálogo (linha 316)

//  Versão do plugin após a modificação: 2023041100
// [] Adicionado os javascripts nas linhas 60 e 61 do arquivo conversation.php
// [] Adicionado os javascripts correspondentes na pasta js