

function main() {
    $('.inputCnpj').on('blur', function () {

        var cnpj = $('.inputCnpj')[0].value.replace(".", "").replace(".", "").replace("/", "").replace("-", "");

        if (cnpj.length != 14) {
            $('#btnSubmit').attr('disabled', true);
            return;
        }

        $.post('./Find/Cnpj/', { cnpj })

            .done(function (data) {

                data = JSON.parse(data);

                if (data.Existe) {
                    $('#btnSubmit').attr('disabled', true);
                    $('.inputCnpj').val('');
                    alert('CNPJ já cadastrado');
                } else {
                    if ($('.inputCnpj').val().length > 0) {
                        $('#btnSubmit').attr('disabled', false);
                    }
                }
            })
            .fail(function (err) {
                //console.log(err);
                alert('Falha ao consultar CNPJ');
            });
    });

    const inputSearchKeyEl = $('#inputSearchKey');

    $('#pageSize').on('change', function () {
        reload(1, inputSearchKeyEl.val(), this.value);
    });
}

main();


function setOnClickDelete() {

    $('.delete-button').on('click', function (event) {

        let id = event.target.attributes.id.value;
        let controller = event.target.attributes.controller.value;

        sessionStorage.setItem('id', id);
        sessionStorage.setItem('controller', controller);
    });

    $('#btn-delete-confirm').on('click', function () {

        let id = sessionStorage.id;
        let controller = sessionStorage.controller;

        $.post('./' + controller + '/Delete', { id });
    });


    $('#btn-delete-confirm').on('click', function () {

        let id = sessionStorage.id;
        let controller = sessionStorage.controller;

        $.post('./' + controller + '/Delete', { id });
    });
} //disabled