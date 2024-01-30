$('#id_send').click(function(e) {
    var verifica_form = verifica_form_moodle();

    if (verifica_form) {
        alert('Aguarde.... Sua mensagem está sendo enviada!');

        $('#id_send').val('Enviando...');
        $('#id_send').style.add('disabled');

        $('#id_save').style.add('disabled');
    }

});

$('#id_save').click(function(e) {
    var verifica_form = verifica_form_moodle();

    if (verifica_form) {
        alert('Aguarde.... Seu rascunho está sendo salvo!');

        $('#id_save').val('Enviando...');
        $('#id_save').style.add('disabled');

        $('#id_send').style.add('disabled');
    }

});

function verifica_form_moodle()
{
    console.log('Iniciado Verifica form Moodle');

    // Verifica se existe o assunto na tela. Se existir, pega seu conteúdo
    //var assunto = document.getElementById('id_subject');
    var assunto = document.getElementById("id_subject");

    if (assunto) {
        var assunto_content = assunto.value; // $('id_subject').text();

        if ( (typeof assunto_content === "undefined") || ("" === assunto_content)) {
            alert('Informe o assunto!');
            console.log('Informe o assunto');
            return false;
        }
    }

    // Se tudo estiver certo...
    return true;

}
