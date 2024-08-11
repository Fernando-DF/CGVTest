$(document).ready(function () {
    $("form").validate({
        rules: {
            "Endereco.Cep": {
                required: true,
                minlength: 9
            },
            "Endereco.Logradouro": {
                required: true
            },
            "Endereco.Bairro": {
                required: true
            }
        },
        messages: {
            "Endereco.Cep": {
                required: "CEP é obrigatório",
                minlength: "CEP deve ter pelo menos 9 caracteres"
            },
            "Endereco.Logradouro": {
                required: "Logradouro é obrigatório"
            },
            "Endereco.Bairro": {
                required: "Bairro é obrigatório"
            }
        },
        errorElement: "span",
        errorClass: "text-danger",
        highlight: function (element, errorClass) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass) {
            $(element).removeClass("is-invalid");
        }
    });

});
