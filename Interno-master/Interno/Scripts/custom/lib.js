function validarCNPJ(cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length != 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;

}

function maskForm() {
    $('.maskData').setMask('date');
    $('.maskFeriado').setMask('feriado');
    $('.maskDataOfCalendar').setMask('date');
    $('.maskDataHora').setMask('date-time');
    $('.maskHora').setMask('time');
    $('.maskCpf').setMask('cpf');
    $('.maskCnpj').setMask('cnpj');
    $('.maskCep').setMask('cep');
    $('.maskInteiro').setMask('integer');
    $('.maskMoney').setMask('decimal');
    $('.maskMoneySigned').setMask('signed-decimal');
    $('.maskMoneyNegative').setMask('negative-decimal');
    $('.maskDataTime').setMask({
        mask: '39/19/9999 29:59'
    });
    $('.maskCnhCategoria').setMask({
        mask: 'aa'
    });
    $('.maskAno').setMask({
        mask: '9999'
    });
    $('.maskQtd').setMask({
        mask: '999'
    });
    $('.maskPlaca').setMask({
        mask: 'aaa-9999'
    });
    $('.maskNumeros').setMask({
        mask: '999999999999'
    });
    $('.maskNumero').setMask({
        mask: '9'
    });
    $('.maskPorcentagem').setMask({
        mask: '99,999',
        type: 'reverse'
    });

    $('.maskContainer').setMask({
        mask: 'aaaa9999999'
    }).css({
        textTransform: 'uppercase'
    });

    $('.upper').on('blur', function () {
        $(this).val($(this).val().toUpperCase());

    });

    $('.maskTime').setMask('time');
    $('.maskTelefone').keydown(function () {
        // Act on the event
        var value_temp1 = $(this).val();
        var value_temp2 = $(this).val();
        var str_ninth = value_temp1.substring(3, 6);
        var str_ddd = value_temp2.substring(0, 2);
        if (str_ninth == ') 9' && (str_ddd == '(1' || str_ddd == '(2' || str_ddd == '(9')) {
            $(this).attr('alt', 'phone_ninth');
        } else {
            $(this).attr('alt', 'phone');
        }
        $(this).setMask();
    });

    $('.uppercase').on('blur', function () {
        $(this).val($(this).val().toUpperCase());
    });

    $('.inputCnpj').on('blur', function () {

        var cnpj = $('.inputCnpj')[0].value.replace(".", "").replace(".", "").replace("/", "").replace("-", "");

        if (cnpj.length != 14) {
            $('#btnSubmit').attr('disabled', true);
            return;
        }

        var v = $(this).val();

        if (!validarCNPJ(v)) {

            alert("Preencher com um CNPJ válido!");
            $(this).val("");
        }
    });
    

    $('#selectSearch').change(function () {
        $('#inputKey').attr('name', $('#selectSearch').val());

        if ($('#selectSearch').val() == 'TRANSP_NOME' || $('#selectSearch').val() == 'CLIENTE_NOME') {//ver diferença para cliente
            $('#inputKey').removeClass('maskCnpj');
        } else {
            $('#inputKey').addClass('maskCnpj');
        }
    });
}

function scripts() {
    maskForm();
}
scripts();