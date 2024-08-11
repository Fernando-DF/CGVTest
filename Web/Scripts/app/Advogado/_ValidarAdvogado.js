$(document).ready(function () {
    $('form').validate({
        rules: {
            Nome: {
                required: true,
                maxlength: 80,
                minlength: 5
            },
        },
        messages: {
            Nome: {
                required: "O campo Nome é obrigatório.",
                maxlength: "O campo Nome não pode ter mais de 80 caracteres.",
                minlength: "O Campo deve ter no mínimo 5 caracteres."
            }
        }
    })
})