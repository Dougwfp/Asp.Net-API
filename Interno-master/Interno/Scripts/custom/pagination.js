let infoPagination = {};

function setSubmitEvent() {
    $('#submitSearchKey').on('click', function () {

        let searchKey = $('#inputSearchKey').val().toUpperCase();

        if (searchKey.trim() != '') {
            reload(1, searchKey, $('#pageSize').val());
        }
    })
}

function popularTable(page = 1, searchKey = null, pageSize = 10) {

    //addLoading()

    let new_page = (page != 1) ? page : infoPagination.currentPage;

    var pagina = $("#pagina").val();

    if (pagina == "Transportadoras") {
        $.post(urlGridTransport, { page: new_page, searchKey, pageSize }, function (data) {
        })

            .done(function (data) {

                removeLoading();

                data = JSON.parse(data);

                for (var i = 0; i < data.length; i++) {
                    addRow(data[i]);
                }
            });
    }
    if (pagina == "Clientes") {
        console.log(pagina);
        $.post(urlGridCliente, { page: new_page, searchKey, pageSize }, function (data) {
        })

            .done(function (data) {

                removeLoading();

                data = JSON.parse(data);

                for (var i = 0; i < data.length; i++) {
                    addRow(data[i]);
                }
            });
    }
    
}

function ajaxRequisition(page, searchKey = null, pageSize = 10) {

    var pagina = $("#pagina").val();
    console.log(pagina);
    if (pagina == "Transportadoras") {
        
        var request = $.ajax({
            url: urlInfoPaginationTransport,
            method: "POST",
            data: { searchKey: searchKey, pageSize: pageSize }
        })
            .done(function (data) {
                //console.log(urlGridTransport)//se preocupar com os cliente
                //console.log(urlInfoPaginationTransport)
                var obj = JSON.parse(data);
                if (obj.itensCount > 0) {
                    createPaginationButtons(page, obj.itensCount, searchKey, pageSize);
                    popularTable(page, searchKey, pageSize);
                }
                else {
                    alert('Não foram encontrados registros correspondentes.');
                    $('#inputSearchKey').val('');
                }

            });
    }
    if (pagina == "Clientes") {
        console.log(pagina);
        var request = $.ajax({
            url: urlInfoPaginationCliente,
            method: "POST",
            data: { searchKey: searchKey, pageSize: pageSize }
        })
            .done(function (data) {
                //console.log(urlGridCliente)//se preocupar com os cliente
                //console.log(urlInfoPaginationCliente)
                var obj = JSON.parse(data);
                if (obj.itensCount > 0) {
                    createPaginationButtons(page, obj.itensCount, searchKey, pageSize);
                    popularTable(page, searchKey, pageSize);
                }
                else {
                    alert('Não foram encontrados registros correspondentes.');
                    $('#inputSearchKey').val('');
                }

            });
    }
    
}

function reload(page = infoPagination.currentPage | 1, searchKey = null, pageSize = 10) {
    //console.log(`${page} -> ${searchKey} -> ${pageSize}`)
    ajaxRequisition(page, searchKey, pageSize)
}

function addLoading() {
    $('#itens_tableBody').get(0).innerHTML = `<img src=""></img>`;
}

function removeLoading() {
    $('#itens_tableBody').get(0).innerHTML = "";
}

function createPaginationButtons(currentPage = 1, totalItens, searchKey, pageSize = 10) {

    document.querySelector('#pagination_list').innerHTML = '';

    //console.log(totalItens)

    if (pageSize == 100 || totalItens <= pageSize) {
        pageSize = totalItens;
        currentPage = 1;
    }

    if (currentPage != 1) {
        addPreviousButton(currentPage, searchKey, pageSize);
    }

    for (var i = 1; i <= Math.ceil(totalItens / pageSize); i++) {

        if (i == currentPage) {
            addActivePaginationButton(i, searchKey, pageSize);
        } else {
            addPaginationButton(i, searchKey, pageSize);
        }
    }

    if (currentPage != Math.ceil(totalItens / pageSize)) {
        addNextButton(currentPage, searchKey, pageSize);
    }
}

function getParams(response) {
    infoPagination = JSON.parse(response);
    //console.log(JSON.parse(response))
}

setSubmitEvent()